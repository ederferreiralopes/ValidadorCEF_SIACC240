using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Entidades
{
    public class Usuario
    {
        public virtual int Id { get; set; }        
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual string Senha { get; set; }
        public virtual DateTime DataCadastro { get; set; }
    }

    public class UsuarioMap : ClassMap<Usuario>
    {
        UsuarioMap()
        {
            Id(x => x.Id);
            Map(x => x.Nome);
            Map(x => x.Email);
            Map(x => x.Senha);
            Map(x => x.DataCadastro);
            Table("Usuario");
        }
    }
}
