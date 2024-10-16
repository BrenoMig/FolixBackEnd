using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourNamespace.Models
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDProduto { get; set; }

        public string NomeProduto { get; set; }

        public decimal ValorKg { get; set; }

        public string ProdutoImagem { get; set; }

        [NotMapped]
        public string ImagemBase64 { get; set; }
    }
}
