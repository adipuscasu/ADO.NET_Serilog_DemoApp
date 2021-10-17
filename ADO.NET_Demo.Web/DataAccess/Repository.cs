using ADO.NET_Demo.Web.Extensions;
using ADO.NET_Demo.Web.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ADO.NET_Demo.Web.DataAccess
{
    public abstract class Repository
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly ILogger<Repository> _logger;

        public Repository(
            IOptions<ConnectionStrings> options,
            ILogger<Repository> logger
            )
        {
            _connectionStrings = options.Value;
            _logger = logger;
        }

        protected async Task<DataTable> FetchDataTable(string commandQuery)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            using (SqlConnection connection = new SqlConnection(_connectionStrings.AsyncDemo))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(commandQuery, connection))
                    {
                        connection.StatisticsEnabled = true;
                        adapter.SelectCommand = command;
                        await connection.OpenAsync();
                        adapter.Fill(dt);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw;
                }
                finally
                {
                    adapter.Dispose();
                }
                var stats = connection.RetrieveStatistics();
                LogInfo("GetRecords: " + typeof(DataTable).Name, stats, commandQuery);
                return dt;
            }
        }

        protected async Task<IEnumerable<T>> FetchCollection<T>(string commandQuery) where T : class, new()
        {
            var result = new List<T>();
            using (SqlConnection connection = new SqlConnection(_connectionStrings.AsyncDemo))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(commandQuery, connection))
                    {
                        await connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            var item = reader.ConvertToObject<T>();
                            result.Add(item);
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw;
                }

            }
            return result;
        }

        private void LogInfo(string logPrefix, IDictionary stats, string sql, object parameters = null)
        {
            long elapsedMilliseconds = (long)stats["ConnectionTime"];

            Log.ForContext("SQL", sql)
            .ForContext("Parameters", parameters)
            .ForContext("ExecutionTime", stats["ExecutionTime"])
            .ForContext("NetworkServerTime", stats["NetworkServerTime"])
            .ForContext("BytesSent", stats["BytesSent"])
            .ForContext("BytesReceived", stats["BytesReceived"])
            .ForContext("SelectRows", stats["SelectRows"])
            .Information("{logPrefix} in {ElaspedTime:0.0000} ms", logPrefix, elapsedMilliseconds);
        }

        
    }
}
