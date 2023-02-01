using DRD.Core.DomainObjects;

namespace DRD.Catalogo.API.Models
{
    public class Produto : Entity, IAggregateRoot
    {
        public string Nome { get; set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Imagem { get; private set; }
        public int QuantidadeEstoque { get; private set; }
        public bool Disponivel { get; private set; }
        public bool Removido { get; private set; }

        public Guid CategoriaId { get; private set; }
        public Categoria Categoria { get; private set; }

        protected Produto(){}

        public Produto(string nome, string descricao, decimal valor, DateTime dataCadastro, string imagem, int quantidadeEstoque, Guid categoriaId)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Ativo = true;
            Valor = valor;
            DataCadastro = dataCadastro;
            Imagem = imagem;
            QuantidadeEstoque = quantidadeEstoque;
            CategoriaId = categoriaId;
            Disponivel = true;
            Removido = false;
        }

        public void AdicionarCategoria(Categoria categoria)
        {
            Categoria = categoria;
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
        public void AtualizarDescricao(string descricao) => Descricao = descricao;
        public void AtualizarValor(decimal valor) => Valor = valor;
        public void AtualizarQauntidadeEstoque(int quantidade) => QuantidadeEstoque += quantidade;
        public void AtualizarImagem(string image) => Imagem = image;
        public void AtualizarCategoria(Guid categoriaId) => CategoriaId = categoriaId;
    }

    public class ProdutoViewModel
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Imagem { get; private set; }
        public int QuantidadeEstoque { get; private set; }

        public string NomeCategoria { get; private set; }

        public ProdutoViewModel(string nome, string descricao, decimal valor, string imagem, int quantidadeEstoque, string nomeCategoria)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            Imagem = imagem;
            QuantidadeEstoque = quantidadeEstoque;
            NomeCategoria = nomeCategoria;
        }
    }
}
