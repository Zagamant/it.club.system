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

		public async Task Create(CourseRegisterModel course)
		{
			if (course == null) throw new ArgumentNullException(nameof(course));

			var courseOrig = _mapper.Map<Course>(course);
			await _context.Courses.AddAsync(courseOrig);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<CourseModel>> GetAllCourses()
		{
			var courses = await _context.Courses
				.AsNoTracking()
				.Select(c => new CourseModel
			{
				Title = c.Title,
				ManualLink = c.ManualLink,
				About = c.About,
				Groups = c.Groups
			})
				.AsQueryable()
				.ToListAsync();
			return courses;
		}

		public async Task<CourseModel> GetCourse(CourseModel course)
		{
			if (course == null) throw new ArgumentNullException(nameof(course));

			var courseOrig =
				await _context.Courses
					.AsNoTracking()
					.SingleAsync(c => c.Title == course.Title && c.ManualLink == course.ManualLink);

			var result = _mapper.Map<CourseModel>(courseOrig);
			return result;
		}

		public async Task Update(int courseId, CourseModel newCourse)
		{
			if (newCourse == null) throw new ArgumentNullException(nameof(newCourse));

			var courseOrig = _mapper.Map<Course>(newCourse);
			courseOrig.Id = courseId;

			_context.Courses.Update(courseOrig);

			await _context.SaveChangesAsync();
		}

		public async Task Update(CourseModel course, CourseModel newCourse)
		{
			if (course == null) throw new ArgumentNullException(nameof(course));
			if (newCourse == null) throw new ArgumentNullException(nameof(newCourse));

			var courseOrig =
				await _context.Courses.SingleOrDefaultAsync(c => c.Title == course.Title);

			if (courseOrig == null) throw new AppException(nameof(course) + " input incorrect.");

			var courseNewOrig = _mapper.Map<Course>(newCourse);
			courseNewOrig.Id = courseOrig.Id;
			
			_context.Courses.Update(courseNewOrig);

			await _context.SaveChangesAsync();
		}

		public async Task Remove(int courseId)
		{
			var courseOrig = await _context.Courses
				.FirstOrDefaultAsync(c => c.Id == courseId);

			if (courseOrig == null) throw new AppException($"Course with id: {courseId} not found.");

			_context.Courses.Remove(courseOrig);

			await _context.SaveChangesAsync();
		}

		public async Task Remove(CourseModel course)
		{
			if (course == null) throw new ArgumentNullException(nameof(course));

			var courseOrig =
				await _context.Courses.SingleOrDefaultAsync(c => c.Title == course.Title && c.ManualLink == course.ManualLink);

			if (courseOrig == null) throw new AppException(nameof(course) + " input incorrect.");

			_context.Courses.Remove(courseOrig);

			await _context.SaveChangesAsync();
		}
	}
}
