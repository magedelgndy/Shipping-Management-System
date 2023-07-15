using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Shipping.Models;
using Shipping.Repository.CityRepo;
using Shipping.Repository.WeightRepo;
using Shipping.ViewModels;
using static NuGet.Packaging.PackagingConstants;

namespace Shipping.Repository.OrderRepo
{
    public class OrderRepositorty : IOrderRepository
    {
        MyContext _myContext;
        ICityRepository _cityRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        IWeightSettingRepository _weightSettingRepository;
        public OrderRepositorty(MyContext myContext, ICityRepository cityRepository, UserManager<ApplicationUser> userManager, IWeightSettingRepository weightSettingRepository)
        {
            _myContext = myContext;
            _cityRepository = cityRepository;
            _userManager = userManager;
            _weightSettingRepository = weightSettingRepository;

        }

        #region AddOrder
        public async void Add(OrderViewModel orderViewModel, ApplicationUser user)
        {
            var city = _myContext.Cities.FirstOrDefault(c => c.Name == orderViewModel.CityName);

            var userId = user.Id;
            var merchantId = _myContext.Merchants.Where(m => m.UserId == userId).FirstOrDefault().Id;
            Order order = new Order()
            {
                MerchantId = merchantId,
                CityId = city.Id,
                IsVillage = orderViewModel.IsVillage,
                ClientEmail = orderViewModel.ClientEmail,
                ClientName = orderViewModel.ClientName,
                ClientPhoneNumber1 = orderViewModel.ClientPhoneNumber1,
                ClientPhoneNumber2 = orderViewModel.ClientPhoneNumber2,
                Notes = orderViewModel.Notes,
                OrderCost = orderViewModel.OrderCost,
                PaymentType = orderViewModel.PaymentType,
                ShippingType = orderViewModel.ShippingType,
                StreetName = orderViewModel.StreetName,
                TotalWeight = orderViewModel.TotalWeight,
                Type = orderViewModel.Type,
                StateId = city.StateId,
                ShippingCost = orderViewModel.ShippingCost,
                orderProducts = orderViewModel.orderProducts


            };
            var BRANCH = _myContext.Branches.Where(b => b.Name == orderViewModel.BranchName).FirstOrDefault();
            if (BRANCH == null)
                return;

            order.BranchId = BRANCH.Id;
            

            _myContext.Orders.Add(order);
            _myContext.SaveChanges();
        }

        #endregion

        #region GetAllOrders
        public List<OrderViewModel> GetAllOrders()
        {
            var Orders = _myContext.Orders.Include(o => o.City).ThenInclude(c => c.State).Where(o => o.IsDeleted != true).ToList();
            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();
            foreach (var order in Orders)
            {
                orderViewModels.Add(new OrderViewModel()
                {
                    CityName = order.City.Name,
                    Id = order.SerialNumber,
                    ClientName = order.ClientName,
                    ClientEmail = order.ClientEmail,
                    ClientPhoneNumber1 = order.ClientPhoneNumber1,
                    ClientPhoneNumber2 = order.ClientPhoneNumber2,
                    Notes = order.Notes,
                    OrderCost = order.OrderCost,
                    PaymentType = order.PaymentType,
                    ShippingType = order.ShippingType,
                    StreetName = order.StreetName,
                    TotalWeight = order.TotalWeight,
                    Type = order.Type,
                    IsVillage = order.IsVillage,
                    StateName = order.City.State.Name,
                    OrderDate = order.Date,
                    OrderStatus = order.OrderStatus,
                    BranchId = order.BranchId,
                    DeliveryId = order.DeliveryId,
                    ShippingCost = order.ShippingCost,
                    TotalCost = order.TotalCost,
                    MerchantId = order.MerchantId



                });
            }
            return orderViewModels;
        }
        #endregion

