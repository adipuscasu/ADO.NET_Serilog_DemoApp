using System.Collections.Generic;
using System.Threading.Tasks;
using ADO.NET_Demo.Web.Models;

namespace ADO.NET_Demo.Web.DataAccess
{
    public interface IBooksRepo
    {
        Task<IEnumerable<BookDto>> FetchBooks();
    }
}