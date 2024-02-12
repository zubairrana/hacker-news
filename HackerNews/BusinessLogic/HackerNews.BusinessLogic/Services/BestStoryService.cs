using Dasync.Collections;
using HackerNews.BusinessLogic.Contracts;
using HackerNews.Domain.Constants;
using HackerNews.Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;

namespace HackerNews.BusinessLogic.Services
{
    public class BestStoryService : IBestStoryService
    {
        private readonly IMemoryCache _memoryCache;

        public BestStoryService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }


        public async Task<List<BestStory>> GetBestStoriesAsync(int count)
        {
            var storyIds = await GetAsync<List<long>>(string.Format(HackerNewsAPIConstants.HackerNewsApiBaseUrl, HackerNewsAPIConstants.BestStoriesIdsEndpoint));
            if (storyIds != null)
            {
                var stories = new List<BestStory>();
                await storyIds.Take(count).ParallelForEachAsync(async storyId =>
                {
                    var story = await GetBestStoryDetailAsync(storyId);

                    if (story != null)
                    {
                        stories.Add(story);
                    }
                });

                return stories;
            }

            return new List<BestStory>();
        }

        private async Task<T?> GetAsync<T>(string url)
        {
            var httpClient = new HttpClient();
            var httpResponse = await httpClient.GetAsync(url);
            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadFromJsonAsync<T>();
            }

            return default;
        }

        private async Task<BestStory?> GetBestStoryDetailAsync(long storyId)
        {
            if (!(_memoryCache.TryGetValue(storyId, out BestStory? bestStory) && bestStory != null))
            {
                bestStory = await GetAsync<BestStory>(string.Format(HackerNewsAPIConstants.HackerNewsApiBaseUrl, string.Format(HackerNewsAPIConstants.BestStoryDetailsEndpoint, storyId)));
                _memoryCache.Set(storyId, bestStory, TimeSpan.FromSeconds(60));
            }

            return bestStory;

        }
    }
}
