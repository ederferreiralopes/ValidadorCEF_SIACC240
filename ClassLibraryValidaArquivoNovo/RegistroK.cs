using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryValidaArquivoCEF
{
    class RegistroK
    {
        string textoValidacao;

        public string TextoValidacao
        {
            get { return textoValidacao; }
            set { textoValidacao = value; }
        }

        internal void validaRegistroK(string linha, int numLinha, string validacao)
        {
            if (linha.Length != 240)
            {
                validacao = "Fora do Padrão: quantidade de colunas é difente de 240! esta linha possui: " + linha.Length + " colunas";
            }
            else
            {
                try
                {
                    //Descrição do Registro DETALHE - “K” (registro obrigatório)
                    string tipoMovimento = linha.Substring(14, 1); //K. 015 015 9(001) Tipo de movimento
                    string codInstrucao = linha.Substring(15, 2); //K. 016 017 9(002) Código da Instrução
                    string identificacaoProduto = linha.Substring(17, 1); //K. 018 018 9(001) Identificação do produto
                    string codSegmentoEmpresa = linha.Substring(18, 1); //K. 019 019 9(001) Código Segmento Empresa
                    string idValor = linha.Substring(19, 1); //K. 020 020 9(001) Identificador de valor
                    string dvCodBarras = linha.Substring(20, 1); //K. 021 021 9(001) DV do Código de Barras
                    string valorDocumento = linha.Substring(21, 9); //K. 022 032 9(009) v99 Valor do Documento
                    string CampoEspecial1 = linha.Substring(32, 8); //K. 033 040 9(008) verificar se é número inteiro
                    string CampoEspecial2 = linha.Substring(40, 16); //K. 041 056 X(016) verificar se não está em branco
                    string filler = linha.Substring(56, 5); //K. 057 061 X(005) Filler

                    string filler2 = linha.Substring(61, 12); //K. 062 073 X(012) Filler
                    string numDocEmpresa = linha.Substring(73, 6); //K. 074 079 X(006) Número do documento na Empresa
                    string filler3 = linha.Substring(79, 14); //K. 080 093 X(014) Filler
                    string dataLancamento = linha.Substring(93, 8); //K. 094 101 9(008) Data Lançamento
                    string tipoMoeda = linha.Substring(101, 3); //K. 102 104 X(003) Tipo da Moeda
                    string quantidadeMoeda = linha.Substring(104, 10); //K. 105 119 9(010)v99999 Quantidade de Moeda
                    string valorLancamento = linha.Substring(119, 15); //K. 120 134 9(015) v99 Valor Lançamento
                    string numDocBanco = linha.Substring(134, 9); //K. 135 143 9(009) Número Documento no banco
                    string filler4 = linha.Substring(143, 11); //K. 144 154 X(011) Filler
                    string dataEfetivacao = linha.Substring(154, 8); //K. 155 162 9(008) Data da efetivação

                    string valorDaEfetivacao = linha.Substring(162, 15); //K. 163 177 9(015) v99 Valor da efetivação
                    string outrasInformacoes = linha.Substring(177, 40); //K. 178 217 X(040) Outras informações
                    string usoFebraban = linha.Substring(217, 12); //K. 218 229 X(012) Uso FEBRABAN
                    string avisoAoFavorecido = linha.Substring(229, 1); //K. 230 230 X(001) Aviso ao favorecido
                    string ocorrenciaRetorno = linha.Substring(230, 10); //K. 231 240 X(010) Ocorrências para retorno

                    switch (tipoMovimento)
                    {
                        case "0": 
                            break;
                        case "9": 
                            break;
                        default: validacao += "<br>Erro no campo: tipo de Movimento = " + tipoMovimento + " : preencher com “0” (inclusão) ou “9” (exclusão)";
                            break;
                    }

                    if (!codInstrucao.Contains("00"))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo:  Código Instrução = " + codInstrucao + " : preencher com “00”<br>");
                    }

                    try
                    {
                        int.Parse(identificacaoProduto);
                    }
                    catch (Exception e)
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Identificação do produto = " + identificacaoProduto + " : preencher conforme consta da 1a posição do código de barras da arrecadação<br>");
                    }

                    switch (codSegmentoEmpresa)
                    {
                        case "1": break;
                        case "2": break;
                        case "3": break;
                        case "4": break;
                        case "5": break;
                        case "6": break;
                        case "7": break;
                        case "9": break;
                        default: validacao +=("Linha: " + numLinha + " Erro no campo: Código Segmento Empresa = " + codSegmentoEmpresa + " : preencher com o código da 2a posição do código de barras da arrecadação<br>");
                            break;
                    }

                    switch (idValor)
                    {
                        case "6": break;
                        case "7": break;
                        default: validacao +=("Linha: " + numLinha + " Erro no campo: Identificador de valor = " + idValor + " : preencher conforme consta da 3a posição do código de barras da arrecadação, sendo: 6(Real) ou 7(Moeda Variável)<br>");
                            break;
                    }

                    try
                    {
                        int.Parse(dvCodBarras);
                    }
                    catch (Exception e)
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: DV do Código de Barras = " + dvCodBarras + " : preencher conforme consta da 4a posição do código de barras da arrecadação<br>");
                    }

                    try
                    {
                        int.Parse(valorDocumento);
                    }
                    catch (Exception e)
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Valor do Documento = " + valorDocumento + " : preencher com o valor constante da 5a a 15a posições do código de barras da arrecadação, não devendo ser considerados os DV de cada módulo de 11<br>");
                    }

                    try
                    {
                        if (!CampoEspecial1.Contains("        "))
                        {
                            try
                            {
                                int.Parse(CampoEspecial1);
                            }
                            catch (Exception erro)
                            {
                                validacao +=("Linha: " + numLinha + " Erro na posição de 33 até 40 = " + CampoEspecial1 + " : preencher apenas com números<br>");
                            }
                        }
                        else
                        {
                            validacao +=("Linha: " + numLinha + " Erro na posição de 33 até 40 = " + CampoEspecial1 + " : Campo está em branco!<br>");
                        }
                    }
                    catch (Exception e)
                    {
                        validacao +=("Linha: " + numLinha + " Erro na posição de 33 até 40 = " + CampoEspecial1 + " : preencher com o valor correspondente<br>");
                    }

                    if (CampoEspecial2.Contains("                "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro na posição de 41 até 56 = " + CampoEspecial2 + " : campo está em branco!<br>");
                    }

                    if (!filler.Contains("     "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Filler = " + filler + " : preencher com espaços!<br>");
                    }

                    if (!filler2.Contains("            "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Filler = " + filler2 + " : preencher com espaços!<br>");
                    }

                    if (numDocEmpresa.Contains("      "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Número do documento na Empresa = " + numDocEmpresa + " : campo está em branco!<br>");
                    }

                    if (!filler3.Contains("              "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Filler = " + filler3 + " : preencher com espaços!<br>");
                    }

                    try
                    {
                        int.Parse(dataLancamento);
                    }
                    catch (Exception e)
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo:  Data Lançamento = " + dataLancamento + " : preencher com a data de vencimento da fatura<br>");
                    }

                    switch (tipoMoeda)
                    {
                        case "BRL": break;
                        case "UFR": break;
                        case "TRD": break;
                        default: validacao +=("Linha: " + numLinha + " Erro no campo: Tipo da Moeda = " + tipoMoeda + " : preencher com BRL(Real) ou UFR(UFIR) ou TRD(Taxa Referencial Diária)<br>");
                            break;
                    }

                    try
                    {
                        int.Parse(quantidadeMoeda);
                        if (!tipoMoeda.Equals("BRL") && quantidadeMoeda.Equals("0000000000"))
                        {
                            validacao +=("Linha: " + numLinha + " Erro no campo: Quantidade de Moeda = " + quantidadeMoeda + "informar a quantidade de moeda a ser convertida<br>");
                        }
                    }
                    catch (Exception e)
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Quantidade de Moeda = " + quantidadeMoeda + " : informar a quantidade de moeda a ser convertida, quando moeda BRL preencher com zeros<br>");
                    }

                    try
                    {
                        int.Parse(valorLancamento);
                        if (tipoMoeda.Equals("BRL") && valorLancamento.Equals("0000000000000"))
                        {
                            validacao +=("Linha: " + numLinha + " Erro no campo: Valor do Lançamento = " + valorLancamento + "informar o valor do lançamento quando tipo de moeda BRL<br>");
                        }
                    }
                    catch (Exception e)
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Valor do Lançamento = " + valorLancamento + " : informar o valor do lançamento quando tipo de moeda BRL<br>");
                    }

                    if (!numDocBanco.Contains("000000000"))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo:  Número Documento no Banco = " + numDocBanco + " : preencher com zeros<br>");
                    }

                    if (!filler4.Contains("           "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Filler = " + filler4 + " : preencher com espaços!<br>");
                    }

                    if (!dataEfetivacao.Contains("00000000"))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo:  Data da efetivação = " + dataEfetivacao + " : na remessa deve ser preenchido com zeros<br>");
                    }

                    if (!valorDaEfetivacao.Contains("0000000000000"))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo:  Valor da efetivação = " + valorDaEfetivacao + " : na remessa deve ser preenchido com zeros<br>");
                    }

                    if (!usoFebraban.Contains("            "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Uso FEBRABAN = " + usoFebraban + " : preencher com espaços!<br>");
                    }

                    if (!outrasInformacoes.Contains("                                        "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Outras informações = " + outrasInformacoes + " : preencher com espaços!<br>");
                    }

                    switch (avisoAoFavorecido)
                    {
                        case "0": break;
                        case "2": break;
                        case "5": break;
                        case "6": break;
                        case "7": break;
                        default: validacao +=("Linha: " + numLinha + " Erro no campo: Aviso ao Favorecido = " + avisoAoFavorecido + " : preencher conforme tabela P006<br>");
                            break;
                    }

                    if (!ocorrenciaRetorno.Contains("          "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Ocorrências do Retorno = " + ocorrenciaRetorno + " : preencher com espaços!<br>");
                    }
                }
                catch (Exception erro)
                {
                    validacao +=("Linha: " + numLinha + " não consegui ler a linha do registro J = " + linha + "<br>" + erro);
                }
            }
            textoValidacao = validacao;
        }
    }
}
