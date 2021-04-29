using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SchoolAPI.ActionFilters;

namespace SchoolAPI.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UsersController : CommonProperties
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public UsersController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = "getAllUsers")]
        //[ServiceFilter(typeof(ActionFilterExample))]
        public override IActionResult Get()
        {
            var users = _repository.User.GetAllUsers(trackChanges: false);

            var userDto = _mapper.Map<IEnumerable<UserDto>>(users);
            //uncomment the code below to test the global exception handling
            //throw new Exception("Exception");
            return Ok(userDto);
        }

        [HttpGet("{id}", Name = "getUserById")]
        //[ServiceFilter(typeof(ActionFilterExample))]
        public override IActionResult Get(Guid id)
        {
            var user = _repository.User.GetUser(id, trackChanges: false); if (user == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);
            }
        }

        // Paging Test: https://localhost:5001/api/v1/users?pageNumber=2&pageSize=2
        // Filtering Test: https://localhost:5001/api/v1/users/paging?MinAge=21&MaxAge=30
        // Searching Test: https://localhost:5001/api/v1/users/paging?name=tomas&MinAge=21&MaxAge=30
        // Sorting Test: https://localhost:5001/api/v1/users/paging?orderBy=name desc&MinAge=21&MaxAge=30
        [HttpGet("paging",Name = "getPagingUsers")]
        public IActionResult GetUsers([FromQuery] UserParameters userParameters)
        {

            if (!userParameters.ValidAgeRange)
            {
                return BadRequest("Max age can't be less than min age.");
            }
            

            var users = _repository.User.GetUsers(userParameters);

            var metadata = new
            {
                users.TotalCount,
                users.PageSize,
                users.CurrentPage,
                users.TotalPages,
                users.HasNext,
                users.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned {users.Count()} users from database.");
            return Ok(users);
        }

        [HttpPost(Name = "createUser")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public override IActionResult Create([FromBody] CreateItem item)
        {
            if (item == null)
            {
                _logger.LogError("User ForUserDto object sent from client is null.");
                return BadRequest("User ForUserDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the UserForCreationDto object");
                return UnprocessableEntity(ModelState);
            }

            var userEntity = _mapper.Map<User>(item);

            _repository.User.CreateUser(userEntity);
            _repository.Save();

            var userToReturn = _mapper.Map<UserDto>(userEntity);

            return CreatedAtRoute("getUserById", new { id = userToReturn.Id }, userToReturn);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public override IActionResult Update(Guid id, [FromBody] NameString str)
        {
            if (str == null)
            {
                _logger.LogError("UserForUpdateDto object sent from client is null.");
                return BadRequest("UserForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the UserForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var userEntity = _repository.User.GetUser(id, trackChanges: true);
            if (userEntity == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(str, userEntity);
            _repository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public override IActionResult Delete(Guid id)
        {
            var user = _repository.User.GetUser(id, trackChanges: false);
            if (user == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.User.DeleteUser(user);
            _repository.Save();

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Patch(Guid id, [FromBody] NameString str)
        {
            if (str == null)
            {
                _logger.LogError("UserForUpdateDto object sent from client is null.");
                return BadRequest("UserForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the UserForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var userEntity = _repository.User.GetUser(id, trackChanges: true);
            if (userEntity == null)
            {
                _logger.LogInfo($"User with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(str, userEntity);
            _repository.Save();

            return NoContent();
        }
    }
}