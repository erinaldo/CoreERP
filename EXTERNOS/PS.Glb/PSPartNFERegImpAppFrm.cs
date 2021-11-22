using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartNFERegImpAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        //private List<PS.Fis.Services.XMLFile> ListXMLFile = new List<PS.Fis.Services.XMLFile>();

        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public PSPartNFERegImpAppFrm()
        {
            InitializeComponent();
        }

        private void LimpaFormulario()
        {
            txtPath.Text = string.Empty;
        }

        private void CarregaGridArquivos()
        {
            //dataGridView1.DataSource = ListXMLFile;                    
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog open = new FolderBrowserDialog();
            open.ShowNewFolderButton = false;
            open.Description = "Selecione um Diretório";

            if (open.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = open.SelectedPath;

                DirectoryInfo objDirInfoImport = new DirectoryInfo(txtPath.Text);

                FileInfo[] AllFilesImport = objDirInfoImport.GetFiles("*.xml");

                if (AllFilesImport.Length > 0)
                {
                    //ListXMLFile.Clear();
                    /*
                    foreach (FileInfo FileTxt in AllFilesImport)
                    {
                        PS.Fis.Services.XMLFile xmlFile = new PS.Fis.Services.XMLFile();

                        xmlFile.Path = txtPath.Text;
                        xmlFile.FileName = FileTxt.Name;
                        xmlFile.FullPath = string.Concat(xmlFile.Path, "\\", xmlFile.FileName);

                        xmlFile.LoadXML();

                        ListXMLFile.Add(xmlFile);
                    }
                    */
                }
                else
                {
                    //Nenhum arquivo encontrado
                }

                CarregaGridArquivos();
            }
        }

        private void PSPartNFERegImpAppFrm_Load(object sender, EventArgs e)
        {
            LimpaFormulario();
        }
    }
}
