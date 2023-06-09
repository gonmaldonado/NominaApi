using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NominaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NominaApi.Data
{
    public class NominaDbContext: IdentityDbContext<Usuario>
    {
        public NominaDbContext(DbContextOptions<NominaDbContext> options) : base(options)
        {

        }
        public DbSet<Empleado> Empleados { get; set; }
    }
}
