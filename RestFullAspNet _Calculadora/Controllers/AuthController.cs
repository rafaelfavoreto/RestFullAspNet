using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFullAspNet.Business;
using RestFullAspNet.Data.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFullAspNet.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiversion}")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private IloginBusiness _loginBusiness;

        public AuthController(IloginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin")]

        public IActionResult Signin([FromBody] UserVO user)
        {
            if (user == null) return BadRequest("Ivalid client request");
            var token = _loginBusiness.ValidateCredentials(user);
            if (token == null) return Unauthorized();
            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVo)
        {
            if (tokenVo == null) return BadRequest("Ivalid client request");
            var token = _loginBusiness.ValidateCredentials(tokenVo);
            if (token == null) return BadRequest("Ivalid client request");
            return Ok(token);
        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var userName = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(userName);

            if (!result) return BadRequest("Ivalid client request");
            return NoContent(); 
        }
    }   
}
