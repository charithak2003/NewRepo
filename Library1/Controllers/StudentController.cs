
using Library1.Data;
using Library1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library1.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel addStudentViewModel)
        {
                if (string.IsNullOrWhiteSpace(addStudentViewModel.Name) || string.IsNullOrWhiteSpace(addStudentViewModel.usn) ||
                    string.IsNullOrWhiteSpace(addStudentViewModel.Department) || string.IsNullOrWhiteSpace(addStudentViewModel.sem) )
                {
                    // If any of the required fields are not filled, return to the view with an error message.
                    ModelState.AddModelError("", "All fields are required");
                    return View(addStudentViewModel);
                }
            var student = new Student
            {
                Name = addStudentViewModel.Name,
                usn = addStudentViewModel.usn,
                Department = addStudentViewModel.Department,
                sem = addStudentViewModel.sem,
            };


            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var student = await _context.Students.Include(r => r.Report).ToListAsync();
            return View(student);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var student = await _context.Students.FindAsync(Id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            var students = await _context.Students.FindAsync(student.Id);
            if (students != null)
            {
                students.Name = student.Name;
                students.usn = student.usn;
                students.Department = student.Department;
                students.sem = student.sem;

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("GetAllStudents", "Student");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Student student)
        {
            var students = _context.Students.AsNoTracking().FirstOrDefault(x => x.Id == student.Id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("GetAllStudents", "Student");
        }



    }
}
