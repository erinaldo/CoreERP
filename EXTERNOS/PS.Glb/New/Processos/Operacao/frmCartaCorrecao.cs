using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace PS.Glb.New.Processos.Operacao
{
    public partial class frmCartaCorrecao : Form
    {
        public int CodOPer;

        public frmCartaCorrecao()
        {
            InitializeComponent();
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            if (txtCorrecao.Text.Length < 15)
            {
                MessageBox.Show("Número de caracteres insuficiente. \r\n (O mínimo aceito são 15 caracteres)", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtCorrecao.Text))
            {
                MessageBox.Show("Favor preencher a correção corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PS.Validate.Services.NFeSrv nf = new Validate.Services.NFeSrv();
            int Idoutbox = nf.CartaCorrecao(AppLib.Context.Empresa, CodOPer, txtCorrecao.Text);
            if (Idoutbox != 0)
            {
                MessageBox.Show("Envio registrado com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                bool retorno = false;
                //Busca Retorno Validate
                while (retorno == false)
                {
                    DataTable dt = nf.GetRetornoCCe(Idoutbox);
                    if (dt.Rows.Count > 0)
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GNFESTADUALCORRECAO (CODEMPRESA, CODOPER, IDOUTBOX, NSEQITEM, CODSTATUS, MOTIVO, PROTOCOLO, DATAPROTOCOLO, XMLENV, XMLPROT) 
VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, CodOPer, Idoutbox, dt.Rows[0]["NSEQITEM"], dt.Rows[0]["CODSTATUS"], dt.Rows[0]["XJUST"], dt.Rows[0]["NPROT"], dt.Rows[0]["DATAULTIMOLOG"], dt.Rows[0]["XMLENV"], dt.Rows[0]["XMLPROT"] });
                        retorno = true;
                    }
                }
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Não foi possível enviar a carta de correção.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }
    }
}
