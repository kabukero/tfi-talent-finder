using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
	public class BackupRestoreRepository : IBackupRestoreRepository<Backup>
	{
		public void Create(Backup obj)
		{
			try
			{
				SqlHelper.ExecuteNonQuery("BackupCreate", CommandType.StoredProcedure,
						new SqlParameter[] { new SqlParameter("@Id", obj.Id),
											new SqlParameter("@FileName", obj.FileName),
											new SqlParameter("@PathBackup", obj.FileName),
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

		public IEnumerable<Backup> GetAll()
		{
			try
			{
				List<Backup> lista = new List<Backup>();
				using(var dr = SqlHelper.ExecuteReader("BackupGetAll", CommandType.StoredProcedure, null))
				{
					while(dr.Read())
					{
						object[] values = new object[dr.FieldCount];
						dr.GetValues(values);
						lista.Add(BackupAdapter.Adapt(values));
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

		public Backup GetOne(Guid id)
		{
			try
			{
				Backup obj = new Backup();
				using(var dr = SqlHelper.ExecuteReader("BackupGetOne", CommandType.StoredProcedure, null))
				{
					while(dr.Read())
					{
						object[] values = new object[dr.FieldCount];
						dr.GetValues(values);
						obj = BackupAdapter.Adapt(values);
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

		public void Update(Backup obj)
		{
			throw new NotImplementedException();
		}

		public void Restore(Backup obj)
		{
			string databaseName = ConfigurationManager.AppSettings.Get("DatabaseName");
			string backupPath = Path.Combine(ConfigurationManager.AppSettings.Get("BackupPath"), obj.FileName);
			string query = "ALTER DATABASE " + databaseName + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE; " +
							"RESTORE DATABASE " + databaseName + " FROM DISK = @backupPath WITH REPLACE; " +
							"ALTER DATABASE " + databaseName + " SET MULTI_USER;";

			SqlHelper.ExecuteNonQuery(query, CommandType.Text,
											new SqlParameter[] { new SqlParameter("@backupPath", backupPath) });
		}
	}
}
