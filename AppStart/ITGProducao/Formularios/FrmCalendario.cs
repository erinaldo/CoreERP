using DevExpress.XtraEditors;
using ITGProducao.Controles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITGProducao.Formularios
{
    public partial class FrmCalendario : Form
    {
        public bool edita = false;
        public string codCalendario = string.Empty;
        private string mask;
        private List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;
        private enum TipoHora
        {
            HoraExtra,
            HoraTrabalhada
        }

        public FrmCalendario()
        {
            InitializeComponent();
        }

        public FrmCalendario(ref NewLookup lookup)
        {
            InitializeComponent();

            this.codCalendario = lookup.txtcodigo.Text;
            this.edita = true;

            this.lookup = lookup;
        }

        void CarregaComboMotivo()
        {
            List<PS.Lib.ComboBoxItem> listMotivo = new List<PS.Lib.ComboBoxItem>();

            listMotivo.Add(new PS.Lib.ComboBoxItem());
            listMotivo[0].ValueMember = 1;
            listMotivo[0].DisplayMember = "1 - Folga Semanal";

            listMotivo.Add(new PS.Lib.ComboBoxItem());
            listMotivo[1].ValueMember = 2;
            listMotivo[1].DisplayMember = "2 - Férias";

            listMotivo.Add(new PS.Lib.ComboBoxItem());
            listMotivo[2].ValueMember = 3;
            listMotivo[2].DisplayMember = "3 - Feriado";

            listMotivo.Add(new PS.Lib.ComboBoxItem());
            listMotivo[3].ValueMember = 4;
            listMotivo[3].DisplayMember = "4 - Parada Técnica";

            cboMotivo.DataSource = listMotivo;
            cboMotivo.DisplayMember = "DisplayMember";
            cboMotivo.ValueMember = "ValueMember";
        }

        private void FrmCalendario_Load(object sender, EventArgs e)
        {
            CarregaComboMotivo();

            if (edita == true)
            {
                carregaCampos();
            }else
            {
                LimpaGrid_Historico();
            }

            txtCodigo.Focus();
        }

        private void btnInserirData_Click(object sender, EventArgs e)
        {
            btnInserirData.Text = "Aguarde...";
            if (Salvar_Datas() == true)
            {
                MessageBox.Show("Processo Concluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            btnInserirData.Text = "Incluir";
            Cursor.Current = Cursors.Default;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private bool validacao_datas()
        {
            bool verifica = true;

            errorProvider.Clear();

            if (string.IsNullOrEmpty(dteDe.Text))
            {
                dteDe.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                errorProvider.SetError(dteDe, "Favor preencher a data do Campo 'De'");
                verifica = false;
            }

            if (string.IsNullOrEmpty(dteAte.Text))
            {
                dteAte.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                errorProvider.SetError(dteAte, "Favor preencher a data do Campo 'Até'");
                verifica = false;
            }

            if (!string.IsNullOrEmpty(dteDe.Text) && !string.IsNullOrEmpty(dteAte.Text))
            {
                if (Convert.ToDateTime(dteAte.Text) < Convert.ToDateTime(dteDe.Text))
                {
                    dteAte.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    errorProvider.SetError(dteAte, "A data do Campo 'Até' não pode ser menor que a  data do Campo 'De'");
                    verifica = false;
                }
            }

            if (!radparada.Checked == true)
            {
                if (timTempo.Text == "00:00")
                {
                    timTempo.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                    errorProvider.SetError(timTempo, "Favor preencher o tempo");
                    verifica = false;
                }
            }

            int diasemana = 0;

            if (chkseg.Checked == true) { diasemana++; }
            if (chkter.Checked == true) { diasemana++; }
            if (chkqua.Checked == true) { diasemana++; }
            if (chkqui.Checked == true) { diasemana++; }
            if (chksex.Checked == true) { diasemana++; }
            if (chksab.Checked == true) { diasemana++; }
            if (chkdom.Checked == true) { diasemana++; }

            if (diasemana == 0)
            {
                if(MessageBox.Show("Nenhum dia da semana selecionado, Deseja selecionar todos os dias do período?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    chkseg.Checked = true;
                    chkter.Checked = true;
                    chkqua.Checked = true;
                    chkqui.Checked = true;
                    chksex.Checked = true;
                    chksab.Checked = true;
                    chkdom.Checked = true;
                }
                else
                {
                    MessageBox.Show("Processo Cancelado, Selecione o dia para geração.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    verifica = false;
                }
            }

            return verifica;
        }
        private bool validacao()
        {
            bool verifica = true;

            errorProvider.Clear();

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                txtCodigo.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                errorProvider.SetError(txtCodigo, "Favor preencher o código do calendário");
                verifica = false;
            }

            if (string.IsNullOrEmpty(txtDescricao.Text)) {
                txtDescricao.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                errorProvider.SetError(txtDescricao, "Favor preencher a descrição");
                verifica = false;
            }

            if (string.IsNullOrEmpty(txtAnoCalendario.Text)) {
                txtAnoCalendario.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                errorProvider.SetError(txtAnoCalendario, "Favor preencher o ano de refêrência do calendário");
                verifica = false;
            }
            //else
            //{
            //    if (Existe_Calendario(txtAnoCalendario.Text.ToString()) == true)
            //    {

            //        txtAnoCalendario.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
            //        dxError.SetError(txtAnoCalendario, "Já existe um calendário para o ano de " + txtAnoCalendario.Text.ToString() + " cadastrado.");
            //        verifica = false;
            //    }
            //}

            //if(chkAtivo.Checked == true)
            //{
            //    if (Existe_Calendario() == false)
            //    {
            //        chkAtivo.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
            //        dxError.SetError(chkAtivo, "O Calendário só pode ser ativo ao preencher todas as datas do ano de referência");
            //        verifica = false;
            //    }else
            //    {

            //        DateTime datainicio = new DateTime(Convert.ToInt16(txtAnoCalendario.Text), 1, 1);
            //        DateTime datafim = new DateTime(Convert.ToInt16(txtAnoCalendario.Text), 12, 31);

            //        TimeSpan dif_datas = datafim - datainicio;
            //        int dias = dif_datas.Days + 1;

            //        DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT DATA FROM PCALENDARIOTRABALHO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCALEND = ? order by DATA", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codCalendario });
            //        if (dt.Rows.Count != dias)
            //        {
            //            chkAtivo.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
            //            dxError.SetError(chkAtivo, "O Calendário só pode ser ativo ao preencher todas as datas do ano de referência");
            //            verifica = false;
            //        }
            //    }
            //}

            return verifica;
        }

        private bool VerificaDiaSemanaCheckado(int dia)
        {
            bool _valida = false;

            switch (dia)
            {
                case 1: //Domingo
                    _valida = (chkdom.Checked == true ? true : false);
                    break;

                case 2: //Segunda-Feira
                    _valida = (chkseg.Checked == true ? true : false);
                    break;

                case 3: //Terça-Feira
                    _valida = (chkter.Checked == true ? true : false);
                    break;

                case 4: //Quarta-Feira
                    _valida = (chkqua.Checked == true ? true : false);
                    break;

                case 5: //Quinta-Feira
                    _valida = (chkqui.Checked == true ? true : false);
                    break;

                case 6: //Sexta-Feira
                    _valida = (chksex.Checked == true ? true : false);
                    break;

                case 7: //Sábado
                    _valida = (chksab.Checked == true ? true : false);
                    break;

                default:
                    _valida = false;
                    break;
            }

            return _valida;
        }

        private void carregaObj(DataTable dt)
        {
            txtCodigo.Text = dt.Rows[0]["CODCALEND"].ToString();
            txtDescricao.Text = dt.Rows[0]["DESCCALEND"].ToString();
            txtAnoCalendario.Text = dt.Rows[0]["ANOCALEND"].ToString();

            dteDe.Properties.MinValue = new DateTime(Convert.ToInt16(txtAnoCalendario.Text), 1, 1);
            dteDe.Properties.MaxValue = new DateTime(Convert.ToInt16(txtAnoCalendario.Text), 12, 31);

            dteAte.Properties.MinValue = new DateTime(Convert.ToInt16(txtAnoCalendario.Text), 1, 1);
            dteAte.Properties.MaxValue = new DateTime(Convert.ToInt16(txtAnoCalendario.Text), 12, 31);

            chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);

            txtCodigo.Enabled = false;
            txtAnoCalendario.Enabled = false;
            btnInserirData.Enabled = true;

            string where = "WHERE PCALENDARIOTRABALHO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND PCALENDARIOTRABALHO.CODFILIAL = '" + AppLib.Context.Filial + "' AND PCALENDARIOTRABALHO.CODCALEND = '" + dt.Rows[0]["CODCALEND"].ToString() + "'";
            carregaHistorico(where);
        }
        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCALENDARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCALEND = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codCalendario });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                switch (lookup.CampoCodigo_Igual_a)
                {
                    case NewLookup.CampoCodigoIguala.CampoCodigoBD:
                        dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCALENDARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCALEND = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codCalendario });
                        if (dt.Rows.Count > 0)
                        {
                            carregaObj(dt);
                        }
                        break;

                    case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                        if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCALENDARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND " + lookup.CampoCodigoInterno + " = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookup.ValorCodigoInterno });
                            if (dt.Rows.Count > 0)
                            {
                                carregaObj(dt);
                            }
                        }
                        else
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCALENDARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCALEND = ?  ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codCalendario });
                            if (dt.Rows.Count > 0)
                            {
                                carregaObj(dt);
                            }
                        }
                        break;
                }
            }
        }

        void LimpaGrid_Historico()
        {
            string tabela = "PCALENDARIOTRABALHO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = new PS.Glb.Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, "").Replace("SELECT", "SELECT TOP 0 ");

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridHistorico.DataSource = null;
                gridView1.Columns.Clear();
                gridHistorico.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridView1.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridView1.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void carregaHistorico(string where)
        {
            string tabela = "PCALENDARIOTRABALHO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();

            try
            {
                string sql = new PS.Glb.Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;

                }

                gridHistorico.DataSource = null;
                gridView1.Columns.Clear();
                gridHistorico.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridView1.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridView1.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Salvar_Datas()
        {
            bool _salvar = false;
            Cursor.Current = Cursors.WaitCursor;
            bool _pergunta_datajaexistente = false;

            if (validacao_datas() == true)
            {
                if (Existe_Calendario() == true)
                {
                    CultureInfo culture = new CultureInfo("pt-BR");
                    DateTimeFormatInfo dtinfo = culture.DateTimeFormat;

                    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                    AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PCALENDARIOTRABALHO");
                    conn.BeginTransaction();

                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT DATA FROM PCALENDARIOTRABALHO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCALEND = ? order by DATA", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text });

                    try
                    {                       
                        TimeSpan dif_datas = Convert.ToDateTime(dteAte.EditValue.ToString()) - Convert.ToDateTime(dteDe.EditValue.ToString());
                        int dias = dif_datas.Days;

                        for (int i = 0; i <= dias; i++) {

                            DateTime data = Convert.ToDateTime(dteDe.Text).AddDays(i);
                            string diasemana = culture.TextInfo.ToTitleCase(dtinfo.GetDayName(data.DayOfWeek));

                            if (VerificaDiaSemanaCheckado((int)data.DayOfWeek + 1) == true)
                            {
                                DataRow[] rows = dt.Select("DATA='" + data.ToString("yyyy-MM-dd")+"'");

                                if (_pergunta_datajaexistente == false) 
                                {
                                    if (rows.Count() > 0)
                                    {
                                        if (MessageBox.Show("Existem datas ja informadas, deseja alterá-las?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            _pergunta_datajaexistente = true;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Processo Cancelado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return false;
                                        }
                                    }
                                }

                                v.Set("CODEMPRESA", AppLib.Context.Empresa);
                                v.Set("CODFILIAL", AppLib.Context.Filial);
                                v.Set("CODCALEND", txtCodigo.Text);

                                v.Set("DATA", data);
                                v.Set("DIADASEMANA", (int)data.DayOfWeek + 1);

                                int hora_em_minutos = (Convert.ToInt16(Convert.ToDateTime(timTempo.Text).Hour * 60)) + Convert.ToInt16(Convert.ToDateTime(timTempo.Text).Minute);

                                if (radtrabalho.Checked == true) {
                                    v.Set("MOTIVOPARADAS", null);
                                    if (ValidaTotalHorasDia(data, TipoHora.HoraTrabalhada, hora_em_minutos) == true)
                                    {
                                        v.Set("HORASDISPONIVEIS", hora_em_minutos);
                                        v.Set("DIAPRODUTIVO", 1);
                                        v.Set("HORASDISPONIVEIS", hora_em_minutos);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Processamento Cancelado, a data " + Convert.ToDateTime(data, System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy") + " não pode somar mais de 24 horas entre horas trabalhadas e horas extras.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        conn.Rollback();
                                        return false;
                                    }
                                }
                                else if (radextra.Checked == true) {

                                    v.Set("MOTIVOPARADAS", null);
                                    if (ValidaTotalHorasDia(data, TipoHora.HoraExtra, hora_em_minutos) == true)
                                    {
                                        v.Set("DIAPRODUTIVO", 1);
                                        v.Set("HORASEXTRAS", hora_em_minutos);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Processamento Cancelado, a data " + Convert.ToDateTime(data, System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy") + " não pode somar mais de 24 horas entre horas trabalhadas e horas extras.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        conn.Rollback();
                                        return false;
                                    }
                                }

                                if (radparada.Checked == true)
                                {
                                    v.Set("DIAPRODUTIVO", 0);
                                    v.Set("MOTIVOPARADAS", cboMotivo.SelectedValue);
                                    v.Set("HORASEXTRAS", 0);
                                    v.Set("HORASDISPONIVEIS", 0);
                                }

                                v.Save();
                            }
                        }

                        conn.Commit();

                        string where = "WHERE PCALENDARIOTRABALHO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND PCALENDARIOTRABALHO.CODFILIAL = '" + AppLib.Context.Filial + "' AND PCALENDARIOTRABALHO.CODCALEND = '" + txtCodigo.Text.ToString() + "'";
                        carregaHistorico(where);

                        _salvar = true;
                    }
                    catch (Exception ex)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conn.Rollback();
                        _salvar = false;
                    }
                }

            }
            else
            {
                _salvar = false;
            }

            Cursor.Current = Cursors.Default;

            return _salvar;
        }

        private bool Valida_DiaSemana(string diasemana)
        {
            bool _diasemana = false;
            switch (diasemana) {
                case "Segunda-Feira":
                    _diasemana = true;
                    break;

                case "Terça-Feira":
                    _diasemana = true;
                    break;

                case "Quarta-Feira":
                    _diasemana = true;
                    break;

                case "Quinta-Feira":
                    _diasemana = true;
                    break;

                case "Sexta-Feira":
                    _diasemana = true;
                    break;

                case "Sábado":
                    _diasemana = false;
                    break;

                case "Domingo":
                    _diasemana = false;
                    break;
            }
            return _diasemana;
        }
        private bool Existe_Calendario(string validaano = "")
        {
            DataTable dt;

            if (!string.IsNullOrEmpty(validaano))
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCALENDARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND ANOCALEND = ? AND CODCALEND <> ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, validaano.ToString(), txtCodigo.Text });
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCALENDARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCALEND = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text });
            }
            
            if (dt.Rows.Count > 0)
            {
                return true;
            }else
            {
                return false;
            }
        }

        private bool ValidaTotalHorasDia(DateTime data, TipoHora tipo, int minutos)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCALENDARIOTRABALHO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCALEND = ? AND DATA = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text, data});
            if (dt.Rows.Count > 0)
            {
                int horaextra = (!Convert.IsDBNull(dt.Rows[0]["HORASEXTRAS"]) ? Convert.ToInt16(dt.Rows[0]["HORASEXTRAS"].ToString()) : 0);
                int horatrabalhada = (!Convert.IsDBNull(dt.Rows[0]["HORASDISPONIVEIS"]) ? Convert.ToInt16(dt.Rows[0]["HORASDISPONIVEIS"].ToString()) : 0);

                if (tipo == TipoHora.HoraExtra)
                {
                    if ((horatrabalhada + minutos) > 1440)
                    {
                        return false;
                    }else
                    {
                        return true;
                    }
                }
                else if(tipo == TipoHora.HoraTrabalhada)
                {
                    if ((horaextra + minutos) > 1440)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }  
            }
            else
            {
                return true;
            }
        }
        private bool Salvar()
        {
            bool _salvar = false;

            if (validacao() == true)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PCALENDARIO");
                conn.BeginTransaction();
                try
                {
                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("CODFILIAL", AppLib.Context.Filial);
                    v.Set("CODCALEND", txtCodigo.Text);
                    v.Set("DESCCALEND", txtDescricao.Text);
                    //v.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                    v.Set("ANOCALEND", txtAnoCalendario.Text);

                    if (Existe_Calendario() == true)
                    {
                        v.Set("DATAMODCALEND", conn.GetDateTime());

                        v.Set("ATIVO", chkAtivo.Checked == true ? 1 : 0);
                    }
                    else
                    {
                        v.Set("DATACRIACALEND", conn.GetDateTime());
                        v.Set("ATIVO", 0);
                        v.Set("DATAMODCALEND", null);
                    }

                    v.Save();
                    conn.Commit();

                    carregaCampos();
                    this.edita = true;

                    _salvar = true;
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Rollback();
                    _salvar = false;
                }
            }
            else
            {
                _salvar = false;
            }

            return _salvar;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                if (this.lookup == null)
                {
                    MessageBox.Show("Cadastro salvo com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    codCalendario = txtCodigo.Text;
                    carregaCampos();
                }
                else
                {
                    switch (lookup.CampoCodigo_Igual_a)
                    {
                        case NewLookup.CampoCodigoIguala.CampoCodigoBD:
                            codCalendario = txtCodigo.Text;
                            carregaCampos();

                            lookup.txtcodigo.Text = txtCodigo.Text;
                            lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                            lookup.ValorCodigoInterno = txtCodigo.Text;

                            this.Dispose();
                            break;
                        case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                            if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                            {
                                codCalendario = txtCodigo.Text;
                                carregaCampos();

                                lookup.txtcodigo.Text = txtCodigo.Text;
                                lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                                lookup.ValorCodigoInterno = txtCodigo.Text;

                                this.Dispose();
                            }
                            else
                            {
                                codCalendario = txtCodigo.Text;
                                carregaCampos();

                                lookup.txtcodigo.Text = txtCodigo.Text;
                                lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();
                                lookup.ValorCodigoInterno = txtCodigo.Text;

                                this.Dispose();
                            }
                            break;
                    }

                }
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (Salvar() == true)
                {

                    this.Dispose();
                }
            }
            else
            {
                btnSalvar.PerformClick();
            }
        }


        private void btnNovo_Click(object sender, EventArgs e)
        {

            errorProvider.Clear();

            txtCodigo.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtAnoCalendario.Text = string.Empty;
            chkAtivo.Checked = false;

            radtrabalho.Checked = true;

            dteDe.Text = string.Empty;
            dteAte.Text = string.Empty;
            timTempo.Text = string.Empty;

            chkseg.Checked = false;
            chkter.Checked = false;
            chkqua.Checked = false;
            chkqui.Checked = false;
            chksex.Checked = false;
            chksab.Checked = false;
            chkdom.Checked = false;

            btnInserirData.Enabled = false;
            txtCodigo.Enabled = true;
            txtAnoCalendario.Enabled = true;

            txtCodigo.Focus();

            LimpaGrid_Historico();
        }


        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM PCENTROTRABALHOCALENDARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCALEND = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codCalendario });
                if (dtFixo.Rows.Count > 0)
                {
                    MessageBox.Show("Este Calendário esta vinculado a um Centro de Trabalho e não pode ser excluído.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                        conn.BeginTransaction();
                        try
                        {
                            conn.ExecTransaction("DELETE FROM PCALENDARIOTRABALHO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCALEND = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codCalendario });
                            conn.ExecTransaction("DELETE FROM PCALENDARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCALEND = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codCalendario });
                            conn.Commit();
                            MessageBox.Show("Cadastro Excluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Dispose();
                        }
                        catch (Exception)
                        {
                            conn.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao Excluir Calendário, contate o Administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAnoCalendario_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        void ValidaMotivoParada(string sender)
        {
            switch (sender.ToString())
            {
                case "radtrabalho":
                    timTempo.Visible = true;
                    lbltempo.Text = "Tempo:";
                    cboMotivo.Visible = false;

                    break;

                case "radparada":
                    timTempo.Visible = false;
                    lbltempo.Text = "Motivo:";
                    cboMotivo.Visible = true;
                    break;

                case "radextra":
                    timTempo.Visible = true;
                    lbltempo.Text = "Tempo:";
                    cboMotivo.Visible = false;

                    break;
            }
        }

        private void radextra_CheckedChanged(object sender, EventArgs e)
        {
            ValidaMotivoParada("radextra");
        }

        private void radparada_CheckedChanged(object sender, EventArgs e)
        {
            ValidaMotivoParada("radparada");
        }

        private void radtrabalho_CheckedChanged(object sender, EventArgs e)
        {
            ValidaMotivoParada("radtrabalho");
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            try
            {
                //Dia da Semana
                switch (e.Column.FieldName.ToString())
                {
                    case "PCALENDARIOTRABALHO.DIADASEMANA":
                        switch (Convert.ToInt16(e.Value))
                        {
                            case 1:
                                e.DisplayText = "Domingo";
                                break;

                            case 2:
                                e.DisplayText = "Segunda-Feira";
                                break;

                            case 3:
                                e.DisplayText = "Terça-Feira";
                                break;

                            case 4:
                                e.DisplayText = "Quarta-Feira";
                                break;

                            case 5:
                                e.DisplayText = "Quinta-Feira";
                                break;

                            case 6:
                                e.DisplayText = "Sexta-Feira";
                                break;

                            case 7:
                                e.DisplayText = "Sábado";
                                break;
                        }
                        break;

                    //Hora Trabalhada
                    case "PCALENDARIOTRABALHO.HORASDISPONIVEIS" :
                        if (Convert.ToInt16(e.Value.ToString()) > 0)
                        {
                            int horas = (Convert.ToInt16(e.Value.ToString()) / 60);
                            int minutos = (Convert.ToInt16(e.Value.ToString()) % 60);
                            e.DisplayText = horas.ToString("00") + ":" + minutos.ToString("00");
                        }else
                        {
                            e.DisplayText = "00:00";
                        }
                        break;

                    //Hora Extra
                    case "PCALENDARIOTRABALHO.HORASEXTRAS":
                        if (Convert.ToInt16(e.Value.ToString()) > 0)
                        {
                            int horas = (Convert.ToInt16(e.Value.ToString()) / 60);
                            int minutos = (Convert.ToInt16(e.Value.ToString()) % 60);
                            e.DisplayText = horas.ToString("00") + ":" + minutos.ToString("00");
                        }
                        else
                        {
                            e.DisplayText = "00:00";
                        }
                        break;

                    //Motivo Parada
                    case "PCALENDARIOTRABALHO.MOTIVOPARADAS":

                        int valor = (!Convert.IsDBNull(e.Value) ? Convert.ToInt16(e.Value.ToString()) : 0); ;

                        switch (valor)
                        {
                            case 1:
                                e.DisplayText = "Folga Semanal";
                                break;

                            case 2:
                                e.DisplayText = "Férias";
                                break;

                            case 3:
                                e.DisplayText = "Feriado";
                                break;

                            case 4:
                                e.DisplayText = "Parada Técnica";
                                break;

                            default:
                                e.DisplayText = "";
                                break;
                        }
                        break;
                    }
                }
            catch (Exception)
            {

            }
        }

        private void txtAnoCalendario_Validating(object sender, CancelEventArgs e)
        {
            TextEdit campo = sender as TextEdit;

            if (campo.IsModified) { 
                if (string.IsNullOrEmpty(txtAnoCalendario.Text))
                {
                    dteDe.Properties.MinValue = new DateTime(DateTime.Now.Year, 1, 1);
                    dteDe.Properties.MaxValue = new DateTime(DateTime.Now.Year, 12, 31);

                    dteAte.Properties.MinValue = new DateTime(DateTime.Now.Year, 1, 1);
                    dteAte.Properties.MaxValue = new DateTime(DateTime.Now.Year, 12, 31);
                }
                else if ((txtAnoCalendario.Text.Replace("_", "").Length >= 4))
                {
                    dteDe.Properties.MinValue = new DateTime(DateTime.Now.Year, 1, 1);
                    dteDe.Properties.MaxValue = new DateTime(DateTime.Now.Year, 12, 31);

                    dteAte.Properties.MinValue = new DateTime(DateTime.Now.Year, 1, 1);
                    dteAte.Properties.MaxValue = new DateTime(DateTime.Now.Year, 12, 31);
                } else
                {
                    dteDe.Properties.MinValue = new DateTime(Convert.ToInt16(txtAnoCalendario.Text), 1, 1);
                    dteDe.Properties.MaxValue = new DateTime(Convert.ToInt16(txtAnoCalendario.Text), 12, 31);

                    dteAte.Properties.MinValue = new DateTime(Convert.ToInt16(txtAnoCalendario.Text), 1, 1);
                    dteAte.Properties.MaxValue = new DateTime(Convert.ToInt16(txtAnoCalendario.Text), 12, 31);                    
                }
            }
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
