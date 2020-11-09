using System.ComponentModel.DataAnnotations;

namespace System.DAL.Entities
{
	public class Photo
	{
		public virtual int Id { get; set; }

		[Required]
		public virtual byte[] PhotoAsBytes { get; set; }

		public virtual int UserId { get; set; }

		public virtual User User { get; set; }
	}
}
