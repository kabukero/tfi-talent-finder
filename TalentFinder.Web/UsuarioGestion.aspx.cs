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
	public partial class UsuarioGestion : BasePage
	{
		private IUsuarioBusinessLogic<Usuario> usuarioService;

		protected void Page_Load(object sender, EventArgs e)
		{
			usuarioService = new UsuarioService();
			if(!IsPostBack)
			{
				if(UsuarioLogueado == null || !usuarioService.TienePermiso(UsuarioLogueado, Permisos.USUARIO))
				{
					Response.Redirect("Login.aspx");
				}
				ChkHabilitado.Checked = true;
				Enlazar();
			}
		}

		private void Enlazar()
		{
			gvListado.DataSource = usuarioService.GetAll();
			gvListado.DataBind();
		}

		protected void gvListado_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			switch(e.CommandName)
			{
				case "Eliminar":
					usuarioService.Remove(Guid.Parse(e.CommandArgument.ToString()));
					Enlazar();
					break;
				case "Seleccionar":
					var usuario = usuarioService.GetOne(Guid.Parse(e.CommandArgument.ToString()));
					HidId.Value = usuario.Id.ToString();
					TxtNombre.Text = usuario.Nombre;
					TxtApellido.Text = usuario.Apellido;
					TxtEmail.Text = usuario.Email;
					TxtPassword.Text = usuario.Password;
					ChkHabilitado.Checked = usuario.Habilitado;
					break;
			}
		}

		private void LimpiarForm()
		{
			HidId.Value = string.Empty;
			TxtNombre.Text = string.Empty;
			TxtApellido.Text = string.Empty;
			TxtEmail.Text = string.Empty;
			TxtPassword.Text = string.Empty;
			ChkHabilitado.Checked = true;
		}

		protected void Guardar_Click(object sender, EventArgs e)
		{
			var esAlta = string.IsNullOrEmpty(HidId.Value);
			var id = esAlta ? Guid.NewGuid() : Guid.Parse(HidId.Value);
			var nombre = TxtNombre.Text;
			var apellido = TxtApellido.Text;
			var email = TxtEmail.Text;
			var password = TxtPassword.Text;
			var habilitado = ChkHabilitado.Checked;
			var usuario = new Usuario() { Id = id, Nombre = nombre, Apellido = apellido, Email = email, Password = password, Habilitado = habilitado };

			if(esAlta)
				usuarioService.Create(usuario);
			else
				usuarioService.Update(usuario);

			Enlazar();
			LimpiarForm();
		}

		protected void Limpiar_Click(object sender, EventArgs e)
		{
			LimpiarForm();
		}
	}
}
