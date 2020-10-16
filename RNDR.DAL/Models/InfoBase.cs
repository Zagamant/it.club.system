namespace System.DAL.Models
{
	public abstract class InfoBase
	{
		public virtual int Id { get; set; }
		public virtual int MemberId { get; set; }
		public virtual User Member { get; set; }
		public virtual string Name { get; set; }
		public virtual string MiddleName { get; set; }
		public virtual string Surname { get; set; }
		public virtual string PhoneNumber { get; set; }
		public virtual DateTime BirthDay { get; set; }
		public virtual string Skype { get; set; }
		public virtual Address Address { get; set; }
		public virtual string AdditionalInfo { get; set; }
		public virtual string Phone { get; set; }
		public virtual string Photo { get; set; }
	}
}
