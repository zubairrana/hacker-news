namespace HackerNews.Domain.DTO
{
    public class BaseResponse
    {
        public int PageNumber { get; set; }
        public List<BestStory> BestStories { get; set; } = new List<BestStory>();
    }
}
