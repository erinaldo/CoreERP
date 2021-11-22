using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroTeste : Form
    {
        public frmCadastroTeste()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GPERMISSAOMENU", new object[] { });

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode node = new TreeNode(dt.Rows[i]["CODMENU"].ToString());
                node.Nodes.Add(dt.Rows[i]["CODUSUARIO"].ToString());
                node.Nodes.Add(dt.Rows[i]["INCLUSAO"].ToString());
                node.Nodes.Add(dt.Rows[i]["EXCLUSAO"].ToString());

                treeView1.Nodes.Add(node);
            }
        }
    }
}
