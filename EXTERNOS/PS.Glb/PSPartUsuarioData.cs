using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartUsuarioData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private PS.Lib.Global gb = new PS.Lib.Global();

        public override string ReadView()
        {
            return @"SELECT CODUSUARIO,
NOME,
SENHA,
EMAIL,
CONVERT(BIT, ATIVO) ATIVO,
DTEXPIRACAO,
CONVERT(BIT, NUNCAEXPIRA) NUNCAEXPIRA,
SUPERVISOR,
ULTIMOLOGIN
FROM GUSUARIO WHERE ";
        }

        public override List<PS.Lib.DataField> InsertRecord(List<PS.Lib.DataField> objArr)
        {
            string chave = "";

            for (int i = 0; i < objArr.Count; i++)
            {
                if (objArr[i].Field == "CODUSUARIO")
                {
                    chave = objArr[i].Valor.ToString();
                }
            }

            for (int i = 0; i < objArr.Count; i++)
            {
                if (objArr[i].Field == "SENHA")
                {
                    objArr[i].Valor = new PS.Lib.Criptografia().Encoder(PS.Lib.Criptografia.OpcoesEncoder.Rijndael, objArr[i].Valor.ToString(), chave);
                }
            }

            return base.InsertRecord(objArr);
        }

        public override List<PS.Lib.DataField> EditRecord(List<PS.Lib.DataField> objArr)
        {
            string chave = "";
            string senhaold = "";
            string senhanew = "";

            for (int i = 0; i < objArr.Count; i++)
            {
                if (objArr[i].Field == "CODUSUARIO")
                {
                    chave = objArr[i].Valor.ToString();
                }
            }

            for (int i = 0; i < objArr.Count; i++)
            {
                if (objArr[i].Field == "SENHA")
                {
                    senhanew = objArr[i].Valor.ToString();
                }
            }

            senhaold = dbs.QueryValue(null, "SELECT SENHA FROM GUSUARIO WHERE CODUSUARIO = ?", chave).ToString();

            if (senhanew != senhaold)
            {
                for (int i = 0; i < objArr.Count; i++)
                {
                    if (objArr[i].Field == "SENHA")
                    {
                        objArr[i].Valor = new PS.Lib.Criptografia().Encoder(PS.Lib.Criptografia.OpcoesEncoder.Rijndael, objArr[i].Valor.ToString(), chave);
                    }
                }            
            }

            return base.EditRecord(objArr);
        }
    }
}
