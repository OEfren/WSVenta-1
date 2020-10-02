using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSVenta.Models.Request;
using WSVenta.Models.Response;
using WSVenta.Services;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("login")]
        public IActionResult Auth([FromBody] AuthRequest model)
        {
            Respuesta oRespuesta = new Respuesta();
            var userresponse = _userService.Auth(model);
            if (userresponse == null)
            {
                oRespuesta.Exito = 0;
                oRespuesta.Mensaje = "Usuario o contraseña incorrecta";
                return BadRequest(oRespuesta);
            }

            oRespuesta.Exito = 1;
            oRespuesta.Data = userresponse;
            return Ok(oRespuesta);

        }
    }
}
