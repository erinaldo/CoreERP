using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITGProducao.Controles;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroBanco : Form
    {
        public bool edita = false;
        public string CodBanco = string.Empty;
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;
        public int CodImagem;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroBanco()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GBANCO");
        }

        public frmCadastroBanco(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GBANCO");

            this.edita = true;
            this.lookup = lookup;
            CodBanco = lookup.ValorCodigoInterno;
            carregaCampos();
        }

        private void frmCadastroBanco_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                tbCodBanco.Enabled = false;
            }
        }

        private bool validacoes()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(tbCodBanco.Text))
            {
                errorProvider1.SetError(tbCodBanco, "Favor informar o código.");
                verifica = false;
            }
            return verifica;
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GBANCO WHERE CODBANCO = ?", new object[] { CodBanco });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GBANCO WHERE CODBANCO = ?", new object[] { CodBanco });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodBanco.Text = dt.Rows[0]["CODBANCO"].ToString();
            tbNome.Text = dt.Rows[0]["NOME"].ToString();
            tbCodFebraban.Text = dt.Rows[0]["CODFEBRABAN"].ToString();
            CodImagem = Convert.ToInt32(dt.Rows[0]["CODIMAGEM"]);
            object bArray = (object)AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT IMAGEM FROM GIMAGEM WHERE CODIMAGEM = ?", new object[] { CodImagem });
            peImagem.EditValue = bArray;
        }

        private void peImagem_Click(object sender, EventArgs e)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            peImagem.LoadImage();
            if (peImagem.Image != null)
            {
                peImagem.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void btnRemoverImagem_Click(object sender, EventArgs e)
        {
            peImagem.EditValue = "";
        }

        public int SalvarImagem()
        {

            int UltimoCodImagem = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT MAX(CODIMAGEM) FROM GIMAGEM", new object[] { }));

            if (edita == false)
            {
                UltimoCodImagem++;

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppLib.Context.poolConnection.Get("Start").ConnectionString);
                System.Data.SqlClient.SqlCommand cmd;

                try
                {
                    conn.Open();

                    cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO GIMAGEM(CODIMAGEM, IMAGEM) VALUES (@CODIMAGEM, @IMAGEM)", conn);
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODIMAGEM", SqlDbType.Int)).Value = UltimoCodImagem;

                    if (peImagem.Image != null)
                    {
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IMAGEM", SqlDbType.Image)).Value = ConverterFotoParaByteArray();
                    }
                    else
                    {
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IMAGEM", SqlDbType.Image)).Value = DBNull.Value;
                    }

                    cmd.ExecuteNonQuery();
                    conn.Close();

                    return UltimoCodImagem;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return UltimoCodImagem;
            }
        }

        private byte[] ConverterFotoParaByteArray()
        {
            using (var stream = new System.IO.MemoryStream())
            {
                peImagem.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                byte[] bArray = new byte[stream.Length];
                stream.Read(bArray, 0, System.Convert.ToInt32(stream.Length));
                return bArray;
            }
        }

        private bool Salvar()
        {
            if (validacoes() == false)
            {
                return false;
            }

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit GBANCO = new AppLib.ORM.Jit(conn, "GBANCO");
            conn.BeginTransaction();

            try
            {
                GBANCO.Set("CODEMPRESA", AppLib.Context.Empresa);
                GBANCO.Set("CODBANCO", tbCodBanco.Text);
                GBANCO.Set("NOME", tbNome.Text);
                GBANCO.Set("CODFEBRABAN", tbCodFebraban.Text);
                GBANCO.Set("CODIMAGEM", SalvarImagem());

                GBANCO.Save();
                conn.Commit();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                Salvar();
            }
            else
            {
                if (Salvar() == true)
                {
                    carregaCampos();
                    this.Dispose();
                }
            }
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
