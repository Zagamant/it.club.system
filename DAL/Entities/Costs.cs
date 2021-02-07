namespace System.DAL.Entities
{
    public class Costs : BaseEntity
    {
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
        public string About { get; set; }
    }
}