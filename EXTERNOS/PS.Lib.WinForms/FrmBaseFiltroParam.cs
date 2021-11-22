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
    public partial class FrmBaseFiltroParam : DevExpress.XtraEditors.XtraForm
    {
        Filter FiltroSelecionado;

        public FrmBaseFiltroParam(Filter objFiltro)
        {
            InitializeComponent();

            FiltroSelecionado = objFiltro;
        }

        private void FrmBaseFiltroParam_Load(object sender, EventArgs e)
        {
            MontaFormulario();
        }

        private void MontaFormulario()
        {
            int contador = 0;
            this.Text = "Parametros do filtro: " + FiltroSelecionado.descricao;

            for (int i = 0; i < FiltroSelecionado.listaCondicao.Count; i++)
            {
                if (FiltroSelecionado.listaCondicao[i].ExisteParametro())
                {
                    AdicionarControle(TextoParametro(FiltroSelecionado.listaCondicao[i]), contador);
                    contador++;
                }
            }
        }

        private string TextoParametro(FilterCondition objCondicaoFiltro)
        {
            /*
            int indiceAbre = 0;
            int indiceFecha = 0;

            for (int i = 0; i < objCondicaoFiltro.valor.Length; i++)
            {
                if (objCondicaoFiltro.valor[i].ToString() == "[")
                {
                    indiceAbre = i;
                }

                if (objCondicaoFiltro.valor[i].ToString() == "]")
                {
                    indiceFecha = i;
                }
            }

            return objCondicaoFiltro.valor.Substring(indiceAbre + 1, indiceFecha - 1);
            */
            return string.Empty;
        }

        private void AdicionarControle(string texto, int numControle)
        {
            int eixoY = (numControle*37)+8;
            /*
            campoTexto ct = new campoTexto();
            ct.AutoSize = true;
            ct.Location = new System.Drawing.Point(9, eixoY);
            ct.Name = "campoTexto" + numControle.ToString();
            ct.Size = new System.Drawing.Size(260, 37);
            ct.TabIndex = 0;
            ct.Texto = texto;
            ct.Largura = 260;
            ct.Set();
            ct.AjustaTamanho();

            this.Controls.Add(ct);
            */
        }

        private void LimpaFormulario()
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i].ToString() == "FrameworkControls.campoTexto")
                {
                    // limpar o texto do campo
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LimpaFormulario();
        }
    }
}
