using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.ERP.Comercial
{
    public partial class FormRegraCFOPVisao : AppLib.Windows.FormVisao
    {
        private static FormRegraCFOPVisao _instance = null;

        public static FormRegraCFOPVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormRegraCFOPVisao();

            return _instance;

        }
        private void FormRegraCFOPVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormRegraCFOPVisao()
        {
            InitializeComponent();
        }

        private void FormRegraCFOPVisao_Load(object sender, EventArgs e)
        {

        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormRegraCFOPCadastro f = new FormRegraCFOPCadastro();
            f.Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormRegraCFOPCadastro f = new FormRegraCFOPCadastro();
            f.Editar(grid1);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            FormRegraCFOPCadastro f = new FormRegraCFOPCadastro();
            f.Excluir(grid1);
        }

    }
}
