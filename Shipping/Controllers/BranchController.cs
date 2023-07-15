using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipping.Constants;
using Shipping.Models;
using Shipping.Repository.BranchRepo;
using Shipping.Repository.StateRepo;

namespace Shipping.Controllers
{
    public class BranchController : Controller
    {
        IbranchRepository _brachRepo;
        IStateRepository _stateRepo;
        public BranchController(IbranchRepository ibranchRepository, IStateRepository stateRepo)
        {
            _brachRepo = ibranchRepository;
            _stateRepo = stateRepo;

        }
        #region view
        [Authorize(Permissions.Branches.View)]
        [HttpGet]
        public IActionResult Index()
        {
            List<Branch> branches = _brachRepo.GetAll();
            return View(branches);
        }
        #endregion

        #region change state
        [Authorize(Permissions.Branches.Edit)]
        [HttpPost]
        public IActionResult ChangeState(int Id, bool status)
        {
            var branch = _brachRepo.GetById(Id);
            branch.Status = status;
            _brachRepo.Update(Id, branch);
            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        [Authorize(Permissions.Branches.Create)]
        [HttpGet]
        public IActionResult Add()
        {

            ViewData["State"] = _stateRepo.GetAll().Where(s=>s.Status==true);

            return View();
        }
        [Authorize(Permissions.Branches.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Branch branch)
        {
            branch.Date = DateTime.Now;
            branch.IsDeleted = false;
            branch.Status = true;
            _brachRepo.Insert(branch);
            return RedirectToAction("index");
        }
        #endregion

        #region Edit
        [Authorize(Permissions.Branches.Edit)]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Branch branch = _brachRepo.GetById(id);

            ViewData["State"] = _stateRepo.GetAll().Where(s=>s.Status==true);

            return View("Edit", branch);
        }
        [Authorize(Permissions.Branches.Edit)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Branch branch)
        {
            branch.Date = DateTime.Now;

            _brachRepo.Update(id, branch);

            return RedirectToAction("index");
        }

        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Branches.Delete)]
        public IActionResult Delete(int id)
        {

            _brachRepo.Delete(id);
            return RedirectToAction("Index", "Branch");
        }

        #endregion

        #region search
        [Authorize(Permissions.Branches.View)]
        public IActionResult Search(string query)
        {
            List<Branch> branch;
            if (string.IsNullOrWhiteSpace(query)) { branch = _brachRepo.GetAll().ToList(); }
            else
            {
                branch = _brachRepo.GetAll().Where(i => i.Name.Contains(query)).ToList();

            }
            return View("Index", branch);
        }

        #endregion
    }
}
