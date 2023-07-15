using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shipping.Constants;
using Shipping.Models;
using Shipping.Repository.BranchRepo;
using Shipping.Repository.DeliveryRepo;
using Shipping.Repository.StateRepo;
using Shipping.ViewModels;
using static Shipping.Constants.Permissions;

namespace Shipping.Controllers
{
    public class DeliveryController : Controller
    {
        IDeliveryRepository _deliveryRepository;
        IbranchRepository _branchRepository;
        IStateRepository _stateRepository;
      
        public DeliveryController(IDeliveryRepository deliveryRepository,IbranchRepository ibranchRepository, IStateRepository stateRepository)
        {
            this._deliveryRepository = deliveryRepository;
            this._branchRepository = ibranchRepository;
            this._stateRepository = stateRepository;

        }


        # region GetALL
        [HttpGet]
        [Authorize(Permissions.Deliveries.View)]
        public async Task<IActionResult> Index(String Name)
        {
            var deliveryViewModel = await _deliveryRepository.GetAll(Name);
            return View(deliveryViewModel);
        }
        #endregion

        #region Add
        [HttpGet]
        [Authorize(Permissions.Deliveries.Create)]

        public IActionResult add()
        {
            var Branchs = _deliveryRepository.GetAllBranches().Where(b => b.Status == true);
            ViewBag.BranchList = new SelectList(Branchs, "Name", "Name");

            var States = _deliveryRepository.GetAllStates().Where(s => s.Status == true);
            ViewBag.StatesList = new SelectList(States, "Name", "Name");
            ViewBag.States = _deliveryRepository.GetAllStates();

            return View();
        }

        [HttpPost]
        [Authorize(Permissions.Deliveries.Create)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> add(DeliveryViewModel deliveryViewModel)
        {
            if (ModelState.IsValid)
            {
                var res = await _deliveryRepository.AddDelivery(deliveryViewModel);
                if (res != null)
                {
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    var Branchs = _deliveryRepository.GetAllBranches().Where(b => b.Status == true);
                    ViewBag.BranchList = new SelectList(Branchs, "Name", "Name");

                    var States = _deliveryRepository.GetAllStates().Where(s => s.Status == true);
                    ViewBag.StatesList = new SelectList(States, "Name", "Name");
                    return View(deliveryViewModel);
                }
                //if everything is good
                return RedirectToAction("Index");
            }
            else
            {
                var Branchs = _deliveryRepository.GetAllBranches().Where(b => b.Status == true);
                ViewBag.BranchList = new SelectList(Branchs, "Name", "Name");

                var States = _deliveryRepository.GetAllStates().Where(s => s.Status == true);
                ViewBag.StatesList = new SelectList(States, "Name", "Name");

                return View(deliveryViewModel);
            }
            
        }
        #endregion

        #region  GetBranchesByState
        [Authorize(Permissions.Deliveries.Create)]
        public IActionResult GetBranchesByState(string state)
        {
            var branches = _branchRepository.GetBranchesByStateName(state);

            return Json(branches);
        }
        #endregion

        #region Edit
        [HttpGet]
        [Authorize(Permissions.Deliveries.Edit)]
        public async Task<IActionResult> Edit(string Id)
        {
            var delivery = await _deliveryRepository.GetById(Id);
            if (delivery == null)
            {
                return View("NotFound");
            }

            


            ViewBag.States = _stateRepository.GetAll().Where(b => b.Status == true);
            var stateId = _stateRepository.GetAll().Where(p => p.Name == delivery.Government).Select(p => p.Id).FirstOrDefault();
         
            ViewBag.Branches = _branchRepository.GetAll().Where(b => b.Status == true && b.StateId == stateId).ToList();


            return View(delivery);
        }

        [HttpPost]
        [Authorize(Permissions.Deliveries.Edit)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Id, DeliveryEditViewModel deliveryEditViewModel)
        {
            Delivery delivery = await _deliveryRepository.GetDeliveryById(Id);
            if (ModelState.IsValid)
            {
                _deliveryRepository.EditDelivery(delivery, deliveryEditViewModel);
            }
            else
            {
                var Branchs = _deliveryRepository.GetAllBranches().Where(b => b.Status == true);
                ViewBag.BranchList = new SelectList(Branchs, "Name", "Name");

                var States = _deliveryRepository.GetAllStates().Where(s => s.Status == true);
                ViewBag.StatesList = new SelectList(States, "Name", "Name");
                return View("Edit", deliveryEditViewModel);
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        [HttpPost]
        [Authorize(Permissions.Deliveries.Delete)]
        public async Task<IActionResult> ChangeState(string Id, bool status)
        {
            Delivery delivery = await _deliveryRepository.GetDeliveryById(Id);
            if(delivery == null)
            {
                return View("NotFound");
            }
            _deliveryRepository.UpdateStatus(delivery, status);
            return RedirectToAction("Index");
        }
        #endregion


    }
}

