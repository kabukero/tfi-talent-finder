using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentFinder.BE;
using TalentFinder.BLL.Contracts;
using TalentFinder.DAL.Contracts;
using TalentFinder.DAL.Repositories.SqlServer;

namespace TalentFinder.BLL.Services
{
	public class BitacoraService : IBitacoraBusinessLogic<Bitacora>
	{
		IGenericRepository<Bitacora> repo;

		public BitacoraService()
		{
			repo = new BitacoraRepository();
		}

		public void Create(Bitacora obj)
		{
			obj.Id = Guid.NewGuid();
			repo.Create(obj);
		}

		public IEnumerable<Bitacora> GetAll(BitacoraFilter filter)
		{
			var lista = repo.GetAll();

			if(filter.Usuario != null)
			{
				lista = lista.Where(x => x.Usuario.Id == filter.Usuario.Id);
			}

			if(filter.FechaDesde.HasValue)
			{
				lista = lista.Where(x => x.FechaHora >= filter.FechaDesde);
			}

			if(filter.FechaHasta.HasValue)
			{
				lista = lista.Where(x => x.FechaHora <= filter.FechaHasta);
			}

			return lista.OrderBy(x => x.Usuario.Apellido).ThenByDescending(x => x.FechaHora);
		}

		public IEnumerable<Bitacora> GetAll()
		{
			throw new NotImplementedException();
		}

		public Bitacora GetOne(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Remove(Guid id)
		{
			throw new NotImplementedException();
		}

		public void Update(Bitacora obj)
		{
			throw new NotImplementedException();
		}
	}
}
