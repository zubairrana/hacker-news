namespace HackerNews.Domain.DTO
{
    public class BestStory
    {
        public string Title { get; set; } = string.Empty;
        public string Uri { get; set; } = string.Empty;
        public string PostedBy { get; set; } = string.Empty;
        public DateTime Time { get; set; }
        public long Score { get; set; }
        public long CommentCount { get; set; }
    }
}
