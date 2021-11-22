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
    public partial class FormCobrancaCadastro : AppLib.Windows.FormCadastroData
    {
        public FormCobrancaCadastro()
        {
            InitializeComponent();
        }

        private void FormCobrancaCadastro_Load(object sender, EventArgs e)
        {

        }

        private void campoLookupCODCLIFOR_SetDescricao(object sender, EventArgs e)
        {
            String consulta = @"SELECT NOME FROM VCLIFOR WHERE CODEMPRESA = ? AND CODCLIFOR = ?";
            AppLib.Windows.CampoLookup.Descricao(campoLookupCODCLIFOR, consulta, new Object[] { AppLib.Context.Empresa, campoLookupCODCLIFOR.Get() });
        }

        private void campoLookupCODCONTA_SetDescricao(object sender, EventArgs e)
        {
            String consulta = @"SELECT DESCRICAO FROM FCONTA WHERE CODEMPRESA = ? AND CODCONTA = ?";
            AppLib.Windows.CampoLookup.Descricao(campoLookupCODCONTA, consulta, new Object[] { AppLib.Context.Empresa, campoLookupCODCONTA.Get() });
        }

        private void campoLookupCODTIPDOC_SetDescricao(object sender, EventArgs e)
        {
            String consulta = @"SELECT NOME FROM FTIPDOC WHERE CODEMPRESA = ? AND CODTIPDOC = ?";
            AppLib.Windows.CampoLookup.Descricao(campoLookupCODTIPDOC, consulta, new Object[] { AppLib.Context.Empresa, campoLookupCODTIPDOC.Get() });
        }

        private void campoLookupCODCONVENIO_SetDescricao(object sender, EventArgs e)
        {
            String consulta = @"SELECT DESCRICAO FROM FCONVENIO WHERE CODEMPRESA = ? AND CODCONVENIO = ?";
            AppLib.Windows.CampoLookup.Descricao(campoLookupCODCONVENIO, consulta, campoLookupCODCONVENIO.Get());
        }
    }
}
