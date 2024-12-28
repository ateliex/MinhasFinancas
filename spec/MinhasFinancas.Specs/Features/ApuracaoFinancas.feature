#language: pt-br

Funcionalidade: Apuração Finanças

Cenário: Sucesso ao lançar um pagamento numa conta
	Dado que uma conta tem um saldo de 200
	Quando eu lanço um pagamento nessa conta
	Então o saldo dessa conta deve ser decrescido do valor do pagamento