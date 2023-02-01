using System.ComponentModel.DataAnnotations;

namespace DRD.WebApp.MVC.Models
{
    public class CategoriaViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string ImagemUrl { get; set; }

        public IList<ProdutoViewModel> Produtos { get; set; }


    }
}
