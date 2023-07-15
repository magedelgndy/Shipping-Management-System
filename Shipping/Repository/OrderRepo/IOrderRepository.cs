using Shipping.Models;
using Shipping.ViewModels;

namespace Shipping.Repository.OrderRepo
{
    public interface IOrderRepository
    {
        List<OrderViewModel> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<OrderViewModel> OrderViewModelById(int id);
        List<OrderViewModel> GetOrderByStatus(string orderStatus);
        void UpdateStatus(Order order, string status);
        void Add(OrderViewModel orderViewModel, ApplicationUser user);
        void Delete(Order order);
        string Edit(Order order, OrderViewModel orderViewModel, ApplicationUser user);
        void UpdateDelivery(Order order, int DeliveryId);

        void CalcShipping(OrderViewModel orderViewModel, ApplicationUser user);
        public List<string> GenerateTable(OrdersPlusDeliverysViewModel OrdersPlusDeliverys);

    }
}