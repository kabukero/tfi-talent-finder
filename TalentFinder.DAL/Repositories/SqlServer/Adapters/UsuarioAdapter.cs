using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentFinder.BE;
using TalentFinder.DAL.Contracts;

namespace TalentFinder.DAL.Repositories.SqlServer.Adapters
{
	public class UsuarioAdapter
	{
		public static Usuario Adapt(object[] values)
		{
			return new Usuario()
			{
				Id = Guid.Parse(values[(int)Columnas.ID].ToString()),
				Nombre = values[(int)Columnas.NOMBRE].ToString(),
				Apellido = values[(int)Columnas.APELLIDO].ToString(),
				Email = values[(int)Columnas.EMAIL].ToString(),
				Password = values[(int)Columnas.PASSWORD].ToString(),
				Habilitado = bool.Parse(values[(int)Columnas.HABILITADO].ToString()),
				UsuarioEditor = new Usuario() {
					Id = Guid.Parse(values[(int)Columnas.ID_EDITOR].ToString()),
					Nombre = values[(int)Columnas.NOMBRE_EDITOR].ToString(),
					Apellido = values[(int)Columnas.APELLIDO_EDITOR].ToString(),
					Email = values[(int)Columnas.EMAIL_EDITOR].ToString(),
					Password = values[(int)Columnas.PASSWORD_EDITOR].ToString(),
					Habilitado = bool.Parse(values[(int)Columnas.HABILITADO_EDITOR].ToString()),
					UsuarioEditor = null,
					FechaEdicion = DateTime.Parse(values[(int)Columnas.FECHA_EDICION_EDITOR].ToString())
				},
				FechaEdicion = DateTime.Parse(values[(int)Columnas.FECHA_EDICION].ToString())
			};
		}

		private enum Columnas
		{
			ID,
			NOMBRE,
			APELLIDO,
			EMAIL,
			PASSWORD,
			HABILITADO,
			USUARIO_EDITOR_ID,
			FECHA_EDICION,
			ID_EDITOR,
			NOMBRE_EDITOR,
			APELLIDO_EDITOR,
			EMAIL_EDITOR,
			PASSWORD_EDITOR,
			HABILITADO_EDITOR,
			USUARIO_EDITOR_ID_EDITOR,
			FECHA_EDICION_EDITOR
		}
	}
}
