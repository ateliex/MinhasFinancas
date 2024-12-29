using MinhasFinancas.Models;
using Moq;
using System;

namespace MinhasFinancas.Support;

public static class CartoesStub
{
    public static CartaoCredito ObtemCartaoCredito(decimal saldo, DateTime data, Mock<ICartoesRepository> cartoesRepositoryMock)
    {
        var cartao = new CartaoCredito(
            id: Guid.NewGuid(),
            tipo: TipoConta.FaturaComum,
            nome: "Conta Corrente",
            numero: "123",
            banco: "001",
            documento: "096",
            repository: cartoesRepositoryMock.Object);

        cartoesRepositoryMock.Setup(r => r.ObtemSaldoDaContaNaDataOrDefault(cartao, data)).ReturnsAsync(new Saldo(cartao, data, saldo));

        return cartao;
    }
}
