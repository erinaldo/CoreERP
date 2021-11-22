using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PS.Lib.WinForms;

namespace PS.Lib.WinForms
{
    public partial class FrmBaseEdit : DevExpress.XtraEditors.XtraForm
    {
        private Global gb = new Global();
        private Security sec = new Security();
        private BindingSource bds = new BindingSource();
        private List<DataField> current = new List<DataField>();
        protected FormRowState formRowState;

        public PSPart _psPart { get; set; }
        public PSPartData _psPartData { get; set; }

        public int _selecionado { get; set; }
        public DataTable _data { get; set; }

        public List<DataField> _setDefault { get; set; }
        public Filter _filtro = new Filter();

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

        [DefaultValue(false)]
        public bool PermiteEditar;
        /*
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
        */
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

        [DefaultValue(true)]
        public bool PermiteSavar
        {
            get
            {
                return buttonSALVAR.Enabled;
            }
            set
            {
                buttonSALVAR.Enabled = value;
                buttonOK.Enabled = value;
            }
        }
        public bool PermiteInserir
        {
            get
            {
                return toolStripButton1.Enabled;
            }
            set
            {
                toolStripButton1.Enabled = value;
               
            }
        }
        
        public FrmBaseEdit()
        {
            InitializeComponent();
        }

        private void FrmBaseEdit_Load(object sender, EventArgs e)
        {
            if (_psPart != null)
            {
                CarregaParametros();

                if (_selecionado == -1)
                {
                    NovoRegistro();
                }
                else
                {
                    CarregaRegistro();
                }
            }


        }

        private void LoopHabilitaAba(Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.GetType() == typeof(PSBaseVisao))
                {
                    PSBaseVisao p = (PSBaseVisao)childControl;
                    List<DataField> objArr = PreparaCrudChave();

                    for (int w = 0; w < objArr.Count; w++)
                    {
                        if (IsKey(objArr[w].Field))
                        {
                            if (objArr[w].Autoinc != Global.TypeAutoinc.None)
                            {
                                if (int.Parse(objArr[w].Valor.ToString()) == 0)
                                {
                                    p.Enabled = false;
                                }
                                else
                                {
                                    p.Enabled = true;
                                }
                            }
                            else
                            {
                                if (_selecionado >= 0)
                                {
                                    p.Enabled = true;
                                }
                                else
                                {
                                    p.Enabled = false;
                                }
                            }
                        }
                    }
                }

