using System;
using System.BLL.Helpers;
using System.BLL.Models.PhotoManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace System.BLL.PhotoManagement
{
    public class PhotoService : Repository<int, Photo, PhotoModel, PhotoModel, PhotoModel>, IPhotoService
    {
        public PhotoService(DataContext context, IMapper mapper, ILogger<PhotoService> logger) : base(context, mapper, logger)
        {
            _table = _context.Photos;
        }


        // public async Task<PhotoModel> AddAsync(PhotoModel entity)
        // {
        //     if (entity == null) throw new ArgumentNullException(nameof(entity));
        //
        //     var result = await _context.Photos
        //         .SingleOrDefaultAsync(ev => ev.Id == entity.Id);
        //
        //     if (result != null) throw new AppException($"Photo already exist.");
        //
        //     var map = _mapper.Map<Photo>(entity);
        //
        //     await _context.Photos
        //         .AddAsync(map);
        //
        //     await _context.SaveChangesAsync();
        //
        //     return entity;
        // }
        //
        // public async Task<PhotoModel> UpdateAsync(int id, PhotoModel newEntity)
        // {
        //     if (_context.Photos.SingleOrDefaultAsync(item => item.Id == id) == null)
        //     {
        //         throw new AppException("Not found");
        //     }
        //
        //     newEntity.Id = id;
        //
        //     var map = _mapper.Map<Photo>(newEntity);
        //
        //     _context.Photos
        //         .Update(map);
        //
        //     await _context.SaveChangesAsync();
        //
        //     return newEntity;
        // }
        
    }
}