using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shipping.Constants;
using Shipping.Models;
using Shipping.Repository.BranchRepo;
using Shipping.Repository.CityRepo;
using Shipping.Repository.MerchantRepo;
using Shipping.Repository.StateRepo;
using Shipping.ViewModels;

namespace Shipping.Controllers
{
    public class MerchantController : Controller
    {
        IMerchantRepository _merchantRepo;
        ICityRepository _cityRepository;
        IbranchRepository _branchRepository;
        IStateRepository _stateRepository;
        public MerchantController(IMerchantRepository merchantRepo, ICityRepository CityRepository, IbranchRepository branchRepository,IStateRepository stateRepository)
        {
            _merchantRepo = merchantRepo;
            _cityRepository = CityRepository;
            _branchRepository = branchRepository;
            _stateRepository = stateRepository;
        }
        #region ViewAll
        [Authorize(Permissions.Merchants.View)]
        [HttpGet]
        public async Task<IActionResult> Index(string Name)
        {
            var merchantViewModels = await _merchantRepo.GetAll(Name);
            return View(merchantViewModels);
        }

        #endregion

        #region Adding

        [HttpGet]
        public IActionResult GetStates()
        {
            var states = _stateRepository.GetAll().Where(b => b.Status == true);
            return Json(states);
        }

        [HttpGet]
        [Authorize(Permissions.Merchants.Create)]
        public IActionResult Add()
        {
            var Branchs = _merchantRepo.GetAllBranches().Where(b => b.Status == true);
            ViewBag.BranchList = new SelectList(Branchs, "Name", "Name");

            //var States = _merchantRepo.GetAllStates().Where(b => b.Status == true);
            //ViewBag.StatesList = new SelectList(States, "Name", "Name");
            //ViewBag.StatesName = _stateRepository.GetAllNames();

            ViewBag.States = _stateRepository.GetAll().Where(b => b.Status == true);
            

            var Cities = _merchantRepo.GetAllCities().Where(b => b.Status == true);
            ViewBag.CitiesList = new SelectList(Cities, "Name", "Name");

            return View();
        }
        [HttpPost]
        [Authorize(Permissions.Merchants.Create)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(MerchantViewModel merchantViewModel)
        {

            if (ModelState.IsValid)
            {
                var res = await _merchantRepo.AddMechant(merchantViewModel);
                if(res != null)
                {
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    var Branchs = _merchantRepo.GetAllBranches().Where(b => b.Status == true);
                    ViewBag.BranchList = new SelectList(Branchs, "Name", "Name");

                    var States = _merchantRepo.GetAllStates().Where(b => b.Status == true);
                    ViewBag.StatesList = new SelectList(States, "Name", "Name");
                    ViewBag.States = _stateRepository.GetAll();

                    var Cities = _merchantRepo.GetAllCities().Where(b => b.Status == true);
                    ViewBag.CitiesList = new SelectList(Cities, "Name", "Name");
                    return View(merchantViewModel);
                }
                //if everything is good
                return RedirectToAction("Index");
            }
            else
            {
                var Branchs = _merchantRepo.GetAllBranches().Where(b => b.Status == true);
                ViewBag.BranchList = new SelectList(Branchs, "Name", "Name");
                ViewBag.Branches = Branchs;

                var States = _stateRepository.GetAll().Where(b => b.Status == true);
                ViewBag.StatesList = new SelectList(States, "Name", "Name");
                ViewBag.States = States;

                return View(merchantViewModel);
            }
        }

        #endregion

        #region Edit
        [HttpGet]
        [Authorize(Permissions.Merchants.Edit)]
        public async Task<IActionResult> Edit(string Id)
        {
            var merchant = await _merchantRepo.GetById(Id);
            var merchantEditViewModel = await _merchantRepo.MapToViewModel(merchant);
            if (merchant == null)
            {
                return NotFound();
            }
            ViewBag.States = _stateRepository.GetAll().Where(b => b.Status == true);
            var stateId = _stateRepository.GetAll().Where(p => p.Name == merchant.Government).Select(p => p.Id).FirstOrDefault();

            ViewBag.Branches = _branchRepository.GetAll().Where(b => b.Status == true && b.StateId == stateId).ToList();

            ViewBag.Cities = _cityRepository.GetAllByStateName(merchant.Government).ToList();


            return View(merchantEditViewModel);
        }
        [HttpPost]
        [Authorize(Permissions.Merchants.Edit)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Id, MerchantEditViewModel merchantEditViewModel)
        {
            var merchant = await _merchantRepo.GetById(Id);
            if (ModelState.IsValid)
            {
                _merchantRepo.EditMerchant(merchant, merchantEditViewModel);
            }
            else
            {
                var Branchs = _merchantRepo.GetAllBranches().Where(b => b.Status == true);
                ViewBag.BranchList = new SelectList(Branchs, "Name", "Name");

                var States = _merchantRepo.GetAllStates().Where(b => b.Status == true);
                ViewBag.StatesList = new SelectList(States, "Name", "Name");

                var Cities = _merchantRepo.GetAllCities().Where(b => b.Status == true);
                ViewBag.CitiesList = new SelectList(Cities, "Name", "Name");
                return View("Edit", merchantEditViewModel);
            }
            return RedirectToAction("Index");
        }
        #endregion


        #region changeStatus
        [Authorize(Permissions.Merchants.Delete)]
        [HttpPost]
        public async Task<IActionResult> ChangeState(string Id, bool status)
        {
            var merchant = await _merchantRepo.GetById(Id);
            _merchantRepo.UpdateStatus(merchant, status);
            return RedirectToAction("Index");
        }

        #endregion


        #region  GetCitiesByState
        [Authorize(Permissions.Merchants.Create)]
        public IActionResult GetCitiesByState(string state)
        {
            var cities = _cityRepository.GetAllByStateName(state);

            return Json(cities);
        }
        #endregion

        #region  GetBranchesByState
        [Authorize(Permissions.Merchants.Create)]
        public IActionResult GetBranchesByState(string state)
        {
            var branches = _branchRepository.GetBranchesByStateName(state);

            return Json(branches);
        }
        #endregion
    }
}
