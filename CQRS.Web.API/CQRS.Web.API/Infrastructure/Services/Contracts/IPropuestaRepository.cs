using CQRS.Web.API.Application.ViewModels;

namespace CQRS.Web.API.Infrastructure.Services.Contracts
{
    public interface IPropuestaRepository
    {
        Task<List<PropuestaDTO>> Lista();
        Task<PropuestaDTO> ObtenerById(int Id);
        Task<PropuestaDTO> Crear(PropuestaDTO modelo);
        Task<bool> Editar(PropuestaDTO modelo);
        Task<bool> Eliminar(int Id);
    }
}
