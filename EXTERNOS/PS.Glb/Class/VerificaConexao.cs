using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PS.Glb.Class
{
    public static class VerificaConexao
    {
        //Use if(IsConnected())
        //Método da API
        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);

        // Um método que verifica se esta conectado
        /*
        public static Boolean temInternet()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }
        */

        public static Boolean temInternet(int filial=1)
        {
            bool temconexao = false;
            string Token = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TOKEN FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, filial }).ToString();
            NFeAPI NfeAPI = new NFeAPI();
            try
            {
                //string filial = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODFILIAL FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codoper }).ToString();
                string Cnpj = NfeAPI.getCGCCPFFormatado(filial);
                string TpAmb = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, filial }).ToString();
                string Versao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT VERSAO FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, filial }).ToString();
                string Uf = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT GESTADO.CODIBGE FROM GFILIAL INNER JOIN GESTADO ON GFILIAL.CODETD = GESTADO.CODETD WHERE GFILIAL.CODEMPRESA = ? AND GFILIAL.CODFILIAL = ? ", new object[] { AppLib.Context.Empresa, filial }).ToString();


                string retornoStatusNf = NfeAPI.StatusNFOperacao(Token, Cnpj, TpAmb, Uf, Versao);

                dynamic JsonRetornoConsultaStatusNf = JsonConvert.DeserializeObject(retornoStatusNf);
                string StatusMonitor = JsonRetornoConsultaStatusNf.status;
                string cStat = JsonRetornoConsultaStatusNf.retStatusServico.cStat;
                string Motivo = JsonRetornoConsultaStatusNf.retStatusServico.xMotivo;
                string Mensagem = string.Empty;

                if (StatusMonitor == "200")
                    temconexao = true;
                else
                    temconexao = false;
            }
            catch (Exception)
            {
                temconexao = false;
            }

            return temconexao;
        }

    }
}
