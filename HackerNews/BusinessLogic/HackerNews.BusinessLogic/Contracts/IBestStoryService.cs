using HackerNews.Domain.DTO;

namespace HackerNews.BusinessLogic.Contracts
{
    public interface IBestStoryService
    {
        Task<List<BestStory>> GetBestStoriesAsync(int count);
    }
}
