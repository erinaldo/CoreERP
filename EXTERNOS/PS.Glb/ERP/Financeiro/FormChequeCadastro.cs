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
    public partial class FormChequeCadastro : AppLib.Windows.FormCadastroData
    {
        bool buscaCheque = false;

        public FormChequeCadastro()
        {
            InitializeComponent();
        }

        private void FormChequeCadastro_Load(object sender, EventArgs e)
        {
            campoInteiroCODEMPRESA.Set(AppLib.Context.Empresa);
        }

        private bool campoLookupCODCONTA_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta = @"SELECT * FROM FCONTA WHERE CODEMPRESA = ?";
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODCONTA, consulta, new Object[] { AppLib.Context.Empresa });
        }

        private void campoLookupCODCONTA_SetDescricao(object sender, EventArgs e)
        {
            String consulta = @"SELECT DESCRICAO FROM FCONTA WHERE CODEMPRESA = ? AND CODCONTA = ?";
            AppLib.Windows.CampoLookup.Descricao(campoLookupCODCONTA, consulta, new Object[] { AppLib.Context.Empresa, campoLookupCODCONTA.Get() });
        }

        public void BuscaProximoNumeroCheque()
        {
            if (campoListaPAGREC.Get().Equals("1"))
            {
                campoInteiroNUMERO.Set(null);
                campoInteiroNUMERO.textEdit1.ReadOnly = false;
            }

            if (campoListaPAGREC.Get().Equals("0"))
            {
                campoInteiroNUMERO.textEdit1.ReadOnly = false;

                int? numero = 1;

                try
                {
                    String consulta = "SELECT NUMEROCHEQUE FROM FCONTA WHERE CODCONTA = ? AND CODEMPRESA = ?";
                    int CODEMPRESA = (int)campoInteiroCODEMPRESA.Get();
                    String CODCONTA = campoLookupCODCONTA.Get();
                    numero = int.Parse(AppLib.Context.poolConnection.Get("Start").ExecGetField(1, consulta, new Object[] { CODCONTA, CODEMPRESA }).ToString());
                }
                catch
                {
                    numero = null;
                }
                campoInteiroNUMERO.Set(numero + 1);
                buscaCheque = true;
            }
        }

        private void campoLookupCODCONTA_AposSelecao(object sender, EventArgs e)
        {
            if (buscaCheque == false)
            {
                this.BuscaProximoNumeroCheque();
            }

        }

        private void campoListaPAGREC_AposSelecao(object sender, EventArgs e)
        {
            if (buscaCheque == false)
            {
                this.BuscaProximoNumeroCheque();
            }
        }


        private void FormChequeCadastro_AposSalvar(object sender, EventArgs e)
        {
        }


    }
}
