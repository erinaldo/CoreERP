using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.ERP.Financeiro
{
    public partial class FormConvenioVisao : AppLib.Windows.FormVisao
    {
        private static FormConvenioVisao _instance = null;

        public static FormConvenioVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormConvenioVisao();

            return _instance;
        }

        private void FormConvenioVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormConvenioVisao()
        {
            InitializeComponent();
        }

        private void FormConvenioVisao_Load(object sender, EventArgs e)
        {

        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            ERP.Financeiro.FormConvenioCadastro f = new FormConvenioCadastro();
            f.Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            ERP.Financeiro.FormConvenioCadastro f = new FormConvenioCadastro();
            f.Editar(grid1);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            ERP.Financeiro.FormConvenioCadastro f = new FormConvenioCadastro();
            f.Excluir(grid1);
        }

        
    }
}
