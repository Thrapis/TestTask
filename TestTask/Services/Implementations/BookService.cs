using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    /// <summary>
    /// Implementation of IBookService.
    /// </summary>
    public class BookService : IBookService
    {
        private DateTime _carolusRexOfSabatonPublicationDateTime = new(2012, 5, 25);

        private ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Book> GetBook()
        {
            return _context.Books
                .Where(b => _context.Books.Max(tb => tb.Price * tb.QuantityPublished) ==  b.Price * b.QuantityPublished)
                .FirstAsync();
        }

        public Task<List<Book>> GetBooks()
        {
            return _context.Books
                .Where(b => b.Title.Contains("Red") && b.PublishDate > _carolusRexOfSabatonPublicationDateTime)
                .ToListAsync();
        }
    }
}
