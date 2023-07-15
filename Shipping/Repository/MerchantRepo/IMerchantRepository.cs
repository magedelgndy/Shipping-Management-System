using Microsoft.AspNetCore.Identity;
using Shipping.Models;
using Shipping.ViewModels;

namespace Shipping.Repository.MerchantRepo
{
    public interface IMerchantRepository
    {

        Task<List<MerchantViewModel>> GetAll(string Name);

        Task<Merchant> GetById(string id);
        Task<MerchantEditViewModel> MapToViewModel(Merchant merchant);

        Task<IdentityResult> AddMechant(MerchantViewModel merchantViewModel);

        void EditMerchant(Merchant merchant, MerchantEditViewModel merchantEditViewModel);

        void UpdateStatus(Merchant merchant, bool status);

        List<Branch> GetAllBranches();
        List<State> GetAllStates();
        List<City> GetAllCities();
    }
}
