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
    public partial class FrmBaseEditCompl : DevExpress.XtraEditors.XtraForm
    {
        private Global gb = new Global();
        private PSPartData psData = new PSPartData();
        private Security sec = new Security();        
        private List<DataField> current = new List<DataField>();
        private DataTable _data { get; set; }

        public List<DataField> _setDefault { get; set; }
        public String _tabela { get; set; } // tabela usada na edição
        public String[] _chaves { get; set; } // chaves da tabela da
        public String _formtext { get; set; } // texto do form
        public PSPartData _psData { get; set; } // classe de dados

        public String SecurityID { get; set; }
        public String ModuleID { get; set; }

        public FrmBaseEditCompl()
        {
            InitializeComponent();
        }

        private void LoopControls(Control control, string target)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl.GetType() == typeof(PSTextoBox))
                {
                    PSTextoBox p = new PSTextoBox();
                    p = (PSTextoBox)childControl;

                    if (target == "M")
                        p.Caption = gb.NomeDoCampo(this._tabela, p.DataField);

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
                        p.Text = _data.Rows[0][p.DataField].ToString();

                        if (IsKey(p.DataField))
                        {
                            p.Edita = false;
                        }
                    }

                    if (target == "S")
                    {
                        DataField obj = new DataField();
                        obj.Field = p.DataField;
                        obj.Autoinc = p.AutoIncremento;

                        if (p.Text.Equals(""))
                            obj.Valor = null;
                        else
                            obj.Valor = p.Text;

                        obj.Tipo = childControl.GetType();
                        current.Add(obj);
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField();
                            obj.Field = p.DataField;
                            obj.Autoinc = p.AutoIncremento;

                            if (p.Text.Equals(""))
                                obj.Valor = null;
                            else
                                obj.Valor = p.Text;

                            obj.Tipo = childControl.GetType();
                            current.Add(obj);
                        }
                    }
                }

                if (childControl.GetType() == typeof(PSMaskedTextBox))
                {
                    PSMaskedTextBox p = new PSMaskedTextBox();
                    p = (PSMaskedTextBox)childControl;

                    if (target == "M")
                        p.Caption = gb.NomeDoCampo(this._tabela, p.DataField);

                    if (target == "N")
                    {
                        p.Text = string.Empty;

                        if (IsKey(p.DataField))
                        {
                            p.Chave = true;
                        }
                    }

                    if (target == "C")
                    {
                        p.Text = _data.Rows[0][p.DataField].ToString();

                        if (IsKey(p.DataField))
                        {
                            p.Chave = false;
                        }
                    }

                    if (target == "S")
                    {
                        DataField obj = new DataField();
                        obj.Field = p.DataField;

                        if (p.Text.Equals(""))
                            obj.Valor = null;
                        else
                            obj.Valor = p.Text;

                        obj.Tipo = childControl.GetType();
                        current.Add(obj);
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField();
                            obj.Field = p.DataField;

                            if (p.Text.Equals(""))
                                obj.Valor = null;
                            else
                                obj.Valor = p.Text;

                            obj.Tipo = childControl.GetType();
                            current.Add(obj);
                        }
                    }
                }

                if (childControl.GetType() == typeof(PSMemoBox))
                {
                    PSMemoBox p = new PSMemoBox();
                    p = (PSMemoBox)childControl;

                    if (target == "M")
                        p.Caption = gb.NomeDoCampo(this._tabela, p.DataField);

                    if (target == "N")
                    {
                        p.Text = string.Empty;

                        if (IsKey(p.DataField))
                        {
                            p.Chave = true;
                        }
                    }

                    if (target == "C")
                    {
                        p.Text = _data.Rows[0][p.DataField].ToString();

                        if (IsKey(p.DataField))
                        {
                            p.Chave = false;
                        }
                    }

                    if (target == "S")
                    {
                        DataField obj = new DataField();
                        obj.Field = p.DataField;

                        if (p.Text.Equals(""))
                            obj.Valor = null;
                        else
                            obj.Valor = p.Text;

                        obj.Tipo = childControl.GetType();
                        current.Add(obj);
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField();
                            obj.Field = p.DataField;

                            if (p.Text.Equals(""))
                                obj.Valor = null;
                            else
                                obj.Valor = p.Text;

                            obj.Tipo = childControl.GetType();
                            current.Add(obj);
                        }
                    }
                }

                if (childControl.GetType() == typeof(PSMoedaBox))
                {
                    PSMoedaBox p = new PSMoedaBox();
                    p = (PSMoedaBox)childControl;

                    if (target == "M")
                        p.Caption = gb.NomeDoCampo(this._tabela, p.DataField);

                    if (target == "N")
                    {
                        p.Text = "0";

                        if (IsKey(p.DataField))
                        {
                            p.Edita = true;
                        }
                    }

                    if (target == "C")
                    {
                        p.Text = _data.Rows[0][p.DataField].ToString();

                        if (IsKey(p.DataField))
                        {
                            p.Edita = false;
                        }
                    }

                    if (target == "S")
                    {
                        DataField obj = new DataField();
                        obj.Field = p.DataField;
                        obj.Valor = Convert.ToDouble(p.Text);

                        obj.Tipo = childControl.GetType();
                        current.Add(obj);
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField();
                            obj.Field = p.DataField;
                            obj.Valor = Convert.ToDouble(p.Text);

                            obj.Tipo = childControl.GetType();
                            current.Add(obj);
                        }
                    }
                }

                if (childControl.GetType() == typeof(PSComboBox))
                {
                    PSComboBox p = new PSComboBox();
                    p = (PSComboBox)childControl;

                    if (target == "M")
                        p.Caption = gb.NomeDoCampo(this._tabela, p.DataField);

                    if (target == "N")
                    {
                        p.SelectedIndex = 0;

                        if (IsKey(p.DataField))
                        {
                            p.Chave = true;
                        }
                    }

                    if (target == "C")
                    {
                        p.Value = _data.Rows[0][p.DataField];

                        if (IsKey(p.DataField))
                        {
                            p.Chave = false;
                        }
                    }

                    if (target == "S")
                    {
                        DataField obj = new DataField();
                        obj.Field = p.DataField;
                        obj.Valor = p.Value;
                        obj.Tipo = childControl.GetType();
                        current.Add(obj);
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField();
                            obj.Field = p.DataField;
                            obj.Valor = p.Value;
                            obj.Tipo = childControl.GetType();
                            current.Add(obj);
                        }
                    }
                }

                if (childControl.GetType() == typeof(PSCheckBox))
                {
                    PSCheckBox p = new PSCheckBox();
                    p = (PSCheckBox)childControl;

                    if (target == "M")
                        p.Caption = gb.NomeDoCampo(this._tabela, p.DataField);

                    if (target == "N")
                    {
                        p.Checked = false;

                        if (IsKey(p.DataField))
                        {
                            p.Chave = true;
                        }
                    }

                    if (target == "C")
                    {
                        try
                        {
                            p.Checked = (bool)_data.Rows[0][p.DataField];
                        }
                        catch
                        {
                            if (int.Parse(_data.Rows[0][p.DataField].ToString()) == 1)
                            {
                                p.Checked = true;
                            }
                            else
                            {
                                p.Checked = false;
                            }

                            if (IsKey(p.DataField))
                            {
                                p.Chave = false;
                            }
                        }                        
                    }

                    if (target == "S")
                    {
                        DataField obj = new DataField();
                        obj.Field = p.DataField;

                        if (p.Checked)
                            obj.Valor = 1;
                        else
                            obj.Valor = 0;

                        obj.Tipo = childControl.GetType();
                        current.Add(obj);
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField();
                            obj.Field = p.DataField;

                            if (p.Checked)
                                obj.Valor = 1;
                            else
                                obj.Valor = 0;

                            obj.Tipo = childControl.GetType();
                            current.Add(obj);
                        }
                    }
                }

                if (childControl.GetType() == typeof(PSLookup))
                {
                    PSLookup p = new PSLookup();
                    p = (PSLookup)childControl;

                    if (target == "M")
                        p.Caption = gb.NomeDoCampo(this._tabela, p.DataField);

                    if (target == "N")
                    {
                        p.Text = string.Empty;
                        p.ClearLookup();

                        if (IsKey(p.DataField))
                        {
                            p.Chave = true;
                        }
                    }

                    if (target == "C")
                    {
                        if (p.DataField != string.Empty)
                        {
                            p.Text = _data.Rows[0][p.DataField].ToString();
                            p.LoadLookup();

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
                            DataField obj = new DataField();
                            obj.Field = p.DataField;

                            if (p.Text.Equals(""))
                            {
                                obj.Valor = null;
                            }
                            else
                            {
                                // validar o lookup
                                p.LoadLookup();
                                obj.Valor = p.Text;
                            }

                            obj.Tipo = childControl.GetType();
                            current.Add(obj);
                        }
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField();
                            obj.Field = p.DataField;

                            if (p.Text.Equals(""))
                            {
                                obj.Valor = null;
                            }
                            else
                            {
                                p.LoadLookup();
                                obj.Valor = p.Text;
                            }

                            obj.Tipo = childControl.GetType();
                            current.Add(obj);
                        }
                    }
                }

                if (childControl.GetType() == typeof(PSDateBox))
                {
                    PSDateBox p = new PSDateBox();
                    p = (PSDateBox)childControl;

                    if (target == "M")
                        p.Caption = gb.NomeDoCampo(this._tabela, p.DataField);

                    if (target == "N")
                    {
                        if (p.Mascara == "00/00/0000 00:00")
                        {
                            string data = DateTime.Today.ToShortDateString();
                            string hora = DateTime.Now.ToShortTimeString();

                            p.Text = string.Concat(data, " ", hora.PadLeft(5, '0'));
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

                    if (target == "C")
                    {
                        if (_data.Rows[0][p.DataField] == DBNull.Value)
                            p.Text = null;
                        else
                            p.Value = Convert.ToDateTime(_data.Rows[0][p.DataField]);

                        if (IsKey(p.DataField))
                        {
                            p.Chave = false;
                        }
                    }

                    if (target == "S")
                    {
                        DataField obj = new DataField();
                        obj.Field = p.DataField;

                        if (p.Text == null)
                        {
                            obj.Valor = null;
                        }
                        else
                        {
                            obj.Valor = Convert.ToDateTime(p.Text);
                        }

                        obj.Tipo = childControl.GetType();
                        current.Add(obj);
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField();
                            obj.Field = p.DataField;

                            if (p.Text == null)
                            {
                                obj.Valor = null;
                            }
                            else
                            {
                                obj.Valor = Convert.ToDateTime(p.Text);
                            }

                            obj.Tipo = childControl.GetType();
                            current.Add(obj);
                        }
                    }
                }

                if (childControl.GetType() == typeof(PSImageBox))
                {
                    PSImageBox p = new PSImageBox();
                    p = (PSImageBox)childControl;

                    if (target == "C")
                        p.IdImagem = int.Parse(_data.Rows[0][p.DataField].ToString());

                    if (target == "N")
                        p.IdImagem = 0;

                    if (target == "S")
                    {
                        DataField obj = new DataField();
                        obj.Field = p.DataField;

                        obj.Valor = p.IdImagem;

                        obj.Tipo = childControl.GetType();
                        current.Add(obj);
                    }

                    if (target == "K")
                    {
                        if (IsKey(p.DataField))
                        {
                            DataField obj = new DataField();
                            obj.Field = p.DataField;

                            obj.Valor = p.IdImagem;

                            obj.Tipo = childControl.GetType();
                            current.Add(obj);
                        }
                    }
                }

                if (childControl.HasChildren)
                    LoopControls(childControl, target);
            }
        }

        private void AdicionaControles()
        {
            DataTable dtCamposCompl = gb.RetornaCamposComplementares(_tabela);

            int eixoY = 0;

            for (int i = 0; i < dtCamposCompl.Rows.Count; i++)
            {
                if (i == 0)
                    eixoY = 6;
                else
                    eixoY = eixoY + 45;

                if (dtCamposCompl.Rows[i]["TIPO"].ToString() == "PSTextoBox")
                {
                    PS.Lib.WinForms.PSTextoBox t = new PS.Lib.WinForms.PSTextoBox();

                    t.DataField = dtCamposCompl.Rows[i]["NOMECAMPO"].ToString();
                    t.Name = dtCamposCompl.Rows[i]["NOMECAMPO"].ToString();
                    t.MaxLength = int.Parse(dtCamposCompl.Rows[i]["TAMANHO"].ToString());

                    t.Caption = dtCamposCompl.Rows[i]["DESCRICAO"].ToString();
                    t.Edita = true;

                    t.Location = new System.Drawing.Point(11, eixoY);
                    t.Size = new System.Drawing.Size(145, 37);
                    t.TabIndex = 0;

                    this.tabPage1.Controls.Add(t);
                }

                if (dtCamposCompl.Rows[i]["TIPO"].ToString() == "PSDateBox")
                {
                    PS.Lib.WinForms.PSDateBox t = new PS.Lib.WinForms.PSDateBox();

                    t.DataField = dtCamposCompl.Rows[i]["NOMECAMPO"].ToString();
                    t.Name = dtCamposCompl.Rows[i]["NOMECAMPO"].ToString();

                    t.Caption = dtCamposCompl.Rows[i]["DESCRICAO"].ToString();
                    t.Chave = true;

                    t.Location = new System.Drawing.Point(11, eixoY);
                    t.Size = new System.Drawing.Size(145, 37);
                    t.TabIndex = 0;

                    this.tabPage1.Controls.Add(t);
                }

                if (dtCamposCompl.Rows[i]["TIPO"].ToString() == "PSLookup")
                {
                    PS.Lib.WinForms.PSLookup t = new PS.Lib.WinForms.PSLookup();

                    t.DinamicTable = dtCamposCompl.Rows[i]["CODTABELA"].ToString();
                    t.DataField = dtCamposCompl.Rows[i]["NOMECAMPO"].ToString();
                    t.Name = dtCamposCompl.Rows[i]["NOMECAMPO"].ToString();
                    t.MaxLength = 25;

                    t.Caption = dtCamposCompl.Rows[i]["DESCRICAO"].ToString();
                    t.Chave = true;

                    t.PSPart = "PSPartTabDinamicaItem";

                    t.KeyField = "CODREGISTRO";
                    t.LookupField = "CODREGISTRO;DESCRICAO";
                    t.LookupFieldResult = "CODREGISTRO;DESCRICAO";

                    t.Location = new System.Drawing.Point(11, eixoY);
                    t.Size = new System.Drawing.Size(401, 38);
                    t.TabIndex = 0;

                    t.BeforeLookup += new PS.Lib.WinForms.PSLookup.BeforeLookupDelegate(this.PSLookup_BeforeLookup);

                    this.tabPage1.Controls.Add(t);
                }

                if (dtCamposCompl.Rows[i]["TIPO"].ToString() == "PSCheckBox")
                {
                    PS.Lib.WinForms.PSCheckBox t = new PS.Lib.WinForms.PSCheckBox();

                    t.DataField = dtCamposCompl.Rows[i]["NOMECAMPO"].ToString();
                    t.Name = dtCamposCompl.Rows[i]["NOMECAMPO"].ToString();

                    t.Caption = dtCamposCompl.Rows[i]["DESCRICAO"].ToString();
                    t.Chave = true;

                    t.Location = new System.Drawing.Point(11, eixoY);
                    t.Size = new System.Drawing.Size(145, 37);
                    t.TabIndex = 0;

                    this.tabPage1.Controls.Add(t);
                }

                if (dtCamposCompl.Rows[i]["TIPO"].ToString() == "PSMoedaBox")
                {
                    PS.Lib.WinForms.PSMoedaBox t = new PS.Lib.WinForms.PSMoedaBox();

                    t.DataField = dtCamposCompl.Rows[i]["NOMECAMPO"].ToString();
                    t.Name = dtCamposCompl.Rows[i]["NOMECAMPO"].ToString();

                    t.CasasDecimais = int.Parse(dtCamposCompl.Rows[i]["CASASDECIMAIS"].ToString());

                    t.Caption = dtCamposCompl.Rows[i]["DESCRICAO"].ToString();
                    t.Edita = true;

                    t.Location = new System.Drawing.Point(11, eixoY);
                    t.Size = new System.Drawing.Size(145, 37);
                    t.TabIndex = 0;

                    this.tabPage1.Controls.Add(t);
                }
            }        
        }

        private void AtualizaRegistroEdit()
        {
            if (_psData != null)
            {
                if (_data.Rows.Count > 0)
                {

                    List<DataField> objArr = _psData.ReadRecordEdit(_data.Rows[0]);

                    for (int i = 0; i < objArr.Count; i++)
                    {
                        for (int j = 0; j < _data.Columns.Count; j++)
                        {
                            if (objArr[i].Field == _data.Columns[j].ColumnName)
                            {
                                _data.Rows[0][j] = objArr[i].Valor;
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Não foi encontrado registro complementar para o registro master.");                
                }
            }
        }        

        private bool IsKey(string campo)
        {
            for (int i = 0; i < this._chaves.Length; i++)
            {
                if (this._chaves[i] == campo)
                    return true;
            }

            return false;
        }

        private void CarregaParametros()
        {
            this.Text = gb.NomeDaTabela(_tabela);
            this.psData = _psData;

            AtualizaGrid();
            AdicionaControles();
        }

        private void AtualizaGrid()
        {
            _data = this._psData.ExecuteFilterMasterDetail(_tabela, _chaves, _setDefault);
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

        public virtual void CarregaParametrosTela()
        {

        }

        public virtual void ValidaRegistro(List<DataField> objArr)
        {
            psData.ValidateRecord(objArr);
        }

        public virtual void CarregaRegistro()
        {
            AtualizaRegistroEdit();
            LoopControls(this, "C");
            tabControl1.SelectTab(0);
        }

        public virtual void SalvaRegistro()
        {
            try
            {
                List<DataField> objArr = PreparaCrud();

                ValidaRegistro(objArr);

                // se verdadeiro significa que não possui campos complementares
                if (objArr.Count > _setDefault.Count)
                {
                    _setDefault = psData.SaveRecord(objArr);

                    PSMessageBox.ShowInfo("Operação realizada com sucesso");

                    AtualizaGrid();
                    CarregaRegistro();
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);            }
        }

        private void PSLookup_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            if (this.ActiveControl != null)
            {
                if (this.ActiveControl.GetType() == typeof(PS.Lib.WinForms.PSLookup))
                {
                    PS.Lib.WinForms.PSLookup t = (PS.Lib.WinForms.PSLookup)this.ActiveControl;

                    e.Filtro.Add(new PS.Lib.PSFilter("CODTABELA", t.DinamicTable));
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            SalvaRegistro();
        }

        private void FrmBaseEdit_Load(object sender, EventArgs e)
        {
            CarregaParametros();
            CarregaRegistro();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            CarregaRegistro();
        }

        private void FrmBaseEdit_KeyDown(object sender, KeyEventArgs e)
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

                if (flag.Equals(Keys.F5))
                {
                    toolStripButton4_Click(this, null);
                }

                if (flag.Equals(Keys.F6))
                {
                    toolStripButton5_Click(this, null);
                } 
            }
        }

        private void FrmBaseEditCompl_FormClosed(object sender, FormClosedEventArgs e)
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