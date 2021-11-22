using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;
using System.Data;

namespace PS.Glb
{
    public class PSPartPerfilData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private PS.Lib.Global gb = new PS.Lib.Global();

        public override string ReadView()
        {
            return @"SELECT CODPERFIL,
NOME,
CONVERT(BIT, ATIVO) ATIVO
FROM GPERFIL WHERE ";
        }

        public override void ValidateRecord(List<DataField> objArr)
        {
            base.ValidateRecord(objArr);

            for (int j = 0; j < objArr.Count; j++)
            {
                if (objArr[j].Field.ToString() == "NOME")
                {
                    if (objArr[j].Valor.ToString() == string.Empty)
                    {
                        throw new Exception(gb.MensagemDeValidacao(_tablename, objArr[j].Field));
                    }
                }
            }
        }

        public override List<DataField> InsertRecord(List<DataField> objArr)
        {
            List<DataField> dtf = base.InsertRecord(objArr);

            System.Data.DataTable dt = new System.Data.DataTable();
            dt = dbs.QuerySelect("SELECT CODPSPART FROM GPSPART ORDER BY CODPSPART");

            if (dt.Rows.Count > 0)
            {
                DataField key = new DataField();

                for (int i = 0; i < objArr.Count; i++)
                {
                    if (objArr[i].Field == "CODPERFIL")
                    {
                        key = objArr[i];
                    }            
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dbs.QueryExec("INSERT INTO GACESSOMENU VALUES(?,?,?,?,?,?,?)", Contexto.Session.Empresa.CodEmpresa,
                                                                               key.Valor,
                                                                               dt.Rows[i]["CODPSPART"].ToString(), 0, 0, 0, 0);
                }
            }

            return dtf;
        }

        public override void DeleteRecord(List<DataField> objArr)
        {
            base.ValidateKeyRecord(objArr);

            string chave = "";

            for (int j = 0; j < objArr.Count; j++)
            {
                if (objArr[j].Field.ToString() == "CODPERFIL")
                {
                    chave = objArr[j].Valor.ToString();
                }
            }

            if (dbs.QueryFind("SELECT CODUSUARIO FROM GUSUARIOPERFIL WHERE CODPERFIL = ?", chave))
            {
                throw new Exception("Atenção. Perfil não pode ser excluido pois esta vinculado a um ou mais usuários.");
            }
            else
            {
                dbs.QueryExec("DELETE FROM GACESSOMENU WHERE CODEMPRESA = ? AND CODPERFIL = ?", Contexto.Session.Empresa.CodEmpresa, chave);
            }

            base.DeleteRecord(objArr);
        }
    }
}
