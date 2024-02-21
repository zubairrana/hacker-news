using AutoMapper;
using Dasync.Collections;
using HackerNews.BusinessLogic.Contracts;
using HackerNews.Domain.Constants;
using HackerNews.Domain.DTO;
using HackerNews.Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;

namespace HackerNews.BusinessLogic.Services
{
    public class BestStoryService : IBestStoryService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;
        public BestStoryService(IMemoryCache memoryCache, IMapper mapper)
        {
            _memoryCache = memoryCache;
            _mapper = mapper;
        }

        public async Task<List<BestStory>> GetBestStoriesAsync(int count, string title, int pageNumber)
        {
            var storyIds = await GetAsync<List<long>>(string.Format(HackerNewsAPIConstants.HackerNewsApiBaseUrl, HackerNewsAPIConstants.BestStoriesIdsEndpoint));
            if (storyIds != null)
            {
                var stories = new List<BestStoryModel>();
                int storiesToReturn = HackerNewsAPIConstants.PageSize;
                int storiesToSkip = (pageNumber - 1) * storiesToReturn;
                int skippedStories = 0;

                if (count < HackerNewsAPIConstants.PageSize)
                {
                    storiesToReturn = count;
                }

                foreach (var storyId in storyIds)
                {
                    var story = await GetBestStoryDetailAsync(storyId);

                    if (story != null && (string.IsNullOrEmpty(title) || story.Title.Contains(title)))
                    {
                        if (skippedStories >= storiesToSkip)
                        {
                            stories.Add(story);
                        }
                        else
                        {
                            skippedStories++;
                        }
                    }

                    if (stories.Count == storiesToReturn)
                    {
                        break;
                    }
                }

                //await storyIds.Take(count).ParallelForEachAsync(async storyId =>
                //{
                //    var story = await GetBestStoryDetailAsync(storyId);

                //    if (story != null)
                //    {
                //        stories.Add(story);
                //    }
                //});

                return _mapper.Map<List<BestStory>>(stories.OrderByDescending(x=>x.Score));
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

        private async Task<BestStoryModel?> GetBestStoryDetailAsync(long storyId)
        {
            if (!(_memoryCache.TryGetValue(storyId, out BestStoryModel? bestStory) && bestStory != null))
            {
                bestStory = await GetAsync<BestStoryModel>(
                    string.Format(HackerNewsAPIConstants.HackerNewsApiBaseUrl, 
                    string.Format(HackerNewsAPIConstants.BestStoryDetailsEndpoint, storyId))
                    );
                _memoryCache.Set(storyId, bestStory, TimeSpan.FromSeconds(60));
            }

            return bestStory;

        }
    }
}
