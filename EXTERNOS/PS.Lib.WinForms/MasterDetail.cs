using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib.WinForms
{
    public class MasterDetail
    {
        public PS.Lib.WinForms.PSPart psPartDetail { get; set; }
        public List<PS.Lib.DataField> dataFilterDetail { get; set; }
        public bool ViewEnabled { get; set; }

        public MasterDetail(PS.Lib.WinForms.PSPart Detail, bool AllowView , params string[] FieldName)
        {
            psPartDetail = Detail;
            dataFilterDetail = SetDataField(FieldName);
            ViewEnabled = AllowView;
        }

        public MasterDetail(PS.Lib.WinForms.PSPart Detail, params string[] FieldName)
        {
            psPartDetail = Detail;
            dataFilterDetail = SetDataField(FieldName);
            ViewEnabled = false;
        }

        private List<PS.Lib.DataField> SetDataField(string[] FieldName)
        {
            List<PS.Lib.DataField> objList = new List<PS.Lib.DataField>();

            for (int i = 0; i < FieldName.Length; i++)
            {
                DataField objDf = new DataField();

                if (FieldName[i] == "CODEMPRESA")
                {
                    if (Contexto.Session.Empresa != null)
                    {
                        objDf.Field = "CODEMPRESA";
                        objDf.Valor = Contexto.Session.Empresa.CodEmpresa;
                        objDf.Tipo = Contexto.Session.Empresa.CodEmpresa.GetType();
                    }
                }
                else
                {
                    objDf.Field = FieldName[i];
                }

                objList.Add(objDf);
            }

            return objList;
        }
    }
}
