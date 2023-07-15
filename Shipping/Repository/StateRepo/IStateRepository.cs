using Shipping.Models;

namespace Shipping.Repository.StateRepo
{
    public interface IStateRepository
    {
        List<State> GetAll();
        List<string> GetAllNames();
        void Add(State state);


        State GetById(int id);
        void Update(int id, State state);
        void UpdateStatus(State state, bool status);
    }
}
