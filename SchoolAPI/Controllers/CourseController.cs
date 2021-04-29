using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SchoolAPI.ActionFilters;

namespace SchoolAPI.Controllers
{
    [Route("api/v1/courses")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CoursesController : CommonProperties
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CoursesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = "getAllCourses")]
        //[ServiceFilter(typeof(ActionFilterExample))]
        public override IActionResult Get()
        {
            var courses = _repository.Course.GetAllCourses(trackChanges: false);

            var courseDto = _mapper.Map<IEnumerable<CourseDto>>(courses);
            //uncomment the code below to test the global exception handling
            //throw new Exception("Exception");
            return Ok(courseDto);
        }

        [HttpGet("{id}", Name = "getCourseById")]
        //[ServiceFilter(typeof(ActionFilterExample))]
        public override IActionResult Get(Guid id)
        {
            var course = _repository.Course.GetCourse(id, trackChanges: false); if (course == null)
            {
                _logger.LogInfo($"Course with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var courseDto = _mapper.Map<CourseDto>(course);
                return Ok(courseDto);
            }
        }

        // Paging Test: https://localhost:5001/api/v1/courses/paging?pageNumber=1&pageSize=2
        // Searching Test: https://localhost:5001/api/v1/courses/paging?name=advanced web applications
        [HttpGet("paging", Name = "getPagingCourses")]
        public IActionResult GetCourses([FromQuery] CourseParameters courseParameters)
        {
            var courses = _repository.Course.GetCourses(courseParameters);

            var metadata = new
            {
                courses.TotalCount,
                courses.PageSize,
                courses.CurrentPage,
                courses.TotalPages,
                courses.HasNext,
                courses.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned {courses.Count()} courses from database.");
            
            return Ok(courses);
        }

        [HttpPost(Name = "createCourse")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public override IActionResult Create([FromBody] CreateItem item)
        {
            if (item == null)
            {
                _logger.LogError("Course ForCreationDto object sent from client is null.");
                return BadRequest("Course ForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CourseForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var courseEntity = _mapper.Map<Course>(item);

            _repository.Course.CreateCourse(courseEntity);
            _repository.Save();

            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);

            return CreatedAtRoute("getCourseById", new { id = courseToReturn.Id }, courseToReturn);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public override IActionResult Update(Guid id, [FromBody] NameString str)
        {
            if (str == null)
            {
                _logger.LogError("CourseForUpdateDto object sent from client is null.");
                return BadRequest("CourseForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CourseForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var courseEntity = _repository.Course.GetCourse(id, trackChanges: true);
            if (courseEntity == null)
            {
                _logger.LogInfo($"Course with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(str, courseEntity);
            _repository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public override IActionResult Delete(Guid id)
        {
            var course = _repository.Course.GetCourse(id, trackChanges: false);
            if (course == null)
            {
                _logger.LogInfo($"Course with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Course.DeleteCourse(course);
            _repository.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Patch(Guid id, [FromBody] NameString str)
        {
            if (str == null)
            {
                _logger.LogError("CourseForUpdateDto object sent from client is null.");
                return BadRequest("CourseForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CourseForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var courseEntity = _repository.Course.GetCourse(id, trackChanges: true);
            if (courseEntity == null)
            {
                _logger.LogInfo($"Course with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(str, courseEntity);
            _repository.Save();

            return NoContent();
        }
    }
}