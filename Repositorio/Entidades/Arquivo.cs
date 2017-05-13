using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Entidades
{
    public class Arquivo
    {
        public virtual int Id { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual String Nome { get; set; }
        public virtual String Tipo { get; set; }
        public virtual String Validacao { get; set; }
        public virtual String Responsavel { get; set; }
    }

    public class ArquivoMap : ClassMap<Arquivo>
    {
        ArquivoMap()
        {
            Id(x => x.Id);
            Map(x => x.Data);
            Map(x => x.Nome);
            Map(x => x.Tipo);
            Map(x => x.Validacao);
            Map(x => x.Responsavel);
            Table("Arquivo");
        }
    }
}
