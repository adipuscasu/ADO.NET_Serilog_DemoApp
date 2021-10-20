using System.Threading.Tasks;
using ADO.NET_Demo.Web.Controllers;

namespace ADO.NET_Demo.Web.Services
{
    public interface ILoginService
    {
        string BuildToken(LoginCredentials loginCredentials);
        Task IsLoginValidAsync(LoginCredentials loginCredentials);
    }
}