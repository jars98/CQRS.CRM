using System.ComponentModel.DataAnnotations;

namespace CQRS.Web.API.Domain
{
    public class crm_Clientes
    {
        [Key]
        public int IdCliente { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string? Correo { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public DateTime? FechaHoraModificacion { get; set; }
    }
}
