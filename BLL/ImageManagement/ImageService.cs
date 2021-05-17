using System.BLL.Models.ImageManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace System.BLL.ImageManagement
{
    public class ImageService : IImageService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private ILogger _logger;

        public ImageService(IMapper mapper, DataContext context, ILogger<ImageService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<IEnumerable<ImageModel>> GetAsync()
        {
            return await _context.Images.Select(el => _mapper.Map<ImageModel>(el)).ToListAsync();
        }

        public async Task<ImageModel> GetByIdAsync(int id)
        {
            return _mapper.Map<ImageModel>(await _context.Images.SingleOrDefaultAsync(el => el.Id == id));
        }

        public async Task<IEnumerable<ImageModel>> GetByUserAsync(int userId)
        {
            return await _context.Images.Where(el => el.UserId == userId).Select(el => _mapper.Map<ImageModel>(el))
                .ToListAsync();
        }

        public async Task<ImageModel> UploadAsync(UploadImageModel image)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == image.UserId);

            var username = user != null
                ? user.UserName
                : throw new ArgumentException($"There is no user with {image.UserId}");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Images", $"{username}",
                $"{Guid.NewGuid()}.{Path.GetExtension(image.Image.FileName)}");

            var entity = new Image
            {
                UserId = image.UserId,
                Path = path
            };

            await using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await image.Image.CopyToAsync(fileStream);
            }

            await _context.Images.AddAsync(entity);

            return _mapper.Map<ImageModel>(entity);
        }

        public async Task Delete(int id)
        {
            var entityToDelete = await _context.Images.FirstOrDefaultAsync(image => image.Id == id) ??
                                 throw new ArgumentException($"Cannot find image with id: {id}");

            _context.Images.Remove(entityToDelete);
            File.Delete(entityToDelete.Path);
            
        }
    }
}