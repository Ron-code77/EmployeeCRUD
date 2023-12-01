using EmployeeCRUD.Data;
using EmployeeCRUD.Models;
using EmployeeCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUD.Controllers
{
	public class EmployeesController : Controller
	{
        private readonly DemoDBContext demoDBContext;

        public EmployeesController(DemoDBContext demoDBContext)
        {
            this.demoDBContext = demoDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var employees = await demoDBContext.Employees.ToListAsync();

            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.EmployeeName).ToList();
                    break;
                default:
                    employees = employees.OrderBy(e => e.EmployeeName).ToList();
                    break;
            }

            return View(employees);
        }
        [HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost] 
		public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeViewModel) 
		{
			var employee = new Employee()
			{
				EmployeeName = addEmployeeViewModel.EmployeeName,
				Email = addEmployeeViewModel.Email,
				Salary = addEmployeeViewModel.Salary,
				DateofBirth = addEmployeeViewModel.DateofBirth,
				Department = addEmployeeViewModel.Department,
			};
			await demoDBContext.Employees.AddAsync(employee);
			await demoDBContext.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> View(int id) 
		{

			var employee =await demoDBContext.Employees.FirstOrDefaultAsync(x =>x.EmployeeId == id);
			 
			if(employee !=null)
			{
				var viewModel = new UpdateEmployee()
				{
					EmployeeId = employee.EmployeeId,
					EmployeeName = employee.EmployeeName,
					Email = employee.Email,
					Salary = employee.Salary,
					DateofBirth = employee.DateofBirth,
					Department = employee.Department
				};
				return await Task.Run(() =>View("View",viewModel));

            }


			return RedirectToAction("Index");
		}
		[HttpPost]

		public async Task<IActionResult> View(UpdateEmployee model)

		{
			var employee = await demoDBContext.Employees.FindAsync(model.EmployeeId);
			if(employee!=null)
			{
				employee.EmployeeName = model.EmployeeName;
				employee.Salary = model.Salary;
				employee.Email = model.Email;
				employee.DateofBirth = model.DateofBirth;
				employee.Department = model.Department;

				await demoDBContext.SaveChangesAsync();
				return RedirectToAction("Index");
			}

            return RedirectToAction("Index");

        }

		[HttpPost]
		public async Task <IActionResult>Delete(UpdateEmployee model)
		{
			var employee = await demoDBContext.Employees.FindAsync(model.EmployeeId);
			if(employee!=null)
			{
				demoDBContext.Employees.Remove(employee);	
				await demoDBContext.SaveChangesAsync();	
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}

    }

}
