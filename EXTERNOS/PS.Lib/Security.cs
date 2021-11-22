using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class Security
    {
        private Data.DBS dbs = new Data.DBS();
        private Controle control = new Controle();

        public enum ButtonAct
        {
            Incluir,
            Editar,
            Excluir
        }

        public bool ValidaControle(string key1, string key2, string key3, string key4)
        {
            if (control.ValidaControle(key1, key2, key3, key4))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidaCodUser()
        {
            return true;
        }

        public bool AuthenticatedUser()
        {
            bool Flag = false;

            if (Contexto.Session.CodUsuario != null)
            {
                Flag = true;
            }

            return Flag;
        }

        public bool SelectedContext()
        {
            bool Flag = false;

            if (Contexto.Session.Empresa != null)
            {
                Flag = true;
            }

            return Flag;
        }

        public bool ValidAccess(string SecurityID, string ModuloID)
        {
            int valor = 0;

            for (int i = 0; i < Contexto.Session.CodPerfil.Length; i++)
            {
                valor = int.Parse(dbs.QueryValue("0", "SELECT ACESSO FROM GACESSOMENU WHERE CODEMPRESA = ? AND CODPERFIL = ? AND CODPSPART = ?", 
                    Contexto.Session.Empresa.CodEmpresa, Contexto.Session.CodPerfil[i], SecurityID).ToString());

                if (valor == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ValidAccess(string SecurityID, string ModuloID, ButtonAct btn)
        {
            int valor = 0;

            if (btn == ButtonAct.Incluir)
            {

                for (int i = 0; i < Contexto.Session.CodPerfil.Length; i++)
                {
                    valor = int.Parse(dbs.QueryValue("0", "SELECT PERMITEINCLUIR FROM GACESSOMENU WHERE CODEMPRESA = ? AND CODPERFIL = ? AND CODPSPART = ?",
                        Contexto.Session.Empresa.CodEmpresa, Contexto.Session.CodPerfil[i], SecurityID).ToString());

                    if (valor == 1)
                    {
                        return true;
                    }
                }

                return false;
            }

            if (btn == ButtonAct.Editar)
            {

                for (int i = 0; i < Contexto.Session.CodPerfil.Length; i++)
                {
                    valor = int.Parse(dbs.QueryValue("0", "SELECT PERMITEALTERAR FROM GACESSOMENU WHERE CODEMPRESA = ? AND CODPERFIL = ? AND CODPSPART = ?",
                        Contexto.Session.Empresa.CodEmpresa, Contexto.Session.CodPerfil[i], SecurityID).ToString());

                    if (valor == 1)
                    {
                        return true;
                    }
                }

                return false;
            }

            if (btn == ButtonAct.Excluir)
            {

                for (int i = 0; i < Contexto.Session.CodPerfil.Length; i++)
                {
                    valor = int.Parse(dbs.QueryValue("0", "SELECT PERMITEEXCLUIR FROM GACESSOMENU WHERE CODEMPRESA = ? AND CODPERFIL = ? AND CODPSPART = ?",
                        Contexto.Session.Empresa.CodEmpresa, Contexto.Session.CodPerfil[i], SecurityID).ToString());

                    if (valor == 1)
                    {
                        return true;
                    }
                }

                return false;
            }

            return false;
        }
    }
}
