# ⚡ TubeBrief

TubeBrief, YouTube videolarını hızlıca özetlemenizi sağlayan, yapay zeka destekli modern bir web uygulamasıdır. Uzun videoları izlemek için saatler harcamanıza gerek yok; videonun en önemli noktalarını saniyeler içinde öğrenin.

# 🚀 Proje Hakkında

Bu proje, kullanıcıların YouTube video linklerini girerek içeriğin ana hatlarına ulaşmasını sağlayan Full-Stack bir çözümdür. OpenRouter API üzerinden güçlü LLM modelleriyle etkileşime girerek döküm analizleri yapar ve karmaşık içerikleri özetler.

# 🛠️ Teknik Yetenekler

Frontend: React.js, Responsive CSS (Grid & Flexbox mimarisi).

Backend: .NET Web API (OpenRouter API entegrasyonu ile LLM destekli içerik analizi).

Veri Yönetimi: LocalStorage (Geçmiş özetlerin tarayıcı bazlı takibi).

Arayüz Tasarımı: Modüler CSS yaklaşımı ile temiz bir UI/UX.

# ✨ Temel Özellikler

Hızlı Özetleme: Videonun metin dökümünü analiz ederek en önemli 5 noktayı listeler.

Dashboard Görünümü: Geçmiş özetlerinizi 2x2 matris yapısında, düzenli ve kompakt bir panelde görüntüler.

Akıllı Yerleşim: CSS Grid ile her ekran boyutuna uyumlu, kaydırmalı (scrollable) içerik kartları.

Modern Navigasyon: Sabit navbar ve katman yönetimi (z-index) ile kesintisiz kullanıcı deneyimi.

# 📸 Görünüm

Anasayfa - Link ekleme 

<img width="397" height="172" alt="Anasayfa" src="https://github.com/user-attachments/assets/aaaeadba-6b62-4f5b-ab26-a54b9219d6cc" /> <img width="382" height="172" alt="Anasayfa-2" src="https://github.com/user-attachments/assets/ef03bd72-9c86-4703-be19-9de4de522648" />



Anasayfa özet kartı / History

<div <img width="387" height="172" alt="Anasayfa-3" src="https://github.com/user-attachments/assets/ebdf061c-2bc8-4806-ab2b-57301fec4594" />
<img width="392" height="172" alt="History" src="https://github.com/user-attachments/assets/53b6c9a0-f2e0-40bb-a2e4-e01589357a2d" /> /> </div>

# 💻 Kurulum

Projenin hem Frontend (React) hem de Backend (.NET) kısmını çalıştırmak için aşağıdaki adımları takip edin:

# 1. Frontend Kurulumu (React)

Bash(Proje dizinine gidin)

cd TubeBriefLLM
npm install
npm start

# 2. Backend Kurulumu (.NET)

Bash (Backend dizinine geçin)

cd BackendAPI/TubeBriefLLM.API
dotnet restore
dotnet run

Not: Backend'in düzgün çalışması için appsettings.json dosyanızda OpenRouter API Key tanımlı olduğundan emin olun.

🏗️ Gelecek Planları

[ ] Kullanıcı hesap yönetimi (Auth) entegrasyonu.

[ ] Özetlerin PDF olarak dışa aktarılması.

[ ] Çoklu dil desteği.

\*\*Bu proje, .NET ve React tabanlı modern bir full-stack mimarisinde, Büyük Dil Modellerinin (LLM Large Language Model) gerçek dünya senaryolarına nasıl entegre edilebileceğini uygulamalı olarak göstermek amacıyla geliştirilmiştir.
