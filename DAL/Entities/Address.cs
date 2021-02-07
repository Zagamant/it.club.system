namespace System.DAL.Entities
{
	public class Address : BaseEntity
	{
		public string Country { get; set; }
		public string City { get; set; }
		public string AddressLine { get; set; }
	}
}
