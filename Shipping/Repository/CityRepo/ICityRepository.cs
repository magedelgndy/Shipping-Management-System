using Shipping.Models;

namespace Shipping.Repository.CityRepo
{
    public interface ICityRepository
    {
        List<City> GetAllByState(int stateId);
        List<string> GetAllByStateName(string stateName);
        void AddToState(int stateId, City city);
        City GetById(int id);
        City GetByName(string name);
        void Update(int id, City city);
    }
}
