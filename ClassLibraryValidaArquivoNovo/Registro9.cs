using System;
using System.Collections.Generic;

using System.Text;

namespace ClassLibraryValidaArquivoCEF
{
    class Registro9
    {
        string textoValidacao;

        public string TextoValidacao
        {
            get { return textoValidacao; }
            set { textoValidacao = value; }
        }

        public void valida(string linha, int numLinha, string validacao, int quantLote)
        {
            if (linha.Length != 240)
            {
                validacao +=("Linha: " + numLinha + " Fora do Padrão: quantidade de colunas é difente de 240! esta linha possui: " + linha.Length + " colunas<br>");
            }
            else
            {
                try
                {
                    // Descrição do Registro “TRAILLER” de arquivo - “9”
                    string loteServico = linha.Substring(3, 4); //9.02 004 007 9(004) - Lote de serviço - será retornado conforme recebido
                    string usoFebraban = linha.Substring(8, 9); //9.04 009 017 X(009)- Uso exclusivo FEBRABAN - será retornado espaços.
                    string quantLotesArq = linha.Substring(17, 6); //9.05 018 023 9(006) - Quantidade de Lotes no Arquivo - será retornado conforme recebido.
                    string quantRegistrosArq = linha.Substring(23, 6); //9.06 024 029 9(006) - Quantidade de Registros do Arquivo - será retornado conforme recebido.
                    string quantContasConciliacao = linha.Substring(29, 6); //9.07 030 035 9(006) - Quantidade de Contas para Conciliação - será retornado zeros.
                    string usoFebraban2 = linha.Substring(35, 205); //9.08 036 240 X(205) - Uso exclusivo FEBRABAN - será retornado espaços.

                    try { int.Parse(loteServico); }
                    catch (Exception erro) { validacao +=("Linha: " + numLinha + " Erro no campo: Lote de serviço = " + loteServico + " : usar apenas números" + erro); }

                    if (!usoFebraban.Contains("         "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Uso FEBRABAN = " + usoFebraban + " : preencher com espaços!<br>");
                    }

                    try {
                            if (int.Parse(quantLotesArq) != quantLote)
                            {
                                validacao += ("Linha: " + numLinha + " Erro no campo: Quantidade de Lotes no Arquivo = " + quantLotesArq + " : deveria ser: " + quantLote + "<br>");
                            }
                        }
                    catch (Exception erro) { validacao +=("Linha: " + numLinha + " Erro no campo: Lote de serviço = " + quantLotesArq + " : usar apenas números" + erro); }

                    try {
                            if (int.Parse(quantRegistrosArq) != numLinha)
                            {
                                validacao += ("Linha: " + numLinha + " Erro no campo: Quantidade de Registros do Arquivo = " + quantRegistrosArq + " : deveria ser: " + numLinha + "<br>");
                            }
                    }
                    catch (Exception erro) { validacao +=("Linha: " + numLinha + " Erro no campo: Lote de serviço = " + quantRegistrosArq + " : usar apenas números" + erro); }

                    try { int.Parse(quantContasConciliacao); }
                    catch (Exception erro) { validacao +=("Linha: " + numLinha + " Erro no campo: Lote de serviço = " + quantContasConciliacao + " : usar apenas números" + erro); }

                    if (!usoFebraban2.Contains("         "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Uso FEBRABAN = " + usoFebraban2 + " : preencher com espaços!<br>");
                    }

                }
                catch (Exception erro)
                {
                    validacao +=("Linha: " + numLinha + " não consegui ler a linha do registro 5 = " + linha + "<br>" + erro);
                }
            }
            textoValidacao = validacao;
        }
    }
}