                if (childControl.HasChildren)
                    this.LoopHabilitaAba(childControl);
            }
        }

        private void LoopMasterDetailControl(Control control, MasterDetail masterDetail, List<DataField> objArr)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.GetType() == typeof(PSBaseVisao))
                {
                    PSBaseVisao p = (PSBaseVisao)childControl;
                    if (p.Name == masterDetail.psPartDetail.SecurityID)
                    {
                        p.psPart = masterDetail.psPartDetail;

                        for (int x = 0; x < objArr.Count; x++)
                        {
                            for (int h = 0; h < masterDetail.dataFilterDetail.Count; h++)
                            {
                                if (objArr[x].Field == masterDetail.dataFilterDetail[h].Field)
                                {
                                    masterDetail.dataFilterDetail[h] = objArr[x];
                                }
                            }
                        }

                        p.CarregaRegistro(masterDetail.dataFilterDetail);
                    }
                }

                if (childControl.HasChildren)
                    this.LoopMasterDetailControl(childControl, masterDetail, objArr);
            }
        }

        private void LoopControls(Control control, string target)
        {
            foreach (Control childControl in control.Controls)
            {
                #region PSTextoBox

                if (childControl.GetType() == typeof(PSTextoBox))
                {
                    PSTextoBox p = (PSTextoBox)childControl;

                    if (target == "M")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Caption = gb.NomeDoCampo(_psPart.TableName, p.DataField);
                        }
                    }

                    if (target == "N")
                    {
                        if (p.AutoIncremento == Global.TypeAutoinc.None)
                        {
                            p.Text = string.Empty;
                        }
                        else
                        {
                            p.Text = "0";
                        }

                        if (IsKey(p.DataField))
                        {
                            p.Edita = true;
                        }
                    }

                    if (target == "C")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Text = _data.Rows[_selecionado][p.DataField].ToString();

                            if (IsKey(p.DataField))
                            {
                                p.Edita = false;
                            }
                        }
                    }

                    if (target == "S")
                    {
                        if (p.DataField != string.Empty)
                        {
                            DataField obj = new DataField(p.DataField, null, p.GetType());
                            obj.Autoinc = p.AutoIncremento;

                            if (p.Text.Equals(""))
                                obj.Valor = null;
                            else
                                obj.Valor = p.Text;

                            current.Add(obj);
                        }
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField(p.DataField, null, p.GetType());
                            obj.Autoinc = p.AutoIncremento;

                            if (p.Text.Equals(""))
                                obj.Valor = null;
                            else
                                obj.Valor = p.Text;

                            current.Add(obj);
                        }
                    }
                }

                #endregion

                #region PSMaskedTextBox

                if (childControl.GetType() == typeof(PSMaskedTextBox))
                {
                    PSMaskedTextBox p = (PSMaskedTextBox)childControl;

                    if (target == "M")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Caption = gb.NomeDoCampo(_psPart.TableName, p.DataField);
                        }
                    }

                    if (target == "N")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Text = string.Empty;

                            if (IsKey(p.DataField))
                            {
                                p.Chave = true;
                            }
                        }
                    }

                    if (target == "C")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Text = _data.Rows[_selecionado][p.DataField].ToString();

                            if (IsKey(p.DataField))
                            {
                                p.Chave = false;
                            }
                        }
                    }

                    if (target == "S")
                    {
                        if (p.DataField != string.Empty)
                        {
                            DataField obj = new DataField(p.DataField, null, p.GetType());

                            if (p.Text.Equals(""))
                                obj.Valor = null;
                            else
                                obj.Valor = p.Text;

                            current.Add(obj);
                        }
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField(p.DataField, null, p.GetType());

                            if (p.Text.Equals(""))
                                obj.Valor = null;
                            else
                                obj.Valor = p.Text;

                            current.Add(obj);
                        }
                    }
                }

                #endregion

                #region PSMemoBox

                if (childControl.GetType() == typeof(PSMemoBox))
                {
                    PSMemoBox p = (PSMemoBox)childControl;

                    if (target == "M")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Caption = gb.NomeDoCampo(_psPart.TableName, p.DataField);
                        }
                    }

                    if (target == "N")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Text = string.Empty;

                            if (IsKey(p.DataField))
                            {
                                p.Chave = true;
                            }
                        }
                    }

                    if (target == "C")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Text = _data.Rows[_selecionado][p.DataField].ToString();

                            if (IsKey(p.DataField))
                            {
                                p.Chave = false;
                            }
                        }
                    }

                    if (target == "S")
                    {
                        if (p.DataField != string.Empty)
                        {
                            DataField obj = new DataField(p.DataField, null, p.GetType());

                            if (p.Text.Equals(""))
                                obj.Valor = null;
                            else
                                obj.Valor = p.Text;

                            current.Add(obj);
                        }
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField(p.DataField, null, p.GetType());

                            if (p.Text.Equals(""))
                                obj.Valor = null;
                            else
                                obj.Valor = p.Text;

                            current.Add(obj);
                        }
                    }
                }

                #endregion

                #region PSMoedaBox

                if (childControl.GetType() == typeof(PSMoedaBox))
                {
                    PSMoedaBox p = (PSMoedaBox)childControl;

                    if (target == "M")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Caption = gb.NomeDoCampo(_psPart.TableName, p.DataField);
                        }
                    }

                    if (target == "N")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Text = "0";

                            if (IsKey(p.DataField))
                            {
                                p.Edita = true;
                            }
                        }
                    }

                    if (target == "C")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Text = _data.Rows[_selecionado][p.DataField].ToString();

                            if (IsKey(p.DataField))
                            {
                                p.Edita = false;
                            }
                        }
                    }

                    if (target == "S")
                    {
                        if (p.DataField != string.Empty)
                        {
                            DataField obj = new DataField(p.DataField, Convert.ToDouble(p.Text), p.GetType());
                            current.Add(obj);
                        }
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField(p.DataField, Convert.ToDouble(p.Text), p.GetType());
                            current.Add(obj);
                        }
                    }
                }

                #endregion

                #region PSComboBox

                if (childControl.GetType() == typeof(PSComboBox))
                {
                    PSComboBox p = (PSComboBox)childControl;

                    if (target == "M")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Caption = gb.NomeDoCampo(_psPart.TableName, p.DataField);
                        }
                    }

                    if (target == "N")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.SelectedIndex = 0;

                            if (IsKey(p.DataField))
                            {
                                p.Chave = true;
                            }
                        }
                    }

                    if (target == "C")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Value = _data.Rows[_selecionado][p.DataField];

                            if (IsKey(p.DataField))
                            {
                                p.Chave = false;
                            }
                        }
                    }

                    if (target == "S")
                    {
                        if (p.DataField != string.Empty)
                        {
                            DataField obj = new DataField(p.DataField, p.Value, p.GetType());
                            current.Add(obj);
                        }
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField(p.DataField, p.Value, p.GetType());
                            current.Add(obj);
                        }
                    }
                }

                #endregion

                #region PSCheckBox

                if (childControl.GetType() == typeof(PSCheckBox))
                {
                    PSCheckBox p = (PSCheckBox)childControl;

                    if (target == "M")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Caption = gb.NomeDoCampo(_psPart.TableName, p.DataField);
                        }
                    }

                    if (target == "N")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Checked = false;

                            if (IsKey(p.DataField))
                            {
                                p.Chave = true;
                            }
                        }
                    }

                    if (target == "C")
                    {
                        if (p.DataField != string.Empty)
                        {
                            try
                            {
                                p.Checked = (bool)_data.Rows[_selecionado][p.DataField];
                            }
                            catch
                            {
                                if (Convert.ToInt32(_data.Rows[_selecionado][p.DataField]) == 1)
                                {
                                    p.Checked = true;
                                }
                                else
                                {
                                    p.Checked = false;
                                }
                            }

                            if (IsKey(p.DataField))
                            {
                                p.Chave = false;
                            }
                        }
                    }

                    if (target == "S")
                    {
                        if (p.DataField != string.Empty)
                        {
                            DataField obj = new DataField(p.DataField, null, p.GetType());

                            if (p.Checked)
                                obj.Valor = 1;
                            else
                                obj.Valor = 0;

                            current.Add(obj);
                        }
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField(p.DataField, null, p.GetType());

                            if (p.Checked)
                                obj.Valor = 1;
                            else
                                obj.Valor = 0;

                            current.Add(obj);
                        }
                    }
                }

                #endregion

                #region PSLookup

                if (childControl.GetType() == typeof(PSLookup))
                {
                    PSLookup p = (PSLookup)childControl;

                    if (target == "M")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Caption = gb.NomeDoCampo(_psPart.TableName, p.DataField);
                        }
                    }

                    if (target == "N")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Text = string.Empty;
                            p.ClearLookup();

                            if (IsKey(p.DataField))
                            {
                                p.Chave = true;
                            }
                        }
                    }

                    if (target == "C")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Text = _data.Rows[_selecionado][p.DataField].ToString();

                            if (p.Text != string.Empty)
                                p.LoadLookup();
                            else
                                p.Description = string.Empty;

                            if (IsKey(p.DataField))
                            {
                                p.Chave = false;
                            }
                        }
                    }

                    if (target == "S")
                    {
                        if (p.DataField != string.Empty)
                        {
                            DataField obj = new DataField(p.DataField, null, p.GetType());

                            if (p.Text.Equals(""))
                            {
                                obj.Valor = null;
                            }
                            else
                            {
                                p.LoadLookup();
                                obj.Valor = p.Text;
                            }
                            current.Add(obj);
                        }
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField(p.DataField, null, p.GetType());

                            if (p.Text.Equals(""))
                            {
                                obj.Valor = null;
                            }
                            else
                            {
                                p.LoadLookup();
                                obj.Valor = p.Text;
                            }

                            current.Add(obj);
                        }
                    }
                }

                #endregion

                #region PSDateBox

                if (childControl.GetType() == typeof(PSDateBox))
                {
                    PSDateBox p = new PSDateBox();
                    p = (PSDateBox)childControl;

                    if (target == "M")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Caption = gb.NomeDoCampo(_psPart.TableName, p.DataField);
                        }
                    }

                    if (target == "N")
                    {
                        if (p.DataField != string.Empty)
                        {
                            if (p.Mascara == "00/00/0000 00:00")
                            {
                                string data = DateTime.Today.ToShortDateString();
                                string hora = DateTime.Now.ToShortTimeString();

                                p.Text = string.Concat(data, " ", hora.PadLeft(5, '0'));
                            }
                            if (p.Mascara == "00/00/0000 00:00:00")
                            {
                                p.Text = DateTime.Now.ToString();
                            }
                            if (p.Mascara == "00/00/0000")
                            {
                                p.Text = DateTime.Today.ToShortDateString();
                            }

                            if (IsKey(p.DataField))
                            {
                                p.Chave = true;
                            }
                        }
                    }

                    if (target == "C")
                    {
                        if (p.DataField != string.Empty)
                        {
                            if (_data.Rows[_selecionado][p.DataField] == DBNull.Value)
                                p.Text = null;
                            else
                                p.Value = Convert.ToDateTime(_data.Rows[_selecionado][p.DataField]);

                            if (IsKey(p.DataField))
                            {
                                p.Chave = false;
                            }
                        }
                    }

                    if (target == "S")
                    {
                        if (p.Mascara == "00/00/0000 00:00:00")
                        {
                            try
                            {
                                if (p.Text == null)
                                {
                                    p.Text = null;
                                }
                            }
                            catch (Exception)
                            {
                                p.Text = null;
                            }
                            
                        }
                        if (p.DataField != string.Empty)
                        {
                            DataField obj = new DataField(p.DataField, null, p.GetType());
                            try
                            {
                                if (p.Text == null)
                                {
                                    obj.Valor = null;
                                }
                                else
                                {
                                    obj.Valor = Convert.ToDateTime(p.Text);
                                }
                            }
                            catch (Exception)
                            {
                                obj.Valor = null;
                            }




                        //if (p.Mascara == "00/00/0000 00:00:00")
                        //{
                        //    string data = DateTime.Today.ToShortDateString();
                        //    string hora = DateTime.Now.ToShortTimeString();

                        //    p.Text = DateTime.Now.ToString();
                        //    //p.Text = _data.Rows[_selecionado][p.DataField].ToString();
                        //}
                      
                            //if (p.Text == null)
                            //{
                            //    obj.Valor = null;
                            //}
                            //else
                            //{
                            //    obj.Valor = Convert.ToDateTime(p.Text);
                            //}
                            current.Add(obj);
                        }
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField(p.DataField, null, p.GetType());

                            if (p.Text == null)
                            {
                                obj.Valor = null;
                            }
                            else
                            {
                                obj.Valor = Convert.ToDateTime(p.Text);
                            }

                            current.Add(obj);
                        }
                    }
                }

                #endregion

                #region PSImageBox

                if (childControl.GetType() == typeof(PSImageBox))
                {
                    PSImageBox p = (PSImageBox)childControl;

                    if (target == "C")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.IdImagem = int.Parse(_data.Rows[_selecionado][p.DataField].ToString());
                        }
                    }

                    if (target == "N")
                        p.IdImagem = 0;

                    if (target == "S")
                    {
                        if (p.DataField != string.Empty)
                        {
                            DataField obj = new DataField(p.DataField, null, p.GetType());
                            obj.Valor = p.IdImagem;
                            current.Add(obj);
                        }
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField(p.DataField, null, p.GetType());

                            obj.Valor = p.IdImagem;
                            current.Add(obj);
                        }
                    }
                }

                #endregion

                if (childControl.HasChildren)
                    LoopControls(childControl, target);
            }
        }

        private void AtualizaRegistroEdit()
        {
            if (_selecionado >= 0)
            {
                if (_psPartData != null)
                {
                    List<DataField> objArr = _psPartData.ReadRecordEdit(_data.Rows[_selecionado]);

                    for (int i = 0; i < objArr.Count; i++)
                    {
                        for (int j = 0; j < _data.Columns.Count; j++)
                        {
                            if (objArr[i].Field == _data.Columns[j].ColumnName)
                            {
                                if (_data.Columns[j].ReadOnly)
                                {
                                    _data.Columns[j].ReadOnly = false;
                                    _data.Rows[_selecionado][j] = objArr[i].Valor;
                                    _data.Columns[j].ReadOnly = true;
                                }
                                else
                                {
                                    _data.Rows[_selecionado][j] = objArr[i].Valor;
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool IsKey(string campo)
        {
            for (int i = 0; i < _psPart.Keys.Length; i++)
            {
                if (_psPart.Keys[i] == campo)
                    return true;
            }

            return false;
        }

        private void FecharPasta(int index)
        {
            tabControl1.TabPages.RemoveAt(index);
        }

        private void FecharPasta(TabPage page)
        {
            tabControl1.TabPages.Remove(page);
        }

        private void AtualizaEditPasta(Folder pasta)
        {
            try
            {
                List<DataField> objArr = new List<DataField>();

                if (_selecionado >= 0)
                {
                    for (int j = 0; j < pasta.dataFilterDetail.Count; j++)
                    {
                        DataField obj = new DataField();
                        obj.Field = _data.Columns[pasta.dataFilterDetail[j].Field].ColumnName;
                        obj.Valor = _data.Rows[_selecionado][pasta.dataFilterDetail[j].Field].ToString();
                        obj.Tipo = _data.Rows[_selecionado][pasta.dataFilterDetail[j].Field].GetType();

                        objArr.Add(obj);
                    }
                }

                pasta.psPartDetail.Execute(objArr);
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
                List<DataField> objArr = new List<DataField>();

                if (_selecionado >= 0)
                {
                    for (int j = 0; j < pasta.dataFilterDetail.Count; j++)
                    {
                        DataField obj = new DataField();
                        obj.Field = _data.Columns[pasta.dataFilterDetail[j].Field].ColumnName;
                        obj.Valor = _data.Rows[_selecionado][pasta.dataFilterDetail[j].Field].ToString();
                        obj.Tipo = _data.Rows[_selecionado][pasta.dataFilterDetail[j].Field].GetType();

                        objArr.Add(obj);
                    }
                }

                PSBaseVisao p = (PSBaseVisao)tab.Controls[0];
                p.psPart = pasta.psPartDetail;
                p.aplicativo = pasta.psPartDetail.PSPartApp;

                p.CarregaRegistro(objArr);
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void AbrirPasta(Folder pasta)
        {
            if (!sec.ValidAccess(pasta.psPartDetail.SecurityID, pasta.psPartDetail.ModuleID))
            {
                PSMessageBox.ShowError("Atenção. Usuário não tem permissão de acesso ao menu " + pasta.psPartDetail.SecurityID + ".");
            }
            else
            {
                if (pasta.psPartDetail.GetFolderType() == FolderType.View)
                {
                    bool criarPasta = true;
                    TabPage selectPag = new TabPage();

                    if (tabControl1.TabPages.Count > 0)
                    {
                        for (int i = 0; i < tabControl1.TabPages.Count; i++)
                        {
                            if (tabControl1.TabPages[i].Name == pasta.psPartDetail.SecurityID)
                            {
                                criarPasta = false;
                                selectPag = tabControl1.TabPages[i];
                                break;
                            }
                        }
                    }

                    if (criarPasta)
                    {
                        selectPag.Text = gb.NomeDaTabela(pasta.psPartDetail.TableName);
                        selectPag.Name = pasta.psPartDetail.SecurityID;

                        PSBaseVisao p = new PSBaseVisao();
                        p.psPart = pasta.psPartDetail;
                        p.Dock = DockStyle.Fill;

                        selectPag.Controls.Add(p);

                        tabControl1.TabPages.Add(selectPag);
                    }
                    else
                    {
                        AtualizaGridPasta(pasta, selectPag);
                    }
                }

                if (pasta.psPartDetail.GetFolderType() == FolderType.Edit)
                {
                    AtualizaEditPasta(pasta);
                }
            }
        }

        private void CarregaAnexos()
        {
            try
            {
                if (_selecionado >= 0)
                {
                    List<DataField> objArr = new List<DataField>();
                    List<DataField> objArr1 = new List<DataField>();

                    objArr = PreparaCrudChave();

                    if (_psPartData == null)
                        return;

                    if (_psPartData.ExistRecord(objArr))
                    {
                        for (int j = 0; j < _psPart.Keys.Length; j++)
                        {
                            for (int i = 0; i < objArr.Count; i++)
                            {
                                if (objArr[i].Field == _psPart.Keys[j])
                                {
                                    DataField obj = new DataField();
                                    obj.Field = objArr[i].Field;
                                    obj.Valor = objArr[i].Valor;
                                    obj.Tipo = objArr[i].Tipo;

                                    objArr1.Add(obj);
                                }
                            }
                        }

                        DataTable dt = gb.BuscaAnexos(objArr1, _psPart.SecurityID);

                        MontaListaAnexos(dt);
                    }
                    else
                    {
                        //throw new Exception("Salve o registro antes de executar um aplicativo.");
                    }
                }
            }
            catch
            {
                //PSMessageBox.ShowError(ex.Message);
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
            //bool Flag = false;

            try
            {
                if (_psPart.PSPartApp.Count > 0)
                {
                    if (toolStripDropDownButton4.DropDownItems.Count == 0)
                    {
                        for (int i = 0; i < _psPart.PSPartApp.Count; i++)
                        {
                            if (_psPart.PSPartApp[i].Image == null)
                            {
                                toolStripDropDownButton4.DropDownItems.Add(_psPart.PSPartApp[i].AppName);
                            }
                            else
                            {
                                toolStripDropDownButton4.DropDownItems.Add(_psPart.PSPartApp[i].AppName, _psPart.PSPartApp[i].Image.Image);
                            }

                            /*
                            for (int j = 0; j < toolStripDropDownButton4.DropDownItems.Count; j++)
                            {
                                if (toolStripDropDownButton4.DropDownItems[j].Text == _aplicativo[i].AppName)
                                {
                                    Flag = true;
                                }

                                if (!Flag)
                                {
                                    if (_aplicativo[i].Image == null)
                                    {
                                        toolStripDropDownButton4.DropDownItems.Add(_aplicativo[i].AppName);
                                    }
                                    else
                                    {
                                        toolStripDropDownButton4.DropDownItems.Add(_aplicativo[i].AppName, _aplicativo[i].Image.Image);
                                    }
                                }
                            }

                            if (!Flag)
                            {
                                toolStripDropDownButton4.DropDownItems.Add(_aplicativo[i].AppName);
                            }
                            */
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
            try
            {
                List<DataField> objArr = new List<DataField>();

                objArr = PreparaCrud();

                if (_psPartData.ExistRecord(objArr))
                {
                    app.Access = AppAccess.Edit;
                    app.DataGrid = null;
                    app.DataField = objArr;
                    app.TableName = _psPart.TableName;
                    app.Keys = _psPart.Keys;
                    app.Execute();

                    if (app.Refresh)
                    {
                        CarregaRegistro();
                    }
                }
                else
                {
                    throw new Exception("Salve o registro antes de executar um aplicativo.");
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void HabilitaAbas()
        {
            try
            {
                this.LoopHabilitaAba(this);
            }
            catch
            {
                // para não dar erro quando se navega nas tabpages em tempo de design
                return;
            }
        }

        private void CarregaPasta()
        {
            if (_psPart.Folder.Count > 0)
            {
                if (toolStripDropDownButton1.DropDownItems.Count < (_psPart.Folder.Count + 5))
                {
                    toolStripDropDownButton1.DropDownItems.Add("-");

                    for (int i = 0; i < _psPart.Folder.Count; i++)
                    {
                        toolStripDropDownButton1.DropDownItems.Add(gb.NomeDaTabela(_psPart.Folder[i].psPartDetail.TableName));

                        if (_psPart.Folder[i].autoShow)
                        {
                            AbrirPasta(_psPart.Folder[i]);
                        }
                    }
                }
            }
        }

        private void CarregaStaticReport()
        {
            try
            {
                if (_psPart.SecurityID != null)
                {
                    Report.ReportUtil rpu = new Report.ReportUtil();

                    if (_psPart.SecurityID == "PSPartOperacao")
                    {
                        DataField dfCODTIPOPER = gb.RetornaDataFieldByFilter(_psPart.DefaultFilter, "CODTIPOPER");
                        _psPart.PSPartStReport = rpu.LoadStaticReportList(_psPart.SecurityID, dfCODTIPOPER.Valor.ToString());
                    }
                    else
                    {
                        _psPart.PSPartStReport = rpu.LoadStaticReportList(_psPart.SecurityID);
                    }

                    //remove tudo
                    for (int i = 0; i < relatóriosToolStripMenuItem.DropDownItems.Count; i++)
                    {
                        relatóriosToolStripMenuItem.DropDownItems.Remove(relatóriosToolStripMenuItem.DropDownItems[i]);
                    }

                    //carrega lista
                    if (_psPart.PSPartStReport.Count > 0)
                    {
                        for (int i = 0; i < _psPart.PSPartStReport.Count; i++)
                        {
                            relatóriosToolStripMenuItem.DropDownItems.Add(_psPart.PSPartStReport[i].ReportName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void MasterDetailEvents()
        {
            List<DataField> objArr = PreparaCrud();

            foreach (MasterDetail masterDetail in _psPart.MasterDetail)
            {
                this.LoopMasterDetailControl(this, masterDetail, objArr);
            }
        }

        private void CarregaParametros()
        {
            if (_psPart != null)
            {
                this.Text = gb.NomeDaTabela(_psPart.TableName);
                this.bds.DataSource = _data;

                LoopControls(this, "M");
                CarregaPasta();
                CarregaAplicativos();
                CarregaStaticReport();
                CarregaAnexos();
            }
        }

        private void AtualizaGrid()
        {
            if (this._setDefault != null)
            {
                _data = this._psPartData.ExecuteFilterMasterDetail(_psPart.TableName, _psPart.Keys, _setDefault);
            }
            else
            {
                //_data = this._psPartData.ExecuteFilter(_filtro, _psPart.TableName, _psPart.Keys, _psPart.DefaultFilter);
                List<Filter> filtros = new List<Filter>();
                filtros.Add(_filtro);
                _data = this._psPartData.ExecuteFilter(filtros, _psPart.TableName, new List<string>(), _psPart.Keys, _psPart.DefaultFilter);
                bds.DataSource = _data;
            }

            _selecionado = gb.RetornaIndiceData(_data, current);
        }

        private void Navegar(int indice)
        {
            if (indice < 0)
                return;

            if (indice == (_data.Rows.Count))
                return;

            _selecionado = indice;
            CarregaRegistro();
        }

        private List<DataField> PreparaCrudChave()
        {
            current.Clear();

            if (_setDefault != null)
            {
                for (int i = 0; i < _setDefault.Count; i++)
                {
                    current.Add(_setDefault[i]);
                }
            }

            LoopControls(this, "K");
            return current;
        }

        private List<DataField> PreparaCrud()
        {
            current.Clear();

            if (_setDefault != null)
            {
                for (int i = 0; i < _setDefault.Count; i++)
                {
                    current.Add(_setDefault[i]);
                }
            }

            LoopControls(this, "S");
            return current;
        }

        private void EditaRegistro()
        {
            // Deixa o foco no primeiro controle possível para edição
        }

        private void DeletaRegistro()
        {
            if (PSMessageBox.ShowQuestion("Deseja excluir o registro ?") == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    List<DataField> objArr = PreparaCrudChave();

                    _psPartData.DeleteRecord(objArr);

                    // PSMessageBox.ShowInfo("Operação realizada com sucesso");

                    AtualizaGrid();
                    // NovoRegistro();

                    this.Close();
                }
                catch (Exception ex)
                {
                    PSMessageBox.ShowError(ex.Message);
                }
            }
        }

        public virtual void CarregaParametrosTela()
        {

        }

        public virtual void ValidaRegistro(List<DataField> objArr)
        {
            _psPartData.ValidateRecord(objArr);
        }

        public virtual void CarregaRegistro()
        {
            try
            {
                if (_selecionado == -1)
                {
                    return;
                }

                formRowState = FormRowState.Modified;
                this.AtualizaRegistroEdit();
                this.LoopControls(this, "C");
                this.MasterDetailEvents();
                this.HabilitaAbas();
                this.CarregaAnexos();
                //this.tabControl1.SelectTab(0);
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        public virtual void NovoRegistro()
        {
            try
            {
                formRowState = FormRowState.Added;
                LoopControls(this, "N");
                _selecionado = -1;
                MasterDetailEvents();
                HabilitaAbas();
                tabControl1.SelectTab(0);
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        public virtual bool ExisteRegistro()
        {
            try
            {
                List<DataField> objArr = PreparaCrudChave();

                return _psPartData.ExistRecord(objArr);
            }
            catch
            {
                return false;
            }
        }

        public virtual void SalvaRegistro()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                List<DataField> objArr = PreparaCrud();

                if (formRowState == FormRowState.Added)
                {
                    if (_psPartData.ExistRecord(objArr))
                    {
                        throw new Exception("Já existe um registro para os dados informados.");
                    }
                }

                current = _psPartData.SaveRecord(objArr);
                if (current.Count.Equals(0))
                {
                    return;
                }
                // PSMessageBox.ShowInfo("Operação realizada com sucesso");
                if (_psPartData._tablename != "GOPERITEM")
                {
                    AtualizaGrid();
                    CarregaRegistro();
                }
               
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        public void toolStripButton1_Click(object sender, EventArgs e)
        {
            NovoRegistro();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            EditaRegistro();
        }

        public void toolStripButton3_Click(object sender, EventArgs e)
        {
            DeletaRegistro();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            SalvaRegistro();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (panel1.Visible == true)
            {
                panel1.Visible = false;
            }
            else
            {
                panel1.Visible = true;
            }

            comboBox1.DataSource = gb.NomeDosCampos(_psPart.TableName);

            comboBox1.DisplayMember = "DESCRICAO";
            comboBox1.ValueMember = "COLUNA";
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            Navegar(0);
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            Navegar(_data.Rows.Count - 1);
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            Navegar(_selecionado - 1);
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            Navegar(_selecionado + 1);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            CarregaRegistro();
        }

        private void FrmBaseEdit_KeyDown(object sender, KeyEventArgs e)
        {
            //Keys flag = e.KeyCode;

            //if (e.Shift || e.Control || e.Alt)
            //{
            //    return;
            //}
            //else
            //{
            //    if (flag.Equals(Keys.Enter))
            //    {
            //        SelectNextControl(ActiveControl, !e.Shift, true, true, true);
            //        e.Handled = true;
            //    }

            //    if (flag.Equals(Keys.Space))
            //    {
            //        if (this.ActiveControl.GetType() == typeof(PSBaseVisao))
            //        {
            //            PSBaseVisao p = (PSBaseVisao)this.ActiveControl;
            //            p.MarcaDesmarca();
            //        }
            //    }

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

            //    if (flag.Equals(Keys.F6))
            //    {
            //        toolStripButton5_Click(this, null);
            //    }
            //}
        }

        private void fecharTodasAsPastasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count > 0)
            {
                TabPage[] lista = new TabPage[tabControl1.TabPages.Count];
                int cont = 0;

                for (int i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    for (int j = 0; j < _psPart.Folder.Count; j++)
                    {
                        if (tabControl1.TabPages[i].Text == gb.NomeDaTabela(_psPart.Folder[j].psPartDetail.TableName))
                        {
                            lista[cont] = tabControl1.TabPages[i];
                            cont++;
                        }
                    }
                }

                for (int i = 0; i < lista.Length; i++)
                {
                    if (lista[i] != null)
                    {
                        FecharPasta(lista[i]);
                    }
                }
            }
        }

        private void fecharPastaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count > 0)
            {
                TabPage tab = tabControl1.SelectedTab;

                for (int i = 0; i < _psPart.Folder.Count; i++)
                {
                    if (tab.Text == gb.NomeDaTabela(_psPart.Folder[i].psPartDetail.TableName))
                    {
                        FecharPasta(tab);
                    }
                }
            }
        }

        private void toolStripDropDownButton1_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "fecharPastaToolStripMenuItem" ||
                e.ClickedItem.Name == "fecharTodasAsPastasToolStripMenuItem")
                return;

            string descricao = e.ClickedItem.Text;

            for (int i = 0; i < _psPart.Folder.Count; i++)
            {
                if (gb.NomeDaTabela(_psPart.Folder[i].psPartDetail.TableName) == descricao)
                {
                    AbrirPasta(_psPart.Folder[i]);
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_psPart != null)
            {
                HabilitaAbas();

                TabPage tab = tabControl1.SelectedTab;

                for (int i = 0; i < _psPart.Folder.Count; i++)
                {
                    if (gb.NomeDaTabela(_psPart.Folder[i].psPartDetail.TableName) == tab.Text)
                    {
                        AbrirPasta(_psPart.Folder[i]);
                    }
                }
            }
        }

        private void toolStripDropDownButton4_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string descricao = e.ClickedItem.Text;

            for (int i = 0; i < _psPart.PSPartApp.Count; i++)
            {
                if (_psPart.PSPartApp[i].AppName == descricao)
                {
                    ExecutaAplicativo(_psPart.PSPartApp[i]);
                }
            }
        }

        private void relatóriosToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                string descricao = e.ClickedItem.Text;

                for (int i = 0; i < _psPart.PSPartStReport.Count; i++)
                {
                    if (_psPart.PSPartStReport[i].ReportName == descricao)
                    {
                        List<DataField> objArr = new List<DataField>();

                        objArr = PreparaCrud();

                        if (_psPartData.ExistRecord(objArr))
                        {
                            _psPart.PSPartStReport[i].DataField = objArr;
                            _psPart.PSPartStReport[i].DataGrid = null;
                            _psPart.PSPartStReport[i].Access = AppAccess.Edit;
                            _psPart.PSPartStReport[i].Execute();
                        }
                        else
                        {
                            throw new Exception("Salve o registro antes de executar um aplicativo.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void criarAnexoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<DataField> objArr = new List<DataField>();
                List<DataField> objArr1 = new List<DataField>();

                objArr = PreparaCrud();

                if (_psPartData.ExistRecord(objArr))
                {
                    for (int j = 0; j < _psPart.Keys.Length; j++)
                    {
                        for (int i = 0; i < objArr.Count; i++)
                        {
                            if (objArr[i].Field == _psPart.Keys[j])
                            {
                                DataField obj = new DataField();
                                obj.Field = objArr[i].Field;
                                obj.Valor = objArr[i].Valor;
                                obj.Tipo = objArr[i].Tipo;

                                objArr1.Add(obj);
                            }
                        }
                    }

                    Anexo.FrmBaseAnexo f = new Anexo.FrmBaseAnexo();
                    f.DataField = objArr1;
                    f.nSeq = 0;
                    f.PSPartName = _psPart.SecurityID;
                    f.ShowDialog();

                    CarregaAnexos();
                }
                else
                {
                    throw new Exception("Salve o registro antes de executar um aplicativo.");
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
                if (_selecionado >= 0)
                {
                    string descricao = e.ClickedItem.Text;

                    List<DataField> objArr = new List<DataField>();
                    List<DataField> objArr1 = new List<DataField>();

                    objArr = PreparaCrud();

                    if (_psPartData.ExistRecord(objArr))
                    {
                        for (int j = 0; j < _psPart.Keys.Length; j++)
                        {
                            for (int i = 0; i < objArr.Count; i++)
                            {
                                if (objArr[i].Field == _psPart.Keys[j])
                                {
                                    DataField obj = new DataField();
                                    obj.Field = objArr[i].Field;
                                    obj.Valor = objArr[i].Valor;
                                    obj.Tipo = objArr[i].Tipo;

                                    objArr1.Add(obj);
                                }
                            }
                        }

                        Anexo.FrmBaseAnexo f = new Anexo.FrmBaseAnexo();
                        f.DataField = objArr1;
                        f.nSeq = int.Parse(e.ClickedItem.Tag.ToString());
                        f.PSPartName = _psPart.SecurityID;
                        f.ShowDialog();

                        CarregaAnexos();
                    }
                    else
                    {
                        throw new Exception("Salve o registro antes de executar um aplicativo.");
                    }
                }
            }
        }

        private void FrmBaseEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            // PSMemoryManager.ReleaseUnusedMemory(false);
        }

        private void buttonSALVAR_Click(object sender, EventArgs e)
        {
            SalvaRegistro();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SalvaRegistro();
            this.Close();
        }

        private void buttonCANCELAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}