using MinhasFinancas.Models;
using Moq;
using System;

namespace MinhasFinancas.Support;

public static class ContasStub
{
    public static Conta ObtemConta(decimal saldo, DateTime data, Mock<IRepositorioDeContas> repositorioDeContasMock)
    {
        var conta = new Conta(
            id: Guid.NewGuid(),
            tipo: TipoDeConta.Corrente,
            nome: "Conta Corrente",
            numero: "123",
            banco: "001",
            documento: "096",
            email: "",
            repositorio: repositorioDeContasMock.Object);

        repositorioDeContasMock.Setup(r => r.ObtemSaldoDaContaNaDataOrDefault(conta, data)).ReturnsAsync(new Saldo(conta, data, saldo));

        return conta;
    }
}
