using System.Text.RegularExpressions;

namespace SEOProject.Services
{
	public class BingSearchService : IBingSearchService
	{
		private readonly HttpClient _httpClient = new();

		public async Task<string> GetSearchPosition(string keyword, string url)
		{
			try
			{
				var searchUrl = $"https://bing.com/search?num=100&q={keyword}";
				var response = await _httpClient.GetAsync(searchUrl);

				if (!response.IsSuccessStatusCode)
				{
					throw new Exception("Failed to fetch search results from Bing.");
				}

				var content = await response.Content.ReadAsStringAsync();
				var matches = Regex.Matches(content, @"<cite>https://.*?(?=<\/cite>)", RegexOptions.IgnoreCase);
				var modifiedMatches = new List<string>();

				foreach (Match match in matches)
				{
					var matchContent = match.Value;

					var contentWithoutStrongTags = Regex.Replace(matchContent, @"<strong>(.*?)</strong>", "$1");

					modifiedMatches.Add(contentWithoutStrongTags);
					if (modifiedMatches.Count == 100)
						break;
				}

				var infoTrackIndices = new List<int>();

				for (var i = 0; i < modifiedMatches.Count; i++)
				{
					if (modifiedMatches[i].Contains(url))
					{
						infoTrackIndices.Add(i + 1);
					}
				}

				var positions = infoTrackIndices.Count > 0 ? string.Join(", ", infoTrackIndices) : "Unable to find the URL within the rendered webpage, please retry.";

				return positions;
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while scraping Bing search results.", ex);
			}
		}
	}
}