using System;
using System.Collections.Generic;
using System.Globalization;

using System.Text;

namespace ClassLibraryValidaArquivoCEF
{
    class RegistroB
    {
        string textoValidacao = string.Empty;

        public string TextoValidacao
        {
            get { return textoValidacao; }
            set { textoValidacao = value; }
        }

        internal void validaRegistroB(string linha, int numLinha)
        {
            if (linha.Length != 240)
            {
                textoValidacao += ("Linha: " + numLinha + " Fora do Padrão: quantidade de colunas é difente de 240! esta linha possui: " + linha.Length + " colunas<br>");
            }
            else
            {
                try
                {
                    string usoFebraban = linha.Substring(14, 3); // B.06 015 017 X(003) Uso FEBRABAN - preencher com espaços
                    string tipoInscricao = linha.Substring(17, 1); // B.07 018 018 9(001) Tipo Inscrição - “1” = quando CPF (pessoa física) “2” = quando CNPJ (pessoa jurídica)
                    string numInscricao = linha.Substring(18, 14); // B.08 019 032 9(014) Número Inscrição - preencher com o número do CPF quando no campo B.07 for preenchido com “1” ou o número do CNPJ quando no campo B.07 for preenchido com “2”
                    string logradouro = linha.Substring(32, 30); // B.09 033 062 X(030) Logradouro - preencher com o nome da Rua, Avenida, Alameda
                    string numLocal = linha.Substring(62, 5); // B.10 063 067 9(005) Número no local - preencher com número do endereço
                    string complemento = linha.Substring(67, 15); // B.11 068 082 X(015) Complemento - preencher com o complemento do endereço
                    string bairro = linha.Substring(82, 15); // B.12 083 097 X(015) Bairro - preencher com o bairro do endereço
                    string cidade = linha.Substring(97, 20); // B.13 098 117 X(020) Cidade - preencher com cidade do endereço
                    string cep = linha.Substring(117, 5); // B.14 118 122 9(005) CEP - Código de Endereçamento Postal
                    string complementoCep = linha.Substring(122, 3); // B.15 123 125 X(003) Complemento CEP - complemento do Código de Endereçamento Postal
                    string siglaEstado = linha.Substring(125, 2); // B.16 126 127 X(002) UF do Estado
                    string dataVencimento = linha.Substring(127, 8); // B.17 128 135 9(008) Data do Vencimento
                    string valorDocumento = linha.Substring(135, 15); // B.18 136 150 9(015) V99 Valor do Documento - preencher com zeros
                    string valorAbatimento = linha.Substring(150, 15); // B.19 151 165 9(015) V99 Valor do Abatimento - preencher com zeros
                    string valorDesconto = linha.Substring(165, 15); // B.20 166 180 9(015) V99 Valor do Desconto - preencher com zeros
                    string valorMora = linha.Substring(180, 15); // B.21 181 195 9(015) V99 Valor da Mora - preencher com zeros
                    string valorMulta = linha.Substring(195, 15); // B.22 196 210 9(015) V99 Valor da Multa - preencher com zeros
                    string codDocfavorecido = linha.Substring(210, 15); // B.23 211 225 X(015) Código Documento Favorecido - preencher com espaços. Quando o campo 1.06 for “10” – OP, preencher com o nº do favorecido. Nos demais casos, preencher com espaços
                    string usoFebraban2 = linha.Substring(225, 15); // B.24 226 240 X(015) Uso da FEBRABAN - preencher com espaço

                    if (!usoFebraban.Contains("   "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: Uso FEBRABAN = " + usoFebraban + " : preencher com espaços!<br>");
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
                    catch (Exception e)
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: Num. Inscricao = " + numInscricao + " : preencher com CPF ou CNPJ<br>");
                    }


                    if (logradouro.Contains("                              "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: Logradouro = " + logradouro + " : está em branco!<br>");
                    }

                    try
                    {
                        if (numLocal.Trim().Length < numLocal.Length)
                        {
                            if (numLocal.Trim().Length < 1)
                            {
                                textoValidacao += ("Linha: " + numLinha + " Erro no campo: Número no local = " + numLocal + " : favor preencher - Exemplo: 00007 <br>");
                            }
                            textoValidacao += ("Linha: " + numLinha + " Erro no campo: Número no local = " + numLocal + " : preencher com o zero a esquerda<br>");
                        }
                    }
                    catch (Exception e)
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: Número no local = " + numLocal + " : preencher com o número no local<br>");
                    }

                    if (complemento.Contains("               "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Atenção no campo: Complemento = " + complemento + " : está em branco!<br>");
                    }

                    if (bairro.Contains("               "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: Bairro = " + logradouro + " : está em branco!<br>");
                    }

                    if (cidade.Contains("                    "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Atenção no campo: Cidade = " + cidade + " : está em branco!<br>");
                    }
                    
                    if (cep.Trim().Length < cep.Length)
                    {
                        if (cep.Trim().Length < 1)
                        {
                            textoValidacao += ("Linha: " + numLinha + " Erro no campo: CEP = " + cep + " : preencher com o zero a esquerda<br>");
                        }
                        else textoValidacao += ("Linha: " + numLinha + " Erro no campo: CEP = " + cep + " : preencher com o zero a esquerda<br>");
                    }

                    if (cep.Contains("   "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Atenção no campo: Complemento do CEP = " + cep + " : está em branco!<br>");
                    }

                    if (complementoCep.Trim().Length < complementoCep.Length)
                    {
                        if (complementoCep.Trim().Length < 1)
                        {
                            textoValidacao +=  ("Linha: " + numLinha + " Erro no campo: CEP = " + complementoCep + " : preencher com o zero a esquerda<br>");
                        }
                        else textoValidacao +=  ("Linha: " + numLinha + " Erro no campo: CEP = " + complementoCep + " : preencher com o zero a esquerda<br>");
                    }

                    if (complementoCep.Contains("   "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Atenção no campo: Complemento do CEP = " + complementoCep + " : está em branco!<br>");
                    }

                    Auxiliar auxEstado = new Auxiliar();
                    auxEstado.validaEstado(numLinha, siglaEstado);
                    textoValidacao +=  auxEstado.TextoValidacao;

                    try 
                    { 
                        DateTime dataVenc = DateTime.ParseExact(dataVencimento, "ddMMyyyy", CultureInfo.InvariantCulture);
                        if (dataVenc < (DateTime.Now.AddDays(-1)) )
                        {
                            textoValidacao +=  ("Linha: " + numLinha + " Atenção no campo: Data de Vencimento = " + dataVencimento + " : já está vencida!<br>");
                        }
                    }
                    catch (Exception erro) { textoValidacao += ("Linha: " + numLinha + " Erro no campo: Data de Vencimento = " + dataVencimento + " : preencher com a data de vencimento<br>"); }
                    //faz o resto logo!!

                    if (!valorDocumento.Contains("000000000000000"))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: Valor do Documento = " + valorDocumento + " : preencher com zeros!<br>");
                    }

                    if (!valorAbatimento.Contains("000000000000000"))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: Valor do Abatimento = " + valorAbatimento + " : preencher com zeros!<br>");
                    }

                    if (!valorDesconto.Contains("000000000000000"))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: Valor do Desconto = " + valorDesconto + " : preencher com zeros!<br>");
                    }

                    if (!valorMora.Contains("000000000000000"))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: Valor da Mora = " + valorMora + " : preencher com zeros!<br>");
                    }

                    if (!valorMulta.Contains("000000000000000"))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: Valor da Multa = " + valorMulta + " : preencher com zeros!<br>");
                    }

                    //faz essa função logo!!
                    // codDocfavorecido = linha.Substring(210, 15); // B.23 211 225 X(015) Código Documento Favorecido - preencher com espaços.
                    // Quando o campo 1.06 for “10” – OP, preencher com o nº do favorecido. Nos demais casos, preencher com espaços

                    if (!usoFebraban2.Contains("               "))
                    {
                        textoValidacao += ("Linha: " + numLinha + " Erro no campo: Uso FEBRABAN = " + usoFebraban + " : preencher com espaços!<br>");
                    }
                }
                catch (Exception erro)
                {
                    textoValidacao += ("Linha: " + numLinha + " não consegui ler a linha do registro J = " + linha + "<br>" + erro);
                }
            }
        }
    }
}
