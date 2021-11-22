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
    public partial class FormTipoChequeVisao : AppLib.Windows.FormVisao
    {
        private static FormTipoChequeVisao _instance = null;

        public static FormTipoChequeVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormTipoChequeVisao();

            return _instance;
        }

        private void FormTipoChequeVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormTipoChequeVisao()
        {
            InitializeComponent();
        }

        private void FormTipoChequeVisao_Load(object sender, EventArgs e)
        {

        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormTipoChequeCadastro f = new FormTipoChequeCadastro();
            f.Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormTipoChequeCadastro f = new FormTipoChequeCadastro();
            f.Editar(grid1);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            FormTipoChequeCadastro f = new FormTipoChequeCadastro();
            f.Excluir(grid1);
        }

    }
}
