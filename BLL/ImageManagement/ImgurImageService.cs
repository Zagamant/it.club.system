using System.BLL.Models.Helpers;
using System.BLL.Models.ImageManagement;
using System.BLL.Models.ImageManagement.Imgur;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace System.BLL.ImageManagement
{
    public class ImgurImageService : IImageService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ImgurSettings _imgurSettings;

        private const string ImgurBaseUrl = @"https://api.imgur.com/3/";

        public ImgurImageService(IMapper mapper, DataContext context, ILogger<ImageService> logger,
            IOptions<ImgurSettings> imgurSettings)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _imgurSettings = imgurSettings.Value;
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
            var client = new RestClient($"{ImgurBaseUrl}upload")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST)
            {
                AlwaysMultipartFormData = true
            };

            request.AddHeader("Authorization", $"Client-ID {_imgurSettings.ClientId}");
            request.AddParameter("image", image.Image);

            var response = await client.ExecuteAsync(request);

            var responseObj = JsonConvert.DeserializeObject<ImageResponse>(response.Content);

            var imageModel = new Image
            {
                Path = responseObj.ResponseData.Link,
                UserId = image.UserId
            };

            await _context.Images.AddAsync(imageModel);

            return _mapper.Map<ImageModel>(imageModel);
        }

        public async Task Delete(int id)
        {
            var entityToDelete = await _context.Images.FirstOrDefaultAsync(image => image.Id == id) ??
                                 throw new ArgumentException($"Cannot find image with id: {id}");

            _context.Images.Remove(entityToDelete);
        }
    }
}