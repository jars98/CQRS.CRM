using AutoMapper;
using CQRS.Web.API.Application.ViewModels;
using CQRS.Web.API.Domain;
using CQRS.Web.API.Infrastructure.DataAccess.Repositories;
using CQRS.Web.API.Infrastructure.Services.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Web.API.Infrastructure.Services
{
    public class PropuestaRepository : IPropuestaRepository
    {
        private readonly IGenericRepository<crm_Propuestas> _propuestaRepositorio;
        private readonly IMapper _mapper;

        public PropuestaRepository(IGenericRepository<crm_Propuestas> propuestaRepositorio, IMapper mapper)
        {
            _propuestaRepositorio = propuestaRepositorio;
            _mapper = mapper;
        }

        public async Task<List<PropuestaDTO>> Lista()
        {
            try
            {
                var query = await _propuestaRepositorio.Consultar();
                var lista = query.Include(x => x.crm_Clientes).ToList();

                return _mapper.Map<List<PropuestaDTO>>(lista);
            }
            catch (Exception)
            {
                throw;
            }
        }
            
        public async Task<PropuestaDTO> ObtenerById(int Id)
        {
            try
            {
                var propuesta = await _propuestaRepositorio.Obtener(x => x.IdPropuesta == Id, x=> x.crm_Clientes);

                if (propuesta == null)
                    return null;

                return _mapper.Map<PropuestaDTO>(propuesta);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<PropuestaDTO> Crear(PropuestaDTO modelo)
        {
            try
            {
                if (modelo == null)
                    throw new ArgumentNullException(nameof(modelo), "El modelo no puede ser nulo");
                             
                var modeloCrear = _mapper.Map<crm_Propuestas>(modelo);

                modeloCrear.Numero = string.IsNullOrEmpty(modeloCrear.Numero) ? await getNumero(modeloCrear.Fecha.ToString("dd/MM/yyyy")) : modeloCrear.Numero;
                modeloCrear.FechaHoraCreacion = DateTime.Now;
                modeloCrear.FechaHoraModificacion = null;
                modeloCrear.FechaVencimiento = null;
                modeloCrear.FechaAprobacion = null;
               
                var propuestaCrear = await _propuestaRepositorio.Crear(modeloCrear);

                if (propuestaCrear.IdPropuesta == 0)
                    throw new TaskCanceledException("No se pudo crear la propuesta");

                var query = await _propuestaRepositorio.Consultar(x => x.IdPropuesta == propuestaCrear.IdPropuesta);
                propuestaCrear = query.Include(rol => rol.crm_Clientes).First();

                return _mapper.Map<PropuestaDTO>(propuestaCrear);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> Editar(PropuestaDTO modelo)
        {
            try
            {
                var propuesta = await _propuestaRepositorio.Obtener(x => x.IdPropuesta == modelo.IdPropuesta);

                if (propuesta == null)
                    throw new TaskCanceledException("No se pudo encontrar la propuesta");

                _mapper.Map(modelo, propuesta);

                propuesta.FechaHoraModificacion = DateTime.Now;
                propuesta.FechaVencimiento = modelo.FechaVencimiento > DateTime.Now ? modelo.FechaVencimiento : null;
                propuesta.FechaAprobacion = propuesta.Aprobada ? DateTime.Now : null;

                bool resp = await _propuestaRepositorio.Editar(propuesta);

                if (!resp)
                    throw new TaskCanceledException("No se pudo editar la propuesta");

                return resp;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> Eliminar(int Id)
        {
            try
            {
                var propuesta = await _propuestaRepositorio.Obtener(x => x.IdPropuesta == Id);

                if (propuesta == null)
                    throw new TaskCanceledException("No se pudo encontrar la propuesta a eliminar");

                bool resp = await _propuestaRepositorio.Eliminar(propuesta);

                if (!resp)
                    throw new TaskCanceledException("No se pudo eliminar la propuesta");

                return resp;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<string> getNumero(string FechaPropuesta)
        {
            DateTime oDate = DateTime.ParseExact(FechaPropuesta, "dd/MM/yyyy", null);

            var countPropuesta = 0;
            var mes = oDate.Month;
            var anio = oDate.Year;

            try
            {
                var query = await _propuestaRepositorio.Consultar(x => x.Fecha.Year == anio);

                countPropuesta = query.Count() + 1;
            }
            catch (Exception e)
            {
                countPropuesta = 0 + 1;
            }

            var countContratoString = countPropuesta.ToString().PadLeft(5, '0');
            var numero = anio + "-" + countContratoString;

            return numero;
        }
    }
}
