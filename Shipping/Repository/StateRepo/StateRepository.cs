using Microsoft.EntityFrameworkCore;
using Shipping.Models;

namespace Shipping.Repository.StateRepo
{
    public class StateRepository : IStateRepository
    {
        MyContext _myContext;
        public StateRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public void Add(State state)
        {
            _myContext.States.Add(state);
            _myContext.SaveChanges();
        }

        public List<State> GetAll()
        {
            return _myContext.States.Where(s => s.IsDeleted != true).ToList();
        }

        public List<string> GetAllNames()
        {
            return _myContext.States.Where(s => s.IsDeleted != true && s.Status==true).Select(s => s.Name).ToList();
        }

        public State GetById(int id)
        {
            return _myContext.States.Where(p => p.Id == id && p.IsDeleted == false).FirstOrDefault();


        }

        public void Update(int id, State state)
        {
            State oldstate = GetById(id);
            oldstate.Name = state.Name;
            oldstate.Status = state.Status;
            oldstate.IsDeleted = state.IsDeleted;

            _myContext.SaveChanges();
        }

        public void UpdateStatus(State state, bool status)
        {
            if (state != null)
            {
                state.Status = status;
                _myContext.SaveChanges();
            }
        }
    }


}
