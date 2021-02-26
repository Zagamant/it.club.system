﻿using System.BLL.Models.AgreementManagement;
using System.Net.Http;
using BlazorClient.Services.Helpers;

namespace BlazorClient.Services.AgreementManagement
{
    public class AgreementService : Repository<int, AgreementModel, AgreementModel, AgreementModel>,
        IAgreementService
    {
        public AgreementService(HttpClient http) : base(http)
        {
        }
    }
}