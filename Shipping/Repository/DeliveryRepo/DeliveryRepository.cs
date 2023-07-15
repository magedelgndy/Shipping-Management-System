using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping.Models;
using Shipping.ViewModels;
using System.Net;
using static Shipping.Constants.Permissions;

namespace Shipping.Repository.DeliveryRepo
{
    public class DeliveryRepository : IDeliveryRepository
    {
        MyContext _myContext;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<ApplicationUser> _roleManager;
        public DeliveryRepository(MyContext context, UserManager<ApplicationUser> userManager)
        {
            _myContext = context;
            _userManager = userManager;
            //_roleManager = roleManager;
        }
        #region GetAllDeliveriesWithNameOrNon
        public async Task<List<DeliveryViewModel>> GetAll(string Name)
        {
            List<Delivery> deliveryList = new List<Delivery>();

            if (Name == null)
            {
                deliveryList = await _myContext.Deliveries
                .Include(m => m.User).Include(d => d.Branch).Where(m => m.User.IsDeleted == false)
                .ToListAsync();
            }
            else
            {
                deliveryList = await _myContext.Deliveries
                .Include(m => m.User)
                .Include(m => m.Branch)
                .Where(m => m.User.Name.Contains(Name) && m.User.IsDeleted == false)
                .ToListAsync();
            }
            //List<ApplicationUser> users = _context.Users.ToList();
            //List<Branch> branch = _context.Branches.ToList();

            List<DeliveryViewModel> deliveriesViewModel = new List<DeliveryViewModel>();
            foreach (var delivery in deliveryList)
            {
                deliveriesViewModel.Add(new DeliveryViewModel
                {
                    
                    DeliveryId = delivery.User.Id,
                    Address = delivery.Address,
                    BranchName = delivery.Branch.Name,
                    Email = delivery.User.Email,
                    Government = delivery.Governement,
                    Name = delivery.User.Name,
                    Password = delivery.User.PasswordHash,
                    Phone = delivery.User.PhoneNumber,
                    //BranchName = branch.Where(p => p.Id == delivery.BranchId).Select(p=>p.Name).SingleOrDefault(),
                    status = delivery.User.Status,
                    //Type = delivery.User.Type.ToString(),
                    DiscountType = delivery.DiscountType,
                    CompanyPercentage = delivery.CompanyPercent,
                    BranchId=delivery.BranchId,
                    OrignalIdOnlyInDeliveryTable=delivery.Id,
                    DeliverName = delivery.User.Name
                    


                });
            }
            return deliveriesViewModel;
        }
        #endregion

        #region AddDelivery
        public async Task<IdentityResult> AddDelivery(DeliveryViewModel deliveryViewModel)
        {

            var delivery = new Delivery
            {
                Governement = deliveryViewModel.Government,
                Address = deliveryViewModel.Address,
                DiscountType = deliveryViewModel.DiscountType,
                CompanyPercent = deliveryViewModel.CompanyPercentage,
                BranchId = _myContext.Branches.FirstOrDefault(b => b.Name == deliveryViewModel.BranchName).Id
            };

            var user = new ApplicationUser
            {
                Name = deliveryViewModel.Name,
                Email = deliveryViewModel.Email,
                PhoneNumber = deliveryViewModel.Phone,
                UserName = deliveryViewModel.Name

            };

            var result = await _userManager.CreateAsync(user, deliveryViewModel.Password);

            if (result.Succeeded)
            {
                var _user = await _userManager.FindByEmailAsync(user.Email);
                if (_user != null)
                    await _userManager.AddToRoleAsync(_user, "المناديب");

                delivery.UserId = _user.Id;
                _myContext.Add(delivery);
                _myContext.SaveChanges();

                return null;
            }
            IdentityResult errorMsgs = result;
            return errorMsgs;
        }
        #endregion

        #region SearchForDeliveryById

        public async Task<Delivery> GetDeliveryById(string id)
        {
            var delivery = await _myContext.Deliveries
            .Include(m => m.User).Where(m => m.User.IsDeleted == false)
            .FirstOrDefaultAsync(m => m.User.Id == id);
            if (delivery == null)
            {
                return null;
            }
            return delivery;
        }
        public async Task<DeliveryEditViewModel> GetById(string id)
        {
            var delivery = await _myContext.Deliveries
            .Include(m => m.User).Where(m => m.User.IsDeleted == false)
            .Include(m => m.Branch)
            .FirstOrDefaultAsync(m => m.User.Id == id);


            if (delivery == null)
            {
                return null;
            }

            var DeliveryEditViewModel = new DeliveryEditViewModel()
            {
                DeliveryId = delivery.User.Id,
                Address = delivery.Address,
                BranchName = delivery.Branch.Name,
                Email = delivery.User.Email,
                Government = delivery.Governement,
                Name = delivery.User.Name,
                Phone = delivery.User.PhoneNumber,
                DiscountType = delivery.DiscountType,
                CompanyPercentage = delivery.CompanyPercent,
            };
            return DeliveryEditViewModel;
        }
        #endregion

        #region EditMyDelivery
        public void EditDelivery(Delivery delivery, DeliveryEditViewModel deliveryEditViewModel)
        {
            if (delivery != null)
            {
                delivery.Address = deliveryEditViewModel.Address;
                delivery.Governement = deliveryEditViewModel.Government;
                delivery.BranchId = _myContext.Branches.FirstOrDefault(b => b.Name == deliveryEditViewModel.BranchName).Id;
                delivery.DiscountType = deliveryEditViewModel.DiscountType;
                delivery.CompanyPercent = deliveryEditViewModel.CompanyPercentage;
                delivery.User.Email = deliveryEditViewModel.Email;
                delivery.User.PhoneNumber = deliveryEditViewModel.Phone;
                delivery.User.Name = deliveryEditViewModel.Name;


                _myContext.SaveChanges();
            }
        }

        #endregion

        public void UpdateStatus(Delivery delivery, bool status)
        {
            if (delivery != null)
            {
                delivery.User.Status = status;
                _myContext.SaveChanges();
            }
        }

        public List<Branch> GetAllBranches()
        {
            var Branchs = _myContext.Branches.Where(b=>b.IsDeleted==false).ToList();
            return Branchs;
        }
        public List<State> GetAllStates()
        {
            var States = _myContext.States.Where(s=>s.IsDeleted==false).ToList();
            return States;
        }
    }
}