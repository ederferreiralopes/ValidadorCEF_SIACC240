using System;
using System.Text.RegularExpressions;

namespace ClassLibraryValidaArquivoCEF
{
    class Auxiliar
    {
        string textoValidacao = string.Empty;

        public string TextoValidacao
        {
            get { return textoValidacao; }
            set { textoValidacao = value; }
        }

        public void validaEstado(int numLinha, string siglaEstado)
        {
            if (!Properties.Settings.Default.siglaEstados.Contains(siglaEstado))
            {
                textoValidacao += ("Linha: " + numLinha + " Erro no campo: cod do Estado = " + siglaEstado + " : preencher com a sigla do Estado<br>");
            }
        }

        private string RemoverAcentos(string strTexto)
        {
            strTexto = strTexto.ToString();
            strTexto = Regex.Replace(strTexto, "[ÁÀÂÃ]", "A");
            strTexto = Regex.Replace(strTexto, "[ÉÈÊ]", "E");
            strTexto = Regex.Replace(strTexto, "[Í]", "I");
            strTexto = Regex.Replace(strTexto, "[ÓÒÔÕ]", "O");
            strTexto = Regex.Replace(strTexto, "[ÚÙÛÜ]", "U");
            strTexto = Regex.Replace(strTexto, "[Ç]", "C");
            strTexto = Regex.Replace(strTexto, "[áàâã]", "a");
            strTexto = Regex.Replace(strTexto, "[éèê]", "e");
            strTexto = Regex.Replace(strTexto, "[í]", "i");
            strTexto = Regex.Replace(strTexto, "[óòôõ]", "o");
            strTexto = Regex.Replace(strTexto, "[úùûü]", "u");
            strTexto = Regex.Replace(strTexto, "[ç]", "c");
            return strTexto;
        }

        public void apagaCaracterEspecial(int numLinha, string linhaAux)
        {
            string texto2 = linhaAux.Replace("[^aA-zZ-Z1-9 ]", "<br>"); // regex: /[^0-9A-Za-z]*/ 

            if (linhaAux.Length != texto2.Length)
            {
                textoValidacao += ("Linha: " + numLinha + " Erro no campo: mensagem de aviso 1 = " + linhaAux + " : possui caracter especial!<br>");
            }
        }

        public void validaNumeroLotes(int numLinha, string linha, int numLote)
        {
            try
            {
                if (int.Parse(linha.Substring(3, 4)) != numLote)
                {
                    textoValidacao += ("Linha: " + numLinha + " Erro no campo: Lote do servico = " + linha.Substring(3, 4) + " : deveria ser " + numLote + "<br>");
                }

            }
            catch (Exception erro)
            {
                textoValidacao += ("Linha: " + numLinha + " Erro no campo: Lote do servico = " + linha.Substring(3, 4) + " : deveria ser " + numLote + "<br>");
            }
        }

        public string converteEmMoeda(int numLinha, string linha, string valor)
        {
            try
            {
                valor = valor.Insert(valor.Length - 2, ",");
                Double valorLanc = Double.Parse(valor);
                valor = valorLanc.ToString("c");
            }
            catch (Exception erro)
            {
                textoValidacao += ("Linha: " + numLinha + " Erro no campo: Valor do lancamento = " + linha.Substring(119, 15) + "<br>");
            }

            return valor;
        }

        public void validaNsr(int numLinha, string linha, int numNsr)
        {
            try
            {
                if (int.Parse(linha.Substring(8, 5)) != numNsr)
                {
                    textoValidacao += ("Linha: " + numLinha + " Erro no campo: Num. sequencial do Registro = " + linha.Substring(8, 5) + " : deveria ser " + numNsr + "<br>");
                }
            }
            catch (Exception erro)
            {
                textoValidacao += ("Linha: " + numLinha + " Erro no campo: Num. sequencial do Registro = " + linha.Substring(8, 5) + " : deveria ser " + numNsr + "<br>");
            }
        }

        public void validaCodBanco(int numLinha, string codigo)
        {
            if (!Properties.Settings.Default.codigoBancos.Contains(codigo))
            {
                textoValidacao += ("Linha: " + numLinha + " Erro no campo: banco de destino = " + codigo + " : preencher com o código do banco de destino do bloqueto, conforme constante da 1a a 3a posições na barra da cobrança<br>");
            }
        }

