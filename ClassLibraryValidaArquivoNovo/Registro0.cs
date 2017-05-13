using System;
using System.Collections.Generic;

using System.Text;

namespace ClassLibraryValidaArquivoCEF
{
    class Registro0
    {
        string textoValidacao;

        public string TextoValidacao
        {
            get { return textoValidacao; }
            set { textoValidacao = value; }
        }

        public void valida(string linha, int numLinha, string validacao)
        {
            if (linha.Length != 240)
            {
                validacao +=("Linha: " + numLinha + " Fora do Padrão: quantidade de colunas é difente de 240! esta linha possui: " + linha.Length + " colunas<br>");
            }
            else
            {
                try
                {
                    // Descrição do Registro “HEADER” de Arquivo - “0”
                    string loteServico = linha.Substring(3, 4); // 0.02 004 007 9(004) Lote de Serviço = “0000”
                    string filler4 = linha.Substring(8, 9); // 0.04 009 017 X(009) Filler = preencher com espaços
                    string tipoInscricao = linha.Substring(17, 1); // 0.05 018 018 9(001) Tipo de inscrição = “1” ,quando CPF (pessoa física) ou “2” ,quando CNPJ (pessoa jurídica)
                    string numInscricao = linha.Substring(18, 14); // 0.06 019 032 9(014) Número de inscrição = preencher com o número do CPF quando no campo 0.05 for preenchido com “1” ou o número do CNPJ quando no campo 0.05 for preenchido com “2”.
                    string codConvenio = linha.Substring(32, 6); // 0.07 033 038 9(006) Código convênio no Banco = preencher com o código do convênio informado pelo Banco.
                    string paramTrans = linha.Substring(38, 2); // 0.08 039 040 9(002) Parâmetro de Transmissão = preencher com o código informado pelo Banco.
                    string ambienteCliente = linha.Substring(40, 1); // 0.09 041 041 X(001) Ambiente Cliente = “T” - teste ou “P” - produção
                    string ambienteCaixa = linha.Substring(41, 1); // 0.10 042 042 X(001) Ambiente CAIXA = preencher com espaço.

                    string origemAplicativo = linha.Substring(42, 3); // 0.11 043 045 X(003) Origem Aplicativo = preencher com espaços.
                    string numVersao = linha.Substring(45, 4); // 0.12 046 049 9(004) Número de Versão = preencher com zeros.
                    string filler13 = linha.Substring(49, 3); // 0.13 050 052 X(003) Filler = preencher com espaços.
                    string agContaCorrente = linha.Substring(52, 5); // 0.14 053 057 9(005) Agencia da conta corrente = preencher com código da agência detentora da conta corrente da empresa
                    string dvAg = linha.Substring(57, 1); // 0.15 058 058 9(001) DV da Agência = preencher com o digito verificador da agência detentora da conta corrente.
                    string numConta = linha.Substring(58, 12); // 0.16 059 070 9(012) Número da conta = De 059 a 062 - preencher com a operação da conta e De 063 a 070 - preencher com o número da conta corrente		
                    string dvConta = linha.Substring(70, 1); // 0.17 071 071 X(001) DV da conta = preencher com o dígito verificador da conta.		
                    string dvAgenciaConta = linha.Substring(71, 1); // 0.18 072 072 X(001) DV da agência/conta = preencher com espaço.
                    string nomeEmpresa = linha.Substring(72, 30); // 0.19 073 102 X(030) Nome da empresa
                    string nomeBanco = linha.Substring(102, 30); // 0.20 103 132 X(030) Nome do Banco = “CAIXA”		

                    string filler21 = linha.Substring(132, 10); // 0.21 133 142 X(010) Filler = preencher com espaços.		
                    string codRemessaRetorno = linha.Substring(142, 1); // 0.22 143 143 9(001) Código de remessa/retorno = “1”
                    string dataGeraArquivo = linha.Substring(143, 8); // 0.23 144 151 9(008) Data geração do arquivo = preencher com DDMMAAAA, esta data deverá ser sempre a do movimento ou de dias posteriores ao movimento, não sendo aceito data vencida, nem datas não úteis.
                    string horaGeraArquivo = linha.Substring(151, 6); // 0.24 152 157 9(006) Hora geração do arquivo = preencher com HHMMSS		
                    string Nsa = linha.Substring(157, 6); // 0.25 158 163 9(006) NSA = Numero Seqüencial do Arquivo - este número deverá ser seqüencial, evoluir de 1 em 1 no mínimo, para cada arquivo gerado e terá uma seqüência para o Banco e outro para a Empresa.		
                    string verLayout = linha.Substring(163, 3); // 0.26 164 166 9(003) Versão leiaute do arquivo = ”080”	
                    string densidadeGravacao = linha.Substring(166, 5); // 0.27 167 171 9(005) Densidade de gravação = “01600”		
                    string reservadoBanco = linha.Substring(171, 20); // 0.28 172 191 X(020) Reservado do Banco = preencher com espaços.		
                    string reservadoEmpresa = linha.Substring(191, 20); // 0.29 192 211 X(020) Reservado da Empresa = esta informação não será tratada pelo Banco.	
                    string usoFebraban = linha.Substring(211, 11); // 0.30 212 222 X(011) Uso exclusivo FEBRABAN = preencher com espaços.		

                    string idCobranca = linha.Substring(222, 3); // 0.31 223 225 X(003) Ident. Cobrança = preencher com espaços.	
                    string usoVan = linha.Substring(225, 3); // 0.32 226 228 9(003) Uso exclusivo das VAN = preencher com zeros.		
                    string tipoServico = linha.Substring(228, 2); // 0.33 229 230 X(002) Tipo de serviço = preencher com espaço.
                    string ocorenciaCob = linha.Substring(230, 10); // 0.34 231 240 X(010) Ocorrência Cob. Sem papel = preencher com espaços.

                    if (!loteServico.Contains("0000"))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Lote do Servico = " + loteServico + " : o pradão é 0000<br>");
                    }

                    if (!filler4.Contains("         "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: filler 4 = " + filler4 + " : preencher com espaços<br>");
                    }

                    switch (tipoInscricao)
                    {
                        case "1": break;
                        case "2": break;
                        default: validacao +=("Linha: " + numLinha + " Erro no campo: Num. Inscricao = " + tipoInscricao + " : preencher com 1 (pessoa física) ou 2 (pessoa jurídica)<br>");
                            break;
                    }

                    try
                    {
                        long.Parse(numInscricao);
                    }
                    catch (Exception e)
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Num. Inscricao = " + linha.Substring(18, 32) + " : preencher com CPF ou CNPJ<br>");
                    }

                    try
                    {
                        int.Parse(codConvenio);
                    }
                    catch (Exception e)
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Cod. Convênio = " + linha.Substring(32, 38) + " : preencher com o código do convênio informado pelo Banco<br>");
                    }

                    try
                    {
                        int.Parse(paramTrans);
                    }
                    catch (Exception e)
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Param. Trans. = " + linha.Substring(38, 40) + " : preencher com o código informado pelo Banco<br>");
                    }

