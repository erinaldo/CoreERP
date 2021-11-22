using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroDDFe : Form
    {
        public bool edita = false;
        public int NSU;

        public frmCadastroDDFe()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GDDFE");
        }

        private void frmCadastroDDFe_Load(object sender, EventArgs e)
        {
            carregaCampos();
        }
        private void carregaCampos()
        {
            DataTable dt;
            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDDFE WHERE CODEMPRESA = ? AND NSU = ?", new object[] { AppLib.Context.Empresa, NSU });
            if (dt.Rows.Count > 0)
            {
                carregaObj(dt);
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbNsu.Text = dt.Rows[0]["NSU"].ToString();
            tbCodEstrutura.Text = dt.Rows[0]["CODESTRUTURA"].ToString();
            tbVersao.Text = dt.Rows[0]["VERSAO"].ToString();
            tbModelo.Text = dt.Rows[0]["MODELO"].ToString();
            tbTpAmb.Text = dt.Rows[0]["TPAMB"].ToString();
            tbNumeroDocumento.Text = dt.Rows[0]["NUMERODOCUMENTO"].ToString();
            
            if (!string.IsNullOrEmpty(dt.Rows[0]["DATA"].ToString()))
            {
                dteData.DateTime = Convert.ToDateTime(dt.Rows[0]["DATA"]);
            }

            tbEmitente.Text = dt.Rows[0]["EMITENTE"].ToString();
            tbCnpjEmitente.Text = dt.Rows[0]["CNPJEMITENTE"].ToString();
            tbUfEmitente.Text = dt.Rows[0]["UFEMITENTE"].ToString();
            tbDestinatario.Text = dt.Rows[0]["DESTINATARIO"].ToString();
            tbCnpjDestinatario.Text = dt.Rows[0]["CNPJDESTINATARIO"].ToString();
            tbUfDestinatario.Text = dt.Rows[0]["UFDESTINATARIO"].ToString();

            tbChave.Text = dt.Rows[0]["CHAVE"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATAEMISSAO"].ToString()))
            {
                dteEmissao.DateTime = Convert.ToDateTime(dt.Rows[0]["DATAEMISSAO"]);
            }

            MemoXmlRecebido.Text = dt.Rows[0]["XMLRECEBIDO"].ToString();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
