using DBSqlLite;
using Microsoft.AspNetCore.Mvc;
using Modelo;
using System.Data.Common;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendTaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public static List<Usuario> usuarios = new List<Usuario>
        {
            new Usuario(1,"Sergi","sergi@gmail.com","1234"),
            new Usuario(2,"Juli","juli@gmail.com","1234"),
            new Usuario(3,"Ricard","ricard@gmail.com","1234"),
            new Usuario(4,"Oriol","oriol@gmail.com","1234"),
        };

        // GET: api/<UsuariosController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Usuario> users = DB_connection.GetUsuarios();

            return Ok(users);
        }

        //POST para validar el login
        [HttpPost("login")]
        public IActionResult PostLogin([FromBody] JsonDocument value)
        {
            /*
             JsonDocument value
                {
	                correo: “”
	                contraseña: “”
                }
             */
            try
            {
                string encryptedPassword = Usuario.EncriptarContraseña(value.RootElement.GetProperty("contraseña").GetString());
                string correo = value.RootElement.GetProperty("correo").GetString();

                var user = DB_connection.CheckUser(correo, encryptedPassword);
                
                if (user != null)
                {
                    return Ok(new
                    {
                        user = new
                        {
                            id = user.Id,
                            nombre = user.Nombre
                        },
                        value = true
                    });
                }
                else
                    return NotFound(new
                    {
                        value = false
                    });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Usuario user = DB_connection.GetUserById(id);
            if (user != null)
            {
                return Ok(new
                {
                    id = user.Id,
                    nombre = user.Nombre,
                    correo = user.Correo
                });
            }
            else
                return NotFound();
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public IActionResult Post([FromBody] JsonDocument value)
        {
            try
            {
                Usuario user = new Usuario(
                    usuarios.Count + 1,
                    value.RootElement.GetProperty("nombre").GetString(),
                    value.RootElement.GetProperty("correo").GetString(),
                    value.RootElement.GetProperty("contraseña").GetString());

                if(DB_connection.AddUser(user))
                    return Ok();
                else
                    throw new Exception("Error al añadir el usuario");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //TODO: Implementar PUT y DELETE

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] JsonDocument value)
        {
            try
            {
                //Creo un nuevo usuario con los datos que vienen en el json para validar los datos
                Usuario newUserData = new Usuario(
                    id,
                    value.RootElement.GetProperty("nombre").GetString(),
                    value.RootElement.GetProperty("correo").GetString(),
                    value.RootElement.GetProperty("contraseña").GetString());

                //Si el usuario existe, actualizo los datos
                if (usuarios.Exists(u => u.Id == id))
                {
                    Usuario user = usuarios.Find(u => u.Id == id);

                    user.Nombre = newUserData.Nombre;
                    user.Correo = newUserData.Correo;
                    user.Contraseña = newUserData.Contraseña;
                }
                else
                    return NotFound();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (usuarios.Exists(u => u.Id == id))
            {
                Usuario user = usuarios.Find(u => u.Id == id);
                usuarios.Remove(user);
                return Ok();
            }
            else
                return NotFound();
        }
    }
}
