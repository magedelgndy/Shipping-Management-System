using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping.Models;
using Shipping.ViewModels;
using System.Security.Policy;
using static Shipping.Constants.Permissions;

namespace Shipping.Repository.EmployeeRepo
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        MyContext _myContext;
      
        public EmployeeRepository(MyContext myContext, UserManager<ApplicationUser> userManager)
        {
            _myContext = myContext;
            _userManager = userManager;
        }


        #region add
        public async Task<IdentityResult> Add(EmployeeViewModel employeeViewModel, string role)
        {

            var branch = _myContext.Branches.FirstOrDefault(b => b.Name == employeeViewModel.BranchName);
            var employee = new Employee
            {
                BranchId = branch.Id,

            };
            var user = new ApplicationUser
            {
                Email = employeeViewModel.Email,
                UserName = employeeViewModel.Name,
                Name = employeeViewModel.Name,
                PhoneNumber = employeeViewModel.Phone,
                //Type =TypeOfUser.Employee
            };

            var result = await _userManager.CreateAsync(user, employeeViewModel.Password);

            if (result.Succeeded)
            {
                var _user = await _userManager.FindByEmailAsync(user.Email);
                if (_user != null)
                    await _userManager.AddToRoleAsync(_user, role);

                employee.UserId = _user.Id;
                _myContext.Add(employee);
                _myContext.SaveChanges();

                return null;
            }
            IdentityResult errorMsgs = result;
            return errorMsgs;
        }
        #endregion


        #region GetAlls
        public async Task<List<EmployeeViewModel>> GetAllEmployees()
        {

            var employees = await _myContext.Employees.Include(e => e.User).Include(e => e.Branch).ToListAsync();
            var employeeViewModels = new List<EmployeeViewModel>();

            foreach (var employee in employees)
            {
                var roles = await _userManager.GetRolesAsync(employee.User);

                employeeViewModels.Add(new EmployeeViewModel
                {
                    Id = employee.User.Id,
                    Name = employee.User.Name,
                    Email = employee.User.Email,
                    Phone = employee.User.PhoneNumber,
                    BranchName = employee.Branch.Name,
                    Status = employee.User.Status,
                    Role = roles.FirstOrDefault()

                });

            }

            return employeeViewModels;
        }
        public IEnumerable<BranchViewModel> GetAllBranches()
        {
            // Get all the Branch objects from the database
            var branches = _myContext.Branches;

            // Map the Branch objects to BranchViewModel objects
            var branchViewModels = branches.Select(b => new BranchViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Status = b.Status
            });

            return branchViewModels;
        }
        #endregion


        #region GetEmployeeById
        public async Task<EmployeeEditViewModel> GetEmployeeById(string id)
        {
            var employee = _myContext.Employees
                .Include(e => e.User)
                .Include(e => e.Branch)
                .FirstOrDefault(e => e.User.Id == id);

            var hisRole =await _userManager.GetRolesAsync(employee.User);
            if (employee != null)
            {
                return new EmployeeEditViewModel
                {
                    Id = employee.User.Id,
                    Name = employee.User.Name,
                    Email = employee.User.Email,
                    Phone = employee.User.PhoneNumber,
                    BranchName = employee.Branch.Name,
                    Role = hisRole[0]

                };
            }
            else
            {
                return null;
            }
        }
        #endregion


        #region Search
        public IEnumerable<EmployeeViewModel> Search(string query)
        {
            var employees = _myContext.Employees
                .Include(e => e.User)
                .Include(e => e.Branch)
                .Where(e => e.User.Name.Contains(query) ||
                            e.User.Email.Contains(query) ||
                            e.User.PhoneNumber.Contains(query) ||
                            e.Branch.Name.Contains(query));

            var employeeViewModels = employees.Select(e => new EmployeeViewModel
            {
                Id = e.User.Id,
                Name = e.User.Name,
                Email = e.User.Email,
                Phone = e.User.PhoneNumber,
                BranchName = e.Branch.Name,
                // Role = e.User.Role
            });

            return employeeViewModels;
        }
        #endregion


        #region update
        public async Task Update(EmployeeEditViewModel NewEmployee)
        {

            var employee = _myContext.Employees.Include(e => e.User).Include(e => e.Branch).FirstOrDefault(e => e.User.Id == NewEmployee.Id);

            if (employee != null)
            {

                employee.User.Name = NewEmployee.Name;
                employee.User.Email = NewEmployee.Email;
                employee.User.PhoneNumber = NewEmployee.Phone;

                var hisRoles = await _userManager.GetRolesAsync(employee.User);
                if (hisRoles[0] != NewEmployee.Role)
                {
                    await _userManager.RemoveFromRoleAsync(employee.User, hisRoles[0]);
                    await _userManager.AddToRoleAsync(employee.User, NewEmployee.Role);
                }
               

                if (employee.Branch.Name != NewEmployee.BranchName)
                {
                    var branch = _myContext.Branches.FirstOrDefault(b => b.Name == NewEmployee.BranchName);
                    if (branch != null)
                    {
                        employee.Branch = branch;
                    }
                }

            }
        }
        #endregion


        #region updateStatus
        public void UpdateStatus(Employee employee, bool status)
        {
            if (employee != null)
            {
                employee.User.Status = status;
                _myContext.SaveChanges();
            }
        }
        #endregion


        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void save() { _myContext.SaveChanges(); }


        public Employee GetById(string id)
        {
            var emp = _myContext.Employees.Include(e => e.User).FirstOrDefault(e => e.UserId == id);
            return emp;
        }
    }
}
