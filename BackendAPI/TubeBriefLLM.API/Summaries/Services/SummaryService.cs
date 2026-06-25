using Microsoft.Extensions.Configuration;
using TubeBriefLLM.API.Summaries.DTOs;
using YoutubeExplode;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;



namespace TubeBriefLLM.API.Summaries.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly YoutubeClient _youtubeClient;
        private readonly IConfiguration _configuration;

        public SummaryService(IConfiguration configuration)
        {
            _youtubeClient = new YoutubeClient();
            _configuration = configuration;
        }

        public async Task<DetailSummaryDto> GetSummaryAsync(CreateSummaryDto createDto)
        {
            try
            {
                // 1-Girdi kontrolü
                var videoId = YoutubeExplode.Videos.VideoId.TryParse(createDto.YoutubeUrl);
                if (videoId == null) throw new Exception("Please enter a valid YouTube link!");

                // Burası önemli: Hem başlığı almak için hem de transkript için videoyu çekiyoruz
                var video = await _youtubeClient.Videos.GetAsync(videoId.Value);

                //2-YouTube dan bilgi alma
                var transcriptClient = _youtubeClient.Videos.ClosedCaptions;
                var manifest = await transcriptClient.GetManifestAsync(videoId.Value);
                var trackInfo = manifest.Tracks.FirstOrDefault();// Türkçe veya İngilizce altyazıyı seçer

                if (trackInfo == null) throw new Exception("Bu videoda altyazı bulunamadı!");

                var transcript = await transcriptClient.GetAsync(trackInfo);

                var fullText = "";

                if (transcript?.Captions != null && transcript.Captions.Any())
                {
                    // Önceki satırını bununla değiştir
                    fullText = string.Join(" ", transcript.Captions.Select(c => c.Text.Replace("\n", " ").Trim()));
                }

                // fallback 1: caption boşsa video açıklaması
                if (string.IsNullOrWhiteSpace(fullText))
                {
                    fullText = video.Description ?? "";
                }

                // fallback 2: her şey boşsa direkt dön
                if (string.IsNullOrWhiteSpace(fullText))
                {
                    return new DetailSummaryDto
                    {
                        Title = video.Title,
                        Content = "Bu video için özet üretilemedi (altyazı ve açıklama yok)."
                    };
                }

                // 3-Yapay Zeka ile Özetleme
                var summary = await GenerateSummaryWithOpenRouter(fullText);

                return new DetailSummaryDto
                {
                    Title = video.Title,
                    Content = summary
                };
            }
            catch (Exception ex)
            {
                // Hatalı link girildiğinde 500 yerine anlamlı bir hata dönüyoruz
                return new DetailSummaryDto { Title = "Hata", Content = ex.Message };
            }
        }


        //OpenRouter ile AI özetleme( metodunu ekledik.)
        private async Task<string> GenerateSummaryWithOpenRouter(string text)
        {
            // Senin geçerli OpenRouter API Anahtarın
            var apiKey = _configuration["OpenRouter:ApiKey"];

            // Eğer anahtar bulunamazsa uygulamayı durdur ve hata ver
            if (string.IsNullOrEmpty(apiKey))
            {
                return "Hata: API anahtarı bulunamadı, .env dosyanı kontrol et!";
            }

            using var httpClient = new HttpClient();

            // Header tanımlamalarını OpenRouter standartlarına tam uyumlu yapıyoruz
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("HTTP-Referer", "http://localhost:3000");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-Title", "TubeBriefLLM");

            var safeText = text.Length > 700 ? text.Substring(0, 700) : text;


            var payload = new
            {
                model = "openai/gpt-4o-mini",
                max_tokens = 250,

                messages = new[]
                {
            new
            {
                role = "user",
content = $"""
Sen bir içerik analiz uzmanısın. Aşağıdaki YouTube transkriptini belirtilen kurallara tam uyarak özetle:

KURALLAR:
1. Toplamda sadece 5 adet madde yaz.
2. Maddelerin başına numaralar ekleme. Sadece metni yaz.
3. Maddelerden sonra bir boş satır bırak ve en alta "Kısa Özet: " etiketiyle başlayarak 2-3 cümlelik bir genel özet yaz.
4. Maddelerin veya özeti niteleyen herhangi bir başlık ("Top 5 Key Points", "**5 Maddelik Özet**" vb.) kullanma.
5. Markdown veya kalın yazı (bold) kullanma.
6. Altyazı metnini türkçeye çevir ve cümleleri türkçe ver 



TRANSKRİPT:
{safeText}
"""
            }
        },

                temperature = 0.3
            };


            var url = "https://openrouter.ai/api/v1/chat/completions";
            var jsonContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            //1-istek başarılı mı?
            var response = await httpClient.PostAsync(url, jsonContent);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return $"OpenRouter HTTP Hatası ({response.StatusCode}): {responseString}";
            }
            //2-json dönüşümü ve hata kontrolü
            using var doc = JsonDocument.Parse(responseString);

            if (doc.RootElement.TryGetProperty("error", out var error))
            {
                return $"OpenRouter API Hatası: {error.GetProperty("message").GetString()}";
            }

            // 3. BAŞARILI SONUCU DÖN
            // Burada choices var mı diye kontrol edelim (güvenli yöntem)
            if (doc.RootElement.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
            {
                return choices[0].GetProperty("message").GetProperty("content").GetString()
                       ?? "İçerik boş döndü.";
            }

            return "Yanıt içerisinde 'choices' alanı bulunamadı.";

        }
    }


}

