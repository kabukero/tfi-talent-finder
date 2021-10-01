using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalentFinder.BE;
using TalentFinder.BLL.Contracts;
using TalentFinder.BLL.Exceptions;
using TalentFinder.BLL.Services;

namespace TalentFinder.Web
{
	public partial class Login : BasePage
	{
		private IUsuarioBusinessLogic<Usuario> serviceUsuario;
		private IBitacoraBusinessLogic<Bitacora> serviceBitacora;
		private IntegridadDatosService integridadDatosService;

		protected void Page_Load(object sender, EventArgs e)
		{
			serviceUsuario = new UsuarioService();
			serviceBitacora = new BitacoraService();
			integridadDatosService = new IntegridadDatosService();

			if(!IsPostBack)
			{
				PanelErrorLogin.Visible = false;
			}
		}

		private void SetError(string msg)
		{
			PanelErrorLogin.Visible = true;
			PanelErrorLogin.GroupingText = msg;
		}

		protected void Ingresar_Click(object sender, EventArgs e)
		{
			var email = TxtEmail.Text;
			var password = TxtPassword.Text;
			var usuario = new Usuario() { Email = email, Password = password };

			try
			{
				PanelErrorLogin.Visible = false;
				var usuarioLogueado = serviceUsuario.Login(usuario);
				Session["UsuarioLogin"] = usuarioLogueado;

				// verificar integridad de datos
				var respuesta = integridadDatosService.VerificarIntegridadDatos();

				if(respuesta.HuboError)
				{
					SetError(respuesta.Mensajes.Aggregate((i, j) => i + "<br>" + j));
					return;
				}

				// log login
				serviceBitacora.Create(new Bitacora()
				{
					Usuario = usuarioLogueado,
					Descripcion = string.Format("El usuario {0} ha ingresado al sistema", usuarioLogueado.NombreCompleto),
					FechaHora = DateTime.Now
				});

				Response.Redirect("~/Default.aspx");
			}
			catch(ServiceException ex)
			{
				SetError("El e-mail o password ingresados son incorrectos");
			}
			catch(Exception ex)
			{
				SetError(ex.Message);
			}
		}
	}
}
