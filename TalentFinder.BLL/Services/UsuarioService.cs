using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentFinder.BE;
using TalentFinder.BLL.Contracts;
using TalentFinder.BLL.Exceptions;
using TalentFinder.DAL.Contracts;
using TalentFinder.DAL.Exceptions;
using TalentFinder.DAL.Repositories.SqlServer;
using TalentFinder.Service.Seguridad;

namespace TalentFinder.BLL.Services
{
	public class UsuarioService : IUsuarioBusinessLogic<Usuario>
	{
		private IGenericRepository<Usuario> repo;

		public UsuarioService()
		{
			repo = new UsuarioRepository();
		}

		public void Create(Usuario obj)
		{
			try
			{
				obj.Id = Guid.NewGuid();
				obj.UsuarioEditor = new Usuario() { Id = obj.Id, FechaEdicion = DateTime.Now };
				repo.Create(obj);
			}
			catch(DALException ex)
			{
				throw new ServiceException(ex.Message, ex);
			}
		}

		public IEnumerable<Usuario> GetAll()
		{
			try
			{
				return repo.GetAll();
			}
			catch(DALException ex)
			{
				throw new ServiceException(ex.Message, ex);
			}
		}

		public Usuario GetOne(Guid id)
		{
			try
			{
				return repo.GetOne(id);
			}
			catch(DALException ex)
			{
				throw new ServiceException(ex.Message, ex);
			}
		}

		public Usuario GetUsuarioAdministrador()
		{
			try
			{
				return repo.GetOne(Guid.Parse(ConfigurationManager.AppSettings.Get("ADMINISTRADOR_ID")));
			}
			catch(DALException ex)
			{
				throw new ServiceException(ex.Message, ex);
			}
		}

		public Usuario Login(Usuario obj)
		{
			try
			{
				IEnumerable<Usuario> lista = GetAll();

				// validar email y password
				if(string.IsNullOrEmpty(obj.Email) || string.IsNullOrEmpty(obj.Password))
				{
					throw new ServiceException("El e-mail o password ingresados son incorrectos");
				}

				// validar si existe el email del usuario
				Usuario usuario = lista.FirstOrDefault(x => x.Email == obj.Email);

				if(usuario == null)
				{
					throw new ServiceException("El e-mail o password ingresados son incorrectos");
				}

				// validar si el usuario esta habilitado
				if(!usuario.Habilitado)
				{
					throw new ServiceException("El usuario no esta habilitado");
				}

				// validar si la password es correcta
				if(!Encriptador.VerifyPasswordHash(obj.Password, usuario.Password))
				{
					throw new ServiceException("El password ingresado es incorrecto");
				}

				return usuario;
			}
			catch(DALException ex)
			{
				throw new ServiceException(ex.Message, ex);
			}
		}

		public void Remove(Guid id)
		{
			repo.Remove(id);
		}

		public void Update(Usuario obj)
		{
			repo.Update(obj);
		}

		public bool TienePermiso(Usuario usuario, string permiso)
		{
			return ValidarPermiso(usuario.Permisos, permiso);
		}

		private bool ValidarPermiso(List<Permiso> permisos, string permisoAChequear)
		{
			foreach(Permiso permiso in permisos)
			{
				if(permiso.Nombre == permisoAChequear)
				{
					return true;
				}
				else
				{
					bool tienePermiso = ValidarPermiso(permiso.DevolverPerfil(), permisoAChequear);
					if(tienePermiso)
					{
						return true;
					}
				}
			}

			return false;
		}
	}
}
