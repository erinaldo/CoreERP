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
    public partial class FrmBaseFiltroRenomear : DevExpress.XtraEditors.XtraForm
    {
        Filter FiltroSelecionado;
        FrmBaseFiltro FrmBaseFiltro1;
        bool Copia;

        public FrmBaseFiltroRenomear(Filter objFiltro, FrmBaseFiltro objFrmBaseFiltro, bool parCopia)
        {
            InitializeComponent();
            FiltroSelecionado = objFiltro;
            FrmBaseFiltro1 = objFrmBaseFiltro;
            Copia = parCopia;
        }

        private void FrmBaseFiltroRenomear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                this.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBaseFiltroRenomear_Load(object sender, EventArgs e)
        {
            textBox1.Text = FiltroSelecionado.descricao;
            textBox1.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string nomeAnterior = "";
            nomeAnterior = FiltroSelecionado.descricao;

            if (textBox1.Text == FiltroSelecionado.descricao)
            {
                if (Copia)
                {

                }
                else
                {
                    this.Close();
                    return;
                }
            }

            if (!Copia)
            {
                FiltroSelecionado.descricao = textBox1.Text;
            }

            try
            {
                if (Copia)
                {
                    Filter ftCopia = (Filter)FiltroSelecionado.Clone();

                    ftCopia.descricao = textBox1.Text;            
                    ftCopia.id = FiltroSelecionado.BuscaProximoId();
                    for (int i = 0; i < FiltroSelecionado.listaCondicao.Count; i++)
                    {
                        ftCopia.listaCondicao[i].idFiltro = ftCopia.id;
                    }

                    ftCopia.Salvar();
                    ftCopia.BuscaCondicao();

                    FrmBaseFiltro1.ListaDeFiltros.Add(ftCopia);
                }
                else
                {
                    FiltroSelecionado.Salvar();
                    FiltroSelecionado.BuscaCondicao();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Menssagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                return;
            }
        }

        private void FrmBaseFiltroRenomear_KeyDown(object sender, KeyEventArgs e)
        {
            Keys flag = e.KeyCode;

            if (flag.Equals(Keys.Enter))
            {
                SelectNextControl(ActiveControl, !e.Shift, true, true, true);
                e.Handled = true;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button6_Click(this, null);
            }
        }

    }
}
