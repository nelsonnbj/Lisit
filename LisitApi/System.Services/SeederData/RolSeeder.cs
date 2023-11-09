using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTheLastBugSpa.Data;
using SystemTheLastBugSpa.Data.Entity;

namespace System.Infrastructure.SeederData
{
    public static class RolSeeder
    {
        public static void SeeadData(AplicationDataContext dbContext)
        {
            if (dbContext.Roles.Any())
            {
                return;
            }

            dbContext.Roles.AddRange(
                new Roles
                {
                    Id = Guid.Parse("d93926e6-25c0-4344-acbb-c9f36c44cfb1"),
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Administrador del sistema"
                },
                new Roles
                {
                    Name = "Aplicante",
                    NormalizedName = "Aplicante",
                    Description = "Estudiante que quiere poder ser becado del mescyt"
                },
                new Roles
                {
                    Name = "Estudiantes",
                    NormalizedName = "Estudiantes",
                    Description = "Estudiantes que ha sido becado por el mescyt"
                },
                new Roles
                {
                    Name = "Empleado",
                    NormalizedName = "Empleado",
                    Description = "Empleado del mescyt"
                },
                new Roles
                {
                    Name = "Soporte Academico y Administrativo",
                    NormalizedName = "Soporte Academico y Administrativo",
                    Description = "Empleado del mescyt"
                },
                new Roles
                {
                    Name = "Auxiliar Administrativo",
                    NormalizedName = "Auxiliar Administrativo",
                    Description = "Empleado del mescyt"
                },
                new Roles
                {
                    Name = "Encargado de Almacen",
                    NormalizedName = "Encargado de Almacen",
                    Description = "Empleado del mescyt"
                },
                new Roles
                {
                    Name = "Encargado Administrativo",
                    NormalizedName = "Encargado Administrativo",
                    Description = "Empleado del mescyt"
                },
                new Roles
                {
                    Name = "Encargado de Mantenimiento",
                    NormalizedName = "Encargado de Mantenimiento",
                    Description = "Empleado del mescyt"
                },
                new Roles
                {
                    Name = "Secretaria del director",
                    NormalizedName = "Secretaria del director",
                    Description = "Empleado del mescyt"
                },
                new Roles
                {
                    Name = "Técnico",
                    NormalizedName = "Técnico",
                    Description = "Empleado del mescyt"
                },
                new Roles
                {
                    Name = "Profesor",
                    NormalizedName = "Profesor",
                    Description = "Empleado del mescyt"
                },
                new Roles
                {
                    Name = "Coordinador",
                    NormalizedName = "Coordinador",
                    Description = "Empleado del mescyt"
                },
                new Roles
                {
                    Name = "Supervisor",
                    NormalizedName = "Supervisor",
                    Description = "Empleado del mescyt"
                }
                ) ;

            dbContext.SaveChanges();
        }
    }
}
