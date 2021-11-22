using ITGProducao.Controles;
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
    public partial class frmCadastroTipoTransporte : Form
    {
        public bool edita = false;
        public string CodTipoTransporte = string.Empty;
        private List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroTipoTransporte()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VTIPOTRANSPORTE");
        }

        public frmCadastroTipoTransporte(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VTIPOTRANSPORTE");

            this.edita = true;
            this.lookup = lookup;
            CodTipoTransporte = lookup.ValorCodigoInterno;
            carregaCampos();
        }

        private void frmCadastroTipoTransporte_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                tbCodTipoTransporte.Enabled = false;
            }
            else
            {
                tbCodTipoTransporte.Text = setCodTipoTransporte();
                tbCodTipoTransporte.Enabled = false;
            }
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VTIPOTRANSPORTE WHERE CODTIPOTRANSPORTE = ?", new object[] { CodTipoTransporte });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VTIPOTRANSPORTE WHERE CODTIPOTRANSPORTE = ?", new object[] { CodTipoTransporte });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodTipoTransporte.Text = dt.Rows[0]["CODTIPOTRANSPORTE"].ToString();
            lpTransportadora.txtcodigo.Text = dt.Rows[0]["CODTRANSPORTADORA"].ToString();
            lpTransportadora.CarregaDescricao();
            tbDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
        }

        public string setCodTipoTransporte()
        {
            string codTipoTransporte = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT ISNULL(MAX(IDLOG), 0) + 1 FROM GLOG WHERE CODEMPRESA = ? AND CODTABELA = 'VTIPOTRANSPORTE'", new object[] { AppLib.Context.Empresa }).ToString();
            return codTipoTransporte;
        }
    }
}
