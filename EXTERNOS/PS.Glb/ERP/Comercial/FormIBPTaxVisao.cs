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
    public partial class FormIBPTaxVisao : AppLib.Windows.FormVisao
    {
        private static FormIBPTaxVisao _instance = null;

        public static FormIBPTaxVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormIBPTaxVisao();

            return _instance;
        }

        private void FormIBPTaxVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormIBPTaxVisao()
        {
            InitializeComponent();
        }

        private void FormIBPTaxVisao_Load(object sender, EventArgs e)
        {
            grid1.GetProcessos().Add("Importar Tabelas", null, ImportarTabelas);
        }

        public void ImportarTabelas(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "CSV|*.csv";
                ofd.Multiselect = true;

                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    splashScreenManager1.ShowWaitForm();

                    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start");
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM VIBPTAX", new Object[] { });
                    for (int indexOfg = 0; indexOfg < ofd.FileNames.Length; indexOfg++)
                    {
                        String arquivo = ofd.FileNames[indexOfg];
                        System.IO.FileInfo fi = new System.IO.FileInfo(arquivo);

                        AppLib.ORM.Jit reg = new AppLib.ORM.Jit(conn, "VIBPTAX");
                        
                        String[] linhas = System.IO.File.ReadAllLines(arquivo, Encoding.Default);
                        if (linhas.Length > 0)
                        {
                            

                            for (int i = 1; i < linhas.Length; i++)
                            {
                                String[] texto = linhas[i].Split(';');

                                reg.Set("ARQUIVO", fi.Name);
                                reg.Set("UF", fi.Name.Substring(12, 2));
                                reg.Set("LINHA", i);

                                // Formata o NCM
                                String CODIGO = "";

                                if (texto[0].Length == 4)
                                {
                                    CODIGO = texto[0];
                                }

                                if (texto[0].Length == 8)
                                {
                                    CODIGO = texto[0].Substring(0, 4) + ".";
                                    CODIGO += texto[0].Substring(4, 2) + ".";
                                    CODIGO += texto[0].Substring(6, 2);
                                }

                                if (texto[0].Length == 9)
                                {
                                    CODIGO = texto[0].Substring(0, 4) + ".";
                                    CODIGO += texto[0].Substring(4, 2) + ".";
                                    CODIGO += texto[0].Substring(6, 2) + ".";
                                    CODIGO += texto[0].Substring(8, 1);
                                }

                                reg.Set("CODIGO", CODIGO);

                                if (texto[1] != String.Empty)
                                {
                                    reg.Set("EX", int.Parse(texto[1]));
                                }
                                else
                                {
                                    reg.Set("EX", 0);
                                }

                                reg.Set("TIPO", texto[2]);
                                reg.Set("DESCRICAO", texto[3]);
                                reg.Set("NACIONALFEDERAL", texto[4]);
                                reg.Set("IMPORTADOSFEDERAL", texto[5]);
                                reg.Set("ESTADUAL", texto[6]);
                                reg.Set("MUNICIPAL", texto[7]);
                                reg.Set("VIGENCIAINICIO", Convert.ToDateTime(texto[8]));
                                reg.Set("VIGENCIAFIM", Convert.ToDateTime(texto[9]));
                                reg.Set("CHAVE", texto[10]);
                                reg.Set("VERSAO", texto[11]);
                                reg.Set("FONTE", texto[12]);

                                int temp = reg.Save();

                                if (temp != 1)
                                {
                                    throw new Exception("Erro ao salvar a linha "+ i +" do arquivo " + fi.Name);
                                }

                                reg.Clear();
                            }
                        }
                    }

                    splashScreenManager1.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                splashScreenManager1.CloseWaitForm();
                AppLib.Windows.FormMessageDefault.ShowError("Erro ao importar arquivo IBPTax.\r\n\nDetalhe técnico: " + ex.Message);
            }

            AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM VIBPTAX WHERE EX <> 0 OR LEN(CODIGO) > 10", new Object[] { });

            grid1.Atualizar();
        }

        private void grid1_Load(object sender, EventArgs e)
        {

        }
    }
}
