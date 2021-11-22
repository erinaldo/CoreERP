using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace PS.Lib
{
    public class Global
    {
        private Data.DBS dbs;

        public Global()
        {
            dbs = new Data.DBS();
        }

        public enum TypeAutoinc
        {
            None,
            AutoInc,
            Max
        }

        public decimal Arredonda(decimal? Valor, int Casas)
        {
            if (Valor == null)
                return 0;
            else
                return Decimal.Round((decimal)Valor, (int)Casas, MidpointRounding.AwayFromZero);
        }

        public DataTable RetornaCamposComplementares(string Entidade)
        {
            string sSql = "SELECT * FROM GTABCAMPOCOMPL WHERE CODENTIDADE = ? AND ATIVO = 1 ORDER BY ORDEM";

            DataTable dt = new DataTable();

            dt = dbs.QuerySelect(sSql, Entidade);

            return dt;
        }

        public Filter BuscaFiltroPadraoVisao(string tabela, string[] chave, int codColigada)
        {
            Filter ft = new Filter();
            ft.codEmpresa = codColigada;
            ft.id = 0;
            ft.descricao = "Todos os Registros";
            ft.codUsuario = null;
            ft.tabela = null;
            ft.selecionado = 0;
            ft.padrao = 1;
            //ft.BuscaCondicao();

            return ft;
        }

        public DataTable NomeDosCampos(string tabela)
        {
            string sSql = "SELECT DESCRICAO, COLUNA, 1 CHECKED FROM GDICIONARIO WHERE TABELA = ? AND COLUNA <> '#' ORDER BY DESCRICAO";

            DataTable dt = new DataTable();

            dt = dbs.QuerySelect(sSql, tabela);

            return dt;
        }

        public DataTable NomeDosCamposFiltro(string tabela)
        {
            //usado na manuteção do filtro para ignorar a coluna CODEMPRESA
            string sSql = @"SELECT DESCRICAO, COLUNA, 1 CHECKED FROM GDICIONARIO WHERE TABELA = ? AND (COLUNA <> '#' AND ((? = 'GEMPRESA' AND COLUNA <> '#') OR (? <> 'GEMPRESA' AND COLUNA <> 'CODEMPRESA'))) ORDER BY DESCRICAO";

            DataTable dt = new DataTable();

            dt = dbs.QuerySelect(sSql, tabela, tabela, tabela);

            return dt;
        }

        public string NomeDoCampo(string tabela, string campo)
        {
            string sSql = "SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = ? AND COLUNA = ?";
            string retorno = "";

            retorno = dbs.QueryValue("", sSql, tabela, campo).ToString();

            return retorno;
        }

        public string NomeDaTabela(string tabela)
        {
            string sSql = "SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = ? AND COLUNA = '#'";
            string retorno = "";

            retorno = dbs.QueryValue("", sSql, tabela).ToString();

            return retorno;
        }

        public string MensagemDeValidacao(string tabela, string campo)
        {
            return string.Concat("Campo ", this.NomeDoCampo(tabela, campo), " deve ser informado.");
        }

        public DataField RetornaDataFieldByCampo(List<DataField> lista, string campo)
        {
            DataField dataField = new DataField();

            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Field == campo)
                {
                    return dataField = lista[i];
                }
            }

            return dataField;
        }

        public List<DataField> RetornaDataFieldByDataGridViewRow(System.Windows.Forms.DataGridViewRow dr)
        {
            List<DataField> ListaDataField = new List<DataField>();

            for (int i = 0; i < dr.Cells.Count; i++)
            {
                DataField dtf = new DataField();
                dtf.Field = dr.DataGridView.Columns[i].Name;
                dtf.Valor = dr.Cells[i].Value;

                ListaDataField.Add(dtf);
            }

            return ListaDataField;
        }

       

        public DataField RetornaDataFieldByFilter(List<PSFilter> DefaultFilter, string Campo)
        {
            DataField data = new DataField();

            if (DefaultFilter != null)
            {
                for (int i = 0; i < DefaultFilter.Count; i++)
                {
                    if (Campo == DefaultFilter[i].Field)
                    {
                        data.Field = DefaultFilter[i].Field;
                        data.Valor = DefaultFilter[i].Value;
                    }
                }
            }

            return data;
        }

        public List<DataField> RetornaDataFieldByDataRow(System.Data.DataRow dr)
        {
            List<DataField> ListaDataField = new List<DataField>();

            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                DataField dtf = new DataField();
                dtf.Field = dr.Table.Columns[i].ColumnName;
                dtf.Valor = dr.ItemArray[i];

                ListaDataField.Add(dtf);
            }
            
            return ListaDataField;
        }

        public DataField RetornaDataFieldByDataRow(System.Data.DataRow dr, string campo)
        {
            List<DataField> ListaDataField = new List<DataField>();

            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                DataField dtf = new DataField();
                dtf.Field = dr.Table.Columns[i].ColumnName;
                dtf.Valor = dr.ItemArray[i];

                ListaDataField.Add(dtf);
            }

            DataField dataField = new DataField();

            for (int i = 0; i < ListaDataField.Count; i++)
            {
                if (ListaDataField[i].Field == campo)
                {
                    return dataField = ListaDataField[i];
                }
            }

            return dataField;
        }

        public DataTable RetornaDataTablebyParameter(string table, string[] key, params object[] parameters)
        {
            string sSQL = string.Concat("SELECT 1 FROM ", table, " WITH(NOLOCK) WHERE ");
            object[] parArr = new object[key.Length];
            parArr = parameters;

            for (int i = 0; i < key.Length; i++)
            {
                if (i == 0)
                {
                    sSQL = string.Concat(sSQL, key[i], " =  ? ");
                }
                else
                {
                    sSQL = string.Concat(sSQL, " AND ", key[i], " = ? ");
                }
            }

            if (dbs.QueryFind(sSQL, parArr))
            {
                sSQL = string.Concat("SELECT * FROM ", table, " WITH(NOLOCK) WHERE ");

                for (int i = 0; i < key.Length; i++)
                {
                    if (i == 0)
                    {
                        sSQL = string.Concat(sSQL, key[i], " =  ? ");
                    }
                    else
                    {
                        sSQL = string.Concat(sSQL, " AND ", key[i], " = ? ");
                    }
                }

                DataTable dt = dbs.QuerySelect(sSQL, parArr);

                return dt;
            }

            return null;
        }

        public int GetIdNovoRegistro(int codEmpresa, string tabela)
        {
            string sSql = "";
            int id = 0;

            sSql = "SELECT IDLOG FROM GLOG WHERE CODEMPRESA = ? AND CODTABELA = ?";

            id = int.Parse(dbs.QueryValue(0, sSql, codEmpresa, tabela).ToString());

            if (id == 0)
            {
                id = id + 1;
                sSql = "INSERT INTO GLOG (CODEMPRESA, IDLOG, CODTABELA) VALUES (?,?,?)";
                dbs.QueryExec(sSql, codEmpresa, id, tabela);

            }
            else
            {
                id = id + 1;
                sSql = "UPDATE GLOG SET IDLOG = ? WHERE CODEMPRESA = ? AND CODTABELA = ?";
                dbs.QueryExec(sSql, id, codEmpresa, tabela);
            }

            return id;
        }

        public int GetIdNovoRegistro(int codEmpresa, string tabela, List<DataField> filter, String[] chaves)
        {
            List<DataField> objArr = new List<DataField>();
            string cBase = "";
            int tam = chaves.Length;
            int id = 0;

            cBase = chaves[tam - 1];

            for (int i = 0; i < filter.Count; i++)
            {
                objArr.Add(filter[i]);
            }

            for (int i = 0; i < objArr.Count; i++)
            {
                if (objArr[i].Field.ToString() == cBase)
                {
                    objArr.RemoveAt(i);
                }
            }

            string sSQL = string.Concat("SELECT ISNULL(MAX(", cBase, "),0) FROM ", tabela, " WHERE ");
            object[] parArr = new object[chaves.Length - 1];

            for (int i = 0; i < chaves.Length; i++)
            {
                if (chaves[i] != cBase)
                {
                    if (i == 0)
                    {
                        sSQL = string.Concat(sSQL, chaves[i], " =  ? ");
                    }
                    else
                    {
                        sSQL = string.Concat(sSQL, " AND ", chaves[i], " = ? ");
                    }

                    for (int j = 0; j < objArr.Count; j++)
                    {
                        if (objArr[j].Field.ToString() == chaves[i])
                        {
                            parArr[i] = objArr[j].Valor;
                        }
                    }
                }
            }

            id = int.Parse(dbs.QueryValue(0, sSQL, parArr).ToString());

            id++;

            return id;
        }

        public int SetImagem(byte[] imagem)
        {
            string sSql = "";
            int id = GetIdNovoRegistro(Contexto.Session.Empresa.CodEmpresa, "GIMAGEM");

            sSql = "INSERT INTO GIMAGEM (CODIMAGEM, IMAGEM) VALUES(?,?)";

            dbs.QueryExec(sSql, id, imagem);

            return id;
        }

        public System.Windows.Forms.BindingSource GetImagem(int idImagem)
        {
            string sSql = "";

            sSql = "SELECT IMAGEM FROM GIMAGEM WHERE CODIMAGEM = ?";

            System.Windows.Forms.BindingSource bds = new System.Windows.Forms.BindingSource();

            bds.DataSource = dbs.QuerySelect(sSql, idImagem);

            return bds;
        }

        public Filter RetornaFiltroSelecionado(List<Filter> filtros)
        {
            for (int i = 0; i < filtros.Count; i++)
            {
                if (filtros[i].selecionado == 1)
                {
                    return filtros[i];
                }
            }

            return null;
        }

        public int RetornaIndiceData(DataTable dt, List<DataField> dtf)
        {
            if (dtf != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (IsCurrentRow(dt.Rows[i], dtf))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public bool IsCurrentRow(DataRow dr, List<DataField> dtf)
        {
            bool Flag = false;

            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                for (int j = 0; j < dtf.Count; j++)
                {
                    if (dr.Table.Columns[i].ColumnName == dtf[j].Field)
                    {
                        if (dr.ItemArray[i].ToString() == dtf[j].Valor.ToString())
                        {
                            Flag = true;
                        }
                        else
                        {
                            Flag = false;
                            return Flag;
                        }
                    }
                }
            }

            return Flag;
        }

        public DataTable RetornaOperacaoUsuario()
        {
            string sSql = @"SELECT DISTINCT(GTIPOPER.CODTIPOPER), GTIPOPER.DESCRICAO 
                            FROM GTIPOPER, GPERFILTIPOPER, GUSUARIOPERFIL
                            WHERE GTIPOPER.CODEMPRESA = GPERFILTIPOPER.CODEMPRESA
                            AND GTIPOPER.CODTIPOPER = GPERFILTIPOPER.CODTIPOPER
                            AND GPERFILTIPOPER.CODEMPRESA = GUSUARIOPERFIL.CODEMPRESA
                            AND GPERFILTIPOPER.CODPERFIL = GUSUARIOPERFIL.CODPERFIL
                            AND GUSUARIOPERFIL.CODEMPRESA = ?
                            AND GUSUARIOPERFIL.CODUSUARIO = ?";

            DataTable dt = new DataTable();

            dt = dbs.QuerySelect(sSql, Contexto.Session.Empresa.CodEmpresa, Contexto.Session.CodUsuario);

            return dt;
        }

        public DataTable RetornaOperacaoUsuario(int TipOper)
        {
            //string sSql = @"SELECT DISTINCT(GTIPOPER.CODTIPOPER), GTIPOPER.DESCRICAO 
            //                FROM GTIPOPER, GPERFILTIPOPER, GUSUARIOPERFIL
            //                WHERE GTIPOPER.CODEMPRESA = GPERFILTIPOPER.CODEMPRESA
            //                AND GTIPOPER.CODTIPOPER = GPERFILTIPOPER.CODTIPOPER
            //                AND GPERFILTIPOPER.CODEMPRESA = GUSUARIOPERFIL.CODEMPRESA
            //                AND GPERFILTIPOPER.CODPERFIL = GUSUARIOPERFIL.CODPERFIL
            //                AND GUSUARIOPERFIL.CODEMPRESA = ?
            //                AND GUSUARIOPERFIL.CODUSUARIO = ?
            //                AND GTIPOPER.TIPOPER = ?";

            string sSql = @"SELECT DISTINCT T.CODTIPOPER, T.DESCRICAO
                            FROM GTIPOPER T
                            INNER JOIN GPERFILTIPOPER P
                            ON T.CODEMPRESA = P.CODEMPRESA AND T.CODTIPOPER = P.CODTIPOPER
                            WHERE T.CODEMPRESA = ? AND P.CODPERFIL = ? AND T.TIPOPER = ? AND P.CONSULTAR = 1";

            DataTable dt = new DataTable();

            dt = dbs.QuerySelect(sSql, Contexto.Session.Empresa.CodEmpresa, Contexto.Session.CodPerfil[0], TipOper);

            return dt;
        }

        public DataTable RetornaOperacaoFaturamento(string CodTipOper)
        {
            string sSql = @"SELECT DISTINCT(GTIPOPERFAT.CODTIPOPERFAT), GTIPOPER.DESCRICAO 
                            FROM GTIPOPER, GPERFILTIPOPER, GUSUARIOPERFIL, GTIPOPERFAT
                            WHERE GTIPOPER.CODEMPRESA = GPERFILTIPOPER.CODEMPRESA
                            AND GTIPOPER.CODTIPOPER = GPERFILTIPOPER.CODTIPOPER
                            AND GPERFILTIPOPER.CODEMPRESA = GUSUARIOPERFIL.CODEMPRESA
                            AND GPERFILTIPOPER.CODPERFIL = GUSUARIOPERFIL.CODPERFIL
                            AND GTIPOPER.CODEMPRESA = GTIPOPERFAT.CODEMPRESA
                            AND GTIPOPER.CODTIPOPER = GTIPOPERFAT.CODTIPOPERFAT
                            AND GUSUARIOPERFIL.CODEMPRESA = ?
                            AND GUSUARIOPERFIL.CODUSUARIO = ?
                            AND GTIPOPERFAT.CODTIPOPER = ?";

            DataTable dt = new DataTable();

            dt = dbs.QuerySelect(sSql, Contexto.Session.Empresa.CodEmpresa, Contexto.Session.CodUsuario, CodTipOper);

            return dt;
        }

        public object RetornaParametrosOperacao(string codTipOper, string campo)
        {
            string sSql = string.Concat("SELECT ", campo, " FROM GTIPOPER WHERE GTIPOPER.CODEMPRESA = ? AND GTIPOPER.CODTIPOPER = ?");

            DataTable dt = new DataTable();

            return dbs.QueryValue(null, sSql, Contexto.Session.Empresa.CodEmpresa, codTipOper);
        }

        public DataRow RetornaParametrosOperacao(string codTipOper)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM GTIPOPER WHERE GTIPOPER.CODEMPRESA = ? AND GTIPOPER.CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper });
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            return null;
            //return dbs.QuerySelect(sSql, Contexto.Session.Empresa.CodEmpresa, codTipOper).Rows[0];

        }

        public DataRow RetornaParametrosTipoDocumento(string codTipDoc)
        {
            string sSql = @"SELECT * FROM FTIPDOC WHERE FTIPDOC.CODEMPRESA = ? AND FTIPDOC.CODTIPDOC = ?";
            return dbs.QuerySelect(sSql, Contexto.Session.Empresa.CodEmpresa, codTipDoc).Rows[0];
        }

        public DataRow RetornaParametrosVarejo()
        {
            string sSql = @"SELECT * FROM VPARAMETROS WHERE VPARAMETROS.CODEMPRESA = ?";
            return dbs.QuerySelect(sSql, Contexto.Session.Empresa.CodEmpresa).Rows[0];
        }

        public bool ExisteCGCCPF(object cgccpf)
        {
            if (cgccpf.Equals("000.000.000-00")) { return false; }
            if (cgccpf.Equals("111.111.111-11")) { return false; }
            if (cgccpf.Equals("222.222.222-22")) { return false; }
            if (cgccpf.Equals("333.333.333-33")) { return false; }
            if (cgccpf.Equals("444.444.444-44")) { return false; }
            if (cgccpf.Equals("555.555.555-55")) { return false; }
            if (cgccpf.Equals("666.666.666-66")) { return false; }
            if (cgccpf.Equals("777.777.777-77")) { return false; }
            if (cgccpf.Equals("888.888.888-88")) { return false; }
            if (cgccpf.Equals("999.999.999-99")) { return false; }

            if (cgccpf.Equals("00.000.000/0000-00")) { return false; }

            string sSql = "SELECT CGCCPF FROM VCLIFOR WHERE CODEMPRESA = ? AND CGCCPF = ?";

            return dbs.QueryFind(sSql, Contexto.Session.Empresa.CodEmpresa, cgccpf);
        }

        public bool ExisteCGCCPF(object cgccpf, object codclifor)
        {
            if (cgccpf.Equals("000.000.000-00")) { return false; }
            if (cgccpf.Equals("111.111.111-11")) { return false; }
            if (cgccpf.Equals("222.222.222-22")) { return false; }
            if (cgccpf.Equals("333.333.333-33")) { return false; }
            if (cgccpf.Equals("444.444.444-44")) { return false; }
            if (cgccpf.Equals("555.555.555-55")) { return false; }
            if (cgccpf.Equals("666.666.666-66")) { return false; }
            if (cgccpf.Equals("777.777.777-77")) { return false; }
            if (cgccpf.Equals("888.888.888-88")) { return false; }
            if (cgccpf.Equals("999.999.999-99")) { return false; }

            if (cgccpf.Equals("00.000.000/0000-00")) { return false; }

            if (codclifor != null)
            {
                string sSql = "SELECT CGCCPF FROM VCLIFOR WHERE CODEMPRESA = ? AND CGCCPF = ? AND CODCLIFOR <> ?";

                return dbs.QueryFind(sSql, Contexto.Session.Empresa.CodEmpresa, cgccpf, codclifor);
            }
            else
            {
                string sSql = "SELECT CGCCPF FROM VCLIFOR WHERE CODEMPRESA = ? AND CGCCPF = ?";

                return dbs.QueryFind(sSql, Contexto.Session.Empresa.CodEmpresa, cgccpf);
            }
        }

        public DataTable BuscaAnexos(List<DataField> chave, string pspsart, int nSeq)
        {
            string sSql = "SELECT * FROM GANEXO WHERE CODPSPART = ? AND CODANEXO = ? AND NSEQ = ?";

            string strChave = string.Empty;

            for (int i = 0; i < chave.Count; i++)
            {
                if (i == (chave.Count - 1))
                {
                    strChave = string.Concat(strChave, chave[i].Valor);
                }
                else
                {
                    strChave = string.Concat(strChave, chave[i].Valor, ";");
                }
            }

            return dbs.QuerySelect(sSql, pspsart, strChave, nSeq);
        }

        public DataTable BuscaAnexos(List<DataField> chave, string pspsart)
        {
            string sSql = "SELECT * FROM GANEXO WHERE CODPSPART = ? AND CODANEXO = ?";

            string strChave = string.Empty;

            for (int i = 0; i < chave.Count; i++)
            {
                if (i == (chave.Count - 1))
                {
                    strChave = string.Concat(strChave, chave[i].Valor);
                }
                else
                {
                    strChave = string.Concat(strChave, chave[i].Valor, ";");
                }
            }

            return dbs.QuerySelect(sSql, pspsart, strChave);
        }

        public DataTable BuscaReferenciaClasse(string TypeName)
        {
            string sSql = "SELECT * FROM GPSPART WHERE CODPSPART = ?";

            return dbs.QuerySelect(sSql, TypeName);
        }

        public DataTable BuscaReferenciaClasse()
        {
            string sSql = "SELECT * FROM GPSPART";

            return dbs.QuerySelect(sSql);
        }

        public void MontaReferenciaClasse()
        {
            DataTable dt = BuscaReferenciaClasse();

            PS.Lib.Contexto.PSPartList.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PSInstance p = new PSInstance();
                p.AssemblyName = dt.Rows[i]["ASSEMBLYNAME"].ToString();
                p.NameSpace = dt.Rows[i]["NAMESPACE"].ToString();
                p.TypeName = dt.Rows[i]["CLASSNAME"].ToString();

                PS.Lib.Contexto.PSPartList.Add(p);
            }
        }

        public string RetornaCidade(string CodEtd, string CodCidade)
        {
            string sSql = "SELECT NOME FROM GCIDADE WHERE CODCIDADE = ? AND CODETD = ?";

            return dbs.QueryValue("", sSql, CodCidade, CodEtd).ToString();

        }

        public List<WebBrowser> CarregaWebBrowser()
        {
            string sSql = "SELECT * FROM GWEBBROWSER";

            List<WebBrowser> ListWB = new List<WebBrowser>();

            DataTable dt = dbs.QuerySelect(sSql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WebBrowser wb = new WebBrowser();

                wb.codigo = dt.Rows[i]["CODWEBBROWSER"].ToString();
                wb.descricao = dt.Rows[i]["DESCRICAO"].ToString();
                wb.url = dt.Rows[i]["URL"].ToString();

                ListWB.Add(wb);
            }

            return ListWB;
        }

        public void EnviarEmail(string EmailFrom, string EmailTo, string Subject, string Boby, bool IsHTML, MemoryStream[] Attachment)
        {
            try
            {
                System.Data.DataRow PARAMVAREJO = this.RetornaParametrosVarejo();
                if (PARAMVAREJO == null)
                {
                    throw new Exception("Não foi possível carregar os parâmetros do módulo.");
                }
                else
                {
                    string EmailHost = (PARAMVAREJO["EMAILHOST"] == DBNull.Value) ? null : PARAMVAREJO["EMAILHOST"].ToString();
                    int EmailPort = (PARAMVAREJO["EMAILPORTA"] == DBNull.Value) ? 0 : Convert.ToInt32(PARAMVAREJO["EMAILPORTA"]);
                    string EmailUser = (PARAMVAREJO["EMAILUSUARIO"] == DBNull.Value) ? null : PARAMVAREJO["EMAILUSUARIO"].ToString();
                    string EmailPass = (PARAMVAREJO["EMAILSENHA"] == DBNull.Value) ? null : PARAMVAREJO["EMAILSENHA"].ToString();
                    int EmailSSL = (PARAMVAREJO["EMAILUSASSL"] == DBNull.Value) ? 0 : Convert.ToInt32(PARAMVAREJO["EMAILUSASSL"]);
                    int EmailAut = (PARAMVAREJO["EMAILAUTENTICA"] == DBNull.Value) ? 0 : Convert.ToInt32(PARAMVAREJO["EMAILAUTENTICA"]);

                    if (string.IsNullOrEmpty(EmailHost) || EmailPort == 0 || string.IsNullOrEmpty(EmailUser) || string.IsNullOrEmpty(EmailPass))
                    {
                        throw new Exception("Não foi possível enviar o e-mail, verifique se os prâmentros do módulo estão preenchidos corretamente.");
                    }
                    else
                    {
                        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(EmailHost, EmailPort);
                        client.Timeout = Int32.MaxValue;
                        client.UseDefaultCredentials = false;

                        if (EmailAut == 1)
                        {
                            System.Net.NetworkCredential netcred = new System.Net.NetworkCredential(EmailUser, EmailPass);
                            client.Credentials = netcred;
                        }

                        if (EmailSSL == 1)
                            client.EnableSsl = true;
                        else
                            client.EnableSsl = false;

                        System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(EmailFrom, EmailTo, Subject, Boby);
                        message.IsBodyHtml = IsHTML;

                        if (Attachment != null)
                        {
                            foreach (MemoryStream men in Attachment)
                            {
                                men.Seek(0, System.IO.SeekOrigin.Begin);
                                System.Net.Mail.Attachment att = new System.Net.Mail.Attachment(men, "");
                                message.Attachments.Add(att);
                            }
                        }

                        client.Send(message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
