using BombaDeAgua_Api.Models;
using BombaDeAgua_Api.Respositories;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;

namespace BombaDeAgua_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlarmaController : Controller
    {
       private IAlarmaCollection db = new AlarmaCollection();

        [HttpGet]
        public async Task<IActionResult> GetAllAlarmas()
        {
            return Ok(await db.GetAllAlarmas());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlarma(int id)
        {
            var alarma = await db.GetAlarma(id);
           
            return Ok(alarma);
        }
        [HttpPost]
        public async Task<IActionResult> AddAlarma([FromBody] AlarmaModel alarma)
        {
            if (alarma == null)
                return BadRequest();
            if (alarma.IdUsuario <= 0)
            {
                ModelState.AddModelError("IdUsuario", "El id del usuario no puede ser menor o igual a 0");
            }

            if (alarma.Hora == TimeSpan.Zero)
            {
                ModelState.AddModelError("Hora", "La hora de la alarma no puede ser 00:00");
            }


            await db.AddAlarma(alarma);
            return Ok(alarma);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlarma(int id, [FromBody] AlarmaModel alarma)
        {
            if (alarma == null)
                return BadRequest("La alarma no puede ser nula.");
            // Asignar el ID de la URL al objeto alarma
            alarma.Id = id;
            var _alarma = await db.GetAlarma(id); // Asegúrate de usar await
            if (alarma.IdUsuario <= 0)
            {
                ModelState.AddModelError("IdUsuario", "El id del usuario no puede ser menor o igual a 0");
            }

            if (alarma.Hora == TimeSpan.Zero)
            {
                ModelState.AddModelError("Hora", "La hora de la alarma no puede ser 00:00");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await db.UpdateAlarma(alarma);
            return Ok(alarma);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAlarma(int id)
        {
            var alarma = await db.GetAlarma(id);
            if (alarma == null)
                return NotFound();

            await db.RemoveAlarma(id);
            return Ok();
        }
    }
}
