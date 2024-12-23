using Data.Database;
using Domain.Entities;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareMindWeb.Authentication;
using SoftwareMindWeb.Utility;

namespace SoftwareMindWeb.Controllers
{
    [WebTokenAuthentication]
    public class EmployeesController : Controller
    {
        private readonly SoftwareMindContext Context;
        private readonly List<Department> Departments;

        public EmployeesController(SoftwareMindContext context)
        {
            Context = context;
        }

        public IActionResult Index()
        {
            try
            {
                var employees = Context.Employees.AsNoTracking().Include(e => e.Department)
                    .Select(e => new EmployeeListView
                    {
                        EmployeeId = e.EmployeeId,
                        FullName = $"{e.FirstName} {e.LastName}",
                        DepartmentName = e.Department.Name,
                        HireDate = e.HireDate.GetTodaysCommaYear(),
                        TimeSinceJoined = e.HireDate.GetYearsMonthsPassedSince(),
                    }).ToList();
                return View(employees);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult CreateEmployee(CreateEmployeeVM employeeVM)
        {
            try
            {
                long phone = employeeVM.PhoneNumber.GetPhoneForEntityFromString();
                var newEmployee = new Employee
                {
                    FirstName = employeeVM.FirstName,
                    LastName = employeeVM.LastName,
                    Address = employeeVM.Address,
                    Phone = phone,
                    HireDate = DateTime.Now,
                    DepartmentId = -1,
                };
                Context.Employees.Add(newEmployee);
                Context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Edit(int? id)
        {
            try
            {
                var employee = Context.Employees.AsNoTracking().Include(e => e.Department).FirstOrDefault(u => u.EmployeeId == id);
                if (employee == null)
                {
                    return NotFound();
                }
                var details = new EmployeeDetailsVM
                {
                    EmployeeId = employee.EmployeeId,
                    FullName = $"{employee.FirstName} {employee.LastName}",
                    DepartmentName = employee.Department.Name,
                    HireDate = employee.HireDate.GetTodaysCommaYear(),
                    TimeSinceJoined = employee.HireDate.GetYearsMonthsPassedSince(),
                    Address = employee.Address,
                    DepartmentId = employee.DepartmentId,
                    Phone = employee.Phone.LongNumberToPhoneFormatString(),
                };
                if (ViewBag.CompanyDepartments == null)
                {
                    ViewBag.CompanyDepartments = Context.Departments.AsNoTracking().ToList();
                }
                return View(details);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeDetailsVM employee)
        {
            try
            {
                if (employee.EmployeeId > 0 && employee.DepartmentId > 0)
                {
                    var current = Context.Employees.FirstOrDefault(u => u.EmployeeId == employee.EmployeeId);
                    if (current == null)
                    {
                        return NotFound();
                    }
                    current.DepartmentId = employee.DepartmentId;
                    await Context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employee = Context.Employees.FirstOrDefault(e => e.EmployeeId == id);
                if (employee == null)
                {
                    return NotFound();
                }
                Context.Employees.Remove(employee);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
