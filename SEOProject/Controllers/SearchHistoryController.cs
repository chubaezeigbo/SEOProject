using Microsoft.AspNetCore.Mvc;
using SEOProject.Services;

namespace SEOProject.Controllers
{
	[ApiController]
	[Route("search/history")]
	public class SearchHistoryController(
		ILogger<SearchController> logger,
		ISearchHistoryService searchHistoryService)
		: ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetSearchHistory()
		{
			try
			{
				var searchHistory = await searchHistoryService.GetSearchHistory();
				return Ok(searchHistory);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred while retrieving search history.");
				return StatusCode(500, "An error occurred while retrieving search history.");
			}
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteSearchHistory()
		{
			try
			{
				await searchHistoryService.DeleteSearchHistory();
				return Ok("Search history deleted successfully.");
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred while deleting search history.");
				return StatusCode(500, "An error occurred while deleting search history.");
			}
		}

		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteSearchHistory(int id)
		{
			try
			{
				await searchHistoryService.DeleteSearchHistoryById(id);
				return Ok("Search history item deleted successfully.");
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred while deleting search history item.");
				return StatusCode(500, "An error occurred while deleting search history item.");
			}
		}
	}
}
