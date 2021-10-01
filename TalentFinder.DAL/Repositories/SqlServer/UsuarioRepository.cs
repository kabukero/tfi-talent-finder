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
using TalentFinder.DAL.Repositories.SqlServer.Adapters;
using TalentFinder.DAL.Settings;

namespace TalentFinder.DAL.Repositories.SqlServer
{
	public class UsuarioRepository : IGenericRepository<Usuario>
	{
		DigitoVerificadorRepository digitoVerificadorRepo;

		public UsuarioRepository()
		{
			digitoVerificadorRepo = new DigitoVerificadorRepository();
		}

		public void Create(Usuario obj)
		{
			try
			{
				// crear usuario
				SqlHelper.ExecuteNonQuery("UsuarioCreate", CommandType.StoredProcedure,
						new SqlParameter[] { new SqlParameter("@Id", obj.Id),
											new SqlParameter("@Nombre", obj.Nombre),
											new SqlParameter("@Apellido", obj.Apellido),
											new SqlParameter("@Email", obj.Email),
											new SqlParameter("@Password", obj.Password),
											new SqlParameter("@Habilitado", obj.Habilitado),
											new SqlParameter("@UsuarioId", obj.UsuarioEditor.Id),
											new SqlParameter("@FechaEdicion", obj.FechaEdicion.ToString("yyyy/MM/dd HH:mm")) });

				// actualizar DV del usuario
				digitoVerificadorRepo.ActualizarDV(obj);

				CrearPermisos(obj);
			}
			catch(Exception ex)
			{
				throw new DALException(ex.Message, ex);
			}
		}

		public void Remove(Guid id)
		{
			try
			{
				SqlHelper.ExecuteNonQuery("UsuarioDelete", CommandType.StoredProcedure,
					new SqlParameter[] { new SqlParameter("@Id", id) });
			}
			catch(Exception ex)
			{
				throw new DALException(ex.Message, ex);
			}
		}

		public IEnumerable<Usuario> GetAll()
		{
			try
			{
				List<Usuario> lista = new List<Usuario>();
				using(var dr = SqlHelper.ExecuteReader("UsuarioGetAll", CommandType.StoredProcedure, null))
				{
					while(dr.Read())
					{
						object[] values = new object[dr.FieldCount];
						dr.GetValues(values);
						lista.Add(UsuarioAdapter.Adapt(values));
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

		public Usuario GetOne(Guid id)
		{
			try
			{
				Usuario obj = new Usuario();
				using(var dr = SqlHelper.ExecuteReader("UsuarioGetOne", CommandType.StoredProcedure,
														new SqlParameter[] { new SqlParameter("@Id", id) }))
				{
					while(dr.Read())
					{
						object[] values = new object[dr.FieldCount];
						dr.GetValues(values);
						obj = UsuarioAdapter.Adapt(values);
					}
					dr.Close();
				}
				return obj;
			}
			catch(Exception ex)
			{
				throw new DALException(ex.Message, ex);
			}
		}

		public void Update(Usuario obj)
		{
			try
			{
				SqlHelper.ExecuteNonQuery("UsuarioUpdate", CommandType.StoredProcedure,
						new SqlParameter[] { new SqlParameter("@Id", obj.Id),
											new SqlParameter("@Nombre", obj.Nombre),
											new SqlParameter("@Apellido", obj.Apellido),
											new SqlParameter("@Email", obj.Email),
											new SqlParameter("@Password", obj.Password),
											new SqlParameter("@Habilitado", obj.Habilitado),
											new SqlParameter("@UsuarioId", obj.UsuarioEditor.Id),
											new SqlParameter("@FechaEdicion", obj.FechaEdicion.ToString("yyyy/MM/dd HH:mm")) });

				digitoVerificadorRepo.ActualizarDV(obj);

				EliminarPermisos(obj);

				CrearPermisos(obj);
			}
			catch(Exception ex)
			{
				throw new DALException(ex.Message, ex);
			}
		}

		private void EliminarPermisos(Usuario obj)
		{
			SqlHelper.ExecuteNonQuery("UsuarioPermisoDelete", CommandType.StoredProcedure,
				new SqlParameter[] { new SqlParameter("@UsuarioId", obj.Id) });
		}

		private void CrearPermisos(Usuario obj)
		{
			foreach(Permiso permiso in obj.Permisos)
			{
				SqlHelper.ExecuteNonQuery("UsuarioPermisoCreate", CommandType.StoredProcedure,
					new SqlParameter[] { new SqlParameter("@UsuarioId", obj.Id),
										new SqlParameter("@PermisoId", permiso.Id) });
			}
		}

	}
}
