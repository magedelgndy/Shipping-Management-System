using Microsoft.AspNetCore.Mvc;
using Shipping.Models;
using Shipping.ViewModels;
using System.Text.Json.Serialization;
using System.Text.Json;
using static NuGet.Packaging.PackagingConstants;
using Shipping.Repository.StateRepo;
using Shipping.Repository.CityRepo;
using Shipping.Repository.OrderRepo;
using Shipping.Repository.DeliveryRepo;
using Shipping.Repository.BranchRepo;
using Microsoft.AspNetCore.Identity;
using Azure;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Shipping.Constants;
using System.Threading.Tasks;

namespace Shipping.Controllers
{
    public class OrderController : Controller
    {
        IStateRepository _stateRepository;
        ICityRepository _cityRepository;
        IOrderRepository _orderRepository;
        IDeliveryRepository _deliveryRepository;
        IbranchRepository _branchRepository;

        UserManager<ApplicationUser> _userManager;
        RoleManager<ApplicationRole> _roleManager;
        MyContext _myContext;
        public OrderController(IStateRepository stateRepository, ICityRepository cityRepository, MyContext myContext, IOrderRepository orderRepository, IDeliveryRepository deliveryRepository, IbranchRepository ibranchRepository, UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager)
        {
            _stateRepository = stateRepository;
            _cityRepository = cityRepository;
            _myContext = myContext;
            _orderRepository = orderRepository;
            _deliveryRepository = deliveryRepository;
            _branchRepository = ibranchRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region ViewAll
        [HttpGet]
        [Authorize(Permissions.Orders.View)]
        [Authorize(Permissions.Orders.Edit)]
        [Authorize(Permissions.Orders.Delete)]
        public async Task<IActionResult> Index()
        {

            var Orders = _orderRepository.GetAllOrders();
            ViewData["Delivery"] = await _deliveryRepository.GetAll(null);


            ViewBag.Branches = _myContext.Branches.ToList();
            return View(Orders);
        }
        #endregion

        #region ChangeDelivery
        [HttpPost]
        [Authorize(Permissions.Orders.Edit)]
        public async Task<IActionResult> ChangeDelivery(int Id, int deliveryId)
        {

            var order = await _orderRepository.GetOrderById(Id);

            _orderRepository.UpdateDelivery(order, deliveryId);

            var Orders = _orderRepository.GetAllOrders();
            ViewData["Delivery"] = await _deliveryRepository.GetAll(null);


            ViewBag.Branches = _myContext.Branches.ToList();
            return View("Index", Orders);
        }
        #endregion


        #region GetOrdersDependonStatus
        [HttpPost]
        [Authorize(Permissions.Orders.View)]
        public async Task<IActionResult> GetOrdersDependonStatus(string? status = null)
        {
            List<OrderViewModel> Orders;
            if (status == null)
            {
                Orders = _orderRepository.GetAllOrders();
            }
            else
            {
                Orders = _orderRepository.GetOrderByStatus(status);
            }
            var dileveryList = await _deliveryRepository.GetAll(null);
            var OrdersPlusDeliverys = new OrdersPlusDeliverysViewModel();
            OrdersPlusDeliverys.orders = Orders;
            OrdersPlusDeliverys.deliveries = dileveryList;

            var Rows = _orderRepository.GenerateTable(OrdersPlusDeliverys);
            return Json(Rows);
        }
        #endregion


        #region ChangeStatus
        [HttpPost]
        [Authorize(Permissions.Orders.View)]
        public async Task<IActionResult> ChangeStatus(int Id, string status)
        {
            var order = await _orderRepository.GetOrderById(Id);
            _orderRepository.UpdateStatus(order, status);
            return RedirectToAction("OrderCount");
        }
        #endregion


        #region Add
        [Authorize(Permissions.Orders.Create)]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.States = _stateRepository.GetAll().Where(b => b.Status == true);
            ViewBag.Branches = _myContext.Branches.ToList().Where(b => b.Status == true);

            return View();
        }
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Orders.Create)]
        [HttpPost]
        public async Task<IActionResult> Add(OrderViewModel orderViewModel)
        {
            if(orderViewModel.orderProducts.Count == 0)
            {
                ModelState.AddModelError("", "يجب عليك اضافه منتاجات");
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var UserRole = User.FindFirstValue(ClaimTypes.Role);

                _orderRepository.CalcShipping(orderViewModel,user);

                _orderRepository.Add(orderViewModel,user);
                return Redirect("/Order/ordercount");
            }

            ViewBag.States = _stateRepository.GetAll().Where(b => b.Status == true);
            ViewBag.Branches = _myContext.Branches.ToList().Where(b => b.Status == true);
            return View(orderViewModel);
        }
        #endregion

        #region  #endregion
        [HttpGet]
        [Authorize(Permissions.Orders.Create)]
        public IActionResult GetCitiesByState(string state)
        {
            var cities = _cityRepository.GetAllByStateName(state);

            return Json(cities);
        }

        #endregion

        #region  GetBranchesByState
        [Authorize(Permissions.Orders.Create)]
        public IActionResult GetBranchesByState(string state)
        {
            var branches = _branchRepository.GetBranchesByStateName(state);

            return Json(branches);
        }
        #endregion

        #region search

        #region SearchByClientName
        [Authorize(Permissions.Orders.View)]
        public async Task<IActionResult> SearchByClientName(string query)
        {
            List<OrderViewModel> Orders;
            if (string.IsNullOrWhiteSpace(query)) { Orders = _orderRepository.GetAllOrders().ToList(); }
            else
            {
                Orders = _orderRepository.GetAllOrders().Where(i => i.ClientName.ToUpper().Contains(query.ToUpper())).ToList();

            }
            var x = ViewData["Delivery"] = await _deliveryRepository.GetAll(null);
            ViewBag.Deliverys = await _deliveryRepository.GetAll(null);


            ViewBag.Branches = _myContext.Branches.ToList();
            return View("Index", Orders);
        }

        #endregion

        #region SearchByDeliveryName
        [Authorize(Permissions.Orders.View)]
        public async Task<IActionResult> SearchByDeliveryName(string query)
        {
            List<OrderViewModel> Orders = new List<OrderViewModel>();

            var delivery = await _deliveryRepository.GetAll(null);

            if (string.IsNullOrWhiteSpace(query)) { 
                Orders = _orderRepository.GetAllOrders().ToList();
                RedirectToAction("Index");
            }
            else
            {


                var deliveryAfterFilteration = delivery.Where(x => x.DeliverName.ToUpper().Contains(query.ToUpper())).ToList();
                foreach (var item in deliveryAfterFilteration)
                {
                    var ordersAfterFilter = _orderRepository.GetAllOrders().Where(i => i.DeliveryId == item.OrignalIdOnlyInDeliveryTable).ToList();
                    foreach (var item1 in ordersAfterFilter)
                    {
                        Orders.Add(item1);
                    }
                }


            }

            ViewData["Delivery"] = await _deliveryRepository.GetAll(null);
            ViewBag.Deliverys = await _deliveryRepository.GetAll(null);


            ViewBag.Branches = _myContext.Branches.ToList();
            return View("Index", Orders);
        }
        #endregion
        #endregion

        #region edit
        [Authorize(Permissions.Orders.Edit)]
        public async Task<IActionResult> Edit(int Id)
        {
            ViewBag.States = _stateRepository.GetAll().Where(b => b.Status == true);
           

            var orderViewModel = await _orderRepository.OrderViewModelById(Id);
            var stateId= _stateRepository.GetAll().Where(p=>p.Name== orderViewModel.StateName).Select(p=>p.Id).FirstOrDefault();
            ViewBag.Branches = _myContext.Branches.ToList().Where(b => b.Status == true && b.StateId == stateId);
            ViewBag.Cities = _cityRepository.GetAllByStateName(orderViewModel.StateName).ToList();

            return View(orderViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Orders.Edit)]
        public async Task<IActionResult> EditAsync(int Id, OrderViewModel orderViewModel)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                
                var order = await _orderRepository.GetOrderById(Id);
                string  res = _orderRepository.Edit(order, orderViewModel,user);
                if(res != "")
                {
                    ModelState.AddModelError("", res);
                    ViewBag.States = _stateRepository.GetAll().Where(b => b.Status == true);
                    ViewBag.Branches = _myContext.Branches.ToList().Where(b => b.Status == true);
                    ViewBag.Cities = _cityRepository.GetAllByStateName(orderViewModel.StateName).ToList();

                    return View(orderViewModel);
                }
                return Redirect("/order/OrderCount");
            }

            ViewBag.States = _stateRepository.GetAll().Where(b => b.Status == true);
            ViewBag.Branches = _myContext.Branches.ToList().Where(b => b.Status == true);
            ViewBag.Cities = _cityRepository.GetAllByStateName(orderViewModel.StateName).ToList();

            return View(orderViewModel);
        }

        #endregion

        #region Delete
        [HttpGet, ActionName("Delete")]
        [Authorize(Permissions.Orders.Delete)]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            var order = await _orderRepository.GetOrderById(Id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Orders.Delete)]
        public async Task<IActionResult> DeleteConfirmedAsync(int Id)
            {
            var order = await _orderRepository.GetOrderById(Id);
            if (order == null)
            {
                return View("NotFound");
            }
            _orderRepository.Delete(order);
            return RedirectToAction("Index");
        }
        #endregion


        #region OrderCount
        [Authorize]
        public IActionResult OrderCount()
        {
            string roleName = User.FindFirstValue(ClaimTypes.Role);
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _orderRepository.GetAllOrders();

            ViewBag.RoleName=roleName;
            if (roleName == "Admin" || roleName == "الموظفين")           
            { return View(result); }
            else if (roleName == "التجار")
            {
                var intMerchantId = _myContext.Merchants.Where(p => p.UserId == userId).Select(p => p.Id).FirstOrDefault();
                return View(result.Where(p => p.MerchantId == intMerchantId).ToList());
            }
            else if (roleName == "المناديب")
            {
                var intDeliveryId = _myContext.Deliveries.Where(p => p.UserId == userId).Select(p => p.Id).FirstOrDefault();
                return View(result.Where(p => p.DeliveryId == intDeliveryId).ToList());
            }
            else
            {
                return View("NotAllowed");
            }
            
        }

        #endregion

        #region related to the count screen [IndexAfterFilter]
        [Authorize(Permissions.Orders.View)]
        public async Task<IActionResult> IndexAfterFilter(string query)
        {
            string roleName = User.FindFirstValue(ClaimTypes.Role);
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            
            var Orders = _orderRepository.GetAllOrders().ToList();

            ViewData["Delivery"] = await _deliveryRepository.GetAll(null);
            var newOrders = Orders.Where(p => p.OrderStatus == query).ToList();

            ViewBag.Branches = _myContext.Branches.ToList();
            ViewBag.RoleName = roleName;



            if (roleName == "Admin" || roleName == "الموظفين")
            { return View("index", model: newOrders); }
            else if (roleName == "التجار")
            {

                var intMerchantId = _myContext.Merchants.Where(p => p.UserId == userId).Select(p => p.Id).FirstOrDefault();
                var result= Orders.Where(p => p.OrderStatus == query &&p.MerchantId==intMerchantId).ToList();
                return View(result);
            }
            else if (roleName == "المناديب")
            {

                var intDeliveryId = _myContext.Deliveries.Where(p => p.UserId == userId).Select(p => p.Id).FirstOrDefault();
                var result = Orders.Where(p => p.OrderStatus == query && p.DeliveryId == intDeliveryId).ToList();
                return View(result);
            }
            else
            {
                return View("NotFound");
            }


        }

        #endregion


        #region OrderReceipt
        public async Task<IActionResult> OrderReicept(int Id)
        {
            var orderViewModel = await _orderRepository.OrderViewModelById(Id);

            return View(orderViewModel);
        }
        #endregion
    }
}
