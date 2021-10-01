using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentFinder.BE;
using TalentFinder.BLL.Contracts;
using TalentFinder.DAL.Contracts;
using TalentFinder.DAL.Repositories.SqlServer;

namespace TalentFinder.BLL.Services
{
	public class BackupRestoreService : IBackupRestoreBusinessLogic<Backup>
	{
		IBackupRestoreRepository<Backup> repo;

		public BackupRestoreService()
		{
			repo = new BackupRestoreRepository();
		}

		private void CrearCarpetaBackup(string path)
		{
			if(!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}

		public void Create(Backup obj)
		{
			CrearCarpetaBackup(ConfigurationManager.AppSettings.Get("BackupPath"));
			repo.Create(obj);
		}

		public IEnumerable<Backup> GetAll()
		{
			return repo.GetAll();
		}

		public Backup GetOne(Guid id)
		{
			return repo.GetOne(id);
		}

		public void Remove(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Restore(Backup obj)
		{
			repo.Restore(obj);
		}

		public void Update(Backup obj)
		{
			throw new NotImplementedException();
		}
	}
}
