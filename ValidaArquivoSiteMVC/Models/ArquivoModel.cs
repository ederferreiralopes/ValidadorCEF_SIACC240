using System;
using System.Web;

namespace ValidaArquivoSiteMVC.Models
{
    public class ArquivoModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public String Nome {get;set;}
        public String Tipo { get; set; }
        public String Validacao { get; set; }
        public String Responsavel { get; set; }
        public HttpPostedFileBase Arquivo { get; set; }
    }
}