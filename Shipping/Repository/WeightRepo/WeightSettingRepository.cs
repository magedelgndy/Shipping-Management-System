using Shipping.Models;

namespace Shipping.Repository.WeightRepo
{
    public class WeightSettingRepository : IWeightSettingRepository
    {
        MyContext _myContext;
        public WeightSettingRepository(MyContext myContext)
        {
            _myContext = myContext;
                
        }
        public int GetWeight()
        {
            return _myContext.weightSettings.Where(w => w.Id == 1).Select(w => w.Cost ).FirstOrDefault();
        }
        public int GetCost()
        {
            return _myContext.weightSettings.Where(w => w.Id == 1).Select(w => w.Addition_Cost).FirstOrDefault();
        }
    }
}
