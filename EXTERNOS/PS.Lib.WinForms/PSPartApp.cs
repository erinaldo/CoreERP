using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PS.Lib.WinForms
{
    public class PSPartApp
    {
        private Security sec = new Security();

        public string AppName { get; set; } //nome do aplicativo
        public System.Windows.Forms.DataGridView DataGrid { get; set; } //tabela contendo os itens selecionados usado na visao
        public List<DataField> DataField { get; set; } //lista de campos do item selecionado usado na edição
        public string TableName { get; set; } //nome da tabela
        public string[] Keys { get; set; } //chave da tabela
        public PS.Lib.WinForms.FrmBaseApp FormApp { get; set; } //formulário do aplicativo
        public AppAccess Access { get; set; } //por onde esta acessando o aplicativo

        [DefaultValue(false)]
        public bool Refresh { get; set; } //atualiza registro no form de edição

        [DefaultValue(SelectType.MultiRows)]
        public SelectType Select { get; set; } //permite um ou mais registros selecionados

        public ImageProperties Image { get; set; }

        public String SecurityID { get; set; }
        public String ModuleID { get; set; }

        public string _ValorSelecionado;

        public PSPartApp()
        {

        }

        public virtual void Execute()
        {
            try
            {
                if (!sec.AuthenticatedUser())
                {
                    throw new Exception("Atenção. Usuario não autenticado.");
                }

                if (!sec.SelectedContext())
                {
                    throw new Exception("Atenção. Selecione uma Empresa para prosseguir.");
                }

                if (!sec.ValidAccess(this.SecurityID, this.ModuleID))
                {
                    throw new Exception(string.Concat("Atenção. Usuário não tem permissão de acesso ao aplicativo [", this.SecurityID, "]."));
                }

                FormApp.psPartApp = this;
                FormApp.Text = AppName;
                FormApp.ShowDialog();
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }
    }
}
