namespace SEOProject.Services
{
	public interface ISearchService
	{
		Task<string> GetSearchPosition(string keyword, string url);
	}
}