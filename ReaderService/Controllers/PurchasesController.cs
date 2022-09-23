using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReaderService.DatabaseEntity;

namespace ReaderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly DigitalBooksContext _context;

        public PurchasesController(DigitalBooksContext context)
        {
            _context = context;
        }

        // GET: api/Purchases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchases()
        {
            if (_context.Purchases == null)
            {
                return NotFound();
            }
            return await _context.Purchases.ToListAsync();
        }

        //// GET: api/Purchases/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Purchase>> GetPurchase(int id)
        //{
        //  if (_context.Purchases == null)
        //  {
        //      return NotFound();
        //  }
        //    var purchase = await _context.Purchases.FindAsync(id);

        //    if (purchase == null)
        //    {
        //        return NotFound();
        //    }

        //    return purchase;
        //}

        //// PUT: api/Purchases/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPurchase(int id, Purchase purchase)
        //{
        //    if (id != purchase.PurchaseId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(purchase).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PurchaseExists(id))
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

        // POST: api/Purchases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Purchase>> PostPurchase(Purchase purchase)
        {
            int purchaseId = 0;
            if (_context.Purchases == null)
            {
                return Problem("Entity set 'DigitalBooksContext.Purchases'  is null.");

            }
            var Ispurchased = await _context.Purchases
                .FirstOrDefaultAsync(m => m.BookId == purchase.BookId && m.EmailId == purchase.EmailId);
            if(Ispurchased == null)
            {
                if (_context.Purchases.Count() == 0)
                {
                    purchaseId = 0;
                }
                else
                {
                    purchaseId = _context.Purchases.OrderByDescending(x => x.PurchaseId).Select(x => x.PurchaseId).FirstOrDefault();

                }

                //int userId = 0;
                //if (userlist.Count() > 0)
                //    userId = userlist.Select(x => x.UserId).FirstOrDefault();
                purchase.PurchaseId = Convert.ToInt32(purchaseId) + 1;
                purchase.PurchaseDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                //purchase.PaymentMode = "Online";
                _context.Purchases.Add(purchase);

                
                
            }
            else
            {
                return Ok(new { message = "Already Purchased" });
            }
            
            try
            {
                await _context.SaveChangesAsync();
                bool result = purchase.callPaymentAuzreFunPost();



                if (result)
                    return Ok(new { message = "Purchased Successfully" });
                else
                    //return BadRequest("Something went wrong");
                    return BadRequest(new { message = "Something went wrong" });
            }
            catch (DbUpdateException)
            {
                if (PurchaseExists(purchase.PurchaseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtAction("GetPurchase", new { id = purchase.PurchaseId }, purchase);
            return Ok(new { message = "Purchased Successfully" });
        }

       

        [HttpGet("GetpurchaseHistory/{emailId}")]
        //[Route("SearchBook/{categoryId}/{Author}/{price}")]
        public List<string> GetpurchaseHistory(string emailId)
        {
            List<string> bookList = new List<string>();
            if (_context.Books == null)
            {
                return bookList;
            }
             bookList = (from t1 in _context.Books
                           from t2 in _context.Purchases.Where(x => t1.BookId == x.BookId && x.EmailId == emailId)
                                           //.DefaultIfEmpty()
                           select t1.BookName).ToList();
            //var bookList = await _context.Books.ToListAsync();
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
            if (bookList == null)
            {
                return bookList;
            }
            else
            {
                //foreach (var item in book)
                //{
                //    item.User = null;
                //}
            }
            return bookList;
        }

        [HttpGet("GetBookName/{bookId}")]
        //[Route("SearchBook/{categoryId}/{Author}/{price}")]
        public string GetBookName(int bookId)
        {
            string? bookName = "";
            bookName =  _context.Books.Where(x => x.BookId == bookId).Select(x => x.BookName).FirstOrDefault();
           
            if (bookName == null)
            {
                return bookName;
            }
            else
            {
            }
            return bookName;
        }
        //// DELETE: api/Purchases/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePurchase(int id)
        //{
        //    if (_context.Purchases == null)
        //    {
        //        return NotFound();
        //    }
        //    var purchase = await _context.Purchases.FindAsync(id);
        //    if (purchase == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Purchases.Remove(purchase);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool PurchaseExists(int id)
        {
            return (_context.Purchases?.Any(e => e.PurchaseId == id)).GetValueOrDefault();
        }
    }
}
