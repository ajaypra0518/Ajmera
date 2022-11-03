using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Interface
{
    public interface IBookService
    {
        List<Book> GetBooks();
        Book GetBookById(Guid id);
        void SaveBook(Book book);
    }
}
