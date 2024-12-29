using System;
using MinhasFinancas.Models;
using MinhasFinancas.Support;
using Moq;
using Reqnroll;

namespace MinhasFinancas.StepDefinitions
{
    [Binding]
    public class CartoesStepDefinitions
    {
        protected CartaoCredito cartao;

        protected Pagamento pagamento;

        protected readonly Mock<ICartoesRepository> cartoesRepositoryMock;

        protected DateTime hoje = DateTime.Today;

        protected decimal saldoCartaoEsperadoHoje;

        public CartoesStepDefinitions()
        {
            cartoesRepositoryMock = new Mock<ICartoesRepository>();
        }

        [Given("que uma tem um saldo de {decimal}")]
        public void GivenQueUmaContaTemUmSaldoDe(decimal saldo)
        {
            cartao = CartoesStub.ObtemCartaoCredito(saldo, hoje, cartoesRepositoryMock);

            pagamento = PagamentosStub.ObtemPagamentoComValorQualquer(hoje);
        }

        [When("eu lanço um pagamento nessa conta")]
        public async Task WhenEuLancoUmPagamentoNessaConta()
        {
            var saldoHoje = await cartao.ObtemSaldoNaData(hoje);

            saldoCartaoEsperadoHoje = saldoHoje.Valor - pagamento.Valor;

            try
            {
                await cartao.Pagar(pagamento);
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        [Then("o saldo dessa conta deve ser decrescido do valor do pagamento")]
        public async Task ThenOSaldoDessaContaDeveSerDecrescidoDoValorDoPagamento()
        {
            var saldoConta = await cartao.ObtemSaldoNaData(hoje);

            saldoCartaoEsperadoHoje.Should().Be(saldoConta.Valor);
        }
    }
}
