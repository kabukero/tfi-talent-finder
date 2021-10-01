using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentFinder.BE
{
    public class PermisoSimple : Permiso
    {
        public override void AgregarPermisoHijo(Permiso permiso)
        {
            // Nunca debe llamarse a esto
            throw new NotImplementedException();
        }

        public override List<Permiso> DevolverPerfil()
        {
            return new List<Permiso>();
        }
    }
}
