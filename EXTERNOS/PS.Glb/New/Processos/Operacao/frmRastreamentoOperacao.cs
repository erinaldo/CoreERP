using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using PS.Lib;

namespace PS.Glb.New.Processos.Operacao
{
    public partial class frmRastreamentoOperacao : Form
    {
        private PS.Lib.Constantes ct = new PS.Lib.Constantes();
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private PS.Lib.Valida vl = new PS.Lib.Valida();

        public frmRastreamentoOperacao(int codoper, string codtipOper, string numero)
        {
            InitializeComponent();
            CarregaTreeView( codoper,  codtipOper,  numero);
        }
        private void CarregaTreeView(int codoper, string codtipOper, string numero)
        {
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add("Origem", "Origem", 3, 3);

            //Item da Árvore
            string sItemValue = string.Concat(AppLib.Context.Empresa, ";", codoper);
            string sItemTexT = string.Concat(codtipOper, " - ", numero);

            treeView1.Nodes[0].Nodes.Add(sItemValue, sItemTexT, 2, 2);
            treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];
            treeView1.ExpandAll();
        }
        private DataTable RetornaOperRelac(int CodEmpresa, int CodOper)
        {
            return AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT GOPER.CODEMPRESA, GOPER.CODOPER, GOPER.CODTIPOPER, GOPER.NUMERO
                    FROM GOPERRELAC, GOPER
                    WHERE GOPERRELAC.CODEMPRESA = ? AND GOPERRELAC.CODOPER = ?
                    AND GOPERRELAC.CODEMPRESA = GOPER.CODEMPRESA
                    AND GOPERRELAC.CODOPERRELAC = GOPER.CODOPER", CodEmpresa, CodOper);
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
        private void IncluirRelacionamentoNivel1(DataTable dt)
        {
            PS.Lib.DataField dfCODEMPRESA = new PS.Lib.DataField();
            PS.Lib.DataField dfCODOPER = new PS.Lib.DataField();
            PS.Lib.DataField dfCODTIPOPER = new PS.Lib.DataField();
            PS.Lib.DataField dfNUMERO = new PS.Lib.DataField();

            List<DataField> objArrRelac = new List<DataField>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
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
    }
}
