using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Visao
{
    public partial class frmVisaoTeste : Form
    {
        public frmVisaoTeste()
        {
            InitializeComponent();
            CarregaGrid();
        }

        private void CarregaGrid()
        {
            DataTable dtPai = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GNFESTADUAL", new object[] { });
            DataTable dtfilha = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODEMPRESA, CODOPER, CODSTATUS
                                                                                        FROM GNFESTADUALEVENTO
                                                                                        UNION ALL
                                                                                        SELECT CODEMPRESA, CODOPER, CODSTATUS
                                                                                        FROM GNFESTADUALCORRECAO 
                                                                                        ORDER BY CODOPER ", new object[] { });
            //DataTable dtfilha = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GNFESTADUALEVENTO", new object[] { });

            DataSet DS = new DataSet();

            dtPai.TableName = "GNFESTADUAL";
            dtfilha.TableName = "X";

            DS.Tables.Add(dtPai);
            DS.Tables.Add(dtfilha);

            //Relações entre as tabelas
            DataColumn[] _pai = new DataColumn[] {DS.Tables[0].Columns["CODEMPRESA"], DS.Tables[0].Columns["CODOPER"]};

            DataColumn[] _filha = new DataColumn[]{DS.Tables[1].Columns["CODEMPRESA"], DS.Tables[1].Columns["CODOPER"]};

            DS.Relations.Add("Evento", _pai, _filha);

            //Populando o Dataset
            gridControl1.DataSource = DS.Tables[0];

            DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { "GNFESTADUAL" });
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { "GNFESTADUAL", AppLib.Context.Usuario });
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                DataRow result = dic.Rows.Find(new object[] { gridView1.Columns[i].FieldName.ToString() });
                if (result != null)
                {
                    gridView1.Columns[i].Caption = result["DESCRICAO"].ToString();
                }
            }

        }

        private void gridView1_MasterRowExpanded(object sender, DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs e)
        {
            GridView dView = gridView1.GetDetailView(e.RowHandle, 0) as GridView;

            dView.Columns["CODEMPRESA"].Visible = false;
            dView.Columns["CODOPER"].Visible = false;

            DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA,DESCRICAO FROM GDICIONARIO WHERE TABELA in (?)", new object[] { "GNFESTADUALEVENTO" });
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { "GNFESTADUALEVENTO", AppLib.Context.Usuario });
            for (int i = 0; i < dView.Columns.Count; i++)
            {
                //dView.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                DataRow result = dic.Rows.Find(new object[] { dView.Columns[i].FieldName.ToString() });
                if (result != null)
                {
                    dView.Columns[i].Caption = result["DESCRICAO"].ToString();
                }
            }
            dView.BestFitColumns();
        }
    }
}
