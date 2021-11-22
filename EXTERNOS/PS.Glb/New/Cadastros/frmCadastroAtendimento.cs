using DevExpress.XtraEditors;
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
    public partial class frmCadastroAtendimento : Form
    {
        public int IdAtendimento;
        public frmCadastroAtendimento()
        {
            InitializeComponent();
        }

        private void frmCadastroAtendimento_Load(object sender, EventArgs e)
        {
            if (IdAtendimento > 0)
            {
                // Edição
                CarregaLookUpUnidade();
                CarregaLookUpContato();
                CarregaLookUpUsuario();
                CarregaComboBoxInLoco();
                CarregaCampos();
            }
            else
            {
                // Novo registro
                CarregaInformacoesIniciais();
                CarregaLookUpUnidade();
                CarregaLookUpContato();
                CarregaLookUpUsuario();
                CarregaComboBoxInLoco();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {
                if (Salvar())
                {
                    XtraMessageBox.Show("Atendimento cadastrado com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    IdAtendimento = CarregaIDRecemCadastrado();
                    tbIdentificador.Text = IdAtendimento.ToString();

                    return;
                }
                else
                {
                    XtraMessageBox.Show("Não foi possível cadastrar o Atendimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Dispose();
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {
                if (Salvar())
                {
                    XtraMessageBox.Show("Atendimento cadastrado com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                else
                {
                    XtraMessageBox.Show("Não foi possível cadastrar o Atendimento.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Dispose();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Métodos

        private void CarregaInformacoesIniciais()
        {
            dteDataAtendimento.EditValue = DateTime.Now.ToShortDateString();
            tbUsuarioAtendimento.Text = AppLib.Context.Usuario;
        }

        private void CarregaLookUpUnidade()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT IDUNIDADE AS 'Código', NOME AS 'Nome' FROM AUNIDADE WHERE CODEMPRESA = ? ORDER BY NOME ASC", new object[] { AppLib.Context.Empresa });

            lpUnidade.Properties.DataSource = dt;
            lpUnidade.Properties.DisplayMember = dt.Columns["Nome"].ToString();
            lpUnidade.Properties.ValueMember = dt.Columns["Código"].ToString();
            lpUnidade.Properties.NullText = "Selecione...";
        }

        private void CarregaLookUpContato()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODCONTATO AS 'Código', NOME AS 'Nome' FROM VCLIFORCONTATO WHERE CODEMPRESA = ? ORDER BY NOME ASC", new object[] { AppLib.Context.Empresa });

            lpContato.Properties.DataSource = dt;
            lpContato.Properties.DisplayMember = dt.Columns["Nome"].ToString();
            lpContato.Properties.ValueMember = dt.Columns["Código"].ToString();
            lpContato.Properties.NullText = "Selecione...";
        }

        private void CarregaLookUpUsuario()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODUSUARIO AS 'Código', NOME AS 'Nome' FROM GUSUARIO WHERE ATIVO = 1 ORDER BY NOME ASC ");

            lpUsuario.Properties.DataSource = dt;
            lpUsuario.Properties.DisplayMember = dt.Columns["Nome"].ToString();
            lpUsuario.Properties.ValueMember = dt.Columns["Código"].ToString();
            lpUsuario.Properties.NullText = "Selecione...";
        }

        private void CarregaComboBoxInLoco()
        {
            List<LocalAtendimento> listLocalAtendimento = new List<LocalAtendimento>();
            LocalAtendimento localAtendimento = new LocalAtendimento();

            // Selecione
            localAtendimento.valor = "";
            localAtendimento.descricao = "- Selecione";

            listLocalAtendimento.Add(localAtendimento);

            // Local
            localAtendimento = new LocalAtendimento();

            localAtendimento.valor = "Local";
            localAtendimento.descricao = "Sim";

            listLocalAtendimento.Add(localAtendimento);

            // Remoto
            localAtendimento = new LocalAtendimento();

            localAtendimento.valor = "Remoto";
            localAtendimento.descricao = "Não";

            listLocalAtendimento.Add(localAtendimento);

            // Preenche combobox
            cmbInLoco.DataSource = listLocalAtendimento;
            cmbInLoco.ValueMember = "valor";
            cmbInLoco.DisplayMember = "descricao";
        }

        private class LocalAtendimento
        {
            public string valor { get; set; }
            public string descricao { get; set; }
        }

        private void CarregaCampos()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM CATENDIMENTO WHERE CODEMPRESA = ? AND IDATENDIMENTO = ?", new object[] { AppLib.Context.Empresa, IdAtendimento });

            if (dt.Rows.Count > 0)
            {
                tbIdentificador.Text = IdAtendimento.ToString();
                tbUsuarioAtendimento.Text = dt.Rows[0]["CODUSUARIO"].ToString();
                dteDataAtendimento.EditValue = Convert.ToDateTime(dt.Rows[0]["DATAATENDIMENTO"]).ToString("yyy-MM-dd");
                lpUnidade.EditValue = Convert.ToInt32(dt.Rows[0]["CODUNIDADE"]);
                lpContato.EditValue = Convert.ToInt32(dt.Rows[0]["CODCONTATO"]);
                tbHistorico.Text = dt.Rows[0]["HISTORICO"].ToString();
                lpUsuario.EditValue = dt.Rows[0]["CODUSUARIOATENDIMENTO"].ToString();

                if (dt.Rows[0]["DATARETORNO"] != DBNull.Value)
                {
                    dteDataRetorno.EditValue = Convert.ToDateTime(dt.Rows[0]["DATARETORNO"]).ToString("yyy-MM-dd");
                }

                teHoraRetorno.EditValue = Convert.ToDateTime(dt.Rows[0]["HORARETORNO"]).ToString("HH:mm");

                if (dt.Rows[0]["LOCALATENDIMENTO"].ToString() == "Local")
                {
                    cmbInLoco.SelectedIndex = 1;
                }
                else if (dt.Rows[0]["LOCALATENDIMENTO"].ToString() == "Remoto")
                {
                    cmbInLoco.SelectedIndex = 2;
                }
                else
                {
                    cmbInLoco.SelectedIndex = 0;
                }
            }
        }

        private bool ValidaCampos()
        {
            bool valida = true;

            if (lpUnidade.EditValue == null)
            {
                dxErrorProvider1.SetIconAlignment(lpUnidade, ErrorIconAlignment.TopRight);
                dxErrorProvider1.SetError(lpUnidade, "A unidade deve ser informada.");
                valida = false;
            }

            if (lpContato.EditValue == null)
            {
                dxErrorProvider1.SetIconAlignment(lpContato, ErrorIconAlignment.TopRight);
                dxErrorProvider1.SetError(lpContato, "O contato deve ser informado.");
                valida = false;
            }

            if (lpUsuario.EditValue == null)
            {
                dxErrorProvider1.SetIconAlignment(lpUsuario, ErrorIconAlignment.TopRight);
                dxErrorProvider1.SetError(lpUsuario, "O Usuário deve ser informado.");
                valida = false;
            }

            return valida;
        }

        private bool Salvar()
        {
            string query = "";
            int resulTransaction = 0;

            try
            {
                if (IdAtendimento > 0)
                {
                    // Edição
                    query = @"UPDATE CATENDIMENTO
                              SET 
                                CODCONTATO = ?,
                                CODUNIDADE = ?,
                                HISTORICO = ?,
                                DATARETORNO = ?,
                                HORARETORNO = ?,
                                CODUSUARIOATENDIMENTO = ?,
                                LOCALATENDIMENTO = ?
                                WHERE CODEMPRESA = ? AND IDATENDIMENTO = ?";

                    resulTransaction = AppLib.Context.poolConnection.Get("Start").ExecTransaction(query, new object[] 
                    { 
                        lpContato.EditValue, 
                        lpUnidade.EditValue,
                        tbHistorico.Text,
                        (dteDataRetorno.Text == "" ? null : dteDataRetorno.DateTime.ToString()),
                        teHoraRetorno.EditValue,
                        lpUsuario.EditValue,
                        cmbInLoco.SelectedValue,
                        AppLib.Context.Empresa, 
                        IdAtendimento
                    });

                    if (resulTransaction > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    // Inserção
                    query = @"INSERT INTO CATENDIMENTO 
                              VALUES
                             (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    resulTransaction = AppLib.Context.poolConnection.Get("Start").ExecTransaction(query, new object[] 
                    {
                        AppLib.Context.Empresa,
                        tbUsuarioAtendimento.Text,
                        dteDataAtendimento.DateTime,
                        lpContato.EditValue,
                        lpUnidade.EditValue,
                        tbHistorico.Text,
                        (dteDataRetorno.Text == "" ? null : dteDataRetorno.DateTime.ToString()),
                        teHoraRetorno.EditValue,
                        lpUsuario.EditValue,
                        cmbInLoco.SelectedValue
                    });

                    if (resulTransaction > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private int CarregaIDRecemCadastrado()
        {
            int idAtendimento = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT MAX(IDATENDIMENTO) FROM CATENDIMENTO WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }));

            return idAtendimento;
        }

        #endregion
    }
}
