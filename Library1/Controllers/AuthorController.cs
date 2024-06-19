using Library1.Data;
using Library1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library1.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthorController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddAuthorViewModel addAuthorViewModel)
        {
            if (string.IsNullOrWhiteSpace(addAuthorViewModel.Author_Name))
            {
                // If any of the required fields are not filled, return to the view with an error message.
                ModelState.AddModelError("", "All fields are required");
                return View(addAuthorViewModel);
            }
            var author = new Author
            {
                Author_Name = addAuthorViewModel.Author_Name,
            };


            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var author = await _context.Authors.ToListAsync();
            return View(author);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var author = await _context.Authors.FindAsync(Id);
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Author author)
        {
            var authors = await _context.Authors.FindAsync(author.Id);
            if (authors != null)
            {
                authors.Author_Name = author.Author_Name;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("GetAllAuthors", "Author");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Author author)
        {
            var authors = _context.Authors.AsNoTracking().FirstOrDefault(x => x.Id == author.Id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("GetAllAuthors", "Author");
        }
      




    }
}
