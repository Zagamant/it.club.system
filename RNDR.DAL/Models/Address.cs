namespace System.DAL.Models
{
	public class Address
	{
		public virtual int Id { get; set; }
		public virtual string Country { get; set; }
		public virtual string City { get; set; }
		public virtual string AddressLine { get; set; }
	}
}
