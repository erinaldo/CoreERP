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
    public partial class FormGTIPOPERREPORT2Cadastro : AppLib.Windows.FormCadastroData
    {
        public FormGTIPOPERREPORT2Cadastro()
        {
            InitializeComponent();
        }

        private void FormGTIPOPERREPORT2Cadastro_Load(object sender, EventArgs e)
        {

        }

        private bool campoLookup1_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta = @"
SELECT IDREPORT, NOME
FROM ZREPORT
WHERE CODREPORTTIPO = 'OPERACAO'";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookup1, consulta, new Object[] { });
        }
    }
}
