using System.Data;

namespace ADO.NET_Demo.Web.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public BookDto()
        {

        }
        public BookDto(DataRow row)
        {
            Name = row.GetStringValue("BOT_BOOK_NAME");
        }
    }
}
