using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentFinder.BE;
using TalentFinder.DAL.Contracts;

namespace TalentFinder.DAL.Repositories.SqlServer.Adapters
{
	public class DigitoVerificadorAdapter
	{
		public static DigitoVerificador Adapt(object[] values)
		{
			return new DigitoVerificador()
			{
				Id = values[(int)Columnas.ID].ToString(),
				DDV = values[(int)Columnas.DVV].ToString()
			};
		}

		private enum Columnas
		{
			ID,
			DVV
		}
	}
}
