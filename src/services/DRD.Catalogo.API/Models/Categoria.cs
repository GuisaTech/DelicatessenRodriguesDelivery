using DRD.Core.DomainObjects;

namespace DRD.Catalogo.API.Models
{
    public class Categoria : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string ImagemUrl { get; private set; }
        public bool Disponivel { get; private set; }
        public bool Removido { get; private set; }
        public IList<Produto> Produtos { get; private set; }

        protected Categoria() { }

        public Categoria(string nome, string imagemUrl, bool disponivel = false)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            ImagemUrl = imagemUrl;
            Disponivel = disponivel;
        }

        public void AlterarDisponibilidade(bool disponivel)
        {
            Disponivel = disponivel;
        }

        public void Disabilitar()
        {
            Removido = true;
        }

        public void AtualizarNome(string nome) => Nome = nome;
        public void AtualizarUrlImagem(string url) => ImagemUrl = url;
    }

    public class CategoriaViewModel
    {
        public string Nome { get; private set; }
        public string ImagemUrl { get; private set; }

        public CategoriaViewModel(string nome, string imagemUrl)
        {
            Nome = nome;
            ImagemUrl = imagemUrl;
        }
    }
}
