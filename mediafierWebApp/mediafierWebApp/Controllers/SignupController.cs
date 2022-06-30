using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mediafierWebApp.Models;
using mediafierWebApp.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mediafierWebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        // GET: api/Signup
        private readonly MediafierContext _mediafiercontext;
        public SignupController(MediafierContext a)
        {
            _mediafiercontext = a;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var res = _mediafiercontext.Users.ToList();
            return Ok(res);
        }

        // GET: api/Signup/5
       

        // POST: api/Signup
        [HttpPost]
        public void Post([FromBody] UserRequest value)
        {
            Users obj = new Users();
            obj.UsersUsername = value.UsersUsername;
            obj.UsersPassword = value.UsersPassword;
            obj.UsersCreatedAt = value.UsersCreatedAt;
            _mediafiercontext.Users.Add(obj);
            _mediafiercontext.SaveChanges();
        }

        // PUT: api/Signup/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
