using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections.ObjectModel;
using ITGProducao.Visao;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;

namespace ITGProducao.Controles
{
    public partial class NewLookup : UserControl
    {
        Collection<NewLookup_GridVisao> NewLookup_Campos = new Collection<NewLookup_GridVisao>();
        Collection<Newlookup_WhereVisao> NewLookup_Where = new Collection<Newlookup_WhereVisao>();
        Collection<Newlookup_OutrasChaves> NewLookup_OutrasChaves = new Collection<Newlookup_OutrasChaves>();

        public enum AbrirPrimeiro
        {
            Filtro,
            Visao
        }

        public enum CampoCodigoIguala
        {
            CampoCodigoBD,
            CampoCodigoInterno
        }

        [Category("ItInit_Propriedades"), Description("Ex: Produto")] public string Titulo { get; set; }
        [Category("ItInit_Propriedades"), Description("Valor númerico e inteiro")] public int Codigo_MaxLenght { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public AbrirPrimeiro Abrir_Primeiro { get; set; }


        [Category("ItInit_Propriedades"), Description("Ex: PS.Glb.New.Filtro.frmFiltroProduto")] public string Formulario_Filtro { get; set; }
        [Category("ItInit_Propriedades"), Description("Ex: PS.Glb.New.Visao.frmVisaoProduto")] public string Formulario_Visao { get; set; }
        [Category("ItInit_Propriedades"), Description("Ex: PS.Glb.New.Cadastros.frmCadastroProduto")] public string Formulario_Cadastro { get; set; }
        [Category("ItInit_Propriedades"), Description("Ex: PS.Glb")] public string Projeto_Formularios { get; set; }
        //[Category("ItInit_Propriedades"), Description("")] public string NomeGridVisao { get; set; }
        //[Category("ItInit_Propriedades"), Description("")] public string NomeGridViewVisao { get; set; }


        [Category("ItInit_Propriedades"), Description(""), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)] public Collection<NewLookup_GridVisao> Grid_Visao { get { return NewLookup_Campos; } }
        [Category("ItInit_Propriedades"), Description(""), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)] public Collection<Newlookup_WhereVisao> Grid_WhereVisao { get { return NewLookup_Where; } }
        [Category("ItInit_Propriedades"), Description(""), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)] public Collection<Newlookup_OutrasChaves> OutrasChaves { get { return NewLookup_OutrasChaves; } }

        [Category("ItInit_Propriedades"), Description("Ex: VPRODUTO")] public string TabelaBD { get; set; }
        [Category("ItInit_Propriedades"), Description("Ex: CODPRODUTO")] public string CampoCodigoBD { get; set; }
        [Category("ItInit_Propriedades"), Description("Ex: NOME")] public string CampoDescricaoBD { get; set; }
        [Category("ItInit_Propriedades"), Description("Propriedade não implementada")] public string MensagemCodigoVazio { get; set; }

        [Category("ItInit_Propriedades"), Description("Propriedade para uso via programação")] public string whereVisao { get; set; }
        [Category("ItInit_Propriedades"), Description("Propriedade para uso via programação")] public string whereParametros { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public CampoCodigoIguala CampoCodigo_Igual_a { get; set; }
        [Category("ItInit_Propriedades"), Description("Ex: CODIGOAUXILIAR")] public string CampoCodigoInterno { get; set; }
        [Category("ItInit_Propriedades"), Description("Propriedade para uso via programação")] public string ValorCodigoInterno{ get; set; }
        [Category("ItInit_Propriedades"), Description("")] public bool CarregaDescricaoSemFiltro { get; set; }

        public event PropertyValueChangedEventHandler PropertyChanged;

        string _mensagemErrorProvider;
        [Category("ItInit_Propriedades"), Description("Propriedade para uso via programação")]
        public string mensagemErrorProvider
        {
            get { return _mensagemErrorProvider; }
            set
            {
                _mensagemErrorProvider = value;
                TrocouValor(_mensagemErrorProvider);
            }
        }

        public void TrocouValor(string mensagem)
        {
            if(!string.IsNullOrEmpty(mensagem))
            {
                errorProvider.SetError(lblerrorprovider, mensagem);
            }
            else
            {
                errorProvider.Clear();
            }
        }
        
        public NewLookup()
        {
            InitializeComponent();
        }

        private void NewLookup_Load(object sender, EventArgs e)
        {
            lbltitulo.Text = (string.IsNullOrEmpty(Titulo) == true ? "TÍTULO" : Titulo);
            txtcodigo.Properties.MaxLength = Codigo_MaxLenght;
        }

        public void Edita(bool enabled)
        {
            txtcodigo.Enabled = enabled;
            btnprocurar.Enabled = enabled;
            txtconteudo.Enabled = enabled;
        }

        private void btnprocurar_Click(object sender, EventArgs e)
        {
            string Formulario = "";

            switch (this.Abrir_Primeiro)
            {
                

                case AbrirPrimeiro.Filtro:
                    //if (string.IsNullOrEmpty(MensagemCodigoVazio))
                    //{
                    //    MessageBox.Show(MensagemCodigoVazio, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    Formulario = Formulario_Filtro;
                    break;
                case AbrirPrimeiro.Visao:
                    Formulario = Formulario_Visao;
                    break;
                default:
                    MessageBox.Show("Erro ao abrir filtro, contate o administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                    break;
            }


            if (!string.IsNullOrEmpty(Formulario))
            {
                try
                {
                    Assembly asm = Assembly.Load(this.Projeto_Formularios);

                    switch (this.Abrir_Primeiro)
                    {
                        case AbrirPrimeiro.Filtro:
                            Type typeFiltro = asm.GetType(Formulario);
                            Form itemFiltro = (Form)Activator.CreateInstance(typeFiltro,this);
                            itemFiltro.Show();
                            break;
                        case AbrirPrimeiro.Visao:
                            //NOVO
                            GeraWhere();
                            Type typeVisao = asm.GetType(Formulario_Visao);
                            Form itemVisao  = (Form)Activator.CreateInstance(typeVisao, this);

                            itemVisao.WindowState = FormWindowState.Normal;
                            itemVisao.StartPosition = FormStartPosition.CenterScreen;

                            itemVisao.ShowDialog();
                            break;
                        default:
                            MessageBox.Show("Erro ao abrir filtro, contate o administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao abrir filtro, contate o administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Erro ao abrir filtro, contate o administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GeraParametros(Newlookup_WhereVisao Campo)
        {
            //try
            //{

            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            string _where = " WHERE ";
            int countReg = 0;

            foreach (Newlookup_Parametros_SelectQuery item in Campo.Where_SelectQuery)
            {
                _where = _where + " " + (countReg > 0 ? " AND " : "") + item.NomeCampo + " ";

                switch (item.OperadorComparacao)
                {
                    case Newlookup_WhereVisao.Operador.Igual:
                        _where = _where + " = ";
                        break;
                    case Newlookup_WhereVisao.Operador.Diferente:
                        _where = _where + " <> ";
                        break;
                    case Newlookup_WhereVisao.Operador.MaiorIgual:
                        _where = _where + " >= ";
                        break;
                    case Newlookup_WhereVisao.Operador.MenorIgual:
                        _where = _where + " <= ";
                        break;
                    case Newlookup_WhereVisao.Operador.Maior:
                        _where = _where + " > ";
                        break;
                    case Newlookup_WhereVisao.Operador.Menor:
                        _where = _where + " < ";
                        break;
                    case Newlookup_WhereVisao.Operador.In:
                        _where = _where + " in ";
                        break;
                    default:
                        MessageBox.Show("Erro ao abrir filtro, contate o administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return "";
                        break;
                }

                switch (item.TipoParametro)
                {
                    case Newlookup_Propriedades.Tipo_do_Parametro.Controle:
                        //switch (item.TipoControle)
                        //{
                        //    case Newlookup_Propriedades.Tipo_do_Controle.Combobox:
                        //        //item.NomeControle
                        //        break;

                        //    case Newlookup_Propriedades.Tipo_do_Controle.TextEdit:

                        //        break;

                        //    case Newlookup_Propriedades.Tipo_do_Controle.GridControl_SelectedRow:

                        //        break;
                        //}
                        string _valor = verificaValorControle(item.NomeFormulario,item.NomeControle,item.NomeCampo);
                        _where = _where + " '" + (_valor =="" ? "": _valor) + "' ";
                        break;
                    case Newlookup_Propriedades.Tipo_do_Parametro.Fixo:
                        _where = _where + " '" + item.ValorFixo + "' ";
                        break;

                }
                countReg = countReg + 1;
            }

            foreach (Newlookup_OutrasChaves outrositens in Campo.OutrosFiltros_SelectQuery)
            {
                _where = _where + " " + (countReg > 0 ? " AND " : "") + outrositens.NomeColunaChave + " ";
                _where = _where + " = ";
                _where = _where + " '" + outrositens.ValorColunaChave + "' ";

                countReg = countReg + 1;
            }

            return _where;
        }

        private string verificaValorControle(string nomeFormulario, string nomeControle, string nomeCampo)
        {
            try
            {
                string valor = "";

                Control ctrl = this.FindForm();
                valor = procuraControleRecursivo(ref ctrl, nomeControle, nomeCampo, true);

                return valor;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private string procuraControleRecursivo(ref Control controle, string nomeControle, string nomeCampo, bool Recursive = true)
        {
            foreach (Control ctrl in controle.Controls)
            {
                if ((object.ReferenceEquals(ctrl.GetType(), typeof(DevExpress.XtraEditors.TextEdit))))
                {
                    DevExpress.XtraEditors.TextEdit campo = (DevExpress.XtraEditors.TextEdit)ctrl;
                    
                    if (campo.Name == nomeControle)
                    {
                        return campo.Text.ToString();
                    }
                    else
                    {
                        continue;
                    }
                }else if ((object.ReferenceEquals(ctrl.GetType(), typeof(ComboBox))))
                {
                    ComboBox campo = (ComboBox)ctrl;
                    if (campo.Name == nomeControle)
                    {
                        if (campo.SelectedIndex == -1)
                        {
                            return "";
                        }
                        else
                        {
                            return campo.SelectedValue.ToString();
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                else if ((object.ReferenceEquals(ctrl.GetType(), typeof(GridControl))))
                {
                    object grid = ctrl;
                    GridControl gridcontrol = (GridControl)grid;

                    GridView view = gridcontrol.MainView as GridView;

                    if (view.Name == nomeControle)
                    {
                        DataRow row1 = view.GetDataRow(Convert.ToInt32(view.GetSelectedRows().GetValue(0).ToString()));
                        return row1[nomeCampo].ToString();
                    }
                    else
                    {
                        continue;
                    }
                }

                if (Recursive)
                {
                    if ((object.ReferenceEquals(ctrl.GetType(), typeof(Panel))))
                    {
                        Control pnl = (Panel)ctrl;
                        string validaPnl = procuraControleRecursivo(ref pnl, nomeControle, nomeCampo, Recursive);
                        if (string.IsNullOrEmpty(validaPnl))
                        {
                            continue;
                        }else
                        {
                            return validaPnl;
                        }
                    }
                    if ((object.ReferenceEquals(ctrl.GetType(), typeof(GroupBox))))
                    {
                        Control grbx = (GroupBox)ctrl;
                        string validaGrbx = procuraControleRecursivo(ref grbx, nomeControle, nomeCampo, Recursive);
                        if (string.IsNullOrEmpty(validaGrbx))
                        {
                            continue;
                        }
                        else
                        {
                            return validaGrbx;
                        }
                    }
                }

            }
            return "";
        }

        void GeraWhere()
        {
            if (NewLookup_Where.Count > 0)
            {
                string _where = " WHERE ";
                int countReg = 0;

                foreach (Newlookup_WhereVisao item in NewLookup_Where)
                {
                    if (item.Variavel_Interna == true)
                    {
                        switch (item.NomeVariavel_Interna)
                        {
                            case Newlookup_WhereVisao.VariavelInterna.CampoCodigo_NewLookup:
                                if (txtcodigo.Text == "")
                                {
                                    continue;
                                }
                                break;
                        }
                    }

                    switch (item.NomeVariavel_Interna)
                    {
                        case  Newlookup_WhereVisao.VariavelInterna.CampoCodigo_NewLookup:

                            switch (CampoCodigo_Igual_a)
                            {
                                case CampoCodigoIguala.CampoCodigoBD:
                                    _where = _where + " " + (countReg > 0 ? " AND " : "") + item.NomeColuna + " ";
                                    break;

                                case CampoCodigoIguala.CampoCodigoInterno:
                                    if (this.CampoCodigoInterno.ToString() != this.CampoCodigoBD.ToString())
                                    {
                                        _where = _where + " " + (countReg > 0 ? " AND " : "") + CampoCodigoInterno + " ";
                                    }
                                    else
                                    {
                                        _where = _where + " " + (countReg > 0 ? " AND " : "") + item.NomeColuna + " ";
                                    }
                                    break;
                            }

                            break;
                        case Newlookup_Propriedades.VariavelInterna.OutrasChaves:
                            if (this.OutrasChaves.Where(estrutura => estrutura.NomeColunaChave == item.NomeColuna).Count() > 0)
                            {
                                _where = _where + " " + (countReg > 0 ? " AND " : "") + item.NomeColuna + " ";
                            }
                            else
                            {
                                continue;
                            }
                            break;

                        default:
                                _where = _where + " " + (countReg > 0 ? " AND " : "") + item.NomeColuna + " ";   
                            break;
                    }

                    switch (item.OperadorComparacao)
                    {
                        case Newlookup_WhereVisao.Operador.Igual:
                            _where = _where + " = ";
                            break;
                        case Newlookup_WhereVisao.Operador.Diferente:
                            _where = _where + " <> ";
                            break;
                        case Newlookup_WhereVisao.Operador.MaiorIgual:
                            _where = _where + " >= ";
                            break;
                        case Newlookup_WhereVisao.Operador.MenorIgual:
                            _where = _where + " <= ";
                            break;
                        case Newlookup_WhereVisao.Operador.Maior:
                            _where = _where + " > ";
                            break;
                        case Newlookup_WhereVisao.Operador.Menor:
                            _where = _where + " < ";
                            break;
                        case Newlookup_WhereVisao.Operador.In:
                            _where = _where + " in ";
                            break;
                        default:
                            MessageBox.Show("Erro ao abrir filtro, contate o administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                            break;
                    }

                    if (item.Variavel_Interna == true)
                    {
                        switch (item.NomeVariavel_Interna)
                        {
                            case Newlookup_WhereVisao.VariavelInterna.AppLib_Context_Empresa:
                                _where = _where + " " + AppLib.Context.Empresa;
                                break;
                            case Newlookup_Propriedades.VariavelInterna.OutrasChaves:

                                if (this.OutrasChaves.Count > 0)
                                {
                                    int indexRevisao = this.OutrasChaves.IndexOf(this.OutrasChaves.Where(estrutura => estrutura.NomeColunaChave == item.NomeColuna).FirstOrDefault());

                                    string valorOutraChave = this.OutrasChaves[0].ValorColunaChave.ToString();

                                    _where = _where + " " + valorOutraChave;
                                }

                                break;
                            case Newlookup_WhereVisao.VariavelInterna.AppLib_Context_Filial:
                                _where = _where + " " + AppLib.Context.Filial;
                                break;
                            case Newlookup_WhereVisao.VariavelInterna.CampoCodigo_NewLookup:
                                _where = _where + " " + (string.IsNullOrEmpty(txtcodigo.Text) ? "''" : "'" + txtcodigo.Text + "'");                                
                                break;
                            case Newlookup_WhereVisao.VariavelInterna.SelectQuery:
                                DataRow[] dtrows;
                                //DataTable dtParametro;
                                if (item.Where_SelectQuery.Count > 0 || item.OutrosFiltros_SelectQuery.Count > 0)
                                {
                                    List<Newlookup_OutrasChaves>  _outrosfiltros = new List<Newlookup_OutrasChaves>();
                                    this.whereParametros = GeraParametros(item);

                                    dtrows = AppLib.Context.poolConnection.Get("Start").ExecQuery(item.ValorFixo + this.whereParametros).DefaultView.ToTable(true, item.NomeColunaValor_SelectQuery).Select(item.NomeColunaValor_SelectQuery + " is not null");

                                    if (dtrows.Count() >= 1)
                                    {
                                        string valoresSelect = "";
                                        int contaRow = 0;

                                        foreach (DataRow row in dtrows)
                                        {
                                            
                                            valoresSelect = valoresSelect + (contaRow == 0 ? "" : ",") + "'" + row[item.NomeColunaValor_SelectQuery].ToString() + "'";
                                            contaRow = contaRow + 1;
                                        }
               
                                        _where = _where + " (" + valoresSelect + ") ";
                                    }
                                    else
                                    {
                                        _where = _where + " ''";
                                    }
                                }
                                else
                                {
                                    dtrows = AppLib.Context.poolConnection.Get("Start").ExecQuery(item.ValorFixo).DefaultView.ToTable(true, item.NomeColunaValor_SelectQuery).Select(item.NomeColunaValor_SelectQuery + " is not null");

                                    if (dtrows.Count() >= 1)
                                    {
                                        string valoresSelect = "";
                                        int contaRow = 0;
                                        foreach (DataRow row in dtrows)
                                        {
                                            valoresSelect = valoresSelect + (contaRow == 0 ? "" : ",") + "'" + row[item.NomeColunaValor_SelectQuery].ToString() + "'";
                                            contaRow = contaRow + 1;
                                        }
                                        _where = _where + " (" + valoresSelect + ") ";
                                    }
                                    else
                                    {
                                        _where = _where + " ''";
                                    }
                                }
                                    break;
                            default:
                                MessageBox.Show("Erro ao abrir filtro, contate o administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                                break;
                        }
                    }
                    else
                    {
                        switch (item.OperadorComparacao)
                        {
                            case Newlookup_WhereVisao.Operador.In:
                                _where = _where + " (" + item.ValorFixo + ") ";
                                break;
                            default:
                                _where = _where + " '" + item.ValorFixo + "' ";
                                break;
                        }
                       
                    }

                    countReg = countReg + 1;
                }
                this.whereVisao = (_where == " WHERE " ? "" : _where);
            }
        }

        public void Clear()
        {
            txtcodigo.Text = "";
            txtconteudo.Text = "";
            ValorCodigoInterno = "";
            mensagemErrorProvider = "";
        }

        public void CarregaDescricao(bool fromDescricao = false)
        {
            if (txtcodigo.Text == "")
            {
                txtconteudo.Text = "";
                this.ValorCodigoInterno = "";
            }
            else
            {
                GeraWhere();
                DataTable dt;
                if (string.IsNullOrEmpty(this.CampoCodigoInterno))
                {
                    if (fromDescricao == true)
                    {
                        dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT " + this.CampoCodigoBD + "," + this.CampoDescricaoBD + " FROM " + this.TabelaBD +  this.whereVisao);
                    }
                    else
                    {
                        dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT " + this.CampoCodigoBD + "," + this.CampoDescricaoBD + " FROM " + this.TabelaBD + (this.CarregaDescricaoSemFiltro == false ? this.whereVisao : " WHERE " + this.CampoCodigoBD + " = '" + this.txtcodigo.Text + "'"));
                    }
                    
                }
                else
                {
                    if (fromDescricao == true)
                    {
                        dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT " + this.CampoCodigoBD + "," + this.CampoDescricaoBD + (this.CampoCodigoInterno.ToString() != this.CampoCodigoBD.ToString() ? (", " + this.CampoCodigoInterno) : "") + " FROM " + this.TabelaBD + this.whereVisao);
                    }
                    else
                    {
                        dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT " + this.CampoCodigoBD + "," + this.CampoDescricaoBD + (this.CampoCodigoInterno.ToString() != this.CampoCodigoBD.ToString() ? (", " + this.CampoCodigoInterno) : "") + " FROM " + this.TabelaBD + (this.CarregaDescricaoSemFiltro == false ? this.whereVisao : " WHERE " + this.CampoCodigoInterno + " = '" + this.txtcodigo.Text + "'"));
                    }
                        
                }

                if (dt.Rows.Count > 0)
                {
                    switch (CampoCodigo_Igual_a)
                    {
                        case CampoCodigoIguala.CampoCodigoBD:
                            txtcodigo.Text = dt.Rows[0][this.CampoCodigoBD].ToString();
                            ValorCodigoInterno = dt.Rows[0][this.CampoCodigoBD].ToString();
                            break;

                        case CampoCodigoIguala.CampoCodigoInterno:
                            if (this.CampoCodigoInterno.ToString() != this.CampoCodigoBD.ToString())
                            {
                                txtcodigo.Text = dt.Rows[0][this.CampoCodigoInterno].ToString();
                                ValorCodigoInterno = dt.Rows[0][this.CampoCodigoBD].ToString();
                            }
                            else
                            {
                                txtcodigo.Text = dt.Rows[0][this.CampoCodigoBD].ToString();
                                ValorCodigoInterno = dt.Rows[0][this.CampoCodigoBD].ToString();
                            }

                            break;
                        default:
                            MessageBox.Show("Erro ao abrir filtro, contate o administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                            break;
                    }

                    txtconteudo.Text = dt.Rows[0][this.CampoDescricaoBD].ToString().ToUpper();
                }
                else
                {
                    txtconteudo.Text = "";
                    //btnprocurar.PerformClick();
                }
            }
        }
        private void txtcodigo_Leave(object sender, EventArgs e)
        {
            CarregaDescricao(true);
        }
    
        private void txtconteudo_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }

        private void txtconteudo_MouseMove(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtconteudo.Text))
            {
                Cursor.Current = Cursors.Hand;
            }
        }

        private void txtconteudo_Click(object sender, EventArgs e)
        {
            Assembly asm = Assembly.Load(this.Projeto_Formularios);

            if (!string.IsNullOrEmpty(txtconteudo.Text))
            {
                if (!string.IsNullOrEmpty(Formulario_Cadastro))
                {
                    Type typeCadastro = asm.GetType(Formulario_Cadastro);
                    Form itemCadastro = (Form)Activator.CreateInstance(typeCadastro, this);
                    itemCadastro.ShowDialog();
                }
            }
        }

        public void txtcodigo_EditValueChanged(object sender, EventArgs e)
        {

        }
    }

    public class NewLookup_GridVisao
    {
        [Category("ItInit_Propriedades"), Description("")] public string Titulo_Coluna { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public string CampoBD { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public bool Coluna_Visivel { get; set; }
    }

    public class Newlookup_Propriedades
    {
        public enum VariavelInterna
        {
            Null,
            AppLib_Context_Empresa,
            AppLib_Context_Filial,
            CampoCodigo_NewLookup,
            SelectQuery,
            OutrasChaves
        }

        public enum Operador
        {
            Igual,
            Diferente,
            MaiorIgual,
            MenorIgual,
            Maior,
            Menor,
            In
        }

        public enum Tipo_do_Parametro
        {
            Fixo,
            Controle
        }


        public enum Tipo_do_Controle
        {
            TextEdit,
            Combobox,
            GridControl_SelectedRow
        }

    }

    public class Newlookup_WhereVisao : Newlookup_Propriedades
    {
       
        Collection<Newlookup_Parametros_SelectQuery> WhereVisao_Parametros = new Collection<Newlookup_Parametros_SelectQuery>();
        Collection<Newlookup_OutrasChaves> OutrasFiltros_Parametros = new Collection<Newlookup_OutrasChaves>();

        [Category("ItInit_Propriedades"), Description("")] public string NomeColuna { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public Operador OperadorComparacao { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public bool Variavel_Interna { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public VariavelInterna NomeVariavel_Interna { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public string ValorFixo { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public string NomeColunaValor_SelectQuery { get; set; }
        [Category("ItInit_Propriedades"), Description(""), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)] public Collection<Newlookup_Parametros_SelectQuery> Where_SelectQuery { get { return WhereVisao_Parametros; } }
        [Category("ItInit_Propriedades"), Description(""), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)] public Collection<Newlookup_OutrasChaves> OutrosFiltros_SelectQuery { get { return OutrasFiltros_Parametros; } }
    }

    public class Newlookup_Parametros_SelectQuery : Newlookup_Propriedades
    {
        [Category("ItInit_Propriedades"), Description("")] public string NomeCampo { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public Tipo_do_Parametro TipoParametro { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public Operador OperadorComparacao { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public string ValorFixo { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public string NomeControle { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public string NomeFormulario { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public Tipo_do_Controle TipoControle { get; set; }
    }

    public class Newlookup_OutrasChaves
    {
        [Category("ItInit_Propriedades"), Description("")] public string NomeColunaChave { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public string ValorColunaChave { get; set; }
    }
}
