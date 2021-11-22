using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using PS.Lib;

namespace PS.Lib.WinForms
{
    public partial class FrmBaseLookup : DevExpress.XtraEditors.XtraForm
    {
        private PSInstance Instance = new PSInstance();
        private Constantes ct = new Constantes();
        private Global gb = new Global();
        private Security sec = new Security();
        private PSLookup psLookup;
        private BindingSource bds = new BindingSource();

        public DataField filter = new DataField();
        public List<MasterDetail> _masterDetail { get; set; }
        public List<Folder> _pastaDetail { get; set; }
        public List<PSPartApp> _aplicativo { get; set; }
        public List<PSFilter> _filtroPadrao { get; set; }

        public String SecurityID { get; set; }
        public String ModuleID { get; set; }

        public PSPart psPart { get; set; }
        public string KeyField { get; set; }
        public string LookupField { get; set; }
        public string LookupFieldResult { get; set; }
        public List<PSFilter> filtroLookup { get; set; }

        [DefaultValue(true)]
        public bool PermiteIncluir
        {
            get
            {
                return toolStripButton1.Visible;
            }
            set
            {
                toolStripButton1.Visible = value;
            }
        }

        [DefaultValue(true)]
        public bool PermiteEditar
        {
            get
            {
                return toolStripButton2.Visible;
            }
            set
            {
                toolStripButton2.Visible = value;
            }
        }

        [DefaultValue(true)]
        public bool PermiteExcluir
        {
            get
            {
                return toolStripButton3.Visible;
            }
            set
            {
                toolStripButton3.Visible = value;
            }
        }

        public FrmBaseLookup(PSLookup objLookup)
        {
            InitializeComponent();

            psLookup = objLookup;
        }

