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
    public partial class FormRegraCFOPCadastro : AppLib.Windows.FormCadastroData
    {
        public FormRegraCFOPCadastro()
        {
            InitializeComponent();
        }

        private void FormRegraCFOPCadastro_Load(object sender, EventArgs e)
        {

        }

        private bool campoLookupUFDESTINO_SetFormConsulta(object sender, EventArgs e)
        {
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupUFDESTINO, "SELECT * FROM GESTADO", new Object[] { });
        }

        private bool campoLookupNCM_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta1 = @"
SELECT DISTINCT CODIGO, DESCRICAO
FROM VIBPTAX
WHERE LEN(CODIGO) >= 8";

            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupNCM, consulta1, new Object[] { });
        }

        private void campoLookupNCM_SetDescricao(object sender, EventArgs e)
        {
            String consulta1 = @"
SELECT TOP 1 DESCRICAO
FROM VIBPTAX
WHERE CODIGO = ?";

            AppLib.Windows.CampoLookup.Descricao(campoLookupNCM, consulta1, new Object[] { campoLookupNCM.Get() });
        }

        private void FormRegraCFOPCadastro_AntesSalvar(object sender, EventArgs e)
        {
            campoInteiroCODEMPRESA.Set(AppLib.Context.Empresa);
        }

        private void FormRegraCFOPCadastro_AposSalvar(object sender, EventArgs e)
        {
            String consulta1 = @"
SELECT COUNT(*)
FROM VREGRAVARCFOP
WHERE CODEMPRESA = ?
  AND CODFILIAL = ?
  AND NCM = ?";

            int contador1 = int.Parse(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, consulta1, new Object[] { campoInteiroCODEMPRESA.Get(), campoLookupCODFILIAL.Get(), campoLookupNCM.Get() }).ToString());

            if (contador1 == 1)
            {
                if (AppLib.Windows.FormMessageDefault.ShowQuestion("Gostaria de copiar este NCM para os demais estados?") == System.Windows.Forms.DialogResult.Yes)
                {
                    String consulta2 = @"
SELECT CODETD
FROM GESTADO
WHERE CODETD <> ?";

                    String comando2 = AppLib.Context.poolConnection.Get("Start").ParseCommand(consulta2, new Object[] { campoLookupUFDESTINO.Get() });
                    System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(comando2, new Object[] { });

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        String CODETD = dt.Rows[i]["CODETD"].ToString();
                        AppLib.ORM.Jit y = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "VREGRAVARCFOP");
                        y.Set("CODEMPRESA", campoInteiroCODEMPRESA.Get());
                        y.Set("CODFILIAL", campoLookupCODFILIAL.Get());
                        y.Set("NCM", campoLookupNCM.Get());
                        y.Set("UFDESTINO", CODETD);
                        y.Set("ALIQINTERNA", campoDecimalALIQINTERNA.Get());
                        y.Set("ALIQINTERESTADUAL", campoDecimalALIQINTERESTADUAL.Get());
                        y.Set("ALIQINTERMATIMPORT", campoDecimalALIQINTERMATIMPORT.Get());
                        y.Set("MVAORIGINAL", campoDecimalMVAORIGINAL.Get());
                        y.Set("MVAAJUSTADO", campoDecimalMVAAJUSTADO.Get());
                        y.Set("MVAAJUSTADOMATIMPORT", campoDecimalMVAAJUSTADOMATIMPORT.Get());
                        y.Set("MODALIDADEICMS", campoTextoMODALIDADEICMS.Get());
                        y.Set("MODALIDADEICMSST", campoTextoMODALIDADEICMSST.Get());
                        int salvou = y.Save();

                        if (salvou == 1)
                        {
                            // ok
                        }
                        else
                        {
                            AppLib.Windows.FormMessageDefault.ShowError("Erro ao copiar para o estado: " + CODETD);
                        }
                    }

                }
            }

        }

        private bool campoLookupCODFILIAL_SetFormConsulta(object sender, EventArgs e)
        {
            String consulta = "SELECT * FROM GFILIAL WHERE CODEMPRESA = ?";
            return new AppLib.Windows.FormVisao().MostrarLookup(campoLookupCODFILIAL, consulta, new Object[] { AppLib.Context.Empresa });
        }

        private void campoLookupCODFILIAL_SetDescricao(object sender, EventArgs e)
        {
            String consulta = "SELECT NOMEFANTASIA FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?";
            AppLib.Windows.CampoLookup.Descricao(campoLookupCODFILIAL, consulta, new Object[] { AppLib.Context.Empresa, campoLookupCODFILIAL.Get() });
        }
    }
}
