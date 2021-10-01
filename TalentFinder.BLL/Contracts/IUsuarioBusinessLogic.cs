using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentFinder.BLL.Contracts
{
	public interface IUsuarioBusinessLogic<T> : IGenericBusinessLogic<T> where T : class, new()
	{
		T Login(T obj);

		T GetUsuarioAdministrador();

		bool TienePermiso(T obj, string permiso);
	}
}
