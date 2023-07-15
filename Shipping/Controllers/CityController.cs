using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shipping.Constants;
using Shipping.Models;
using Shipping.Repository.CityRepo;
using Shipping.Repository.StateRepo;

namespace Shipping.Controllers
{
    public class CityController : Controller
    {
        ICityRepository _cityRepository;
        IStateRepository _stateRepository;
        public CityController(ICityRepository cityRepository, IStateRepository stateRepository)
        {
            _cityRepository = cityRepository;
            _stateRepository = stateRepository;
        }


        [Authorize(Permissions.Cities.View)]
        public IActionResult Index(int stateId)
        {
            ViewBag.StateId = stateId;
            return View(_cityRepository.GetAllByState(stateId));
        }

        #region Add
        [Authorize(Permissions.Cities.Create)]
        public IActionResult Add(int stateId)
        {
            ViewBag.stateId = stateId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Cities.Create)]
        public async Task<IActionResult> Add(int stateId, City city)
        {
            if (ModelState.IsValid)
            {
                _cityRepository.AddToState(stateId, city);
                return Redirect("/State/Index");
            }

            return View(city);
        }

        #endregion


        #region change state
        [Authorize(Permissions.Cities.Edit)]
        public IActionResult ChangeState(int Id, bool status)
        {
            var City = _cityRepository.GetById(Id);
            City.Status = status;
            _cityRepository.Update(Id, City);
            return RedirectToAction("Index");
        }
        #endregion


        #region Edit
        [Authorize(Permissions.Cities.Edit)]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var city = _cityRepository.GetById(id);

            if (city == null)
            {
                return View("NotFound");
            }
            var states = _stateRepository.GetAll().ToList();
            ViewBag.StateList = new SelectList(states, "Id", "Name");
            return View(city);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Cities.Edit)]
        public IActionResult Edit(int id, City city)
        {
            if (id != city.Id)
            {
                return View("NotFound");
            }
            _cityRepository.Update(id, city);

            return RedirectToAction(nameof(Index), new { stateId = city.StateId });
        }
        #endregion

        #region search
        [Authorize(Permissions.Cities.View)]
        public IActionResult Search(int id, string query)
        {

            List<City> city;
            if (string.IsNullOrWhiteSpace(query)) { city = _cityRepository.GetAllByState(id).ToList(); }
            else
            {
                city = _cityRepository.GetAllByState(id).Where(i => i.Name.ToUpper().Contains(query.ToUpper())).ToList();

            }
            return View("Index", city);
        }

        #endregion

        #region Delete
        [Authorize(Permissions.Cities.Delete)]
        public IActionResult Delete(int id)
        {

            City city = _cityRepository.GetById(id);

            if (city == null)
                return View("NotFound");
            

            city.IsDeleted = true;
            _cityRepository.Update(id, city);

            return RedirectToAction(nameof(Index), new { stateId = city.StateId });
        }

        #endregion
    }
}
