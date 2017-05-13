using System;
using System.Collections.Generic;
using System.IO;

using System.Text;

namespace ClassLibraryValidaArquivoCEF
{
    public class ValidaArquivoCAIXA
    {
        public String lerRemessaCnab240( Stream arquivo)
        {
            string linha, linhaHeaderRemessa = "", linhaTraillerRemessa = "", linhaHeaderLote = "", linhaTraillerLote = "", mensagemValidacao = "";
            int contadorLinhas = 0, contadorHeaderRemessa = 0, contadorTraillerRemessa = 0, contadorHeaderLote = 0, contadorTraillerLote = 0, quantPagamento = 0;
            int numNsr = 1, numLote = 1, campoQuantRegLote = 0, quantRegLote = 2, quantLote = 0;
            long somaValorPagamento = 0, campoSomaValorPagamento = 0, campoSomaQuantMoeda = 0, somaQuantMoeda = 0;

            try
            {
                // construtor que recebe o objeto do tipo arquivo
                StreamReader sr = new StreamReader(arquivo);

                // emquanto houver linhas
                while (((linha = sr.ReadLine()) != null))
                {
                    contadorLinhas++;
                    if (linha.Length > 7)
                    {
                        string codBanco = linha.Substring(0, 3);
                        if (!codBanco.Contains("104"))
                        {
                            mensagemValidacao +=("Erro no campo: Código do Banco = " + codBanco + " : código pradão é 104<br>");
                        }

                        string codRegistro = linha.Substring(7, 1);
                        switch (codRegistro)
                        {
                            case "0":
                                linhaHeaderRemessa = linha;                                
                                //mensagemValidacao += ("Linha: " + contadorLinhas + ": Validando registro do Tipo: 0 (Header Remessa)...<br>");
                                Registro0 validaReg0 = new Registro0( );
                                validaReg0.valida(linha, contadorLinhas, mensagemValidacao);
                                mensagemValidacao = validaReg0.TextoValidacao;
                                contadorHeaderRemessa++;

                                mensagemValidacao += ("Nome da Empresa: " + linhaHeaderRemessa.Substring(72, 30) + "<br>");
                                if (linha.Substring(17, 1).Contains("1"))
                                {
                                    mensagemValidacao += ("CPF: " + linhaHeaderRemessa.Substring(18, 14) + "<br>");
                                }
                                if (linha.Substring(17, 1).Contains("2"))
                                {
                                    mensagemValidacao += ("CNPJ: " + linhaHeaderRemessa.Substring(18, 14) + "<br>");
                                }
                                mensagemValidacao += ("Convênio: " + linhaHeaderRemessa.Substring(32, 6) + "<br>NSA: " + linhaHeaderRemessa.Substring(157, 6) + "<br><br>");
                                break;

                            case "1":
                                linhaHeaderLote = linha;
                                //mensagemValidacao +=("Linha: " + contadorLinhas + ": Validando registro do Tipo: 1 (Header Lote)...<br>");

                                Auxiliar auxLoteHeader = new Auxiliar();
                                auxLoteHeader.validaNumeroLotes(contadorLinhas, linha, numLote);
                                mensagemValidacao += auxLoteHeader.TextoValidacao;

                                Registro1 validaReg1 = new Registro1();
                                validaReg1.valida(linha, contadorLinhas);
                                mensagemValidacao += validaReg1.TextoValidacao;
                                contadorHeaderLote++;                                
                                break;

                            case "3":
                                if (linha.Substring(13, 1).Equals("A", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    //mensagemValidacao +=("Linha: " + contadorLinhas + ": Validando registro do Tipo: A...<br>");

                                    Auxiliar auxLoteSeg = new Auxiliar();
                                    auxLoteSeg.validaNumeroLotes(contadorLinhas, linha, numLote);
                                    auxLoteSeg.validaNsr(contadorLinhas, linha, numNsr);
                                    mensagemValidacao += auxLoteSeg.TextoValidacao;

                                    quantRegLote++;
                                    quantPagamento++;
                                    somaValorPagamento += long.Parse(linha.Substring(119, 15));
                                    somaQuantMoeda += long.Parse(linha.Substring(114, 10));

                                    RegistroA regA = new RegistroA();
                                    regA.validaRegistroA(linha, contadorLinhas);
                                    mensagemValidacao += regA.TextoValidacao;
                                }
                                if (linha.Substring(13, 1).Equals("B", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    //mensagemValidacao +=("Linha: " + contadorLinhas + ": Validando registro do Tipo: B...<br>");

                                    Auxiliar auxLoteSeg = new Auxiliar();
                                    auxLoteSeg.validaNumeroLotes(contadorLinhas, linha, numLote);
                                    auxLoteSeg.validaNsr(contadorLinhas, linha, numNsr);
                                    mensagemValidacao += auxLoteSeg.TextoValidacao;

                                    quantRegLote++;
                                    RegistroB regB = new RegistroB();
                                    regB.validaRegistroB(linha, contadorLinhas);
                                    mensagemValidacao += regB.TextoValidacao;
                                }
                                if (linha.Substring(13, 1).Equals("J", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    //mensagemValidacao +=("Linha: " + contadorLinhas + ": Validando registro do Tipo: J...<br>");

                                    Auxiliar auxLoteSeg = new Auxiliar();
                                    auxLoteSeg.validaNumeroLotes(contadorLinhas, linha, numLote);
                                    auxLoteSeg.validaNsr(contadorLinhas, linha, numNsr);
                                    mensagemValidacao += auxLoteSeg.TextoValidacao;

                                    quantRegLote++;
                                    quantPagamento++;
                                    somaValorPagamento += long.Parse(linha.Substring(152, 15));
                                    somaQuantMoeda += long.Parse(linha.Substring(167, 10));

                                    RegistroJ regJ = new RegistroJ();
                                    regJ.validaRegistroJ(linha, contadorLinhas);
                                    mensagemValidacao += regJ.TextoValidacao;
                                }
                                if (linha.Substring(13, 1).Equals("K", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    //mensagemValidacao +=("Linha: " + contadorLinhas + ": Validando registro do Tipo: K...<br>");

                                    Auxiliar auxLoteSeg = new Auxiliar();
                                    auxLoteSeg.validaNumeroLotes(contadorLinhas, linha, numLote);
                                    auxLoteSeg.validaNsr(contadorLinhas, linha, numNsr);
                                    mensagemValidacao += auxLoteSeg.TextoValidacao;

                                    quantRegLote++;
                                    quantPagamento++;
                                    somaValorPagamento += long.Parse(linha.Substring(119, 15));
                                    somaQuantMoeda = long.Parse(linha.Substring(114, 10));

                                    RegistroK regK = new RegistroK();
                                    regK.validaRegistroK(linha, contadorLinhas, mensagemValidacao);
                                    mensagemValidacao = regK.TextoValidacao;

                                }
                                numNsr++;
                                break;

                            case "5":
                                linhaTraillerLote = linha;
                                //mensagemValidacao +=("Linha: " + contadorLinhas + ": Validando registro do Tipo: 5 (Trailler Lote)...<br>");
                                try
                                {
                                    campoQuantRegLote = int.Parse(linha.Substring(17, 6));
                                    campoSomaValorPagamento = long.Parse(linha.Substring(23, 18));
                                    campoSomaQuantMoeda = long.Parse(linha.Substring(41, 15));

                                    if (campoQuantRegLote != quantRegLote)
                                    {
                                        mensagemValidacao +=("Erro no campo: Somatório de registros do lote: " + campoQuantRegLote + " A soma dos registros neste lote é: " + quantRegLote + "<br>");
                                    }

                                    if (campoSomaValorPagamento != somaValorPagamento)
                                    {
                                        Auxiliar aux = new Auxiliar();
                                        string campoSomaValorPag = aux.converteEmMoeda(contadorLinhas, linha, campoSomaValorPagamento.ToString());
                                        string somaValorPag = aux.converteEmMoeda(contadorLinhas, linha, somaValorPagamento.ToString());
                                        mensagemValidacao += ("Erro no campo: Somatório de valores: " + campoSomaValorPag + " A soma dos valores neste lote é: " + somaValorPag + "<br>");
                                    }

                                    if (campoSomaQuantMoeda != somaQuantMoeda)
                                    {
                                        mensagemValidacao += ("Erro no campo: Somatório de quantidade moeda: " + campoSomaQuantMoeda + " A soma de quantidade moeda neste lote é: " + somaQuantMoeda + "<br>");
                                    }
                                }
                                catch (Exception erro)
                                {
                                    mensagemValidacao += ("Erro na formatação do registro do Tipo: 5 (Trailler Lote)...<br>" + erro + "<br>");
                                }

                                Registro5 validaReg5 = new Registro5();
                                validaReg5.valida(linha, mensagemValidacao);
                                mensagemValidacao = validaReg5.TextoValidacao;

                                quantRegLote = 2;
                                somaQuantMoeda = 0;
                                somaValorPagamento = 0;

                                contadorTraillerLote++;
                                numNsr = 1;
                                numLote++;
                                quantLote++;
                                break;

                            case "9":
                                linhaTraillerRemessa = linha;
                                //mensagemValidacao +=("Linha: " + contadorLinhas + ": Validando registro do Tipo: 9 (Trailler Remessa)...<br>");

                                Registro9 validaReg9 = new Registro9();
                                validaReg9.valida(linha, contadorLinhas, mensagemValidacao, quantLote);
                                mensagemValidacao = validaReg9.TextoValidacao;

                                contadorTraillerRemessa++;
                                break;

                            default:
                                mensagemValidacao += ("Linha: " + contadorLinhas + ": Não foi possível identificar a linha: " + linha + "<br>");
                                break;
                        }
                    }
                    else { mensagemValidacao += ("Linha: " + contadorLinhas + ": Não foi possível identificar a linha: " + linha + "<br>"); }
                } // fecha o laço while

                sr.Close(); //fecha o StreamReader
                

            }
            catch (IOException ex)
            {
                mensagemValidacao += ("Ocorreu um erro ao ler o arquivo: " + ex + "<br>");
            }
            catch (FormatException ex)
            {
                mensagemValidacao += ("Ocorreu um erro ao ler o arquivo: " + ex + "<br>");
            }

            if (contadorHeaderRemessa - contadorTraillerRemessa == 0)
            {
                mensagemValidacao += ("<br>Quantidade de lotes: " + contadorHeaderLote + "<br>Quantidade de pagamentos: " + quantPagamento + "<br>Quantidade de linhas lidas: " + contadorLinhas + "<br><br>");
            }
            else
            {
                mensagemValidacao +=("<br>Problema com HEADER ou TRAILLER da remessa!<br>Quant. Header: " + contadorHeaderRemessa + "<br>Quant. Trailler: " + contadorTraillerRemessa + "<br><br>");
            }

            return mensagemValidacao;
        }

        public String lerRetornoCnab240( Stream arquivo)
        {
            string linha, linhaHeaderRetorno = "", linhaTraillerRetorno = "", linhaHeaderLote = "", linhaTraillerLote = "", mensagemValidacao = "";
            int contadorLinhas = 0, contadorHeaderRetorno = 0, contadorTraillerRetorno = 0, contadorHeaderLote = 0, contadorTraillerLote = 0, quantPagamento = 0;
            int numNsr = 1, numLote = 1, campoQuantRegLote = 0, quantRegLote = 2;
            long somaValorPagamento = 0, campoSomaValorPagamento = 0, campoSomaQuantMoeda = 0, somaQuantMoeda = 0;

            //StreamWriter sw = new StreamWriter(caminhoLogValidacao + "\\validacao.log", true, UTF8Encoding.UTF8);

            try
            {
                // construtor que recebe o objeto do tipo arquivo
                StreamReader sr = new StreamReader(arquivo);

                // emquanto houver linhas
                while (((linha = sr.ReadLine()) != null))
                {
                    contadorLinhas++;
                    if (linha.Length > 7)
                    {
                        string codBanco = linha.Substring(0, 3);
                        if (!codBanco.Contains("104"))
                        {
                            mensagemValidacao += ("Erro no campo: Código do Banco = " + codBanco + " : código pradão é 104<br>");
                        }

                        string codRegistro = linha.Substring(7, 1);
                        switch (codRegistro)
                        {
                            case "0":
                                linhaHeaderRetorno = linha;
                                //mensagemValidacao += ("Linha: " + contadorLinhas + ": Validando registro do Tipo: 0 (Header Retorno)...<br>");
                                contadorHeaderRetorno++;

                                mensagemValidacao += ("Nome da Empresa: " + linhaHeaderRetorno.Substring(72, 30) + "<br>");
                                if (linha.Substring(17, 1).Contains("1"))
                                {
                                    mensagemValidacao += ("CPF: " + linhaHeaderRetorno.Substring(18, 14) + "<br>");
                                }
                                if (linha.Substring(17, 1).Contains("2"))
                                {
                                    mensagemValidacao += ("CNPJ: " + linhaHeaderRetorno.Substring(18, 14) + "<br>");
                                }
                                mensagemValidacao += ("Convênio: " + linhaHeaderRetorno.Substring(32, 6) + "<br>NSA: " + linhaHeaderRetorno.Substring(157, 6) + "<br><br>");
                                break;

                            case "1":
                                linhaHeaderLote = linha;
                                //mensagemValidacao += ("Linha: " + contadorLinhas + ": Validando registro do Tipo: 1 (Header Lote)...<br>");

                                Auxiliar auxLoteHeader = new Auxiliar();
                                auxLoteHeader.validaNumeroLotes(contadorLinhas, linha, numLote);
                                mensagemValidacao += auxLoteHeader.TextoValidacao;

                                contadorHeaderLote++;
                                break;

                            case "3":

                                if (linha.Substring(13, 1).Equals("A", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    mensagemValidacao += ("Linha: " + contadorLinhas + ": Pagamento do tipo: A...<br>");

                                    Auxiliar aux = new Auxiliar();
                                    aux.validaNumeroLotes(contadorLinhas, linha, numLote);
                                    aux.validaNsr(contadorLinhas, linha, numNsr);
                                    mensagemValidacao += aux.TextoValidacao;

                                    quantRegLote++;
                                    quantPagamento++;
                                    somaValorPagamento += long.Parse(linha.Substring(119, 15));
                                    somaQuantMoeda += long.Parse(linha.Substring(114, 10));

                                    string codBancoDestino = linha.Substring(20, 3); // A.09 021 023 9(003) Código Banco de Destino
                                    string codAgenciaDestino = linha.Substring(23, 5); // A.10 024 028 9(005) Código da Agência de Destino
                                    string dvAgenciaDestino = linha.Substring(28, 1); // A.11 029 029 X(001) DV Agência de Destino
                                    string contaCorrenteDestino = linha.Substring(29, 12); // A.12 030 041 9(012) Conta Corrente Destino
                                    string dvContaDestino = linha.Substring(41, 1); // A.13 042 042 X(001) DV Conta Destino
                                    string dvAgenciaContaDestino = linha.Substring(42, 1); // A.14 043 043 X(001) DV Agência/Conta Destino
                                    string nomeFavorecido = linha.Substring(43, 30); // A.15 044 073 X(030) Nome do terceiro - favorecido
                                    string numDocAtribuidoEmpresa = linha.Substring(73, 6); // A.16 074 079 9(006) Número do Documento Atribuído pela Empresa
                                    string dataVencimento = linha.Substring(93, 8); // A.19 094 101 9(008) Data de vencimento
                                    string valorLancamento = linha.Substring(119, 15); // A.22 120 134 9(015) Valor do Lançamento
                                    string ocorrencias = linha.Substring(230, 10); // A.36 231 240 X(010) Ocorrências - preencher com espaços

                                    valorLancamento = aux.converteEmMoeda(contadorLinhas, linha, valorLancamento);
                                    ocorrencias = aux.TrataOcorencias(ocorrencias);
                                    mensagemValidacao += (" - Banco: " + codBancoDestino + " - Ag: " + codAgenciaDestino + "-" + dvAgenciaDestino + " - Cta: " + contaCorrenteDestino + "-" + dvAgenciaDestino + "-" + dvAgenciaContaDestino + "<br> - Favorecido: " + nomeFavorecido + "<br> - Numero do Documento: " + numDocAtribuidoEmpresa + " - Vencimento: " + dataVencimento + " - Valor: " + valorLancamento + "<br> - Ocorrências: " + ocorrencias + "<br>");
                                }

                                if (linha.Substring(13, 1).Equals("B", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    //mensagemValidacao +=("Linha: " + contadorLinhas + ": Pagamento do tipo: B...<br>");
                                    //Auxiliar auxLoteSeg = new Auxiliar();
                                    //auxLoteSeg.validaNumeroLotes(contadorLinhas, linha, numLote, mensagemValidacao);
                                    //auxLoteSeg.validaNsr(contadorLinhas, linha, numNsr, mensagemValidacao);
                                    quantRegLote++;
                                    //RegistroB regB = new RegistroB();
                                    //regB.validaRegistroB(linha, contadorLinhas, mensagemValidacao);
                                }

                                if (linha.Substring(13, 1).Equals("J", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    mensagemValidacao += ("Linha: " + contadorLinhas + ": Pagamento do tipo: J...<br>");

                                    Auxiliar aux = new Auxiliar();
                                    aux.validaNumeroLotes(contadorLinhas, linha, numLote);
                                    aux.validaNsr(contadorLinhas, linha, numNsr);
                                    mensagemValidacao += aux.TextoValidacao;

                                    quantRegLote++;
                                    quantPagamento++;
                                    somaValorPagamento += long.Parse(linha.Substring(152, 15));
                                    somaQuantMoeda += long.Parse(linha.Substring(167, 10));

                                    string bancoDestino = linha.Substring(17, 3); //J.08 018 020 9(003) Banco destino
                                    string nomeCedente = linha.Substring(61, 30); //J.14 062 091 X(030) Nome do Cedente
                                    string dataVencimento = linha.Substring(91, 8); //J.15 092 099 9(008) Data Vencimento
                                    string valorPagamento = linha.Substring(152, 15); //J.20 153 167 9(015)v99 Valor Pagamento
                                    string numDocumentoEmpresa = linha.Substring(182, 6); //J.22 183 188 X(006) Número documento atribuído pela empresa
                                    string ocorrencias = linha.Substring(230, 10); //J.28 231 240 X(010) Ocorrências do Retorno

                                    valorPagamento = aux.converteEmMoeda(contadorLinhas, linha, valorPagamento);
                                    ocorrencias = aux.TrataOcorencias(ocorrencias);
                                    mensagemValidacao += (" - Banco: " + bancoDestino + " - Cedente: " + nomeCedente + " - Numero do Documento: " + numDocumentoEmpresa + " - Vencimento: " + dataVencimento + " - Valor: " + valorPagamento + "<br> - Ocorrências: " + ocorrencias + "<br>");
                                }

                                if (linha.Substring(13, 1).Equals("K", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    mensagemValidacao += ("Linha: " + contadorLinhas + ": Pagamento do Tipo: K...<br>");

                                    Auxiliar aux = new Auxiliar();
                                    aux.validaNumeroLotes(contadorLinhas, linha, numLote);
                                    aux.validaNsr(contadorLinhas, linha, numNsr);
                                    mensagemValidacao += aux.TextoValidacao;

                                    quantRegLote++;
                                    quantPagamento++;
                                    somaValorPagamento += long.Parse(linha.Substring(119, 15));
                                    somaQuantMoeda = long.Parse(linha.Substring(114, 10));

                                    string codSegmentoEmpresa = linha.Substring(18, 1); //K. 019 019 9(001) Código Segmento Empresa
                                    string numDocEmpresa = linha.Substring(73, 6); //K. 074 079 X(006) Número do documento na Empresa
                                    string dataLancamento = linha.Substring(93, 8); //K. 094 101 9(008) Data Lançamento
                                    string valorLancamento = linha.Substring(119, 15); //K. 120 134 9(015) v99 Valor Lançamento
                                    string dataEfetivacao = linha.Substring(154, 8); //K. 155 162 9(008) Data da efetivação
                                    string ocorrencias = linha.Substring(230, 10); //K. 231 240 X(010) Ocorrências para retorno

                                    valorLancamento = aux.converteEmMoeda(contadorLinhas, linha, valorLancamento);
                                    ocorrencias = aux.TrataOcorencias(ocorrencias);
                                    codSegmentoEmpresa = aux.SegmentoEmpresa(codSegmentoEmpresa);
                                    mensagemValidacao += (" - Segmento empresa: " + codSegmentoEmpresa + " - Numero do Documento: " + numDocEmpresa + "<br> - Data: " + dataLancamento + " - Valor: " + valorLancamento + "<br> - Ocorrências: " + ocorrencias + "<br>");

                                }
                                numNsr++;
                                break;

                            case "5":
                                linhaTraillerLote = linha;
                                //mensagemValidacao += ("Linha: " + contadorLinhas + ": Validando registro do Tipo: 5 (Trailler Lote)...<br>");
                                try
                                {
                                    campoQuantRegLote = int.Parse(linha.Substring(17, 6));
                                    campoSomaValorPagamento = long.Parse(linha.Substring(23, 18));
                                    campoSomaQuantMoeda = long.Parse(linha.Substring(41, 15));

                                    if (campoQuantRegLote != quantRegLote)
                                    {
                                        mensagemValidacao += ("Linha: " + contadorLinhas + " Erro no campo: Somatório de registros do lote: " + campoQuantRegLote + " A soma dos registros neste lote é: " + quantRegLote + "<br>");
                                    }

                                    if (campoSomaValorPagamento != somaValorPagamento)
                                    {
                                        Auxiliar aux = new Auxiliar();
                                        string campoSomaValorPag = aux.converteEmMoeda(contadorLinhas, linha, campoSomaValorPagamento.ToString());
                                        string somaValorPag = aux.converteEmMoeda(contadorLinhas, linha, somaValorPagamento.ToString());
                                        mensagemValidacao += ("Linha: " + contadorLinhas + " Erro no campo: Somatório de valores: " + campoSomaValorPag + " A soma dos valores neste lote é: " + somaValorPag + "<br>");
                                    }

                                    if (campoSomaQuantMoeda != somaQuantMoeda)
                                    {
                                        mensagemValidacao += ("Linha: " + contadorLinhas + " Erro no campo: Somatório de quantidade moeda: " + campoSomaQuantMoeda + " A soma de quantidade moeda neste lote é: " + somaQuantMoeda + "<br>");
                                    }
                                }
                                catch (Exception erro)
                                {
                                    mensagemValidacao += ("Linha: " + contadorLinhas + " Erro na formatação do registro do Tipo: 5 (Trailler Lote)...<br>" + erro + "<br>");
                                }
                                quantRegLote = 2;
                                somaQuantMoeda = 0;
                                somaValorPagamento = 0;

                                contadorTraillerLote++;
                                numNsr = 1;
                                numLote++;
                                break;

                            case "9":
                                linhaTraillerRetorno = linha;
                                //mensagemValidacao += ("Linha: " + contadorLinhas + ": Validando registro do Tipo: 9 (Trailler Retorno)...<br>");
                                try
                                {
                                    int quantRegistrosArq = int.Parse(linhaTraillerRetorno.Substring(23, 6));
                                    if (contadorLinhas != quantRegistrosArq)
                                    {
                                        mensagemValidacao += ("Linha: " + contadorLinhas + " Erro no campo: Quantidade de Registros do Arquivo: " + quantRegistrosArq + " A soma de linhas neste arquivo é: " + contadorLinhas + "<br>");
                                    }
                                }
                                catch (Exception erro)
                                {
                                    mensagemValidacao += ("Linha: " + contadorLinhas + " Erro na formatação do registro do Tipo: 9 (Trailler Retorno)...<br>" + erro + "<br>");
                                }
                                contadorTraillerRetorno++;
                                break;

                            default:
                                mensagemValidacao += ("Linha: " + contadorLinhas + ": Não foi possível identificar a linha: " + linha + "<br>");
                                break;
                        }
                    }
                    else { mensagemValidacao += ("Linha: " + contadorLinhas + ": Não foi possível identificar a linha: " + linha + "<br>"); }
                } // fecha o laço while
                sr.Close(); //fecha o StreamReader
            }
            catch (IOException ex)
            {
                mensagemValidacao += ("Linha: " + contadorLinhas + " Ocorreu um erro ao ler o arquivo: " + ex + "<br>");
            }
            catch (FormatException ex)
            {
                mensagemValidacao += ("Linha: " + contadorLinhas + " Ocorreu um erro ao ler o arquivo: " + ex + "<br>");
            }

            if (contadorHeaderRetorno - contadorTraillerRetorno == 0)
            {
                mensagemValidacao += ("<br>Quantidade de lotes: " + contadorHeaderLote + "<br>Quantidade de pagamentos: " + quantPagamento + "<br>Quantidade de linhas lidas: " + contadorLinhas + "<br><br>");
            }
            else
            {
                mensagemValidacao += ("<br>Problema com HEADER ou TRAILLER da Retorno!<br>Quant. Header: " + contadorHeaderRetorno + "<br>Quant. Trailler: " + contadorTraillerRetorno + "<br><br>");
            } 
            return mensagemValidacao;
        }
    }
}
