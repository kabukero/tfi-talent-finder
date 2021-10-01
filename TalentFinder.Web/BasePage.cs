using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using TalentFinder.BE;

namespace TalentFinder.Web
{
	public class BasePage : Page
	{
        public Usuario UsuarioLogueado
        {
            get
            {
                return Session["UsuarioLogueado"] != null ? (Usuario)Session["UsuarioLogueado"] : null;
            }
            set
            {
                Session["UsuarioLogueado"] = value;
            }
        }
    }
}
