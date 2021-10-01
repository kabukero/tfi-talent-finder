using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalentFinder.BE;
using TalentFinder.BLL.Contracts;
using TalentFinder.BLL.Services;

namespace TalentFinder.Web
{
	public partial class _Default : BasePage
	{
		private IUsuarioBusinessLogic<Usuario> usuarioService;

		protected void Page_Load(object sender, EventArgs e)
		{
			usuarioService = new UsuarioService();
			if(!IsPostBack)
			{
				if(UsuarioLogueado == null)
				{
					Response.Redirect("Login.aspx");
				}
			}
		}

		private void SetPermisos()
		{
			if(UsuarioLogueado == null || !usuarioService.TienePermiso(UsuarioLogueado, Permisos.USUARIO))
			{
				BtnUsuarioGestion.Visible = false;
			}

			if(UsuarioLogueado == null || !usuarioService.TienePermiso(UsuarioLogueado, Permisos.BITACORA))
			{
				BtnBitacoraGestion.Visible = false;
			}
			if(UsuarioLogueado == null || !usuarioService.TienePermiso(UsuarioLogueado, Permisos.BACKUPS))
			{
				BtnBackupGestion.Visible = false;
			}

			if(UsuarioLogueado == null || !usuarioService.TienePermiso(UsuarioLogueado, Permisos.IDIOMA))
			{
				BtnIdiomaGestion.Visible = false;
			}
		}
	}
}
