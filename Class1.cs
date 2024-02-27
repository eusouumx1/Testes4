namespace Testes4
{
    public class ProdutoServiceTest
    {

        public readonly Mock<IProdutoRepository> _Repo;

        public readonly ProdutoService _Service;

        public ProdutoServiceTest()
        {
            _Repo = new();
            _Service = new(_Repo.Object);
        }

        [Fact(DisplayName = "Teste do metodo GetProduto")]
        public void ProdutoService_GetProduto()
        {
            //Arrange

            Produto produto = new Produto()
            {
                Nome = "Carro",
                Id = 1,
                Preco = 100000
            };
            Produto produto2 = new Produto()
            {
                Nome = "Lixo",
                Id = 2,
                Preco = 199
            };

            _Repo.Setup(x => x.GetById(1)).Returns(produto);

            //Act
            var result = _Service.GetProduto(1);

            //Assert
            result.Should().Be(produto);
        }



        [Fact(DisplayName = "Teste do metodo SalvarProduto")]
        public void ProdutoService_Produto_SalvarProduto_Sucesso()
        {
            //Arrange
            Produto produto = new()
            {
                Nome = "Papel",
                Id = 1,
                Preco = 10

            };

            //Act
            _Service.SalvarProduto(produto);

            //Assert
            _Repo.Verify(x => x.Save(produto), Times.Once);
        }



        [Fact(DisplayName = "Falha por produto nulo - Salvar")]
        public void ProdutoService_Produto_SalvarProduto_FalhaProdutoNulo()
        {
            //Arrange
            Produto produto = null;


            //Act
            _Service.Invoking(x => x.SalvarProduto(produto)).Should().Throw<ArgumentNullException>().Where(x => x.Message.StartsWith("O produto não pode"));



            //Assert
            _Repo.Verify(x => x.Save(produto), Times.Never);



        }



        [Fact(DisplayName = "Falha por nome nulo - Salvar")]
        public void ProdutoService_Produto_SalvarProduto_FalhaNomeNulo()
        {
            //Arrange
            Produto produto = new()
            {
                Id = 1,
                Preco = 10
            };


            //Act
            _Service.Invoking(x => x.SalvarProduto(produto)).Should().Throw<ArgumentException>().Where(x => x.Message.StartsWith("O nome do produto"));



            //Assert
            _Repo.Verify(x => x.Save(produto), Times.Never);

        }



        [Fact(DisplayName = "Falha por preço invalido - Salvar")]
        public void ProdutoService_Produto_SalvarProduto_FalhaPrecoInvalido()
        {
            //Arrange
            Produto produto = new()
            {
                Nome = "Papel",
                Id = 1,
                Preco = -23
            };


            //Act
            _Service.Invoking(x => x.SalvarProduto(produto)).Should().Throw<ArgumentException>().Where(x => x.Message.StartsWith("O preço do produto"));



            //Assert
            _Repo.Verify(x => x.Save(produto), Times.Never);

        }



        [Fact(DisplayName = "Teste do metodo AtualizarProduto")]
        public void ProdutoService_Produto_AtualizarProduto_Sucesso()
        {
            //Arrange
            Produto produto = new()
            {
                Nome = "Papel",
                Id = 1,
                Preco = 10

            };
            _Repo.Setup(x => x.GetById(1)).Returns(produto);
            //Act
            _Service.AtualizarProduto(produto);

            //Assert
            _Repo.Verify(x => x.Update(produto), Times.Once);
        }




        [Fact(DisplayName = "Falha por produto nulo - Atualizar")]
        public void ProdutoService_Produto_AtualizarProduto_FalhaProdutoNulo()
        {
            //Arrange
            Produto produto = null;
            _Repo.Setup(x => x.GetById(1)).Returns(produto);
            //Act
            _Service.Invoking(x => x.AtualizarProduto(produto)).Should().Throw<ArgumentNullException>().Where(x => x.Message.StartsWith("O produto não pode"));

            //Assert
            _Repo.Verify(x => x.Update(produto), Times.Never);
        }



        [Fact(DisplayName = "Falha por produto não encontrado - Atualizar")]
        public void ProdutoService_Produto_AtualizarProduto_FalhaProdutoNaoEncontrado()
        {
            //Arrange
            Produto produto = new()
            {
                Nome = "Papel",
                Id = 1,
                Preco = 10
            };

            //Act
            _Service.Invoking(x => x.AtualizarProduto(produto)).Should().Throw<InvalidOperationException>().Where(x => x.Message.StartsWith("Não é possível atualizar"));

            //Assert
            _Repo.Verify(x => x.Update(produto), Times.Never);
        }



        [Fact(DisplayName = "Falha por nome nulo - Atualizar")]
        public void ProdutoService_Produto_AtualizarProduto_FalhaNomeNulo()
        {
            //Arrange
            Produto produto = new()
            {
                Nome = null,
                Id = 1,
                Preco = 10
            };
            _Repo.Setup(x => x.GetById(1)).Returns(produto);

            //Act
            _Service.Invoking(x => x.AtualizarProduto(produto)).Should().Throw<ArgumentException>().Where(x => x.Message.StartsWith("O nome do produto"));

            //Assert
            _Repo.Verify(x => x.Update(produto), Times.Never);
        }



        [Fact(DisplayName = "Falha por preço invalido - Atualizar")]
        public void ProdutoService_Produto_AtualizarProduto_FalhaPrecoInvalido()
        {
            //Arrange
            Produto produto = new()
            {
                Nome = "Papel",
                Id = 1,
                Preco = -10
            };
            _Repo.Setup(x => x.GetById(1)).Returns(produto);

            //Act
            _Service.Invoking(x => x.AtualizarProduto(produto)).Should().Throw<ArgumentException>().Where(x => x.Message.StartsWith("O preço do produto"));

            //Assert
            _Repo.Verify(x => x.Update(produto), Times.Never);
        }



        [Fact(DisplayName = "Teste do metodo ExcluirProduto")]
        public void ProdutoService_ExcluirProduto_Sucesso()
        {
            //Arrange
            Produto produto = new()
            {
                Nome = "Papel",
                Id = 1,
                Preco = 10

            };
            _Repo.Setup(x => x.GetById(1)).Returns(produto);
            //Act
            _Service.ExcluirProduto(1);

            //Assert
            _Repo.Verify(x => x.Delete(1), Times.Once);

        }



        [Fact(DisplayName = "Falha por produto não encontrado - Excluir")]
        public void ProdutoService_ExcluirProduto_FalhaNaoEncontrado()
        {
            //Arrange
            Produto produto = new()
            {
                Nome = "Papel",
                Id = 1,
                Preco = 10

            };

            //Act
            _Service.Invoking(x => x.ExcluirProduto(1)).Should().Throw<InvalidOperationException>().Where(x => x.Message.StartsWith("Não é possível excluir"));

            //Assert
            _Repo.Verify(x => x.Delete(1), Times.Never);

        }



        [Fact(DisplayName = "Teste do metodo ObterTodosProdutos")]
        public void ProdutoService_ProdutoList_ObterTodosProdutos_Sucesso()
        {
            //Arrange
            Produto produto = new()
            {
                Nome = "Papel",
                Id = 1,
                Preco = 10

            };
            Produto produto2 = new()
            {
                Nome = "Avião",
                Id = 2,
                Preco = 904654

            };
            List<Produto> produtoList = new() { produto, produto2 };

            _Repo.Setup(x => x.GetAll()).Returns(produtoList);
            //Act
            _Service.ObterTodosProdutos();

            //Assert
            _Repo.Verify(x => x.GetAll(), Times.Once);
        }


    }
}
