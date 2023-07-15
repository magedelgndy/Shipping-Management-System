using Shipping.ViewModels;
using Shipping.Models;
using Microsoft.AspNetCore.Identity;

namespace Shipping.Repository.EmployeeRepo
{
    public interface IEmployeeRepository
    {
        IEnumerable<BranchViewModel> GetAllBranches();
        Task<List<EmployeeViewModel>> GetAllEmployees();
        IEnumerable<EmployeeViewModel> Search(string query);
        Task<EmployeeEditViewModel> GetEmployeeById(string id);
        Employee GetById(string id);
        Task<IdentityResult> Add(EmployeeViewModel NewEmp, string role);
        void Delete(int id);

        Task Update(EmployeeEditViewModel newemp);
        void UpdateStatus(Employee employee, bool status);
        void save();
    }
}
