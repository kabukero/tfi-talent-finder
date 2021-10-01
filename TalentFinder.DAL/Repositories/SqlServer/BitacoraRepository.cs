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

namespace TalentFinder.DAL.Repositories.SqlServer
{
	public class BitacoraRepository : IGenericRepository<Bitacora>
	{
		public void Create(Bitacora obj)
		{
			try
			{
				SqlHelper.ExecuteNonQuery("BitacoraCreate", CommandType.StoredProcedure,
						new SqlParameter[] { new SqlParameter("@Id", obj.Id),
											new SqlParameter("@Descripcion", obj.Descripcion),
											new SqlParameter("@UsuarioId", obj.Usuario.Id),
											new SqlParameter("@FechaHora", obj.FechaHora.ToString("yyyy/MM/dd HH:mm")) });
			}
			catch(Exception ex)
			{
				throw new DALException(ex.Message, ex);
			}
		}

		public void Remove(Guid id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Bitacora> GetAll()
		{
			try
			{
				List<Bitacora> lista = new List<Bitacora>();
				using(var dr = SqlHelper.ExecuteReader("BitacoraGetAll", CommandType.StoredProcedure, null))
				{
					while(dr.Read())
					{
						object[] values = new object[dr.FieldCount];
						dr.GetValues(values);
						lista.Add(BitacoraAdapter.Adapt(values));
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

		public Bitacora GetOne(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Update(Bitacora obj)
		{
			throw new NotImplementedException();
		}
	}
}
