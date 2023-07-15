using Microsoft.EntityFrameworkCore;
using Shipping.Models;

namespace Shipping.Repository.BranchRepo
{
    public class BranchRepository : IbranchRepository
    {
        MyContext _myContext;
        public BranchRepository(MyContext context)
        {

            _myContext = context;

        }
        public void Delete(int id)
        {
            Branch oldBranch = GetById(id);
            if (oldBranch != null)
            {
                oldBranch.IsDeleted = true;
            }
            Update(id, oldBranch);
        }

        public List<Branch> GetAll()
        {
            return _myContext.Branches.Where(o => o.IsDeleted == false).Include(e => e.State).ToList();
        }


        public List<Branch> GetBranchesByStateId(int id)
        {
            return _myContext.Branches.Where(o => o.IsDeleted == false && o.StateId == id).ToList();
        }


        public Branch GetById(int? id)
        {
            return _myContext.Branches.Where(o => o.IsDeleted == false).FirstOrDefault(d => d.Id == id);
        }

        public void Insert(Branch branch)
        {
            _myContext.Branches.Add(branch);
            _myContext.SaveChanges();
        }
        public void Update(int id, Branch branch)
        {
            Branch oldBranch = GetById(id);
            oldBranch.Name = branch.Name;
            oldBranch.State = branch.State;
            oldBranch.Date = branch.Date;
            oldBranch.StateId = branch.StateId;
            oldBranch.Status = branch.Status;

            _myContext.SaveChanges();
        }
        public List<String> GetBranchesByStateName(string name)
        {
            var stateId = _myContext.States.Where(s => s.Name == name && s.IsDeleted == false).FirstOrDefault().Id;
            return _myContext.Branches.Where(c => c.StateId == stateId && c.IsDeleted == false && c.Status == true).Select(c => c.Name).ToList();
        }
    }
}
