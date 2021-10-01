using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentFinder.BE
{
	public class Usuario
	{
		public Guid Id { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public bool Habilitado { get; set; }
		public Usuario UsuarioEditor { get; set; }
		public DateTime FechaEdicion { get; set; }
		public string NombreCompleto
		{
			get => string.Format("{0} {1}", Nombre, Apellido);
		}
		public string Enable
		{
			get => Habilitado ? "SI" : "NO";
		}
		
		public string DVH { get; set; }
		
		public List<Permiso> Permisos { get; set; } = new List<Permiso>();
	}
}
