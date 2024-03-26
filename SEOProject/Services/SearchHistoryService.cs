using Microsoft.Data.SqlClient;
using SEOProject.Models;

namespace SEOProject.Services
{
    public class SearchHistoryService(IConfiguration configuration) : ISearchHistoryService
    {
	    public Task AddSearchHistory(SearchHistoryItem searchHistoryItem)
        {
            AddSearch(searchHistoryItem);
            return Task.CompletedTask;
        }

        public async Task DeleteSearchHistory()
        {
            try
            {
                var conn = GetConnection();
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                var sqlCommandText = "DELETE FROM [dbo].[Histories]";
                var sqlCommand = new SqlCommand(sqlCommandText, conn);

                await sqlCommand.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while deleting search history.", ex);
            }
        }

        public async Task DeleteSearchHistoryById(int id)
        {
            try
            {
                var conn = GetConnection();
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                const string sqlCommandText = "DELETE FROM [dbo].[Histories] WHERE HistoryId = @Id";

                var sqlCommand = new SqlCommand(sqlCommandText, conn);

                sqlCommand.Parameters.AddWithValue("@Id", id);

                var rowsAffected = await sqlCommand.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    throw new Exception($"No history item found with ID {id}.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting search history item.", ex);
            }
        }

        public Task<IEnumerable<SearchHistoryItem>> GetSearchHistory()
        {
            var data = GetSearch();

            return Task.FromResult(data);
        }

        private SqlConnection GetConnection()
        {
            var connString = configuration.GetConnectionString("DbConnection");

            return new SqlConnection(connString);
        }

        private bool AddSearch(SearchHistoryItem searchHistory)
        {
            try
            {
                var conn = GetConnection();
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                var sqlCommandText = $"INSERT INTO [dbo].[Histories] ([Keyword] ,[Url] ,[Position] ,[SearchTime] ,[SearchEngine]) VALUES ('{searchHistory.Keyword}', '{searchHistory.URL}', '{searchHistory.Position}', '{searchHistory.SearchTime.ToString("yyyy-MM-dd HH:mm:ss")}', '{searchHistory.SearchEngine}')";

                var sqlCommand = new SqlCommand(sqlCommandText, conn);
                var result = sqlCommand.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private IEnumerable<SearchHistoryItem> GetSearch()
        {
            List<SearchHistoryItem> searchHistoryItems = new List<SearchHistoryItem>();
            try
            {
                var conn = GetConnection();
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                const string sqlCommandText = "SELECT TOP (5) * FROM dbo.Histories ORDER BY SearchTime DESC";

                var sqlCommand = new SqlCommand(sqlCommandText, conn);
                var reader = sqlCommand.ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new SearchHistoryItem
                        {
	                        Id = reader.GetInt32(0),
							Keyword = reader.GetString(1),
                            URL = reader.GetString(2),
                            Position = reader.GetString(3),
                            SearchTime = reader.GetDateTime(4),
                            SearchEngine = reader.GetString(5),
                        };

                        searchHistoryItems.Add(item);
                    }
                }
                return searchHistoryItems;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}