        public string TrataOcorencias(string ocorrencias)
        {
            //TABELA G059 - Código das Ocorrências para Retorno. Pode-se informar até 5 ocorrências simultaneamente, cada uma delas codificada com dois dígitos
            string ocorrencias1 = ocorrencias.Substring(0, 2);
            string ocorrencias2 = ocorrencias.Substring(2, 2);
            string ocorrencias3 = ocorrencias.Substring(4, 2);
            string ocorrencias4 = ocorrencias.Substring(6, 2);
            string ocorrencias5 = ocorrencias.Substring(8, 2);

            string[] ocor = new string[5] { ocorrencias1, ocorrencias2, ocorrencias3, ocorrencias4, ocorrencias5 };

            if (ocorrencias.Length >= 2)
            {
                ocorrencias = string.Empty;

                foreach (var item in ocor)
                {
                    switch (item)
                    {
                        case "00": ocorrencias += " Crédito ou Débito Efetivado -> Este código indica que o pagamento foi confirmado<br>";
                            break;
                        case "01": ocorrencias += " Insuficiência de Fundos - Débito não efetuado<br>";
                            break;
                        case "02": ocorrencias += " Crédito ou Débito Cancelado pelo Pagador/Credor<br>";
                            break;
                        case "03": ocorrencias += " Débito Autorizado pela Agência - Efetuado<br>";
                            break;
                        case "HA": ocorrencias += " Lote não aceito<br>";
                            break;
                        case "HB": ocorrencias += " Inscrição da Empresa Inválida para o Contrato<br>";
                            break;
                        case "HC": ocorrencias += " Convênio com a Empresa Inexistente/Inválido para o Contrato<br>";
                            break;
                        case "HD": ocorrencias += " Agência/Conta Corrente da Empresa Inexistente/Inválido para o Contrato<br>";
                            break;
                        case "HE": ocorrencias += " Tipo de Serviço Inválido para o Contrato<br>";
                            break;
                        case "HF": ocorrencias += " Conta Corrente da Empresa com Saldo Insuficiente<br>";
                            break;
                        case "HG": ocorrencias += " Lote de Serviço fora de Seqüência<br>";
                            break;
                        case "HH": ocorrencias += " Lote de serviço inválido<br>";
                            break;
                        case "HI": ocorrencias += " Número da remessa inválido<br>";
                            break;
                        case "HJ": ocorrencias += " Arquivo sem HEADER<br>";
                            break;
                        case "HM": ocorrencias += " Versão do arquivo inválido<br>";
                            break;
                        case "AA": ocorrencias += " Controle inválido<br>";
                            break;
                        case "AB": ocorrencias += " Tipo de operação inválido<br>";
                            break;
                        case "AC": ocorrencias += " Tipo de serviço inválido<br>";
                            break;
                        case "AD": ocorrencias += " Forma de Lançamento inválida<br>";
                            break;
                        case "AE": ocorrencias += " Tipo/Número de inscrição inválido<br>";
                            break;
                        case "AF": ocorrencias += " Código de convênio inválido<br>";
                            break;
                        case "AG": ocorrencias += " Agência/Conta corrente/DV inválido<br>";
                            break;
                        case "AH": ocorrencias += " Número seqüencial do registro no lote inválido<br>";
                            break;
                        case "AI": ocorrencias += " Código de segmento de detalhe inválido<br>";
                            break;
                        case "AJ": ocorrencias += " Tipo de movimento inválido<br>";
                            break;
                        case "AK": ocorrencias += " Código da câmara de compensação do banco favorecido/depositário inválido<br>";
                            break;
                        case "AL": ocorrencias += " Código do banco favorecido ou depositário inválido<br>";
                            break;
                        case "AM": ocorrencias += " Agência mantenedora da conta corrente do favorecido inválida<br>";
                            break;
                        case "AN": ocorrencias += " Conta Corrente / DV do favorecido inválido<br>";
                            break;
                        case "AO": ocorrencias += " Nome do favorecido não informado<br>";
                            break;
                        case "AP": ocorrencias += " Data de lançamento inválido<br>";
                            break;
                        case "AQ": ocorrencias += " Tipo/quantidade de moeda inválida<br>";
                            break;
                        case "AR": ocorrencias += " Valor do lançamento inválido<br>";
                            break;
                        case "AS": ocorrencias += " Aviso ao favorecido - identificação inválida<br>";
                            break;
                        case "AT": ocorrencias += " Tipo/número de inscrição do favorecido inválido<br>";
                            break;
                        case "AU": ocorrencias += " Logradouro do favorecido não informado<br>";
                            break;
                        case "AV": ocorrencias += " Número do local do favorecido não informado<br>";
                            break;
                        case "AW": ocorrencias += " Cidade do favorecido não informada<br>";
                            break;
                        case "AX": ocorrencias += " CEP/complemento do favorecido inválido<br>";
                            break;
                        case "AY": ocorrencias += " Sigla do Estado do Favorecido Inválido<br>";
                            break;
                        case "AZ": ocorrencias += " Código/nome do banco depositário inválido<br>";
                            break;
                        case "BA": ocorrencias += " Código/nome da agência depositária não informado<br>";
                            break;
                        case "BB": ocorrencias += " Seu número inválido<br>";
                            break;
                        case "BC": ocorrencias += " Nosso número inválido<br>";
                            break;
                        case "BD": ocorrencias += " Inclusão efetuada com sucesso<br>";
                            break;
                        case "BE": ocorrencias += " Alteração efetuada com sucesso<br>";
                            break;
                        case "BF": ocorrencias += " Exclusão efetuada com sucesso<br>";
                            break;
                        case "BG": ocorrencias += " Agência/conta impedida legalmente<br>";
                            break;
                        case "CA": ocorrencias += " Código de barras - código do banco inválido<br>";
                            break;
                        case "CB": ocorrencias += " Código de barras - código da moeda inválida<br>";
                            break;
                        case "CC": ocorrencias += " Código de barras - dígito verificador geral inválido<br>";
                            break;
                        case "CD": ocorrencias += " Código de barras - valor do título inválido<br>";
                            break;
                        case "CE": ocorrencias += " Código de barras - campo livre inválido<br>";
                            break;
                        case "CF": ocorrencias += " Valor do documento inválido<br>";
                            break;
                        case "CG": ocorrencias += " Valor do abatimento inválido<br>";
                            break;
                        case "CH": ocorrencias += " Valor do desconto inválido<br>";
                            break;
                        case "CI": ocorrencias += " Valor de mora inválido<br>";
                            break;
                        case "CJ": ocorrencias += " Valor da multa inválido<br>";
                            break;
                        case "CK": ocorrencias += " Valor do IR inválido<br>";
                            break;
                        case "CL": ocorrencias += " Valor do ISS inválido<br>";
                            break;
                        case "CM": ocorrencias += " Valor do IOF inválido<br>";
                            break;
                        case "CN": ocorrencias += " Valor de outras deduções inválido<br>";
                            break;
                        case "CO": ocorrencias += " Valor de outros acréscimos inválido<br>";
                            break;
                        case "CP": ocorrencias += " Valor do INSS inválido<br>";
                            break;
                        case "CQ": ocorrencias += " Código de barras inválido<br>";
                            break;
                        case "TA": ocorrencias += " Lote não aceito - totais de lote com diferença<br>";
                            break;
                        case "TB": ocorrencias += " Lote sem trailler<br>";
                            break;
                        case "TC": ocorrencias += " Lote de Arquivo sem trailler<br>";
                            break;
                        case "YA": ocorrencias += " Título não encontrado<br>";
                            break;
                        case "YB": ocorrencias += " Identificador registro opcional inválido<br>";
                            break;
                        case "YC": ocorrencias += " Código padrão inválido<br>";
                            break;
                        case "YD": ocorrencias += " Código de ocorrência inválido<br>";
                            break;
                        case "YE": ocorrencias += " Complemento de ocorrência inválido<br>";
                            break;
                        case "YF": ocorrencias += " Alegação já informada<br>";
                            break;
                        case "ZA": ocorrencias += " Agência/conta do favorecido substituída<br>";
                            break;
                        default: ocorrencias += "";
                            break;
                    }
                }
            }
            else
            {
                ocorrencias = "Ocorrencia não informada pelo banco<br>";
            }
            return ocorrencias;
        }

        public string SegmentoEmpresa(string codSegmento)
        {
            switch (codSegmento)
            {
                case "1": codSegmento = "1 - Prefeituras";
                    break;
                case "2": codSegmento = "2 - Saneamento";
                    break;
                case "3": codSegmento = "3 - Energia";
                    break;
                case "4": codSegmento = "4 - Telefone";
                    break;
                case "5": codSegmento = "5 - Órgãos Governamentais";
                    break;
                case "6": codSegmento = "6 - Carnês e assemelhados";
                    break;
                case "7": codSegmento = "7 - Multas de Trânsito";
                    break;
                case "9": codSegmento = "9 - Exclusivo CAIXA";
                    break;
                default: codSegmento += "cod. segmento não encontrado: " + codSegmento;
                    break;
            }
            return codSegmento;
        }

    }
}
