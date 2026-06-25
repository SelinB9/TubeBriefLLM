using TubeBriefLLM.API.Summaries.DTOs;

namespace TubeBriefLLM.API.Summaries.Services
{
    public interface ISummaryService
    {
        Task<DetailSummaryDto> GetSummaryAsync(CreateSummaryDto createDto);
    }
}