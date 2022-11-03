using BusinessAccessLayer.Interface;
using DataAccessLayer.Interface;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public class BookService: IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Book GetBookById(Guid id)
        {
            return _bookRepository.GetBookById(id);
        }

        public List<Book> GetBooks()
        {
            return _bookRepository.GetBooks();
        }

        public void SaveBook(Book book)
        {
            _bookRepository.SaveBook(book);
        }
    }
}
