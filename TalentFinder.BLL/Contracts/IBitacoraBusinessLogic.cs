using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentFinder.BE;

namespace TalentFinder.BLL.Contracts
{
	public interface IBitacoraBusinessLogic<T> : IGenericBusinessLogic<T> where T : class, new()
	{
		IEnumerable<T> GetAll(BitacoraFilter filter);
	}
}
