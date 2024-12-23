using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    /// <summary>
    /// Implementation of IAuthorService.
    /// </summary>
    public class AuthorService : IAuthorService
    {

        private ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public Task<Author> GetAuthor()
        {
            return _context.Authors
                .Where(a => a.Books.Max(ab => ab.Title.Length) == _context.Books.Max(b => b.Title.Length))
                .OrderBy(a => a.Id)
                .FirstAsync();
        }

        public Task<List<Author>> GetAuthors()
        {
            return _context.Authors
                .Where(a => a.Books.Where(b => b.PublishDate.Year > 2015).Count() % 2 == 0)
                .ToListAsync();
        }
    }
}
