using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb.Class
{
    public class FBOLETO
    {
        public FBOLETO() { }


        public int CODEMPRESA { get; set; }
        public int CODLANCA { get; set; }
        public int CODFILIAL { get; set; }
        public string NUMERO { get; set; }
        public string CODCLIFOR { get; set; }
        public DateTime DATAEMISSAO { get; set; }
        public DateTime DATAVENCIMENTO { get; set; }
        public DateTime DATABOLETO { get; set; }
        public string CODMOEDA { get; set; }
        public decimal VALOR { get; set; }
        public string CODCONTA { get; set; }
        public string CODTIPDOC { get; set; }
        public string CODCONVENIO { get; set; }
        public int ACEITE { get; set; }
        public string NOSSONUMERO { get; set; }
        public string CODIGOBARRAS { get; set; }
        public string IPTE { get; set; }
        public int CODREMESSA { get; set; }
        public DateTime? DATAREMESSA { get; set; }
        public int IDBOLETOSTATUS { get; set; }
        public string DVNOSSONUMERO { get; set; }

        /// <summary>
        /// Método para realizar o update ou insert na tabela FBOLETO
        /// </summary>
        /// <param name="conn"> Conexão</param>
        /// <param name="fboleto">Classe</param>
        /// <returns>true / false</returns>
        public bool persistFBOLETO(AppLib.Data.Connection conn, FBOLETO fboleto)
        {
            try
            {
                AppLib.ORM.Jit FBOLETO = new AppLib.ORM.Jit(conn, "FBOLETO");
                FBOLETO.Set("CODEMPRESA", CODEMPRESA);
                FBOLETO.Set("CODLANCA", CODLANCA);
                FBOLETO.Set("CODFILIAL", CODFILIAL);
                FBOLETO.Set("NUMERO", NUMERO);
                FBOLETO.Set("CODCLIFOR", CODCLIFOR);
                FBOLETO.Set("DATAEMISSAO", DATAEMISSAO);
                FBOLETO.Set("DATAVENCIMENTO", DATAVENCIMENTO);
                FBOLETO.Set("DATABOLETO", DATABOLETO);
                FBOLETO.Set("CODMOEDA", CODMOEDA);
                FBOLETO.Set("VALOR", VALOR);
                FBOLETO.Set("CODCONTA", CODCONTA);
                FBOLETO.Set("CODTIPDOC", CODTIPDOC);
                FBOLETO.Set("CODCONVENIO", CODCONVENIO);
                FBOLETO.Set("ACEITE", ACEITE);
                FBOLETO.Set("NOSSONUMERO", NOSSONUMERO);
                FBOLETO.Set("CODIGOBARRAS", CODIGOBARRAS);
                FBOLETO.Set("IPTE", IPTE);
                FBOLETO.Set("CODREMESSA", CODREMESSA);
                FBOLETO.Set("DATAREMESSA", DATAREMESSA);
                FBOLETO.Set("IDBOLETOSTATUS", IDBOLETOSTATUS);
                FBOLETO.Set("DVNOSSONUMERO", DVNOSSONUMERO);
                FBOLETO.Save();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
