using SEOProject.Models;

namespace SEOProject.Services
{
	public interface ISearchHistoryService
	{
		Task AddSearchHistory(SearchHistoryItem searchHistoryItem);
		Task DeleteSearchHistory();
		Task DeleteSearchHistoryById(int id);
		Task<IEnumerable<SearchHistoryItem>> GetSearchHistory();
	}
}