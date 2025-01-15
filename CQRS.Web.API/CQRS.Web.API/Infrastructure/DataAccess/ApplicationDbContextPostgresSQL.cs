using CQRS.Web.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Web.API.Infrastructure.DataAccess
{
    public class ApplicationDbContextPostgresSQL : DbContext
    {
        public ApplicationDbContextPostgresSQL(DbContextOptions<ApplicationDbContextPostgresSQL> options) : base(options)
        {
        }

        public DbSet<crm_Clientes> crm_Clientes { get; set; }
        public DbSet<crm_Propuestas> crm_Propuestas { get; set; }
    }
}
