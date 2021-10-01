using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalentFinder.BE;

namespace TalentFinder.Web
{
	public partial class SiteMaster : MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var usuario = (Usuario)Session["UsuarioLogin"];
			if(usuario != null)
			{
				LinkUsuario.Text = usuario.NombreCompleto;
			}
		}
	}
}
