using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartTabCampoComplData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public override string ReadView()
        {
            return @"SELECT CODENTIDADE,
NOMECAMPO,
DESCRICAO,
TIPO,
TAMANHO,
ORDEM,
CODTABELA,
CONVERT(BIT, ATIVO) ATIVO,
CASASDECIMAIS,
TAMANHOCAMPO,
ALTURACAMPO
FROM GTABCAMPOCOMPL WHERE ";
        }

        private void ExecutaComandoDDL(List<PS.Lib.DataField> objArr)
        { 
            PS.Lib.DataField dtCODENTIDADE = gb.RetornaDataFieldByCampo(objArr, "CODENTIDADE");
            PS.Lib.DataField dtNOMECAMPO = gb.RetornaDataFieldByCampo(objArr, "NOMECAMPO");
            PS.Lib.DataField dtTIPO = gb.RetornaDataFieldByCampo(objArr, "TIPO");
            PS.Lib.DataField dtTAMANHO = gb.RetornaDataFieldByCampo(objArr, "TAMANHO");
            PS.Lib.DataField dtTAMANHOCAMPO = gb.RetornaDataFieldByCampo(objArr, "TAMANHOCAMPO");
            PS.Lib.DataField dtALTURACAMPO = gb.RetornaDataFieldByCampo(objArr, "ALTURACAMPO");

            string sCmd = string.Concat("ALTER TABLE ",dtCODENTIDADE.Valor.ToString()," ADD ", dtNOMECAMPO.Valor.ToString(), " ");

            if(dtTIPO.Valor.ToString() == "PSTextoBox")
            {
                int iTamanho = int.Parse(dtTAMANHO.Valor.ToString());

                sCmd = string.Concat(sCmd, "VARCHAR(",iTamanho,") NULL");
            }

            if (dtTIPO.Valor.ToString() == "PSDateBox")
            {
                sCmd = string.Concat(sCmd, " DATETIME NULL");
            }

            if (dtTIPO.Valor.ToString() == "PSLookup")
            {
                int iTamanho = 25;

                sCmd = string.Concat(sCmd, "VARCHAR(", iTamanho, ") NULL");
            }

            if (dtTIPO.Valor.ToString() == "PSCheckBox")
            {
                sCmd = string.Concat(sCmd, " INTEGER NOT NULL DEFAULT(0)");
            }

            if (dtTIPO.Valor.ToString() == "PSMoedaBox")
            {
                sCmd = string.Concat(sCmd, " FLOAT NOT NULL DEFAULT(0)");
            }

            if (dtTIPO.Valor.ToString() == "TextEdit")
            {
                int iTamanho = int.Parse(dtTAMANHO.Valor.ToString());

                sCmd = string.Concat(sCmd, "VARCHAR(", iTamanho, ") NULL");
            }

            dbs.QueryExec(sCmd);
        }

        private bool CaracterValido(char caracter)
        {
            bool Flag = false;

            if (caracter == '1' || caracter == '2' || caracter == '3' || caracter == '4' || caracter == '5' || 
                caracter == '6' || caracter == '7' || caracter == '8' || caracter == '9' || caracter == '0')
                Flag = true;

            if (caracter == 'Q' || caracter == 'W' || caracter == 'E' || caracter == 'R' || caracter == 'T' ||
                caracter == 'Y' || caracter == 'U' || caracter == 'I' || caracter == 'O' || caracter == 'P' ||
                caracter == 'A' || caracter == 'S' || caracter == 'D' || caracter == 'F' || caracter == 'G' ||
                caracter == 'H' || caracter == 'J' || caracter == 'K' || caracter == 'L' || caracter == 'Z' ||
                caracter == 'X' || caracter == 'C' || caracter == 'V' || caracter == 'B' || caracter == 'N' ||
                caracter == 'M')
                Flag = true;

            if (caracter == '_')
                Flag = true;

            return Flag;        
        }

        public override void ValidateRecord(List<PS.Lib.DataField> objArr)
        {
            base.ValidateRecord(objArr);

            PS.Lib.DataField dtNOMECAMPO = gb.RetornaDataFieldByCampo(objArr, "NOMECAMPO");
            PS.Lib.DataField dtDESCRICAO = gb.RetornaDataFieldByCampo(objArr, "DESCRICAO");
            PS.Lib.DataField dtTIPO = gb.RetornaDataFieldByCampo(objArr, "TIPO");
            PS.Lib.DataField dtTAMANHO = gb.RetornaDataFieldByCampo(objArr, "TAMANHO");
            PS.Lib.DataField dtTAMANHOCAMPO = gb.RetornaDataFieldByCampo(objArr, "TAMANHOCAMPO");

            for (int i = 0; i < dtNOMECAMPO.Valor.ToString().Length -1; i++)
            {
                if (!CaracterValido(dtNOMECAMPO.Valor.ToString().ToUpper()[i]))
                {
                    throw new Exception(string.Concat("Caracter [", dtNOMECAMPO.Valor.ToString()[i], "] inválido para o campo Nome do Campo."));
                }
            }

            if (dtDESCRICAO.Valor == null)
            {
                throw new Exception(string.Concat("Campo Descrição deve ser informado."));            
            }

            if (dtTIPO.Valor.ToString() == "PSTextoBox")
            {
                if (dtTAMANHO.Valor == null)
                {
                    throw new Exception("Tamanho do campo deve ser maior que zero.");
                }

                if (int.Parse(dtTAMANHO.Valor.ToString()) <= 0)
                {
                    throw new Exception("Tamanho do campo deve ser maior que zero.");
                }
            }
        }

        public override List<PS.Lib.DataField> SaveRecord(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dtNOMECAMPO = gb.RetornaDataFieldByCampo(objArr, "NOMECAMPO");

            if (dtNOMECAMPO.Valor != null)
            {
                for (int i = 0; i < objArr.Count; i++)
                {
                    if (objArr[i].Field == "NOMECAMPO")
                    {
                        objArr[i].Valor = dtNOMECAMPO.Valor.ToString().ToUpper();
                    }
                }
            }

            return base.SaveRecord(objArr);
        }

        public override List<PS.Lib.DataField> InsertRecord(List<PS.Lib.DataField> objArr)
        {
            List<PS.Lib.DataField> objArrDDL = objArr;

            List<PS.Lib.DataField> temp = base.InsertRecord(objArr);

            ExecutaComandoDDL(objArrDDL);

            return temp;
        }
    }
}
