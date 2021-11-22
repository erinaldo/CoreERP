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
    public partial class frmCadastroContatoCliente : Form
    {
        public bool edita = false;
        private string codCliente;
        public int codContato;
        public bool verifica;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroContatoCliente(string _codCliente)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VCLIFORCONTATO");
            codCliente = _codCliente;
        }

        public frmCadastroContatoCliente(string _codCliente, int _codContato)
        {
            InitializeComponent();
            codCliente = _codCliente;
            codContato = _codContato;
        }

        public frmCadastroContatoCliente(ref NewLookup lookup)
        {
            InitializeComponent();
            carregaCampos();
            this.lookup = lookup;
        }

        private void frmCadastroContatoCliente_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                new Class.Utilidades().getDicionario(this, tabControl1, "VCLIFORCONTATO");
                carregaCampos();
            }
        }

        private bool valida()
        {
            bool _valida = true;

            PS.Glb.Class.Utilidades util = new PS.Glb.Class.Utilidades();

            _valida = util.validaCamposObrigatorios(this, ref errorProvider);

            return _valida;
        }

        // Descomentar caso preciso. Modificado em 28/08.

        //private bool salvar()
        //{
        //    if (valida() == true)
        //    {
        //        DateTime? dataNascimento;
        //        if (!string.IsNullOrEmpty(dteDataNascimento.Text))
        //        {
        //            dataNascimento = Convert.ToDateTime(dteDataNascimento.EditValue);
        //        }
        //        else
        //        {
        //            dataNascimento = null;
        //        }
        //        AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
        //        conn.BeginTransaction();
        //        try
        //        {
        //            if (edita == true)
        //            {
        //                conn.ExecTransaction("UPDATE VCLIFORCONTATO SET NOME = ?, EMAIL = ?, TELEFONE = ?, CPF = ?, DATANASCIMENTO = ?, NUMERORG = ?, OREMISSOR = ?, DEPARTAMENTO = ? WHERE CODEMPRESA = ? AND CODCLIFOR = ? AND CODCONTATO = ? AND CELULAR = ? AND OBSERVACOES = ?", new object[] { txtNome.Text, txtEmail.Text, txtTelefone.Text, txtCPF.Text, dataNascimento, txtNumeroRG.Text, txtOrEmissor.Text, txtDepartamento.Text, AppLib.Context.Empresa, codCliente, txtCodContato.Text, tbCelular.Text, tbObervacoes.Text });

        //            }
        //            else
        //            {
        //                codContato = Convert.ToInt32(conn.ExecGetField(0, "SELECT MAX(CODCONTATO) FROM VCLIFORCONTATO WHERE CODEMPRESA = ? AND CODCLIFOR = ?", new object[] { AppLib.Context.Empresa, codCliente })) + 1;

        //                conn.ExecTransaction("INSERT INTO VCLIFORCONTATO (CODEMPRESA, CODCLIFOR, CODCONTATO, NOME, EMAIL, TELEFONE, CPF, DATANASCIMENTO, NUMERORG, OREMISSOR, DEPARTAMENTO, CELULAR, OBSERVACOES) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, codCliente, codContato + 1, txtNome.Text, txtEmail.Text, txtTelefone.Text, txtCPF.Text, dataNascimento, txtNumeroRG.Text, txtOrEmissor.Text, txtDepartamento.Text, tbCelular.Text, tbObervacoes.Text });
        //                txtCodContato.Text = codContato.ToString();
        //                edita = true;
        //            }
        //            conn.Commit();
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            conn.Rollback();
        //            MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return false;
        //        }
        //    }else
        //    {
        //        return false;
        //    }
        //}

        private void carregaCampos()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORCONTATO WHERE CODEMPRESA = ? AND CODCLIFOR = ? AND CODCONTATO = ?", new object[] { AppLib.Context.Empresa, codCliente, codContato });
            if (dt.Rows.Count > 0)
            {
                txtCodContato.Text = codContato.ToString();
                txtNome.Text = dt.Rows[0]["NOME"].ToString();
                txtEmail.Text = dt.Rows[0]["EMAIL"].ToString();
                txtTelefone.Text = dt.Rows[0]["TELEFONE"].ToString();
                txtCPF.Text = dt.Rows[0]["CPF"].ToString();
                dteDataNascimento.EditValue = dt.Rows[0]["DATANASCIMENTO"].ToString();
                txtNumeroRG.Text = dt.Rows[0]["NUMERORG"].ToString();
                txtOrEmissor.Text = dt.Rows[0]["OREMISSOR"].ToString();
                txtDepartamento.Text = dt.Rows[0]["DEPARTAMENTO"].ToString();
                tbCelular.Text = dt.Rows[0]["CELULAR"].ToString();
                tbObervacoes.Text = dt.Rows[0]["OBSERVACOES"].ToString();
            }
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (verifica == true)
            {
                this.Dispose();
                GC.Collect();
            }
            else
            {
                Salvar();
                this.Dispose();
            }
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                Salvar();
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        // Terminar 28/08

        private bool Salvar()
        {
            if (valida() == false)
            {
                return false;
            }

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VCLIFORCONTATO = new AppLib.ORM.Jit(conn, "VCLIFORCONTATO");
            conn.BeginTransaction();

            try
            {
                VCLIFORCONTATO.Set("CODEMPRESA", AppLib.Context.Empresa);

                if (edita == true)
                {
                    VCLIFORCONTATO.Set("CODCONTATO", Convert.ToInt32(conn.ExecGetField(0, @"SELECT CODCONTATO FROM VCLIFORCONTATO WHERE CODEMPRESA = ? AND CODCONTATO = ?", new object[] { AppLib.Context.Empresa, codContato })));
                }
                else
                {
                    VCLIFORCONTATO.Set("CODCONTATO", Convert.ToInt32(conn.ExecGetField(0, @"SELECT (MAX(CODCONTATO) + 1) AS CODCONTATO FROM VCLIFORCONTATO", new object[] { })));
                }
                VCLIFORCONTATO.Set("CODCLIFOR", codCliente);

                VCLIFORCONTATO.Set("NOME", txtNome.Text);
                VCLIFORCONTATO.Set("EMAIL", txtEmail.Text);
                VCLIFORCONTATO.Set("TELEFONE", txtTelefone.Text);
                VCLIFORCONTATO.Set("CPF", txtCPF.Text);

                if (!string.IsNullOrEmpty(dteDataNascimento.Text))
                {
                    VCLIFORCONTATO.Set("DATANASCIMENTO", Convert.ToDateTime(dteDataNascimento.Text));
                }
                else
                {
                    VCLIFORCONTATO.Set("DATANASCIMENTO", null);
                }

                VCLIFORCONTATO.Set("NUMERORG", txtNumeroRG.Text);
                VCLIFORCONTATO.Set("OREMISSOR", txtOrEmissor.Text);
                VCLIFORCONTATO.Set("DEPARTAMENTO", txtDepartamento.Text);
                VCLIFORCONTATO.Set("CELULAR", tbCelular.Text);
                VCLIFORCONTATO.Set("OBSERVACOES", tbObervacoes.Text);
                VCLIFORCONTATO.Save();
                conn.Commit();

                verifica = true;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
