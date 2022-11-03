using DataAccessLayer.Models;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Interface
{
    public interface IBookRepository
    {
        List<Book> GetBooks();
        Book GetBookById(Guid id);
        void SaveBook(Book book);
    }
}

