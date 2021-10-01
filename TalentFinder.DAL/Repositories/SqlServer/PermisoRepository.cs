using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentFinder.BE;
using TalentFinder.DAL.Contracts;
using TalentFinder.DAL.Exceptions;
using TalentFinder.DAL.Helpers;

namespace TalentFinder.DAL.Repositories.SqlServer
{
	public class PermisoRepository
	{
        private enum Columnas
        {
            ID,
            NOMBRE
        }

        IGenericRepository<Usuario> repoUsuario;
		public PermisoRepository()
		{
			repoUsuario = new UsuarioRepository();
		}

        public IEnumerable<Permiso> ObtenerPorUsuario(int usuarioId)
        {
            try
            {
                List<Permiso> permisos = new List<Permiso>();
                using(var dr = SqlHelper.ExecuteReader("UsuarioPermisoGetAll", CommandType.StoredProcedure,
                                            new SqlParameter[] { new SqlParameter("@PermisoPadreId", usuarioId) }))
                {
                    while(dr.Read())
                    {
                        object[] values = new object[dr.FieldCount];
                        dr.GetValues(values);
                        Permiso permiso = ObtenerPermiso(Guid.Parse(values[(int)Columnas.ID].ToString()), 1);
                        permisos.Add(permiso);
                    }
                    dr.Close();
                }
                return permisos;
            }
            catch(Exception ex)
            {
                throw new DALException(ex.Message, ex);
            }
        }

        private Permiso ObtenerPermiso(Guid permisoId, int profundidad)
        {
            try
            {
                Permiso permiso;
                List<Guid> hijosIds = new List<Guid>();
                if(profundidad < 10)
                {
                    hijosIds = ObtenerPermisosHijos(permisoId);
                }

                if(hijosIds.Any())
                {
                    // Es un permiso compuesto
                    permiso = new PermisoCompuesto();
                    foreach(Guid hijoId in hijosIds)
                    {
                        Permiso permisoHijo = ObtenerPermiso(hijoId, profundidad + 1);
                        permiso.AgregarPermisoHijo(permisoHijo);
                    }
                }
                else
                {
                    // Es un permiso simple
                    permiso = new PermisoSimple();
                }

                CompletarPermiso(permiso, permisoId);

                return permiso;
            }
            catch(Exception ex)
            {
                throw new DALException(ex.Message, ex);
            }
        }

        private List<Guid> ObtenerPermisosHijos(Guid permisoId)
        {
            try
            {
                List<Guid> lista = new List<Guid>();
                using(var dr = SqlHelper.ExecuteReader("PermisoGetAllHijos", CommandType.StoredProcedure,
                                            new SqlParameter[] { new SqlParameter("@PermisoPadreId", permisoId) }))
                {
                    while(dr.Read())
                    {
                        object[] values = new object[dr.FieldCount];
                        dr.GetValues(values);
                        lista.Add(Guid.Parse(values[(int)Columnas.ID].ToString()));
                    }
                    dr.Close();
                }
                return lista;
            }
            catch(Exception ex)
            {
                throw new DALException(ex.Message, ex);
            }
        }

        private void CompletarPermiso(Permiso permiso, Guid permisoId)
        {
            try
            {
                Usuario obj = new Usuario();
                using(var dr = SqlHelper.ExecuteReader("PermisoGetOne", CommandType.StoredProcedure,
                                                        new SqlParameter[] { new SqlParameter("@Id", permisoId) }))
                {
                    while(dr.Read())
                    {
                        object[] values = new object[dr.FieldCount];
                        dr.GetValues(values);
                        permiso.Id = Guid.Parse(values[(int)Columnas.ID].ToString());
                        permiso.Nombre = values[(int)Columnas.NOMBRE].ToString();
                    }
                    dr.Close();
                }
            }
            catch(Exception ex)
            {
                throw new DALException(ex.Message, ex);
            }
        }
    }
}
