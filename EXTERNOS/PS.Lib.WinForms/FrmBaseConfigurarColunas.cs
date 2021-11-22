using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PS.Lib;
using PS.Lib.WinForms;

namespace PS.Lib.WinForms
{
    public partial class FrmBaseConfigurarColunas : DevExpress.XtraEditors.XtraForm
    {
        // OBJETOS
        Global gb = new Global();

        public string tabela;
        public List<String> campos;
        public bool atualiza;
        public List<PSFilter> defaultFilter;

        private DataTable Dados;

        public FrmBaseConfigurarColunas()
        {
            InitializeComponent();
        }

        private void ConfigurarColunas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                this.Close();
            }
        }

        private void RemoveColunaFiltroPadrao()
        {
            for (int i = 0; i < defaultFilter.Count; i++)
            {
                for (int j = 0; j < Dados.Rows.Count; j++)
                {
                    if (defaultFilter[i].Field == Dados.Rows[j]["COLUNA"].ToString())
                    {
                        Dados.Rows.RemoveAt(j);
                    }
                }
            }
        }

        private void CarregaTabela()
        {
            if (campos.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = gb.NomeDosCampos(tabela);
                Dados = dt.Copy();
                Dados.Clear();

                for (int i = 0; i < campos.Count; i++)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (campos[i] == dt.Rows[j]["COLUNA"].ToString())
                        {
                            DataRow dr = Dados.NewRow();
                            dr["DESCRICAO"] = dt.Rows[j]["DESCRICAO"];
                            dr["COLUNA"] = dt.Rows[j]["COLUNA"];
                            dr["CHECKED"] = dt.Rows[j]["CHECKED"];

                            Dados.Rows.Add(dr);
                        }
                    }
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!campos.Contains(dt.Rows[i]["COLUNA"].ToString()))
                    {
                        DataRow dr = Dados.NewRow();
                        dr["DESCRICAO"] = dt.Rows[i]["DESCRICAO"];
                        dr["COLUNA"] = dt.Rows[i]["COLUNA"];
                        dr["CHECKED"] = dt.Rows[i]["CHECKED"];

                        Dados.Rows.Add(dr);
                    }
                }

                RemoveColunaFiltroPadrao();
            }
            else
            {
                Dados = gb.NomeDosCampos(tabela);
                RemoveColunaFiltroPadrao();
            }
        }

        private void ColunasVisíveis()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemCheckState(i) == CheckState.Unchecked)
                {
                    Dados.Rows[i]["CHECKED"] = 0;
                }
            }

            campos.Clear();

            for (int i = 0; i < Dados.Rows.Count; i++)
            {
                if (int.Parse(Dados.Rows[i]["CHECKED"].ToString()) == 1)
                {
                    campos.Add(Dados.Rows[i]["COLUNA"].ToString());
                }
            }
        }

        private void MarcaDesmarca()
        {
            if (campos.Count > 0)
            {
                for (int i = 0; i < campos.Count; i++)
                {
                    for (int j = 0; j < Dados.Rows.Count; j++)
                    {
                        if (campos[i] == Dados.Rows[j]["COLUNA"].ToString())
                        {
                            Dados.Rows[j]["CHECKED"] = 1;
                            checkedListBox1.SetItemChecked(j, true);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < Dados.Rows.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
        }

        private void CarregaCamposDisponíveis(DataTable dt)
        {
            checkedListBox1.DataSource = null;
            checkedListBox1.Items.Clear();

            checkedListBox1.DataSource = dt;

            checkedListBox1.DisplayMember = "DESCRICAO";
            checkedListBox1.ValueMember = "COLUNA";
        }

        private void ConfigurarColunas_Load(object sender, EventArgs e)
        {
            CarregaTabela();
            CarregaCamposDisponíveis(Dados);
            MarcaDesmarca();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            atualiza = false;
            this.Close();
        }

        private void Movimentar(string valor)
        {
            // valor = "C" movimentar para cima
            // valor = "B" movimentar para baixo

            DataTable dt = new DataTable();
            dt = Dados;

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemCheckState(i) == CheckState.Unchecked)
                {
                    dt.Rows[i]["CHECKED"] = 0;
                }
            }

            int Indice = checkedListBox1.SelectedIndex;
            string Coluna = checkedListBox1.SelectedValue.ToString();
            string Descricao = checkedListBox1.Text;
            int Check = 0;

            if (checkedListBox1.GetItemCheckState(Indice) == CheckState.Checked)
                Check = 1;
            else
                Check = 0;

            if (valor == "C")
            {
                // Modifica linha movimentada
                dt.Rows[Indice]["DESCRICAO"] = dt.Rows[Indice - 1]["DESCRICAO"];
                dt.Rows[Indice]["COLUNA"] = dt.Rows[Indice - 1]["COLUNA"];
                dt.Rows[Indice]["CHECKED"] = dt.Rows[Indice - 1]["CHECKED"];

                // Modifica linha destino do movimento
                dt.Rows[Indice - 1]["DESCRICAO"] = Descricao;
                dt.Rows[Indice - 1]["COLUNA"] = Coluna;
                dt.Rows[Indice - 1]["CHECKED"] = Check;

                checkedListBox1.SelectedIndex = Indice - 1;
            }

            if (valor == "B")
            {
                // Modifica linha movimentada
                dt.Rows[Indice]["DESCRICAO"] = dt.Rows[Indice + 1]["DESCRICAO"];
                dt.Rows[Indice]["COLUNA"] = dt.Rows[Indice + 1]["COLUNA"];
                dt.Rows[Indice]["CHECKED"] = dt.Rows[Indice + 1]["CHECKED"];

                // Modifica linha destino do movimento
                dt.Rows[Indice + 1]["DESCRICAO"] = Descricao;
                dt.Rows[Indice + 1]["COLUNA"] = Coluna;
                dt.Rows[Indice + 1]["CHECKED"] = Check;

                checkedListBox1.SelectedIndex = Indice + 1;
            }

            Dados = dt;
            CarregaCamposDisponíveis(Dados);

            for (int i = 0; i < Dados.Rows.Count; i++)
            {
                if (int.Parse(Dados.Rows[i]["CHECKED"].ToString()) == 1)
                    checkedListBox1.SetItemChecked(i, true);
                else
                    checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex > 0)
            {
                Movimentar("C");
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            if ((checkedListBox1.SelectedIndex + 1) < Dados.Rows.Count)
            {
                Movimentar("B");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            atualiza = true;
            ColunasVisíveis();

            if (campos.Count <= 0)
            {
                PSMessageBox.ShowInfo("Ao menos uma coluna deve ser selecionada.");
                return;
            }
            else
            {
                this.Close();
            }
        }
    }
}