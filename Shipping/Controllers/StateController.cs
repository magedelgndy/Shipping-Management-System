using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shipping.Constants;
using Shipping.Models;
using Shipping.Repository.StateRepo;

namespace Shipping.Controllers
{
    public class StateController : Controller
    {
        IStateRepository _stateRepository;
        public StateController(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }


        #region View
        [Authorize(Permissions.Staties.View)]

        public IActionResult Index()
        {
            return View(_stateRepository.GetAll());
        }

        #endregion

        #region Add
        [Authorize(Permissions.Staties.Create)]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Staties.Create)]
        public async Task<IActionResult> Add(State state)
        {
            if (ModelState.IsValid)
            {
                _stateRepository.Add(state);
                return RedirectToAction("Index");
            }

            return View(state);
        }

        #endregion

        #region Edit
        [Authorize(Permissions.Staties.Edit)]
        public IActionResult Edit(int id)
        {
            var state = _stateRepository.GetById(id);

            if (state == null)
            {
                return NotFound();
            }
            return View(state);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Staties.Edit)]
        public IActionResult Edit(int id, State state)
        {
            if (id != state.Id)
            {
                return View("NotFound");
            }

            _stateRepository.Update(id, state);




            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region changeStatus
        [Authorize(Permissions.Staties.Delete)]
        public async Task<IActionResult> ChangeState(int Id, bool status)
        {
            var state = _stateRepository.GetById(Id);
            _stateRepository.UpdateStatus(state, status);
            return RedirectToAction("Index");
        }

        #endregion

        #region search
        [Authorize(Permissions.Staties.View)]
        public IActionResult Search(string query)
        {
            List<State> state;
            if (string.IsNullOrWhiteSpace(query)) { state = _stateRepository.GetAll().ToList(); }
            else
            {
                state = _stateRepository.GetAll().Where(i => i.Name.Contains(query)).ToList();

            }
            return View("Index", state);
        }

        #endregion

        #region Delete
        [Authorize(Permissions.Staties.Delete)]
        public IActionResult Delete(int id)
        {

            State state = _stateRepository.GetById(id);

            if (state == null)
                return View("NotFound");
            

            state.IsDeleted = true;
            _stateRepository.Update(id, state);
            return RedirectToAction(nameof(Index));
        }

#endregion
    }
}
