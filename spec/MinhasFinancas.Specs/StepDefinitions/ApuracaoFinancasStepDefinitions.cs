using System;
using MinhasFinancas.Models;
using MinhasFinancas.Support;
using Moq;
using Reqnroll;

namespace MinhasFinancas.StepDefinitions
{
    [Binding]
    public class ApuracaoFinancasStepDefinitions
    {
        protected Conta conta;

        protected Pagamento pagamento;

        protected Lancamento lancamento;

        protected readonly Mock<IRepositorioDeContas> repositorioDeContasMock;

        protected DateTime hoje = DateTime.Today;

        protected decimal saldoDaContaEsperadoHoje;

        public ApuracaoFinancasStepDefinitions()
        {
            repositorioDeContasMock = new Mock<IRepositorioDeContas>();
        }

        [Given("que uma conta tem um saldo de {decimal}")]
        public void GivenQueUmaContaTemUmSaldoDe(decimal saldo)
        {
            conta = ContasStub.ObtemConta(saldo, hoje, repositorioDeContasMock);

            pagamento = PagamentosStub.ObtemPagamentoComValorQualquer(hoje);
        }

        [When("eu lanço um pagamento nessa conta")]
        public async Task WhenEuLancoUmPagamentoNessaConta()
        {
            var saldoHoje = await conta.ObtemSaldoNaData(hoje);

            saldoDaContaEsperadoHoje = saldoHoje.Valor - pagamento.Valor;

            try
            {
                lancamento = await conta.Lanca(pagamento);
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        [Then("o saldo dessa conta deve ser decrescido do valor do pagamento")]
        public async Task ThenOSaldoDessaContaDeveSerDecrescidoDoValorDoPagamento()
        {
            var saldoDaConta = await conta.ObtemSaldoNaData(hoje);

            saldoDaContaEsperadoHoje.Should().Be(saldoDaConta.Valor);
        }
    }
}
