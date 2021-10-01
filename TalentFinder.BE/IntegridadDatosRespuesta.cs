using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentFinder.BE
{
	public class IntegridadDatosRespuesta
	{
		public bool HuboError { get; set; }
		public List<string> Mensajes { get; set; }

		public IntegridadDatosRespuesta()
		{
			Mensajes = new List<string>();
		}
	}
}
