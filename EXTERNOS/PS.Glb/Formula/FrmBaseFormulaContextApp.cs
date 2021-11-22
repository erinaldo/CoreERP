using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PS.Lib;

namespace PS.Glb.Formula
{
    public partial class FrmBaseFormulaContextApp : DevExpress.XtraEditors.XtraForm
    {
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private Global gb = new Global();
        private Constantes ct = new Constantes();

        public FrmBaseFormulaContextApp()
        {
            InitializeComponent();

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = 0;
            list1[0].DisplayMember = "Operação";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = 1;
            list1[1].DisplayMember = "Operação - Item";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = 2;
            list1[2].DisplayMember = "Lançamento";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";
        }

        private void FrmBaseApp_KeyDown(object sender, KeyEventArgs e)
        {
            Keys flag = e.KeyCode;

            if (flag.Equals(Keys.Enter))
            {
                SelectNextControl(ActiveControl, !e.Shift, true, true, true);
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBaseApp_Load(object sender, EventArgs e)
        {
            this.Text = "Formula - Seleção de Contexto";

            TabPage tab = new TabPage();

            tab = tabControl1.SelectedTab;

            tab.Text = this.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Execute();
        }

        public virtual void Execute()
        {
            List<DataField> objArr = new List<DataField>();

            //Operação
            if (psComboBox1.Value.ToString() == "0")
            {
                objArr = gb.RetornaDataFieldByDataGridViewRow(dataGridView1.Rows[dataGridView1.CurrentRow.Index]);
                //PS.Lib.Contexto.Session.Current = objArr;
                
                PS.Lib.Contexto.Session.key1 = PS.Lib.Contexto.Session.Empresa.CodEmpresa;
                PS.Lib.Contexto.Session.key2 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CODOPER"].Value;
            }

            //Operação - Item
            if (psComboBox1.Value.ToString() == "1")
            {
                objArr = gb.RetornaDataFieldByDataGridViewRow(dataGridView2.Rows[dataGridView2.CurrentRow.Index]);
                //PS.Lib.Contexto.Session.Current = objArr;

                PS.Lib.Contexto.Session.key1 = PS.Lib.Contexto.Session.Empresa.CodEmpresa;
                PS.Lib.Contexto.Session.key2 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CODOPER"].Value;
                PS.Lib.Contexto.Session.key3 = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["NSEQITEM"].Value;
            }

            //Lançamento
            if (psComboBox1.Value.ToString() == "2")
            {
                objArr = gb.RetornaDataFieldByDataGridViewRow(dataGridView1.Rows[dataGridView1.CurrentRow.Index]);
                //PS.Lib.Contexto.Session.Current = objArr;

                PS.Lib.Contexto.Session.key1 = PS.Lib.Contexto.Session.Empresa.CodEmpresa;
                PS.Lib.Contexto.Session.key2 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CODLANCA"].Value;
            }

            this.Close();
        }

        private void AlteraNomeColuna(int chave)
        {
            if (chave == 0 || chave == 1)
            {
                DataTable dt = gb.NomeDosCampos("GOPER");

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dataGridView1.Columns[i].Name == dt.Rows[j]["COLUNA"].ToString())
                            {
                                dataGridView1.Columns[i].HeaderText = dt.Rows[j]["DESCRICAO"].ToString();
                            }
                        }
                    }
                }

                if (chave == 1)
                {
                    DataTable dt1 = gb.NomeDosCampos("GOPERITEM");

                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dataGridView2.ColumnCount; i++)
                        {
                            for (int j = 0; j < dt1.Rows.Count; j++)
                            {
                                if (dataGridView2.Columns[i].Name == dt1.Rows[j]["COLUNA"].ToString())
                                {
                                    dataGridView2.Columns[i].HeaderText = dt1.Rows[j]["DESCRICAO"].ToString();
                                }
                            }
                        }
                    }
                }
            }

            if (chave == 2)
            {
                DataTable dt = gb.NomeDosCampos("FLANCA");

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dataGridView1.Columns[i].Name == dt.Rows[j]["COLUNA"].ToString())
                            {
                                dataGridView1.Columns[i].HeaderText = dt.Rows[j]["DESCRICAO"].ToString();
                            }
                        }
                    }
                }
            }        
        }

        private void CarregaGrid(int chave)
        {
            string sSql = string.Empty;

            if (chave == 0 || chave == 1)
            {
                sSql = @" SELECT CODOPER, CODTIPOPER, NUMERO, CODCLIFOR FROM GOPER WHERE CODEMPRESA = ?";

                dataGridView1.DefaultCellStyle.SelectionBackColor = ct.corSelecaoGrid;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ct.corImparGrid;

                dataGridView1.DataSource = dbs.QuerySelect(sSql, PS.Lib.Contexto.Session.Empresa.CodEmpresa);

                AlteraNomeColuna(chave);

                if (chave == 1)
                {
                    CarregaGridDetail(chave);                                    
                }
            }

            if (chave == 2)
            {
                sSql = @" SELECT CODLANCA, NUMERO, CODCLIFOR FROM FLANCA WHERE CODEMPRESA = ?";

                dataGridView1.DefaultCellStyle.SelectionBackColor = ct.corSelecaoGrid;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ct.corImparGrid;

                dataGridView1.DataSource = dbs.QuerySelect(sSql, PS.Lib.Contexto.Session.Empresa.CodEmpresa);

                AlteraNomeColuna(chave);
            }
        }

        private void CarregaGridDetail(int chave)
        {
            //Operação - Item
            if (chave == 1)
            {
                string sSql = @" SELECT NSEQITEM, CODPRODUTO FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?";

                dataGridView2.DefaultCellStyle.SelectionBackColor = ct.corSelecaoGrid;
                dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = ct.corImparGrid;

                dataGridView2.DataSource = dbs.QuerySelect(sSql, PS.Lib.Contexto.Session.Empresa.CodEmpresa, dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CODOPER"].Value);

                AlteraNomeColuna(chave);
            }
        
        }

        private void psComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            //Operação
            if (psComboBox1.Value.ToString() == "0")
            {
                panel4.Visible = true;
                panel4.Dock = DockStyle.Fill;
                panel5.Visible = false;
                panel5.Dock = DockStyle.None;

                CarregaGrid(0);
            }

            //Operação - Item
            if (psComboBox1.Value.ToString() == "1")
            {
                panel4.Visible = true;
                panel4.Dock = DockStyle.Left;
                panel5.Visible = true;
                panel5.Dock = DockStyle.Right;

                CarregaGrid(1);
            }

            //Lançamento
            if (psComboBox1.Value.ToString() == "2")
            {
                panel4.Visible = true;
                panel4.Dock = DockStyle.Fill;
                panel5.Visible = false;
                panel5.Dock = DockStyle.None;

                CarregaGrid(2);
            }   
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                //Operação - Item
                if (psComboBox1.Value.ToString() == "1")
                {
                    CarregaGridDetail(1);
                }                
            }
        }
    }
}
