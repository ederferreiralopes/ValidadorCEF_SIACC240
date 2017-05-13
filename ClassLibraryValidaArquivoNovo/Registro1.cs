using System;
using System.Collections.Generic;

using System.Text;

namespace ClassLibraryValidaArquivoCEF
{
    public class Registro1
    {
        string textoValidacao = string.Empty;

        public string TextoValidacao
        {
            get { return textoValidacao; }
            set { textoValidacao = value; }
        }

        public void valida(string linha, int numLinha)
        {
            if (linha.Length != 240)
            {
                textoValidacao += ("Linha: " + numLinha + " Fora do Padrão: quantidade de colunas é difente de 240! esta linha possui: " + linha.Length + " colunas<br>");
            }
            else
            {
                try
                {
                    // Descrição do Registro “HEADER” de Lote - “1”
                    string tipoOperacao = linha.Substring(8, 1); //	1.04 009 009 X(001) Tipo de Operação
                    string tipoServico = linha.Substring(9, 2); //	1.05 010 011 9(002) Tipo de Serviço
                    string formaLancamento = linha.Substring(11, 2); //	1.06 012 013 9(002) Forma de Lançamento
                    string verLayoutLote = linha.Substring(13, 3); //	1.07 014 016 9(003) Versão do leiaute do lote
                    string filler8 = linha.Substring(16, 1); //	1.08 017 017 X(001) Filler
                    string tipoInscricao = linha.Substring(17, 1); //	1.09 018 018 9(001) Tipo de inscrição
                    string numInscricao = linha.Substring(18, 14); //	1.10 019 032 9(014) Número da inscrição
                    string codConvenio = linha.Substring(32, 6); //	1.11 033 038 9(006) Código Convênio no Banco
                    string tipoCompromisso = linha.Substring(38, 2); //	1.12 039 040 9(002) Tipo de Compromisso

                    string codCompromisso = linha.Substring(40, 4); //	1.13 041 044 9(004) Código do compromisso
                    string paramTrans = linha.Substring(44, 2); //	1.14 045 046 X(002) Parâmetro de transmissão
                    string filler15 = linha.Substring(46, 6); //	1.15 047 052 X(006) Filler
                    string agContaCorrente = linha.Substring(52, 5); //	1.16 053 057 9(005) Agencia da Conta Corrente
                    string dvAgencia = linha.Substring(57, 1); //	1.17 058 058 X(001) DV da Agência
                    string numContaCorrente = linha.Substring(58, 12); //	1.18 059 070 9(012) Número da Conta Corrente
                    string DvContaCorrente = linha.Substring(70, 1); //	1.19 071 071 X(001) DV da Conta Corrente
                    string digAgConta = linha.Substring(71, 1); //	1.20 072 072 X(001) Dígito da Agência/Conta
                    string nomeEmpresa = linha.Substring(72, 30); //	1.21 073 102 X(030) Nome da Empresa
                    string msgAviso1 = linha.Substring(102, 40); //	1.22 103 142 X(040) Mensagem de Aviso 1

                    string logradouro = linha.Substring(142, 30); //	1.23 143 172 X(030) Logradouro
                    string numLocal = linha.Substring(172, 5); //	1.24 173 177 9(005) Número no local
                    string complemento = linha.Substring(177, 15); //	1.25 178 192 X(015) Complemento
                    string cidade = linha.Substring(192, 20); //	1.26 193 212 X(020) Cidade
                    string cep = linha.Substring(212, 5); //	1.27 213 217 9(005) CEP
                    string complementoCep = linha.Substring(217, 3); //	1.28 218 220 X(003) Complemento CEP
                    string siglaEstado = linha.Substring(220, 2); //	1.29 221 222 X(002) Sigla do Estado
                    string usoFebraban = linha.Substring(222, 8); //	1.30 223 230 X(008) Uso exclusivo FEBRABAN
                    string ocorrencias = linha.Substring(230, 10); //	1.31 231 240 X(010) Ocorrências

                    switch (tipoOperacao)
                    {
                        case "C": break;
                        case "D": break;
                        default: textoValidacao += ("Linha: " + numLinha + " Erro no campo: tipo de Operação = " + tipoOperacao + " : preencher com C (compromisso de pagamento) ou D (compromisso de recebimento)<br>");
                            break;
                    }

                    switch (tipoServico)
                    {
                        case "00": break;
                        case "05": break;
                        case "10": break;
                        case "20": break;
                        case "30": break;
                        case "50": break;
                        case "60": break;
                        case "70": break;
                        case "75": break;
                        case "80": break;
                        case "90": break;
                        case "98": break;
                        default: textoValidacao += ("Linha: " + numLinha + " Erro no campo: tipo de Serviço = " + tipoServico + " : preencher conforme tabela G025<br>");
                            break;
                    }

                    switch (formaLancamento)
                    {
                        case "01": break;
                        case "02": break;
                        case "03": break;
                        case "05": break;
                        case "10": break;
                        case "11": break;
                        case "30": break;
                        case "31": break;
                        case "41": break;
                        case "50": break;
                        default: textoValidacao += ("Linha: " + numLinha + " Erro no campo: tipo de Serviço = " + formaLancamento + " : preencher conforme tabela G029<br>");
                            break;
                    }

                    if (!verLayoutLote.Contains("041"))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: ver. do layout do lote = " + verLayoutLote + " : o pradão é 041<br>");
                    }

                    if (!filler8.Contains(" "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: filler 8 = " + filler8 + " : preencher com espaços<br>");
                    }

                    switch (tipoInscricao)
                    {
                        case "1": break;
                        case "2": break;
                        default: textoValidacao += ("Linha: " + numLinha + " Erro no campo: Num. Inscricao = " + tipoInscricao + " : preencher com 1 (pessoa física) ou 2 (pessoa jurídica)<br>");
                            break;
                    }


                    try
                    {
                        long.Parse(numInscricao);
                    }
                    catch (Exception erro)
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: Num. Inscricao = " + linha.Substring(18, 32) + " : preencher com CPF ou CNPJ" + erro);
                    }

                    try
                    {
                        int.Parse(codConvenio);
                    }
                    catch (Exception erro)
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: Cod. Convênio = " + linha.Substring(32, 38) + " : preencher com o código do convênio informado pelo Banco" + erro);
                    }

                    switch (tipoCompromisso)
                    {
                        case "01": break;
                        case "02": break;
                        case "03": break;
                        case "06": break;
                        case "11": break;
                        default: textoValidacao += ("Linha: " + numLinha + " Erro no campo: tipo de compromisso = " + tipoCompromisso + " : preencher com o tipo de compromisso:<br>");
                            textoValidacao += ("Linha: " + numLinha + "  >>>>  01 (Pag. Fornecedor), 02 (Pag. Salários), 03 (Autopagamento), 06 (Salário Ampliação de Base), 11 (Débito em conta) <br>");
                            break;
                    }

                    try
                    {
                        int.Parse(codCompromisso);
                    }
                    catch (Exception e)
                    {
                        textoValidacao +=  ("Linha: " + numLinha + " Erro no campo: Cod. compromisso = " + codCompromisso + " : preencher com o código do compromisso informado pelo Banco<br>");
                    }

                    if (paramTrans.Contains("  "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: param. de Transmissão = " + paramTrans + " : está em branco! preencher com o código informado pelo Banco<br>");
                    }

                    if (!filler15.Contains("      "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: filler 15 = " + filler15 + " : preencher com espaços<br>");
                    }

                    try
                    {
                        int.Parse(agContaCorrente);
                    }
                    catch (Exception e)
                    {
                        textoValidacao +=  ("Linha: " + numLinha + " Erro no campo: ag. conta corrente = " + agContaCorrente + " : preencher com a agência da conta<br>");
                    }

                    if (dvAgencia.Contains(" "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: DV. da agência = " + dvAgencia + " : está em branco! preencher com DV. da agência<br>");
                    }

                    try
                    {
                        long.Parse(numContaCorrente);
                    }
                    catch (Exception e)
                    {
                        textoValidacao +=  ("Linha: " + numLinha + " Erro no campo: num. da conta corrente = " + numContaCorrente + " : preencher com num. da conta corrente<br>");
                    }

                    if (DvContaCorrente.Contains(" "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: DV da Conta Corrente = " + DvContaCorrente + " : está em branco! preencher com DV da Conta Corrente<br>");
                    }

                    if (!digAgConta.Contains(" "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: dígito da Agência/Conta = " + digAgConta + " : preencher com espaços<br>");
                    }

                    if (nomeEmpresa.Contains("                              "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: Nome da Empresa = " + nomeEmpresa + " : está em branco!<br>");
                    }

                    Auxiliar aux = new Auxiliar();
                    aux.apagaCaracterEspecial(numLinha, msgAviso1);

                    if (logradouro.Contains("                              "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: Logradouro = " + logradouro + " : está em branco!<br>");
                    }

                    try
                    {
                        //int numLocalParse = int.Parse(numLocal.Trim());
                        //string.Format("%05d", int.Parse(numLocal.Trim())); // preenche com zero a esquerda
                        if (numLocal.Trim().Length < numLocal.Length)
                        {
                            textoValidacao +=  ("Linha: " + numLinha + " Erro no campo: Número no local = " + numLocal + " : preencher com o zero a esquerda<br>");
                        }
                    }
                    catch (Exception e)
                    {
                        textoValidacao +=  ("Linha: " + numLinha + " Erro no campo: Número no local = " + numLocal + " : preencher com o número no local<br>");
                    }

                    if (complemento.Contains("               "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Atenção no campo: Complemento = " + complemento + " : está em branco!<br>");
                    }

                    if (cidade.Contains("                    "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Atenção no campo: Cidade = " + cidade + " : está em branco!<br>");
                    }


                    try
                    {

                        //int.Parse(cep.Trim());
                        //string.Format("%05d", int.Parse(cep.Trim())); // preenche com zero a esquerda
                        if (cep.Trim().Length < cep.Length)
                        {
                            textoValidacao +=  ("Linha: " + numLinha + " Erro no campo: CEP = " + cep + " : preencher com o zero a esquerda<br>");
                        }
                    }
                    catch (Exception e)
                    {
                        textoValidacao +=  ("Linha: " + numLinha + " Erro no campo: CEP = " + cep + " : preencher com o CEP<br>");
                    }

                    //if (cep.Contains("     "))
                    //{
                    //    textoValidacao += ("Linha: " + numLinha + " Atenção no campo: Complemento do CEP = " + cep + " : está em branco!<br>");
                    //}

                    if (complementoCep.Contains("   "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Atenção no campo: Complemento do CEP = " + complementoCep + " : está em branco!<br>");
                    }

                    Auxiliar auxEstado = new Auxiliar();
                    auxEstado.validaEstado(numLinha, siglaEstado);

                    if (!usoFebraban.Contains("        "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: Uso exclusivo FEBRABAN = " + usoFebraban + " : preencher com espaços<br>");
                    }

                    if (!ocorrencias.Contains("          "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: Ocorrências = " + ocorrencias + " : preencher com espaços<br>");
                    }

                }
                catch (Exception erro)
                {
                    textoValidacao += ("Linha: " + numLinha + " não consegui ler a linha do registro 1 (HEADER LOTE) = " + linha + "Erro: " + erro);
                }
            }
        }
    }
}