        #region GetOrderById
        public async Task<Order> GetOrderById(int id)
        {
            var order = _myContext.Orders.Include(o => o.orderProducts).FirstOrDefault(o => o.SerialNumber == id);

            return order;
        }

        #endregion

        #region GetOrdersByStatus
        public List<OrderViewModel> GetOrderByStatus(string orderStatus)
        {
            var Orders = _myContext.Orders.Where(o => o.OrderStatus == orderStatus).Include(o => o.City).ThenInclude(c => c.State).Where(o => o.IsDeleted != true).ToList();

            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();
            foreach (var order in Orders)
            {
                orderViewModels.Add(new OrderViewModel()
                {
                    CityName = order.City.Name,
                    Id = order.SerialNumber,
                    ClientName = order.ClientName,
                    ClientEmail = order.ClientEmail,
                    ClientPhoneNumber1 = order.ClientPhoneNumber1,
                    ClientPhoneNumber2 = order.ClientPhoneNumber2,
                    Notes = order.Notes,
                    OrderCost = order.OrderCost,
                    PaymentType = order.PaymentType,
                    ShippingType = order.ShippingType,
                    StreetName = order.StreetName,
                    TotalWeight = order.TotalWeight,
                    Type = order.Type,
                    IsVillage = order.IsVillage,
                    StateName = order.City.State.Name,
                    OrderDate = order.Date,
                    OrderStatus = order.OrderStatus,
                    DeliveryId = order.DeliveryId,
                    BranchId = order.BranchId,
                    ShippingCost = order.ShippingCost,


                });
            }
            return orderViewModels;

        }

        #endregion

        #region UpdateStatus
        public void UpdateStatus(Order order, string status)
        {
            if (order != null)
            {
                order.OrderStatus = status;
                _myContext.SaveChanges();
            }
        }

        #endregion

        #region UpdateDelivery
        public void UpdateDelivery(Order order, int DeliveryId)
        {
            if (order != null)
            {
                order.OrderStatus = "قيد_الانتظار";
                order.DeliveryId = DeliveryId;

                _myContext.SaveChanges();
            }
        }

        #endregion

