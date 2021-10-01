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
	public partial class BitacoraGestion : BasePage
	{
		private IBitacoraBusinessLogic<Bitacora> bitacoraService;
		private IUsuarioBusinessLogic<Usuario> usuarioService;

		protected void Page_Load(object sender, EventArgs e)
		{
			bitacoraService = new BitacoraService();
			usuarioService = new UsuarioService();

			if(!IsPostBack)
			{
				if(UsuarioLogueado == null || !usuarioService.TienePermiso(UsuarioLogueado, Permisos.BITACORA))
				{
					Response.Redirect("Login.aspx");
				}
				EnlazarDDL(usuarioService);
			}
		}

		private void EnlazarDDL(IUsuarioBusinessLogic<Usuario> usuarioService)
		{
			DDLUsuario.DataValueField = "Id";
			DDLUsuario.DataTextField = "NombreCompleto";
			DDLUsuario.DataSource = usuarioService.GetAll().Where(x => x.Habilitado);
			DDLUsuario.DataBind();
		}

		private void Enlazar(IEnumerable<Bitacora> lista)
		{
			gvListado.DataSource = lista;
			gvListado.DataBind();
		}

		protected void Buscar_Click(object sender, EventArgs e)
		{
			var usuario = new Usuario() { Id = Guid.Parse(DDLUsuario.SelectedValue) };
			var fechaDesde = CalFechaDesde.SelectedDate;
			var fechaHasta = CalFechaHasta.SelectedDate;
			var bitacoraFilter = new BitacoraFilter() { Usuario = usuario, FechaDesde = fechaDesde, FechaHasta = fechaHasta };
			Enlazar(bitacoraService.GetAll(bitacoraFilter));
		}
	}
}
