﻿using System.BLL.Helpers;
using System.BLL.Models.ClubManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.BLL.ClubManagement
{
	public interface IClubService : IBaseService<int, ClubModel,ClubModel,ClubModel>
	{
		Task<ClubModel> AddRoomAsync(int clubId, int roomId, string userId);
		Task<ClubModel> RemoveRoomAsync(int clubId, int roomId, string userId, bool isDeleteRoom = false);
		Task<IEnumerable<ClubModel>> GetByUserId(int userId);
	}
}