        #region GenerateTable
        public List<string> GenerateTable(OrdersPlusDeliverysViewModel OrdersPlusDeliverys)
        {
            List<string> Rows = new List<string>();
            if (OrdersPlusDeliverys.orders.Count != 0)
            {
                for (int i = 0; i < OrdersPlusDeliverys.orders.Count; i++)
                {
                    string x = $"<tr>" +
                                    $"<td>{OrdersPlusDeliverys.orders[i].Id}</td>" +
                                    $"<td>{OrdersPlusDeliverys.orders[i].OrderDate}</td>" +
                                    $"<td>{OrdersPlusDeliverys.orders[i].ClientName} <br /> {OrdersPlusDeliverys.orders[i].ClientPhoneNumber1}</td>" +
                                    /*$"<td>{OrdersPlusDeliverys.orders[i].StateName}</td>" +*/
                                    $"<td>{OrdersPlusDeliverys.orders[i].CityName}</td>" +
                                    $"<td>{OrdersPlusDeliverys.orders[i].OrderCost + OrdersPlusDeliverys.orders[i].ShippingCost}</td>" +
                                    /*$"<td>{OrdersPlusDeliverys.orders[i].ShippingCost} </td>" +*/

                                    $"<td> <a class='btn btn-outline-success' href='/Order/Edit/{OrdersPlusDeliverys.orders[i].Id}'>" +
                                        $"<i class='fa-solid fa-pen-to-square fa-1x'></i>" +
                                    $"</a></td>" +

                                    "<td>" +
                                    $"<select id = 'status_{OrdersPlusDeliverys.orders[i].Id}' class='form-select-cust' onchange='changeStatus({OrdersPlusDeliverys.orders[i].Id})'>" +
                        $"<option selected = 'true' disabled value = '{OrdersPlusDeliverys.orders[i].OrderStatus}'>{OrdersPlusDeliverys.orders[i].OrderStatus}</option>";
                    foreach (string statusName in Enum.GetNames(typeof(OrderStatus)))
                    {
                        x += $"<option value = '{statusName}'>{statusName} </option>";
                    }
                    x += $"</select>" +
                            $"</td>" +
                            $"<td>" +
                            $"<a class='btn btn-outline-danger' href='/Order/Delete/{OrdersPlusDeliverys.orders[i].Id}'>" +
                            $"<i class='fa-solid fa-trash-can fa-1x'></i></a>"+
                            $"</td>" +
                            "<td>" +
                            $"<a class='btn btn-outline-secondary' href='/Order/OrderReicept/{OrdersPlusDeliverys.orders[i].Id}'><i class='fa-solid fa-print fa-1x'></i></a>" +
                            "</td>" +
                            "<td>" +
                                $"<select id = 'delivery_{OrdersPlusDeliverys.orders[i].Id}' class= 'form-select-cust' onchange = 'AssignDelivery({OrdersPlusDeliverys.orders[i].Id})'>" +
                                $"<option value = '' > اختر المندوب</option>";
                    for (int j = 0; j < OrdersPlusDeliverys.deliveries.Count; j++)
                    {
                        var item1 = OrdersPlusDeliverys.deliveries[j];
                        if (OrdersPlusDeliverys.orders[i].DeliveryId == null)
                            OrdersPlusDeliverys.orders[i].DeliveryId = -1;

                        if (item1.BranchId == OrdersPlusDeliverys.orders[i].BranchId)
                        {
                            string type = "";
                            if (item1.OrignalIdOnlyInDeliveryTable == OrdersPlusDeliverys.orders[i].DeliveryId)
                                type = "selected";
                            else
                                type = "";

                            x += $"<option {type} value = '{item1.OrignalIdOnlyInDeliveryTable}' >{item1.Name}</option>";
                        }
                    }
                    x += "</select></td></tr>";
                    Rows.Add(x);
                }
            }
            return Rows;

        }

        #endregion

        #region GetOrderViewModelById
        public async Task<OrderViewModel> OrderViewModelById(int id)
        {
            var order = await _myContext.Orders.Include(e => e.City).ThenInclude(c => c.State).Include(o=>o.Branch).FirstOrDefaultAsync(o => o.SerialNumber == id);
            var orderProducts = await _myContext.OrderProducts.Where(o => o.OrderId == id).ToListAsync();

            OrderViewModel orderViewModel = new OrderViewModel()
            {
                CityName = order.City.Name,
                Id = order.SerialNumber,
                ClientName = order.ClientName,
                ClientEmail = order.ClientEmail,
                ClientPhoneNumber1 = order.ClientPhoneNumber1,
                ClientPhoneNumber2 = order.ClientPhoneNumber2,
                Notes = order.Notes,
                OrderCost = order.OrderCost,
                PaymentType = order.PaymentType,
                ShippingType = order.ShippingType,
                StreetName = order.StreetName,
                TotalWeight = order.TotalWeight,
                Type = order.Type,
                IsVillage = order.IsVillage,
                IsDeleted = order.IsDeleted,
                StateName = order.City.State.Name,
                OrderDate = order.Date,
                OrderStatus = order.OrderStatus,
                orderProducts = orderProducts,
                ShippingCost= order.ShippingCost,
                TotalCost = order.TotalCost,
                BranchName = order.Branch.Name
            };
            return orderViewModel;
        }
        #endregion

        #region Delete
        public void Delete(Order order)
        {
            order.IsDeleted = true;
            _myContext.SaveChanges();
        }
        #endregion

