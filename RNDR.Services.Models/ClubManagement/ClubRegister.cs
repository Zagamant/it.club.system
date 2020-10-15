using System.DAL.Enums;
using System.DAL.Models;

namespace System.BLL.Models.ClubManagement
{
	public class ClubRegister
	{
		public virtual string Title { get; set; }
		public virtual Address Address { get; set; }
		public virtual ClubStatus Status { get; set; }

	}
}
