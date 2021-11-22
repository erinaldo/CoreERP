using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PS.Lib.WinForms.Anexo
{
    public class AnexoServices
    {
        Data.DBS dbs = new Data.DBS();
        Global gb = new Global();

        public AnexoServices()
        { 
        
        }

        public bool SalverAnexo(string pspart, string chave, int seq, string nome, string descricao, string extensao, string tipo, FileStream anexo)
        {
            if (seq == 0)
            {
                byte[] bAnexo = new byte[anexo.Length];
                anexo.Read(bAnexo, 0, System.Convert.ToInt32(anexo.Length));

                anexo.Close();

                return IncluirAnexo(pspart, chave, nome, descricao, extensao, tipo, bAnexo);
            }
            else
            {
                return AlterarAnexo(pspart, chave, seq, nome, descricao, extensao, tipo, null);
            }
        }

        public bool IncluirAnexo(string pspart, string chave, string nome, string descricao, string extensao, string tipo, byte[] anexo)
        {
            int idimg = gb.SetImagem(anexo);

            if(idimg > 0)
            {
                int idseq = gb.GetIdNovoRegistro(Contexto.Session.Empresa.CodEmpresa, "GANEXO");

                string sSql = "INSERT INTO GANEXO (CODPSPART, CODANEXO, NSEQ, NOME, DESCRICAO, EXTENSAO, TIPO, CODIMAGEM) VALUES(?,?,?,?,?,?,?,?)";

                dbs.QueryExec(sSql, pspart, chave, idseq, nome, descricao, extensao, tipo, idimg);

                return true;
            }

            return false;        
        }

        public bool AlterarAnexo(string pspart, string chave, int seq, string nome, string descricao, string extensao, string tipo, byte[] anexo)
        {
            string sSql = "UPDATE GANEXO SET NOME = ?, DESCRICAO = ?, TIPO = ? WHERE CODPSPART = ? AND CODANEXO = ? AND NSEQ = ?";

            dbs.QueryExec(sSql, nome, descricao, tipo, pspart, chave, seq);

            return true;      
        }

        public bool ExcluirAnexo(string pspart, string chave, int seq, int idImg)
        {
            string sSql = string.Empty;

            sSql = "DELETE FROM GANEXO WHERE CODPSPART = ? AND CODANEXO = ? AND NSEQ = ?";

            dbs.QueryExec(sSql, pspart, chave, seq);

            sSql = "DELETE FROM GIMAGEM WHERE CODIMAGEM = ?";

            dbs.QueryExec(sSql, idImg);

            return true;        
        }

        public bool Download(int idImg, string FileName)
        {
            try
            {
                string sSql = "SELECT IMAGEM FROM GIMAGEM WHERE CODIMAGEM = ?";

                System.Data.DataTable dt = dbs.QuerySelect(sSql, idImg);

                if (dt.Rows.Count > 0)
                {
                    byte[] Anexo = new byte[0];

                    Anexo = (byte[])dt.Rows[0]["IMAGEM"];
                    int ArraySize = new int();
                    ArraySize = Anexo.GetUpperBound(0);

                    FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(Anexo, 0, ArraySize);
                    fs.Close();

                    return true;
                }

                return false;
            }
            catch
            {
                return false;            
            }
        }
    }
}
