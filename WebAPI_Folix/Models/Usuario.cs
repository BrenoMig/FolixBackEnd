using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourNamespace.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDCadastro { get; set; }
        public string NomeCompleto { get; set; }
        public int Idade { get; set; }
        public string Email { get; set; }
        public long Telefone { get; set; }
        public long CPF { get; set; }
        public string Senha { get; set; }
    }
}
