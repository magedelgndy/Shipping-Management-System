namespace Shipping.Models
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }= false;
        public bool Status { get; set; } = true;

        public List<City>? Cities { get; set; }
        public virtual List<Branch>? Branches { get; set; }
    }
}
