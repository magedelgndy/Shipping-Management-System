using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping.Models;
using Shipping.ViewModels;
using static Shipping.Constants.Permissions;

namespace Shipping.Repository.MerchantRepo
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<ApplicationRole> _roleManager;
        MyContext _myContext;

        public MerchantRepository(UserManager<ApplicationUser> userManager, MyContext myContext)
        {
            _userManager = userManager;
            _myContext = myContext;
            //_roleManager = roleManager;
        }


        #region addMerchant
        public async Task<IdentityResult> AddMechant(MerchantViewModel merchantViewModel)
        {
            var merchant = new Merchant
            {
                Address = merchantViewModel.Address,
                City = merchantViewModel.City,
                Government = merchantViewModel.Government,
                PickUpSpecialCost = merchantViewModel.PickUpPrice,
                RefusedOrderPercent = merchantViewModel.RefuseCostPercent,
                BranchId = _myContext.Branches.FirstOrDefault(b => b.Name == merchantViewModel.BranchName).Id,
                SpecialCitiesPrices = merchantViewModel.SpecialCities

            };
            var user = new ApplicationUser()
            {
                Email = merchantViewModel.Email,
                PhoneNumber = merchantViewModel.Phone,
                Name = merchantViewModel.Name,
                UserName = merchantViewModel.Name
                //Type = TypeOfUser.Merchant


            };
            var result = await _userManager.CreateAsync(user, merchantViewModel.Password);
            if (result.Succeeded)
            {
                var _user = await _userManager.FindByEmailAsync(user.Email);
                if (_user != null)
                    await _userManager.AddToRoleAsync(_user, "التجار");

                merchant.UserId = _user.Id;
                _myContext.Add(merchant);
                _myContext.SaveChanges();

                return null;
            }
            IdentityResult errorMsgs = result;
            return errorMsgs;
        }



        #endregion


        #region getAll

        public async Task<List<MerchantViewModel>> GetAll(string Name)
        {

            List<Merchant> merchants = new List<Merchant>();

            if (Name == null)
            {
                merchants = await _myContext.Merchants
                .Include(m => m.User)
                .Include(m => m.Branch)
                .ToListAsync();
            }
            else
            {
                merchants = await _myContext.Merchants
                .Include(m => m.User)
                .Include(m => m.Branch)
                .Where(m => m.User.Name.Contains(Name))
                .ToListAsync();
            }
            List<MerchantViewModel> merchantsViewModel = new List<MerchantViewModel>();
            foreach (var merchant in merchants)
            {
                var roles = await _userManager.GetRolesAsync(merchant.User);


                merchantsViewModel.Add(new MerchantViewModel
                {
                    MerchantId = merchant.User.Id,
                    Address = merchant.Address,
                    BranchName = merchant.Branch.Name,
                    City = merchant.City,
                    Email = merchant.User.Email,
                    Government = merchant.Government,
                    Name = merchant.User.Name,
                    Phone = merchant.User.PhoneNumber,
                    Password = merchant.User.PasswordHash,
                    PickUpPrice = merchant.PickUpSpecialCost,
                    RefuseCostPercent = merchant.RefusedOrderPercent,
                    Role = roles.FirstOrDefault(),
                    Status = merchant.User.Status


                });
            }
            return merchantsViewModel;
        }

        #endregion


        #region GetById
        public async Task<Merchant> GetById(string id)
        {
            var merchant = await _myContext.Merchants
            .Include(m => m.User)
            .Include(m => m.Branch)
            .Include(m=>m.SpecialCitiesPrices)
            .FirstOrDefaultAsync(m => m.User.Id == id);


            if (merchant == null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(merchant.User);
            var merchantViewModel = new MerchantViewModel
            {
                MerchantId = merchant.User.Id,
                Address = merchant.Address,
                BranchName = merchant.Branch.Name,
                City = merchant.City,
                Email = merchant.User.Email,
                Government = merchant.Government,
                Name = merchant.User.Name,
                Phone = merchant.User.PhoneNumber,
                Password = merchant.User.PasswordHash,
                PickUpPrice = merchant.PickUpSpecialCost,
                RefuseCostPercent = merchant.RefusedOrderPercent,
                Role = roles.FirstOrDefault()
            };

            return merchant;
        }

        public async Task<MerchantEditViewModel> MapToViewModel(Merchant merchant)
        {
            var roles = await _userManager.GetRolesAsync(merchant.User);
            var merchantEditViewModel = new MerchantEditViewModel
            {
                MerchantId = merchant.User.Id,
                Address = merchant.Address,
                BranchName = merchant.Branch.Name,
                City = merchant.City,
                Email = merchant.User.Email,
                Government = merchant.Government,
                Name = merchant.User.Name,
                Phone = merchant.User.PhoneNumber,
                PickUpPrice = merchant.PickUpSpecialCost,
                RefuseCostPercent = merchant.RefusedOrderPercent,
                SpecialCities = merchant.SpecialCitiesPrices
            };

            return merchantEditViewModel;
        }

        #endregion


        #region EditUser
        public async void EditMerchant(Merchant merchant, MerchantEditViewModel merchantEditViewModel)
        {
            var specialPrices = _myContext.SpecialCitiesPrice.Where(s => s.MerchantId == merchant.Id).ToList();

            if(specialPrices!=null)
            {
                foreach (var item in specialPrices)
                {
                    _myContext.Remove(item);
                }
            }


            if (merchant != null)
            {
                merchant.Address = merchantEditViewModel.Address;
                merchant.City = merchantEditViewModel.City;
                merchant.Government = merchantEditViewModel.Government;
                merchant.PickUpSpecialCost = merchantEditViewModel.PickUpPrice;
                merchant.RefusedOrderPercent = merchantEditViewModel.RefuseCostPercent;
                merchant.BranchId = _myContext.Branches.FirstOrDefault(b => b.Name == merchantEditViewModel.BranchName).Id;

                merchant.User.Email = merchantEditViewModel.Email;
                merchant.User.PhoneNumber = merchantEditViewModel.Phone;
                merchant.User.Name = merchantEditViewModel.Name;
                merchant.SpecialCitiesPrices = merchantEditViewModel.SpecialCities;

                _myContext.SaveChanges();
            }
        }



        #endregion


        #region updateStatus
        public void UpdateStatus(Merchant merchant, bool status)
        {
            if (merchant != null)
            {
                merchant.User.Status = status;
                _myContext.SaveChanges();
            }
        }


        #endregion

        public List<Branch> GetAllBranches()
        {
            var Branchs = _myContext.Branches.ToList();
            return Branchs;
        }
        public List<State> GetAllStates()
        {
            var States = _myContext.States.ToList();
            return States;
        }

        public List<City> GetAllCities()
        {
            var Cities = _myContext.Cities.ToList();
            return Cities;
        }
    }
}
