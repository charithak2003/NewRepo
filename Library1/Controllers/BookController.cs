
using Library1.Data;
using Library1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library1.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel addBookViewModel)
        {
            if (string.IsNullOrWhiteSpace(addBookViewModel.Title) || string.IsNullOrWhiteSpace(addBookViewModel.PUB_YEAR) || string.IsNullOrWhiteSpace(addBookViewModel.Publisher_Name))
            {
                // If any of the required fields are not filled, return to the view with an error message.
                ModelState.AddModelError("", "All fields are required");
                return View(addBookViewModel);
            }
            var book = new Book
            {
                Title = addBookViewModel.Title,
                PUB_YEAR = addBookViewModel.PUB_YEAR,
                Publisher_Name = addBookViewModel.Publisher_Name,
            };


            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var book = await _context.Books.Include(r => r.Report).ToListAsync();
            return View(book);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var book = await _context.Books.FindAsync(Id);
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            var books = await _context.Books.FindAsync(book.Id);
            if (books != null)
            {
                books.Title = book.Title;
                books.PUB_YEAR = book.PUB_YEAR;
                books.Publisher_Name = book.Publisher_Name;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("GetAllBooks", "Book");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Book book)
        {
            var books = _context.Books.AsNoTracking().FirstOrDefault(x => x.Id == book.Id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("GetAllBooks", "Book");
        }




    }
}
