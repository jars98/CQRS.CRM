using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CQRS.Web.API.Domain
{
    public class crm_Propuestas
    {
        [Key]
        public int IdPropuesta { get; set; }
        public string Numero { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string FormaPago { get; set; }
        public int NumCuotas { get; set; }
        public decimal Total { get; set; }
        public bool Aprobada { get; set; }
        public bool Rechazada { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public DateTime? FechaHoraModificacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        [ForeignKey("IdCliente")]
        public virtual crm_Clientes crm_Clientes { get; set; }
    }
}