                    switch (ambienteCliente)
                    {
                        case "T": break;
                        case "P": break;
                        default: validacao +=("Linha: " + numLinha + " Erro no campo: Ambiente Cliente = " + ambienteCliente + " : preencher com T(teste) ou P(produção)<br>");
                            break;
                    }


                    if (!ambienteCaixa.Contains(" "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Ambiente Caixa = " + ambienteCaixa + " : preencher com espaços<br>");
                    }

                    if (!origemAplicativo.Contains("   "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Origem Aplicativo = " + origemAplicativo + " : preencher com espaços<br>");
                    }

                    if (!numVersao.Contains("0000"))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Num. Versao = " + numVersao + " : preencher com zeros<br>");
                    }

                    if (!filler13.Contains("   "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: filler 13 = " + filler13 + " : preencher com espaços<br>");
                    }

                    try
                    {
                        int.Parse(agContaCorrente);
                    }
                    catch (Exception e)
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Ag. Conta Corrente = " + linha.Substring(52, 57) + " : preencher com o num. da Agência da Conta Corrente<br>");
                    }

                    try
                    {
                        int.Parse(dvAg);
                    }
                    catch (Exception e)
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: DV. da Agencia = " + linha.Substring(57, 58) + " : preencher com o DV. da Agencia<br>");
                    }

