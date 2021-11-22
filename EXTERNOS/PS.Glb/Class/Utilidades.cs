using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.Net.Mail;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Text.RegularExpressions;

namespace PS.Glb.Class
{
    public class Utilidades
    {
        #region Variáveis

        private decimal total = 0;

        #endregion

        #region Busca Dicionário

        public void getDicionario(Form form, Control control, string tabela)
        {

            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
            dt.PrimaryKey = new DataColumn[] { dt.Columns["COLUNA"] };
            Glb.Class.Utilidades util = new Class.Utilidades();

            #region TabControl
            TabControl tab = (TabControl)control;
            for (int iTab = 0; iTab < tab.TabPages.Count; iTab++)
            {
                foreach (Control item in tab.TabPages[iTab].Controls)
                {
                    if (item.GetType().Name == "GroupBox")
                    {
                        foreach (Control controleGroup in item.Controls)
                        {
                            switch (controleGroup.GetType().Name.ToString())
                            {
                                case "NewLookup":
                                    ITGProducao.Controles.NewLookup Newlookup = (ITGProducao.Controles.NewLookup)controleGroup;
                                    DataRow resultNewLookup = dt.Rows.Find(new object[] { tabela + "." + Newlookup.Titulo.ToString() });
                                    if (resultNewLookup != null)
                                    {
                                        Newlookup.Titulo = resultNewLookup["DESCRICAO"].ToString();
                                        toolTip1.SetToolTip(Newlookup, resultNewLookup["COLUNA"].ToString());
                                    }
                                    break;
                                case "PSTextoBox":
                                    PS.Lib.WinForms.PSTextoBox text = (PS.Lib.WinForms.PSTextoBox)controleGroup;
                                    DataRow resultText = dt.Rows.Find(new object[] { tabela + "." + text.DataField.ToString() });
                                    if (resultText != null)
                                    {
                                        text.Caption = resultText["DESCRICAO"].ToString();
                                        toolTip1.SetToolTip(text, resultText["COLUNA"].ToString());
                                    }
                                    break;
                                case "PSCheckBox":
                                    PS.Lib.WinForms.PSCheckBox check = (PS.Lib.WinForms.PSCheckBox)controleGroup;
                                    DataRow resultCheck = dt.Rows.Find(new object[] { tabela + "." + check.DataField.ToString() });
                                    if (resultCheck != null)
                                    {
                                        check.Caption = resultCheck["DESCRICAO"].ToString();
                                        toolTip1.SetToolTip(check, resultCheck["COLUNA"].ToString());
                                    }
                                    break;
                                case "PSComboBox":
                                    PS.Lib.WinForms.PSComboBox combo = (PS.Lib.WinForms.PSComboBox)controleGroup;
                                    DataRow resultCombo = dt.Rows.Find(new object[] { tabela + "." + combo.DataField.ToString() });
                                    if (resultCombo != null)
                                    {
                                        combo.Caption = resultCombo["DESCRICAO"].ToString();
                                        toolTip1.SetToolTip(combo, resultCombo["COLUNA"].ToString());
                                    }
                                    break;
                                case "PSDateBox":
                                    PS.Lib.WinForms.PSDateBox date = (PS.Lib.WinForms.PSDateBox)controleGroup;
                                    DataRow resultDate = dt.Rows.Find(new object[] { tabela + "." + date.DataField.ToString() });
                                    if (resultDate != null)
                                    {
                                        date.Caption = resultDate["DESCRICAO"].ToString();
                                        toolTip1.SetToolTip(date, resultDate["COLUNA"].ToString());
                                    }
                                    break;
                                case "PSLookup":
                                    PS.Lib.WinForms.PSLookup lookup = (PS.Lib.WinForms.PSLookup)controleGroup;
                                    DataRow resultLookup = dt.Rows.Find(new object[] { tabela + "." + lookup.DataField.ToString() });
                                    if (resultLookup != null)
                                    {
                                        lookup.Caption = resultLookup["DESCRICAO"].ToString();
                                        toolTip1.SetToolTip(lookup, resultLookup["COLUNA"].ToString());
                                    }
                                    break;
                                case "PSMaskedTextBox":
                                    PS.Lib.WinForms.PSMaskedTextBox masked = (PS.Lib.WinForms.PSMaskedTextBox)controleGroup;
                                    DataRow resultMasked = dt.Rows.Find(new object[] { tabela + "." + masked.DataField.ToString() });
                                    if (resultMasked != null)
                                    {
                                        masked.Caption = resultMasked["DESCRICAO"].ToString();
                                        toolTip1.SetToolTip(masked, resultMasked["COLUNA"].ToString());
                                    }
                                    break;
                                case "PSMemoBox":
                                    PS.Lib.WinForms.PSMemoBox memo = (PS.Lib.WinForms.PSMemoBox)controleGroup;
                                    DataRow resultMemo = dt.Rows.Find(new object[] { tabela + "." + memo.DataField.ToString() });
                                    if (resultMemo != null)
                                    {
                                        memo.Caption = resultMemo["DESCRICAO"].ToString();
                                        toolTip1.SetToolTip(memo, resultMemo["COLUNA"].ToString());
                                    }
                                    break;
                                case "PSMoedaBox":
                                    PS.Lib.WinForms.PSMoedaBox moeda = (PS.Lib.WinForms.PSMoedaBox)controleGroup;
                                    DataRow resultMoeda = dt.Rows.Find(new object[] { tabela + "." + moeda.DataField.ToString() });
                                    if (resultMoeda != null)
                                    {
                                        moeda.Caption = resultMoeda["DESCRICAO"].ToString();
                                        toolTip1.SetToolTip(moeda, resultMoeda["COLUNA"].ToString());
                                    }
                                    break;
                                case "LabelControl":

                                    DataRow resultLabel = dt.Rows.Find(new object[] { tabela + "." + controleGroup.Text });
                                    if (resultLabel != null)
                                    {
                                        controleGroup.Text = resultLabel["DESCRICAO"].ToString();
                                        toolTip1.SetToolTip(controleGroup, resultLabel["COLUNA"].ToString());
                                    }
                                    break;
                                case "CheckEdit":
                                    DataRow resultCheckEdit = dt.Rows.Find(new object[] { tabela + "." + controleGroup.Text });
                                    if (resultCheckEdit != null)
                                    {
                                        controleGroup.Text = resultCheckEdit["DESCRICAO"].ToString();
                                        toolTip1.SetToolTip(controleGroup, resultCheckEdit["COLUNA"].ToString());
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (item.GetType().Name.ToString())
                        {
                            case "PSTextoBox":
                                PS.Lib.WinForms.PSTextoBox text = (PS.Lib.WinForms.PSTextoBox)item;
                                DataRow resultText = dt.Rows.Find(new object[] { tabela + "." + text.DataField.ToString() });
                                if (resultText != null)
                                {
                                    text.Caption = resultText["DESCRICAO"].ToString();
                                    toolTip1.SetToolTip(text, resultText["COLUNA"].ToString());
                                }
                                break;
                            case "PSCheckBox":
                                PS.Lib.WinForms.PSCheckBox check = (PS.Lib.WinForms.PSCheckBox)item;
                                DataRow resultCheck = dt.Rows.Find(new object[] { tabela + "." + check.DataField.ToString() });
                                if (resultCheck != null)
                                {
                                    check.Caption = resultCheck["DESCRICAO"].ToString();
                                    toolTip1.SetToolTip(check, resultCheck["COLUNA"].ToString());
                                }
                                break;
                            case "PSComboBox":
                                PS.Lib.WinForms.PSComboBox combo = (PS.Lib.WinForms.PSComboBox)item;
                                DataRow resultCombo = dt.Rows.Find(new object[] { tabela + "." + combo.DataField.ToString() });
                                if (resultCombo != null)
                                {
                                    combo.Caption = resultCombo["DESCRICAO"].ToString();
                                    toolTip1.SetToolTip(combo, resultCombo["COLUNA"].ToString());
                                }
                                break;
                            case "PSDateBox":
                                PS.Lib.WinForms.PSDateBox date = (PS.Lib.WinForms.PSDateBox)item;
                                DataRow resultDate = dt.Rows.Find(new object[] { tabela + "." + date.DataField.ToString() });
                                if (resultDate != null)
                                {
                                    date.Caption = resultDate["DESCRICAO"].ToString();
                                    toolTip1.SetToolTip(date, resultDate["COLUNA"].ToString());
                                }
                                break;
                            case "PSLookup":
                                PS.Lib.WinForms.PSLookup lookup = (PS.Lib.WinForms.PSLookup)item;
                                DataRow resultLookup = dt.Rows.Find(new object[] { tabela + "." + lookup.DataField.ToString() });
                                if (resultLookup != null)
                                {
                                    lookup.Caption = resultLookup["DESCRICAO"].ToString();
                                    toolTip1.SetToolTip(lookup, resultLookup["COLUNA"].ToString());
                                }
                                break;
                            case "PSMaskedTextBox":
                                PS.Lib.WinForms.PSMaskedTextBox masked = (PS.Lib.WinForms.PSMaskedTextBox)item;
                                DataRow resultMasked = dt.Rows.Find(new object[] { tabela + "." + masked.DataField.ToString() });
                                if (resultMasked != null)
                                {
                                    masked.Caption = resultMasked["DESCRICAO"].ToString();
                                    toolTip1.SetToolTip(masked, resultMasked["COLUNA"].ToString());
                                }
                                break;
                            case "PSMemoBox":
                                PS.Lib.WinForms.PSMemoBox memo = (PS.Lib.WinForms.PSMemoBox)item;
                                DataRow resultMemo = dt.Rows.Find(new object[] { tabela + "." + memo.DataField.ToString() });
                                if (resultMemo != null)
                                {
                                    memo.Caption = resultMemo["DESCRICAO"].ToString();
                                    toolTip1.SetToolTip(memo, resultMemo["COLUNA"].ToString());
                                }
                                break;
                            case "PSMoedaBox":
                                PS.Lib.WinForms.PSMoedaBox moeda = (PS.Lib.WinForms.PSMoedaBox)item;
                                DataRow resultMoeda = dt.Rows.Find(new object[] { tabela + "." + moeda.DataField.ToString() });
                                if (resultMoeda != null)
                                {
                                    moeda.Caption = resultMoeda["DESCRICAO"].ToString();
                                    toolTip1.SetToolTip(moeda, resultMoeda["COLUNA"].ToString());
                                }
                                break;
                            case "LabelControl":

                                DataRow resultLabel = dt.Rows.Find(new object[] { tabela + "." + item.Text });
                                if (resultLabel != null)
                                {
                                    item.Text = resultLabel["DESCRICAO"].ToString();
                                    toolTip1.SetToolTip(item, resultLabel["COLUNA"].ToString());
                                }
                                break;
                            case "CheckEdit":
                                DataRow resultCheckEdit = dt.Rows.Find(new object[] { tabela + "." + item.Text });
                                if (resultCheckEdit != null)
                                {
                                    item.Text = resultCheckEdit["DESCRICAO"].ToString();
                                    toolTip1.SetToolTip(item, resultCheckEdit["COLUNA"].ToString());
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            #endregion
        }

        #endregion

        #region Busca Visão

        public string getVisao(string tabela, string relacionamento, List<string> tabelasFilhas, string where)
        {
            try
            {
                //Verifica se existe registro na tabela GVISAOUSUARIO
                int colunas = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(COLUNA) FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1", new object[] { tabela, AppLib.Context.Usuario }));
                if (colunas == 0)
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });
                    string tabelas = "'" + tabela + "'";
                    for (int i = 0; i < tabelasFilhas.Count; i++)
                    {
                        tabelas = tabelas + ", '" + tabelasFilhas[i].ToString() + "'";
                    }

                    DataTable db = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUMN_NAME, TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME IN ( " + tabelas + "  )", new object[] { });

                    for (int i = 0; i < db.Rows.Count; i++)
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO GVISAOUSUARIO (VISAO, CODUSUARIO, SEQUENCIA, COLUNA, LARGURA, VISIVEL, FIXO) VALUES (?, ?, ?, ?, ?, ?, ?)", new object[] { tabela, AppLib.Context.Usuario, i, (db.Rows[i]["TABLE_NAME"].ToString() + "." + db.Rows[i]["COLUMN_NAME"].ToString()), 100, 1, 0 });
                    }

                    if (tabela == "AAPONTAMENTO" && tabelasFilhas.Contains("AUNIDADE"))
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND COLUNA NOT IN (?,?) AND COLUNA LIKE 'AUNIDADE.%'", new object[] { "AAPONTAMENTO", AppLib.Context.Usuario, "AUNIDADE.IDUNIDADE", "AUNIDADE.NOME" });
                    }
                }
                DataTable dt = new DataTable();
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                string sql = string.Empty;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(sql))
                    {
                        sql = "SELECT " + dt.Rows[i]["COLUNA"].ToString() + " AS " + "'" + dt.Rows[i]["COLUNA"].ToString() + "'";
                    }
                    else
                    {
                        sql = sql + ", " + dt.Rows[i]["COLUNA"].ToString() + " AS " + "'" + dt.Rows[i]["COLUNA"].ToString() + "'";
                    }
                }
                sql = sql + " FROM " + tabela + " " + relacionamento + " " + where;

                return sql;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        #endregion

        #region Busca Filtro Usuário

        public string getFiltroUsuario(string tabela)
        {
            string filtroUsuario = string.Empty;
            DataTable dtFiltroUsuario = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM GFILTROUSUARIO WHERE CODUSUARIO = ? AND CODEMPRESA = ? AND TABELA = ?", new object[] { AppLib.Context.Usuario, AppLib.Context.Empresa, tabela });
            if (dtFiltroUsuario.Rows.Count > 0)
            {
                for (int i = 0; i < dtFiltroUsuario.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        filtroUsuario = dtFiltroUsuario.Rows[i]["CONDICAO"].ToString() + " " + dtFiltroUsuario.Rows[i]["TABELA"].ToString() + "." + dtFiltroUsuario.Rows[i]["COLUNA"].ToString() + " " + dtFiltroUsuario.Rows[i]["OPERADOR"].ToString() + " " + "'" + dtFiltroUsuario.Rows[i]["VALOR"].ToString() + "'";
                    }
                    else
                    {
                        filtroUsuario = filtroUsuario + " " + dtFiltroUsuario.Rows[i]["CONDICAO"].ToString() + " " + dtFiltroUsuario.Rows[i]["TABELA"].ToString() + "." + dtFiltroUsuario.Rows[i]["COLUNA"].ToString() + " " + dtFiltroUsuario.Rows[i]["OPERADOR"].ToString() + " " + dtFiltroUsuario.Rows[i]["VALOR"].ToString();
                    }
                }

            }
            return filtroUsuario;
        }

        #endregion

        #region INSERT
        /// <summary>
        /// Funcção de persistência de dados INSERT
        /// </summary>
        /// <param name="frm">O Formulário que deseja retirar os dados</param>
        /// <param name="ctrl">O Controle Ex. Painel</param>
        /// <param name="table">A Tabela do banco de dados</param>
        /// <returns>True OK, False </returns>
        public string Insert(Form frm, Control ctrl, string table)
        {
            try
            {
                string sql = "INSERT INTO " + table + " ( ";
                Control controle;
                //Monta a primeira parte do insert
                for (int i = 0; i < ctrl.Controls.Count; i++)
                {
                    controle = ctrl.Controls[i];
                    if (controle.GetType().Name.Equals("TextEdit") || controle.GetType().Name.Equals("ComboBox") || controle.GetType().Name.Equals("DateEdit"))
                    {
                        sql = sql + ctrl.Controls[i].Name + ", ";
                    }
                }
                int valor = sql.Length;
                sql = sql.Remove(valor - 2, 1);
                sql = sql + ") VALUES (";
                //Monta a segunda parte do insert
                for (int i = 0; i < ctrl.Controls.Count; i++)
                {
                    controle = ctrl.Controls[i];
                    if (controle.GetType().Name.Equals("TextEdit") || controle.GetType().Name.Equals("ComboBox") || controle.GetType().Name.Equals("DateEdit"))
                    {
                        sql = sql + "'" + ctrl.Controls[i].Text + "', ";
                    }
                    //else if (controle.GetType().Name.Equals("KryptonCheckBox"))
                    //{
                    //  //  KryptonCheckBox ck = (KryptonCheckBox)ctrl.Controls[i];
                    //    //if (ck.Checked)
                    //    //{
                    //    //    sql = sql + "'S', ";
                    //    //}
                    //    //else
                    //    //{
                    //    //    sql = sql + "'N', ";
                    //    //}
                    //}
                }
                valor = sql.Length;
                sql = sql.Remove(valor - 2, 1);
                sql = sql + ");";
                //  persist(sql);
                return sql;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        #endregion

        #region UPDATE
        /// <summary>
        /// Função de persistência de dados - UPDATE
        /// </summary>
        /// <param name="frm">O Formulário que deseja retirar os dados</param>
        /// <param name="ctrl">O Controle Ex. Painel</param>
        /// <param name="table">A Tabela do banco de dados</param>
        /// <param name="parametro">O Paramêtro do WHERE</param>
        /// <param name="valorParametro">Valor do Paramêtro</param>
        /// <returns></returns>
        public string update(Form frm, Control ctrl, string table)
        {
            try
            {
                string sql = "UPDATE " + table + " SET ";
                Control controle;
                //Monta a primeira parte do insert
                for (int i = 0; i < ctrl.Controls.Count; i++)
                {
                    controle = ctrl.Controls[i];

                    //if (sql.Contains(ctrl.Controls[i].Name.Remove(0, 4)))
                    //{
                    //    continue;
                    //}

                    if (controle.GetType().Name.Equals("TextEdit") || controle.GetType().Name.Equals("ComboBox") || controle.GetType().Name.Equals("DateEdit") || controle.GetType().Name.Equals("MemoEdit"))
                    {
                        if (controle.Name.Substring(0, 4) == "Num_")
                        {
                            sql = sql + ctrl.Controls[i].Name.Remove(0, 4) + " = " + "'" + ctrl.Controls[i].Text.Replace(",", ".") + "'" + ", ";
                        }
                        else
                        {
                            if (controle.GetType().Name.Equals("DateEdit"))
                            {

                                sql = sql + ctrl.Controls[i].Name.Remove(0, 4) + " = " + "'" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ctrl.Controls[i].Text)) + "'" + ", ";
                            }
                            else
                            {
                                if (sql.Contains(ctrl.Controls[i].Name.Remove(0, 4)))
                                {
                                    if (!table.Contains(ctrl.Controls[i].Name.Remove(0, 4)))
                                    {
                                        continue;
                                    }
                                }

                                sql = sql + ctrl.Controls[i].Name.Remove(0, 4) + " = " + "'" + ctrl.Controls[i].Text + "'" + ", ";
                            }
                        }


                    }
                    else if (controle.GetType().Name.Equals("CheckEdit"))
                    {
                        DevExpress.XtraEditors.CheckEdit ck = (DevExpress.XtraEditors.CheckEdit)ctrl.Controls[i];
                        if (ck.Checked)
                        {
                            sql = sql + ctrl.Controls[i].Name.Remove(0, 4) + " = '1', ";
                        }
                        else
                        {
                            sql = sql + ctrl.Controls[i].Name.Remove(0, 4) + " = '0', ";
                        }
                    }
                }
                //int valor;
                //valor = sql.Length;
                //sql = sql.Remove(valor - 2, 1);
                //sql = sql + " WHERE " + parametro + " =  " + "'" + valorParametro + "';";
                // persist(sql);
                return sql;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        #endregion

        #region Valida Campos Obrigatórios

        public bool validaCamposObrigatorios(Form frm, ref ErrorProvider errorProvider, object control = null, string codtipoper = null)
        {
            bool valida = false;

            if (!string.IsNullOrEmpty(frm.Tag.ToString()))
            {
                DataTable dtcampos = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM GCAMPOSOBRIGATORIO WHERE CODEMPRESA = ? AND TABELA = ? ", new object[] { AppLib.Context.Empresa, frm.Tag.ToString() });
                DataTable dtdicionario = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ? ", new object[] { frm.Tag.ToString() });

                if (dtcampos.Rows.Count > 0)
                {
                    valida = true;

                    errorProvider.Clear();

                    foreach (Control ctrl in frm.Controls)
                    {
                        validaCamposObrigatoriosRecursivo(ref valida, ref errorProvider, ctrl, dtcampos, dtdicionario, frm, codtipoper);
                    }
                }
                else
                {
                    valida = true;
                }
            }

            if (valida == false)
            {
                MessageBox.Show("Favor preencher os campos obrigatórios.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return valida;
        }

        #endregion

        #region Valida Campos Obrigatórios Recursivo

        public bool validaCamposObrigatoriosRecursivo(ref bool valida, ref ErrorProvider errorProvider, Control controle, DataTable dtcampos, DataTable dtdicionario, Form frm, string codtipoper = null)
        {
            foreach (Control ctrl in controle.Controls)
            {
                if ((object.ReferenceEquals(ctrl.GetType(), typeof(DevExpress.XtraEditors.TextEdit))))
                {
                    DevExpress.XtraEditors.TextEdit campo = (DevExpress.XtraEditors.TextEdit)ctrl;
                    #region TextEdit
                    if (campo.Tag == null)
                    {
                        continue;
                    }

                    DataRow[] rows = dtcampos.Select("COLUNA = '" + campo.Tag.ToString() + "'" + (string.IsNullOrEmpty(codtipoper) ? "" : " AND CODTIPOPER ='" + codtipoper + "'"));

                    if (rows.Count() > 0)
                    {
                        if (string.IsNullOrEmpty(campo.Text))
                        {
                            DataRow[] rowsDicionario = dtdicionario.Select("COLUNA = '" + frm.Tag.ToString() + "." + campo.Tag.ToString() + "'");
                            if (rowsDicionario.Count() > 0)
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'");
                                    }
                                }
                                else
                                {
                                    errorProvider.SetError(campo, "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'");
                                }
                            }
                            else
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Favor preencher o campo!");
                                    }
                                }
                                else
                                {
                                    errorProvider.SetError(campo, "Favor preencher o campo!");
                                }
                            }
                            valida = false;
                        }
                        else
                        {
                            if (!Convert.IsDBNull(rows[0]["INFNAOPERMITIDA"]))
                            {
                                if (campo.Text.Trim().ToUpper() == rows[0]["INFNAOPERMITIDA"].ToString().Trim().ToUpper())
                                {
                                    valida = false;
                                    errorProvider.SetError(campo, "Valor inválido!");
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(ComboBox))))
                {
                    ComboBox campo = (ComboBox)ctrl;
                    #region ComboBox
                    if (campo.Tag == null)
                    {
                        continue;
                    }

                    DataRow[] rows = dtcampos.Select("COLUNA = '" + campo.Tag.ToString() + "'" + (string.IsNullOrEmpty(codtipoper) ? "" : " AND CODTIPOPER ='" + codtipoper + "'"));

                    if (rows.Count() > 0)
                    {
                        if (campo.SelectedIndex == -1 || campo.Text == "")
                        {
                            DataRow[] rowsDicionario = dtdicionario.Select("COLUNA = '" + frm.Tag.ToString() + "." + campo.Tag.ToString() + "'");
                            if (rowsDicionario.Count() > 0)
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'");
                                    }
                                }
                                else
                                {
                                    errorProvider.SetError(campo, "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'");
                                }
                            }
                            else
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Favor preencher o campo!");
                                    }
                                }
                                else
                                {
                                    errorProvider.SetError(campo, "Favor preencher o campo!");
                                }
                            }
                            valida = false;
                        }
                        else
                        {
                            if (!Convert.IsDBNull(rows[0]["INFNAOPERMITIDA"]))
                            {
                                if (campo.SelectedValue.ToString().Trim().ToUpper() == rows[0]["INFNAOPERMITIDA"].ToString().Trim().ToUpper())
                                {
                                    valida = false;
                                    errorProvider.SetError(campo, "Valor inválido!");
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(PS.Lib.WinForms.PSLookup))))
                {
                    PS.Lib.WinForms.PSLookup campo = (PS.Lib.WinForms.PSLookup)ctrl;
                    #region PSLookup
                    if (campo.Tag == null)
                    {
                        continue;
                    }

                    DataRow[] rows = dtcampos.Select("COLUNA = '" + campo.Tag.ToString() + "'" + (string.IsNullOrEmpty(codtipoper) ? "" : " AND CODTIPOPER ='" + codtipoper + "'"));

                    if (rows.Count() > 0)
                    {
                        if (string.IsNullOrEmpty(campo.textBox1.Text))
                        {
                            DataRow[] rowsDicionario = dtdicionario.Select("COLUNA = '" + frm.Tag.ToString() + "." + campo.Tag.ToString() + "'");
                            if (rowsDicionario.Count() > 0)
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'");
                                    }
                                }
                                else
                                {
                                    errorProvider.SetError(campo, "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'");
                                }
                            }
                            else
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Favor preencher o campo!");
                                    }
                                }
                                else
                                {
                                    errorProvider.SetError(campo, "Favor preencher o campo!");
                                }
                            }
                            valida = false;
                        }
                        else
                        {
                            if (!Convert.IsDBNull(rows[0]["INFNAOPERMITIDA"]))
                            {
                                if (campo.textBox1.Text.Trim().ToUpper() == rows[0]["INFNAOPERMITIDA"].ToString().Trim().ToUpper())
                                {
                                    valida = false;
                                    errorProvider.SetError(campo, "Valor inválido!");
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(PS.Lib.WinForms.PSMoedaBox))))
                {
                    PS.Lib.WinForms.PSMoedaBox campo = (PS.Lib.WinForms.PSMoedaBox)ctrl;
                    #region PSMoedaBox
                    if (campo.Tag == null)
                    {
                        continue;
                    }

                    DataRow[] rows = dtcampos.Select("COLUNA = '" + campo.Tag.ToString() + "'" + (string.IsNullOrEmpty(codtipoper) ? "" : " AND CODTIPOPER ='" + codtipoper + "'"));

                    if (rows.Count() > 0)
                    {
                        if (string.IsNullOrEmpty(campo.textBox1.Text))
                        {
                            DataRow[] rowsDicionario = dtdicionario.Select("COLUNA = '" + frm.Tag.ToString() + "." + campo.Tag.ToString() + "'");
                            if (rowsDicionario.Count() > 0)
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'");
                                    }
                                }
                                else
                                {
                                    if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                    {
                                        if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                        {
                                            valida = false;
                                            errorProvider.SetError(campo, "Favor preencher o campo!");
                                        }
                                    }
                                    else
                                    {
                                        errorProvider.SetError(campo, "Favor preencher o campo!");
                                    }
                                }
                            }
                            else
                            {
                                errorProvider.SetError(campo, "Favor preencher o campo!");
                            }
                            valida = false;
                        }
                        else
                        {
                            if (!Convert.IsDBNull(rows[0]["INFNAOPERMITIDA"]))
                            {
                                if (campo.Text.Trim().ToUpper() == rows[0]["INFNAOPERMITIDA"].ToString().Trim().ToUpper())
                                {
                                    valida = false;
                                    errorProvider.SetError(campo, "Valor inválido!");
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(PS.Lib.WinForms.PSTextoBox))))
                {
                    PS.Lib.WinForms.PSTextoBox campo = (PS.Lib.WinForms.PSTextoBox)ctrl;
                    #region PSTextoBox
                    if (campo.Tag == null)
                    {
                        continue;
                    }

                    DataRow[] rows = dtcampos.Select("COLUNA = '" + campo.Tag.ToString() + "'" + (string.IsNullOrEmpty(codtipoper) ? "" : " AND CODTIPOPER ='" + codtipoper + "'"));

                    if (rows.Count() > 0)
                    {
                        if (string.IsNullOrEmpty(campo.textBox1.Text))
                        {
                            DataRow[] rowsDicionario = dtdicionario.Select("COLUNA = '" + frm.Tag.ToString() + "." + campo.Tag.ToString() + "'");
                            if (rowsDicionario.Count() > 0)
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'");
                                    }
                                }
                                else
                                {
                                    if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                    {
                                        if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                        {
                                            valida = false;
                                            errorProvider.SetError(campo, "Favor preencher o campo!");
                                        }
                                    }
                                    else
                                    {
                                        errorProvider.SetError(campo, "Favor preencher o campo!");
                                    }
                                }
                            }
                            else
                            {
                                errorProvider.SetError(campo, "Favor preencher o campo!");
                            }
                            valida = false;
                        }
                        else
                        {
                            if (!Convert.IsDBNull(rows[0]["INFNAOPERMITIDA"]))
                            {
                                if (campo.Text.Trim().ToUpper() == rows[0]["INFNAOPERMITIDA"].ToString().Trim().ToUpper())
                                {
                                    valida = false;
                                    errorProvider.SetError(campo, "Valor inválido!");
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(PS.Lib.WinForms.PSComboBox))))
                {
                    PS.Lib.WinForms.PSComboBox campo = (PS.Lib.WinForms.PSComboBox)ctrl;
                    #region ComboBox
                    if (campo.Tag == null)
                    {
                        continue;
                    }

                    DataRow[] rows = dtcampos.Select("COLUNA = '" + campo.Tag.ToString() + "'" + (string.IsNullOrEmpty(codtipoper) ? "" : " AND CODTIPOPER ='" + codtipoper + "'"));

                    if (rows.Count() > 0)
                    {
                        if (campo.comboBox1.SelectedIndex == -1 || campo.comboBox1.Text == "")
                        {
                            DataRow[] rowsDicionario = dtdicionario.Select("COLUNA = '" + frm.Tag.ToString() + "." + campo.Tag.ToString() + "'");
                            if (rowsDicionario.Count() > 0)
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'");
                                    }
                                }
                                else
                                {
                                    errorProvider.SetError(campo, "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'");
                                }
                            }
                            else
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Favor preencher o campo!");
                                    }
                                }
                                else
                                {
                                    errorProvider.SetError(campo, "Favor preencher o campo!");
                                }
                            }
                            valida = false;
                        }
                        else
                        {
                            if (!Convert.IsDBNull(rows[0]["INFNAOPERMITIDA"]))
                            {
                                if (campo.comboBox1.SelectedValue.ToString().Trim().ToUpper() == rows[0]["INFNAOPERMITIDA"].ToString().Trim().ToUpper())
                                {
                                    valida = false;
                                    errorProvider.SetError(campo, "Valor inválido!");
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(PS.Lib.WinForms.PSMaskedTextBox))))
                {
                    PS.Lib.WinForms.PSMaskedTextBox campo = (PS.Lib.WinForms.PSMaskedTextBox)ctrl;
                    #region PSMaskedTextBox
                    if (campo.Tag == null)
                    {
                        continue;
                    }

                    DataRow[] rows = dtcampos.Select("COLUNA = '" + campo.Tag.ToString() + "'" + (string.IsNullOrEmpty(codtipoper) ? "" : " AND CODTIPOPER ='" + codtipoper + "'"));

                    if (rows.Count() > 0)
                    {
                        if (string.IsNullOrEmpty(campo.textBox1.Text))
                        {
                            DataRow[] rowsDicionario = dtdicionario.Select("COLUNA = '" + frm.Tag.ToString() + "." + campo.Tag.ToString() + "'");
                            if (rowsDicionario.Count() > 0)
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'");
                                    }
                                }
                                else
                                {
                                    if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                    {
                                        if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                        {
                                            valida = false;
                                            errorProvider.SetError(campo, "Favor preencher o campo!");
                                        }
                                    }
                                    else
                                    {
                                        errorProvider.SetError(campo, "Favor preencher o campo!");
                                    }
                                }
                            }
                            else
                            {
                                errorProvider.SetError(campo, "Favor preencher o campo!");
                            }
                            valida = false;
                        }
                        else
                        {
                            if (!Convert.IsDBNull(rows[0]["INFNAOPERMITIDA"]))
                            {
                                if (campo.Text.Trim().ToUpper() == rows[0]["INFNAOPERMITIDA"].ToString().Trim().ToUpper())
                                {
                                    valida = false;
                                    errorProvider.SetError(campo, "Valor inválido!");
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(ITGProducao.Controles.NewLookup))))
                {
                    ITGProducao.Controles.NewLookup campo = (ITGProducao.Controles.NewLookup)ctrl;
                    campo.mensagemErrorProvider = "";
                    #region NewLookup
                    if (campo.Tag == null)
                    {
                        continue;
                    }

                    DataRow[] rows = dtcampos.Select("COLUNA = '" + campo.Tag.ToString() + "'" + (string.IsNullOrEmpty(codtipoper) ? "" : " AND CODTIPOPER ='" + codtipoper + "'"));

                    if (rows.Count() > 0)
                    {
                        if (string.IsNullOrEmpty(campo.txtcodigo.Text))
                        {
                            DataRow[] rowsDicionario = dtdicionario.Select("COLUNA = '" + frm.Tag.ToString() + "." + campo.Tag.ToString() + "'");
                            if (rowsDicionario.Count() > 0)
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        campo.mensagemErrorProvider = "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'";
                                    }
                                }
                                else
                                {
                                    campo.mensagemErrorProvider = "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'";
                                }
                            }
                            else
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        campo.mensagemErrorProvider = "Favor preencher o campo!";
                                    }
                                }
                                else
                                {
                                    errorProvider.SetError(campo, "Favor preencher o campo!");
                                }
                            }
                            valida = false;
                        }
                        else
                        {
                            if (!Convert.IsDBNull(rows[0]["INFNAOPERMITIDA"]))
                            {
                                if (campo.Text.Trim().ToUpper() == rows[0]["INFNAOPERMITIDA"].ToString().Trim().ToUpper())
                                {
                                    valida = false;
                                    campo.mensagemErrorProvider = "Valor inválido!";
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(System.Windows.Forms.CheckBox))))
                {
                    continue;
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(DevExpress.XtraEditors.CheckEdit))))
                {
                    continue;
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(System.Windows.Forms.TextBox))))
                {
                    System.Windows.Forms.TextBox campo = (System.Windows.Forms.TextBox)ctrl;
                    #region TextBox
                    if (campo.Tag == null)
                    {
                        continue;
                    }

                    DataRow[] rows = dtcampos.Select("COLUNA = '" + campo.Tag.ToString() + "'" + (string.IsNullOrEmpty(codtipoper) ? "" : " AND CODTIPOPER ='" + codtipoper + "'"));

                    if (rows.Count() > 0)
                    {
                        if (string.IsNullOrEmpty(campo.Text))
                        {
                            DataRow[] rowsDicionario = dtdicionario.Select("COLUNA = '" + frm.Tag.ToString() + "." + campo.Tag.ToString() + "'");
                            if (rowsDicionario.Count() > 0)
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'");
                                    }
                                }
                                else
                                {
                                    if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                    {
                                        if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                        {
                                            valida = false;
                                            errorProvider.SetError(campo, "Favor preencher o campo!");
                                        }
                                    }
                                    else
                                    {
                                        errorProvider.SetError(campo, "Favor preencher o campo!");
                                    }
                                }
                            }
                            else
                            {
                                errorProvider.SetError(campo, "Favor preencher o campo!");
                            }
                            valida = false;
                        }
                        else
                        {
                            if (!Convert.IsDBNull(rows[0]["INFNAOPERMITIDA"]))
                            {
                                if (campo.Text.Trim().ToUpper() == rows[0]["INFNAOPERMITIDA"].ToString().Trim().ToUpper())
                                {
                                    valida = false;
                                    errorProvider.SetError(campo, "Valor inválido!");
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(DevExpress.XtraEditors.MemoEdit))))
                {
                    DevExpress.XtraEditors.MemoEdit campo = (DevExpress.XtraEditors.MemoEdit)ctrl;
                    #region MemoEdit
                    if (campo.Tag == null)
                    {
                        continue;
                    }

                    DataRow[] rows = dtcampos.Select("COLUNA = '" + campo.Tag.ToString() + "'" + (string.IsNullOrEmpty(codtipoper) ? "" : " AND CODTIPOPER ='" + codtipoper + "'"));

                    if (rows.Count() > 0)
                    {
                        if (string.IsNullOrEmpty(campo.Text))
                        {
                            DataRow[] rowsDicionario = dtdicionario.Select("COLUNA = '" + frm.Tag.ToString() + "." + campo.Tag.ToString() + "'");
                            if (rowsDicionario.Count() > 0)
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'");
                                    }
                                }
                                else
                                {
                                    if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                    {
                                        if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                        {
                                            valida = false;
                                            errorProvider.SetError(campo, "Favor preencher o campo!");
                                        }
                                    }
                                    else
                                    {
                                        errorProvider.SetError(campo, "Favor preencher o campo!");
                                    }
                                }
                            }
                            else
                            {
                                errorProvider.SetError(campo, "Favor preencher o campo!");
                            }
                            valida = false;
                        }
                        else
                        {
                            if (!Convert.IsDBNull(rows[0]["INFNAOPERMITIDA"]))
                            {
                                if (campo.Text.Trim().ToUpper() == rows[0]["INFNAOPERMITIDA"].ToString().Trim().ToUpper())
                                {
                                    valida = false;
                                    errorProvider.SetError(campo, "Valor inválido!");
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(DevExpress.XtraEditors.DateEdit))))
                {
                    DevExpress.XtraEditors.DateEdit campo = (DevExpress.XtraEditors.DateEdit)ctrl;
                    #region DateEdit
                    if (campo.Tag == null)
                    {
                        continue;
                    }

                    DataRow[] rows = dtcampos.Select("COLUNA = '" + campo.Tag.ToString() + "'" + (string.IsNullOrEmpty(codtipoper) ? "" : " AND CODTIPOPER ='" + codtipoper + "'"));

                    if (rows.Count() > 0)
                    {
                        if (string.IsNullOrEmpty(campo.Text))
                        {
                            DataRow[] rowsDicionario = dtdicionario.Select("COLUNA = '" + frm.Tag.ToString() + "." + campo.Tag.ToString() + "'");
                            if (rowsDicionario.Count() > 0)
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'");
                                    }
                                }
                                else
                                {
                                    if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                    {
                                        if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                        {
                                            valida = false;
                                            errorProvider.SetError(campo, "Favor preencher o campo!");
                                        }
                                    }
                                    else
                                    {
                                        errorProvider.SetError(campo, "Favor preencher o campo!");
                                    }
                                }
                            }
                            else
                            {
                                errorProvider.SetError(campo, "Favor preencher o campo!");
                            }
                            valida = false;
                        }
                        else
                        {
                            if (!Convert.IsDBNull(rows[0]["INFNAOPERMITIDA"]))
                            {
                                DateTime data;
                                if (DateTime.TryParse(campo.Text, out data))
                                {
                                    if (campo.Text.Trim() == rows[0]["INFNAOPERMITIDA"].ToString().Trim())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Valor inválido!");
                                    }
                                }
                                else
                                {
                                    valida = false;
                                    errorProvider.SetError(campo, "Valor inválido!");
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(PS.Lib.WinForms.PSDateBox))))
                {
                    PS.Lib.WinForms.PSDateBox campo = (PS.Lib.WinForms.PSDateBox)ctrl;
                    #region PSDateBox
                    if (campo.Tag == null)
                    {
                        continue;
                    }

                    DataRow[] rows = dtcampos.Select("COLUNA = '" + campo.Tag.ToString() + "'" + (string.IsNullOrEmpty(codtipoper) ? "" : " AND CODTIPOPER ='" + codtipoper + "'"));

                    if (rows.Count() > 0)
                    {
                        if (string.IsNullOrEmpty(campo.Text))
                        {
                            DataRow[] rowsDicionario = dtdicionario.Select("COLUNA = '" + frm.Tag.ToString() + "." + campo.Tag.ToString() + "'");
                            if (rowsDicionario.Count() > 0)
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'");
                                    }
                                }
                                else
                                {
                                    if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                    {
                                        if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                        {
                                            valida = false;
                                            errorProvider.SetError(campo, "Favor preencher o campo!");
                                        }
                                    }
                                    else
                                    {
                                        errorProvider.SetError(campo, "Favor preencher o campo!");
                                    }
                                }
                            }
                            else
                            {
                                errorProvider.SetError(campo, "Favor preencher o campo!");
                            }
                            valida = false;
                        }
                        else
                        {
                            if (!Convert.IsDBNull(rows[0]["INFNAOPERMITIDA"]))
                            {
                                DateTime data;
                                if (DateTime.TryParse(campo.Text, out data))
                                {
                                    if (campo.Text.Trim() == rows[0]["INFNAOPERMITIDA"].ToString().Trim())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Valor inválido!");
                                    }
                                }
                                else
                                {
                                    valida = false;
                                    errorProvider.SetError(campo, "Valor inválido!");
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(DevExpress.XtraEditors.TimeEdit))))
                {
                    DevExpress.XtraEditors.TimeEdit campo = (DevExpress.XtraEditors.TimeEdit)ctrl;
                    #region TimeEdit
                    if (campo.Tag == null)
                    {
                        continue;
                    }

                    DataRow[] rows = dtcampos.Select("COLUNA = '" + campo.Tag.ToString() + "'" + (string.IsNullOrEmpty(codtipoper) ? "" : " AND CODTIPOPER ='" + codtipoper + "'"));

                    if (rows.Count() > 0)
                    {
                        if (string.IsNullOrEmpty(campo.Text))
                        {
                            DataRow[] rowsDicionario = dtdicionario.Select("COLUNA = '" + frm.Tag.ToString() + "." + campo.Tag.ToString() + "'");
                            if (rowsDicionario.Count() > 0)
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'");
                                    }
                                }
                                else
                                {
                                    if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                    {
                                        if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                        {
                                            valida = false;
                                            errorProvider.SetError(campo, "Favor preencher o campo!");
                                        }
                                    }
                                    else
                                    {
                                        errorProvider.SetError(campo, "Favor preencher o campo!");
                                    }
                                }
                            }
                            else
                            {
                                errorProvider.SetError(campo, "Favor preencher o campo!");
                            }
                            valida = false;
                        }
                        else
                        {
                            if (!Convert.IsDBNull(rows[0]["INFNAOPERMITIDA"]))
                            {
                                if (campo.Text.Trim().ToUpper() == rows[0]["INFNAOPERMITIDA"].ToString().Trim().ToUpper())
                                {
                                    valida = false;
                                    errorProvider.SetError(campo, "Valor inválido!");
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(PS.Lib.WinForms.PSMemoBox))))
                {
                    PS.Lib.WinForms.PSMemoBox campo = (PS.Lib.WinForms.PSMemoBox)ctrl;
                    #region PSMemoBox
                    if (campo.Tag == null)
                    {
                        continue;
                    }

                    DataRow[] rows = dtcampos.Select("COLUNA = '" + campo.Tag.ToString() + "'" + (string.IsNullOrEmpty(codtipoper) ? "" : " AND CODTIPOPER ='" + codtipoper + "'"));

                    if (rows.Count() > 0)
                    {
                        if (string.IsNullOrEmpty(campo.Text))
                        {
                            DataRow[] rowsDicionario = dtdicionario.Select("COLUNA = '" + frm.Tag.ToString() + "." + campo.Tag.ToString() + "'");
                            if (rowsDicionario.Count() > 0)
                            {
                                if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                {
                                    if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                    {
                                        valida = false;
                                        errorProvider.SetError(campo, "Favor preencher o campo '" + rowsDicionario[0]["DESCRICAO"].ToString() + "'");
                                    }
                                }
                                else
                                {
                                    if (!Convert.IsDBNull(rows[0]["CODTIPOPER"]))
                                    {
                                        if (codtipoper.Trim().ToUpper() == rows[0]["CODTIPOPER"].ToString().Trim().ToUpper())
                                        {
                                            valida = false;
                                            errorProvider.SetError(campo, "Favor preencher o campo!");
                                        }
                                    }
                                    else
                                    {
                                        errorProvider.SetError(campo, "Favor preencher o campo!");
                                    }
                                }
                            }
                            else
                            {
                                errorProvider.SetError(campo, "Favor preencher o campo!");
                            }
                            valida = false;
                        }
                        else
                        {
                            if (!Convert.IsDBNull(rows[0]["INFNAOPERMITIDA"]))
                            {
                                if (campo.Text.Trim().ToUpper() == rows[0]["INFNAOPERMITIDA"].ToString().Trim().ToUpper())
                                {
                                    valida = false;
                                    errorProvider.SetError(campo, "Valor inválido!");
                                }
                            }
                        }
                    }
                    #endregion

                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(System.Windows.Forms.SplitContainer))))
                {
                    validaCamposObrigatoriosRecursivo(ref valida, ref errorProvider, ctrl, dtcampos, dtdicionario, frm, codtipoper);
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(System.Windows.Forms.SplitterPanel))))
                {
                    validaCamposObrigatoriosRecursivo(ref valida, ref errorProvider, ctrl, dtcampos, dtdicionario, frm, codtipoper);
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(System.Windows.Forms.Panel))))
                {
                    validaCamposObrigatoriosRecursivo(ref valida, ref errorProvider, ctrl, dtcampos, dtdicionario, frm, codtipoper);
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(System.Windows.Forms.TabControl))))
                {
                    validaCamposObrigatoriosRecursivo(ref valida, ref errorProvider, ctrl, dtcampos, dtdicionario, frm, codtipoper);
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(System.Windows.Forms.TabPage))))
                {
                    validaCamposObrigatoriosRecursivo(ref valida, ref errorProvider, ctrl, dtcampos, dtdicionario, frm, codtipoper);
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(System.Windows.Forms.GroupBox))))
                {
                    validaCamposObrigatoriosRecursivo(ref valida, ref errorProvider, ctrl, dtcampos, dtdicionario, frm, codtipoper);
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(System.Windows.Forms.Label))))
                {
                    continue;
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(System.Windows.Forms.ToolStrip))))
                {
                    continue;
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(DevExpress.XtraEditors.SimpleButton))))
                {
                    continue;
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(DevExpress.XtraEditors.LabelControl))))
                {
                    continue;
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(DevExpress.XtraGrid.GridControl))))
                {
                    continue;
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(System.Windows.Forms.DataGridView))))
                {
                    continue;
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(DevExpress.XtraEditors.PictureEdit))))
                {
                    continue;
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(DevExpress.XtraPdfViewer.PdfViewer))))
                {
                    continue;
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(System.Windows.Forms.Button))))
                {
                    continue;
                }
                else
                {
                    MessageBox.Show("Favor verificar tipo do controle, contate o administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    valida = false;
                }
            }
            return valida;
        }

        #endregion

        /// <summary>
        /// Método para exclusão do registro quando existem regsitro em outras tabelas.
        /// </summary>
        /// <param name="campo">Campo para exlusão</param>
        /// <param name="tabelaOrigem">Tabela de Origem do campo</param>
        /// /// <param name="valorParametro">Valor do campo da exclusão</param>
        /// <returns></returns>
        public bool Excluir(string campo, string tabelaOrigem, string valorParametro)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();

            try
            {
                string resultadoOrigem = conn.ExecGetField(string.Empty, "SELECT " + campo + " FROM " + tabelaOrigem + " WHERE " + campo + " = '" + valorParametro + "'", new object[] { }).ToString();
                if (string.IsNullOrEmpty(resultadoOrigem))
                {
                    return false;
                }

                DataTable dt1 = conn.ExecQuery("SELECT T.name AS Tabela, C.name AS Coluna FROM sys.sysobjects AS T(NOLOCK) INNER JOIN sys.all_columns AS C(NOLOCK) ON T.id = C.object_id AND T.XTYPE = 'U' WHERE C.NAME LIKE '%" + campo + "%' AND T.NAME <> '" + tabelaOrigem + "' ORDER BY T.name ASC", new object[] { });

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string resultado = conn.ExecGetField("0", "SELECT COUNT(" + dt1.Rows[i]["Coluna"] + ") AS RESULTADO FROM " + dt1.Rows[i]["Tabela"] + " WHERE " + dt1.Rows[i]["Coluna"] + " = '" + valorParametro + "'", new object[] { }).ToString();
                    if (resultado != "0")
                    {
                        return false;
                    }
                }
                conn.ExecTransaction("DELETE FROM " + tabelaOrigem + " WHERE " + campo + " = '" + valorParametro + "'", new object[] { });
                conn.Commit();
                return true;
            }

            catch (Exception EX)
            {
                conn.Rollback();
                return false;
            }
        }


        /// <summary>
        /// Método para salvar os campos complementares
        /// </summary>
        /// <param name="frm">Formulário que está usando</param>
        /// <param name="tabela">Nome da tabela Complementar</param>
        /// <param name="tabCamposComplementares">TabPage onde se encontra os campos complementares</param>
        /// <param name="listaParametro">Lista com os parametros, seguindo nome e valor</param>
        /// <param name="conn">Conexão ativa do banco de dados</param>
        /// <returns></returns>
        public bool salvaCamposComplementares(Form frm, string tabela, TabPage tabCamposComplementares, List<Parametro> listaParametro, AppLib.Data.Connection conn)
        {
            if (tabCamposComplementares.Controls.Count > 0)
            {
                string sql = string.Empty;
                //Monta a query para verificar se existe registro na tabela compl
                if (listaParametro.Count > 0)
                {
                    sql = "SELECT COUNT(" + listaParametro[0].nomeParametro + ") FROM " + tabela + " WHERE ";
                }
                for (int i = 0; i < listaParametro.Count; i++)
                {
                    if (i == 0)
                    {
                        sql = sql + listaParametro[i].nomeParametro + " = '" + listaParametro[i].valorParametro + "'";
                    }
                    else
                    {
                        sql = sql + " AND " + listaParametro[i].nomeParametro + " = '" + listaParametro[i].valorParametro + "'";
                    }
                }
                //Executa a query montada no bloco acima
                bool retorno = Convert.ToBoolean(conn.ExecGetField(false, sql, new object[] { }));
                //Verifica se o existe ou não o registro, se caso não existir o retorno será como false e irá criar o bloco de insert
                if (retorno == false)
                {
                    sql = "INSERT INTO " + tabela + " (";
                    for (int i = 0; i < listaParametro.Count; i++)
                    {
                        if (i == 0)
                        {
                            sql = sql + listaParametro[i].nomeParametro;
                        }
                        else
                        {
                            sql = sql + ", " + listaParametro[i].nomeParametro;
                        }

                    }
                    for (int i = 0; i < listaParametro.Count; i++)
                    {
                        if (i == 0)
                        {
                            sql = sql + ") VALUES ( '" + listaParametro[i].valorParametro + "'";
                        }
                        else
                        {
                            sql = sql + ", '" + listaParametro[i].valorParametro + "'";
                        }
                    }
                    sql = sql + " )";
                    conn.ExecTransaction(sql, new object[] { });
                }
                //Bloco para realizar o update
                sql = new Class.Utilidades().update(frm, tabCamposComplementares, tabela);
                if (!string.IsNullOrEmpty(sql))
                {
                    sql = sql.Remove(sql.Length - 2, 1);
                    for (int i = 0; i < listaParametro.Count; i++)
                    {
                        if (i == 0)
                        {
                            sql = sql + " WHERE " + listaParametro[i].nomeParametro + " = '" + listaParametro[i].valorParametro + "'";
                        }
                        else
                        {
                            sql = sql + " AND " + listaParametro[i].nomeParametro + " = '" + listaParametro[i].valorParametro + "'";
                        }
                    }
                    conn.ExecTransaction(sql, new object[] { });
                }
                return true;
            }
            else
            {
                return true;
            }
        }

        public void criaCamposComplementares(string tabela, TabPage tabCamposComplementares)
        {
            int x = 20, y = 13;

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM GTABCAMPOCOMPL WHERE CODENTIDADE = ? AND ATIVO = 1 ORDER BY ORDEM", new object[] { tabela });
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                switch (dt.Rows[i]["TIPO"].ToString())
                {
                    case "PSTextoBox":
                        //Cria labelControl
                        DevExpress.XtraEditors.LabelControl labelTexto = new DevExpress.XtraEditors.LabelControl();
                        labelTexto.Name = "lbl" + dt.Rows[i]["NOMECAMPO"].ToString();
                        labelTexto.Text = dt.Rows[i]["DESCRICAO"].ToString();
                        labelTexto.Location = new System.Drawing.Point(x, y);
                        tabCamposComplementares.Controls.Add(labelTexto);
                        y = y + 20;

                        //Cria Campo Texto
                        DevExpress.XtraEditors.TextEdit texto = new DevExpress.XtraEditors.TextEdit();
                        texto.Name = "Txt_" + dt.Rows[i]["NOMECAMPO"].ToString();
                        texto.Properties.MaxLength = Convert.ToInt32(dt.Rows[i]["TAMANHO"]);
                        texto.Location = new System.Drawing.Point(x, y);
                        if (!string.IsNullOrEmpty(dt.Rows[i]["TAMANHOCAMPO"].ToString()))
                        {
                            texto.Size = new System.Drawing.Size(Convert.ToInt32(dt.Rows[i]["TAMANHOCAMPO"]), 20);
                        }
                        else
                        {
                            texto.Size = new System.Drawing.Size(100, 20);
                        }

                        tabCamposComplementares.Controls.Add(texto);
                        y = y + 30;

                        break;
                    case "PSDateBox":
                        //Cria labelControl
                        DevExpress.XtraEditors.LabelControl labelData = new DevExpress.XtraEditors.LabelControl();
                        labelData.Name = "lbl" + dt.Rows[i]["NOMECAMPO"].ToString();
                        labelData.Text = dt.Rows[i]["DESCRICAO"].ToString();
                        labelData.Location = new System.Drawing.Point(x, y);
                        tabCamposComplementares.Controls.Add(labelData);
                        y = y + 20;

                        //Cria Campo Data
                        DevExpress.XtraEditors.DateEdit data = new DevExpress.XtraEditors.DateEdit();
                        data.Name = "Dte_" + dt.Rows[i]["NOMECAMPO"].ToString();
                        data.Location = new System.Drawing.Point(x, y);
                        tabCamposComplementares.Controls.Add(data);
                        y = y + 30;

                        break;
                    case "PSLookup":
                        //Cria labelControl
                        DevExpress.XtraEditors.LabelControl labelLista = new DevExpress.XtraEditors.LabelControl();
                        labelLista.Name = "lbl" + dt.Rows[i]["NOMECAMPO"].ToString();
                        labelLista.Text = dt.Rows[i]["DESCRICAO"].ToString();
                        labelLista.Location = new System.Drawing.Point(x, y);
                        tabCamposComplementares.Controls.Add(labelLista);
                        y = y + 20;

                        DataTable dtLista = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODREGISTRO VALOR, DESCRICAO FROM GTABDINAMICAITEM WHERE CODTABELA = ? AND CODEMPRESA = ?", new object[] { dt.Rows[i]["CODTABELA"].ToString(), AppLib.Context.Empresa });
                        ComboBox combo = new ComboBox();
                        combo.Name = "Cmb_" + dt.Rows[i]["NOMECAMPO"].ToString();
                        combo.Size = new System.Drawing.Size(Convert.ToInt32(dt.Rows[i]["TAMANHOCAMPO"]), 20);
                        combo.DataSource = dtLista;
                        combo.ValueMember = "VALOR";
                        combo.DisplayMember = "DESCRICAO";
                        combo.Location = new System.Drawing.Point(x, y);
                        tabCamposComplementares.Controls.Add(combo);
                        y = y + 30;

                        break;
                    case "PSMoedaBox":
                        //Cria labelControl
                        DevExpress.XtraEditors.LabelControl labelNumeric = new DevExpress.XtraEditors.LabelControl();
                        labelNumeric.Name = "lbl" + dt.Rows[i]["NOMECAMPO"].ToString();
                        labelNumeric.Text = dt.Rows[i]["DESCRICAO"].ToString();
                        labelNumeric.Location = new System.Drawing.Point(x, y);
                        tabCamposComplementares.Controls.Add(labelNumeric);
                        y = y + 20;

                        //Cria Campo Texto
                        DevExpress.XtraEditors.TextEdit numerico = new DevExpress.XtraEditors.TextEdit();
                        numerico.Name = "Num_" + dt.Rows[i]["NOMECAMPO"].ToString();
                        numerico.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                        switch (dt.Rows[i]["CASASDECIMAIS"].ToString())
                        {
                            case "2":
                                numerico.Properties.Mask.EditMask = "n2";
                                break;
                            case "4":
                                numerico.Properties.Mask.EditMask = "n4";
                                break;
                            default:
                                numerico.Properties.Mask.EditMask = "n2";
                                break;
                        }
                        numerico.Location = new System.Drawing.Point(x, y);
                        tabCamposComplementares.Controls.Add(numerico);
                        y = y + 30;

                        break;
                    case "PSCheckBox":
                        DevExpress.XtraEditors.CheckEdit checkbox = new DevExpress.XtraEditors.CheckEdit();
                        checkbox.Name = "Che_" + dt.Rows[i]["NOMECAMPO"].ToString();
                        checkbox.Text = dt.Rows[i]["DESCRICAO"].ToString();
                        checkbox.Location = new System.Drawing.Point(x, y);
                        checkbox.Size = new System.Drawing.Size(Convert.ToInt32(dt.Rows[i]["TAMANHOCAMPO"]), 20);
                        tabCamposComplementares.Controls.Add(checkbox);
                        y = y + 30;

                        break;
                    case "TextEdit":
                        //Cria labelControl
                        DevExpress.XtraEditors.LabelControl labelTextoLongo = new DevExpress.XtraEditors.LabelControl();
                        labelTextoLongo.Name = "lbl" + dt.Rows[i]["NOMECAMPO"].ToString();
                        labelTextoLongo.Text = dt.Rows[i]["DESCRICAO"].ToString();
                        labelTextoLongo.Location = new System.Drawing.Point(x, y);
                        tabCamposComplementares.Controls.Add(labelTextoLongo);
                        y = y + 20;

                        //Cria Campo Texto
                        DevExpress.XtraEditors.MemoEdit textoLongo = new DevExpress.XtraEditors.MemoEdit();
                        textoLongo.Name = "Txt_" + dt.Rows[i]["NOMECAMPO"].ToString();
                        textoLongo.Properties.MaxLength = Convert.ToInt32(dt.Rows[i]["TAMANHO"]);
                        textoLongo.Location = new System.Drawing.Point(x, y);

                        if (!string.IsNullOrEmpty(dt.Rows[i]["TAMANHOCAMPO"].ToString()) && !string.IsNullOrEmpty(dt.Rows[i]["ALTURACAMPO"].ToString()))
                        {
                            textoLongo.Size = new System.Drawing.Size(Convert.ToInt32(dt.Rows[i]["TAMANHOCAMPO"]), Convert.ToInt32(dt.Rows[i]["ALTURACAMPO"]));

                            tabCamposComplementares.Controls.Add(textoLongo);

                            y = y + (Convert.ToInt32(dt.Rows[i]["ALTURACAMPO"]) + 10);
                        }
                        else
                        {
                            textoLongo.Size = new System.Drawing.Size(100, 150);

                            tabCamposComplementares.Controls.Add(textoLongo);

                            y = y + 160;
                        }

                        tabCamposComplementares.AutoScroll = true;

                        break;
                    default:
                        break;
                }

                tabCamposComplementares.AutoScroll = true;
            }
        }

        public void criaCamposComplementaresOperacao(string tabela, TabPage tabCamposComplementares, string Codtipoper)
        {
            int x = 20, y = 13;

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT GTABCAMPOCOMPL.*
                                                                                 FROM 
                                                                                 GTABCAMPOCOMPL 
                                                                                 INNER JOIN GTIPOPERCOMPL ON GTIPOPERCOMPL.CODENTIDADE = GTABCAMPOCOMPL.CODENTIDADE AND GTIPOPERCOMPL.NOMECAMPO = GTABCAMPOCOMPL.NOMECAMPO
                                                                                 WHERE GTABCAMPOCOMPL.CODENTIDADE = ? 
                                                                                 AND GTABCAMPOCOMPL.ATIVO = ? 
                                                                                 AND GTIPOPERCOMPL.CODTIPOPER = ?
                                                                                 ORDER BY GTABCAMPOCOMPL.ORDEM", new object[] { tabela, 1, Codtipoper });
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                switch (dt.Rows[i]["TIPO"].ToString())
                {
                    case "PSTextoBox":
                        //Cria labelControl
                        DevExpress.XtraEditors.LabelControl labelTexto = new DevExpress.XtraEditors.LabelControl();
                        labelTexto.Name = "lbl" + dt.Rows[i]["NOMECAMPO"].ToString();
                        labelTexto.Text = dt.Rows[i]["DESCRICAO"].ToString();
                        labelTexto.Location = new System.Drawing.Point(x, y);
                        tabCamposComplementares.Controls.Add(labelTexto);
                        y = y + 20;

                        //Cria Campo Texto
                        DevExpress.XtraEditors.TextEdit texto = new DevExpress.XtraEditors.TextEdit();
                        texto.Name = "Txt_" + dt.Rows[i]["NOMECAMPO"].ToString();
                        texto.Properties.MaxLength = Convert.ToInt32(dt.Rows[i]["TAMANHO"]);
                        texto.Location = new System.Drawing.Point(x, y);
                        if (!string.IsNullOrEmpty(dt.Rows[i]["TAMANHOCAMPO"].ToString()))
                        {
                            texto.Size = new System.Drawing.Size(Convert.ToInt32(dt.Rows[i]["TAMANHOCAMPO"]), 20);
                        }
                        else
                        {
                            texto.Size = new System.Drawing.Size(100, 20);
                        }

                        tabCamposComplementares.Controls.Add(texto);
                        y = y + 30;

                        break;
                    case "PSDateBox":
                        //Cria labelControl
                        DevExpress.XtraEditors.LabelControl labelData = new DevExpress.XtraEditors.LabelControl();
                        labelData.Name = "lbl" + dt.Rows[i]["NOMECAMPO"].ToString();
                        labelData.Text = dt.Rows[i]["DESCRICAO"].ToString();
                        labelData.Location = new System.Drawing.Point(x, y);
                        tabCamposComplementares.Controls.Add(labelData);
                        y = y + 20;

                        //Cria Campo Data
                        DevExpress.XtraEditors.DateEdit data = new DevExpress.XtraEditors.DateEdit();
                        data.Name = "Dte_" + dt.Rows[i]["NOMECAMPO"].ToString();
                        data.Location = new System.Drawing.Point(x, y);
                        tabCamposComplementares.Controls.Add(data);
                        y = y + 30;

                        break;
                    case "PSLookup":
                        //Cria labelControl
                        DevExpress.XtraEditors.LabelControl labelLista = new DevExpress.XtraEditors.LabelControl();
                        labelLista.Name = "lbl" + dt.Rows[i]["NOMECAMPO"].ToString();
                        labelLista.Text = dt.Rows[i]["DESCRICAO"].ToString();
                        labelLista.Location = new System.Drawing.Point(x, y);
                        tabCamposComplementares.Controls.Add(labelLista);
                        y = y + 20;

                        DataTable dtLista = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODREGISTRO VALOR, DESCRICAO FROM GTABDINAMICAITEM WHERE CODTABELA = ? AND CODEMPRESA = ?", new object[] { dt.Rows[i]["CODTABELA"].ToString(), AppLib.Context.Empresa });
                        ComboBox combo = new ComboBox();
                        combo.Name = "Cmb_" + dt.Rows[i]["NOMECAMPO"].ToString();
                        combo.Size = new System.Drawing.Size(Convert.ToInt32(dt.Rows[i]["TAMANHOCAMPO"]), 20);
                        combo.DataSource = dtLista;
                        combo.ValueMember = "VALOR";
                        combo.DisplayMember = "DESCRICAO";
                        combo.Location = new System.Drawing.Point(x, y);
                        tabCamposComplementares.Controls.Add(combo);
                        y = y + 30;

                        break;
                    case "PSMoedaBox":
                        //Cria labelControl
                        DevExpress.XtraEditors.LabelControl labelNumeric = new DevExpress.XtraEditors.LabelControl();
                        labelNumeric.Name = "lbl" + dt.Rows[i]["NOMECAMPO"].ToString();
                        labelNumeric.Text = dt.Rows[i]["DESCRICAO"].ToString();
                        labelNumeric.Location = new System.Drawing.Point(x, y);
                        tabCamposComplementares.Controls.Add(labelNumeric);
                        y = y + 20;

                        //Cria Campo Texto
                        DevExpress.XtraEditors.TextEdit numerico = new DevExpress.XtraEditors.TextEdit();
                        numerico.Name = "Num_" + dt.Rows[i]["NOMECAMPO"].ToString();
                        numerico.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                        switch (dt.Rows[i]["CASASDECIMAIS"].ToString())
                        {
                            case "2":
                                numerico.Properties.Mask.EditMask = "n2";
                                break;
                            case "4":
                                numerico.Properties.Mask.EditMask = "n4";
                                break;
                            default:
                                numerico.Properties.Mask.EditMask = "n2";
                                break;
                        }
                        numerico.Location = new System.Drawing.Point(x, y);
                        tabCamposComplementares.Controls.Add(numerico);
                        y = y + 30;

                        break;
                    case "PSCheckBox":
                        DevExpress.XtraEditors.CheckEdit checkbox = new DevExpress.XtraEditors.CheckEdit();
                        checkbox.Name = "Che_" + dt.Rows[i]["NOMECAMPO"].ToString();
                        checkbox.Text = dt.Rows[i]["DESCRICAO"].ToString();
                        checkbox.Location = new System.Drawing.Point(x, y);
                        checkbox.Size = new System.Drawing.Size(Convert.ToInt32(dt.Rows[i]["TAMANHOCAMPO"]), 20);
                        tabCamposComplementares.Controls.Add(checkbox);
                        y = y + 30;

                        break;
                    case "TextEdit":
                        //Cria labelControl
                        DevExpress.XtraEditors.LabelControl labelTextoLongo = new DevExpress.XtraEditors.LabelControl();
                        labelTextoLongo.Name = "lbl" + dt.Rows[i]["NOMECAMPO"].ToString();
                        labelTextoLongo.Text = dt.Rows[i]["DESCRICAO"].ToString();
                        labelTextoLongo.Location = new System.Drawing.Point(x, y);
                        tabCamposComplementares.Controls.Add(labelTextoLongo);
                        y = y + 20;

                        //Cria Campo Texto
                        DevExpress.XtraEditors.MemoEdit textoLongo = new DevExpress.XtraEditors.MemoEdit();
                        textoLongo.Name = "Txt_" + dt.Rows[i]["NOMECAMPO"].ToString();
                        textoLongo.Properties.MaxLength = Convert.ToInt32(dt.Rows[i]["TAMANHO"]);
                        textoLongo.Location = new System.Drawing.Point(x, y);

                        if (!string.IsNullOrEmpty(dt.Rows[i]["TAMANHOCAMPO"].ToString()) && !string.IsNullOrEmpty(dt.Rows[i]["ALTURACAMPO"].ToString()))
                        {
                            textoLongo.Size = new System.Drawing.Size(Convert.ToInt32(dt.Rows[i]["TAMANHOCAMPO"]), Convert.ToInt32(dt.Rows[i]["ALTURACAMPO"]));

                            tabCamposComplementares.Controls.Add(textoLongo);

                            y = y + (Convert.ToInt32(dt.Rows[i]["ALTURACAMPO"]) + 10);
                        }
                        else
                        {
                            textoLongo.Size = new System.Drawing.Size(100, 150);

                            tabCamposComplementares.Controls.Add(textoLongo);

                            y = y + 160;
                        }

                        tabCamposComplementares.AutoScroll = true;

                        break;
                    default:
                        break;
                }

                tabCamposComplementares.AutoScroll = true;
            }
        }

        /// <summary>
        /// Método que realiza a soma dos registros selecionados na visão.
        /// </summary>
        /// <param name="gridview">Gridview que será usado para a seleção de registros</param>
        /// <returns>Retorna a soma dos registros selecionados</returns>
        public decimal CalculaTotal(GridView gridview, string tabela)
        {
            if (tabela == "FLANCA")
            {
                if (gridview.SelectedRowsCount == 1)
                {
                    DataRow row1 = gridview.GetDataRow(Convert.ToInt32(gridview.GetSelectedRows().GetValue(0).ToString()));

                    if (total != Convert.ToDecimal(row1["FLANCA.VLLIQUIDO"]))
                    {
                        total = 0;
                    }

                    total += Convert.ToDecimal(row1["FLANCA.VLLIQUIDO"]);
                    return total;
                }
                else
                {
                    for (int i = 0; i < gridview.SelectedRowsCount; i++)
                    {
                        DataRow row1 = gridview.GetDataRow(Convert.ToInt32(gridview.GetSelectedRows().GetValue(i).ToString()));

                        total += Convert.ToDecimal(row1["FLANCA.VLLIQUIDO"]);
                    }
                    return total;
                }
            }
            else if (tabela == "AAPONTAMENTO")
            {
                if (gridview.SelectedRowsCount == 1)
                {
                    DataRow row1 = gridview.GetDataRow(Convert.ToInt32(gridview.GetSelectedRows().GetValue(0).ToString()));

                    if (total != Convert.ToDecimal(row1["AAPONTAMENTO.TOTALHORAS"]))
                    {
                        total = 0;
                    }

                    total += Convert.ToDecimal(row1["AAPONTAMENTO.TOTALHORAS"]);
                    return total;
                }
                else
                {
                    for (int i = 0; i < gridview.SelectedRowsCount; i++)
                    {
                        DataRow row1 = gridview.GetDataRow(Convert.ToInt32(gridview.GetSelectedRows().GetValue(i).ToString()));

                        total += Convert.ToDecimal(row1["AAPONTAMENTO.TOTALHORAS"]);
                    }
                    return total;
                }
            }
            else
            {
                if (gridview.SelectedRowsCount == 1)
                {
                    DataRow row1 = gridview.GetDataRow(Convert.ToInt32(gridview.GetSelectedRows().GetValue(0).ToString()));

                    total += Convert.ToDecimal(row1["GOPER.VALORLIQUIDO"]);
                    return total;
                }
                else
                {
                    for (int i = 0; i < gridview.SelectedRowsCount; i++)
                    {
                        DataRow row1 = gridview.GetDataRow(Convert.ToInt32(gridview.GetSelectedRows().GetValue(i).ToString()));

                        total += Convert.ToDecimal(row1["GOPER.VALORLIQUIDO"]);
                    }
                    return total;
                }
            }
        }

        public string CalculaTotalHoras(GridView gridview, string tabela)
        {
            double totalHoras = 0;
            double totalMinutos = 0;

            TimeSpan tsHoras;
            TimeSpan tsMinutos;

            TimeSpan tsTotal = new TimeSpan();

            if (tabela == "AAPONTAMENTO")
            {
                if (gridview.SelectedRowsCount == 1)
                {
                    DataRow row1 = gridview.GetDataRow(Convert.ToInt32(gridview.GetSelectedRows().GetValue(0).ToString()));

                    // Converte para Datetime para conseguir converter em TimeSpan posteriormente
                    TimeSpan totalHorasAponatas = TimeSpan.Parse(row1["AAPONTAMENTO.TOTALHORAS"].ToString());

                    if (totalHorasAponatas.Minutes > 0)
                    {
                        totalMinutos += totalHorasAponatas.Minutes;
                    }

                    if (totalHorasAponatas.Hours > 0)
                    {
                        totalHoras += totalHorasAponatas.Hours;
                    }

                    tsMinutos = TimeSpan.FromMinutes(totalMinutos);
                    tsHoras = TimeSpan.FromHours(totalHoras);

                    tsTotal = (tsHoras + tsMinutos);

                    return string.Concat((totalHoras < 10 ? "0" + totalHoras.ToString() : totalHoras.ToString()), ":", (totalMinutos < 10 ? "0" + totalMinutos.ToString() : totalMinutos.ToString()));
                }
                else
                {
                    for (int i = 0; i < gridview.SelectedRowsCount; i++)
                    {
                        DataRow row1 = gridview.GetDataRow(Convert.ToInt32(gridview.GetSelectedRows().GetValue(i).ToString()));

                        // Converte para Datetime para conseguir converter em TimeSpan posteriormente
                        TimeSpan totalHorasAponatas = TimeSpan.Parse(row1["AAPONTAMENTO.TOTALHORAS"].ToString());

                        if (totalHorasAponatas.Minutes > 0)
                        {
                            totalMinutos += totalHorasAponatas.Minutes;
                        }

                        if (totalHorasAponatas.Hours > 0)
                        {
                            totalHoras += totalHorasAponatas.Hours;
                        }
                    }

                    tsMinutos = TimeSpan.FromMinutes(totalMinutos);
                    tsHoras = TimeSpan.FromHours(totalHoras);

                    tsTotal = (tsHoras + tsMinutos);

                    return string.Concat((totalHoras < 10 ? "0" + totalHoras.ToString() : totalHoras.ToString()), ":", (totalMinutos < 10 ? "0" + totalMinutos.ToString() : totalMinutos.ToString()));
                }
            }

            return "";
        }

        public Boolean ValidarEmail(String Email)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(Email))
                {
                    Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

                    if (rg.IsMatch(Email))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
