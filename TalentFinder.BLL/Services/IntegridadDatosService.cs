using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentFinder.BE;
using TalentFinder.DAL.Repositories.SqlServer;

namespace TalentFinder.BLL.Services
{
	public class IntegridadDatosService
	{
		DigitoVerificadorRepository repo;

		public IntegridadDatosService()
		{
			repo = new DigitoVerificadorRepository();
		}

		public IntegridadDatosRespuesta VerificarIntegridadDatos()
		{
			return repo.VerificarIntegridad();
		}
	}
}
