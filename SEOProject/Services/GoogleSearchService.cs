using System.Text.RegularExpressions;

namespace SEOProject.Services
{
	public class GoogleSearchService : IGoogleSearchService
	{
		private readonly HttpClient _httpClient = new();

		public async Task<string> GetSearchPosition(string keyword, string url)
		{
			try
			{
				var searchUrl = $"https://www.google.co.uk/search?num=100&q={keyword}";
				var response = await _httpClient.GetAsync(searchUrl);

				if (!response.IsSuccessStatusCode)
				{
					throw new Exception("Failed to fetch search results from Google.");
				}

				var content = await response.Content.ReadAsStringAsync();
				var matches = Regex.Matches(content, @"<a\s+href=""/url\?q=https?:\/\/([^&""]+)", RegexOptions.IgnoreCase);
				var urls = new List<string>();

				foreach (var match in matches.Cast<Match>())
				{
					var urlString = match.Groups[1].Value;

					if (urlString.Contains("maps.google")) continue;
					urls.Add(urlString);
					if (urls.Count == 100)
						break;
				}

				var infoTrackIndices = new List<int>();

				for (var i = 0; i < urls.Count; i++)
				{
					if (urls[i].Contains(url))
					{
						infoTrackIndices.Add(i + 1);
					}
				}

				var positions = infoTrackIndices.Count > 0 ? string.Join(", ", infoTrackIndices) : "Unable to find the URL in the top 100 results of the webpage.";

				return positions;
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while scraping Google search results.", ex);
			}
		}
	}
}