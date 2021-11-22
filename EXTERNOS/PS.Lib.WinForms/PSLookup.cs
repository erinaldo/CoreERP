using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PS.Lib.WinForms
{
    public partial class PSLookup : UserControl
    {
        #region ATRIBUTOS PS FRAMEWORK

        private Constantes ct = new Constantes();
        private Global gb = new Global();
        private Security sec = new Security();
        private LookupEventArgs eventArgs = new LookupEventArgs();
        private List<PSFilter> filtroLookup = new List<PSFilter>();
        private string tDataField;
        private PSPart PSPartLookup = new PSPart();
        private PSInstance Instance = new PSInstance();
        private DataTable DataForm;
        
        #endregion

        #region ATRIBUTOS APPLIB2

        public AppLib.Windows.CampoLookup campoLookup1 = new AppLib.Windows.CampoLookup();

        #endregion

        #region PROPRIEDADES PS FRAMEWORK

        [Category("PSLib"), Description("Evento chamado antes de fazer a pesquisa no lookup")]
        public event BeforeLookupDelegate BeforeLookup;

        [Category("PSLib"), Description("Evento chamado depois de fazer a pesquisa no lookup")]
        public event AfterLookupDelegate AfterLookup;

        [Category("PSLib"), Description("Não preencher esta propriedade")]
        public string ValorRetorno { get; set; }

        [Category("PSLib"), Description("DataField")]
        public string DataField
        {
            set
            {
                this.tDataField = value;
                if (this.tDataField != string.Empty)
                    this.label1.Text = value;
            }
            get
            {
                return this.tDataField;
            }
        }

        [Category("PSLib"), Description("DinamicTable")]
        public string DinamicTable { get; set; }

        [Category("PSLib"), Description("KeyField")]
        public string KeyField { get; set; }

        [Category("PSLib"), Description("LookupField")]
        public string LookupField { get; set; }

        [Category("PSLib"), Description("LookupFieldResult")]
        public string LookupFieldResult { get; set; }

        [Category("PSLib"), Description("PSPart")]
        public string PSPart { get; set; }

        [Category("PSLib"), Description("Chave"), DefaultValue(false)]
        public bool Chave
        {
            set
            {
                this.textBox1.Enabled = value;
                this.button1.Enabled = value;
                /*
                if (value)
                    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                else
                    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                */
            }
            get
            {
                this.button1.Enabled = this.textBox1.Enabled;
                return this.textBox1.Enabled;
            }
        }

        [Category("PSLib"), Description("Text")]
        public override string Text
        {
            set
            {
                this.textBox1.Text = value;
            }
            get
            {
                return this.textBox1.Text;
            }
        }

        [Category("PSLib"), Description("Description")]
        public string Description
        {
            set
            {
                this.textBox2.Text = value;
            }
            get
            {
                return this.textBox2.Text;
            }
        }

        [Category("PSLib"), Description("Caption")]
        public string Caption
        {
            set
            {
                this.label1.Text = value;
            }
            get
            {
                return this.label1.Text;
            }
        }

        [Category("PSLib"), Description("MaxLength"), DefaultValue(Int32.MaxValue)]
        public int MaxLength
        {
            set
            {
                this.textBox1.MaxLength = value;
            }
            get
            {
                return this.textBox1.MaxLength;
            }
        }

        [Category("PSLib"), Description("Formulário Consulta do Lookup")]
        public Framework FormConsulta { get; set; }

        [Category("PSLib"), Description("Formulário Cadastro do Lookup")]
        public Framework FormCadastro { get; set; }

        #endregion

        #region CONSTRUTOR

        public PSLookup()
        {
            InitializeComponent();
            FormConsulta = WinForms.Framework.PSFramework;
        }
        
        #endregion

        #region EVENTOS

        private void button1_Click(object sender, EventArgs e)
        {
            if (FormConsulta == WinForms.Framework.PSFramework)
            {
                #region REGRA

                OnBeforeLookup(new LookupEventArgs());

                FrmBaseLookup p = new FrmBaseLookup(this);

                PSPartLookup = (PSPart)Instance.CreateInstance(this.PSPart);

                p.psPart = PSPartLookup;
                p.KeyField = this.KeyField;
                p.LookupField = this.LookupField;
                p.LookupFieldResult = this.LookupFieldResult;

                p.psPart.PSPartData._tablename = PSPartLookup.TableName;
                p.psPart.PSPartData._keys = PSPartLookup.Keys;

                p.SecurityID = PSPartLookup.SecurityID;
                p.ModuleID = PSPartLookup.ModuleID;

                p._masterDetail = PSPartLookup.MasterDetail;
                p._pastaDetail = PSPartLookup.Folder;
                p._aplicativo = PSPartLookup.PSPartApp;

                // objeto de apoio 
                List<PSFilter> filtroLookup1 = new List<PSFilter>();

                for (int i = 0; i < PSPartLookup.DefaultFilterLookup.Count; i++)
                {
                    if (PSPartLookup.DefaultFilterLookup[i].Value != null)
                    {
                        PSFilter pf = new PSFilter(PSPartLookup.DefaultFilterLookup[i].Field, PSPartLookup.DefaultFilterLookup[i].Value);
                        filtroLookup1.Add(pf);
                    }
                }

                if (p.filtroLookup == null)
                {
                    p.filtroLookup = new List<PSFilter>();
                }

                if (filtroLookup != null)
                {
                    for (int i = 0; i < filtroLookup.Count; i++)
                    {
                        PSFilter pf = new PSFilter(filtroLookup[i].Field, filtroLookup[i].Oper, filtroLookup[i].Value);
                        filtroLookup1.Add(pf);
                    }

                    /*
                    for (int i = 0; i < filtroLookup.Count; i++)
                    {
                        if (filtroLookup[i].Value == null || filtroLookup[i].Value.ToString() == string.Empty)
                        {
                            PSMessageBox.ShowError(string.Concat("Nem todos os campos necessários foram preenchido para o campo: ", gb.NomeDoCampo(PSPartLookup.PSPartData._tablename, this.DataField)));

                            textBox1.Text = string.Empty;
                            textBox2.Text = string.Empty;

                            return;
                        }
                        else
                        {
                            PSFilter pf = new PSFilter(filtroLookup[i].Field, filtroLookup[i].Value);
                            filtroLookup1.Add(pf);
                        }
                    }
                    */
                }

                p.filtroLookup = filtroLookup1;

                p.ShowDialog();

                if (ValorRetorno != null)
                {
                    Regex rSplit = new Regex(";");
                    String[] sFields = rSplit.Split(this.ValorRetorno);

                    textBox1.Text = sFields[0].ToString();
                    // textBox2.Text = sFields[1].ToString();

                    this.LoadLookup();

                    // OnAfterLookup(new LookupEventArgs());
                }

                p.Dispose();
                
                #endregion
            }
            else
            {
                try
                {
                    this.SetFormConsulta(this, null);
                    textBox1.Text = campoLookup1.Get();
                    this.LoadLookup();
                }
                catch { }
            }            
        }

        private void PSLookup_KeyDown(object sender, KeyEventArgs e)
        {
            Keys flag = e.KeyCode;

            if (flag.Equals(Keys.Enter))
            {
                //SelectNextControl(ActiveControl, !e.Shift, true, true, true);
                //e.Handled = true;

                if (textBox1.Focus())
                    if (textBox1.Text == string.Empty)
                        button1_Click(this, null);
                    else
                        return;
                else
                    return;
            }

            if (flag.Equals(Keys.F2))
            {
                button1_Click(this, null);
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            LoadLookup();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {
                textBox2.Cursor = Cursors.Hand;
                textBox2.Font = ct.fonteCampoLookup;
                textBox2.ForeColor = Color.Blue;
            }
            else
            {
                textBox2.Cursor = Cursors.IBeam;
                textBox2.Font = ct.fonteCampoDefault;
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (FormCadastro == WinForms.Framework.PSFramework)
            {
                if (textBox2.Text != string.Empty)
                {
                    PSPartLookup = (PSPart)Instance.CreateInstance(this.PSPart);
                    PSPartLookup.PSPartData._tablename = PSPartLookup.TableName;
                    PSPartLookup.PSPartData._keys = PSPartLookup.Keys;

                    if (!sec.ValidAccess(PSPartLookup.SecurityID, PSPartLookup.ModuleID))
                    {
                        PSMessageBox.ShowError("Atenção. Usuário não tem permissão de acesso.");
                        return;
                    }

                    int vSelecionado = 0;

                    FrmBaseEdit f = (FrmBaseEdit)Instance.CreateInstanceFormEdit(PSPartLookup.FormEditName);
                    f._psPart = PSPartLookup;
                    f._selecionado = vSelecionado;
                    f._data = DataForm;
                    f._psPartData = PSPartLookup.PSPartData;
                    f.PermiteEditar = PSPartLookup.AllowEdit;
                    f.PermiteExcluir = PSPartLookup.AllowDelete;
                    f.PermiteIncluir = PSPartLookup.AllowInsert;

                    f.ShowDialog();

                    this.LoadLookup();
                }
                else
                {
                    return;
                }
            }
            else
            {
                
                try
                {
                    AppLib.Windows.CampoLookup campoLookup1 = new AppLib.Windows.CampoLookup();
                    this.SetFormCadastro(this, null);
                    this.LoadLookup();
                }
                catch { }
            }            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //if (sender == textBox1)
            //{
            //    if (!textBox1.Focused)
            //    {
            //        return;
            //    }
            //    try
            //    {
            //        BaseLookupAutoComplete.SetAutoComplete();
            //        UpdateAutoCompleteViewLocation();
            //        return;
            //    }
            //    finally
            //    {
            //        if (!textBox1.Focused)
            //        {
            //            textBox1.Focus();
            //        }
            //    }
            //}
        }

        #endregion

        #region MÉTODOS

        public void OperSearchForm()
        {
            button1_Click(this, null);
        }

        public void ClearLookup()
        {
            textBox2.Text = string.Empty;
        }

        public void edita(bool estado)
        {
            textBox1.Enabled = estado;
            textBox2.Enabled = estado;
            button1.Enabled = estado;
        }

        public void LoadLookup()
        {
            try
            {

                if (!textBox1.Text.Equals(string.Empty))
                {
                    OnBeforeLookup(new LookupEventArgs());

                    bool Flag = false;

                    DataField filter = new DataField();

                    filter.Field = this.KeyField;
                    filter.Valor = this.textBox1.Text;

                    //cria a instancia do objeto do lookup
                    PSPartLookup = (PSPart)Instance.CreateInstance(this.PSPart);

                    PSPartLookup.PSPartData._tablename = PSPartLookup.TableName;
                    PSPartLookup.PSPartData._keys = PSPartLookup.Keys;

                    // objeto de apoio 
                    List<PSFilter> filtroLookup1 = new List<PSFilter>();

                    for (int i = 0; i < PSPartLookup.DefaultFilterLookup.Count; i++)
                    {
                        if (PSPartLookup.DefaultFilterLookup[i].Value != null)
                        {
                            PSFilter pf = new PSFilter(PSPartLookup.DefaultFilterLookup[i].Field, PSPartLookup.DefaultFilterLookup[i].Oper, PSPartLookup.DefaultFilterLookup[i].Value);
                            filtroLookup1.Add(pf);
                        }
                    }

                    if (filtroLookup != null)
                    {
                        for (int i = 0; i < filtroLookup.Count; i++)
                        {
                            if (filtroLookup[i].Value != null)
                            {
                                PSFilter pf = new PSFilter(filtroLookup[i].Field, filtroLookup[i].Oper, filtroLookup[i].Value);
                                filtroLookup1.Add(pf);
                            }
                            else
                            {
                                textBox1.Text = string.Empty;
                                textBox2.Text = string.Empty;

                                throw new Exception(string.Concat("Valor digitado não é válido para o campo: ", gb.NomeDoCampo(PSPartLookup.PSPartData._tablename, this.DataField)));
                            }
                        }
                    }

                    DataTable dt = new DataTable();
                    PSPartLookup.PSPartData.FillLookup(ref dt, filter, filtroLookup1);

                    DataForm = dt;

                    Regex rSplit = new Regex(";");
                    String[] sFields = rSplit.Split(LookupFieldResult);
                    String[] sResult = rSplit.Split(LookupFieldResult);
                    string retorno = "";

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            for (int j = 0; j < sFields.Length; j++)
                            {
                                if (dt.Columns[i].ColumnName.ToString() == sFields[j])
                                {
                                    if (j == 0)
                                    {
                                        retorno = dt.Rows[0][i].ToString();
                                    }
                                    else
                                    {
                                        retorno = string.Concat(retorno, ";", dt.Rows[0][i].ToString());
                                    }
                                }
                            }
                        }

                        if (retorno != null)
                        {
                            int cont = sResult.Length;

                            sFields = rSplit.Split(retorno);

                            if (cont != sFields.Length)
                            {
                                for (int y = 0; y < PSPartLookup.DefaultCustomDataColumns.Count; y++)
                                {
                                    for (int j = 0; j < sResult.Length; j++)
                                    {
                                        if (sResult[j] == PSPartLookup.DefaultCustomDataColumns[y].DataName)
                                        {
                                            Flag = true;

                                            for (int z = 0; z < filtroLookup1.Count; z++)
                                            {
                                                for (int w = 0; w < PSPartLookup.DefaultCustomDataColumns[y].Condition.Length; w++)
                                                {
                                                    if (filtroLookup1[z].Field == PSPartLookup.DefaultCustomDataColumns[y].Condition[w].ColumnNameRefGrid)
                                                    {
                                                        PSPartLookup.DefaultCustomDataColumns[y].Condition[w].Value = filtroLookup1[z].Value;
                                                    }

                                                    if (filter.Field == PSPartLookup.DefaultCustomDataColumns[y].Condition[w].ColumnNameRefGrid)
                                                    {
                                                        PSPartLookup.DefaultCustomDataColumns[y].Condition[w].Value = filter.Valor;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (Flag)
                                        retorno = string.Concat(retorno, ";", PSPartLookup.DefaultCustomDataColumns[y].LoadLookup());
                                }

                                sFields = rSplit.Split(retorno);
                            }

                            textBox1.Text = sFields[0].ToString();
                            textBox2.Text = sFields[1].ToString();

                            OnAfterLookup(new LookupEventArgs());
                        }
                    }
                    else
                    {
                        if (FormConsulta == WinForms.Framework.PSFramework)
                        {
                            textBox1.Text = string.Empty;
                            textBox2.Text = string.Empty;

                            throw new Exception(string.Concat("Valor digitado não é válido para o campo: ", gb.NomeDoCampo(PSPartLookup.PSPartData._tablename, this.DataField)));
                        }
                        else
                        {
                            try
                            {
                                this.button1_Click(this, null);
                            }
                            catch { }
                        }
                    }
                }
                else
                {
                    textBox1.Text = string.Empty;
                    textBox2.Text = string.Empty;
                }

                // PSMemoryManager.ReleaseUnusedMemory(false);
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        #region Teste Auto Lookup
        /*
        private FrmBaseLookupAutoComplete _frmBaseLookupAutoComplete;

        private FrmBaseLookupAutoComplete BaseLookupAutoComplete
        {
            get
            {
                if (_frmBaseLookupAutoComplete == null)
                {
                    _frmBaseLookupAutoComplete = new FrmBaseLookupAutoComplete();
                    _frmBaseLookupAutoComplete.OnHideForm += new FrmBaseLookupAutoComplete.OnHideFormDelegate(_frmBaseLookupAutoComplete_OnHideForm);
                }
                return _frmBaseLookupAutoComplete;
            }
        }

        private void _frmBaseLookupAutoComplete_OnHideForm()
        {
            if (_frmBaseLookupAutoComplete != null)
            {
                _frmBaseLookupAutoComplete.Close();
                _frmBaseLookupAutoComplete.Dispose();
                _frmBaseLookupAutoComplete = null;
            }
        }
        */
        #endregion

        #endregion

        #region DELEGATES PS FRAMEWORK

        public delegate void BeforeLookupDelegate(object sender, LookupEventArgs e);
        public delegate void AfterLookupDelegate(object sender, LookupEventArgs e);

        private void OnBeforeLookup(LookupEventArgs e)
        {
            if (BeforeLookup != null)
            {
                BeforeLookup(this, e);

                filtroLookup = e.Filtro;
            }
        }

        private void OnAfterLookup(LookupEventArgs e)
        {
            if (AfterLookup != null)
            {
                AfterLookup(this, e);
            }
        }

        #endregion

        #region DELEGATES APPLIB2

        public delegate Boolean SetFormConsultaHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método setar o formulário de consulta"), DefaultValue(false)]
        public event SetFormConsultaHandler SetFormConsulta;

        public delegate void SetFormCadastroHandler(object sender, EventArgs e);
        [Category("_APP"), Description("Método setar o formulário de cadastro"), DefaultValue(false)]
        public event SetFormCadastroHandler SetFormCadastro;

        #endregion


    }

    public enum Framework { PSFramework, AppLib2 }

}
