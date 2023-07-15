using Shipping.Models;
using Shipping.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shipping.Constants;
using Shipping.ViewModels.ClaimsPermissions;
using System.Security.Claims;
using System.Data;
using Shipping.Repository.ArabicNamesColumnIntoRoleClaimsTable;

namespace Shipping.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserManager<ApplicationUser> _userManager { get; }
        public IAddArabicNamesToRoleCaimsTable IAddArabicNamesToRoleCaimsTable { get; }

        public AdministrationController(RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager, IAddArabicNamesToRoleCaimsTable _IAddArabicNamesToRoleCaimsTable)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            IAddArabicNamesToRoleCaimsTable = _IAddArabicNamesToRoleCaimsTable;
        }

        /*------------------------------- Roles --------------------------------------------------*/
        #region ListRoles
        [Authorize(Permissions.Controls.View)]
        [HttpGet]
        public async Task<IActionResult> ListRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return View(roles);
        }
        #endregion

        #region Add Role
        [Authorize(Permissions.Controls.Create)]
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [Authorize(Permissions.Controls.Create)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel RoleVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole applicationRole = new ApplicationRole();
                applicationRole.Id = Guid.NewGuid().ToString();
                applicationRole.Name = RoleVM.RoleName;
                applicationRole.Date = DateTime.Now.ToString();
                var result = await _roleManager.CreateAsync(applicationRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }
        #endregion

        #region Edit Roles
        [Authorize(Permissions.Controls.Edit)]
        [HttpGet]
        public async Task<IActionResult> EditRole(string ID)
        {
            var role = await _roleManager.FindByIdAsync(ID);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id : {ID} can't be found.";
                return View("Notfound");
            }
            else if (role.Name == "Admin" || role.Name == "التجار" || role.Name == "الموظفين" || role.Name == "المناديب")
            { return View("NotAllowed"); }
            else
            {
                var model = new EditRoleViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                return View(model);
            }
        }

        [HttpPost]
        [Authorize(Permissions.Controls.Edit)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id : {model.RoleId} can't be found.";
                return View("Notfound");
            }
            else if (role.Name == "Admin" || role.Name == "التجار" || role.Name == "الموظفين" || role.Name == "المناديب")
            { return View("NotAllowed"); }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                return View(model);
            }
        }

        #endregion

        #region Delete Role
        [Authorize(Permissions.Controls.Delete)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id : {RoleId} can't be found.";
                return View("NotFound");
            }
            else if (role.Name == "Admin")
            { return View("NotAllowed"); }
            else
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                    }
                    return RedirectToAction("ListRoles");
                }
            }
        }
        #endregion

        #region Search role
        [Authorize(Permissions.Controls.View)]
        public async Task<IActionResult> Search(string query)
        {
            List<ApplicationRole> roles;
            if (string.IsNullOrWhiteSpace(query))
            { roles = await _roleManager.Roles.ToListAsync(); }
            else
            {
                roles = await _roleManager.Roles.Where(r => r.Name.Contains(query)).ToListAsync();


            }
            return View("ListRoles", roles);
        }
        #endregion

        /*------------------------------- Cliams --------------------------------------------------*/
        #region Manage Permissions on roles
        [HttpGet]
        [Authorize(Permissions.Controls.View)]
        public async Task<IActionResult> ManagePermissions(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return View("NotFound");


            var roleClaims = _roleManager.GetClaimsAsync(role).Result.Select(c => c.Value).ToList();

            var allClaims = Permissions.GenerateAllPermissions();

            var allPermissions = allClaims.Select(p => new CheckBoxViewModel { DisplayValue = p }).ToList();

            foreach (var permission in allPermissions)
            {
                if (roleClaims.Any(c => c == permission.DisplayValue))
                    permission.IsSelected = true;
            }
            var viewModel = new PermissionsFormViewModel
            {
                RoleId = roleId,
                RoleName = role.Name,
                RoleCalims = allPermissions


            };
            return View(viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Controls.View)]
        public async Task<IActionResult> ManagePermissions(PermissionsFormViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);

            if (role == null)
                return View("NotFound");

            var roleClaims = await _roleManager.GetClaimsAsync(role);

            foreach (var claim in roleClaims)
                await _roleManager.RemoveClaimAsync(role, claim);

            var selectedClaims = model.RoleCalims.Where(c => c.IsSelected).ToList();


            
            foreach (var claim in selectedClaims)
            {
                var arabicName = "";
                foreach (var item in EnglishVsArabic.ModulesInEn_AR)
                {
                    if(item.Key == claim.DisplayValue.Split(".")[1])
                    {
                        arabicName = item.Value;
                        break;
                    }
                }
                await _roleManager.AddClaimAsync(role, new Claim("Permissions", claim.DisplayValue));

                var result = IAddArabicNamesToRoleCaimsTable.AddArabicNamesToRoleCaims(role, arabicName, claim.DisplayValue);
                if (!result)
                    return View("NotFound");
                
            }

            return RedirectToAction("ListRoles");
        }
        #endregion


    }
}
