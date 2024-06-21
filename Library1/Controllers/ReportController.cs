using Library1.Data;
using Library1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Library1.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddReportViewModel addReportViewModel)
        {
            if (addReportViewModel.Student_Id <= 0 || addReportViewModel.Book_Id <= 0 ||
                addReportViewModel.Penalty <= 0)
            {
                // If any of the required fields are not filled, return to the view with an error message.
                ModelState.AddModelError("", "All fields are required");
                return View(addReportViewModel);
            }
            var report = new Report
            {
                Student_Id = addReportViewModel.Student_Id,
                Book_Id = addReportViewModel.Book_Id,
                Issue_date = addReportViewModel.Issue_date,
                Return_date = addReportViewModel.Return_date,
                Penalty = addReportViewModel.Penalty,
            };

            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();

            return RedirectToAction("GetAllReports", "Report");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await _context.Reports.Include(r => r.Student).Include(r => r.Book).ToListAsync();
            return View(reports);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var report = await _context.Reports.FindAsync(Id);
            return View(report);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Report report)
        {

            var existingReport = await _context.Reports.FindAsync(report.Id);
            if (existingReport == null)
            {
                return NotFound();
            }

            existingReport.Student_Id = report.Student_Id;
            existingReport.Book_Id = report.Book_Id;
            existingReport.Issue_date = report.Issue_date;
            existingReport.Return_date = report.Return_date;
            existingReport.Penalty = report.Penalty;

            await _context.SaveChangesAsync();
            return RedirectToAction("GetAllReports", "Report");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Report report)
        {
            var reports = _context.Reports.AsNoTracking().FirstOrDefault(x => x.Id == report.Id);
            if (report != null)
            {
                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("GetAllReports", "Report");
        }
    }
}