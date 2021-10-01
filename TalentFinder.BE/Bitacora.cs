using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentFinder.BE
{
	public class Bitacora
	{
		public Guid Id { get; set; }
		public DateTime FechaHora { get; set; }
		public Usuario Usuario { get; set; }
		public string Descripcion { get; set; }
	}
}
