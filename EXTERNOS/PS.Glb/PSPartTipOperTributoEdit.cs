using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartTipOperTributoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public PSPartTipOperTributoEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartTributo";
            psLookup2.PSPart = "PSPartFormula";
            PSComboCODQUERY.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODQUERY, DESCRICAO FROM GQUERY WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa});
            PSComboCODQUERY.ValueMember = "CODQUERY";
            PSComboCODQUERY.DisplayMember = "DESCRICAO";
            
            


        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            //psLookup2.Chave = false;
            //psMoedaBox2.Chave = false;
        }

        public override void CarregaRegistro()
        {
            base.CarregaRegistro();

            //psLookup2.Chave = false;
            //psMoedaBox2.Chave = false;
        }
    }
}
