using Microsoft.AspNetCore.Mvc;
using SEOProject.Models;
using SEOProject.Services;

namespace SEOProject.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SearchController(
		ILogger<SearchController> logger,
		IGoogleSearchService googleSearchService,
		IBingSearchService bingSearchService,
		ISearchHistoryService searchHistoryService)
		: ControllerBase
	{
		[HttpGet]
		[Route(("google/{keyword}/{url}"))]
		public async Task<IActionResult> GoogleSearch(string keyword, string url)
		{
			try
			{
				var position = await googleSearchService.GetSearchPosition(keyword, url);
				var searchHistoryItem = new SearchHistoryItem
				{
					Keyword = keyword,
					URL = url,
					Position = position,
					SearchTime = DateTime.Now,
					SearchEngine = "Google"
				};
				await searchHistoryService.AddSearchHistory(searchHistoryItem);
				return Ok(searchHistoryItem);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred while processing Google search request.");
				return StatusCode(500, $"An error occurred while processing Google search request. {ex.Message}");
			}
		}

		[HttpGet]
		[Route("bing/{keyword}/{url}")]
		public async Task<IActionResult> BingSearch(string keyword, string url)
		{
			try
			{
				var position = await bingSearchService.GetSearchPosition(keyword, url);
				var searchHistoryItem = new SearchHistoryItem
				{
					Keyword = keyword,
					URL = url,
					Position = position,
					SearchTime = DateTime.Now,
					SearchEngine = "Bing"
				};
				await searchHistoryService.AddSearchHistory(searchHistoryItem);
				return Ok(searchHistoryItem);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred while processing Bing search request.");

                return StatusCode(500, $"An error occurred while processing Bing search request. {ex.Message}");
			}
		}
	}
}
