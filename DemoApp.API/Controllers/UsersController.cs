using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DemoApp.API.Dtos;
using DemoApp.API.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.API.Controllers {
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IDataRepository _repo;
        private readonly IMapper _mapper;
        public UsersController (IDataRepository repo, IMapper mapper) {
            _mapper = mapper;
            this._repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers () {
            var users = await _repo.GetUsers ();
            var usersToReturn = _mapper.Map<IEnumerable<UserForListdto>>(users);
            return Ok (usersToReturn);
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetUser (int id) {
            var user = await _repo.GetUser (id);
            // if(user!= null)
           var userToReturn = _mapper.Map<UserForDetailedDto>(user);
            return Ok (userToReturn);
            //return StatusCode( HttpStatusCode.NotFound);
        }
    }
}