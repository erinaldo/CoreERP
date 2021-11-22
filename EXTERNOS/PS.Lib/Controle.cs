using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class Controle
    {
        #region CONSTANTES

        private const string CONST_A = "A";
        private const string CONST_B = "B";
        private const string CONST_C = "C";
        private const string CONST_D = "D";
        private const string CONST_E = "E";
        private const string CONST_F = "F";
        private const string CONST_G = "G";
        private const string CONST_H = "H";
        private const string CONST_I = "I";
        private const string CONST_J = "J";
        private const string CONST_K = "K";
        private const string CONST_L = "L";
        private const string CONST_M = "M";
        private const string CONST_N = "N";
        private const string CONST_O = "O";
        private const string CONST_P = "P";
        private const string CONST_Q = "Q";
        private const string CONST_R = "R";
        private const string CONST_S = "S";
        private const string CONST_T = "T";
        private const string CONST_U = "U";
        private const string CONST_V = "V";
        private const string CONST_W = "W";
        private const string CONST_X = "X";
        private const string CONST_Y = "Y";
        private const string CONST_Z = "Z";

        private string[] Words = new string[26];

        private Data.DBS dbs = new Data.DBS();

        #endregion

        #region MÉTODOS ANTIGOS

        public Controle()
        {
            Words[0] = CONST_A;
            Words[1] = CONST_B;
            Words[2] = CONST_C;
            Words[3] = CONST_D;
            Words[4] = CONST_E;
            Words[5] = CONST_F;
            Words[6] = CONST_G;
            Words[7] = CONST_H;
            Words[8] = CONST_I;
            Words[9] = CONST_J;
            Words[10] = CONST_K;
            Words[11] = CONST_L;
            Words[12] = CONST_M;
            Words[13] = CONST_N;
            Words[14] = CONST_O;
            Words[15] = CONST_P;
            Words[16] = CONST_Q;
            Words[17] = CONST_R;
            Words[18] = CONST_S;
            Words[19] = CONST_T;
            Words[20] = CONST_U;
            Words[21] = CONST_W;
            Words[22] = CONST_X;
            Words[23] = CONST_Y;
            Words[24] = CONST_Z;
        }

        public bool ValidaVersao(string key1, string key2, string key3)
        {
            try
            {
                if (key1 != key2)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidaControle(string key1, string key2, string key3, string key4)
        {
            return true;
            try
            {
                string key5 = string.Empty;
                string key6 = string.Empty;
                string key7 = string.Empty;
                string key8 = string.Empty;

                key2 = key2.Replace(".", "").Replace("-", "").Replace("/", "");

                for (int i = 0; i < key2.Length; i++)
                {
                    int indice = int.Parse(key2[i].ToString());
                    key5 = string.Concat(key5, Words[indice]);
                }

                for (int i = 0; i < key3.Length; i++)
                {
                    int indice = int.Parse(key3[i].ToString());

                    if (indice <= key5.Length)
                    {
                        key5 = key5.Remove(indice, 1);
                    }
                }

                for (int i = 0; i < key4.Length; i++)
                {
                    int indice = int.Parse(key4[i].ToString());
                    key8 = string.Concat(key8, Words[indice]);
                }

                key7 = string.Concat(key8, key5);
                key7 = new PS.Lib.Criptografia().Hash(Criptografia.OpcoesHash.MD5, key7);

                if (key7 != key1)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidaIntegridade()
        {
            try
            {
                string sSql = "SELECT CGCCPF, COUNT(CODEMPRESA) CONTADOR FROM GEMPRESA GROUP BY CGCCPF HAVING COUNT(CODEMPRESA) > 1";
                System.Data.DataTable dt = dbs.QuerySelect(sSql);
                if (dt.Rows.Count > 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        
        #endregion

        public int ObtemTotalLicencasDeAplicativo()
        {
            try
            {
                String consulta = "SELECT TOP 1 LICENCAAPP FROM GPARAMETROS WHERE LICENCAAPP IS NOT NULL";
                System.Data.DataTable dt = dbs.QuerySelect(consulta);
                if (dt.Rows.Count > 0)
                {
                    String LICENCAAPP = dt.Rows[0]["LICENCAAPP"].ToString();
                    AppLib.Util.Criptografia c = new AppLib.Util.Criptografia();
                    String result = c.Decoder(AppLib.Util.Criptografia.OpcoesEncoder.Rijndael, LICENCAAPP, "merbibuziberbibonzi");
                    String[] parametros = result.Split(';');
                    int licencas = int.Parse(parametros[0]);
                    return licencas;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public int ObtemTotalUsuariosAtivos()
        {
            try
            {
                String consulta = @"
SELECT COUNT(*) TOTAL
FROM GUSUARIO
WHERE ULTIMOLOGIN > DATEADD(SS, -((3*60)+5), GETDATE())";

                System.Data.DataTable dt = dbs.QuerySelect(consulta);
                if (dt.Rows.Count > 0)
                {
                    int TOTAL = int.Parse(dt.Rows[0]["TOTAL"].ToString());
                    return TOTAL;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter total de usuários ativos. Detalhe técnico: " + ex.Message);
            }
        }

        public Boolean TemLicencaDisponivel()
        {
            if (ObtemTotalLicencasDeAplicativo() > this.ObtemTotalUsuariosAtivos())
            {
                return true;
            }

            return false;
        }

        public Boolean AtualizarDataLogin(String CODUSUARIO)
        {
            try
            {
                String comando = "UPDATE GUSUARIO SET ULTIMOLOGIN = GETDATE() WHERE CODUSUARIO = ?";
                if (dbs.QueryExec(comando, new Object[] { CODUSUARIO }) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public Boolean TrataLicencaNoLogin(String CODUSUARIO)
        {
            if (this.TemLicencaDisponivel())
            {
                return this.AtualizarDataLogin(CODUSUARIO);
            }
            else
            {
                return false;
            }
        }

        public Boolean AtualizarLogoff(String CODUSUARIO)
        {
            try
            {
                String comando = "UPDATE GUSUARIO SET ULTIMOLOGIN = NULL WHERE CODUSUARIO = ?";
                if (dbs.QueryExec(comando, new Object[] { CODUSUARIO }) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
