using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PS.Lib.WinForms
{
    public partial class PSImageBox : UserControl
    {
        public PSImageBox()
        {
            InitializeComponent();
        }

        private PS.Lib.Global gb = new PS.Lib.Global();
        private string fileName;
        private byte[] bImagem;
        private int pIdImagem = 0;

        [Category("PSLib"), Description("DataField")]
        public string DataField { get; set; }

        [Category("PSLib"), Description("IdImagem")]
        public int IdImagem
        {
            set 
            {
                LoadImagem(value);
            }
            get
            {
                return SaveImagem();
            } 
        }

        private void LoadImagem(int codImagem)
        {
            if (codImagem == 0)
            {
                pictureBox1.DataBindings.Clear();
                pictureBox1.Image = null;

                pIdImagem = 0;
            }
            else
            {
                pictureBox1.DataBindings.Clear();
                pictureBox1.DataBindings.Add("Image",gb.GetImagem(codImagem), "IMAGEM",true);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                pIdImagem = codImagem;
            }
        }

        private int SaveImagem()
        {
            if (fileName != null)
            {
                if (pictureBox1.Image == null)
                {
                    return 0;
                }
                else
                {
                    bImagem = GetImagem();

                    if (bImagem != null)
                    {
                        pIdImagem = gb.SetImagem(bImagem);
                    }
                }
            }

            return pIdImagem;
        }

        private byte[] GetImagem()
        {
            if (fileName == null)
            {
                return null;
            }
            else
            {
                if (pictureBox1.Image == null)
                {
                    return null;
                }
                else
                {
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);

                    byte[] imagem = br.ReadBytes((int)fs.Length);

                    br.Close();
                    fs.Close();
                    fileName = null;

                    return imagem;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            fileName = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Selecione uma Imagem";
            openFileDialog.Filter = "JPG (*.jpg)|*.jpg" + "|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(openFileDialog.OpenFile());
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    fileName = openFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    PSMessageBox.ShowError("Não foi possivel carregar a imagem : " + ex.Message);
                }
            }

            openFileDialog.Dispose();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            pictureBox1.DataBindings.Clear();
            pictureBox1.Image = null;
            pIdImagem = 0;
        }
    }
}
