using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PS.Lib;

namespace ERP
{
    public partial class FormSelecaoEmpresa : DevExpress.XtraEditors.XtraForm
    {
        private PS.Lib.Login login = new PS.Lib.Login();
        private Global gb = new Global();
        private const int MF_BYPOSITION = 0x400;

        [DllImport("User32")]
        private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("User32")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("User32")]
        private static extern int GetMenuItemCount(IntPtr hWnd);

        public FormSelecaoEmpresa()
        {
            InitializeComponent();
        }

        private void FrmSelecionaEmpresa_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuItemCount = GetMenuItemCount(hMenu);
            RemoveMenu(hMenu, MenuItemCount - 1, MF_BYPOSITION);
            //DrawMenuBar(hWnd);

            Inicializacao();
            CarregaGrid();
        }

        private void AlteraNomeColuna()
        {
            DataTable dt = new DataTable();
            dt = gb.NomeDosCampos("GEMPRESA");

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

        private void DesabilitaColua()
        {
            dataGridView1.Columns["CODCONTROLE"].Visible = false;
            dataGridView1.Columns["CODCHAVE1"].Visible = false;
            dataGridView1.Columns["CODCHAVE2"].Visible = false;
        }

        private void Inicializacao()
        {
            if (login.InitializeFile())
                bntIni.Visible = false;
            else
                bntIni.Visible = true;
        }

        private void CarregaGrid()
        {
            dataGridView1.DataSource = login.EmpresaUserList();
            AlteraNomeColuna();
            DesabilitaColua();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bntIni_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Arquivo arq = new Arquivo();
                    XMLParse xml = new XMLParse();
                    Empresa emp = new Empresa();

                    emp = (Empresa)xml.Read(arq.Ler(openFileDialog1.FileName), new Empresa());

                    if (emp.VaidateCod())
                    {
                        if (emp.ImportCod())
                        {
                            PSMessageBox.ShowInfo("Importação realizada com sucesso.");
                            Inicializacao();
                            CarregaGrid();
                        }
                    }
                    else
                    {
                        PSMessageBox.ShowError("Código de Controle Inválido");
                    }
                }
                catch (Exception ex)
                {
                    PS.Lib.PSMessageBox.ShowError(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                if (dataGridView1.Rows != null)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Selected)
                        {
                            Empresa emp = new Empresa();
                            emp.CodEmpresa = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                            emp.NomeFantasia = dataGridView1.Rows[i].Cells[1].Value.ToString();
                            emp.Nome = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            emp.CNPJCPF = dataGridView1.Rows[i].Cells[3].Value.ToString();
                            emp.InscricaoEstadual = dataGridView1.Rows[i].Cells[4].Value.ToString();
                            emp.CodControle = dataGridView1.Rows[i].Cells[5].Value.ToString();
                            emp.CodChave1 = dataGridView1.Rows[i].Cells[6].Value.ToString();
                            emp.CodChave2 = dataGridView1.Rows[i].Cells[7].Value.ToString();
                            
                            Contexto.Session.Empresa = emp;
                            Contexto.Session.Empresa.GetPerfilList();
                        }
                    }
                }
            }

            this.Close();
        }

        private void FrmSelecionaEmpresa_KeyDown(object sender, KeyEventArgs e)
        {
            Keys flag = e.KeyCode;

            if (flag.Equals(Keys.Enter))
            {
                SelectNextControl(ActiveControl, !e.Shift, true, true, true);
                e.Handled = true;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button1_Click(this, null);
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }
    }
}
