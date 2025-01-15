using CQRS.Web.API.Application.ViewModels;
using CQRS.Web.API.Infrastructure.Commands.Propuestas;
using CQRS.Web.API.Infrastructure.Queries;
using CQRS.Web.API.Utilidad;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Web.API.Controllers
{
    [Route("api/Propuestas")]
    [ApiController]
    public class PropuestasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropuestasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("Listado")]
        public async Task<ActionResult> ObtenerPropuestas()
        {
            var rsp = new Response<IEnumerable<PropuestaDTO>>();

            try
            {

                rsp.status = true;
                rsp.value = await _mediator.Send(new GetAllPropuestasQuery());

               
            }
            catch (Exception e)
            {
                rsp.status = false;
                rsp.msg = e.Message;
                return BadRequest(rsp);
            }

            return Ok(rsp);

        }

        [HttpGet]
        [Route("Obtener")]
        public async Task<ActionResult<PropuestaDTO>> ObtenerById(int Id)
        {
            var rsp = new Response<PropuestaDTO>();

            var propuesta = await _mediator.Send(new GetPropuestaByIdQuery(Id));
            if (propuesta == null)
            {
                rsp.status = false;
                rsp.msg = "No se encontro la propuesta";
                return NotFound(rsp);
            }

            try
            {
                rsp.status = true;
                rsp.value = propuesta;


            }
            catch (Exception e)
            {
                rsp.status = false;
                rsp.msg = e.Message;

                return BadRequest(rsp);
            }

            return Ok(rsp);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<ActionResult<PropuestaDTO>> CrearPropuesta([FromBody] CreatePropuestaCommand command)
        {
            var rsp = new Response<PropuestaDTO>();

            var modelo = await _mediator.Send(command);
            try
            {
                rsp.status = true;
                rsp.value = modelo;
            }
            catch (Exception e)
            {
                rsp.status = false;
                rsp.msg = e.Message;
                return BadRequest(rsp);
            }

            return Ok(rsp);
        }

        [HttpPut]
        [Route("Actualizar")]
        public async Task<ActionResult<PropuestaDTO>> ActualizarPropuesta([FromBody] UpdatePropuestaCommand command)
        {
            var rsp = new Response<PropuestaDTO>();

            var propuesta = await _mediator.Send(command);

            if(propuesta == null)
            {
                rsp.status = false;
                rsp.msg = "No se encontro la propuesta";
                return NotFound(rsp);
            }
            try
            {
                rsp.status = true;
                rsp.value = propuesta;


            }
            catch (Exception e)
            {
                rsp.status = false;
                rsp.msg = e.Message;
                return BadRequest(rsp);
            }

            return Ok(rsp);
        }
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ActionResult> EliminarPropuesta(int Id)
        {
            var rsp = new Response<bool>();

            var propuesta = await _mediator.Send(new DeletePropuestasCommand(Id));

            if (!propuesta)
            {
                rsp.status = false;
                rsp.msg = "No se encontro la propuesta";
                return NotFound(rsp);
            }
            try
            {
                rsp.status = true;
                rsp.msg = "Se elimino exitosamente la propuesta";


            }
            catch (Exception e)
            {
                rsp.status = false;
                rsp.msg = e.Message;
                return BadRequest(rsp);
            }

            return Ok(rsp);

        }
    }
}
