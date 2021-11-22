using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Cadastros.Globais
{
    public partial class frmSituacao : Form
    {
        public bool edita;
        public string CodSituacao;
        public string Tabela;
        public int codEmpresa;

        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        public frmSituacao()
        {
            InitializeComponent();
            cmbTabela.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS GROUP BY TABLE_NAME ORDER BY TABLE_NAME");
            cmbTabela.DisplayMember = "TABLE_NAME";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (edita == false)
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtCodStatus.Text) && !string.IsNullOrEmpty(cmbTabela.Text.ToString()))
                    {
                        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppLib.Context.poolConnection.Get("Start").ConnectionString);
                        conn.Open();
                        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("INSERT INTO GSITUACAO (CODEMPRESA, CODSITUACAO, DESCRICAO, IMAGEM, TABELA) VALUES (@CODEMPRESA, @CODSITUACAO, @DESCRICAO, @IMAGEM, @TABELA)", conn);
                        command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODEMPRESA", SqlDbType.Int)).Value = Convert.ToInt32(txtCodEmpresa.Text);
                        command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODSITUACAO", SqlDbType.VarChar)).Value = txtCodStatus.Text;
                        command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DESCRICAO", SqlDbType.VarChar)).Value = txtDescricao.Text;
                        command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IMAGEM", SqlDbType.Image)).Value = ConverterFotoParaByteArray();
                        command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TABELA", SqlDbType.VarChar)).Value = cmbTabela.Text.ToString();
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        MessageBox.Show("Favor preencher os campos corretamentes.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtCodStatus.Text) && !string.IsNullOrEmpty(cmbTabela.Text.ToString()))
                    {
                        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppLib.Context.poolConnection.Get("Start").ConnectionString);
                        conn.Open();
                        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("UPDATE GSITUACAO SET  DESCRICAO = @DESCRICAO, IMAGEM = @IMAGEM, TABELA = @TABELA WHERE CODEMPRESA = @CODEMPRESA AND CODSITUACAO = @CODSITUACAO ", conn);
                        command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODEMPRESA", SqlDbType.Int)).Value = Convert.ToInt32(txtCodEmpresa.Text);
                        command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CODSITUACAO", SqlDbType.Int)).Value = txtCodStatus.Text;
                        command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DESCRICAO", SqlDbType.VarChar)).Value = txtDescricao.Text;
                        command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IMAGEM", SqlDbType.Image)).Value = ConverterFotoParaByteArray();
                        command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TABELA", SqlDbType.VarChar)).Value = cmbTabela.Text.ToString();
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        MessageBox.Show("Favor preencher os campos corretamentes.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            peImagem.LoadImage();

            peImagem.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);



        }

        private byte[] ConverterFotoParaByteArray()
        {
            using (var stream = new System.IO.MemoryStream())
            {
                peImagem.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                byte[] bArray = new byte[stream.Length];
                stream.Read(bArray, 0, System.Convert.ToInt32(stream.Length));
                return bArray;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            object bArray = (object)AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT IMAGEM FROM GSITUACAO", new object[] { });

            peImagem.EditValue = bArray;
        }

        private void frmStatus_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
            }
            
           
        }


        private void carregaCampos()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GSITUACAO WHERE CODEMPRESA = ? AND CODSITUACAO = ? AND TABELA = ?", new object[] { codEmpresa, CodSituacao, Tabela });
            if (dt.Rows.Count > 0)
            {
                txtCodEmpresa.Text = dt.Rows[0]["CODEMPRESA"].ToString();
                txtCodStatus.Text = dt.Rows[0]["CODSITUACAO"].ToString();
                cmbTabela.Text = dt.Rows[0]["TABELA"].ToString();
                object bArray = (object)dt.Rows[0]["IMAGEM"];
                peImagem.EditValue = bArray;
                txtDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
            }
            txtCodEmpresa.Enabled = false;
            txtCodStatus.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            btnSalvar_Click(sender, e);
            this.Dispose();
            GC.Collect();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
           // AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GSTATUS SET SITUACAO = 'I' WHERE CODSTATUS = ? AND CODEMPRESA = ? AND TABELA = ?", new object[] { txtCodStatus.Text, txtCodEmpresa.Text, cmbTabela.Text.ToString() });
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtCodEmpresa.Text = string.Empty;
            txtCodStatus.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            cmbTabela.Text = string.Empty;
            peImagem.EditValue = "";
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }
    }
}
