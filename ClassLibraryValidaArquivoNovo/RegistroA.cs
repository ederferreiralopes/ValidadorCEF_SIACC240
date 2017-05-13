using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryValidaArquivoCEF
{
    class RegistroA
    {

        string textoValidacao;

        public string TextoValidacao
        {
            get { return textoValidacao; }
            set { textoValidacao = value; }
        }

        internal void validaRegistroA(string linha, int numLinha)
        {
            if (linha.Length != 240)
            {
                textoValidacao +=("Linha: " + numLinha + " Fora do Padrão: quantidade de colunas é difente de 240! esta linha possui: " + linha.Length + " colunas<br>");
            }
            else
            {
                try
                {
                    string tipoMovimento = linha.Substring(14, 1); // A.06 015 015 9(001) Tipo de movimento
                    string codInstrucaoMovimento = linha.Substring(15, 2); // A.07 016 017 9(002) Código Instrução Movimento
                    string camaraCompensacao = linha.Substring(17, 3); // A.08 018 020 9(003) Câmara de Compensação
                    string codBancoDestino = linha.Substring(20, 3); // A.09 021 023 9(003) Código Banco de Destino
                    string codAgenciaDestino = linha.Substring(23, 5); // A.10 024 028 9(005) Código da Agência de Destino
                    string dvAgenciaDestino = linha.Substring(28, 1); // A.11 029 029 X(001) DV Agência de Destino
                    string contaCorrenteDestino = linha.Substring(29, 12); // A.12 030 041 9(012) Conta Corrente Destino
                    string dvContaDestino = linha.Substring(41, 1); // A.13 042 042 X(001) DV Conta Destino
                    string dvAgenciaContaDestino = linha.Substring(42, 1); // A.14 043 043 X(001) DV Agência/Conta Destino
                    string nomeFavorecido = linha.Substring(43, 30); // A.15 044 073 X(030) Nome do terceiro - favorecido

                    string numDocAtribuidoEmpresa = linha.Substring(73, 6); // A.16 074 079 9(006) Número do Documento Atribuído pela Empresa
                    string filler = linha.Substring(79, 13); // A.17 080 092 X(013) Filler
                    string tipoConta = linha.Substring(92, 1); // A.18 093 093 X(001) Tipo de conta
                    string dataVencimento = linha.Substring(93, 8); // A.19 094 101 9(008) Data de vencimento
                    string tipoMoeda = linha.Substring(101, 3); // A.20 102 104 X(003) Tipo de Moeda
                    string quantMoeda = linha.Substring(114, 10); // A.21 105 119 9(010) Quantidade de Moeda
                    string valorLancamento = linha.Substring(119, 15); // A.22 120 134 9(015) Valor do Lançamento
                    string numDocBanco = linha.Substring(134, 9); // A.23 135 143 9(009) Número Documento Banco
                    string filler24 = linha.Substring(143, 3); // A.24 144 146 X(003) Filler
                    string quantParcelas = linha.Substring(146, 2); // A.25 147 148 9(002) Quantidade de Parcelas

                    string indicadorBloqueio = linha.Substring(148, 1); // A.26 149 149 X(001) Indicador de Bloqueio, indica se as parcelas posteriores deverão ser efetivadas ou não. Caso alguma não seja efetuada, preencher: S - Bloqueia as demais parcelas ou N - Não bloqueia as demais parcelas
                    string indicadorFormaParcelamento = linha.Substring(149, 1); // A.27 150 150 9(001) Indicador Forma de Parcelamento, preencher: “1” - Data Fixa ou “2” - Periódico ou “3” - Dia útil
                    string periodoOuDiaVencimento = linha.Substring(150, 2); // A.28 151 152 X(002) Período ou dia de vencimento, preencher com número desejado para o tratamento do Indicador da Forma de Parcelamento, sendo: - Quando for informado o Indicador de Forma de Lançamento, Data Fixa, significa que será efetuado no dia informado, por exemplo, se for informado 05, será efetuado o lançamento no dia 05 de cada mês; - Quando for informado o Indicador de Forma de Lançamento, Periódico, significa que será efetuado a cada período informado, por exemplo, se for informado 05, será efetuado a cada 5 dias; - Quando for informado o Indicador de Forma de Parcelamento, Dia útil, significa que será efetuado no dia útil informado, por exemplo, se for informado 05, será efetuado no 5º dia útil do mês
                    string numeroParcela = linha.Substring(152, 2); // A.29 153 154 9(002) Número Parcela - Quando parcela única informar “00”
                    string dataEfetivacao = linha.Substring(154, 8); // A.30 155 162 9(008) Data da efetivação - na remessa deve ser preenchido com zeros
                    string valorRealEfetivado = linha.Substring(162, 15); // A.31 163 177 9(015) Valor Real Efetivado - na remessa deve ser preenchidos com zeros
                    string informacao2 = linha.Substring(177, 40); // A.32 178 217 X(040) Informação 2 - preencher com espaços
                    string FinalidadeDoc = linha.Substring(217, 2); // A.33 218 219 9(002) Finalidade DOC - preencher conforme tabela P005, quando pagamento através de DOC para os demais preencher com “00”
                    string usoFebraban = linha.Substring(219, 10); // A.34 220 229 X(010) Uso FEBRABAN - preencher com espaços
                    string avisoFavorecido = linha.Substring(229, 1); // A.35 230 230 9(001) Aviso ao Favorecido - preencher conforme tabela P006
                    string ocorrencias = linha.Substring(230, 10); // A.36 231 240 X(010) Ocorrências - preencher com espaços

                    switch (tipoMovimento)
                    {
                        case "0": break;
                        case "9": break;
                        default: textoValidacao +=("Linha: " + numLinha + " Atenção no campo: Tipo de movimento = " + tipoMovimento + " : preencher: “0” - inclusão ou “9” - exclusão<br>");
                            break;
                    }

                    if (!codInstrucaoMovimento.Contains("00"))
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo:  Código Instrução Movimento = " + codInstrucaoMovimento + " : preencher com “00”<br>");
                    }

                    switch (camaraCompensacao)
                    {
                        case "018": break;
                        case "700": break;
                        default: textoValidacao +=("Linha: " + numLinha + " Erro no campo: Câmara de Compensação = " + camaraCompensacao + " : preencher: “018” – Finalidade TED ou “700” – Finalidade DOC<br>");
                            break;
                    }

                    Auxiliar auxBancoDestino = new Auxiliar();
                    auxBancoDestino.validaCodBanco(numLinha, codBancoDestino);
                    textoValidacao += auxBancoDestino.TextoValidacao;

                    try
                    {
                        int.Parse(codAgenciaDestino);
                    }
                    catch (Exception e)
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo: Código da Agência de Destino = " + codAgenciaDestino + " : preencher com código da Agência do favorecido<br>");
                    }

                    if (dvAgenciaDestino.Contains(" "))
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo:  DV Agência de Destino = " + dvAgenciaDestino + " : preencher com o dígito verificador da Agência do favorecido<br>");
                    }

                    try
                    {
                        long.Parse(contaCorrenteDestino);
                    }
                    catch (Exception e)
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo: Conta Corrente Destino = " + contaCorrenteDestino + " : preencher com o número da conta corrente do favorecido<br>");
                    }

                    if (dvContaDestino.Contains(" ") && codBancoDestino.Equals("104"))
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo:  DV Conta Destino = " + dvContaDestino + " : preencher com o dígito verificador da conta corrente do favorecido<br>");
                    }

                    if (!dvAgenciaContaDestino.Contains(" "))
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo:  DV Agência/Conta Destino = " + dvAgenciaContaDestino + " : preencher com espaço<br>");
                    }

                    if (nomeFavorecido.Contains("                              "))
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo:  Nome do terceiro = " + nomeFavorecido + " : preencher com o nome do favorecido<br>");
                    }

                    try
                    {
                        int.Parse(numDocAtribuidoEmpresa);
                    }
                    catch (Exception e)
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo: Número do Documento Atribuído pela Empresa = " + numDocAtribuidoEmpresa + " : preencher com o número do agendamento atribuído pela empresa<br>");
                    }

                    if (!filler.Contains("             "))
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo:  Filler = " + filler + " : preencher com espaços<br>");
                    }

                    switch (tipoConta)
                    {
                        case "0": break;
                        case "1": break;
                        case "2": break;
                        default: textoValidacao +=("Linha: " + numLinha + " Erro no campo: Tipo de conta – Finalidade TED = " + tipoConta + " : preencher: “0” – Sem conta ou “1” – Conta corrente ou “2” – Poupança<br>");
                            break;
                    }

                    try
                    {
                        int.Parse(dataVencimento);
                    }
                    catch (Exception e)
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo: Data de vencimento = " + dataVencimento + " : preencher com a data de vencimento do agendamento (DDMMAAAA)<br>");
                    }

                    switch (tipoMoeda)
                    {
                        case "BRL": break;
                        case "USD": break;
                        case "UFR": break;
                        case "TRD": break;
                        default: textoValidacao +=("Linha: " + numLinha + " Erro no campo: Tipo de Moeda = " + tipoConta + " : preencher: “BRL”(Real) ou “USD”(Dólar Americano) ou “UFR”(UFIR) ou “TRD”(Taxa Referencial Diária)<br>");
                            break;
                    }

                    try
                    {
                        int.Parse(quantMoeda);
                        if (!tipoMoeda.Equals("BRL") && quantMoeda.Equals("0000000000"))
                        {
                            textoValidacao +=("Linha: " + numLinha + " Erro no campo: Quantidade de Moeda = " + quantMoeda + "informar a quantidade de moeda a ser convertida<br>");
                        }
                    }
                    catch (Exception e)
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo: Quantidade de Moeda = " + quantMoeda + " : informar a quantidade de moeda a ser convertida, quando moeda BRL preencher com zeros<br>");
                    }

                    try
                    {
                        int.Parse(valorLancamento);
                        if (tipoMoeda.Equals("BRL") && valorLancamento.Equals("0000000000000"))
                        {
                            textoValidacao +=("Linha: " + numLinha + " Erro no campo: Valor do Lançamento = " + valorLancamento + "informar o valor do lançamento quando tipo de moeda BRL<br>");
                        }
                    }
                    catch (Exception e)
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo: Valor do Lançamento = " + valorLancamento + " : informar o valor do lançamento quando tipo de moeda BRL<br>");
                    }

                    if (!numDocBanco.Contains("000000000"))
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo:  Número Documento Banco = " + numDocBanco + " : preencher com zeros<br>");
                    }

                    if (!filler24.Contains("   "))
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo:  Filler = " + filler24 + " : preencher com espaços<br>");
                    }

                    try
                    {
                        int numParcelas = int.Parse(quantParcelas);
                        if (numParcelas < 1)
                        {
                            textoValidacao +=("Linha: " + numLinha + " Erro no campo: Quantidade de Parcelas = " + quantParcelas + " :preencher com a quantidade de parcelas a ser efetuado o pagamento, para pagamento único preencher com “01”<br>");
                        }
                    }
                    catch (Exception e)
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo: Quantidade de Parcelas = " + quantParcelas + " : preencher com a quantidade de parcelas a ser efetuado o pagamento, para pagamento único preencher com “01”<br>");
                    }



                    switch (indicadorBloqueio)
                    {
                        case "S": break;
                        case "N": break;
                        default: textoValidacao +=("Linha: " + numLinha + " Erro no campo: Indicador de Bloqueio = " + indicadorBloqueio + " : preencher: S - Bloqueia as demais parcelas ou N - Não bloqueia as demais parcelas<br>");
                            break;
                    }

                    switch (indicadorFormaParcelamento)
                    {
                        case "1": break;
                        case "2": break;
                        case "3": break;
                        default: textoValidacao +=("Linha: " + numLinha + " Erro no campo: Indicador Forma de Parcelamento = " + indicadorFormaParcelamento + " : preencher: “1” - Data Fixa ou “2” - Periódico ou “3” - Dia útil<br>");
                            break;
                    }

                    if (periodoOuDiaVencimento.Contains("  "))
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo:  Período ou dia de vencimento = " + periodoOuDiaVencimento + " : preencher com número desejado para o tratamento do Indicador da Forma de Parcelamento<br>");
                    }
                    try
                    {
                        if (!numeroParcela.Contains("00") && quantParcelas.Contains("1"))
                        {
                            textoValidacao +=("Linha: " + numLinha + " Erro no campo:  Número Parcela = " + numeroParcela + " : Quando parcela única informar “00”<br>");
                        }
                        int.Parse(numeroParcela);
                    }
                    catch (Exception e)
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo:  Número Parcela = " + numeroParcela + " : Quando parcela única informar “00”<br>");
                    }

                    try { int.Parse(dataEfetivacao); }
                    catch (Exception e) { textoValidacao +=("Linha: " + numLinha + " Erro no campo:  Data da efetivação = " + dataEfetivacao + " : na remessa deve ser preenchido com zeros<br>"); }

                    try { int.Parse(valorRealEfetivado); }
                    catch (Exception e) { textoValidacao +=("Linha: " + numLinha + " Erro no campo:  Valor Real Efetivado = " + valorRealEfetivado + " : na remessa deve ser preenchido com zeros<br>"); }

                    if (!informacao2.Contains("                                        "))
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo:  Informação 2 = " + informacao2 + " : preencher com espaços<br>");
                    }

                    switch (FinalidadeDoc)
                    {
                        case "01": break;
                        case "02": break;
                        case "03": break;
                        case "04": break;
                        case "05": break;
                        case "06": break;
                        case "07": break;
                        case "08": break;
                        case "09": break;
                        case "10": break;
                        case "11": break;
                        case "00": break;
                        default: textoValidacao +=("Linha: " + numLinha + " Erro no campo: Finalidade DOC = " + FinalidadeDoc + " : preencher conforme tabela P005, se não for DOC preencher com “00”<br>");
                            break;
                    }

                    if (!usoFebraban.Contains("          "))
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo: Uso FEBRABAN = " + usoFebraban + " : preencher com espaços!<br>");
                    }

                    switch (avisoFavorecido)
                    {
                        case "0": break;
                        case "2": break;
                        case "5": break;
                        case "6": break;
                        case "7": break;
                        default: textoValidacao +=("Linha: " + numLinha + " Erro no campo: Aviso ao Favorecido = " + avisoFavorecido + " : preencher conforme tabela P006<br>");
                            break;
                    }

                    if (!ocorrencias.Contains("          "))
                    {
                        textoValidacao +=("Linha: " + numLinha + " Erro no campo: Ocorrências do Retorno = " + ocorrencias + " : preencher com espaços!<br>");
                    }

                }
                catch (Exception erro)
                {
                    textoValidacao +=("Linha: " + numLinha + " não consegui ler a linha do registro J = " + linha + "<br>" + erro);
                }
            }
        }
    }
}
