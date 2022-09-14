using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthorService.DatabaseEntity;
using Microsoft.AspNetCore.Authorization;
using ReaderService.DatabaseEntity;

namespace AuthorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorBooksController : ControllerBase
    {
        private readonly DigitalBooksContext _context;

        public AuthorBooksController(DigitalBooksContext context)
        {
            _context = context;
        }

        // GET: api/AuthorBooks
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
          if (_context.Books == null)
          {
              return NotFound();
          }
            //return await _context.Books.Where(c => c.Active == true).Select(c => c.User1.UserID).ToListAsync();
            return await _context.Books.Where(c => c.Active == true).ToListAsync();
        }

        // GET: api/AuthorBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

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
            var book = (from t1 in _context.Books.Where(y => y.CategoryId.Equals(categoryId) || y.UserId == Author || y.Price == price).DefaultIfEmpty()
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
                            Description = t1.Description,
                            Active = t1.Active
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
                                            Description = x.Description,
                                            Active = x.Active
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

        // PUT: api/AuthorBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateBook/{id}/{userId}")]
        //[Route("UpdateBook")]
        [Authorize]
        public async Task<IActionResult> PutBook(int id,int userId, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }
            var data = await _context.Books.FirstOrDefaultAsync(x => x.BookId == id);
            if (data != null)
            {
                data.BookName = book.BookName;
                data.CategoryId = book.CategoryId;
                data.Price = book.Price;
                data.Publisher = book.Publisher;
                data.PublishedDate = book.PublishedDate;
                data.UserId = book.UserId;
                data.Description = book.Description;
                data.Active = book.Active;
                data.ModifiedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                data.Modifiedby = userId;

            }
            //_context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Modified");
        }

        // POST: api/AuthorBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost("{userId}")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            int bookId = 0;
          if (_context.Books == null)
          {
              return Problem("Entity set 'DigitalBooksContext.Books'  is null.");
          }
            if (_context.Books.Count() == 0)
            {
                bookId = 0;
            }
            else
            {
                bookId = _context.Books.OrderByDescending(x => x.BookId).Select(x => x.BookId).FirstOrDefault();
            }
            
            //_context.Books.Add(book);
            //var data = await _context.Books.FirstOrDefaultAsync();
           
            //if (data != null)
            //{
                book.BookId = Convert.ToInt32(bookId) + 1;
                //data.BookName = book.BookName;
                //data.CategoryId = book.CategoryId;
                //data.Price = book.Price;
                //data.Publisher = book.Publisher;
                //data.PublishedDate = book.PublishedDate;
                //data.UserId = book.UserId;
                //data.Description = book.Description;
                //data.Active = book.Active;
                book.CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                book.Createdby = book.UserId;
                book.ModifiedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                book.Modifiedby = book.UserId;
                _context.Books.Add(book);

            //}
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookExists(book.BookId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtAction("GetBook", new { id = book.BookId }, book);
            return Ok(new { message = "Added Successfully" });
        }

        // DELETE: api/AuthorBooks/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok("Deleted");
        }

        [HttpPut("BlockBook/{id}/{userId}")]
        [Authorize]
        public async Task<IActionResult> BlockBook(int id, int userId)
        {
            var data = await _context.Books.FirstOrDefaultAsync(x => x.BookId == id);
            if (data != null)
            {
                data.Active = false;
                data.ModifiedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                data.Modifiedby = userId;

            }
            else
            {
                return BadRequest();
            }
            //_context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return Ok("Blocked");
            return Ok(new { message = "Blocked" });
        }

        [HttpPut("UnBlockBook/{id}/{userId}")]
        [Authorize]
        public async Task<IActionResult> UnBlockBook(int id, int userId)
        {
            var data = await _context.Books.FirstOrDefaultAsync(x => x.BookId == id);
            if (data != null)
            {
                data.Active = true;
                data.ModifiedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                data.Modifiedby = userId;

            }
            else
            {
                return BadRequest();
            }
            //_context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return Ok("UnBlocked");
            return Ok(new { message = "UnBlocked" });
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
