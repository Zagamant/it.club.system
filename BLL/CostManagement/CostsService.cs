using System.BLL.Helpers;
using System.BLL.Models.CostsManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.CostManagement
{
    public class CostsService :  Repository<int, Costs, CostsModel,CostsModel,CostsModel>, ICostsService
    {
        public CostsService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _table = _context.Costs;
        }
    }
}