                    try
                    {
                        int.Parse(numConta);
                    }
                    catch (Exception e)
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Num. da Conta = " + linha.Substring(58, 70) + " : preencher com o num. da Conta<br>");
                    }

                    if (dvConta.Contains(" "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: DV. da Conta = " + dvConta + " : está em branco!<br>");
                    }

                    if (!dvAgenciaConta.Contains(" "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: DV. Agencia da Conta = " + dvAgenciaConta + " : preencher com espaços<br>");
                    }

                    if (nomeEmpresa.Contains("                              "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Nome da Empresa = " + nomeEmpresa + " : está em branco!<br>");
                    }

                    if (nomeBanco.Equals("CAIXA", StringComparison.InvariantCultureIgnoreCase))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: Nome do Banco = " + nomeBanco + " : o pradão é CAIXA<br>");
                    }

                    if (!filler21.Contains("          "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: filler 21 = " + filler21 + " : preencher com espaços<br>");
                    }

                    if (!codRemessaRetorno.Contains("1"))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: cod. Remessa = " + codRemessaRetorno + " : o pradão é 1<br>");
                    }

                    try
                    {
                        int.Parse(dataGeraArquivo);
                    }
                    catch (Exception e)
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: data de geração do arquivo = " + linha.Substring(143, 151) + " : preencher com a data de geração do arquivo<br>");
                    }

                    try
                    {
                        int.Parse(horaGeraArquivo);
                    }
                    catch (Exception e)
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: hora de geração do arquivo = " + linha.Substring(151, 157) + " : preencher com a hora de geração do arquivo<br>");
                    }

                    try
                    {
                        if (Nsa.Equals("000000"))
                        {
                            validacao +=("Linha: " + numLinha + " Erro no campo: Nsa = " + linha.Substring(157, 163) + " : Nsa deve ser maior que 0<br>");
                        }
                        else
                        {
                            int.Parse(Nsa);
                        }

                    }
                    catch (Exception e)
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: hora de geração do arquivo = " + linha.Substring(157, 163) + " : preencher com a hora de geração do arquivo<br>");
                    }

                    if (!verLayout.Contains("080"))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: ver. do Layout = " + verLayout + " : o pradão é 080<br>");
                    }

                    if (!densidadeGravacao.Contains("01600"))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: densidade de gravacao = " + densidadeGravacao + " : o pradão é 01600<br>");
                    }

                    if (!reservadoBanco.Contains("                    "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: reservado ao Banco = " + reservadoBanco + " : preencher com espaços<br>");
                    }

                    if (!reservadoEmpresa.Contains("                    "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: reservado a Empresa = " + reservadoEmpresa + " : preencher com espaços<br>");
                    }

                    if (!usoFebraban.Contains("           "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: reservado ao uso da Febraban = " + usoFebraban + " : preencher com espaços<br>");
                    }

                    if (!idCobranca.Contains("   "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: id de cobranca = " + idCobranca + " : preencher com espaços<br>");
                    }

                    if (!usoVan.Contains("000"))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: reservado ao uso da van = " + usoVan + " : o padrão é 000<br>");
                    }

                    if (!tipoServico.Contains("  "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: tipo de servico = " + tipoServico + " : preencher com espaços<br>");
                    }

                    if (!ocorenciaCob.Contains("          "))
                    {
                        validacao +=("Linha: " + numLinha + " Erro no campo: ocorencia de Cobrança = " + ocorenciaCob + " : preencher com espaços<br>");
                    }

                }
                catch (Exception e)
                {
                    validacao +=("Linha: " + numLinha + " não consegui ler a linha do registro 0 (HEADER REMESSA) = " + linha);
                }
            }

            textoValidacao = validacao;
        }
    }
}
