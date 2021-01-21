using System.ComponentModel.DataAnnotations;

namespace System.DAL.Entities
{
	public class Photo
	{
		public int Id { get; set; }

		[Required]
		public byte[] PhotoAsBytes { get; set; }

		public int UserId { get; set; }

		public virtual User User { get; set; }
	}
}
