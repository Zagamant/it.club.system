﻿using System.BLL.Models.Helpers;

namespace System.BLL.Models.CostsManagement
{
    public class CostsModel : BaseModel
    {
        public int ClubId { get; set; }
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
        public string About { get; set; }

    }
}