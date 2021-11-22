using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartRegCaixaData : PS.Lib.WinForms.PSPartData
    {
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public override void ValidateRecord(List<PS.Lib.DataField> objArr)
        {
            base.ValidateRecord(objArr);

            PS.Lib.DataField dataField = new PS.Lib.DataField();
            dataField = gb.RetornaDataFieldByCampo(objArr, "CODOPERADOR");

            if (dataField.Valor == null)
            {
                throw new Exception(gb.MensagemDeValidacao(_tablename, dataField.Field.ToString()));            
            }

            dataField = gb.RetornaDataFieldByCampo(objArr, "CODCAIXA");

            if (dbs.QueryFind("SELECT CODCAIXA FROM VREGCAIXA WHERE CODEMPRESA = ? AND CODCAIXA = ? AND DATAFECHAMENTO IS NULL", PS.Lib.Contexto.Session.Empresa.CodEmpresa, dataField.Valor))
            {
                throw new Exception("Ja existe um caixa " + dataField.Valor + " aberto.");
            }

            dataField = gb.RetornaDataFieldByCampo(objArr, "CODOPERADOR");

            if (dbs.QueryFind("SELECT CODOPERADOR FROM VREGCAIXA WHERE CODEMPRESA = ? AND CODOPERADOR = ? AND DATAFECHAMENTO IS NULL", PS.Lib.Contexto.Session.Empresa.CodEmpresa, dataField.Valor))
            {
                throw new Exception("Operador " + dataField.Valor + " ja esta alocado em outro caixa.");
            }
        }
    }
}
