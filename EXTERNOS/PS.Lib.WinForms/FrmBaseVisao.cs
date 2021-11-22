using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using PS.Lib;
using PS.Lib.WinForms;

namespace PS.Lib.WinForms
{
    public partial class FrmBaseVisao : DevExpress.XtraEditors.XtraForm
    {
        // OBJETOS
        private PSInstance Instance = new PSInstance();
        private Constantes ct = new Constantes();
        private Global gb = new Global();
        private Security sec = new Security();
        private BindingSource bds = new BindingSource();
        private DataTable dtOrdenado;
        private System.Data.DataTable dtFiltrado;
        public string ValorSelecionado;

        // PROPERTIES
        public PSPart _psPart { get; set; }
        public PSPartData _psData { get; set; }

        public List<Filter> _filtros { get; set; } // lista de filtros
        public bool _atualiza { get; set; } // controle

        public List<String> _campos = new List<string>();

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

        public FrmBaseVisao()
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
                for (int i = 0; i < _psPart.DefaultCustomDataColumns.Count; i++)
                {
                    if (_psPart.DefaultCustomDataColumns[i].ColumnType == DataGridColumnType.CheckBox)
                    {
                        /*
                        for (int j = 0; j < dataGridView1.Rows.Count; j++)
                        {
                            if (int.Parse(dataGridView1.Rows[j].Cells[_customColumn[i].ReplaceFor].Value.ToString()) == 1)
                            {
                                dataGridView1.Rows[j].Cells[_customColumn[i].DataName].Value = true;
                                dataGridView1.Rows[j].Cells[_customColumn[i].DataName].Style.Alignment = _customColumn[i].ContentAlignment;
                            }
                            else
                            {
                                dataGridView1.Rows[j].Cells[_customColumn[i].DataName].Value = false;
                                dataGridView1.Rows[j].Cells[_customColumn[i].DataName].Style.Alignment = _customColumn[i].ContentAlignment;
                            }
                        }
                        */
                    }

                    if (_psPart.DefaultCustomDataColumns[i].ColumnType == DataGridColumnType.TextBox)
                    {

                    }

                    if (_psPart.DefaultCustomDataColumns[i].ColumnType == DataGridColumnType.Image)
                    {
                        for (int y = 0; y < _psPart.DefaultCustomDataColumns[i].Image.Length; y++)
                        {
                            for (int j = 0; j < dataGridView1.Rows.Count; j++)
                            {
                                if (dataGridView1.Rows[j].Cells[_psPart.DefaultCustomDataColumns[i].ReplaceFor].Value.Equals(_psPart.DefaultCustomDataColumns[i].Image[y].Value))
                                {
                                    dataGridView1.Rows[j].Cells[_psPart.DefaultCustomDataColumns[i].DataName].Value = _psPart.DefaultCustomDataColumns[i].Image[y].Image;
                                    dataGridView1.Rows[j].Cells[_psPart.DefaultCustomDataColumns[i].DataName].ToolTipText = _psPart.DefaultCustomDataColumns[i].Image[y].Text;
                                    dataGridView1.Rows[j].Cells[_psPart.DefaultCustomDataColumns[i].DataName].Style.Alignment = _psPart.DefaultCustomDataColumns[i].ContentAlignment;
                                }
                            }
                        }
                    }

                    if (_psPart.DefaultCustomDataColumns[i].ColumnType == DataGridColumnType.Lookup)
                    {
                        /*
                        for (int j = 0; j < dataGridView1.Rows.Count; j++)
                        {
                            for (int y = 0; y < _customColumn[i].Condition.Length; y++)
                            {
                                _customColumn[i].Condition[y].Value = dataGridView1.Rows[j].Cells[_customColumn[i].Condition[y].ColumnNameRefGrid].Value;
                            }

                            dataGridView1.Rows[j].Cells[_customColumn[i].DataName].Value = _customColumn[i].LoadLookup();
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
                for (int i = 0; i < _psPart.DefaultCustomDataColumns.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        if (_psPart.DefaultCustomDataColumns[i].DataName == dataGridView1.Columns[j].Name)
                        {
                            // Text
                            if (_psPart.DefaultCustomDataColumns[i].Text != null)
                            {
                                dataGridView1.Columns[j].HeaderText = _psPart.DefaultCustomDataColumns[i].Text;
                            }

                            // Formatação
                            if (_psPart.DefaultCustomDataColumns[i].FormatString != null)
                            {
                                dataGridView1.Columns[j].DefaultCellStyle.Format = _psPart.DefaultCustomDataColumns[i].FormatString;
                            }

                            // Alinhamento 
                            dataGridView1.Columns[j].DefaultCellStyle.Alignment = _psPart.DefaultCustomDataColumns[i].ContentAlignment;

                            // Visível
                            dataGridView1.Columns[j].Visible = _psPart.DefaultCustomDataColumns[i].Visible;
                        }
                    }
                }

                // Adiciona colunas customizadas
                for (int i = 0; i < _psPart.DefaultCustomDataColumns.Count; i++)
                {
                    // DataGridColumnType
                    if (_psPart.DefaultCustomDataColumns[i].ColumnType != DataGridColumnType.None)
                    {
                        bool Flag = false;

                        // verifica se a coluna ja existe na grid
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            if (_psPart.DefaultCustomDataColumns[i].DataName == dataGridView1.Columns[j].Name)
                            {
                                Flag = true;
                            }
                        }

                        if (!Flag)
                        {
                            if (_psPart.DefaultCustomDataColumns[i].ColumnType == DataGridColumnType.CheckBox)
                            {
                                /*
                                DataGridViewCheckBoxColumn c = new DataGridViewCheckBoxColumn();
                                c.HeaderText = _customColumn[i].Text;
                                c.Name = _customColumn[i].DataName;
                                c.FalseValue = 0;
                                c.TrueValue = 1;

                                if (_customColumn[i].ReplaceFor != null)
                                {
                                    dataGridView1.Columns[_customColumn[i].ReplaceFor].Visible = false;
                                }

                                
                                if (_customColumn[i].Order == 0)
                                {
                                    dataGridView1.Columns.Insert(dataGridView1.Columns.IndexOf(dataGridView1.Columns[_customColumn[i].ReplaceFor]), c);
                                }
                                else
                                {
                                    if (dataGridView1.Columns.Count < _customColumn[i].Order)
                                    {
                                        dataGridView1.Columns.Insert(_customColumn[i].Order - (_customColumn[i].Order - dataGridView1.Columns.Count), c);
                                        
                                    }
                                    else
                                    {
                                        dataGridView1.Columns.Insert(_customColumn[i].Order, c);
                                    }
                                }
                                */
                            }

                            if (_psPart.DefaultCustomDataColumns[i].ColumnType == DataGridColumnType.TextBox)
                            {
                                DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
                                c.HeaderText = _psPart.DefaultCustomDataColumns[i].Text;
                                c.Name = _psPart.DefaultCustomDataColumns[i].DataName;

                                if (_psPart.DefaultCustomDataColumns[i].ReplaceFor != null)
                                {
                                    dataGridView1.Columns[_psPart.DefaultCustomDataColumns[i].ReplaceFor].Visible = false;
                                }

                                if (_psPart.DefaultCustomDataColumns[i].Order == 0)
                                {
                                    dataGridView1.Columns.Insert(dataGridView1.Columns.IndexOf(dataGridView1.Columns[_psPart.DefaultCustomDataColumns[i].ReplaceFor]), c);
                                }
                                else
                                {
                                    if (dataGridView1.Columns.Count < _psPart.DefaultCustomDataColumns[i].Order)
                                        dataGridView1.Columns.Insert(_psPart.DefaultCustomDataColumns[i].Order - (_psPart.DefaultCustomDataColumns[i].Order - dataGridView1.Columns.Count), c);
                                    else
                                        dataGridView1.Columns.Insert(_psPart.DefaultCustomDataColumns[i].Order, c);
                                }
                            }

                            if (_psPart.DefaultCustomDataColumns[i].ColumnType == DataGridColumnType.Image)
                            {
                                DataGridViewImageColumn c = new DataGridViewImageColumn();
                                c.HeaderText = _psPart.DefaultCustomDataColumns[i].Text;
                                c.Name = _psPart.DefaultCustomDataColumns[i].DataName;
                                c.Width = 30;

                                if (_psPart.DefaultCustomDataColumns[i].ReplaceFor != null)
                                {
                                    dataGridView1.Columns[_psPart.DefaultCustomDataColumns[i].ReplaceFor].Visible = false;
                                }

                                if (_psPart.DefaultCustomDataColumns[i].Order == 0)
                                {
                                    dataGridView1.Columns.Insert(dataGridView1.Columns.IndexOf(dataGridView1.Columns[_psPart.DefaultCustomDataColumns[i].ReplaceFor]), c);
                                }
                                else
                                {
                                    if (dataGridView1.Columns.Count < _psPart.DefaultCustomDataColumns[i].Order)
                                        dataGridView1.Columns.Insert(_psPart.DefaultCustomDataColumns[i].Order - (_psPart.DefaultCustomDataColumns[i].Order - dataGridView1.Columns.Count), c);
                                    else
                                        dataGridView1.Columns.Insert(_psPart.DefaultCustomDataColumns[i].Order, c);
                                }
                            }

                            if (_psPart.DefaultCustomDataColumns[i].ColumnType == DataGridColumnType.Lookup)
                            {
                                /*
                                DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
                                c.HeaderText = _customColumn[i].Text;
                                c.Name = _customColumn[i].DataName;

                                if (_customColumn[i].ReplaceFor != null)
                                {
                                    dataGridView1.Columns[_customColumn[i].ReplaceFor].Visible = false;
                                }

                                if (_customColumn[i].Order == 0)
                                {
                                    dataGridView1.Columns.Insert(dataGridView1.Columns.IndexOf(dataGridView1.Columns[_customColumn[i].ReplaceFor]), c);
                                }
                                else
                                {
                                    if (dataGridView1.Columns.Count < _customColumn[i].Order)
                                        dataGridView1.Columns.Insert(_customColumn[i].Order - (_customColumn[i].Order - dataGridView1.Columns.Count), c);
                                    else
                                        dataGridView1.Columns.Insert(_customColumn[i].Order, c);
                                }
                                */
                            }
                        }
                    }
                }
            }
        }

        private void DesabilitaColunaPadrao()
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < _psPart.DefaultFilter.Count; j++)
                {
                    if (dataGridView1.Columns[i].Name == _psPart.DefaultFilter[j].Field)
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
                        for (int y = 0; y < _psPart.DefaultCustomDataColumns.Count; y++)
                        {
                            if (_psPart.DefaultCustomDataColumns[y].ReplaceFor == _campos[i])
                            {
                                dataGridView1.Columns[j].Visible = false;
                                dataGridView1.Columns[_psPart.DefaultCustomDataColumns[y].DataName].Visible = true;
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
            bool vSelecionado = false;
            string[] lista = new string[_psPart.DefaultCustomDataColumns.Count];
            int cont = 0;

            for (int i = 0; i < _filtros.Count; i++)
            {
                if (_filtros[i].selecionado == 1)
                {
                    vSelecionado = true;
                }
            }

            if (!vSelecionado)
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    for (int j = 0; j < _psPart.DefaultCustomDataColumns.Count; j++)
                    {
                        if (dataGridView1.Columns[i].Name == _psPart.DefaultCustomDataColumns[j].DataName)
                        {
                            lista[cont] = _psPart.DefaultCustomDataColumns[j].DataName;
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
        }

        private void AtualizaEditPasta(Folder pasta)
        {
            try
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    List<DataField> objArr = new List<DataField>();

                    int vSelecionado = dataGridView1.CurrentRow.Index;

                    for (int j = 0; j < pasta.dataFilterDetail.Count; j++)
                    {
                        DataField obj = new DataField();
                        obj.Field = dataGridView1.Columns[pasta.dataFilterDetail[j].Field].Name;
                        obj.Valor = dataGridView1.Rows[vSelecionado].Cells[pasta.dataFilterDetail[j].Field].Value;
                        obj.Tipo = dataGridView1.Rows[vSelecionado].Cells[pasta.dataFilterDetail[j].Field].Value.GetType();

                        objArr.Add(obj);
                    }

                    pasta.psPartDetail.Execute(objArr);
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void AtualizaEditPasta(MasterDetail pasta)
        {
            try
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    List<DataField> objArr = new List<DataField>();

                    int vSelecionado = dataGridView1.CurrentRow.Index;

                    for (int j = 0; j < pasta.dataFilterDetail.Count; j++)
                    {
                        DataField obj = new DataField();
                        obj.Field = dataGridView1.Columns[pasta.dataFilterDetail[j].Field].Name;
                        obj.Valor = dataGridView1.Rows[vSelecionado].Cells[pasta.dataFilterDetail[j].Field].Value;
                        obj.Tipo = dataGridView1.Rows[vSelecionado].Cells[pasta.dataFilterDetail[j].Field].Value.GetType();

                        objArr.Add(obj);
                    }

                    pasta.psPartDetail.Execute(objArr);
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void AtualizaGridPasta(MasterDetail pasta, TabPage tab)
        {
            try
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    List<DataField> objArr = new List<DataField>();

                    int vSelecionado = dataGridView1.CurrentRow.Index;

                    for (int j = 0; j < pasta.dataFilterDetail.Count; j++)
                    {
                        DataField obj = new DataField();
                        obj.Field = dataGridView1.Columns[pasta.dataFilterDetail[j].Field].Name;
                        obj.Valor = dataGridView1.Rows[vSelecionado].Cells[pasta.dataFilterDetail[j].Field].Value;
                        obj.Tipo = dataGridView1.Rows[vSelecionado].Cells[pasta.dataFilterDetail[j].Field].Value.GetType();

                        objArr.Add(obj);
                    }

                    PSBaseVisao p = (PSBaseVisao)tab.Controls[0];
                    p.psPart = pasta.psPartDetail;
                    p.aplicativo = pasta.psPartDetail.PSPartApp;

                    p.CarregaRegistro(objArr);
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void AtualizaGridPasta(Folder pasta, TabPage tab)
        {
            try
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    List<DataField> objArr = new List<DataField>();

                    if (dataGridView1.CurrentRow != null)
                    {
                        int vSelecionado = dataGridView1.CurrentRow.Index;

                        for (int j = 0; j < pasta.dataFilterDetail.Count; j++)
                        {
                            DataField obj = new DataField();
                            obj.Field = dataGridView1.Columns[pasta.dataFilterDetail[j].Field].Name;
                            obj.Valor = dataGridView1.Rows[vSelecionado].Cells[pasta.dataFilterDetail[j].Field].Value;
                            obj.Tipo = dataGridView1.Rows[vSelecionado].Cells[pasta.dataFilterDetail[j].Field].Value.GetType();

                            objArr.Add(obj);
                        }

                        PSBaseVisao p = (PSBaseVisao)tab.Controls[0];
                        p.psPart = pasta.psPartDetail;
                        p.aplicativo = pasta.psPartDetail.PSPartApp;

                        p.CarregaRegistro(objArr);
                    }
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void AbrirPasta(Folder pasta)
        {
            try
            {
                if (!sec.ValidAccess(pasta.psPartDetail.SecurityID, pasta.psPartDetail.ModuleID))
                {
                    PSMessageBox.ShowError("Atenção. Usuário não tem permissão de acesso ao menu " + pasta.psPartDetail.SecurityID + ".");
                }
                else
                {
                    if (pasta.psPartDetail.GetFolderType() == FolderType.View)
                    {
                        int indice = tabControl1.TabPages.Count;

                        if (indice > 0)
                            indice = indice - 1;

                        TabPage tab = new TabPage();
                        tab.Text = gb.NomeDaTabela(pasta.psPartDetail.TableName);
                        tab.Name = pasta.psPartDetail.SecurityID;

                        PSBaseVisao p = new PSBaseVisao();
                        p.psPart = pasta.psPartDetail;

                        tab.Controls.Add(new PSBaseVisao());
                        tab.Controls[0].Dock = DockStyle.Fill;

                        tabControl1.TabPages.Add(tab);

                        splitContainer1.Panel2Collapsed = false;

                        AtualizaGridPasta(pasta, tab);
                    }

                    if (pasta.psPartDetail.GetFolderType() == FolderType.Edit)
                    {
                        AtualizaEditPasta(pasta);
                    }
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void AbrirPasta(MasterDetail pasta)
        {
            try
            {
                if (!sec.ValidAccess(pasta.psPartDetail.SecurityID, pasta.psPartDetail.ModuleID))
                {
                    PSMessageBox.ShowError("Atenção. Usuário não tem permissão de acesso ao menu " + pasta.psPartDetail.SecurityID + ".");
                }
                else
                {
                    if (pasta.psPartDetail.GetFolderType() == FolderType.View)
                    {
                        int indice = tabControl1.TabPages.Count;

                        if (indice > 0)
                            indice = indice - 1;

                        TabPage tab = new TabPage();
                        tab.Text = gb.NomeDaTabela(pasta.psPartDetail.TableName);
                        tab.Name = pasta.psPartDetail.SecurityID;

                        PSBaseVisao p = new PSBaseVisao();
                        p.psPart = pasta.psPartDetail;

                        tab.Controls.Add(new PSBaseVisao());
                        tab.Controls[0].Dock = DockStyle.Fill;

                        tabControl1.TabPages.Add(tab);

                        splitContainer1.Panel2Collapsed = false;

                        AtualizaGridPasta(pasta, tab);
                    }

                    if (pasta.psPartDetail.GetFolderType() == FolderType.Edit)
                    {
                        AtualizaEditPasta(pasta);
                    }
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void FecharPasta(int index)
        {
            try
            {
                tabControl1.TabPages.RemoveAt(index);
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void FecharPasta(TabPage page)
        {
            try
            {
                tabControl1.TabPages.Remove(page);
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void CarregaAnexos()
        {
            try
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    List<DataField> objArr = new List<DataField>();

                    if (dataGridView1.CurrentRow != null)
                    {
                        int vSelecionado = dataGridView1.CurrentRow.Index;

                        for (int j = 0; j < _psPart.Keys.Length; j++)
                        {
                            DataField obj = new DataField();
                            obj.Field = dataGridView1.Columns[_psPart.Keys[j]].Name;
                            obj.Valor = dataGridView1.Rows[vSelecionado].Cells[_psPart.Keys[j]].Value;
                            obj.Tipo = dataGridView1.Rows[vSelecionado].Cells[_psPart.Keys[j]].Value.GetType();

                            objArr.Add(obj);
                        }

                        DataTable dt = gb.BuscaAnexos(objArr, _psPart.SecurityID);

                        MontaListaAnexos(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void MontaListaAnexos(DataTable anexos)
        {
            try
            {
                string tipo = string.Empty;
                int tamanho = anexosToolStripMenuItem.DropDownItems.Count;
                int cont = 2;

                if (anexosToolStripMenuItem.DropDownItems.Count > 2)
                {
                    for (int i = 0; i < tamanho; i++)
                    {
                        if (i > 0)
                            anexosToolStripMenuItem.DropDownItems.RemoveAt(1);
                    }
                }

                if (anexos.Rows.Count > 0)
                {
                    anexosToolStripMenuItem.DropDownItems.Add("-");

                    for (int i = 0; i < anexos.Rows.Count; i++)
                    {
                        tipo = anexos.Rows[i]["TIPO"].ToString();

                        if (tipo == "O")
                        {
                            anexosToolStripMenuItem.DropDownItems.Add(anexos.Rows[i]["NOME"].ToString(), Properties.Resources.anexo_other);
                            anexosToolStripMenuItem.DropDownItems[cont].Tag = anexos.Rows[i]["NSEQ"].ToString();
                        }

                        if (tipo == "D")
                        {
                            anexosToolStripMenuItem.DropDownItems.Add(anexos.Rows[i]["NOME"].ToString(), Properties.Resources.anexo_document);
                            anexosToolStripMenuItem.DropDownItems[cont].Tag = anexos.Rows[i]["NSEQ"].ToString();
                        }

                        if (tipo == "I")
                        {
                            anexosToolStripMenuItem.DropDownItems.Add(anexos.Rows[i]["NOME"].ToString(), Properties.Resources.anexo_image);
                            anexosToolStripMenuItem.DropDownItems[cont].Tag = anexos.Rows[i]["NSEQ"].ToString();
                        }

                        if (tipo == "V")
                        {
                            anexosToolStripMenuItem.DropDownItems.Add(anexos.Rows[i]["NOME"].ToString(), Properties.Resources.anexo_video);
                            anexosToolStripMenuItem.DropDownItems[cont].Tag = anexos.Rows[i]["NSEQ"].ToString();
                        }

                        cont = cont + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void CarregaAplicativos()
        {
            try
            {
                //CODIGO ANTIGO QUE SÓ ABRIA APP DO PS FRAMEWORK
                //if (_psPart.PSPartApp.Count > 0)
                //{
                //    toolStripDropDownButton4.DropDownItems.Clear();
                //    for (int i = 0; i < _psPart.PSPartApp.Count; i++)
                //    {
                //        if (_psPart.PSPartApp[i].Image == null)
                //        {
                //            toolStripDropDownButton4.DropDownItems.Add(_psPart.PSPartApp[i].AppName);
                //        }
                //        else
                //        {
                //            toolStripDropDownButton4.DropDownItems.Add(_psPart.PSPartApp[i].AppName, _psPart.PSPartApp[i].Image.Image);
                //        }
                //    }
                //}

                // CODIGO NOVO QUE ABRE APP DO APPLIB_V2
                for (int i = 0; i < _psPart.PSPartApp.Count; i++)
                {
                    String nomeApp = _psPart.PSPartApp[i].AppName;
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
                        if (_psPart.PSPartApp[i].Image == null)
                        {
                            toolStripDropDownButton4.DropDownItems.Add(_psPart.PSPartApp[i].AppName);
                        }
                        else
                        {
                            toolStripDropDownButton4.DropDownItems.Add(_psPart.PSPartApp[i].AppName, _psPart.PSPartApp[i].Image.Image);
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
                    if (dataGridView1 != null)
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].Selected)
                            {
                                cont++;
                            }
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
                        app.TableName = _psPart.TableName;
                        app.Keys = _psPart.Keys;
                        app._ValorSelecionado = ValorSelecionado;
                        app.Execute();

                        if (app.Refresh)
                        {
                            toolStripButtonATUALIZAR1_ButtonClick(this, null);
                        }
                    }
                }
                catch (Exception ex)
                {
                    PSMessageBox.ShowError(ex.Message);
                }
            }
        }

        private void CarregaPasta()
        {
            try
            {
                bool Flag = false;
                bool Flag1 = false;

                //Pastas
                if (_psPart.Folder.Count > 0)
                {
                    Flag1 = true;

                    toolStripDropDownButton1.DropDownItems.Add("-");

                    for (int i = 0; i < _psPart.Folder.Count; i++)
                    {
                        toolStripDropDownButton1.DropDownItems.Add(gb.NomeDaTabela(_psPart.Folder[i].psPartDetail.TableName));

                        if (_psPart.Folder[i].autoShow)
                        {
                            Flag = true;

                            AbrirPasta(_psPart.Folder[i]);
                        }
                    }
                }

                //MasterDetail
                if (_psPart.MasterDetail.Count > 0)
                {
                    if (!Flag1)
                    {
                        toolStripDropDownButton1.DropDownItems.Add("-");
                    }

                    for (int i = 0; i < _psPart.MasterDetail.Count; i++)
                    {
                        if (_psPart.MasterDetail[i].ViewEnabled)
                        {
                            toolStripDropDownButton1.DropDownItems.Add(gb.NomeDaTabela(_psPart.MasterDetail[i].psPartDetail.TableName));
                        }
                    }
                }

                if (Flag)
                    splitContainer1.Panel2Collapsed = false;
                else
                    splitContainer1.Panel2Collapsed = true;
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void CarregaStaticReport()
        {
            try
            {
                Report.ReportUtil rpu = new Report.ReportUtil();

                try
                {
                    if (_psPart.SecurityID == "PSPartOperacao")
                    {
                        DataField dfCODTIPOPER = gb.RetornaDataFieldByFilter(_psPart.DefaultFilter, "CODTIPOPER");
                        _psPart.PSPartStReport = rpu.LoadStaticReportList(_psPart.SecurityID, dfCODTIPOPER.Valor.ToString());
                    }
                    else
                    {
                        _psPart.PSPartStReport = rpu.LoadStaticReportList(_psPart.SecurityID);
                    }

                    if (_psPart.PSPartStReport.Count > 0)
                    {
                        for (int i = 0; i < _psPart.PSPartStReport.Count; i++)
                        {
                            relatóriosToolStripMenuItem.DropDownItems.Add(_psPart.PSPartStReport[i].ReportName);
                        }
                    }
                }
                catch { }
                {

                }

                //if (_psPart.SecurityID == "PSPartOperacao")
                //{
                //    DataField dfCODTIPOPER = gb.RetornaDataFieldByFilter(_psPart.DefaultFilter, "CODTIPOPER");
                //    _psPart.PSPartStReport = rpu.LoadStaticReportList(_psPart.SecurityID, dfCODTIPOPER.Valor.ToString());
                //}
                //else
                //{
                //    _psPart.PSPartStReport = rpu.LoadStaticReportList(_psPart.SecurityID);
                //}                

                //if (_psPart.PSPartStReport.Count > 0)
                //{
                //    for (int i = 0; i < _psPart.PSPartStReport.Count; i++)
                //    {
                //        relatóriosToolStripMenuItem.DropDownItems.Add(_psPart.PSPartStReport[i].ReportName);
                //    }
                //}
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        public ToolStripItemCollection GetProcessos()
        {
            return toolStripDropDownButton4.DropDownItems;
        }

        public ToolStripItemCollection GetAnexos()
        {
            return toolStripDropDownButton1.DropDownItems;
        }

        private void CarregaParametros()
        {
            try
            {
                this.WindowState = FormWindowState.Maximized;

                this.Text = gb.NomeDaTabela(_psPart.TableName);

                // Se for visão de Operação, mostra a descrição do tipo de operação
                if (_psPart.SecurityID == "PSPartOperacao")
                {
                    DataField dfCODTIPOPER = gb.RetornaDataFieldByFilter(_psPart.DefaultFilter, "CODTIPOPER");
                    this.Text = string.Concat(this.Text, " - ", gb.RetornaParametrosOperacao((dfCODTIPOPER.Valor == null) ? null : dfCODTIPOPER.Valor.ToString(), "DESCRICAO"));
                }

                pSPartNameToolStripMenuItem.Text = this.Text;

                CarregaAcesso();
                CarregaPasta();
                CarregaAplicativos();
                CarregaStaticReport();
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void CarregaAcesso()
        {
            bool PermiteIncluir;
            bool PermiteEditar;
            bool PermiteExcluir;

            PermiteIncluir = sec.ValidAccess(_psPart.SecurityID, _psPart.ModuleID, Security.ButtonAct.Incluir);
            PermiteEditar = sec.ValidAccess(_psPart.SecurityID, _psPart.ModuleID, Security.ButtonAct.Editar);
            PermiteExcluir = sec.ValidAccess(_psPart.SecurityID, _psPart.ModuleID, Security.ButtonAct.Excluir);

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

        private void GerenciarFiltro()
        {
            try
            {
                _filtros = this._psData.GetFilter(_psPart.TableName, _psPart.Keys, Contexto.Session.CodUsuario, Contexto.Session.Empresa.CodEmpresa, _psPart.GetTableColumn());

                FrmBaseFiltro f = new FrmBaseFiltro(this);
                f.ShowDialog();

                CarregaFiltrosDisponiveis();
                ExisteParametro();

                AtualizaGrid(this._psData.ExecuteFilter(_filtros, _psPart.TableName, _campos, _psPart.Keys, _psPart.DefaultFilter));
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void ExisteParametro()
        {
            try
            {
                for (int i = 0; i < _filtros.Count; i++)
                {
                    if (_filtros[i].selecionado == 1)
                    {
                        for (int j = 0; j < _filtros[i].listaCondicao.Count; j++)
                        {
                            if (_filtros[i].listaCondicao[j].ExisteParametro())
                            {
                                FrmBaseFiltroParam f = new FrmBaseFiltroParam(_filtros[i]);
                                f.ShowDialog();

                                return;
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

        private void CarregaFiltrosDisponiveis()
        {
            try
            {
                int tamanho = toolStripDropDownButton3.DropDownItems.Count;

                toolStripDropDownButton3.Text = "[Filtrar]";

                if (toolStripDropDownButton3.DropDownItems.Count > 4)
                {
                    for (int i = 0; i < tamanho; i++)
                    {
                        if (i > 3)
                            toolStripDropDownButton3.DropDownItems.RemoveAt(4);
                    }
                }

                if (_filtros.Count > 0)
                {
                    toolStripDropDownButton3.DropDownItems.Add("-");

                    for (int i = 0; i < _filtros.Count; i++)
                    {
                        if (_filtros[i].selecionado == 0)
                            toolStripDropDownButton3.DropDownItems.Add(_filtros[i].descricao, imageList1.Images[15]);
                        else
                        {
                            toolStripDropDownButton3.DropDownItems.Add(_filtros[i].descricao, imageList1.Images[19]);
                            toolStripDropDownButton3.Text = "[Filtrar: " + _filtros[i].descricao + "]";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void AlteraNomeColuna()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = gb.NomeDosCampos(_psPart.TableName);

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
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void AtualizaGrid(DataTable dt)
        {
            try
            {
                dataGridView1.DefaultCellStyle.SelectionBackColor = ct.corSelecaoGrid;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ct.corImparGrid;

                #region LIMPA A MEMÓRIA

                bds.DataSource = null;
                dataGridView1.DataSource = null;
                dtOrdenado = null;
                PS.Lib.PSMemoryManager.ReleaseUnusedMemory();

                #endregion

                bds.DataSource = dt;
                dataGridView1.DataSource = bds;
                dtOrdenado = dt;

                AlteraNomeColuna();
                DesabilitaColunaPadrao();
                MontaGridView();
                MontaGridViewValores();
                RemoveColunas();

                this._psPart.CustomDataGridView(dataGridView1);

                this.CarregarConfiguracaoColuna();

                toolStripButton10_Click(this, null);
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
                if (dataGridView1.Rows.Count != 0)
                {
                    if (PermiteExcluir)
                    {
                        if (PSMessageBox.ShowQuestion("Deseja excluir o(s) registro(s) selecionado(s) ?") == System.Windows.Forms.DialogResult.Yes)
                        {
                            this.Cursor = Cursors.WaitCursor;

                            if (dataGridView1 != null)
                            {
                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    if (dataGridView1.Rows[i].Selected)
                                    {
                                        cont++;

                                        List<DataField> objArr = new List<DataField>();

                                        for (int j = 0; j < _psPart.Keys.Length; j++)
                                        {
                                            DataField obj = new DataField();
                                            obj.Field = dataGridView1.Columns[_psPart.Keys[j]].Name;
                                            obj.Valor = dataGridView1.Rows[i].Cells[_psPart.Keys[j]].Value;
                                            obj.Tipo = dataGridView1.Rows[i].Cells[_psPart.Keys[j]].Value.GetType();

                                            objArr.Add(obj);
                                        }

                                        try
                                        {
                                            _psData.DeleteRecord(objArr);
                                        }
                                        catch (Exception ex)
                                        {
                                            PSMessageBox.ShowError(ex.Message);
                                        }
                                    }
                                }
                            }

                            if (cont <= 0)
                            {
                                throw new Exception("Nenhum registro selecionado.");
                            }

                            this.Cursor = Cursors.Default;
                            toolStripButtonATUALIZAR1_ButtonClick(this, null);
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


        private void toolStripButtonATUALIZAR0_Click(object sender, EventArgs e)
        {
            AtualizaGrid(this._psData.ExecuteFilter(_filtros, _psPart.TableName, _campos, _psPart.Keys, _psPart.DefaultFilter));
        }

        private void toolStripButtonATUALIZAR1_ButtonClick(object sender, EventArgs e)
        {
            // AtualizaGrid(this._psData.ExecuteFilter(_filtros, _psPart.TableName, _campos, _psPart.Keys, _psPart.DefaultFilter));
            AtualizaGrid(this._psData.ExecuteQuery(_psData.Consulta, _psData.Parametros));
        }

        private void atualizarToolStripMenuItemATUALIZAR2_Click(object sender, EventArgs e)
        {
            toolStripButtonATUALIZAR1_ButtonClick(this, null);
        }

        private void toolStripButtonLOCALIZAR_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text.Equals(""))
            {
                toolStripButtonATUALIZAR1_ButtonClick(this, null);
            }
            else
            {
                toolStripButtonATUALIZAR1_ButtonClick(this, null);

                dtFiltrado = null;
                dtOrdenado = null;
                dtFiltrado = ((System.Data.DataTable)this.bds.DataSource).Copy();
                dtOrdenado = ((System.Data.DataTable)this.bds.DataSource).Copy();
                Boolean busca;

                // varrer linhas
                for (int linha = 0; linha < dtFiltrado.Rows.Count; linha++)
                {
                    busca = false;

                    // varrer colunas
                    for (int coluna = 0; coluna < dtFiltrado.Columns.Count; coluna++)
                    {
                        String celula = dtFiltrado.Rows[linha][coluna].ToString().ToUpper();
                        String texto = toolStripTextBox1.Text.ToUpper();

                        // se contér na busca
                        if (celula.Contains(texto))
                        {
                            busca = true;
                        }
                    }

                    if (busca == false)
                    {
                        dtFiltrado.Rows.RemoveAt(linha);
                        dtOrdenado.Rows.RemoveAt(linha);
                        linha--;
                    }

                }

                try
                {
                    bds.DataSource = null;
                    bds.DataSource = dtFiltrado;

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = bds;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro no método toolStripButtonLOCALIZAR_Click ao setar datasource: " + ex.Message);
                }

                try
                {
                    this.MontaGridViewValores();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao montar a grid view valores: " + ex.Message);
                }


                this.CarregarConfiguracaoColuna();
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                bds.MoveFirst();
                dataGridView1_Click(this, null);
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                bds.MoveLast();
                dataGridView1_Click(this, null);
            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                bds.MovePrevious();
                dataGridView1_Click(this, null);
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                bds.MoveNext();
                dataGridView1_Click(this, null);
            }
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click(this, null);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                incluirToolStripMenuItem.Enabled = false;
                editarToolStripMenuItem.Enabled = false;
                excluirToolStripMenuItem.Enabled = false;
            }
            else
            {
                incluirToolStripMenuItem.Enabled = true;
                editarToolStripMenuItem.Enabled = true;
                excluirToolStripMenuItem.Enabled = true;
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

        private void exportarParaCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textoToolStripMenuItem_Click(this, null);
        }

        private void exportarParaExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            excelToolStripMenuItem_Click(this, null);
        }

        private void exportarParaPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pDFToolStripMenuItem_Click(this, null);
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

                        if (dataGridView1.CurrentRow == null)
                        {
                            vSelecionado = 0;
                        }
                        else
                        {
                            vSelecionado = dataGridView1.CurrentRow.Index;
                        }

                        FrmBaseEdit f = (FrmBaseEdit)Instance.CreateInstanceFormEdit(_psPart.FormEditName);
                        f._psPart = _psPart;

                        f._psPart = _psPart;
                        f._selecionado = vSelecionado;
                        f._data = dtOrdenado;
                        f._psPartData = this._psData;
                        f._filtro = gb.RetornaFiltroSelecionado(_filtros);

                        f.PermiteEditar = this.PermiteEditar;
                        f.PermiteExcluir = this.PermiteExcluir;
                        f.PermiteIncluir = this.PermiteIncluir;

                        f.ShowDialog();

                        toolStripButtonLOCALIZAR_Click(this, null);
                    }
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (dataGridView1.Rows.Count != 0)
            //    {
            //        _atualiza = false;

            //        FrmBaseConfigurarColunas f = new FrmBaseConfigurarColunas();
            //        f.tabela = _psPart.TableName;
            //        f.campos = this._campos;
            //        f.atualiza = _atualiza;
            //        f.defaultFilter = _psPart.DefaultFilter;

            //        f.ShowDialog();

            //        _atualiza = f.atualiza;

            //        if (_atualiza)
            //        {
            //            AtualizaGrid(this._psData.ExecuteFilter(_filtros, _psPart.TableName, _campos, _psPart.Keys, _psPart.DefaultFilter));
            //            DesabilitaColunas();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    PSMessageBox.ShowError("Ocorreu um erro: " + ex.Message);
            //}
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (PermiteIncluir)
                {
                    FrmBaseEdit f = (FrmBaseEdit)Instance.CreateInstanceFormEdit(_psPart.FormEditName);
                    f._psPart = _psPart;

                    f._psPart = _psPart;
                    f._selecionado = -1;
                    f._data = (DataTable)this.bds.DataSource;
                    f._psPartData = this._psData;
                    f._filtro = gb.RetornaFiltroSelecionado(_filtros);

                    f.PermiteEditar = this.PermiteEditar;
                    f.PermiteExcluir = this.PermiteExcluir;
                    f.PermiteIncluir = this.PermiteIncluir;

                    f.ShowDialog();

                    toolStripButtonATUALIZAR1_ButtonClick(this, null);
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void FrmBaseVisao_Load(object sender, EventArgs e)
        {
            gerenciarFiltroToolStripMenuItem_Click(this, null);
            CarregaParametros();
        }

        private void gerenciarFiltroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GerenciarFiltro();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton3_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "gerenciarFiltroToolStripMenuItem" ||
                e.ClickedItem.Name == "novoFiltroToolStripMenuItem" ||
                e.ClickedItem.Name == "cancelarFiltroToolStripMenuItem")
                return;

            string descricao = e.ClickedItem.Text;

            for (int i = 0; i < _filtros.Count; i++)
            {
                _filtros[i].selecionado = 0;
            }

            for (int i = 0; i < _filtros.Count; i++)
            {
                if (_filtros[i].descricao == descricao)
                {
                    _filtros[i].selecionado = 1;
                }
            }

            CarregaFiltrosDisponiveis();
            ExisteParametro();
            AtualizaGrid(this._psData.ExecuteFilter(_filtros, _psPart.TableName, _campos, _psPart.Keys, _psPart.DefaultFilter));
        }

        private void cancelarFiltroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _filtros.Count; i++)
            {
                _filtros[i].selecionado = 0;
            }

            CarregaFiltrosDisponiveis();
            AtualizaGrid(this._psData.ExecuteFilter(_filtros, _psPart.TableName, _campos, _psPart.Keys, _psPart.DefaultFilter));
        }

        private void novoFiltroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int codcoligada = 0;
            int id = 0;

            FrmBaseFiltro FrmBaseFiltro1 = new FrmBaseFiltro(this);
            FrmBaseFiltro1._tabela = _psPart.TableName;

            FrmBaseFiltroManutencao f = new FrmBaseFiltroManutencao(null, FrmBaseFiltro1);
            f.ShowDialog();

            for (int i = 0; i < _filtros.Count; i++)
            {
                if (_filtros[i].selecionado == 1)
                {
                    codcoligada = _filtros[i].codEmpresa;
                    id = _filtros[i].id;
                }
            }

            _filtros = this._psData.GetFilter(_psPart.TableName, _psPart.Keys, Contexto.Session.CodUsuario, Contexto.Session.Empresa.CodEmpresa);

            for (int i = 0; i < _filtros.Count; i++)
            {
                if ((_filtros[i].codEmpresa == codcoligada) && (_filtros[i].id == id))
                {
                    _filtros[i].selecionado = 1;
                }
            }

            CarregaFiltrosDisponiveis();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            toolStripButton2_Click(this, null);
        }

        private void FrmBaseVisao_KeyDown(object sender, KeyEventArgs e)
        {
            //Keys flag = e.KeyCode;

            //if (e.Shift || e.Control || e.Alt)
            //{
            //    return;
            //}
            //else
            //{
            //    //if (flag.Equals(Keys.Space))
            //    //{
            //    //    if (this.ActiveControl.GetType() == typeof(PSBaseVisao))
            //    //    {
            //    //        PSBaseVisao p = (PSBaseVisao)this.ActiveControl;
            //    //        p.MarcaDesmarca();
            //    //    }
            //    //    else
            //    //    {
            //    //        if (dataGridView1.Focus())
            //    //        {
            //    //            marcaDesmarcaToolStripMenuItem_Click(this, null);
            //    //        }
            //    //    }
            //    //}

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
            //        toolStripButtonATUALIZAR1_ButtonClick(this, null);
            //    }
            //}
        }

        private void fecharTodasAsPastasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count > 0)
            {
                for (int i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    FecharPasta(i);
                }

                splitContainer1.Panel2Collapsed = true;
            }
        }

        private void fecharPastaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count > 0)
            {
                TabPage tab = tabControl1.SelectedTab;
                FecharPasta(tab);

                if (tabControl1.TabPages.Count <= 0)
                {
                    splitContainer1.Panel2Collapsed = true;
                }
            }
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            return;
        }

        private void toolStripDropDownButton1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "fecharPastaToolStripMenuItem" ||
                e.ClickedItem.Name == "fecharTodasAsPastasToolStripMenuItem" ||
                e.ClickedItem.Name == "verticalToolStripMenuItem" ||
                e.ClickedItem.Name == "horizontalToolStripMenuItem")
                return;

            string descricao = e.ClickedItem.Text;

            if (tabControl1.TabPages.Count > 0)
            {
                for (int i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    if (tabControl1.TabPages[i].Text == descricao)
                    {
                        return;
                    }
                }
            }

            for (int i = 0; i < _psPart.Folder.Count; i++)
            {
                if (gb.NomeDaTabela(_psPart.Folder[i].psPartDetail.TableName) == descricao)
                {
                    if (_psPart.Folder[i].GetType() == typeof(Folder))
                    {
                        AbrirPasta(_psPart.Folder[i]);
                    }
                }
            }

            for (int i = 0; i < _psPart.MasterDetail.Count; i++)
            {
                if (gb.NomeDaTabela(_psPart.MasterDetail[i].psPartDetail.TableName) == descricao)
                {
                    if (_psPart.MasterDetail[i].GetType() == typeof(MasterDetail))
                    {
                        AbrirPasta(_psPart.MasterDetail[i]);
                    }
                }
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            CarregaAnexos();

            if (tabControl1.TabPages.Count > 0)
            {
                TabPage tab = tabControl1.SelectedTab;

                string descricao = tab.Text;

                for (int i = 0; i < _psPart.Folder.Count; i++)
                {
                    if (gb.NomeDaTabela(_psPart.Folder[i].psPartDetail.TableName) == descricao)
                    {
                        AtualizaGridPasta(_psPart.Folder[i], tab);
                    }
                }

                for (int i = 0; i < _psPart.MasterDetail.Count; i++)
                {
                    if (gb.NomeDaTabela(_psPart.MasterDetail[i].psPartDetail.TableName) == descricao)
                    {
                        if (_psPart.MasterDetail[i].GetType() == typeof(MasterDetail))
                        {
                            AtualizaGridPasta(_psPart.MasterDetail[i], tab);
                        }
                    }
                }
            }
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer1.Orientation = Orientation.Vertical;
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer1.Orientation = Orientation.Horizontal;
        }

        private void incluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(this, null);
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(this, null);
        }

        private void excluirToolStripMenuItem_Click_1(object sender, EventArgs e)
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

        private void toolStripDropDownButton4_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string descricao = e.ClickedItem.Text;

            ValorSelecionado = descricao;

            for (int i = 0; i < _psPart.PSPartApp.Count; i++)
            {
                if (_psPart.PSPartApp[i].AppName == descricao)
                {
                    ExecutaAplicativo(_psPart.PSPartApp[i]);
                }
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

        private void relatóriosToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string descricao = e.ClickedItem.Text;

            if (dataGridView1.Rows.Count != 0)
            {
                for (int i = 0; i < _psPart.PSPartStReport.Count; i++)
                {
                    if (_psPart.PSPartStReport[i].ReportName == descricao)
                    {
                        _psPart.PSPartStReport[i].DataField = null;
                        _psPart.PSPartStReport[i].DataGrid = this.dataGridView1;
                        _psPart.PSPartStReport[i].Access = AppAccess.View;
                        _psPart.PSPartStReport[i].Execute();
                    }
                }
            }
        }

        private void FrmBaseVisao_Shown(object sender, EventArgs e)
        {
            // Necessário devido o problema com o Bind das colunas customizadas
            MontaGridViewValores();

            this._psPart.CustomDataGridView(dataGridView1);
        }

        private void criarAnexoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    List<DataField> objArr = new List<DataField>();

                    int vSelecionado = dataGridView1.CurrentRow.Index;

                    for (int j = 0; j < this._psPart.Keys.Length; j++)
                    {
                        DataField obj = new DataField();
                        obj.Field = dataGridView1.Columns[_psPart.Keys[j]].Name;
                        obj.Valor = dataGridView1.Rows[vSelecionado].Cells[_psPart.Keys[j]].Value;
                        obj.Tipo = dataGridView1.Rows[vSelecionado].Cells[_psPart.Keys[j]].Value.GetType();

                        objArr.Add(obj);
                    }

                    Anexo.FrmBaseAnexo f = new Anexo.FrmBaseAnexo();
                    f.DataField = objArr;
                    f.nSeq = 0;
                    f.PSPartName = _psPart.SecurityID;
                    f.ShowDialog();

                    CarregaAnexos();
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void anexosToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (int.Parse(e.ClickedItem.Tag.ToString()) > 0)
            {
                string descricao = e.ClickedItem.Text;

                if (dataGridView1.Rows.Count != 0)
                {
                    List<DataField> objArr = new List<DataField>();

                    int vSelecionado = dataGridView1.CurrentRow.Index;

                    for (int j = 0; j < this._psPart.Keys.Length; j++)
                    {
                        DataField obj = new DataField();
                        obj.Field = dataGridView1.Columns[_psPart.Keys[j]].Name;
                        obj.Valor = dataGridView1.Rows[vSelecionado].Cells[_psPart.Keys[j]].Value;
                        obj.Tipo = dataGridView1.Rows[vSelecionado].Cells[_psPart.Keys[j]].Value.GetType();

                        objArr.Add(obj);
                    }

                    Anexo.FrmBaseAnexo f = new Anexo.FrmBaseAnexo();
                    f.DataField = objArr;
                    f.nSeq = int.Parse(e.ClickedItem.Tag.ToString());
                    f.PSPartName = _psPart.SecurityID;
                    f.ShowDialog();

                    CarregaAnexos();
                }
            }
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

        private void FrmBaseVisao_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            _psPart.MasterDetail.GetEnumerator().Dispose();
            _psPart.Folder.GetEnumerator().Dispose();
            _psPart.PSPartApp.GetEnumerator().Dispose();
            _psPart.PSPartStReport.GetEnumerator().Dispose();

            _psPart.DefaultFilter.GetEnumerator().Dispose();
            _psPart.DefaultCustomDataColumns.GetEnumerator().Dispose();
            */
        }

        private void FrmBaseVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            // PSMemoryManager.ReleaseUnusedMemory(false);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1_Click(this, null);
        }

        private void limparSeleçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStripButtonLOCALIZAR_Click(this, null);
            }
        }

        private void toolStripDropDownButton5_Click(object sender, EventArgs e)
        {
            GerenciarFiltro();
        }

        public void SalvarConfiguracaoColuna()
        {
            try
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    GUSUARIOVISAO item = new GUSUARIOVISAO();

                    item.VISAO = this._psData._tablename;
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

        private void gerenciarVisãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConfigurarColunas f = new FormConfigurarColunas(this._psData._tablename, PS.Lib.Contexto.Session.CodUsuario, dataGridView1);
            f.ShowDialog();
            this.toolStripButtonATUALIZAR1_ButtonClick(this, null);
        }

        private void salvarVisãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SalvarConfiguracaoColuna();
        }

        public void CarregarConfiguracaoColuna()
        {
            String consulta1 = @"
SELECT *
FROM GUSUARIOVISAO
WHERE VISAO = ?
  AND CODUSUARIO = ?
ORDER BY VISIVEL DESC, SEQUENCIA, COLUNA";

            System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(consulta1, new Object[] { this._psData._tablename, PS.Lib.Contexto.Session.CodUsuario });

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].DisplayIndex = 0;
                    dataGridView1.Columns[i].Visible = false;
                }

                if (dataGridView1.Columns.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            dataGridView1.Columns[dt.Rows[i]["COLUNA"].ToString()].DisplayIndex = int.Parse(dt.Rows[i]["SEQUENCIA"].ToString());
                            dataGridView1.Columns[dt.Rows[i]["COLUNA"].ToString()].Width = int.Parse(dt.Rows[i]["LARGURA"].ToString());

                            if (int.Parse(dt.Rows[i]["VISIVEL"].ToString()) == 1)
                            {
                                dataGridView1.Columns[dt.Rows[i]["COLUNA"].ToString()].Visible = true;
                            }
                            else
                            {
                                dataGridView1.Columns[dt.Rows[i]["COLUNA"].ToString()].Visible = false;
                            }
                        }
                        catch { }
                    }
                }


            }
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

            this.MontaGridViewValores();
        }

        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // busca o caminho da planilha temporária
                String arquivo = System.Reflection.Assembly.GetExecutingAssembly().Location;
                System.IO.FileInfo fi = new FileInfo(arquivo);
                String pasta = fi.DirectoryName;
                String planilha = pasta + "\\Temp.xlsx";

                // remove a planilha
                if (System.IO.File.Exists(planilha))
                {
                    System.IO.File.Delete(planilha);
                }

                // cria planilha vazia
                System.IO.File.WriteAllBytes(planilha, Properties.Resources.temp);

                // carrega a planilha
                AppLib.Util.ExcelManager em = new AppLib.Util.ExcelManager(planilha);
                em.Abrir();
                String aba1 = em.GetAbas()[0];

                // primeiro o cabeçalho
                em.GetAba(aba1);
                for (int i = 0; i < dtOrdenado.Columns.Count; i++)
                {
                    em.SetValor((i + 1), 1, dtOrdenado.Columns[i].ColumnName);
                }

                // agora os dados
                em.SetDados2(aba1, "A", 2, dtOrdenado);
                em.Fechar(true);

                // abre a planilha
                System.Diagnostics.Process.Start(planilha);
            }
            catch (Exception ex)
            {
                AppLib.Windows.FormMessageDefault.ShowError(ex.Message);
            }
        }

    }
}