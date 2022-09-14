using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReaderService.DatabaseEntity;

namespace ReaderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly DigitalBooksContext _context;

        public BooksController(DigitalBooksContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
          if (_context.Books == null)
          {
              return NotFound();
          }
            return await _context.Books.Where(c => c.Active == true).ToListAsync();
        }

        //// GET: api/Books/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Book>> GetBook(int id)
        //{
        //  if (_context.Books == null)
        //  {
        //      return NotFound();
        //  }
        //    var book = await _context.Books.FindAsync(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    return book;
        //}

        // GET: api/Books/5
        [HttpGet("SearchBook/{categoryId}/{Author}/{price}")]
        //[Route("SearchBook/{categoryId}/{Author}/{price}")]
        public async Task<ActionResult<IEnumerable<bookList>>> SearchBook(int categoryId, int Author, decimal price)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            //var userlist = await _context.Users.Where(x => x.UserName.Equals(Author)).ToListAsync();
            //int userId = 0;
            //if (userlist.Count() > 0)
            //    userId = userlist.Select(x => x.UserId).FirstOrDefault();
            //var book = await _context.Books.Where(x => x.CategoryId.Equals(categoryId) || x.UserId == Author || x.Price == price).ToListAsync();
            var book = (from t1 in _context.Books.Where(y => y.Active == true && (y.CategoryId.Equals(categoryId) || y.UserId == Author || y.Price == price)).DefaultIfEmpty()
                         from t3 in _context.Categories.Where(z => z.CategoryId == t1.CategoryId).DefaultIfEmpty()
                         from t4 in _context.Users.Where(z => z.UserId == t1.UserId).DefaultIfEmpty()
                         select new
                         {
                             BookId = t1.BookId,
                             BookName = t1.BookName,
                             Author = t4.FirstName + " " + t4.LastName,
                             Publisher = t1.Publisher,
                             Price = t1.Price,
                             PublishedDate = t1.PublishedDate,
                             CategoryName = t3.CategoryName,
                             Description = t1.Description
                             //EmailId = purchase.EmailId == null? "NA" : purchase.EmailId
                         })
                                        .Select(x => new bookList()
                                        {
                                            BookId = x.BookId,
                                            BookName = x.BookName,
                                            Author = x.Author,
                                            Publisher = x.Publisher,
                                            Price = x.Price,
                                            PublishedDate = x.PublishedDate,
                                            CategoryName = x.CategoryName,
                                            Description = x.Description
                                        }).ToList();
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                //foreach (var item in book)
                //{
                //    item.User = null;
                //}
            }
            return book;
        }

        [HttpGet("GetBookList/{emailId}")]
        //[Route("SearchBook/{categoryId}/{Author}/{price}")]
        public IEnumerable<bookList> GetBookList(string emailId)
        {
            List<bookList> bookLists = new List<bookList>();
            if (_context.Books == null)
            {
                return bookLists;
            }
            //var bookList = await _context.Books.ToListAsync();
            bookLists = (from t1 in _context.Books.Where(y => y.Active == true).DefaultIfEmpty()
                         from t3 in _context.Categories.Where(z => z.CategoryId == t1.CategoryId).DefaultIfEmpty()
                         from t4 in _context.Users.Where(z => z.UserId == t1.UserId).DefaultIfEmpty()
                         from t2 in _context.Purchases.Where(x => t1.BookId == x.BookId && x.EmailId == emailId)
                                             .DefaultIfEmpty()
                                             select new
                                             {
                                                 BookId = t1.BookId,
                                                 BookName = t1.BookName,
                                                 Author = t4.FirstName + " " + t4.LastName,
                                                 Publisher = t1.Publisher,
                                                 Price = t1.Price,
                                                 PublishedDate = t1.PublishedDate,
                                                 CategoryName = t3.CategoryName,
                                                 EmailId = t2.EmailId,
                                                 Description = t1.Description
                                                 //EmailId = purchase.EmailId == null? "NA" : purchase.EmailId
                                             })
                                         .Select(x => new bookList()
                                         {
                                             BookId = x.BookId,
                                             BookName = x.BookName,
                                             Author = x.Author,
                                             Publisher = x.Publisher,
                                             Price = x.Price,
                                             PublishedDate = x.PublishedDate,
                                             CategoryName = x.CategoryName,
                                             EmailId = x.EmailId == null ? "NA" : x.EmailId,
                                             Description = x.Description
                                         }).ToList();

            //bookList = (List<bookList>) _context.Books.Join(_context.Purchases,
            //           books => books.BookId,
            //            purchase => purchase.BookId,        
            //            (books, purchase) => new
            //            {
            //                BookName = books.BookName,
            //                UserId=books.UserId,
            //                Publisher=books.Publisher,
            //                Price = books.Price,
            //                PublishedDate=books.PublishedDate,
            //                CategoryId = books.CategoryId,
            //                //EmailId = purchase.EmailId
            //                //EmailId = purchase.EmailId == null? "NA" : purchase.EmailId
            //            });
            //var bookList = await _context.Purchases.Where(x => x.EmailId.Equals(emailId)).ToListAsync();
            if (bookLists == null)
            {
                return bookLists;
            }
            else
            {
                //foreach (var item in book)
                //{
                //    item.User = null;
                //}
            }
            return bookLists;
        }

        //// PUT: api/Books/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //[Authorize]
        //public async Task<IActionResult> PutBook(int id, Book book)
        //{
        //    if (id != book.BookId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(book).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BookExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Books
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //[Authorize]
        //public async Task<ActionResult<Book>> PostBook(Book book)
        //{
        //  if (_context.Books == null)
        //  {
        //      return Problem("Entity set 'DigitalBooksContext.Books'  is null.");
        //  }
        //    _context.Books.Add(book);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (BookExists(book.BookId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        //}

        //// DELETE: api/Books/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBook(int id)
        //{
        //    if (_context.Books == null)
        //    {
        //        return NotFound();
        //    }
        //    var book = await _context.Books.FindAsync(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Books.Remove(book);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
