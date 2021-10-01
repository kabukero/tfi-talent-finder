using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentFinder.BE
{
    public abstract class Permiso
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }

        public abstract void AgregarPermisoHijo(Permiso permiso);

        public abstract List<Permiso> DevolverPerfil();

        public class Comparador : IEqualityComparer<Permiso>
        {
            public bool Equals(Permiso x, Permiso y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(Permiso obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
