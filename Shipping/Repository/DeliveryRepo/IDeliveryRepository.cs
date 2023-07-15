using Shipping.ViewModels;
using Shipping.Models;
using Microsoft.AspNetCore.Identity;

namespace Shipping.Repository.DeliveryRepo
{
    public interface IDeliveryRepository
    {
        Task<List<DeliveryViewModel>> GetAll(string Name);
        Task<DeliveryEditViewModel> GetById(string id);
        Task<IdentityResult> AddDelivery(DeliveryViewModel deliveryViewModel);
        void EditDelivery(Delivery delivery, DeliveryEditViewModel deliveryEditViewModel);
        void UpdateStatus(Delivery delivery, bool status);
        Task<Delivery> GetDeliveryById(string id);
        List<Branch> GetAllBranches();
        List<State> GetAllStates();
    }
}
