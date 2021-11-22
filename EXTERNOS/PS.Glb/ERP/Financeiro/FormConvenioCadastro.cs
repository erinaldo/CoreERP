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
    public partial class FormConvenioCadastro : AppLib.Windows.FormCadastroData
    {
        public FormConvenioCadastro()
        {
            InitializeComponent();
        }

        private void FormConvenioCadastro_Load(object sender, EventArgs e)
        {

        }

        private bool campoLookupCODCONTA_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta = "SELECT * FROM FCONTA WHERE CODEMPRESA = ?";
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODCONTA, consulta, new Object[] { AppLib.Context.Empresa });
        }

        private void campoLookupCODCONTA_SetDescricao(object sender, EventArgs e)
        {
            String consulta = "SELECT DESCRICAO FROM FCONTA WHERE CODEMPRESA = ? AND CODCONTA = ?";
            AppLib.Windows.CampoLookup.Descricao(campoLookupCODCONTA, consulta, new Object[] { AppLib.Context.Empresa, campoLookupCODCONTA.Get() });
        }

        private void FormConvenioCadastro_AntesSalvar(object sender, EventArgs e)
        {
            campoInteiroCODEMPRESA.Set(AppLib.Context.Empresa);
        }
    }
}
