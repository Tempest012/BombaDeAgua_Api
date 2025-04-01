using BombaDeAgua_Api.Models;
using BombaDeAgua_Api.Respositories;
using Microsoft.AspNetCore.Mvc;

namespace BombaDeAgua_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private IUsuariosCollection db = new UsuariosCollection();
        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            return Ok(await db.GetAllUsuarios());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await db.GetUsuario(id);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }
        [HttpGet("by-email/{correo}")]
        public async Task<IActionResult> GetUsuarioPorCorreo(string correo)
        {
            var usuario = await db.GetUsuario(correo);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> AddUsuario([FromBody] UsuarioModel usuario)
        {
            if (usuario == null)
                return BadRequest();
            if(usuario.Nombre == string.Empty)
            {
                ModelState.AddModelError("Nombre", "El nombre del usuario no puede ser vacio");
            }
            else if(usuario.Correo == string.Empty)
            {
                ModelState.AddModelError("Correo", "El correo del usuario no puede ser vacio");
            }
            else if (usuario.Contrasena == string.Empty)
            {
                ModelState.AddModelError("Contraseña", "La contraseña del usuario no puede ser vacio");
            }
            await db.AddUsuario(usuario);
            return Ok(usuario);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioModel usuario)
        {
            if (usuario == null)
                return BadRequest("El usuario no puede ser nulo.");

            // Asignar el ID de la URL al objeto usuario
            usuario.Id = id;

            var _usuario = await db.GetUsuario(id); // Asegúrate de usar await
         

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                ModelState.AddModelError("Nombre", "El nombre del usuario no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(usuario.Correo))
                ModelState.AddModelError("Correo", "El correo del usuario no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(usuario.Contrasena))
                ModelState.AddModelError("Contraseña", "La contraseña del usuario no puede estar vacía.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await db.UpdateUsuario(usuario);

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveUsuario(int id)
        {
            var usuario = db.GetUsuario(id);
            if (usuario == null)
                return NotFound();
            await db.RemoveUsuario(id);
            return Ok();
        }
    }
}
