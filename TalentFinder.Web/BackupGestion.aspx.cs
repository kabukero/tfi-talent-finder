using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
	public partial class BackupGestion : BasePage
	{
		private IBackupRestoreBusinessLogic<Backup> backupService;
		private IntegridadDatosService integridadDatosService;
		private IUsuarioBusinessLogic<Usuario> usuarioService;

		protected void Page_Load(object sender, EventArgs e)
		{
			backupService = new BackupRestoreService();
			integridadDatosService = new IntegridadDatosService();
			usuarioService = new UsuarioService();

			if(!IsPostBack)
			{
				if(UsuarioLogueado == null || !usuarioService.TienePermiso(UsuarioLogueado, Permisos.BITACORA))
				{
					Response.Redirect("Login.aspx");
				}
				Enlazar(backupService);
				PanelError.Visible = false;
			}
		}

		private void Enlazar(IBackupRestoreBusinessLogic<Backup> backupService)
		{
			gvListado.DataSource = backupService.GetAll();
			gvListado.DataBind();
		}

		protected void gvListado_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			try
			{
				switch(e.CommandName)
				{
					case "Restore":
						var backup = backupService.GetOne(Guid.Parse(e.CommandArgument.ToString()));
						backupService.Restore(backup);
						break;
				}
			}
			catch(ServiceException ex)
			{
				SetError("El Restore no pudo realizarse");
			}
			catch(Exception ex)
			{
				SetError(ex.Message);
			}
		}

		private void SetError(string msg)
		{
			PanelError.Visible = true;
			PanelError.GroupingText = msg;
		}

		protected void CrearBackup_Click(object sender, EventArgs e)
		{
			try
			{
				// verificar integridad de datos
				var respuesta = integridadDatosService.VerificarIntegridadDatos();

				if(respuesta.HuboError)
				{
					SetError(respuesta.Mensajes.Aggregate((i, j) => i + "<br>" + j));
					return;
				}

				var id = Guid.NewGuid();
				var usuario = (Usuario)Session["UsuarioLogin"];
				var fechaHora = DateTime.Now;
				var fileName = string.Format("backup-{0}-{1}", id, fechaHora.ToString("dd-MM-yyyy-HH-mm"));
				var pathBackup = Path.Combine(ConfigurationManager.AppSettings.Get("BackupPath"), fileName);
				var backup = new Backup() { Id = id, Usuario = usuario, FileName = fileName, PathBackup = pathBackup, FechaHora = fechaHora };
				backupService.Create(backup);
			}
			catch(ServiceException ex)
			{
				SetError("El backup no pudo realizarse");
			}
			catch(Exception ex)
			{
				SetError(ex.Message);
			}
		}
	}
}
