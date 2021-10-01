using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentFinder.BE;
using TalentFinder.DAL.Contracts;

namespace TalentFinder.DAL.Repositories.SqlServer.Adapters
{
	public class BackupAdapter
	{
		public static Backup Adapt(object[] values)
		{
			IGenericRepository<Usuario> repo = new UsuarioRepository();

			return new Backup()
			{
				Id = Guid.Parse(values[(int)Columnas.ID].ToString()),
				FileName = values[(int)Columnas.FILENAME].ToString(),
				Usuario = repo.GetOne(Guid.Parse(values[(int)Columnas.USUARIO_ID].ToString())),
				FechaHora = DateTime.Parse(values[(int)Columnas.FECHA_HORA].ToString())
			};
		}

		private enum Columnas
		{
			ID,
			FILENAME,
			USUARIO_ID,
			FECHA_HORA
		}
	}
}
