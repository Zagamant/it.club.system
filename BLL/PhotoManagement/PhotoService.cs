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

namespace System.BLL.PhotoManagement
{
    public class PhotoService : IPhotoService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PhotoService(DataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PhotoModel> GetAsync(int id)
        {
            var result = await _context.Photos
                .SingleOrDefaultAsync(photo => photo.Id == id);

            return _mapper.Map<PhotoModel>(result);
        }

        public async Task<PhotoModel> AddAsync(PhotoModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var result = await _context.Photos
                .SingleOrDefaultAsync(ev => ev.Id == entity.Id);

            if (result != null) throw new AppException($"Photo already exist.");

            var map = _mapper.Map<Photo>(entity);

            await _context.Photos
                .AddAsync(map);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<PhotoModel>> GetAllAsync()
        {
            return await _context.Photos.Select(item => 
                _mapper.Map<PhotoModel>(item)).ToListAsync();
        }

        public async Task<PhotoModel> UpdateAsync(int id, PhotoModel newEntity)
        {
            if (_context.Photos.SingleOrDefaultAsync(item => item.Id == id) == null)
            {
                throw new AppException("Not found");
            }

            newEntity.Id = id;

            var map = _mapper.Map<Photo>(newEntity);

            _context.Photos
                .Update(map);

            await _context.SaveChangesAsync();

            return newEntity;
        }
        
        public async Task DeleteAsync(int id)
        {
            var photo = await _context.Photos.SingleOrDefaultAsync(ph => ph.Id == id);

            if (photo == null) throw new AppException($"Cant find photo with id: {id}");

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();
        }
    }
}