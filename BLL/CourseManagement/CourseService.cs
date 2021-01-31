using System.BLL.Helpers;
using System.BLL.Models.CourseManagement;
using System.Collections.Generic;
using System.DAL;
using System.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace System.BLL.CourseManagement
{
	public class CourseService : ICourseService
	{
		private DataContext _context;
		private IMapper _mapper;

		public CourseService(DataContext context, IMapper mapper)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		public async Task<CourseModel> AddAsync(CourseRegisterModel course)
		{
			if (course == null) throw new ArgumentNullException(nameof(course));

			var courseOrig = _mapper.Map<Course>(course);
			await _context.Courses.AddAsync(courseOrig);
			await _context.SaveChangesAsync();
			
			return _mapper.Map<CourseModel>(courseOrig);
		}

		public async Task<IEnumerable<CourseModel>> GetAllAsync()
		{
			var courses = await _context.Courses
				.Select(c => _mapper.Map<CourseModel>(c)).ToListAsync();
			return courses;
		}
		
		public async Task<CourseModel> GetAsync(int id)
		{
			var courseOrig =
				await _context.Courses
					.SingleAsync(c => c.Id == id);

			var result = _mapper.Map<CourseModel>(courseOrig);
			return result;
		}

		public async Task<CourseModel> UpdateAsync(int courseId, CourseModel newCourse)
		{
			if (newCourse == null) throw new ArgumentNullException(nameof(newCourse));

			var courseOrig = _mapper.Map<Course>(newCourse);
			courseOrig.Id = courseId;

			_context.Courses.Update(courseOrig);

			await _context.SaveChangesAsync();
			
			return _mapper.Map<CourseModel>(courseOrig);
		}

		public async Task DeleteAsync(int courseId, bool isDelete = false)
		{
			var courseOrig = await _context.Courses
				.FirstOrDefaultAsync(c => c.Id == courseId);

			if (courseOrig == null) throw new AppException($"Course with id: {courseId} not found.");

			_context.Courses.Remove(courseOrig);

			await _context.SaveChangesAsync();
		}
	}
}
