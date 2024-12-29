#language: pt-br

Funcionalidade: Contas

Cenário: Sucesso ao lançar uma compra numa fatura de cartão de crédito
	Dado que o cartão tem um limite de 20000
	E que o cartão tem um limite disponível de 2000
	Quando eu lançar uma compra de 200 no cartão
	Então o limite disponível do cartão deverá ser decrescido do valor da compra

Cenário: Sucesso ao pagar uma conta
	Dado que o cartão tem um limite de 20000
	E que o cartão tem um limite disponível de 2000
	Quando eu pagar 2000 desse cartão
	Então o limite disponível do cartão deverá ser acrescido do valor do pagamento