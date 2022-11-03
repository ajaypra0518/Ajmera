using DataAccessLayer.Interface;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _context;
        public BookRepository(BookDbContext context)
        {
            _context = context;
        }
    
        public Book GetBookById(Guid id)
        {
           return _context.Books.Where(x => x.Id == id).SingleOrDefault();
        }

        public List<Book> GetBooks()
        {
            return _context.Books.ToList();
        }

        public void SaveBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }
    }
}
