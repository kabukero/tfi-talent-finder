using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentFinder.BE
{
    public class PermisoCompuesto : Permiso
    {
        private readonly List<Permiso> permisosHijos = new List<Permiso>();

        public override void AgregarPermisoHijo(Permiso permiso)
        {
            if(!permisosHijos.Any(p => p.Id == permiso.Id))
            {
                permisosHijos.Add(permiso);
            }
        }

        public override List<Permiso> DevolverPerfil()
        {
            return permisosHijos;
        }
    }
}
