using BombaDeAgua_Api.Models;
using BombaDeAgua_Api.Respositories;
using Microsoft.AspNetCore.Mvc;

namespace BombaDeAgua_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController : Controller
    {
        private IHistorialCollection db = new HistorialCollection();

        [HttpGet]
        public async Task<IActionResult> GetAllHistorial()
        {
            return Ok(await db.GetAllHistorial());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistorial(int id)
        {
            var historial = await db.GetHistorial(id);
            if (historial == null)
                return NotFound();
            return Ok(historial);
        }
        [HttpPost]
        public async Task<IActionResult> AddHistorial([FromBody] HistorialModel historial)
        {
            
            if (historial == null)
                return BadRequest();
            if (historial.IdUsuario == null)
            {
                ModelState.AddModelError("IdUsuario", "El id del usuario no puede ser vacio");
            }
            else if (historial.Porcentaje == null)
            {
                ModelState.AddModelError("Fecha", "La fecha del historial no puede ser vacia");
            }
            
            await db.AddHistorial(historial);
            return Ok(historial);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHistorial(int id, [FromBody] HistorialModel historial)
        {
            if (historial == null)
                return BadRequest("El historial no puede ser nulo.");
            // Asignar el ID de la URL al objeto historial
            historial.Id = id;
            var _historial = await db.GetHistorial(id); // Asegúrate de usar await

            if (historial.IdUsuario == null)
                ModelState.AddModelError("IdUsuario", "El id del usuario no puede ser vacio");
            if (historial.Porcentaje == null)
                ModelState.AddModelError("Porcentaje", "El porcentaje del historial no puede ser vacio");
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await db.UpdateHistorial(historial);
            return Ok(historial);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveHistorial(int id)
        {
            var historial =  db.GetHistorial(id);
            if (historial == null)
                return NotFound();
            await db.RemoveHistorial(id);
            return Ok();
        }
    }
}
