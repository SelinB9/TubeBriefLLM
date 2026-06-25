using Microsoft.AspNetCore.Mvc;
using TubeBriefLLM.API.Summaries.DTOs;
using TubeBriefLLM.API.Summaries.Services;

namespace TubeBriefLLM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SummaryController : ControllerBase
    {
        private readonly ISummaryService _summaryService;

        public SummaryController(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        [HttpPost]
        public async Task<IActionResult> Summarize([FromBody] CreateSummaryDto createDto)
        {
            var result = await _summaryService.GetSummaryAsync(createDto);
            // Servis katmanından gelen "Hata" başlığını kontrol ediyoruz
            if (result.Title == "Hata")
            {
                // Eğer Title "Hata" ise 400 Bad Request döndür
                return BadRequest(result);
            }

            // Aksi takdirde başarılıdır, 200 OK döndür
            return Ok(result);
        }
    }
}