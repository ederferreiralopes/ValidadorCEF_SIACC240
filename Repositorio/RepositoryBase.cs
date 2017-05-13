using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        /// <summary>
        /// Método para inserir
        /// </summary>
        /// <param name="entidade"></param>        
        public object Add(TEntity entidade)
        {
            using (ISession session = SessionFactory.AbrirSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        var obj = session.Save(entidade);
                        transacao.Commit();

                        return obj;
                    }
                    catch (Exception erro)
                    {
                        if (!transacao.WasCommitted)
                        {
                            transacao.Rollback();
                        }
                        throw new Exception("Erro ao inserir dados : " + erro.Message);
                    }
                }
            }
        }

        public void Update(TEntity entidade)
        {
            using (ISession session = SessionFactory.AbrirSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(entidade);
                        transacao.Commit();
                    }
                    catch (Exception erro)
                    {
                        if (!transacao.WasCommitted)
                        {
                            transacao.Rollback();
                        }
                        throw new Exception("Erro ao alterar dados : " + erro.Message);
                    }
                }
            }
        }

        public void Remove(TEntity entidade)
        {
            using (ISession session = SessionFactory.AbrirSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(entidade);
                        transacao.Commit();
                    }
                    catch (Exception erro)
                    {
                        if (!transacao.WasCommitted)
                        {
                            transacao.Rollback();
                        }
                        throw new Exception("Erro ao excluir dados : " + erro.Message);
                    }
                }
            }
        }

        public TEntity GetById(int Id)
        {
            using (ISession session = SessionFactory.AbrirSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    return session.Get<TEntity>(Id);
                }
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (ISession session = SessionFactory.AbrirSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    return (from c in session.Query<TEntity>() select c).ToList();
                }
            }
        }

        public void Dispose()
        {

        }
    }
}
