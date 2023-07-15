using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shipping.Constants;
using Shipping.Models;
using Shipping.Repository.EmployeeRepo;
using Shipping.ViewModels;
using static Shipping.Constants.Permissions;

namespace Shipping.Controllers
{
    public class EmployeeController : Controller
    {

        IEmployeeRepository _employeeRepository;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public EmployeeController(IEmployeeRepository employeeRepository, RoleManager<ApplicationRole> roleManager)
        {
            _employeeRepository = employeeRepository;
            _roleManager = roleManager;
        }

        [Authorize(Permissions.Employees.View)]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _employeeRepository.GetAllEmployees());
        }

        #region Add
        [Authorize(Permissions.Employees.Create)]
        [HttpGet]
        public async Task<IActionResult> Add()
        {

            var employeeViewModel = new EmployeeViewModel();

            var branches = _employeeRepository.GetAllBranches().Where(b=>b.Status==true);
            var branchList = new SelectList(branches, "Name", "Name");

            ViewBag.BranchList = branchList;

            var empRoles = await _roleManager.Roles.Where(r => r.Name != "admin" && r.Name != "التجار" && r.Name != "المناديب").ToListAsync();
            var empRolesSelect = new SelectList(empRoles, "Name", "Name");
            ViewBag.empRoles = empRolesSelect;

            return View(employeeViewModel);
        }

        [Authorize(Permissions.Employees.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var res = await _employeeRepository.Add(employeeViewModel, employeeViewModel.Role);
                if (res != null)
                {
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    var branches = _employeeRepository.GetAllBranches().Where(b => b.Status == true);
                    var branchList = new SelectList(branches, "Name", "Name");
                    ViewBag.BranchList = branchList;
                    var empRoles = await _roleManager.Roles.Where(r => r.Name != "admin" && r.Name != "التجار" && r.Name != "المناديب").ToListAsync();
                    var empRolesSelect = new SelectList(empRoles, "Name", "Name");
                    ViewBag.empRoles = empRolesSelect;
                    return View(employeeViewModel);

                }
                else
                {   //if everything is good
                    _employeeRepository.save();
                    return RedirectToAction("Index", "Employee");
                }
            }
            return View(employeeViewModel);

        }
        #endregion

        #region Edit
        [Authorize(Permissions.Employees.Edit)]
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            var employee = await _employeeRepository.GetEmployeeById(Id);

            if (employee != null)
            {
                var branches = _employeeRepository.GetAllBranches().ToList().Where(b => b.Status == true);
                ViewBag.BranchList = new SelectList(branches, "Name", "Name");

                var empRoles = await _roleManager.Roles.Where(r => r.Name != "admin" && r.Name != "التجار" && r.Name != "المناديب").ToListAsync();
                var empRolesSelect = new SelectList(empRoles, "Name", "Name");
                ViewBag.empRoles = empRolesSelect;

                return View(employee);
            }
            else
            {
                return View("NotFound");
            }
        }
        [Authorize(Permissions.Employees.Edit)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Edit(EmployeeEditViewModel Newemployee)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepository.Update(Newemployee);
                _employeeRepository.save();
            }
            else
            {
                var branches = _employeeRepository.GetAllBranches().ToList().Where(b => b.Status == true);
                ViewBag.BranchList = new SelectList(branches, "Name", "Name");

                var empRoles = await _roleManager.Roles.Where(r => r.Name != "admin" && r.Name != "التجار" && r.Name != "المناديب").ToListAsync();
                var empRolesSelect = new SelectList(empRoles, "Name", "Name");
                ViewBag.empRoles = empRolesSelect;

                return View("Edit", Newemployee);
            }
            return RedirectToAction("Index");

        }
        public IActionResult Search(string searchTerm)
        {
            var employees = _employeeRepository.Search(searchTerm);
            return View("Index", employees);
        }
        #endregion

        #region Change State
        [HttpPost]
        [Authorize(Permissions.Employees.Delete)]
        public IActionResult ChangeState(string Id, bool status)
        {
            var employee = _employeeRepository.GetById(Id);
            _employeeRepository.UpdateStatus(employee, status);
            return RedirectToAction("Index");
        }

        #endregion

    }
}
