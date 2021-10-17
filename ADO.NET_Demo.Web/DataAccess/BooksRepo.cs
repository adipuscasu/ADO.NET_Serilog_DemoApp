using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ADO.NET_Demo.Web.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;

namespace ADO.NET_Demo.Web.DataAccess
{
    public class BooksRepo : Repository, IBooksRepo
    {
        public BooksRepo(
            IOptions<ConnectionStrings> options,
            ILogger<BooksRepo> logger
            ) : base(options, logger)
        {

        }

        public async Task<IEnumerable<BookDto>> FetchBooks()
        {

            var select = $@"SELECT TOP (1000) [Id]
                            ,[Name]
                            FROM [dbo].[Book]";

            var books = await FetchCollection<BookDto>(select);
            
            //var books = new List<BookDto>();

            //if (dt.Rows.Count == 0)
            //{
            //    return books;
            //}

            //books.AddRange(from DataRow row in dt.Rows
            //               select new BookDto(row));

            return books.ToList();
        }
    }
}