        private void MontaGridViewValores()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < psPart.DefaultCustomDataColumns.Count; i++)
                {
                    if (psPart.DefaultCustomDataColumns[i].ColumnType == DataGridColumnType.CheckBox)
                    {
                        /*
                        for (int j = 0; j < dataGridView1.Rows.Count; j++)
                        {
                            if (int.Parse(dataGridView1.Rows[j].Cells[psPart.DefaultCustomDataColumns[i].ReplaceFor].Value.ToString()) == 1)
                            {
                                dataGridView1.Rows[j].Cells[psPart.DefaultCustomDataColumns[i].DataName].Value = true;
                                dataGridView1.Rows[j].Cells[psPart.DefaultCustomDataColumns[i].DataName].Style.Alignment = psPart.DefaultCustomDataColumns[i].ContentAlignment;
                            }
                            else
                            {
                                dataGridView1.Rows[j].Cells[psPart.DefaultCustomDataColumns[i].DataName].Value = false;
                                dataGridView1.Rows[j].Cells[psPart.DefaultCustomDataColumns[i].DataName].Style.Alignment = psPart.DefaultCustomDataColumns[i].ContentAlignment;
                            }
                        }
                        */
                    }

                    if (psPart.DefaultCustomDataColumns[i].ColumnType == DataGridColumnType.TextBox)
                    {

                    }

                    if (psPart.DefaultCustomDataColumns[i].ColumnType == DataGridColumnType.Image)
                    {
                        for (int y = 0; y < psPart.DefaultCustomDataColumns[i].Image.Length; y++)
                        {
                            for (int j = 0; j < dataGridView1.Rows.Count; j++)
                            {
                                if (dataGridView1.Rows[j].Cells[psPart.DefaultCustomDataColumns[i].ReplaceFor].Value.Equals(psPart.DefaultCustomDataColumns[i].Image[y].Value))
                                {
                                    dataGridView1.Rows[j].Cells[psPart.DefaultCustomDataColumns[i].DataName].Value = psPart.DefaultCustomDataColumns[i].Image[y].Image;
                                    dataGridView1.Rows[j].Cells[psPart.DefaultCustomDataColumns[i].DataName].ToolTipText = psPart.DefaultCustomDataColumns[i].Image[y].Text;
                                    dataGridView1.Rows[j].Cells[psPart.DefaultCustomDataColumns[i].DataName].Style.Alignment = psPart.DefaultCustomDataColumns[i].ContentAlignment;
                                }
                            }
                        }
                    }

                    if (psPart.DefaultCustomDataColumns[i].ColumnType == DataGridColumnType.Lookup)
                    {
                        /*
                        for (int j = 0; j < dataGridView1.Rows.Count; j++)
                        {
                            for (int y = 0; y < psPart.DefaultCustomDataColumns[i].Condition.Length; y++)
                            {
                                psPart.DefaultCustomDataColumns[i].Condition[y].Value = dataGridView1.Rows[j].Cells[psPart.DefaultCustomDataColumns[i].Condition[y].ColumnNameRefGrid].Value;
                            }

                            dataGridView1.Rows[j].Cells[psPart.DefaultCustomDataColumns[i].DataName].Value = psPart.DefaultCustomDataColumns[i].LoadLookup();
                        }
                        */
                    }
                }
            }
        }

        private void MontaGridView()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                // Modifica colunas nativas
                for (int i = 0; i < psPart.DefaultCustomDataColumns.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (psPart.DefaultCustomDataColumns[i].DataName == dataGridView1.Columns[j].Name)
                        {
                            // Text
                            if (psPart.DefaultCustomDataColumns[i].Text != null)
                            {
                                dataGridView1.Columns[j].HeaderText = psPart.DefaultCustomDataColumns[i].Text;
                            }

                            // Formatação
                            if (psPart.DefaultCustomDataColumns[i].FormatString != null)
                            {
                                dataGridView1.Columns[j].DefaultCellStyle.Format = psPart.DefaultCustomDataColumns[i].FormatString;
                            }

                            // Alinhamento 
                            dataGridView1.Columns[j].DefaultCellStyle.Alignment = psPart.DefaultCustomDataColumns[i].ContentAlignment;

                            // Visível
                            dataGridView1.Columns[j].Visible = psPart.DefaultCustomDataColumns[i].Visible;
                        }
                    }
                }

                // Adiciona colunas customizadas
                for (int i = 0; i < psPart.DefaultCustomDataColumns.Count; i++)
                {
                    // DataGridColumnType
                    if (psPart.DefaultCustomDataColumns[i].ColumnType != DataGridColumnType.None)
                    {
                        bool Flag = false;

                        // verifica se a coluna ja existe na grid
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            if (psPart.DefaultCustomDataColumns[i].DataName == dataGridView1.Columns[j].Name)
                            {
                                Flag = true;
                            }
                        }

                        if (!Flag)
                        {
                            if (psPart.DefaultCustomDataColumns[i].ColumnType == DataGridColumnType.CheckBox)
                            {
                                /*
                                DataGridViewCheckBoxColumn c = new DataGridViewCheckBoxColumn();
                                c.HeaderText = psPart.DefaultCustomDataColumns[i].Text;
                                c.Name = psPart.DefaultCustomDataColumns[i].DataName;
                                c.FalseValue = 0;
                                c.TrueValue = 1;

                                if (psPart.DefaultCustomDataColumns[i].ReplaceFor != null)
                                {
                                    dataGridView1.Columns[psPart.DefaultCustomDataColumns[i].ReplaceFor].Visible = false;
                                }

                                if (psPart.DefaultCustomDataColumns[i].Order == 0)
                                {
                                    dataGridView1.Columns.Insert(dataGridView1.Columns.IndexOf(dataGridView1.Columns[psPart.DefaultCustomDataColumns[i].ReplaceFor]), c);
                                }
                                else
                                {
                                    if (dataGridView1.Columns.Count < psPart.DefaultCustomDataColumns[i].Order)
                                        dataGridView1.Columns.Insert(psPart.DefaultCustomDataColumns[i].Order - (psPart.DefaultCustomDataColumns[i].Order - dataGridView1.Columns.Count), c);
                                    else
                                        dataGridView1.Columns.Insert(psPart.DefaultCustomDataColumns[i].Order, c);
                                }
                                */
                            }

                            if (psPart.DefaultCustomDataColumns[i].ColumnType == DataGridColumnType.TextBox)
                            {
                                DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
                                c.HeaderText = psPart.DefaultCustomDataColumns[i].Text;
                                c.Name = psPart.DefaultCustomDataColumns[i].DataName;

                                if (psPart.DefaultCustomDataColumns[i].ReplaceFor != null)
                                {
                                    dataGridView1.Columns[psPart.DefaultCustomDataColumns[i].ReplaceFor].Visible = false;
                                }

                                if (psPart.DefaultCustomDataColumns[i].Order == 0)
                                {
                                    dataGridView1.Columns.Insert(dataGridView1.Columns.IndexOf(dataGridView1.Columns[psPart.DefaultCustomDataColumns[i].ReplaceFor]), c);
                                }
                                else
                                {
                                    if (dataGridView1.Columns.Count < psPart.DefaultCustomDataColumns[i].Order)
                                        dataGridView1.Columns.Insert(psPart.DefaultCustomDataColumns[i].Order - (psPart.DefaultCustomDataColumns[i].Order - dataGridView1.Columns.Count), c);
                                    else
                                        dataGridView1.Columns.Insert(psPart.DefaultCustomDataColumns[i].Order, c);
                                }
                            }

                            if (psPart.DefaultCustomDataColumns[i].ColumnType == DataGridColumnType.Image)
                            {
                                DataGridViewImageColumn c = new DataGridViewImageColumn();
                                c.HeaderText = psPart.DefaultCustomDataColumns[i].Text;
                                c.Name = psPart.DefaultCustomDataColumns[i].DataName;

                                if (psPart.DefaultCustomDataColumns[i].ReplaceFor != null)
                                {
                                    dataGridView1.Columns[psPart.DefaultCustomDataColumns[i].ReplaceFor].Visible = false;
                                }

                                if (psPart.DefaultCustomDataColumns[i].Order == 0)
                                {
                                    dataGridView1.Columns.Insert(dataGridView1.Columns.IndexOf(dataGridView1.Columns[psPart.DefaultCustomDataColumns[i].ReplaceFor]), c);
                                }
                                else
                                {
                                    if (dataGridView1.Columns.Count < psPart.DefaultCustomDataColumns[i].Order)
                                        dataGridView1.Columns.Insert(psPart.DefaultCustomDataColumns[i].Order - (psPart.DefaultCustomDataColumns[i].Order - dataGridView1.Columns.Count), c);
                                    else
                                        dataGridView1.Columns.Insert(psPart.DefaultCustomDataColumns[i].Order, c);
                                }
                            }

                            if (psPart.DefaultCustomDataColumns[i].ColumnType == DataGridColumnType.Lookup)
                            {
                                /*
                                DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
                                c.HeaderText = psPart.DefaultCustomDataColumns[i].Text;
                                c.Name = psPart.DefaultCustomDataColumns[i].DataName;

                                if (psPart.DefaultCustomDataColumns[i].ReplaceFor != null)
                                {
                                    dataGridView1.Columns[psPart.DefaultCustomDataColumns[i].ReplaceFor].Visible = false;
                                }

                                if (psPart.DefaultCustomDataColumns[i].Order == 0)
                                {
                                    dataGridView1.Columns.Insert(dataGridView1.Columns.IndexOf(dataGridView1.Columns[psPart.DefaultCustomDataColumns[i].ReplaceFor]), c);
                                }
                                else
                                {
                                    if (dataGridView1.Columns.Count < psPart.DefaultCustomDataColumns[i].Order)
                                        dataGridView1.Columns.Insert(psPart.DefaultCustomDataColumns[i].Order - (psPart.DefaultCustomDataColumns[i].Order - dataGridView1.Columns.Count), c);
                                    else
                                        dataGridView1.Columns.Insert(psPart.DefaultCustomDataColumns[i].Order, c);
                                }
                                */
                            }
                        }
                    }
                }
            }
        }

        private bool DesabilitaCampoCombo(string campo)
        {
            Regex rSplit = new Regex(";");
            String[] sFields = rSplit.Split(this.LookupField);

            bool Flag = true;

            for (int i = 0; i < sFields.Length; i++)
            {
                if (sFields[i].ToString() == campo)
                {
                    Flag = false;                
                }
            }

            return Flag;            
        }

        private void DesabilitaColunas()
        {
            Regex rSplit = new Regex(";");
            String[] sFields = rSplit.Split(this.LookupField);

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].Visible = false;
            }

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < sFields.Length; j++)
                {
                    if (dataGridView1.Columns[i].Name == sFields[j].ToString())
                    {
                        dataGridView1.Columns[i].Visible = true;
                    }
                    if (dataGridView1.Columns[i].Name == "IDENTIFICACAO")
                    {
                        dataGridView1.Columns["IDENTIFICACAO"].DisplayIndex = 2;                       
                    }
                }
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }              
        }

        private void LimpaSelecao()
        {
            if (dataGridView1 != null)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                }
            }
        }

        private void CarregaParametros()
        {
            this.Text = gb.NomeDaTabela(psPart.TableName);

            PermiteEditar = psPart.AllowEdit;
            PermiteExcluir = psPart.AllowDelete;
            PermiteIncluir = psPart.AllowInsert;

            CarregaAcesso();
            CarregaComboPesquisa();
            AtualizaGrid();

            MontaGridViewValores();

            button4_Click(this, null);
        }

        private void CarregaAcesso()
        {
            bool PermiteIncluir;
            bool PermiteEditar;
            bool PermiteExcluir;

            PermiteIncluir = sec.ValidAccess(this.psPart.SecurityID, this.psPart.ModuleID, Security.ButtonAct.Incluir);
            PermiteEditar = sec.ValidAccess(this.psPart.SecurityID, this.psPart.ModuleID, Security.ButtonAct.Editar);
            PermiteExcluir = sec.ValidAccess(this.psPart.SecurityID, this.psPart.ModuleID, Security.ButtonAct.Excluir);

            if (toolStripButton1.Visible != false)
            {
                toolStripButton1.Visible = PermiteIncluir;
            }

            if (toolStripButton2.Visible != false)
            {
                toolStripButton2.Visible = PermiteEditar;
            }

            if (toolStripButton3.Visible != false)
            {
                toolStripButton3.Visible = PermiteExcluir;
            }
        }

        private void CarregaComboPesquisa()
        {
            DataTable dt = gb.NomeDosCampos(psPart.TableName);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (DesabilitaCampoCombo(dt.Rows[i]["COLUNA"].ToString()))
                {
                    dt.Rows[i].Delete();
                }
            }

            comboBox1.DataSource = dt;

            comboBox1.DisplayMember = "DESCRICAO";
            comboBox1.ValueMember = "COLUNA";
        }

        private void AlteraNomeColuna()
        {
            DataTable dt = new DataTable();
            dt = gb.NomeDosCampos(psPart.TableName);

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dataGridView1.Columns[i].Name == dt.Rows[j]["COLUNA"].ToString())
                    {
                        dataGridView1.Columns[i].HeaderText = dt.Rows[j]["DESCRICAO"].ToString();
                    }
                }
            }
        }

        private void AtualizaGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                psPart.PSPartData.FillLookup(ref dt, filter, filtroLookup);

                dataGridView1.DefaultCellStyle.SelectionBackColor = ct.corSelecaoGrid;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ct.corImparGrid;

                bds.DataSource = dt;
                dataGridView1.DataSource = bds;

                toolStripButton10_Click(this, null);

                AlteraNomeColuna();
                MontaGridView();
                MontaGridViewValores();
                DesabilitaColunas();
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBaseLookup_Load(object sender, EventArgs e)
        {
            CarregaParametros();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            filter.Field = null;
            filter.Valor = null;

            AtualizaGrid();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                bds.MoveFirst();
            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                bds.MovePrevious();
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                bds.MoveNext();
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                bds.MoveLast();
            }
        }

        private void FrmBaseLookup_KeyDown(object sender, KeyEventArgs e)
        {
            //Keys flag = e.KeyCode;

            //if (e.Shift || e.Control || e.Alt)
            //{
            //    return;
            //}
            //else
            //{
            //    if (flag.Equals(Keys.F2))
            //    {
            //        toolStripButton1_Click(this, null);
            //    }

            //    if (flag.Equals(Keys.F3))
            //    {
            //        toolStripButton2_Click(this, null);
            //    }

            //    if (flag.Equals(Keys.F4))
            //    {
            //        toolStripButton3_Click(this, null);
            //    }

            //    if (flag.Equals(Keys.F5))
            //    {
            //        toolStripButton4_Click(this, null);
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            filter.Field = comboBox1.SelectedValue.ToString();
            filter.Valor = string.Concat("%",textBox1.Text,"%");
            
            AtualizaGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Regex rSplit = new Regex(";");
            String[] sFields = rSplit.Split(LookupFieldResult);
            string retorno = "";


            int vSelecionado = 0;

            if (dataGridView1 != null)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Selected)
                    {
                        vSelecionado = i;
                    }
                }
            }

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < sFields.Length; j++)
                {
                    if (dataGridView1.Columns[i].Name.ToString() == sFields[j])
                    {
                        if (j == 0)
                        {
                            retorno = dataGridView1.Rows[vSelecionado].Cells[i].Value.ToString();
                        }
                        else
                        {
                            retorno = string.Concat(retorno,";",dataGridView1.Rows[vSelecionado].Cells[i].Value.ToString());
                        }
                    }
                }
            }

            psLookup.ValorRetorno = retorno;

            this.Close();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            button3_Click(this, null);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (PermiteIncluir)
                {
                    FrmBaseEdit f = (FrmBaseEdit)Instance.CreateInstanceFormEdit(psPart.FormEditName);
                    f._psPart = psPart;
                    f._selecionado = -1;
                    f._data = (DataTable)this.bds.DataSource;
                    f._psPartData = psPart.PSPartData;

                    f.PermiteEditar = this.PermiteEditar;
                    f.PermiteExcluir = this.PermiteExcluir;
                    f.PermiteIncluir = this.PermiteIncluir;

                    f.ShowDialog();

                    toolStripButton4_Click(this, null);
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (PermiteEditar)
                {
                    if (dataGridView1.Rows.Count != 0)
                    {
                        int vSelecionado = 0;

                        if(dataGridView1.CurrentRow == null)
                            vSelecionado = 0;
                        else
                            vSelecionado = dataGridView1.CurrentRow.Index;

                        FrmBaseEdit f = (FrmBaseEdit)Instance.CreateInstanceFormEdit(psPart.FormEditName);
                        f._psPart = psPart;
                        f._selecionado = vSelecionado;
                        f._data = (DataTable)this.bds.DataSource;
                        f._psPartData = psPart.PSPartData;

                        f.PermiteEditar = this.PermiteEditar;
                        f.PermiteExcluir = this.PermiteExcluir;
                        f.PermiteIncluir = this.PermiteIncluir;

                        f.ShowDialog();

                        toolStripButton4_Click(this, null);
                    }
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                if (PSMessageBox.ShowQuestion("Deseja excluir o registro ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        List<DataField> objArr = new List<DataField>();

                        if (dataGridView1 != null)
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                if (dataGridView1.Rows[i].Selected)
                                {
                                    for (int j = 0; j < psPart.Keys.Length; j++)
                                    {
                                        DataField obj = new DataField();
                                        obj.Field = dataGridView1.Columns[psPart.Keys[j]].Name;
                                        obj.Valor = dataGridView1.Rows[i].Cells[psPart.Keys[j]].Value;
                                        obj.Tipo = dataGridView1.Rows[i].Cells[psPart.Keys[j]].Value.GetType();

                                        objArr.Add(obj);
                                    }
                                }
                            }
                        }

                        psPart.PSPartData.DeleteRecord(objArr);

                        PSMessageBox.ShowInfo("Operação realizada com sucesso");

                        toolStripButton4_Click(this, null);
                    }
                    catch (Exception ex)
                    {
                        PSMessageBox.ShowError(ex.Message);
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1_Click(this, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void FrmBaseLookup_FormClosed(object sender, FormClosedEventArgs e)
        {
            // PSMemoryManager.ReleaseUnusedMemory(false);
        }
    }
}
