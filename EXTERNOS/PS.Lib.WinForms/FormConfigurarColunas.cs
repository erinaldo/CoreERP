using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib.WinForms
{
    public partial class FormConfigurarColunas : Form
    {
        private String VISAO { get; set; }
        private String CODUSUARIO { get; set; }
        private System.Windows.Forms.DataGridView dataGridView1;
        public List<GUSUARIOVISAO> listaOriginal { get; set; }
        private List<GUSUARIOVISAO> listaAtual { get; set; }

        public FormConfigurarColunas()
        {
            InitializeComponent();
        }

        public FormConfigurarColunas(String _VISAO, String _CODUSUARIO, System.Windows.Forms.DataGridView _dataGridView1)
        {
            InitializeComponent();
            VISAO = _VISAO;
            CODUSUARIO = _CODUSUARIO;
            dataGridView1 = _dataGridView1;
        }

        private void FormConfigurarColunas_Load(object sender, EventArgs e)
        {
            this.Carregar();
        }

        public void Carregar()
        {
            #region CARREGA LISTA ORIGINAL

            String consulta1 = @"
SELECT *
FROM GUSUARIOVISAO
WHERE VISAO = ?
  AND CODUSUARIO = ?
ORDER BY VISIVEL DESC, SEQUENCIA, COLUNA";

            System.Data.DataTable dt1 = AppLib.Context.poolConnection.Get("Start").ExecQuery(consulta1, new Object[] { VISAO, CODUSUARIO });

            listaOriginal = new List<GUSUARIOVISAO>();

            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    GUSUARIOVISAO item = new GUSUARIOVISAO();

                    item.VISAO = VISAO;
                    item.CODUSUARIO = CODUSUARIO;
                    item.SEQUENCIA = int.Parse(dt1.Rows[i]["SEQUENCIA"].ToString());
                    item.COLUNA = dt1.Rows[i]["COLUNA"].ToString();
                    item.LARGURA = int.Parse(dt1.Rows[i]["LARGURA"].ToString());
                    item.VISIVEL = int.Parse(dt1.Rows[i]["VISIVEL"].ToString());

                    listaOriginal.Add(item);
                }
            }

            #region ADICIONA AO OBJECT LIST NOVAS COLUNAS (COLUNAS QUE NÃO ESTÃO NAS CONFIGURAÇÕES)

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                Boolean existe = false;

                for (int x = 0; x < listaOriginal.Count; x++)
                {
                    if (listaOriginal[x].COLUNA.ToUpper().Equals(dataGridView1.Columns[i].Name.ToUpper()))
                    {
                        existe = true;
                    }
                }

                if (!existe)
                {
                    GUSUARIOVISAO item = new GUSUARIOVISAO();

                    item.VISAO = VISAO;
                    item.CODUSUARIO = CODUSUARIO;
                    item.SEQUENCIA = listaOriginal.Count;
                    item.COLUNA = dataGridView1.Columns[i].Name;
                    item.LARGURA = 100;
                    item.VISIVEL = 1;

                    listaOriginal.Add(item);
                }
            }

            #endregion

            #endregion

            listaAtual = new List<GUSUARIOVISAO>();

            for (int i = 0; i < listaOriginal.Count; i++)
            {
                listaAtual.Add(listaOriginal[i]);

                if (listaOriginal[i].VISIVEL == 1)
                {
                    checkedListBox1.Items.Add(listaOriginal[i].COLUNA, true);
                }
                else
                {
                    checkedListBox1.Items.Add(listaOriginal[i].COLUNA, false);
                }
            }
        }

        private void SobrepoeListaAtual(List<GUSUARIOVISAO> listaTemp)
        {
            listaAtual.Clear();
            checkedListBox1.Items.Clear();

            for (int i = 0; i < listaTemp.Count; i++)
            {
                listaAtual.Add(listaTemp[i]);

                if (listaTemp[i].VISIVEL == 1)
                {
                    checkedListBox1.Items.Add(listaTemp[i].COLUNA, true);
                }
                else
                {
                    checkedListBox1.Items.Add(listaTemp[i].COLUNA, false);
                }
            }
        }

        private void pictureBoxPRIMEIRO_Click(object sender, EventArgs e)
        {
            try
            {
                List<GUSUARIOVISAO> listaTemp = new List<GUSUARIOVISAO>();

                listaTemp.Add(listaAtual[checkedListBox1.SelectedIndex]);

                for (int i = 0; i < listaAtual.Count; i++)
                {
                    if (i != checkedListBox1.SelectedIndex)
                    {
                        listaTemp.Add(listaAtual[i]);
                    }
                }

                this.SobrepoeListaAtual(listaTemp);

                checkedListBox1.Focus();
                checkedListBox1.SelectedIndex = 0;
            }
            catch { }
        }

        private void pictureBoxSUBIR1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBox1.SelectedIndex != 0)
                {
                    List<GUSUARIOVISAO> listaTemp = new List<GUSUARIOVISAO>();

                    for (int i = 0; i < checkedListBox1.SelectedIndex - 1; i++)
                    {
                        if (i != checkedListBox1.SelectedIndex)
                        {
                            listaTemp.Add(listaAtual[i]);
                        }
                    }

                    listaTemp.Add(listaAtual[checkedListBox1.SelectedIndex]);

                    listaTemp.Add(listaAtual[checkedListBox1.SelectedIndex - 1]);

                    for (int i = checkedListBox1.SelectedIndex + 1; i < listaAtual.Count; i++)
                    {
                        if (i != checkedListBox1.SelectedIndex)
                        {
                            listaTemp.Add(listaAtual[i]);
                        }
                    }

                    int selecionarIndice = checkedListBox1.SelectedIndex - 1;
                    
                    this.SobrepoeListaAtual(listaTemp);

                    checkedListBox1.Focus();
                    checkedListBox1.SelectedIndex = selecionarIndice;
                }
            }
            catch { }
        }

        private void pictureBoxDESCER1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBox1.SelectedIndex != checkedListBox1.Items.Count)
                {
                    List<GUSUARIOVISAO> listaTemp = new List<GUSUARIOVISAO>();

                    for (int i = 0; i < checkedListBox1.SelectedIndex; i++)
                    {
                        if (i != checkedListBox1.SelectedIndex)
                        {
                            listaTemp.Add(listaAtual[i]);
                        }
                    }

                    listaTemp.Add(listaAtual[checkedListBox1.SelectedIndex + 1]);

                    listaTemp.Add(listaAtual[checkedListBox1.SelectedIndex]);

                    for (int i = checkedListBox1.SelectedIndex + 2; i < listaAtual.Count; i++)
                    {
                        if (i != checkedListBox1.SelectedIndex)
                        {
                            listaTemp.Add(listaAtual[i]);
                        }
                    }

                    int selecionarIndice = checkedListBox1.SelectedIndex + 1;

                    this.SobrepoeListaAtual(listaTemp);

                    checkedListBox1.Focus();
                    checkedListBox1.SelectedIndex = selecionarIndice;
                }
            }
            catch { }
        }

        private void pictureBoxULTIMO_Click(object sender, EventArgs e)
        {
            try
            {
                List<GUSUARIOVISAO> listaTemp = new List<GUSUARIOVISAO>();

                for (int i = 0; i < listaAtual.Count; i++)
                {
                    if (i != checkedListBox1.SelectedIndex)
                    {
                        listaTemp.Add(listaAtual[i]);
                    }
                }

                listaTemp.Add(listaAtual[checkedListBox1.SelectedIndex]);

                this.SobrepoeListaAtual(listaTemp);

                checkedListBox1.Focus();
                checkedListBox1.SelectedIndex = listaAtual.Count - 1;
            }
            catch { }
        }

        private void buttonSALVAR_Click(object sender, EventArgs e)
        {
            String comando1 = "DELETE GUSUARIOVISAO WHERE VISAO = ? AND CODUSUARIO = ?";
            int temp1 = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comando1, new Object[] { VISAO, CODUSUARIO });

            for (int i = 0; i < listaAtual.Count; i++)
            {
                GUSUARIOVISAO item = new GUSUARIOVISAO();

                item.VISAO = VISAO;
                item.CODUSUARIO = CODUSUARIO;
                item.SEQUENCIA = i;
                item.COLUNA = listaAtual[i].COLUNA;
                item.LARGURA = listaAtual[i].LARGURA;
                item.VISIVEL = listaAtual[i].VISIVEL;

                String comando2 = "INSERT GUSUARIOVISAO ( VISAO, CODUSUARIO, SEQUENCIA, COLUNA, LARGURA, VISIVEL ) VALUES ( ?, ?, ?, ?, ?, ? )";
                int temp2 = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comando2, new Object[] { item.VISAO, item.CODUSUARIO, item.SEQUENCIA, item.COLUNA, item.LARGURA, item.VISIVEL });

                if (temp2 != 1)
                {
                    MessageBox.Show("Erro ao salvar configuração de visão por usuário.");
                }
            }

            this.Close();
        }

        private void buttonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                listaAtual[e.Index].VISIVEL = 1;
            }
            else
            {
                listaAtual[e.Index].VISIVEL = 0;
            }
        }

        private void pictureBoxTODOS_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void pictureBoxNENHUM_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

    }
}
