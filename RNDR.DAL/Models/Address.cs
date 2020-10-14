namespace RNDR.DAL.Models
{
	public class Address
	{
		public virtual User UserId { get; set; }
		public virtual string Country { get; set; }
		public virtual string City { get; set; }
		public virtual string AddressLine { get; set; }
	}
}
