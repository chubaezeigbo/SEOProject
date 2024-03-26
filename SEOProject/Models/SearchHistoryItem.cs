using System;

namespace SEOProject.Models
{
    public class SearchHistoryItem
	{
		public int Id { get; set; }
		public string Keyword { get; set; }
		public string URL { get; set; }
		public string Position { get; set; }
		public DateTime SearchTime { get; set; }
		public string SearchEngine { get; set; }
	}
}