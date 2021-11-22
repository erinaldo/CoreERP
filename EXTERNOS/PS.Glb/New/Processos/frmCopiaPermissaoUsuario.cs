using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Processos
{
    public partial class frmCopiaPermissaoUsuario : Form
    {
        public List<string> UsuarioDestino = new List<string>();

        public frmCopiaPermissaoUsuario()
        {
            InitializeComponent();
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < UsuarioDestino.Count; i++)
            {
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppLib.Context.poolConnection.Get("Start").ConnectionString);

                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    conn.Open();

                    //Validar procedure COPIAPERMISSAOMENU e alterar na função
                    System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("COPIAPERMISSAOMENU", conn);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@USERORIGEM", lpUsuarioDestino.ValorCodigoInterno);
                    command.Parameters.AddWithValue("@USERDESTINO", UsuarioDestino[i]);

                    command.CommandTimeout = 1800;

                    System.Data.SqlClient.SqlDataReader rs = command.ExecuteReader();
                    if (rs.HasRows)
                    {
                        rs.Read();

                        MessageBox.Show(rs["retorno"].ToString(), "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    Cursor.Current = Cursors.Default;
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            MessageBox.Show("Processo de cópia de permissão executado com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lpUsuarioDestino.Clear();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
