using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCRUDMODEL.Data;
using MVCCRUDMODEL.Models;
using MVCCRUDMODEL.Models.Domain;

namespace MVCCRUDMODEL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MvcDbContext db;
        public EmployeeController(MvcDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await db.Employees.ToListAsync();
            return View(employees);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployees addEmployee)
        {
            var employ = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployee.Name,
                Email = addEmployee.Email,
                Salary = addEmployee.Salary,
                DateOfBirth = addEmployee.DateOfBirth,
                Department = addEmployee.Department
            };
            await db.Employees.AddAsync(employ);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id) 
        {
            var employees=await db.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employees != null)
            {
                var update = new UpdateModel()
                {
                    Id = employees.Id,
                    Name = employees.Name,
                    Email = employees.Email,
                    Salary = employees.Salary,
                    DateOfBirth = employees.DateOfBirth,
                    Department = employees.Department
                };
                return await Task.Run(()=>View("View",update));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateModel model)
        {
            var e = await db.Employees.FindAsync(model.Id);
            if (e != null) 
            { 
                e.Name = model.Name;
                e.Email = model.Email;
                e.Salary = model.Salary;
                e.DateOfBirth = model.DateOfBirth;
                e.Department = model.Department;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
			return RedirectToAction("Index");
		}
        public async Task<IActionResult> Delete(UpdateModel model)
        {
            var e = await db.Employees.FindAsync(model.Id);
            if(e != null)
            {
                db.Employees.Remove(e);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
			return RedirectToAction("Index");
		}
    }
}
