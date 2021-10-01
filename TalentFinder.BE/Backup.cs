using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentFinder.BE
{
	public class Backup
	{
		public Guid Id { get; set; }
		public Usuario Usuario { get; set; }
		public string FileName { get; set; }
		public string PathBackup { get; set; }
		public DateTime FechaHora { get; set; }
	}
}
