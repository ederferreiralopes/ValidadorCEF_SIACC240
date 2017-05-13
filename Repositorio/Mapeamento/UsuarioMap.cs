
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace Repositorio.Entidades
{
    public class UsuarioRepositorio : RepositoryBase<Usuario>
    {
        public bool LoginExiste(string login)
        {
            using (ISession session = SessionFactory.AbrirSession())
            {
                return (from e in session.Query<Usuario>() where e.Email.Equals(login) select e).Count() > 0;
            }
        }

        public Usuario ValidarAcesso(string email, string senha)
        {
            using (ISession session = SessionFactory.AbrirSession())
            {
                return (from e in session.Query<Usuario>() where e.Email.Equals(email) && e.Senha.Equals(senha) select e).FirstOrDefault();
            }
        }
    }
}
