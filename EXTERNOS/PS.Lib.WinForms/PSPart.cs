using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;

namespace PS.Lib.WinForms
{
    public class PSPart
    {
        private PSInstance Instance = new PSInstance();

        private bool _AllowInsert;
        private bool _AllowEdit;
        private bool _AllowDelete;
        private bool _AllowSave;

        private Security sec = new Security();

        public string TableName { get; set; }
        public string[] Keys { get; set; }

        public string FormEditName { get; set; }
        public string DataServerName { get; set; }

        public PSPartData PSPartData { get; set; }

        public List<MasterDetail> MasterDetail = new List<MasterDetail>();
        public List<Folder> Folder = new List<Folder>();
        public List<PSPartApp> PSPartApp = new List<PSPartApp>();
        public List<StaticReport.PSPartStaticReport> PSPartStReport = new List<StaticReport.PSPartStaticReport>();

        public List<PSFilter> DefaultFilter = new List<PSFilter>();
        public List<PSFilter> DefaultFilterLookup = new List<PSFilter>();

        public List<CustomDataColumn> DefaultCustomDataColumns = new List<CustomDataColumn>();

        public FolderType folderType { get; set; }

        public string SecurityID { get; set; }
        public string ModuleID { get; set; }

        [DefaultValue(true)]
        public bool AllowInsert
        {
            get
            {
                return _AllowInsert;
            }
            set
            {
                _AllowInsert = value;
            }
        }

        [DefaultValue(true)]
        public bool AllowEdit
        {
            get
            {
                return _AllowEdit;
            }
            set
            {
                _AllowEdit = value;
            }
        }

        [DefaultValue(true)]
        public bool AllowDelete
        {
            get
            {
                return _AllowDelete;
            }
            set
            {
                _AllowDelete = value;
            }
        }

        [DefaultValue(true)]
        public bool AllowSave
        {
            get
            {
                return _AllowSave;
            }
            set
            {
                _AllowSave = value;
            }
        }

        public PSPart()
        {
            _AllowInsert = true;
            _AllowEdit = true;
            _AllowDelete = true;        
        }

        public FolderType GetFolderType()
        {
            return folderType;
        }

        public virtual void CustomDataGridView(DataGridView DataGrid)
        {
            if (DataGrid.Rows.Count <= 0)
                return;
        }

        public bool IsKey(string campo)
        {
            for (int i = 0; i < this.Keys.Length; i++)
            {
                if (this.Keys[i] == campo)
                    return true;
            }

            return false;
        }

        public virtual void Execute(List<DataField> ObjArr)
        {
            if (!sec.AuthenticatedUser())
            {
                throw new Exception("Atenção. Usuario não autenticado.");
            }

            if (!sec.SelectedContext())
            {
                throw new Exception("Atenção. Selecione uma Empresa para prosseguir.");
            }

            if (!sec.ValidaControle(Contexto.Session.Empresa.CodControle, Contexto.Session.Empresa.CNPJCPF, Contexto.Session.Empresa.CodChave1, Contexto.Session.Empresa.CodChave2))
            {
                throw new Exception("Atenção. Código de Controle Inválido.");
            }

            if (!sec.ValidAccess(this.SecurityID, this.ModuleID))
            {
                throw new Exception(string.Concat("Atenção. Usuário não tem permissão de acesso [",this.SecurityID,"]."));
            }
        }

        public virtual void Execute()
        {
            if (!sec.AuthenticatedUser())
            {
                throw new Exception("Atenção. Usuario não autenticado.");
            }

            if (!sec.SelectedContext())
            {
                throw new Exception("Atenção. Selecione uma Empresa para prosseguir.");
            }

            if (!sec.ValidaControle(Contexto.Session.Empresa.CodControle, Contexto.Session.Empresa.CNPJCPF, Contexto.Session.Empresa.CodChave1, Contexto.Session.Empresa.CodChave2))
            {
                throw new Exception("Atenção. Código de Controle Inválido.");            
            }

            if (!sec.ValidAccess(this.SecurityID, this.ModuleID))
            {
                throw new Exception(string.Concat("Atenção. Usuário não tem permissão de acesso [", this.SecurityID, "]."));
            }
        }

        public virtual void ExecuteWithParams(params object[] parameters)
        {
            PS.Lib.Global gb = new PS.Lib.Global();

            if (!sec.AuthenticatedUser())
            {
                throw new Exception("Atenção. Usuario não autenticado.");
            }

            if (!sec.SelectedContext())
            {
                throw new Exception("Atenção. Selecione uma Empresa para prosseguir.");
            }

            if (!sec.ValidaControle(Contexto.Session.Empresa.CodControle, Contexto.Session.Empresa.CNPJCPF, Contexto.Session.Empresa.CodChave1, Contexto.Session.Empresa.CodChave2))
            {
                throw new Exception("Atenção. Código de Controle Inválido.");
            }

            if (!sec.ValidAccess(this.SecurityID, this.ModuleID))
            {
                throw new Exception(string.Concat("Atenção. Usuário não tem permissão de acesso [", this.SecurityID, "]."));
            }

            try
            {   
                this.PSPartData._tablename = this.TableName;
                this.PSPartData._keys = this.Keys;

                FrmBaseEdit FormEdit = (FrmBaseEdit)Instance.CreateInstanceFormEdit(this.FormEditName);
                FormEdit._psPart = this;
                FormEdit._selecionado = 0;
                FormEdit._data = this.PSPartData.ReadRecordEdit(parameters);
                FormEdit._psPartData = this.PSPartData;
                FormEdit._filtro = null;

                FormEdit.PermiteExcluir = this.AllowDelete;
                FormEdit.PermiteIncluir = this.AllowInsert;
                FormEdit.PermiteSavar = this.AllowSave;

                FormEdit.ShowDialog();
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        public virtual List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
