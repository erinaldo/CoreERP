using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PS.Lib;
using PS.Lib.WinForms;

namespace PS.Lib.WinForms
{
    public partial class FrmBaseFiltroManutencao : DevExpress.XtraEditors.XtraForm
    {
        // OBJETOS
        Global gb = new Global();
        Constantes ct = new Constantes();

        Filter FiltroSelecionado;
        FrmBaseFiltro FrmBaseFiltro1;
        List<FilterCondition> ListaDeCondicao;
        List<DataField> ListaDeCampos;
        string tabela = "";

        public FrmBaseFiltroManutencao(Filter objFiltro, FrmBaseFiltro objFrmBaseFiltro)
        {
            InitializeComponent();

            FiltroSelecionado = objFiltro;
            FrmBaseFiltro1 = objFrmBaseFiltro;
            tabela = FrmBaseFiltro1._tabela;

            ListaDeCampos = FrmBaseFiltro1.FrmBaseVisao1._psPart.GetTableColumn();
        }

        private void FrmBaseFiltroManutencao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1 != null)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Selected)
                    {
                        ListaDeCondicao.RemoveAt(i);
                    }
                }
            }

            CarregaGrid();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ListaDeCondicao.Clear();
            CarregaGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string operadorlogico = comboBox1.SelectedValue.ToString();
                string operador = "";

                operador = comboBox2.Text;

                if (ListaDeCondicao.Count == 0)
                    operador = "";

                FilterCondition Condicao = new FilterCondition();
                Condicao.codEmpresa = FiltroSelecionado.codEmpresa;
                Condicao.idFiltro = FiltroSelecionado.id;

                int idcondicao = Condicao.BuscaProximoId();

                if (idcondicao == 0)
                    idcondicao = ListaDeCondicao.Count + 1;
                else
                    idcondicao = idcondicao + 1;

                Condicao.id = idcondicao;

                Condicao.ordem = ListaDeCondicao.Count + 1;
                Condicao.operador = operadorlogico;
                Condicao.valor = RetornaValorParametro();
                Condicao.operadorlogicoText = operador;
                Condicao.campoText = comboBox3.Text;
                Condicao.campoValue = comboBox3.SelectedValue.ToString();
                Condicao.DefineConficao(ListaDeCampos);

                ListaDeCondicao.Add(Condicao);
                //LimpaFormulario();
                PreparaFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (FiltroSelecionado.listaCondicao != null)
            {
                FiltroSelecionado.listaCondicao.Clear();
                FiltroSelecionado.listaCondicao = ListaDeCondicao;

                try
                {
                    FiltroSelecionado.Salvar();
                    FiltroSelecionado.BuscaCondicao();
                    this.Close();
                }
                catch (Exception ex)
                {
                    PSMessageBox.ShowError(ex.Message);
                    button5.Focus();
                    return;
                }
            }
            else
            {
                FiltroSelecionado.listaCondicao = ListaDeCondicao;

                FrmBaseFiltroRenomear f = new FrmBaseFiltroRenomear(FiltroSelecionado, FrmBaseFiltro1, false);
                f.ShowDialog();

                FrmBaseFiltro1.ListaDeFiltros.Add(FiltroSelecionado);

                this.Close();
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            txtValor.Visible = true;

            if (comboBox1.Text == "IS NULL")
                txtValor.Visible = false;

            if (comboBox1.Text == "IS NOT NULL")
                txtValor.Visible = false;
        }

        private void FrmBaseFiltroManutencao_KeyDown(object sender, KeyEventArgs e)
        {
            Keys flag = e.KeyCode;

            if (flag.Equals(Keys.Enter))
            {
                SelectNextControl(ActiveControl, !e.Shift, true, true, true);
                e.Handled = true;
            }
        }

        private void FrmBaseFiltroManutencao_Load(object sender, EventArgs e)
        {
            CarregaComboOperador();
            CarregaCamposDisponíveis();
            CarregaDados();
            LimpaFormulario();
            PreparaFormulario();
        }

        private void CarregaComboOperador()
        {
            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = "=";
            list1[0].DisplayMember = "=";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = "<>";
            list1[1].DisplayMember = "<>";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = ">";
            list1[2].DisplayMember = ">";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[3].ValueMember = ">=";
            list1[3].DisplayMember = ">=";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[4].ValueMember = "<";
            list1[4].DisplayMember = "<";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[5].ValueMember = "<=";
            list1[5].DisplayMember = "<=";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[6].ValueMember = "IS NULL";
            list1[6].DisplayMember = "Nulo";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[7].ValueMember = "IS NOT NULL";
            list1[7].DisplayMember = "Não Nulo";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[8].ValueMember = "LIKE";
            list1[8].DisplayMember = "Contem";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[9].ValueMember = "NOT LIKE";
            list1[9].DisplayMember = "Não Contem";

            comboBox1.DataSource = list1;
            comboBox1.DisplayMember = "DisplayMember";
            comboBox1.ValueMember = "ValueMember";        
        }

        private void RemoveColunaFiltroPadrao(DataTable dt)
        {
            for (int i = 0; i < FrmBaseFiltro1.FrmBaseVisao1._psPart.DefaultFilter.Count; i++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (FrmBaseFiltro1.FrmBaseVisao1._psPart.DefaultFilter[i].Field == dt.Rows[j]["COLUNA"].ToString())
                    {
                        dt.Rows.RemoveAt(j);
                    }
                }
            }
        }

        private void CarregaCamposDisponíveis()
        {
            DataTable dt = new DataTable();
            dt = gb.NomeDosCamposFiltro(tabela);

            RemoveColunaFiltroPadrao(dt);

            comboBox3.DataSource = dt;

            comboBox3.DisplayMember = "DESCRICAO";
            comboBox3.ValueMember = "COLUNA";
        }

        private void PreparaFormulario()
        {
            if (FiltroSelecionado != null)
            {
                CarregaGrid();
            }
            else
            {
                FiltroSelecionado = new Filter();
                FiltroSelecionado.codEmpresa = Contexto.Session.Empresa.CodEmpresa;
                FiltroSelecionado.id = FiltroSelecionado.BuscaProximoId();
                FiltroSelecionado.codUsuario = Contexto.Session.CodUsuario;
                FiltroSelecionado.tabela = tabela;
                FiltroSelecionado.selecionado = 0;
            }        
        }

        private void LimpaFormulario()
        {
            comboBox3.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            comboBox3.Focus();
        }

        private void CarregaDados()
        {
            ListaDeCondicao = new List<FilterCondition>();

            if (FiltroSelecionado == null)
                return;

            for (int i = 0; i < FiltroSelecionado.listaCondicao.Count; i++)
            {
                FilterCondition Condicao = new FilterCondition();
                Condicao.codEmpresa = FiltroSelecionado.listaCondicao[i].codEmpresa;
                Condicao.idFiltro = FiltroSelecionado.listaCondicao[i].idFiltro;
                Condicao.id = FiltroSelecionado.listaCondicao[i].id;
                Condicao.operador = FiltroSelecionado.listaCondicao[i].operador;
                Condicao.valor = FiltroSelecionado.listaCondicao[i].valor;
                Condicao.operadorlogicoText = FiltroSelecionado.listaCondicao[i].operadorlogicoText;
                Condicao.operadorlogicoValue = FiltroSelecionado.listaCondicao[i].operadorlogicoValue;
                Condicao.operador2 = FiltroSelecionado.listaCondicao[i].operador2;
                Condicao.valor2 = FiltroSelecionado.listaCondicao[i].valor2;
                Condicao.campoValue = FiltroSelecionado.listaCondicao[i].campoValue;
                Condicao.campoText = FiltroSelecionado.listaCondicao[i].campoText;
                Condicao.condicaoText = FiltroSelecionado.listaCondicao[i].condicaoText;
                Condicao.condicaoValue = FiltroSelecionado.listaCondicao[i].condicaoValue;

                ListaDeCondicao.Add(Condicao);
            }
        }

        private void CarregaGrid()
        {
            // Baseado na lista de filtros verifica as condições de cada filtro para popular o DataGrid
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ListaDeCondicao;

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if ((dataGridView1.Columns[i].HeaderText == "condicaoText") || (dataGridView1.Columns[i].HeaderText == "operadorlogicoText"))
                {
                    dataGridView1.Columns[i].Visible = true;
                }
                else
                {
                    dataGridView1.Columns[i].Visible = false;
                }
            }

            // Modifica o tamanho das colunas
            dataGridView1.Columns[6].Width = 40;
            dataGridView1.Columns[12].Width = 416;
        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            AdicionaControles(comboBox3.SelectedValue.ToString());
        }

        private void AdicionaControles(string campo)
        {
            DataField dfCAMPO = gb.RetornaDataFieldByCampo(ListaDeCampos, campo);

            int eixoX = 9;
            int eixoY = 73;
            int width = 275;
            int height = 37;

            string texto = "Valor";
            string name = "txtValor";

            Control[] ctrs = this.Controls.Find("txtValor", true);
            for (int i = 0; i < ctrs.Length; i++)
            {
                if (ctrs[i].Name == "txtValor")
                {
                    this.groupBox1.Controls.Remove(ctrs[i]);
                }
            }

            if (dfCAMPO.Tipo == typeof(PS.Lib.WinForms.PSTextoBox) || dfCAMPO.Tipo == typeof(PS.Lib.WinForms.PSLookup) || dfCAMPO.Tipo == typeof(PS.Lib.WinForms.PSMemoBox))
            {
                PS.Lib.WinForms.PSTextoBox t = new PS.Lib.WinForms.PSTextoBox();

                t.DataField = texto;
                t.Name = name;

                t.Caption = texto;
                t.Edita = true;

                t.Location = new System.Drawing.Point(eixoX, eixoY);
                t.Size = new System.Drawing.Size(width, height);

                this.groupBox1.Controls.Add(t);
            }

            if (dfCAMPO.Tipo == typeof(PS.Lib.WinForms.PSDateBox))
            {
                PS.Lib.WinForms.PSTextoBox t = new PS.Lib.WinForms.PSTextoBox();

                t.DataField = texto;
                t.Name = name;

                t.Caption = texto;
                t.Edita = true;

                t.Location = new System.Drawing.Point(eixoX, eixoY);
                t.Size = new System.Drawing.Size(width, height);

                this.groupBox1.Controls.Add(t);
            }

            if (dfCAMPO.Tipo == typeof(PS.Lib.WinForms.PSComboBox))
            {
                PS.Lib.WinForms.PSComboBox t = new PS.Lib.WinForms.PSComboBox();

                t.DataField = texto;
                t.Name = name;

                t.Caption = texto;
                t.Chave = true;

                t.DataSource = dfCAMPO.Item;
                t.DisplayMember = "DisplayMember";
                t.ValueMember = "ValueMember";

                t.Location = new System.Drawing.Point(eixoX, eixoY);
                t.Size = new System.Drawing.Size(width, height);

                this.groupBox1.Controls.Add(t);
            }

            if (dfCAMPO.Tipo == typeof(PS.Lib.WinForms.PSMoedaBox))
            {
                PS.Lib.WinForms.PSMoedaBox t = new PS.Lib.WinForms.PSMoedaBox();

                t.DataField = texto;
                t.Name = name;

                t.CasasDecimais = 4;
                t.Caption = texto;
                t.Edita = true;

                t.Location = new System.Drawing.Point(eixoX, eixoY);
                t.Size = new System.Drawing.Size(width, height);

                this.groupBox1.Controls.Add(t);
            }

            if (dfCAMPO.Tipo == typeof(PS.Lib.WinForms.PSCheckBox))
            {
                PS.Lib.WinForms.PSCheckBox t = new PS.Lib.WinForms.PSCheckBox();

                t.DataField = texto;
                t.Name = name;
                t.Chave = true;

                t.Location = new System.Drawing.Point(eixoX, eixoY);
                t.Size = new System.Drawing.Size(width, height);

                this.groupBox1.Controls.Add(t);            
            }
        }

        private object RetornaValorParametro()
        {
            object obj = null;

            for (int i = 0; i < this.groupBox1.Controls.Count; i++)
            {
                if (this.groupBox1.Controls[i].GetType() == typeof(PSTextoBox))
                {
                    PSTextoBox p = new PSTextoBox();
                    p = (PSTextoBox)this.groupBox1.Controls[i];

                    if (p.Text.Equals(""))
                        obj = null;
                    else
                        obj = p.Text;
                }

                if (this.groupBox1.Controls[i].GetType() == typeof(PSMaskedTextBox))
                {
                    PSMaskedTextBox p = new PSMaskedTextBox();
                    p = (PSMaskedTextBox)this.groupBox1.Controls[i];

                    if (p.Text.Equals(""))
                        obj = null;
                    else
                        obj = p.Text;
                }

                if (this.groupBox1.Controls[i].GetType() == typeof(PSMemoBox))
                {
                    PSMemoBox p = new PSMemoBox();
                    p = (PSMemoBox)this.groupBox1.Controls[i];

                    if (p.Text.Equals(""))
                        obj = null;
                    else
                        obj = p.Text; ;
                }

                if (this.groupBox1.Controls[i].GetType() == typeof(PSMoedaBox))
                {
                    PSMoedaBox p = new PSMoedaBox();
                    p = (PSMoedaBox)this.groupBox1.Controls[i];

                    obj = Convert.ToDouble(p.Text);
                }

                if (this.groupBox1.Controls[i].GetType() == typeof(PSComboBox))
                {
                    PSComboBox p = new PSComboBox();
                    p = (PSComboBox)this.groupBox1.Controls[i];

                    obj = p.Value;
                }

                if (this.groupBox1.Controls[i].GetType() == typeof(PSCheckBox))
                {
                    PSCheckBox p = new PSCheckBox();
                    p = (PSCheckBox)this.groupBox1.Controls[i];

                    if (p.Checked)
                        obj = 1;
                    else
                        obj = 0;
                }

                if (this.groupBox1.Controls[i].GetType() == typeof(PSDateBox))
                {
                    PSDateBox p = new PSDateBox();
                    p = (PSDateBox)this.groupBox1.Controls[i];

                    if (p.Text == null)
                    {
                        obj = null;
                    }
                    else
                    {
                        obj = Convert.ToDateTime(p.Text);
                    }
                }
            }

            return obj;
        }

        private void FrmBaseFiltroManutencao_FormClosed(object sender, FormClosedEventArgs e)
        {
            // PSMemoryManager.ReleaseUnusedMemory(false);
        }
    }
}
