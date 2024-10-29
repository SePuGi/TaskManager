using DBSqlLite;
using Microsoft.AspNetCore.Mvc;
using Modelo;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        public static List<Tarea> tareas = new List<Tarea>{
            new Tarea(1,1,"Descripción tarea 1", DateTime.Now, DateTime.Now.AddDays(5)),
            new Tarea(2,2,"Descripción tarea 2", DateTime.Now, DateTime.Now.AddDays(2)),
            new Tarea(3,1,"Descripción tarea 3", DateTime.Now, DateTime.Now.AddDays(3)),
            new Tarea(4,2,"Descripción tarea 4", DateTime.Now, DateTime.Now.AddDays(7)),
            new Tarea(5,1,"Descripción tarea 5", DateTime.Now, DateTime.Now.AddDays(1)),
            new Tarea(6,3,"Descripción tarea 6", DateTime.Now, DateTime.Now.AddDays(10)),
            new Tarea(7,2,"Descripción tarea 7", DateTime.Now, DateTime.Now.AddDays(6)),
            new Tarea(8,3,"Descripción tarea 8", DateTime.Now, DateTime.Now.AddDays(4)),
            new Tarea(9,4,"Descripción tarea 9", DateTime.Now, DateTime.Now.AddDays(1)),
            new Tarea(10,4,"Descripción tarea 10", DateTime.Now, DateTime.Now.AddDays(5)),
            new Tarea(11,4,"Descripción tarea 11", DateTime.Now, DateTime.Now.AddDays(5))
        };

        // GET: api/<TareasController>
        [HttpGet]
        public IActionResult Get()
        {
            List<Tarea> taskList = DB_connection.GetTask();

            if(taskList == null)
                return NotFound();
            else
                return Ok(taskList);
        }

        // GET api/<TareasController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            List<Tarea> taskList = DB_connection.GetTaskByUserId(id);
            if (taskList != null)
                return Ok(taskList);
            
            else
                return NotFound();
            
        }

        // POST api/<TareasController>
        [HttpPost]
        public IActionResult Post([FromBody] JsonDocument value)
        {
            //crear tarea
            try
            {
                Tarea tarea = new Tarea(
                    tareas.Count + 1, 
                    value.RootElement.GetProperty("idUsuario").GetInt32(), 
                    value.RootElement.GetProperty("descripcion").GetString(),
                    value.RootElement.GetProperty("dataInicio").GetDateTime(),
                    value.RootElement.GetProperty("dataFin").GetDateTime());

                if (DB_connection.AddTask(tarea))
                    return Ok();

                else
                    return NotFound();


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // PUT api/<TareasController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] JsonDocument value)
        {
            try
            {
                Tarea tareaEdited = new Tarea(
                    1,
                    value.RootElement.GetProperty("idUsuario").GetInt32(),
                    value.RootElement.GetProperty("descripcion").GetString(),
                    value.RootElement.GetProperty("dataInicio").GetDateTime(),
                    value.RootElement.GetProperty("dataFin").GetDateTime());

                if(tareas.Exists(t => t.Id == id))
                {
                    Tarea tarea = tareas.Find(t => t.Id == id);
                    tarea.IdUsuario = tareaEdited.IdUsuario;
                    tarea.Descripcion = tareaEdited.Descripcion;
                    tarea.DataInicio = tareaEdited.DataInicio;
                    tarea.DataFin = tareaEdited.DataFin;

                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<TareasController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (tareas.Remove(tareas.Find(t => t.Id == id)))
                return Ok();
            return NotFound();
        }
    }
}
