﻿using System.BLL.Helpers;
using System.BLL.Models.EventManagement;

namespace System.BLL.EventManagement
{
	public interface IEventService : IRepository<int, EventModel,EventModel,EventModel>
	{
	}
}
