using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryValidaArquivoCEF
{
    class RegistroJ
    {
        string textoValidacao = string.Empty;

        public string TextoValidacao
        {
            get { return textoValidacao; }
            set { textoValidacao = value; }
        }

        public void validaRegistroJ(string linha, int numLinha)
        {
            if (linha.Length != 240)
            {
                textoValidacao += ( "Linha: " + numLinha + " Fora do Padrão: quantidade de colunas é difente de 240! esta linha possui: " + linha.Length + " colunas<br>");
            }
            else
            {
                try
                {
                    // Descrição do Registro DETALHE - “J” (registro obrigatório)
                    string tipoMovimento = linha.Substring(14, 1); //J.06 015 015 9(001) Tipo Movimento
                    string codMovimento = linha.Substring(15, 2); //J.07 016 017 9(002) Cód. Movimento
                    string bancoDestino = linha.Substring(17, 3); //J.08 018 020 9(003) Banco destino
                    string codMoeda = linha.Substring(20, 1); //J.09 021 021 9(001) Cód. Moeda
                    string dvCodBarras = linha.Substring(21, 1); //J.10 022 022 9(001) DV Cód. Barras
                    string fatorVenc = linha.Substring(22, 4); //J.11 023 026 9(004) Fator de vencimento
                    string valorDocumento = linha.Substring(26, 8); //J.12 027 036 9(008) v99 Valor do Documento
                    string campoLivre = linha.Substring(36, 25); //J.13 037 061 X(025) Campo Livre
                    string nomeCedente = linha.Substring(61, 30); //J.14 062 091 X(030) Nome do Cedente

                    string dataVencimento = linha.Substring(91, 8); //J.15 092 099 9(008) Data Vencimento
                    string valorTitulo = linha.Substring(99, 15); //J.16 100 114 9(015) v99 Valor do Título
                    string valorDesconto = linha.Substring(114, 15); //J.17 115 129 9(013) v99 Valor do desconto + Abatimento
                    string valorMulta = linha.Substring(129, 15); //J.18 130 144 9(013) v99 Valor da mora + multa
                    string dataPagamento = linha.Substring(144, 8); //J.19 145 152 9(008) Data do Pagamento
                    string valorPagamento = linha.Substring(152, 15); //J.20 153 167 9(015)v99 Valor Pagamento
                    string quantidadeMoeda = linha.Substring(167, 10); //J.21 168 182 9(010)v99999 Quantidade Moeda
                    string numDocumentoEmpresa = linha.Substring(182, 6); //J.22 183 188 X(006) Número documento atribuído pela empresa
                    string filler23 = linha.Substring(188, 14); //J.23 189 202 X(014) Filler - preencher com espaço.
                    string numDocumentoBanco = linha.Substring(202, 9); //J.24 203 211 9(009) Número documento atribuído pelo banco

                    string filler25 = linha.Substring(211, 11); //J.25 212 222 X(011) Filler
                    string codigoDaMoeda = linha.Substring(222, 2); //J.26 223 224 9(002) Código da Moeda = 04(TRD) ou 02(Dólar) ou 06(UFIR diária) ou 09(Real)
                    string usoFebraban = linha.Substring(224, 6); //J.27 225 230 X(006) Uso FEBRABAN
                    string ocorrenciasRetorno = linha.Substring(230, 10); //J.28 231 240 X(010) Ocorrências do Retorno

                    switch (tipoMovimento)
                    {
                        case "0": break;
                        case "9": break;
                        default: textoValidacao += ( "Linha: " + numLinha + " Erro no campo: tipo de Movimento = " + tipoMovimento + " : preencher com “0” (inclusão) ou “9” (exclusão)<br>");
                            break;
                    }

                    switch (codMovimento)
                    {
                        case "00": break;
                        case "09": break;
                        case "10": break;
                        case "11": break;
                        case "23": break;
                        case "99": break;
                        default: textoValidacao += ( "Linha: " + numLinha + " Erro no campo: cod. de Movimento = " + codMovimento + " : preencher conforme tabela G061<br>");
                            break;
                    }

                    Auxiliar auxBancoDestino = new Auxiliar();
                    auxBancoDestino.validaCodBanco(numLinha, bancoDestino);
                    textoValidacao += auxBancoDestino.TextoValidacao;

                    try
                    {
                        int.Parse(codMoeda);
                        switch (codMoeda)
                        {
                            case "0": break;
                            case "9": break;
                            default: textoValidacao += ( "Linha: " + numLinha + " Atenção no campo: Cod. moeda = " + codMoeda + " : Está diferente do padrão FEBRABAN<br>");
                                break;
                        }
                    }
                    catch (Exception erro) { textoValidacao += ( "Linha: " + numLinha + " Erro no campo: Cod. codMoeda = " + codMoeda + " :  preencher com o código da moeda conforme constante da 4a posição na barra da cobrança = 9(Real) ou 2(Moeda Variável)" + erro); }

                    try { int.Parse(dvCodBarras); }
                    catch (Exception erro) { textoValidacao += ( "Linha: " + numLinha + " Erro no campo: DV Cod. Barra = " + dvCodBarras + " : preencher conforme código de barras, constante da 5a posição da barra da cobrança" + erro); }

                    try { int.Parse(fatorVenc); }
                    catch (Exception erro) { textoValidacao += ( "Linha: " + numLinha + " Erro no campo: fator de Vencimento = " + fatorVenc + " : preencher com o fator de vencimento constante da 6a a 9a posição da barra da cobrança" + erro); }

                    try { int.Parse(valorDocumento); }
                    catch (Exception erro) { textoValidacao += ( "Linha: " + numLinha + " Erro no campo: Valor do Documento = " + valorDocumento + " : Preencher conforme constante da 10a a 19a posições da barra da cobrança" + erro); }

                    if (campoLivre.Contains("                         "))
                    {
                        textoValidacao += ( "Linha: " + numLinha + " Erro no campo: Campo Livre = " + campoLivre + " : está em branco! preencher conforme constante da 20a a 44a posições na barra da cobrança<br>");
                    }

                    if (nomeCedente.Contains("                              "))
                    {
                        textoValidacao += ( "Linha: " + numLinha + " Erro no campo: Nome do Cedente = " + nomeCedente + " : está em branco! preencher com Nome do Cedente<br>");
                    }

                    try { int.Parse(dataVencimento); }
                    catch (Exception erro) { textoValidacao += ( "Linha: " + numLinha + " Erro no campo: Data de Vencimento = " + dataVencimento + " : preencher com a data de vencimento do bloqueto" + erro); }

                    try { int.Parse(valorDesconto); }
                    catch (Exception erro) { textoValidacao += ( "Linha: " + numLinha + " Erro no campo: Valor do Desconto = " + valorDesconto + " : preencher se for o caso" + erro); }

                    try { int.Parse(valorMulta); }
                    catch (Exception erro) { textoValidacao += ( "Linha: " + numLinha + " Erro no campo: Valor da mora + multa = " + valorMulta + " : Válido somente para bloquetos da CAIXA, bloquetos de outros bancos não podem ser pagos em atraso" + erro); }

                    try { int.Parse(dataPagamento); }
                    catch (Exception erro) { textoValidacao += ( "Linha: " + numLinha + " Erro no campo: Data do Pagamento = " + dataPagamento + " : Mensagem: " + erro); }

                    try { int.Parse(valorPagamento); }
                    catch (Exception erro) { textoValidacao += ( "Linha: " + numLinha + " Erro no campo: Valor Pagamento = " + valorPagamento + " : Mensagem: " + erro); }

                    try { int.Parse(quantidadeMoeda); }
                    catch (Exception erro) { textoValidacao += ( "Linha: " + numLinha + " Erro no campo: Quantidade Moeda = " + quantidadeMoeda + " : preencher com a quantidade de moeda para pagamentos a serem realizados com moeda variável " + erro); }

                    if (numDocumentoEmpresa.Contains("      "))
                    {
                        textoValidacao += ( "Linha: " + numLinha + " Erro no campo: Número documento atribuído pela empresa = " + numDocumentoEmpresa + " : está em branco!<br>");
                    }

                    if (!filler23.Contains("              "))
                    {
                        textoValidacao += ( "Linha: " + numLinha + " Erro no campo: Filler = " + filler23 + " : preencher com espaços!<br>");
                    }

                    if (!numDocumentoBanco.Contains("         "))
                    {
                        textoValidacao += ( "Linha: " + numLinha + " Atenção no campo: Número documento atribuído pelo banco = " + numDocumentoBanco + " : preencher com espaços!<br>");
                    }

                    if (!filler25.Contains("           "))
                    {
                        textoValidacao += ( "Linha: " + numLinha + " Erro no campo: Filler = " + filler25 + " : preencher com espaços!<br>");
                    }

                    try
                    {
                        int.Parse(codigoDaMoeda);
                        switch (codigoDaMoeda)
                        {
                            case "02": break;
                            case "04": break;
                            case "06": break;
                            case "09": break;
                            default: textoValidacao += ( "Linha: " + numLinha + " Atenção no campo: Código da Moeda = " + codigoDaMoeda + " : Está diferente do padrão FEBRABAN<br>");
                                break;
                        }
                    }
                    catch (Exception erro) { textoValidacao += ( "Linha: " + numLinha + " Erro no campo: Código da Moeda = " + codigoDaMoeda + " : Mensagem: " + erro); }

                    if (!usoFebraban.Contains("      "))
                    {
                        textoValidacao += ( "Linha: " + numLinha + " Erro no campo: Uso FEBRABAN = " + usoFebraban + " : preencher com espaços!<br>");
                    }

                    if (!ocorrenciasRetorno.Contains("          "))
                    {
                        textoValidacao += ( "Linha: " + numLinha + " Erro no campo: Ocorrências do Retorno = " + ocorrenciasRetorno + " : preencher com espaços!<br>");
                    }

                }
                catch (Exception erro)
                {
                    textoValidacao += ( "Linha: " + numLinha + " não consegui ler a linha do registro J = " + linha + "<br>" + erro);
                }
            }
        }
    }
}
