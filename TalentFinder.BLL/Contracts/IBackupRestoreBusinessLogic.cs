using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentFinder.BE;

namespace TalentFinder.BLL.Contracts
{
	public interface IBackupRestoreBusinessLogic<T> : IGenericBusinessLogic<T> where T : class, new()
	{
		void Restore(Backup obj);
	}
}
