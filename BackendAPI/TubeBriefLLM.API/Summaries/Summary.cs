namespace TubeBriefLLM.API.Summaries.Models
{
    public class Summary
    {
        public int Id { get; set; }
        public string YoutubeUrl { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}