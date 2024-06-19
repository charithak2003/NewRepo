
using Library1.Data;
using Library1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library1.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PublisherController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddPublisherViewModel addPublisherViewModel)
        {
            if (string.IsNullOrWhiteSpace(addPublisherViewModel.Name) || string.IsNullOrWhiteSpace(addPublisherViewModel.Address) || addPublisherViewModel.Phone <= 0)
            {
                // If any of the required fields are not filled, return to the view with an error message.
                ModelState.AddModelError("", "All fields are required");
                return View(addPublisherViewModel);
            }
            var publisher = new Publisher
            {
                Name = addPublisherViewModel.Name,
                Phone = addPublisherViewModel.Phone,
                Address = addPublisherViewModel.Address,
            };


            await _context.Publishers.AddAsync(publisher);
            await _context.SaveChangesAsync();

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPublishers()
        {
            var publisher = await _context.Publishers.ToListAsync();
            return View(publisher);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var publisher = await _context.Publishers.FindAsync(Id);
            return View(publisher);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Publisher publisher)
        {
            var publishers = await _context.Publishers.FindAsync(publisher.Id);
            if (publishers != null)
            {
                publishers.Name = publisher.Name;
                publishers.Address = publisher.Address;
                publishers.Phone = publisher.Phone;

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("GetAllPublishers", "Publisher");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Publisher publisher)
       {
            var publishers = _context.Publishers.AsNoTracking().FirstOrDefault(x => x.Id == publisher.Id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("GetAllPublishers", "Publisher");
        }




    }
}