        #region EditOrder
        public string Edit(Order order, OrderViewModel orderViewModel, ApplicationUser user)
        {
            var merchanttt = _myContext.Merchants.Where(m => m.UserId == user.Id).FirstOrDefault();
            if(merchanttt == null)
            {

                return "لا يمكنك التعديل على الاوردر فقط التاجر هو الذى لدية الصلاحيه";
                 
            }

            var city = _myContext.Cities.FirstOrDefault(c => c.Name == orderViewModel.CityName);
            if (orderViewModel != null)
            {
                var items = _myContext.OrderProducts.Where(o => o.OrderId == orderViewModel.Id).ToList();
                foreach (var item in items)
                {

                    _myContext.OrderProducts.Remove(item);
                    //var existingProduct = _myContext.OrderProducts.FirstOrDefault(o => o.ProductName == item.ProductName && o.Id == order.SerialNumber);
                    //if (existingProduct != null)
                    //{
                    //    _myContext.OrderProducts.Remove(existingProduct);
                    //}
                }
            }
            if (order != null)
            {
                CalcShipping(orderViewModel, user);
                order.CityId = city.Id;
                order.IsVillage = orderViewModel.IsVillage;
                order.ClientEmail = orderViewModel.ClientEmail;
                order.ClientName = orderViewModel.ClientName;
                order.ClientPhoneNumber1 = orderViewModel.ClientPhoneNumber1;
                order.ClientPhoneNumber2 = orderViewModel.ClientPhoneNumber2;
                order.Notes = orderViewModel.Notes;
                order.IsDeleted = orderViewModel.IsDeleted;
                order.OrderCost = orderViewModel.OrderCost;
                order.PaymentType = orderViewModel.PaymentType;
                order.ShippingType = orderViewModel.ShippingType;
                order.StreetName = orderViewModel.StreetName;
                order.TotalWeight = orderViewModel.TotalWeight;
                order.Type = orderViewModel.Type;
                order.StateId = city.StateId;
                order.orderProducts = orderViewModel.orderProducts;
                order.ShippingCost = orderViewModel.ShippingCost;
                _myContext.SaveChanges();
            }
            return "";
        }
        #endregion

        public async void CalcShipping(OrderViewModel orderViewModel, ApplicationUser user)
        {
            int merchantId;
            var userId = user.Id;
            var merchantt = _myContext.Merchants.Where(m => m.UserId == userId).FirstOrDefault();
            if(merchantt == null)
            {
                return;
            }
            else { merchantId = merchantt.Id; }

            var specialPrices = _myContext.SpecialCitiesPrice.Where(s => s.MerchantId == merchantId && orderViewModel.CityName == s.City).FirstOrDefault();

            var OrderWeight = Math.Ceiling((double)orderViewModel.TotalWeight / 1000);

            var MaxWeight = _weightSettingRepository.GetWeight();
            var additionCost = _weightSettingRepository.GetCost();

            if (OrderWeight > MaxWeight)
            {
                orderViewModel.ShippingCost += (int)(OrderWeight - MaxWeight) * additionCost;
            }

            if (orderViewModel.IsVillage == true)
                orderViewModel.ShippingCost += 20;
            if (orderViewModel.ShippingType == ShippingType.توصيل_في_نفس_اليوم)
            {
                orderViewModel.ShippingCost += 50;
            }
            else if (orderViewModel.ShippingType == ShippingType.توصيل_سريع)
            {
                orderViewModel.ShippingCost += 20;
            }

            if (orderViewModel.PaymentType == PaymentType.واجبة_التحصيل)
            {
                orderViewModel.ShippingCost += 10;
            }

            if (specialPrices != null)
                orderViewModel.ShippingCost += specialPrices.Price;
            else if (orderViewModel.Type == Models.Type.تسليم_فالفرع)
                orderViewModel.ShippingCost += _cityRepository.GetByName(orderViewModel.CityName).PickUpPrice;

            else if (orderViewModel.Type == Models.Type.توصيل_الي_المنزل)
                orderViewModel.ShippingCost += _cityRepository.GetByName(orderViewModel.CityName).ShippingPrice;

        }


    }

}
