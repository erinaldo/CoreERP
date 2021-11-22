using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using PS.Lib;

namespace PS.Glb
{
    public partial class PSPartRastreiaOperAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        private PS.Lib.Constantes ct = new PS.Lib.Constantes();
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private PS.Lib.Valida vl = new PS.Lib.Valida();

        private string sTipOperAnt = string.Empty;

        public PSPartRastreiaOperAppFrm()
        {
            InitializeComponent();
        }

        private void PSPartRastreiaOperAppFrm_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.psPartApp.Access == PS.Lib.AppAccess.View)
                {
                    if (psPartApp.DataGrid != null)
                    {
                        for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
                        {
                                sTipOperAnt = gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[i]), "CODTIPOPER").Valor.ToString();
                                CarregaTreeView(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[i]));
                        }
                    }
                }

                if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
                {
                    sTipOperAnt = gb.RetornaDataFieldByCampo(this.psPartApp.DataField, "CODTIPOPER").Valor.ToString();
                    CarregaTreeView(this.psPartApp.DataField);
                }

            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
            }
        }

        private void CarregaTreeView(List<DataField> objArr)
        {
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add("Origem", "Origem", 3, 3);

            //Operação de Origem
            PS.Lib.DataField dfCODEMPRESA = new PS.Lib.DataField();
            dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");

            PS.Lib.DataField dfCODOPER = new PS.Lib.DataField();
            dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            PS.Lib.DataField dfCODTIPOPER = new PS.Lib.DataField();
            dfCODTIPOPER = gb.RetornaDataFieldByCampo(objArr, "CODTIPOPER");

            PS.Lib.DataField dfNUMERO = new PS.Lib.DataField();
            dfNUMERO = gb.RetornaDataFieldByCampo(objArr, "NUMERO");

            //Item da Árvore
            string sItemValue = string.Concat(dfCODEMPRESA.Valor.ToString(), ";", dfCODOPER.Valor.ToString());
            string sItemTexT = string.Concat(dfCODTIPOPER.Valor.ToString(), " - ", dfNUMERO.Valor.ToString());

            treeView1.Nodes[0].Nodes.Add(sItemValue, sItemTexT, 2, 2);
            treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];
            treeView1.ExpandAll();
        }

        private DataTable RetornaOperRelac(int CodEmpresa, int CodOper)
        {
            string sSql = string.Empty;

            sSql = @"SELECT GOPER.CODEMPRESA, GOPER.CODOPER, GOPER.CODTIPOPER, GOPER.NUMERO
                    FROM GOPERRELAC, GOPER
                    WHERE GOPERRELAC.CODEMPRESA = ? AND GOPERRELAC.CODOPER = ?
                    AND GOPERRELAC.CODEMPRESA = GOPER.CODEMPRESA
                    AND GOPERRELAC.CODOPERRELAC = GOPER.CODOPER";

            return dbs.QuerySelect(sSql, CodEmpresa, CodOper);
        }

        private void IncluirRelacionamentoNivel1(DataTable dt)
        {
            PS.Lib.DataField dfCODEMPRESA = new PS.Lib.DataField();
            PS.Lib.DataField dfCODOPER = new PS.Lib.DataField();
            PS.Lib.DataField dfCODTIPOPER = new PS.Lib.DataField();
            PS.Lib.DataField dfNUMERO = new PS.Lib.DataField();

            List<DataField> objArrRelac = new List<DataField>();

            if (dt.Rows.Count > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    objArrRelac = gb.RetornaDataFieldByDataRow(dt.Rows[i]);

                    if (objArrRelac.Count > 0)
                    {
                        dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArrRelac, "CODEMPRESA");
                        dfCODOPER = gb.RetornaDataFieldByCampo(objArrRelac, "CODOPER");
                        dfCODTIPOPER = gb.RetornaDataFieldByCampo(objArrRelac, "CODTIPOPER");
                        dfNUMERO = gb.RetornaDataFieldByCampo(objArrRelac, "NUMERO");

                        //Item da Árvore
                        string sItemValue = string.Concat(dfCODEMPRESA.Valor.ToString(), ";", dfCODOPER.Valor.ToString());
                        string sItemTexT = string.Concat(dfCODTIPOPER.Valor.ToString(), " - ", dfNUMERO.Valor.ToString());

                        treeView1.SelectedNode.Nodes.Add(sItemValue, sItemTexT, 2, 2);
                    }
                }
            }       
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            string key = string.Empty;

            TreeNode node = new TreeNode();

            try
            {
                node = treeView1.SelectedNode;

                if (node.Nodes.Count > 0)
                    return;

                key = node.Name;

                Regex rSplit = new Regex(";");
                String[] sResult = rSplit.Split(key);

                DataTable dt = RetornaOperRelac(int.Parse(sResult[0]), int.Parse(sResult[1]));
                IncluirRelacionamentoNivel1(dt);
            }
            catch
            {
                return;
            }
        }

        private void dadosDaOperaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string key = string.Empty;

            TreeNode node = new TreeNode();

            try
            {
                node = treeView1.SelectedNode;

                key = node.Name;

                Regex rSplit = new Regex(";");
                String[] sResult = rSplit.Split(key);
                String[] skey = { "CODEMPRESA", "CODOPER" };

                DataTable dt = gb.RetornaDataTablebyParameter("GOPER", skey, sResult[0], sResult[1]);
                PSPartOperacao psPartOperacao = new PSPartOperacao();
                psPartOperacao.DefaultFilter.Add(new PS.Lib.PSFilter("CODFILIAL", dt.Rows[0]["CODFILIAL"]));
                psPartOperacao.DefaultFilter.Add(new PS.Lib.PSFilter("CODTIPOPER", dt.Rows[0]["CODTIPOPER"]));
                psPartOperacao.ExecuteWithParams(sResult[0], sResult[1]);
            }
            catch
            {
                return;
            }
        }

        public override Boolean Execute()
        {
            return false;
        }
    }
}
