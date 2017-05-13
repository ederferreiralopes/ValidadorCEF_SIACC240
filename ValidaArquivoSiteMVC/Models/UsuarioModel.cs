using System;

namespace ValidaArquivoSiteMVC.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }        
        public String Nome { get; set; }
        public String Email { get; set; }
        public String Senha { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}