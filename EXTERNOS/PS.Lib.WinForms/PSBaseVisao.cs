using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib.WinForms
{
    public partial class PSBaseVisao : UserControl
    {
        private PSInstance Instance = new PSInstance();
        private Global gb = new Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private Constantes ct = new Constantes();
        private Security sec = new Security();
        private List<DataField> objParams;
        private BindingSource bds = new BindingSource();
        private DataTable dtOrdenado;

        public List<PSPartApp> aplicativo { get; set; }
        public List<String> campos = new List<string>();
        public PSPart psPart { get; set; }
        public bool _atualiza { get; set; }

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

        public List<String> _campos = new List<string>();

        public PSBaseVisao()
        {
            InitializeComponent();

            bds.DataError += bds_DataError;
            dataGridView1.DataError += dataGridView1_DataError;
        }

        void bds_DataError(object sender, BindingManagerDataErrorEventArgs e)
        {
            // MessageBox.Show("ERRO bds_DataError");
        }

        void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // MessageBox.Show("ERRO dataGridView1_DataError");
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
            //if (dataGridView1.Rows.Count > 0)
            //{
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
                                c.Width = 30;

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
            //}
        }

        private void DesabilitaColunaPadrao()
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < this.psPart.DefaultFilter.Count; j++)
                {
                    if (dataGridView1.Columns[i].Name == this.psPart.DefaultFilter[j].Field)
                    {
                        dataGridView1.Columns[i].Visible = false;
                    }
                }
            }
        }

        private void DesabilitaColunas()
        {
            // desabilita todas as colunas exceto o check de seleção
            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                dataGridView1.Columns[j].Visible = false;
            }

            // habilita as colunas conforme configuração
            for (int i = 0; i < _campos.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (_campos[i] == dataGridView1.Columns[j].Name)
                    {
                        for (int y = 0; y < this.psPart.DefaultCustomDataColumns.Count; y++)
                        {
                            if (this.psPart.DefaultCustomDataColumns[y].ReplaceFor == _campos[i])
                            {
                                dataGridView1.Columns[j].Visible = false;
                                dataGridView1.Columns[this.psPart.DefaultCustomDataColumns[y].DataName].Visible = true;
                            }
                            else
                            {
                                dataGridView1.Columns[j].Visible = true;
                            }
                        }
                    }
                }
            }
        }

        private void RemoveColunas()
        {
            string[] lista = new string[this.psPart.DefaultCustomDataColumns.Count];
            int cont = 0;

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < this.psPart.DefaultCustomDataColumns.Count; j++)
                {
                    if (dataGridView1.Columns[i].Name == this.psPart.DefaultCustomDataColumns[j].DataName)
                    {
                        lista[cont] = this.psPart.DefaultCustomDataColumns[j].DataName;
                        cont++;
                    }
                }
            }

            for (int i = 0; i < lista.Length; i++)
            {
                if (lista[i] != null)
                {
                    dataGridView1.Columns.Remove(lista[i]);
                }
            }
        }

        public void MarcaDesmarca()
        {
            marcaDesmarcaToolStripMenuItem_Click(this, null);                    
        }

        private void CarregaAplicativos()
        {
            try
            {
                //CODIGO ANTIGO QUE SÓ ABRIA APP DO PS FRAMEWORK
                //if (aplicativo != null)
                //{
                //    if (aplicativo.Count > 0)
                //    {
                //        toolStripDropDownButton4.DropDownItems.Clear();
                //        for (int i = 0; i < aplicativo.Count; i++)
                //        {
                //            if (aplicativo[i].Image == null)
                //            {
                //                toolStripDropDownButton4.DropDownItems.Add(aplicativo[i].AppName);
                //            }
                //            else
                //            {
                //                toolStripDropDownButton4.DropDownItems.Add(aplicativo[i].AppName, aplicativo[i].Image.Image);
                //            }
                //        }
                //    }
                //}

                // CODIGO NOVO QUE ABRE APP DO APPLIB_V2
                if (aplicativo != null)
                {
                    for (int i = 0; i < aplicativo.Count; i++)
                    {
                        String nomeApp = aplicativo[i].AppName;
                        Boolean existe = false;

                        for (int x = 0; x < toolStripDropDownButton4.DropDownItems.Count; x++)
                        {
                            if (toolStripDropDownButton4.DropDownItems[x].Text.Equals(nomeApp))
                            {
                                existe = true;
                                x = toolStripDropDownButton4.DropDownItems.Count;
                            }
                        }

                        if (!existe)
                        {
                            if (aplicativo[i].Image == null)
                            {
                                toolStripDropDownButton4.DropDownItems.Add(aplicativo[i].AppName);
                            }
                            else
                            {
                                toolStripDropDownButton4.DropDownItems.Add(aplicativo[i].AppName, aplicativo[i].Image.Image);
                            }
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void ExecutaAplicativo(PSPartApp app)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                int cont = 0;

                try
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value))
                        {
                            cont++;
                        }
                    }

                    if (cont == 0)
                    {
                        PSMessageBox.ShowInfo("Nenhum registro selecionado");
                    }
                    else
                    {
                        if (cont > 1)
                        {
                            if (app.Select == SelectType.OnlyOneRow)
                            {
                                PSMessageBox.ShowInfo("Este aplicativo permite apenas um registro selecionado");
                                return;
                            }
                        }

                        app.Access = AppAccess.View;
                        app.DataField = null;
                        app.DataGrid = this.dataGridView1;
                        app.TableName = this.psPart.TableName;
                        app.Keys = this.psPart.Keys;
                        app.Execute();
                    }
                }
                catch (Exception ex)
                {
                    PSMessageBox.ShowError(ex.Message);
                }
            }
        }

        public void CarregaRegistro(List<DataField> DataFilter)
        {
            PermiteEditar = psPart.AllowEdit;
            PermiteExcluir = psPart.AllowDelete;
            PermiteIncluir = psPart.AllowInsert;

            CarregaAcesso();
            CarregaAplicativos();

            objParams = DataFilter;

            AtualizaGrid();

            MontaGridViewValores();

            //CustomDataGridView
            this.psPart.CustomDataGridView(dataGridView1);
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
                bool Flag = true;

                dataGridView1.DefaultCellStyle.SelectionBackColor = ct.corSelecaoGrid;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ct.corImparGrid;

                System.Data.DataTable dt = new DataTable("Table1");

                if (objParams != null)
                {
                    if (objParams.Count > 0)
                    {
                        dt = this.psPart.PSPartData.ExecuteFilterMasterDetail(psPart.TableName, psPart.Keys, objParams);
                        bds.DataSource = dt;

                        for (int i = 0; i < objParams.Count; i++)
                        {
                            if (objParams[i].Valor == null)
                            {
                                Flag = false;
                            }
                        }
                    }
                    else
                    {
                        bds.DataSource = null;
                        Flag = false;
                    }
                }
                else
                {
                    bds.DataSource = this.psPart.PSPartData.ExecuteFilterMasterDetail(psPart.TableName, psPart.Keys, objParams);
                    Flag = false;
                }

                dataGridView1.DataSource = bds;

                dtOrdenado = dt;

                AlteraNomeColuna();
                DesabilitaColunaPadrao();

                if (Flag)
                {
                    MontaGridView();
                    MontaGridViewValores();
                }
                else
                {
                    RemoveColunas();                
                }

                this.CarregarConfiguracaoColuna();

                toolStripButton10_Click(this, null);
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
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
                    f._psPartData._keys = psPart.Keys;
                    f._psPartData._tablename = psPart.TableName;

                    f._setDefault = objParams;

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
                        int vSelecionado = dataGridView1.CurrentRow.Index;

                        FrmBaseEdit f = (FrmBaseEdit)Instance.CreateInstanceFormEdit(psPart.FormEditName);
                        f._psPart = psPart;
                        f._selecionado = vSelecionado;
                        f._data = (DataTable)this.bds.DataSource;
                        f._psPartData = psPart.PSPartData;
                        f._psPartData._keys = psPart.Keys;
                        f._psPartData._tablename = psPart.TableName;

                        f._setDefault = objParams;

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
            int cont = 0;
            try
            {
                if (PermiteExcluir)
                {
                    if (dataGridView1 != null)
                    {
                        if (dataGridView1.Rows.Count != 0)
                        {
                            if (PSMessageBox.ShowQuestion("Deseja excluir o(s) registro(s) selecionado(s) ?") == System.Windows.Forms.DialogResult.Yes)
                            {
                                this.Cursor = Cursors.WaitCursor;
                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    if (dataGridView1.Rows[i].Selected)
                                    {
                                        cont++;

                                        List<DataField> objArr = new List<DataField>();

                                        for (int j = 0; j < psPart.Keys.Length; j++)
                                        {
                                            DataField obj = new DataField();
                                            obj.Field = dataGridView1.Columns[psPart.Keys[j]].Name;
                                            obj.Valor = dataGridView1.Rows[i].Cells[psPart.Keys[j]].Value;
                                            obj.Tipo = dataGridView1.Rows[i].Cells[psPart.Keys[j]].Value.GetType();

                                            objArr.Add(obj);
                                        }

                                        psPart.PSPartData._tablename = psPart.TableName;
                                        psPart.PSPartData._keys = psPart.Keys;
                                        psPart.PSPartData.DeleteRecord(objArr);

                                    }
                                }

                                if (cont <= 0)
                                {
                                    throw new Exception("Nenhum registro selecionado.");
                                }

                                this.Cursor = Cursors.Default;
                                toolStripButton4_Click(this, null);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
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

        private void PSBaseVisao_KeyDown(object sender, KeyEventArgs e)
        {
            Keys flag = e.KeyCode;

            if (e.Shift || e.Control || e.Alt)
            {
                return;
            }
            else
            {
                if (flag.Equals(Keys.Enter))
                {
                    SelectNextControl(ActiveControl, !e.Shift, true, true, true);
                    e.Handled = true;
                }

                //if (flag.Equals(Keys.Space))
                //{
                //    marcaDesmarcaToolStripMenuItem_Click(this, null);
                //}

                if (flag.Equals(Keys.F2))
                {
                    toolStripButton1_Click(this, null);
                }

                if (flag.Equals(Keys.F3))
                {
                    toolStripButton2_Click(this, null);
                }

                if (flag.Equals(Keys.F4))
                {
                    toolStripButton3_Click(this, null);
                }

                if (flag.Equals(Keys.F5))
                {
                    toolStripButton4_Click(this, null);
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButton2_Click(this, null);
        }

        private void incluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(this, null);
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(this, null);
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click(this, null);
        }

        private void marcaDesmarcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1 != null)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Selected)
                    {
                        dataGridView1.Rows[i].Selected = false;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Selected = true;
                    }
                }
            }
        }

        private void marcaDesmarcaTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectAll();
        }

        private void textoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                FrmBaseAppExport f = new FrmBaseAppExport();
                f.FileType = ExportFileType.CSV;
                f.Dados = dataGridView1;
                f.ShowDialog();
            } 
        }

        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                FrmBaseAppExport f = new FrmBaseAppExport();
                f.FileType = ExportFileType.Excel;
                f.Dados = dataGridView1;
                f.ShowDialog();
            } 
        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                FrmBaseAppExport f = new FrmBaseAppExport();
                f.FileType = ExportFileType.PDF;
                f.Dados = dataGridView1;
                f.ShowDialog();
            }  
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                FrmBaseAppPrint f = new FrmBaseAppPrint();
                f.Dados = dataGridView1;
                f.DisplaySelectedRow = false;
                f.ShowDialog();
            } 
        }

        private void toolStripDropDownButton4_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string descricao = e.ClickedItem.Text;

            for (int i = 0; i < aplicativo.Count; i++)
            {
                if (aplicativo[i].AppName == descricao)
                {
                    ExecutaAplicativo(aplicativo[i]);
                }
            }
        }

        private void PSBaseVisao_Paint(object sender, PaintEventArgs e)
        {
            MontaGridViewValores();

            //CustomDataGridView
            if (this.psPart != null)
                this.psPart.CustomDataGridView(dataGridView1);
        }

        private void primeiroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton10_Click(this, null);
        }

        private void anteriorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton11_Click(this, null);
        }

        private void próximoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton12_Click(this, null);
        }

        private void ultimoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton13_Click(this, null);
        }

        private void limparSeleçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dtOrdenado = (DataTable)this.bds.DataSource;
            String coluna = dtOrdenado.DefaultView.Sort;
            DataTable sortedDT = dtOrdenado.Clone();
            sortedDT.Clear();
            foreach (DataRow row in dtOrdenado.Select("", coluna))
            {
                sortedDT.NewRow();
                sortedDT.Rows.Add(row.ItemArray);
            }
            dtOrdenado = sortedDT;
        }

        public void SalvarConfiguracaoColuna()
        {
            try
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    GUSUARIOVISAO item = new GUSUARIOVISAO();

                    item.VISAO = this.psPart.TableName;
                    item.CODUSUARIO = PS.Lib.Contexto.Session.CodUsuario;
                    item.COLUNA = dataGridView1.Columns[i].Name;
                    item.LARGURA = dataGridView1.Columns[i].Width;

                    String comando2 = "UPDATE GUSUARIOVISAO SET LARGURA = ? WHERE VISAO = ? AND CODUSUARIO = ? AND COLUNA = ?";
                    int temp2 = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comando2, new Object[] { item.LARGURA, item.VISAO, item.CODUSUARIO, item.COLUNA });

                    if (temp2 != 1)
                    {
                        throw new Exception("Erro ao salvar configuração de visão por usuário.");
                    }
                }
            }
            catch (Exception ex)
            {
                gerenciarVisãoToolStripMenuItem_Click(this, null);
            }

        }

        private void salvarVisãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SalvarConfiguracaoColuna();
        }

        public void CarregarConfiguracaoColuna()
        {
            String consulta1 = @"SELECT *
FROM GUSUARIOVISAO
WHERE VISAO = ?
  AND CODUSUARIO = ?
  AND VISIVEL = 1
ORDER BY SEQUENCIA";

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(consulta1, new Object[] { this.psPart.TableName, PS.Lib.Contexto.Session.CodUsuario });

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].DisplayIndex = 0;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Columns[dt.Rows[i]["COLUNA"].ToString()].Width = int.Parse(dt.Rows[i]["LARGURA"].ToString());
                    dataGridView1.Columns[dt.Rows[i]["COLUNA"].ToString()].DisplayIndex = int.Parse(dt.Rows[i]["SEQUENCIA"].ToString());
                }
            }
        }

        private void gerenciarVisãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConfigurarColunas f = new FormConfigurarColunas(this.psPart.TableName, PS.Lib.Contexto.Session.CodUsuario, dataGridView1);
            f.ShowDialog();
            this.toolStripButton4_Click(this, null);
        }

        private void toolStripButton7_ButtonClick(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        private void atualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

    }
}
