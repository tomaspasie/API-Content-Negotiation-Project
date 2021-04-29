using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Entities.Helpers;

namespace Repository
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Course> GetAllCourses(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToList();

        public Course GetCourse(Guid companyId, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(companyId), trackChanges)
                .SingleOrDefault();

        public void CreateCourse(Course course) => Create(course);

        public IEnumerable<Course> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToList();

        public void DeleteCourse(Course course)
        {
            Delete(course);
        }

        public PagedList<Course> GetCourses(CourseParameters courseParameters)
        {
            var courses = FindByCondition(o => o.Name.Length >= 0)
                .OrderBy(on => on.Name);

            SearchByName(ref courses, courseParameters.Name);

            return PagedList<Course>.ToPagedList(courses,
                courseParameters.PageNumber,
                courseParameters.PageSize);
        }

        private void SearchByName(ref IOrderedQueryable<Course> courses, string courseName)
        {
            if (!courses.Any() || string.IsNullOrWhiteSpace(courseName))
                return;
            courses = (IOrderedQueryable<Course>)courses.Where(o => o.Name.ToLower().Contains(courseName.Trim().ToLower()));
        }
    }
}