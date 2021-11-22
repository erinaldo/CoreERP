using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartOrdemProducaoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public PSPartOrdemProducaoEdit()
        {
            InitializeComponent();

            psLookup14.PSPart = "PSPartUsuario";

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = 0;
            list1[0].DisplayMember = "Aberto";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = 1;
            list1[1].DisplayMember = "Iniciado";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = 2;
            list1[2].DisplayMember = "Finalizado";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[3].ValueMember = 3;
            list1[3].DisplayMember = "Cancalado";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            //Aqui vai as definições dos parametros do tipo da operação
            DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();

            #region Código Cliente/Fornecedor

            //Usa Código de Cliente/Fornecedor Sequencial
            if (int.Parse(PARAMVAREJO["CODORDEMUSANUMEROSEQ"].ToString()) == 1)
            {
                psTextoBox1.Edita = false;
            }

            #endregion

            psComboBox1.Chave = false;
            psDateBox1.Chave = false;
            psLookup14.Chave = false;
            psLookup14.Text = PS.Lib.Contexto.Session.CodUsuario;
            psLookup14.LoadLookup();
        }

        public override void CarregaRegistro()
        {
            base.CarregaRegistro();

            psComboBox1.Chave = false;
            psDateBox1.Chave = false;
            psLookup14.Chave = false;
        }
    }
}
