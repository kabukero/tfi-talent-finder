using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TalentFinder.BE;
using TalentFinder.DAL.Contracts;
using TalentFinder.DAL.Exceptions;
using TalentFinder.DAL.Helpers;
using TalentFinder.DAL.Repositories.SqlServer.Adapters;

namespace TalentFinder.DAL.Repositories.SqlServer
{
	public class DigitoVerificadorRepository
	{
        private string DV_KEY;
        private IGenericRepository<Usuario> repoUsuario;

        public DigitoVerificadorRepository()
		{
            repoUsuario = new UsuarioRepository();
            DV_KEY = ConfigurationManager.AppSettings.Get("DV_KEY");
        }

        public void ActualizarDV(Usuario usuario)
        {
            // Primero el DVH
            string dvh = ObtenerDVH(usuario);

            SqlHelper.ExecuteNonQuery("UsuarioUpdateDVH", CommandType.StoredProcedure,
                    new SqlParameter[] { new SqlParameter("@Id", usuario.Id),
                                            new SqlParameter("@DVH", dvh) });

            // Ahora el DVV
            RecalcularDVV("Usuario");
        }

        public bool VerificarUsuario(Guid idUsuario)
        {
            var usuario = repoUsuario.GetOne(idUsuario);
            string dvhBD = usuario.DVH;
            string dvhCalculado = ObtenerDVH(usuario);
            return dvhBD == dvhCalculado;
        }

        public IntegridadDatosRespuesta VerificarIntegridad()
        {
            IntegridadDatosRespuesta integridadDatosRespuesta = new IntegridadDatosRespuesta();
            StringBuilder sbDvhs = new StringBuilder();

            #region Usuario
            var usuarios = repoUsuario.GetAll();
            if(usuarios != null)
            {
                // validar DVH
                foreach(var usuario in usuarios)
                {
                    string dvhBD = usuario.DVH;
                    string dvhCalculado = ObtenerDVH(usuario);
                    if(dvhBD != dvhCalculado)
                    {
                        integridadDatosRespuesta.Mensajes.Add(string.Format("El usuario con el ID {0} fue modificado externamente.", usuario.Id));
                        integridadDatosRespuesta.HuboError = true;
                    }
                    sbDvhs.Append(dvhBD.ToString());
                }

                // validar DVV
                string dvvCalculado = CalcularDV(sbDvhs.ToString());
                DigitoVerificador digitoVerificador = GetOne("Usuario");

                if(dvvCalculado != digitoVerificador.DDV)
                {
                    integridadDatosRespuesta.Mensajes.Add("Un registro de la tabla Usuario fue eliminado externamente.");
                    integridadDatosRespuesta.HuboError = true;
                }
            }
            #endregion

            return integridadDatosRespuesta;
        }

        private string ObtenerDVH(Usuario usuario)
        {
            string registro = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                usuario.Id,
                usuario.Nombre,
                usuario.Apellido,
                usuario.Email,
                usuario.Password,
                usuario.Habilitado,
                usuario.UsuarioEditor.Id,
                usuario.FechaEdicion);
            return CalcularDV(registro);
        }

        private string CalcularDV(string registro)
        {
            byte[] data = Encoding.ASCII.GetBytes(registro + DV_KEY);
            data = new SHA256Managed().ComputeHash(data);
            return Convert.ToBase64String(data);
        }

        private void RecalcularDVV(string tabla)
        {
            var usuarios = repoUsuario.GetAll();

            StringBuilder sb = new StringBuilder();
            foreach(var u in usuarios)
			{
                sb.Append(u.DVH);
			}

            string dvv = CalcularDV(sb.ToString());

            SqlHelper.ExecuteNonQuery("DigitoVerificadorUpdate", CommandType.StoredProcedure,
                                            new SqlParameter[] { new SqlParameter("@Id", tabla),
                                            new SqlParameter("@DVV", dvv) });
        }

        public DigitoVerificador GetOne(string id)
        {
            try
            {
                DigitoVerificador obj = new DigitoVerificador();
                using(var dr = SqlHelper.ExecuteReader("DigitoVerificadorGetOne", CommandType.StoredProcedure,
                                                        new SqlParameter[] { new SqlParameter("@Id", id) }))
                {
                    while(dr.Read())
                    {
                        object[] values = new object[dr.FieldCount];
                        dr.GetValues(values);
                        obj = DigitoVerificadorAdapter.Adapt(values);
                    }
                    dr.Close();
                }
                return obj;
            }
            catch(Exception ex)
            {
                throw new DALException(ex.Message, ex);
            }
        }
    }
}
