using Shipping.Models;

namespace Shipping.Repository.BranchRepo
{
    public interface IbranchRepository
    {
        List<Branch> GetAll();
        List<Branch> GetBranchesByStateId(int id);
        Branch GetById(int? id);

        void Insert(Branch branch);
        void Update(int id, Branch branch);
        void Delete(int id);

        List<String> GetBranchesByStateName(string name);
    }
}
