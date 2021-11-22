using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using ValidateLib;
/*
using PS.Validate.Services.WSConsultaCTe;
using PS.Validate.Services.WSConsultaDistribuicaoNFe;
using PS.Validate.Services.WSRecepcaoEventoNFe;
using PS.Validate.Services.WSStatusServicoNFe;
using PS.Validate.Services.WSNFeRetAutorizacao;
using PS.Validate.Services.WSConsultaNFe;
using PS.Validate.Services.WSNFeAutorizacao;
*/
namespace ValidateLib
{
    public class NFeSrv
    {
        private AppLib.Data.Connection _conn;
        private AppLib.Util.ObjetoXML _objetoXML;

        public System.Data.DataSet StringToDataSet(string XML)
        {
            try
            {
                System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                xdoc.LoadXml(XML);
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(xdoc.DocumentElement.OuterXml)));

                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static byte[] Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new System.IO.Compression.GZipStream(compressedStream, System.IO.Compression.CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                var buffer = new byte[4096];
                int read;

                while ((read = zipStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    resultStream.Write(buffer, 0, read);
                }

                return resultStream.ToArray();
            }
        }

        public string NFAutorizacao(List<ValidateLib.OutBoxParams> ListOutBoxPar, ValidateLib.WebServiceParams WebServicePar, ValidateLib.EmpresaParams EmpresaPar)
        {
            System.Xml.XmlDocument xdoc;
            string envioXML;

            _conn = AppLib.Context.poolConnection.Get("Start").Clone();

            //string Versao = _conn.ExecGetField(string.Empty, "SELECT VERSAO FROM VPARAMETROS WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();
            string Versao = _conn.ExecGetField(string.Empty, "SELECT VERSAO FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial }).ToString();

            //carrega os parametros
            WebServicePar.Versao = Versao;

            WebServicePar.CodEstrutura = "NF-e";
            //WebServicePar.Versao = "3.10";
            ValidateLib.Contexto.Session.DiretorioSchemas = @"schemas\";
            WebServicePar.Schema = "enviNFe_v3.10";

            TimeZone timeZone = TimeZone.CurrentTimeZone;

            ValidateLib.enviNFe_ns.TEnviNFe _enviNFe = new ValidateLib.enviNFe_ns.TEnviNFe();
            EmpresaPar.GetNewLote();
            
            //Dirlei
            //_enviNFe.idLote = EmpresaPar.UltLote.ToString();
            _enviNFe.indSinc = ValidateLib.enviNFe_ns.TEnviNFeIndSinc.Item1;
            _enviNFe.NFe = new ValidateLib.enviNFe_ns.TNFe[ListOutBoxPar.Count];
            _enviNFe.versao = WebServicePar.Versao;
            
            
            int cont = 0;
            foreach (ValidateLib.OutBoxParams OutBoxPar in ListOutBoxPar)
            {
                ValidateLib.enviNFe_ns.TNFe _nfe = new ValidateLib.enviNFe_ns.TNFe();

                #region infNFe

                _nfe.infNFe = new ValidateLib.enviNFe_ns.TNFeInfNFe();
                _nfe.infNFe.versao = WebServicePar.Versao;
                _nfe.infNFe.Id = string.Concat("NFe", OutBoxPar.nfeDoc.Chave);

                #region ide

                _nfe.infNFe.ide = new ValidateLib.enviNFe_ns.TNFeInfNFeIde();

                _nfe.infNFe.ide.cDV = OutBoxPar.nfeDoc.nfeIde.cDV.ToString();
                _nfe.infNFe.ide.cMunFG = OutBoxPar.nfeDoc.nfeIde.cMunFG;
                _nfe.infNFe.ide.cNF = OutBoxPar.nfeDoc.nfeIde.cNF;

                if (OutBoxPar.nfeDoc.nfeIde.cUF == 11)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item11;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 12)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item12;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 13)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item13;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 14)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item14;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 15)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item15;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 16)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item16;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 17)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item17;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 21)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item21;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 22)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item22;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 23)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item23;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 24)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item24;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 25)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item25;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 26)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item26;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 27)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item27;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 28)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item28;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 29)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item29;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 31)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item31;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 32)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item32;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 33)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item33;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 35)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item35;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 41)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item41;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 42)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item42;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 43)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item43;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 50)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item50;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 51)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item51;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 52)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item52;
                if (OutBoxPar.nfeDoc.nfeIde.cUF == 53)
                    _nfe.infNFe.ide.cUF = ValidateLib.enviNFe_ns.TCodUfIBGE.Item53;

                _nfe.infNFe.ide.dhCont = (OutBoxPar.nfeDoc.nfeIde.dhCont == null) ? null : (timeZone.ToLocalTime((DateTime)OutBoxPar.nfeDoc.nfeIde.dhCont)).ToString("yyyy-MM-ddThh:mm:sszzz");
                _nfe.infNFe.ide.dhEmi = string.Format("{0:yyyy-MM-ddTHH:mm:sszzz}", OutBoxPar.nfeDoc.nfeIde.dhEmi);
                _nfe.infNFe.ide.dhSaiEnt = string.Format("{0:yyyy-MM-ddTHH:mm:sszzz}", OutBoxPar.nfeDoc.nfeIde.dhSaiEnt);

                if (OutBoxPar.nfeDoc.nfeIde.finNFe == 1)
                    _nfe.infNFe.ide.finNFe = ValidateLib.enviNFe_ns.TFinNFe.Item1;
                if (OutBoxPar.nfeDoc.nfeIde.finNFe == 2)
                    _nfe.infNFe.ide.finNFe = ValidateLib.enviNFe_ns.TFinNFe.Item2;
                if (OutBoxPar.nfeDoc.nfeIde.finNFe == 3)
                    _nfe.infNFe.ide.finNFe = ValidateLib.enviNFe_ns.TFinNFe.Item3;
                if (OutBoxPar.nfeDoc.nfeIde.finNFe == 4)
                    _nfe.infNFe.ide.finNFe = ValidateLib.enviNFe_ns.TFinNFe.Item4;

                if (OutBoxPar.nfeDoc.nfeIde.idDest == 1)
                    _nfe.infNFe.ide.idDest = ValidateLib.enviNFe_ns.TNFeInfNFeIdeIdDest.Item1;
                if (OutBoxPar.nfeDoc.nfeIde.idDest == 2)
                    _nfe.infNFe.ide.idDest = ValidateLib.enviNFe_ns.TNFeInfNFeIdeIdDest.Item2;
                if (OutBoxPar.nfeDoc.nfeIde.idDest == 3)
                    _nfe.infNFe.ide.idDest = ValidateLib.enviNFe_ns.TNFeInfNFeIdeIdDest.Item3;

                if (OutBoxPar.nfeDoc.nfeIde.indFinal == 0)
                    _nfe.infNFe.ide.indFinal = ValidateLib.enviNFe_ns.TNFeInfNFeIdeIndFinal.Item0;
                if (OutBoxPar.nfeDoc.nfeIde.indFinal == 1)
                    _nfe.infNFe.ide.indFinal = ValidateLib.enviNFe_ns.TNFeInfNFeIdeIndFinal.Item1;

                if (OutBoxPar.nfeDoc.nfeIde.indPag == 0)
                    _nfe.infNFe.ide.indPag = ValidateLib.enviNFe_ns.TNFeInfNFeIdeIndPag.Item0;
                if (OutBoxPar.nfeDoc.nfeIde.indPag == 1)
                    _nfe.infNFe.ide.indPag = ValidateLib.enviNFe_ns.TNFeInfNFeIdeIndPag.Item1;
                if (OutBoxPar.nfeDoc.nfeIde.indPag == 2)
                    _nfe.infNFe.ide.indPag = ValidateLib.enviNFe_ns.TNFeInfNFeIdeIndPag.Item2;

                if (OutBoxPar.nfeDoc.nfeIde.indPres == 0)
                    _nfe.infNFe.ide.indPres = ValidateLib.enviNFe_ns.TNFeInfNFeIdeIndPres.Item0;
                if (OutBoxPar.nfeDoc.nfeIde.indPres == 1)
                    _nfe.infNFe.ide.indPres = ValidateLib.enviNFe_ns.TNFeInfNFeIdeIndPres.Item1;
                if (OutBoxPar.nfeDoc.nfeIde.indPres == 2)
                    _nfe.infNFe.ide.indPres = ValidateLib.enviNFe_ns.TNFeInfNFeIdeIndPres.Item2;
                if (OutBoxPar.nfeDoc.nfeIde.indPres == 3)
                    _nfe.infNFe.ide.indPres = ValidateLib.enviNFe_ns.TNFeInfNFeIdeIndPres.Item3;
                if (OutBoxPar.nfeDoc.nfeIde.indPres == 4)
                    _nfe.infNFe.ide.indPres = ValidateLib.enviNFe_ns.TNFeInfNFeIdeIndPres.Item4;
                if (OutBoxPar.nfeDoc.nfeIde.indPres == 9)
                    _nfe.infNFe.ide.indPres = ValidateLib.enviNFe_ns.TNFeInfNFeIdeIndPres.Item9;

                if (OutBoxPar.nfeDoc.nfeIde.mod == "55")
                    _nfe.infNFe.ide.mod = ValidateLib.enviNFe_ns.TMod.Item55;
                if (OutBoxPar.nfeDoc.nfeIde.mod == "65")
                    _nfe.infNFe.ide.mod = ValidateLib.enviNFe_ns.TMod.Item65;

                _nfe.infNFe.ide.natOp = OutBoxPar.nfeDoc.nfeIde.natOp;
                _nfe.infNFe.ide.nNF = OutBoxPar.nfeDoc.nfeIde.nNF;

                if (OutBoxPar.nfeDoc.nfeIde.procEmi == 0)
                    _nfe.infNFe.ide.procEmi = ValidateLib.enviNFe_ns.TProcEmi.Item0;
                if (OutBoxPar.nfeDoc.nfeIde.procEmi == 1)
                    _nfe.infNFe.ide.procEmi = ValidateLib.enviNFe_ns.TProcEmi.Item1;
                if (OutBoxPar.nfeDoc.nfeIde.procEmi == 2)
                    _nfe.infNFe.ide.procEmi = ValidateLib.enviNFe_ns.TProcEmi.Item2;
                if (OutBoxPar.nfeDoc.nfeIde.procEmi == 3)
                    _nfe.infNFe.ide.procEmi = ValidateLib.enviNFe_ns.TProcEmi.Item3;

                _nfe.infNFe.ide.serie = OutBoxPar.nfeDoc.nfeIde.serie;

                if (OutBoxPar.nfeDoc.nfeIde.tpAmb == 1)
                    _nfe.infNFe.ide.tpAmb = ValidateLib.enviNFe_ns.TAmb.Item1;
                if (OutBoxPar.nfeDoc.nfeIde.tpAmb == 2)
                    _nfe.infNFe.ide.tpAmb = ValidateLib.enviNFe_ns.TAmb.Item2;

                if (OutBoxPar.nfeDoc.nfeIde.tpEmis == 1)
                    _nfe.infNFe.ide.tpEmis = ValidateLib.enviNFe_ns.TNFeInfNFeIdeTpEmis.Item1;
                if (OutBoxPar.nfeDoc.nfeIde.tpEmis == 2)
                    _nfe.infNFe.ide.tpEmis = ValidateLib.enviNFe_ns.TNFeInfNFeIdeTpEmis.Item2;
                if (OutBoxPar.nfeDoc.nfeIde.tpEmis == 3)
                    _nfe.infNFe.ide.tpEmis = ValidateLib.enviNFe_ns.TNFeInfNFeIdeTpEmis.Item3;
                if (OutBoxPar.nfeDoc.nfeIde.tpEmis == 4)
                    _nfe.infNFe.ide.tpEmis = ValidateLib.enviNFe_ns.TNFeInfNFeIdeTpEmis.Item4;
                if (OutBoxPar.nfeDoc.nfeIde.tpEmis == 5)
                    _nfe.infNFe.ide.tpEmis = ValidateLib.enviNFe_ns.TNFeInfNFeIdeTpEmis.Item5;
                if (OutBoxPar.nfeDoc.nfeIde.tpEmis == 6)
                    _nfe.infNFe.ide.tpEmis = ValidateLib.enviNFe_ns.TNFeInfNFeIdeTpEmis.Item6;
                if (OutBoxPar.nfeDoc.nfeIde.tpEmis == 7)
                    _nfe.infNFe.ide.tpEmis = ValidateLib.enviNFe_ns.TNFeInfNFeIdeTpEmis.Item7;
                if (OutBoxPar.nfeDoc.nfeIde.tpEmis == 9)
                    _nfe.infNFe.ide.tpEmis = ValidateLib.enviNFe_ns.TNFeInfNFeIdeTpEmis.Item9;

                if (OutBoxPar.nfeDoc.nfeIde.tpImp == 0)
                    _nfe.infNFe.ide.tpImp = ValidateLib.enviNFe_ns.TNFeInfNFeIdeTpImp.Item0;
                if (OutBoxPar.nfeDoc.nfeIde.tpImp == 1)
                    _nfe.infNFe.ide.tpImp = ValidateLib.enviNFe_ns.TNFeInfNFeIdeTpImp.Item1;
                if (OutBoxPar.nfeDoc.nfeIde.tpImp == 2)
                    _nfe.infNFe.ide.tpImp = ValidateLib.enviNFe_ns.TNFeInfNFeIdeTpImp.Item2;
                if (OutBoxPar.nfeDoc.nfeIde.tpImp == 3)
                    _nfe.infNFe.ide.tpImp = ValidateLib.enviNFe_ns.TNFeInfNFeIdeTpImp.Item3;
                if (OutBoxPar.nfeDoc.nfeIde.tpImp == 4)
                    _nfe.infNFe.ide.tpImp = ValidateLib.enviNFe_ns.TNFeInfNFeIdeTpImp.Item4;
                if (OutBoxPar.nfeDoc.nfeIde.tpImp == 5)
                    _nfe.infNFe.ide.tpImp = ValidateLib.enviNFe_ns.TNFeInfNFeIdeTpImp.Item5;

                if (OutBoxPar.nfeDoc.nfeIde.tpNF == 0)
                    _nfe.infNFe.ide.tpNF = ValidateLib.enviNFe_ns.TNFeInfNFeIdeTpNF.Item0;
                if (OutBoxPar.nfeDoc.nfeIde.tpNF == 1)
                    _nfe.infNFe.ide.tpNF = ValidateLib.enviNFe_ns.TNFeInfNFeIdeTpNF.Item1;

                _nfe.infNFe.ide.verProc = OutBoxPar.nfeDoc.nfeIde.verProc;
                _nfe.infNFe.ide.xJust = OutBoxPar.nfeDoc.nfeIde.xJust;

                #endregion

                #region NFref

                System.Data.DataTable dtNfeRef = AppLib.Context.poolConnection.Get().ExecQuery("SELECT * FROM ZNFEREF WHERE IDOUTBOX = ?", new object[] { OutBoxPar.IdOutbox }); ;
                if (dtNfeRef.Rows.Count > 0)
                {
                    ValidateLib.enviNFe_ns.TNFeInfNFeIdeNFref[] ChaveRef = new ValidateLib.enviNFe_ns.TNFeInfNFeIdeNFref[] { };

                    for (int i = 0; i < dtNfeRef.Rows.Count; i++)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeIdeNFref NFRef = new ValidateLib.enviNFe_ns.TNFeInfNFeIdeNFref();
                        NFRef.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType1.refNFe;
                        NFRef.Item = dtNfeRef.Rows[i]["CHAVENFEREF"].ToString();
                        ChaveRef = new ValidateLib.enviNFe_ns.TNFeInfNFeIdeNFref[] { NFRef };
                    }
                    _nfe.infNFe.ide.NFref = ChaveRef;
                }

                #endregion

                #region emit

                _nfe.infNFe.emit = new ValidateLib.enviNFe_ns.TNFeInfNFeEmit();
                _nfe.infNFe.emit.CNAE = OutBoxPar.nfeDoc.nfeEmit.CNAE;

                if (OutBoxPar.nfeDoc.nfeEmit.CRT == 1)
                    _nfe.infNFe.emit.CRT = ValidateLib.enviNFe_ns.TNFeInfNFeEmitCRT.Item1;
                if (OutBoxPar.nfeDoc.nfeEmit.CRT == 2)
                    _nfe.infNFe.emit.CRT = ValidateLib.enviNFe_ns.TNFeInfNFeEmitCRT.Item2;
                if (OutBoxPar.nfeDoc.nfeEmit.CRT == 3)
                    _nfe.infNFe.emit.CRT = ValidateLib.enviNFe_ns.TNFeInfNFeEmitCRT.Item3;

                _nfe.infNFe.emit.IE = OutBoxPar.nfeDoc.nfeEmit.IE;
                _nfe.infNFe.emit.IEST = OutBoxPar.nfeDoc.nfeEmit.IEST;
                _nfe.infNFe.emit.IM = OutBoxPar.nfeDoc.nfeEmit.IM;
                _nfe.infNFe.emit.xFant = OutBoxPar.nfeDoc.nfeEmit.xFant;
                _nfe.infNFe.emit.xNome = OutBoxPar.nfeDoc.nfeEmit.xNome;

                if (string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfeEmit.CNPJ))
                {
                    _nfe.infNFe.emit.Item = OutBoxPar.nfeDoc.nfeEmit.CPF;
                    _nfe.infNFe.emit.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType2.CPF;
                }

                if (string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfeEmit.CPF))
                {
                    _nfe.infNFe.emit.Item = OutBoxPar.nfeDoc.nfeEmit.CNPJ;
                    _nfe.infNFe.emit.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType2.CNPJ;
                }

                _nfe.infNFe.emit.enderEmit = new ValidateLib.enviNFe_ns.TEnderEmi();
                _nfe.infNFe.emit.enderEmit.CEP = OutBoxPar.nfeDoc.nfeEmit.CEP;
                _nfe.infNFe.emit.enderEmit.cMun = OutBoxPar.nfeDoc.nfeEmit.cMun;
                _nfe.infNFe.emit.enderEmit.cPais = ValidateLib.enviNFe_ns.TEnderEmiCPais.Item1058;
                _nfe.infNFe.emit.enderEmit.xPais = ValidateLib.enviNFe_ns.TEnderEmiXPais.BRASIL;
                _nfe.infNFe.emit.enderEmit.cPaisSpecified = true;
                _nfe.infNFe.emit.enderEmit.fone = OutBoxPar.nfeDoc.nfeEmit.fone;
                _nfe.infNFe.emit.enderEmit.nro = OutBoxPar.nfeDoc.nfeEmit.nro;

                if (OutBoxPar.nfeDoc.nfeEmit.UF == "AC")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.AC;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "AL")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.AL;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "AM")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.AM;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "AP")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.AP;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "BA")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.BA;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "CE")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.CE;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "DF")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.DF;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "ES")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.ES;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "GO")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.GO;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "MA")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.MA;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "MG")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.MG;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "MS")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.MS;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "MT")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.MT;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "PA")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.PA;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "PB")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.PB;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "PE")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.PE;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "PI")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.PI;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "PR")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.PR;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "RJ")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.RJ;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "RN")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.RN;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "RO")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.RO;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "RR")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.RR;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "RS")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.RS;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "SC")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.SC;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "SE")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.SE;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "SP")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.SP;
                if (OutBoxPar.nfeDoc.nfeEmit.UF == "TO")
                    _nfe.infNFe.emit.enderEmit.UF = ValidateLib.enviNFe_ns.TUfEmi.TO;

                _nfe.infNFe.emit.enderEmit.xBairro = OutBoxPar.nfeDoc.nfeEmit.xBairro;
                _nfe.infNFe.emit.enderEmit.xCpl = OutBoxPar.nfeDoc.nfeEmit.xCpl;
                _nfe.infNFe.emit.enderEmit.xLgr = OutBoxPar.nfeDoc.nfeEmit.xLgr;
                _nfe.infNFe.emit.enderEmit.xMun = OutBoxPar.nfeDoc.nfeEmit.xMun;

                #endregion

                #region dest

                _nfe.infNFe.dest = new ValidateLib.enviNFe_ns.TNFeInfNFeDest();
                _nfe.infNFe.dest.email = OutBoxPar.nfeDoc.nfeDest.email;
                _nfe.infNFe.dest.IE = OutBoxPar.nfeDoc.nfeDest.IE;
                _nfe.infNFe.dest.IM = OutBoxPar.nfeDoc.nfeDest.IM;

                if (OutBoxPar.nfeDoc.nfeDest.indIEDest == 1)
                    _nfe.infNFe.dest.indIEDest = ValidateLib.enviNFe_ns.TNFeInfNFeDestIndIEDest.Item1;
                if (OutBoxPar.nfeDoc.nfeDest.indIEDest == 2)
                    _nfe.infNFe.dest.indIEDest = ValidateLib.enviNFe_ns.TNFeInfNFeDestIndIEDest.Item2;
                if (OutBoxPar.nfeDoc.nfeDest.indIEDest == 9)
                    _nfe.infNFe.dest.indIEDest = ValidateLib.enviNFe_ns.TNFeInfNFeDestIndIEDest.Item9;

                _nfe.infNFe.dest.ISUF = OutBoxPar.nfeDoc.nfeDest.ISUF;

                if (!string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfeDest.idEstrangeiro))
                {
                    _nfe.infNFe.dest.Item = OutBoxPar.nfeDoc.nfeDest.idEstrangeiro;
                    _nfe.infNFe.dest.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType3.idEstrangeiro;
                }
                else if (!string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfeDest.CNPJ))
                {
                    _nfe.infNFe.dest.Item = OutBoxPar.nfeDoc.nfeDest.CNPJ;
                    _nfe.infNFe.dest.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType3.CNPJ;
                }
                else
                {
                    _nfe.infNFe.dest.Item = OutBoxPar.nfeDoc.nfeDest.CPF;
                    _nfe.infNFe.dest.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType3.CPF;
                }
                //if (string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfeDest.CNPJ))
                //{
                //    _nfe.infNFe.dest.Item = OutBoxPar.nfeDoc.nfeDest.CPF;
                //    _nfe.infNFe.dest.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType3.CPF;
                //}
                //else
                //{
                //    if (string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfeDest.CPF))
                //    {
                //        _nfe.infNFe.dest.Item = OutBoxPar.nfeDoc.nfeDest.CNPJ;
                //        _nfe.infNFe.dest.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType3.CNPJ;
                //    }
                //    else
                //    {
                //        if (string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfeDest.idEstrangeiro))
                //        {
                //            _nfe.infNFe.dest.Item = OutBoxPar.nfeDoc.nfeDest.idEstrangeiro;
                //            _nfe.infNFe.dest.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType3.idEstrangeiro;
                //        }
                //    }
                //}

                _nfe.infNFe.dest.xNome = (OutBoxPar.nfeDoc.nfeIde.tpAmb == 1) ? OutBoxPar.nfeDoc.nfeDest.xNome : "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";

                _nfe.infNFe.dest.enderDest = new ValidateLib.enviNFe_ns.TEndereco();
                _nfe.infNFe.dest.enderDest.CEP = OutBoxPar.nfeDoc.nfeDest.CEP;
                _nfe.infNFe.dest.enderDest.cMun = OutBoxPar.nfeDoc.nfeDest.cMun;
                switch (OutBoxPar.nfeDoc.nfeDest.xPais)
                {
                    case "Brasil":
                        _nfe.infNFe.dest.enderDest.cPais = ValidateLib.enviNFe_ns.Tpais.Item1058;
                        break;
                    case "Paraguay":
                        _nfe.infNFe.dest.enderDest.cPais = ValidateLib.enviNFe_ns.Tpais.Item5860;
                        break;
                    default:
                        break;
                }
                _nfe.infNFe.dest.enderDest.xPais = OutBoxPar.nfeDoc.nfeDest.xPais;
                _nfe.infNFe.dest.enderDest.cPaisSpecified = true;
                _nfe.infNFe.dest.enderDest.fone = OutBoxPar.nfeDoc.nfeDest.fone;
                _nfe.infNFe.dest.enderDest.nro = OutBoxPar.nfeDoc.nfeDest.nro;

                if (OutBoxPar.nfeDoc.nfeDest.UF == "AC")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.AL;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "AL")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.AL;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "AM")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.AM;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "AP")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.AP;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "BA")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.BA;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "CE")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.CE;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "DF")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.DF;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "ES")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.ES;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "GO")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.GO;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "MA")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.MA;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "MG")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.MG;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "MS")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.MS;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "MT")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.MT;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "PA")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.PA;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "PB")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.PB;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "PE")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.PE;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "PI")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.PI;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "PR")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.PR;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "RJ")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.RJ;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "RN")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.RN;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "RO")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.RO;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "RR")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.RR;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "RS")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.RS;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "SC")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.SC;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "SE")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.SE;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "SP")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.SP;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "TO")
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.TO;
                if (OutBoxPar.nfeDoc.nfeDest.UF == "EX")
                {
                    _nfe.infNFe.dest.enderDest.UF = ValidateLib.enviNFe_ns.TUf.EX;
                }

                _nfe.infNFe.dest.enderDest.xBairro = OutBoxPar.nfeDoc.nfeDest.xBairro;
                _nfe.infNFe.dest.enderDest.xCpl = OutBoxPar.nfeDoc.nfeDest.xCpl;
                _nfe.infNFe.dest.enderDest.xLgr = OutBoxPar.nfeDoc.nfeDest.xLgr;
                _nfe.infNFe.dest.enderDest.xMun = OutBoxPar.nfeDoc.nfeDest.xMun;
                _nfe.infNFe.dest.enderDest.xPais = OutBoxPar.nfeDoc.nfeDest.xPais;

                #endregion

                //Colocar tags da entrega

                #region retirada
                if (OutBoxPar.nfeDoc.nfeRetirada != null)
                {
                    if (OutBoxPar.nfeDoc.nfeRetirada.IdOutbox > 0)
                    {
                        _nfe.infNFe.retirada = new ValidateLib.enviNFe_ns.TLocal();
                        _nfe.infNFe.retirada.cMun = OutBoxPar.nfeDoc.nfeRetirada.cMun;

                        if (string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfeRetirada.CNPJ))
                        {
                            _nfe.infNFe.retirada.Item = OutBoxPar.nfeDoc.nfeRetirada.CPF;
                            _nfe.infNFe.retirada.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType4.CPF;
                        }

                        if (string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfeRetirada.CPF))
                        {
                            _nfe.infNFe.retirada.Item = OutBoxPar.nfeDoc.nfeRetirada.CNPJ;
                            _nfe.infNFe.retirada.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType4.CNPJ;
                        }

                        _nfe.infNFe.retirada.nro = OutBoxPar.nfeDoc.nfeRetirada.nro;

                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "AC")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.AL;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "AL")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.AL;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "AM")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.AM;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "AP")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.AP;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "BA")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.BA;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "CE")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.CE;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "DF")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.DF;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "ES")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.ES;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "GO")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.GO;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "MA")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.MA;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "MG")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.MG;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "MS")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.MS;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "MT")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.MT;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "PA")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.PA;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "PB")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.PB;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "PE")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.PE;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "PI")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.PI;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "PR")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.PR;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "RJ")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.RJ;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "RN")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.RN;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "RO")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.RO;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "RR")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.RR;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "RS")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.RS;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "SC")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.SC;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "SE")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.SE;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "SP")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.SP;
                        if (OutBoxPar.nfeDoc.nfeRetirada.UF == "TO")
                            _nfe.infNFe.retirada.UF = ValidateLib.enviNFe_ns.TUf.TO;

                        _nfe.infNFe.retirada.xBairro = OutBoxPar.nfeDoc.nfeRetirada.xBairro;
                        _nfe.infNFe.retirada.xCpl = OutBoxPar.nfeDoc.nfeRetirada.xCpl;
                        _nfe.infNFe.retirada.xLgr = OutBoxPar.nfeDoc.nfeRetirada.xLgr;
                        _nfe.infNFe.retirada.xMun = OutBoxPar.nfeDoc.nfeRetirada.xMun;
                    }
                }
                #endregion

                #region entrega

                if (OutBoxPar.nfeDoc.nfeEntrega != null)
                {
                    if (OutBoxPar.nfeDoc.nfeEntrega.IdOutbox > 0)
                    {
                        _nfe.infNFe.entrega = new ValidateLib.enviNFe_ns.TLocal();
                        _nfe.infNFe.entrega.cMun = OutBoxPar.nfeDoc.nfeEntrega.cMun;

                        if (string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfeEntrega.CNPJ))
                        {
                            _nfe.infNFe.entrega.Item = OutBoxPar.nfeDoc.nfeEntrega.CPF;
                            _nfe.infNFe.entrega.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType4.CPF;
                        }

                        if (string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfeEntrega.CPF))
                        {
                            _nfe.infNFe.entrega.Item = OutBoxPar.nfeDoc.nfeEntrega.CNPJ;
                            _nfe.infNFe.entrega.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType4.CNPJ;
                        }

                        _nfe.infNFe.entrega.nro = OutBoxPar.nfeDoc.nfeEntrega.nro;

                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "AC")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.AL;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "AL")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.AL;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "AM")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.AM;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "AP")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.AP;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "BA")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.BA;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "CE")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.CE;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "DF")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.DF;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "ES")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.ES;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "GO")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.GO;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "MA")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.MA;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "MG")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.MG;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "MS")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.MS;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "MT")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.MT;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "PA")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.PA;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "PB")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.PB;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "PE")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.PE;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "PI")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.PI;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "PR")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.PR;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "RJ")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.RJ;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "RN")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.RN;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "RO")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.RO;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "RR")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.RR;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "RS")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.RS;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "SC")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.SC;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "SE")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.SE;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "SP")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.SP;
                        if (OutBoxPar.nfeDoc.nfeEntrega.UF == "TO")
                            _nfe.infNFe.entrega.UF = ValidateLib.enviNFe_ns.TUf.TO;

                        _nfe.infNFe.entrega.xBairro = OutBoxPar.nfeDoc.nfeEntrega.xBairro;
                        _nfe.infNFe.entrega.xCpl = OutBoxPar.nfeDoc.nfeEntrega.xCpl;
                        _nfe.infNFe.entrega.xLgr = OutBoxPar.nfeDoc.nfeEntrega.xLgr;
                        _nfe.infNFe.entrega.xMun = OutBoxPar.nfeDoc.nfeEntrega.xMun;
                    }
                }
                #endregion

                #region autXML

                /*
                 * ANALISAR
                 * 
                 * 
                _nfe.infNFe.autXML = new ValidateLib.enviNFe_ns.TNFeInfNFeAutXML[1];
                ValidateLib.enviNFe_ns.TNFeInfNFeAutXML tNFeInfNFeAutXML = new ValidateLib.enviNFe_ns.TNFeInfNFeAutXML();

                System.Data.DataTable dtAutXml = AppLib.Context.poolConnection.Get().ExecQuery("SELECT * FROM ZNFEAUTXML", new object[] { });

                if (dtAutXml.Rows.Count > 0)
                {
                    if (dtAutXml.Rows[0]["TIPO"].Equals("CNPJ"))
                    {
                        tNFeInfNFeAutXML.Item = dtAutXml.Rows[0]["CNPJCPF"].ToString();
                        tNFeInfNFeAutXML.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType5.CNPJ;
                    }
                    else
                    {
                        tNFeInfNFeAutXML.Item = dtAutXml.Rows[0]["CNPJCPF"].ToString();
                        tNFeInfNFeAutXML.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType5.CPF;
                    }
                }
                _nfe.infNFe.autXML[0] = tNFeInfNFeAutXML;
                */


                //_nfe.infNFe.autXML = new ValidateLib.enviNFe_ns.TNFeInfNFeAutXML[1];

                //ValidateLib.enviNFe_ns.TNFeInfNFeAutXML tNFeInfNFeAutXML = new ValidateLib.enviNFe_ns.TNFeInfNFeAutXML();
                //if (string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfeDest.CNPJ))
                //{
                //    tNFeInfNFeAutXML.Item = OutBoxPar.nfeDoc.nfeDest.CPF;
                //    tNFeInfNFeAutXML.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType5.CPF;

                //}

                //if (string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfeDest.CPF))
                //{
                //    tNFeInfNFeAutXML.Item = OutBoxPar.nfeDoc.nfeDest.CNPJ;
                //    tNFeInfNFeAutXML.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType5.CNPJ;
                //}
                //_nfe.infNFe.autXML[0] = tNFeInfNFeAutXML;


                #endregion

                #region det

                _nfe.infNFe.det = new ValidateLib.enviNFe_ns.TNFeInfNFeDet[OutBoxPar.nfeDoc.nfeDet.Count];
                int item = 0;
                foreach (NFeDet nfeDet in OutBoxPar.nfeDoc.nfeDet)
                {
                    ValidateLib.enviNFe_ns.TNFeInfNFeDet tNFeInfNFeDet = new ValidateLib.enviNFe_ns.TNFeInfNFeDet();
                    _nfe.infNFe.det[item] = tNFeInfNFeDet;

                    _nfe.infNFe.det[item].nItem = nfeDet.nItem.ToString();

                    #region prod

                    _nfe.infNFe.det[item].prod = new ValidateLib.enviNFe_ns.TNFeInfNFeDetProd();

                    if (!string.IsNullOrEmpty(nfeDet.nfeProd.cEAN))
                        _nfe.infNFe.det[item].prod.cEAN = nfeDet.nfeProd.cEAN;
                    else
                        _nfe.infNFe.det[item].prod.cEAN = string.Empty;

                    if (!string.IsNullOrEmpty(nfeDet.nfeProd.cEANTrib))
                        _nfe.infNFe.det[item].prod.cEANTrib = nfeDet.nfeProd.cEANTrib;
                    else
                        _nfe.infNFe.det[item].prod.cEANTrib = string.Empty;

                    #region CFOP

                    if (nfeDet.nfeProd.CFOP == 1101)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1101;
                    if (nfeDet.nfeProd.CFOP == 1102)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1102;
                    if (nfeDet.nfeProd.CFOP == 1111)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1111;
                    if (nfeDet.nfeProd.CFOP == 1113)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1113;
                    if (nfeDet.nfeProd.CFOP == 1116)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1116;
                    if (nfeDet.nfeProd.CFOP == 1117)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1117;
                    if (nfeDet.nfeProd.CFOP == 1118)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1118;
                    if (nfeDet.nfeProd.CFOP == 1120)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1120;
                    if (nfeDet.nfeProd.CFOP == 1121)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1121;
                    if (nfeDet.nfeProd.CFOP == 1122)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1122;
                    if (nfeDet.nfeProd.CFOP == 1124)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1124;
                    if (nfeDet.nfeProd.CFOP == 1125)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1125;
                    if (nfeDet.nfeProd.CFOP == 1126)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1126;
                    if (nfeDet.nfeProd.CFOP == 1128)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1128;
                    if (nfeDet.nfeProd.CFOP == 1151)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1151;
                    if (nfeDet.nfeProd.CFOP == 1152)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1152;
                    if (nfeDet.nfeProd.CFOP == 1153)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1153;
                    if (nfeDet.nfeProd.CFOP == 1154)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1154;
                    if (nfeDet.nfeProd.CFOP == 1201)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1201;
                    if (nfeDet.nfeProd.CFOP == 1202)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1202;
                    if (nfeDet.nfeProd.CFOP == 1203)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1203;
                    if (nfeDet.nfeProd.CFOP == 1204)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1204;
                    if (nfeDet.nfeProd.CFOP == 1205)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1205;
                    if (nfeDet.nfeProd.CFOP == 1206)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1206;
                    if (nfeDet.nfeProd.CFOP == 1207)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1207;
                    if (nfeDet.nfeProd.CFOP == 1208)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1208;
                    if (nfeDet.nfeProd.CFOP == 1209)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1209;
                    if (nfeDet.nfeProd.CFOP == 1251)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1251;
                    if (nfeDet.nfeProd.CFOP == 1252)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1252;
                    if (nfeDet.nfeProd.CFOP == 1253)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1253;
                    if (nfeDet.nfeProd.CFOP == 1254)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1254;
                    if (nfeDet.nfeProd.CFOP == 1255)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1255;
                    if (nfeDet.nfeProd.CFOP == 1256)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1256;
                    if (nfeDet.nfeProd.CFOP == 1257)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1257;
                    if (nfeDet.nfeProd.CFOP == 1301)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1301;
                    if (nfeDet.nfeProd.CFOP == 1302)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1302;
                    if (nfeDet.nfeProd.CFOP == 1303)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1303;
                    if (nfeDet.nfeProd.CFOP == 1304)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1304;
                    if (nfeDet.nfeProd.CFOP == 1305)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1305;
                    if (nfeDet.nfeProd.CFOP == 1306)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1306;
                    if (nfeDet.nfeProd.CFOP == 1351)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1351;
                    if (nfeDet.nfeProd.CFOP == 1352)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1352;
                    if (nfeDet.nfeProd.CFOP == 1353)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1353;
                    if (nfeDet.nfeProd.CFOP == 1354)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1354;
                    if (nfeDet.nfeProd.CFOP == 1355)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1355;
                    if (nfeDet.nfeProd.CFOP == 1356)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1356;
                    if (nfeDet.nfeProd.CFOP == 1360)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1360;
                    if (nfeDet.nfeProd.CFOP == 1401)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1401;
                    if (nfeDet.nfeProd.CFOP == 1403)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1403;
                    if (nfeDet.nfeProd.CFOP == 1406)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1406;
                    if (nfeDet.nfeProd.CFOP == 1407)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1407;
                    if (nfeDet.nfeProd.CFOP == 1408)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1408;
                    if (nfeDet.nfeProd.CFOP == 1409)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1409;
                    if (nfeDet.nfeProd.CFOP == 1410)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1410;
                    if (nfeDet.nfeProd.CFOP == 1411)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1411;
                    if (nfeDet.nfeProd.CFOP == 1414)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1414;
                    if (nfeDet.nfeProd.CFOP == 1415)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1415;
                    if (nfeDet.nfeProd.CFOP == 1451)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1451;
                    if (nfeDet.nfeProd.CFOP == 1452)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1452;
                    if (nfeDet.nfeProd.CFOP == 1501)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1501;
                    if (nfeDet.nfeProd.CFOP == 1503)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1503;
                    if (nfeDet.nfeProd.CFOP == 1504)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1504;
                    if (nfeDet.nfeProd.CFOP == 1505)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1505;
                    if (nfeDet.nfeProd.CFOP == 1506)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1506;
                    if (nfeDet.nfeProd.CFOP == 1551)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1551;
                    if (nfeDet.nfeProd.CFOP == 1552)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1552;
                    if (nfeDet.nfeProd.CFOP == 1553)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1553;
                    if (nfeDet.nfeProd.CFOP == 1554)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1554;
                    if (nfeDet.nfeProd.CFOP == 1555)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1555;
                    if (nfeDet.nfeProd.CFOP == 1556)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1556;
                    if (nfeDet.nfeProd.CFOP == 1557)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1557;
                    if (nfeDet.nfeProd.CFOP == 1601)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1601;
                    if (nfeDet.nfeProd.CFOP == 1602)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1602;
                    if (nfeDet.nfeProd.CFOP == 1603)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1603;
                    if (nfeDet.nfeProd.CFOP == 1604)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1604;
                    if (nfeDet.nfeProd.CFOP == 1605)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1605;
                    if (nfeDet.nfeProd.CFOP == 1651)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1651;
                    if (nfeDet.nfeProd.CFOP == 1652)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1652;
                    if (nfeDet.nfeProd.CFOP == 1653)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1653;
                    if (nfeDet.nfeProd.CFOP == 1658)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1658;
                    if (nfeDet.nfeProd.CFOP == 1659)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1659;
                    if (nfeDet.nfeProd.CFOP == 1660)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1660;
                    if (nfeDet.nfeProd.CFOP == 1661)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1661;
                    if (nfeDet.nfeProd.CFOP == 1662)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1662;
                    if (nfeDet.nfeProd.CFOP == 1663)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1663;
                    if (nfeDet.nfeProd.CFOP == 1664)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1664;
                    if (nfeDet.nfeProd.CFOP == 1901)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1901;
                    if (nfeDet.nfeProd.CFOP == 1902)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1902;
                    if (nfeDet.nfeProd.CFOP == 1903)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1903;
                    if (nfeDet.nfeProd.CFOP == 1904)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1904;
                    if (nfeDet.nfeProd.CFOP == 1905)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1905;
                    if (nfeDet.nfeProd.CFOP == 1906)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1906;
                    if (nfeDet.nfeProd.CFOP == 1907)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1907;
                    if (nfeDet.nfeProd.CFOP == 1908)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1908;
                    if (nfeDet.nfeProd.CFOP == 1909)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1909;
                    if (nfeDet.nfeProd.CFOP == 1910)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1910;
                    if (nfeDet.nfeProd.CFOP == 1911)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1911;
                    if (nfeDet.nfeProd.CFOP == 1912)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1912;
                    if (nfeDet.nfeProd.CFOP == 1913)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1913;
                    if (nfeDet.nfeProd.CFOP == 1914)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1914;
                    if (nfeDet.nfeProd.CFOP == 1915)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1915;
                    if (nfeDet.nfeProd.CFOP == 1916)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1916;
                    if (nfeDet.nfeProd.CFOP == 1917)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1917;
                    if (nfeDet.nfeProd.CFOP == 1918)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1918;
                    if (nfeDet.nfeProd.CFOP == 1919)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1919;
                    if (nfeDet.nfeProd.CFOP == 1920)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1920;
                    if (nfeDet.nfeProd.CFOP == 1921)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1921;
                    if (nfeDet.nfeProd.CFOP == 1922)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1922;
                    if (nfeDet.nfeProd.CFOP == 1923)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1923;
                    if (nfeDet.nfeProd.CFOP == 1924)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1924;
                    if (nfeDet.nfeProd.CFOP == 1925)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1925;
                    if (nfeDet.nfeProd.CFOP == 1926)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1926;
                    if (nfeDet.nfeProd.CFOP == 1931)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1931;
                    if (nfeDet.nfeProd.CFOP == 1932)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1932;
                    if (nfeDet.nfeProd.CFOP == 1933)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1933;
                    if (nfeDet.nfeProd.CFOP == 1934)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1934;
                    if (nfeDet.nfeProd.CFOP == 1949)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item1949;
                    if (nfeDet.nfeProd.CFOP == 2101)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2101;
                    if (nfeDet.nfeProd.CFOP == 2102)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2102;
                    if (nfeDet.nfeProd.CFOP == 2111)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2111;
                    if (nfeDet.nfeProd.CFOP == 2113)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2113;
                    if (nfeDet.nfeProd.CFOP == 2116)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2116;
                    if (nfeDet.nfeProd.CFOP == 2117)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2117;
                    if (nfeDet.nfeProd.CFOP == 2118)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2118;
                    if (nfeDet.nfeProd.CFOP == 2120)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2120;
                    if (nfeDet.nfeProd.CFOP == 2121)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2121;
                    if (nfeDet.nfeProd.CFOP == 2122)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2122;
                    if (nfeDet.nfeProd.CFOP == 2124)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2124;
                    if (nfeDet.nfeProd.CFOP == 2125)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2125;
                    if (nfeDet.nfeProd.CFOP == 2126)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2126;
                    if (nfeDet.nfeProd.CFOP == 2128)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2128;
                    if (nfeDet.nfeProd.CFOP == 2151)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2151;
                    if (nfeDet.nfeProd.CFOP == 2152)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2152;
                    if (nfeDet.nfeProd.CFOP == 2153)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2153;
                    if (nfeDet.nfeProd.CFOP == 2154)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2154;
                    if (nfeDet.nfeProd.CFOP == 2201)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2201;
                    if (nfeDet.nfeProd.CFOP == 2202)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2202;
                    if (nfeDet.nfeProd.CFOP == 2203)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2203;
                    if (nfeDet.nfeProd.CFOP == 2204)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2204;
                    if (nfeDet.nfeProd.CFOP == 2205)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2205;
                    if (nfeDet.nfeProd.CFOP == 2206)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2206;
                    if (nfeDet.nfeProd.CFOP == 2207)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2207;
                    if (nfeDet.nfeProd.CFOP == 2208)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2208;
                    if (nfeDet.nfeProd.CFOP == 2209)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2209;
                    if (nfeDet.nfeProd.CFOP == 2251)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2251;
                    if (nfeDet.nfeProd.CFOP == 2252)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2252;
                    if (nfeDet.nfeProd.CFOP == 2253)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2253;
                    if (nfeDet.nfeProd.CFOP == 2254)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2254;
                    if (nfeDet.nfeProd.CFOP == 2255)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2255;
                    if (nfeDet.nfeProd.CFOP == 2256)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2256;
                    if (nfeDet.nfeProd.CFOP == 2257)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2257;
                    if (nfeDet.nfeProd.CFOP == 2301)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2301;
                    if (nfeDet.nfeProd.CFOP == 2302)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2302;
                    if (nfeDet.nfeProd.CFOP == 2303)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2303;
                    if (nfeDet.nfeProd.CFOP == 2304)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2304;
                    if (nfeDet.nfeProd.CFOP == 2305)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2305;
                    if (nfeDet.nfeProd.CFOP == 2306)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2306;
                    if (nfeDet.nfeProd.CFOP == 2351)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2351;
                    if (nfeDet.nfeProd.CFOP == 2352)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2352;
                    if (nfeDet.nfeProd.CFOP == 2353)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2353;
                    if (nfeDet.nfeProd.CFOP == 2354)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2354;
                    if (nfeDet.nfeProd.CFOP == 2355)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2355;
                    if (nfeDet.nfeProd.CFOP == 2356)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2356;
                    if (nfeDet.nfeProd.CFOP == 2401)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2401;
                    if (nfeDet.nfeProd.CFOP == 2403)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2403;
                    if (nfeDet.nfeProd.CFOP == 2406)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2406;
                    if (nfeDet.nfeProd.CFOP == 2407)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2407;
                    if (nfeDet.nfeProd.CFOP == 2408)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2408;
                    if (nfeDet.nfeProd.CFOP == 2409)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2409;
                    if (nfeDet.nfeProd.CFOP == 2410)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2410;
                    if (nfeDet.nfeProd.CFOP == 2411)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2411;
                    if (nfeDet.nfeProd.CFOP == 2414)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2414;
                    if (nfeDet.nfeProd.CFOP == 2415)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2415;
                    if (nfeDet.nfeProd.CFOP == 2501)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2501;
                    if (nfeDet.nfeProd.CFOP == 2503)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2503;
                    if (nfeDet.nfeProd.CFOP == 2504)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2504;
                    if (nfeDet.nfeProd.CFOP == 2505)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2505;
                    if (nfeDet.nfeProd.CFOP == 2506)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2506;
                    if (nfeDet.nfeProd.CFOP == 2551)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2551;
                    if (nfeDet.nfeProd.CFOP == 2552)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2552;
                    if (nfeDet.nfeProd.CFOP == 2553)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2553;
                    if (nfeDet.nfeProd.CFOP == 2554)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2554;
                    if (nfeDet.nfeProd.CFOP == 2555)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2555;
                    if (nfeDet.nfeProd.CFOP == 2556)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2556;
                    if (nfeDet.nfeProd.CFOP == 2557)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2557;
                    if (nfeDet.nfeProd.CFOP == 2603)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2603;
                    if (nfeDet.nfeProd.CFOP == 2651)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2651;
                    if (nfeDet.nfeProd.CFOP == 2652)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2652;
                    if (nfeDet.nfeProd.CFOP == 2653)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2653;
                    if (nfeDet.nfeProd.CFOP == 2658)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2658;
                    if (nfeDet.nfeProd.CFOP == 2659)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2659;
                    if (nfeDet.nfeProd.CFOP == 2660)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2660;
                    if (nfeDet.nfeProd.CFOP == 2661)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2661;
                    if (nfeDet.nfeProd.CFOP == 2662)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2662;
                    if (nfeDet.nfeProd.CFOP == 2663)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2663;
                    if (nfeDet.nfeProd.CFOP == 2664)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2664;
                    if (nfeDet.nfeProd.CFOP == 2901)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2901;
                    if (nfeDet.nfeProd.CFOP == 2902)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2902;
                    if (nfeDet.nfeProd.CFOP == 2903)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2903;
                    if (nfeDet.nfeProd.CFOP == 2904)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2904;
                    if (nfeDet.nfeProd.CFOP == 2905)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2905;
                    if (nfeDet.nfeProd.CFOP == 2906)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2906;
                    if (nfeDet.nfeProd.CFOP == 2907)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2907;
                    if (nfeDet.nfeProd.CFOP == 2908)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2908;
                    if (nfeDet.nfeProd.CFOP == 2909)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2909;
                    if (nfeDet.nfeProd.CFOP == 2910)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2910;
                    if (nfeDet.nfeProd.CFOP == 2911)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2911;
                    if (nfeDet.nfeProd.CFOP == 2912)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2912;
                    if (nfeDet.nfeProd.CFOP == 2913)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2913;
                    if (nfeDet.nfeProd.CFOP == 2914)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2914;
                    if (nfeDet.nfeProd.CFOP == 2915)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2915;
                    if (nfeDet.nfeProd.CFOP == 2916)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2916;
                    if (nfeDet.nfeProd.CFOP == 2917)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2917;
                    if (nfeDet.nfeProd.CFOP == 2918)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2918;
                    if (nfeDet.nfeProd.CFOP == 2919)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2919;
                    if (nfeDet.nfeProd.CFOP == 2920)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2920;
                    if (nfeDet.nfeProd.CFOP == 2921)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2921;
                    if (nfeDet.nfeProd.CFOP == 2922)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2922;
                    if (nfeDet.nfeProd.CFOP == 2923)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2923;
                    if (nfeDet.nfeProd.CFOP == 2924)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2924;
                    if (nfeDet.nfeProd.CFOP == 2925)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2925;
                    if (nfeDet.nfeProd.CFOP == 2931)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2931;
                    if (nfeDet.nfeProd.CFOP == 2932)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2932;
                    if (nfeDet.nfeProd.CFOP == 2933)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2933;
                    if (nfeDet.nfeProd.CFOP == 2934)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2934;
                    if (nfeDet.nfeProd.CFOP == 2949)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item2949;
                    if (nfeDet.nfeProd.CFOP == 3101)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3101;
                    if (nfeDet.nfeProd.CFOP == 3102)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3102;
                    if (nfeDet.nfeProd.CFOP == 3126)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3126;
                    if (nfeDet.nfeProd.CFOP == 3127)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3127;
                    if (nfeDet.nfeProd.CFOP == 3128)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3128;
                    if (nfeDet.nfeProd.CFOP == 3201)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3201;
                    if (nfeDet.nfeProd.CFOP == 3202)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3202;
                    if (nfeDet.nfeProd.CFOP == 3205)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3205;
                    if (nfeDet.nfeProd.CFOP == 3206)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3206;
                    if (nfeDet.nfeProd.CFOP == 3207)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3207;
                    if (nfeDet.nfeProd.CFOP == 3211)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3211;
                    if (nfeDet.nfeProd.CFOP == 3251)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3251;
                    if (nfeDet.nfeProd.CFOP == 3301)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3301;
                    if (nfeDet.nfeProd.CFOP == 3351)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3351;
                    if (nfeDet.nfeProd.CFOP == 3352)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3352;
                    if (nfeDet.nfeProd.CFOP == 3353)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3353;
                    if (nfeDet.nfeProd.CFOP == 3354)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3354;
                    if (nfeDet.nfeProd.CFOP == 3355)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3355;
                    if (nfeDet.nfeProd.CFOP == 3356)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3356;
                    if (nfeDet.nfeProd.CFOP == 3503)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3503;
                    if (nfeDet.nfeProd.CFOP == 3551)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3551;
                    if (nfeDet.nfeProd.CFOP == 3553)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3553;
                    if (nfeDet.nfeProd.CFOP == 3556)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3556;
                    if (nfeDet.nfeProd.CFOP == 3651)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3651;
                    if (nfeDet.nfeProd.CFOP == 3652)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3652;
                    if (nfeDet.nfeProd.CFOP == 3653)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3653;
                    if (nfeDet.nfeProd.CFOP == 3930)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3930;
                    if (nfeDet.nfeProd.CFOP == 3949)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item3949;
                    if (nfeDet.nfeProd.CFOP == 5101)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5101;
                    if (nfeDet.nfeProd.CFOP == 5102)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5102;
                    if (nfeDet.nfeProd.CFOP == 5103)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5103;
                    if (nfeDet.nfeProd.CFOP == 5104)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5104;
                    if (nfeDet.nfeProd.CFOP == 5105)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5105;
                    if (nfeDet.nfeProd.CFOP == 5106)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5106;
                    if (nfeDet.nfeProd.CFOP == 5109)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5109;
                    if (nfeDet.nfeProd.CFOP == 5110)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5110;
                    if (nfeDet.nfeProd.CFOP == 5111)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5111;
                    if (nfeDet.nfeProd.CFOP == 5112)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5112;
                    if (nfeDet.nfeProd.CFOP == 5113)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5113;
                    if (nfeDet.nfeProd.CFOP == 5114)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5114;
                    if (nfeDet.nfeProd.CFOP == 5115)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5115;
                    if (nfeDet.nfeProd.CFOP == 5116)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5116;
                    if (nfeDet.nfeProd.CFOP == 5117)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5117;
                    if (nfeDet.nfeProd.CFOP == 5118)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5118;
                    if (nfeDet.nfeProd.CFOP == 5119)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5119;
                    if (nfeDet.nfeProd.CFOP == 5120)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5120;
                    if (nfeDet.nfeProd.CFOP == 5122)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5122;
                    if (nfeDet.nfeProd.CFOP == 5123)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5123;
                    if (nfeDet.nfeProd.CFOP == 5124)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5124;
                    if (nfeDet.nfeProd.CFOP == 5125)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5125;
                    if (nfeDet.nfeProd.CFOP == 5151)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5151;
                    if (nfeDet.nfeProd.CFOP == 5152)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5152;
                    if (nfeDet.nfeProd.CFOP == 5153)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5153;
                    if (nfeDet.nfeProd.CFOP == 5155)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5155;
                    if (nfeDet.nfeProd.CFOP == 5156)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5156;
                    if (nfeDet.nfeProd.CFOP == 5201)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5201;
                    if (nfeDet.nfeProd.CFOP == 5202)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5202;
                    if (nfeDet.nfeProd.CFOP == 5205)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5205;
                    if (nfeDet.nfeProd.CFOP == 5206)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5206;
                    if (nfeDet.nfeProd.CFOP == 5207)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5207;
                    if (nfeDet.nfeProd.CFOP == 5208)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5208;
                    if (nfeDet.nfeProd.CFOP == 5209)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5209;
                    if (nfeDet.nfeProd.CFOP == 5210)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5210;
                    if (nfeDet.nfeProd.CFOP == 5251)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5251;
                    if (nfeDet.nfeProd.CFOP == 5252)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5252;
                    if (nfeDet.nfeProd.CFOP == 5253)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5253;
                    if (nfeDet.nfeProd.CFOP == 5254)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5254;
                    if (nfeDet.nfeProd.CFOP == 5255)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5255;
                    if (nfeDet.nfeProd.CFOP == 5256)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5256;
                    if (nfeDet.nfeProd.CFOP == 5257)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5257;
                    if (nfeDet.nfeProd.CFOP == 5258)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5258;
                    if (nfeDet.nfeProd.CFOP == 5401)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5401;
                    if (nfeDet.nfeProd.CFOP == 5402)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5402;
                    if (nfeDet.nfeProd.CFOP == 5403)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5403;
                    if (nfeDet.nfeProd.CFOP == 5405)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5405;
                    if (nfeDet.nfeProd.CFOP == 5408)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5408;
                    if (nfeDet.nfeProd.CFOP == 5409)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5409;
                    if (nfeDet.nfeProd.CFOP == 5410)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5410;
                    if (nfeDet.nfeProd.CFOP == 5411)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5411;
                    if (nfeDet.nfeProd.CFOP == 5412)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5412;
                    if (nfeDet.nfeProd.CFOP == 5413)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5413;
                    if (nfeDet.nfeProd.CFOP == 5414)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5414;
                    if (nfeDet.nfeProd.CFOP == 5415)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5415;
                    if (nfeDet.nfeProd.CFOP == 5451)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5451;
                    if (nfeDet.nfeProd.CFOP == 5501)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5501;
                    if (nfeDet.nfeProd.CFOP == 5502)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5502;
                    if (nfeDet.nfeProd.CFOP == 5503)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5503;
                    if (nfeDet.nfeProd.CFOP == 5504)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5504;
                    if (nfeDet.nfeProd.CFOP == 5505)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5505;
                    if (nfeDet.nfeProd.CFOP == 5551)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5551;
                    if (nfeDet.nfeProd.CFOP == 5552)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5552;
                    if (nfeDet.nfeProd.CFOP == 5553)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5553;
                    if (nfeDet.nfeProd.CFOP == 5554)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5554;
                    if (nfeDet.nfeProd.CFOP == 5555)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5555;
                    if (nfeDet.nfeProd.CFOP == 5556)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5556;
                    if (nfeDet.nfeProd.CFOP == 55571)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5557;
                    if (nfeDet.nfeProd.CFOP == 5601)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5601;
                    if (nfeDet.nfeProd.CFOP == 5602)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5602;
                    if (nfeDet.nfeProd.CFOP == 5603)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5603;
                    if (nfeDet.nfeProd.CFOP == 5605)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5605;
                    if (nfeDet.nfeProd.CFOP == 5606)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5606;
                    if (nfeDet.nfeProd.CFOP == 5651)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5651;
                    if (nfeDet.nfeProd.CFOP == 5652)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5652;
                    if (nfeDet.nfeProd.CFOP == 5653)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5653;
                    if (nfeDet.nfeProd.CFOP == 5654)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5654;
                    if (nfeDet.nfeProd.CFOP == 5655)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5655;
                    if (nfeDet.nfeProd.CFOP == 5656)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5656;
                    if (nfeDet.nfeProd.CFOP == 5657)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5657;
                    if (nfeDet.nfeProd.CFOP == 5658)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5658;
                    if (nfeDet.nfeProd.CFOP == 5659)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5659;
                    if (nfeDet.nfeProd.CFOP == 5660)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5660;
                    if (nfeDet.nfeProd.CFOP == 5661)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5661;
                    if (nfeDet.nfeProd.CFOP == 5662)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5662;
                    if (nfeDet.nfeProd.CFOP == 5663)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5663;
                    if (nfeDet.nfeProd.CFOP == 5664)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5664;
                    if (nfeDet.nfeProd.CFOP == 5665)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5665;
                    if (nfeDet.nfeProd.CFOP == 5666)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5666;
                    if (nfeDet.nfeProd.CFOP == 5667)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5667;
                    if (nfeDet.nfeProd.CFOP == 5901)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5901;
                    if (nfeDet.nfeProd.CFOP == 5902)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5902;
                    if (nfeDet.nfeProd.CFOP == 5903)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5903;
                    if (nfeDet.nfeProd.CFOP == 5904)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5904;
                    if (nfeDet.nfeProd.CFOP == 5905)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5905;
                    if (nfeDet.nfeProd.CFOP == 5906)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5906;
                    if (nfeDet.nfeProd.CFOP == 5907)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5907;
                    if (nfeDet.nfeProd.CFOP == 5908)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5908;
                    if (nfeDet.nfeProd.CFOP == 5909)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5909;
                    if (nfeDet.nfeProd.CFOP == 5910)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5910;
                    if (nfeDet.nfeProd.CFOP == 5911)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5911;
                    if (nfeDet.nfeProd.CFOP == 5912)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5912;
                    if (nfeDet.nfeProd.CFOP == 5913)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5913;
                    if (nfeDet.nfeProd.CFOP == 5914)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5914;
                    if (nfeDet.nfeProd.CFOP == 5915)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5915;
                    if (nfeDet.nfeProd.CFOP == 5916)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5916;
                    if (nfeDet.nfeProd.CFOP == 5917)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5917;
                    if (nfeDet.nfeProd.CFOP == 5918)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5918;
                    if (nfeDet.nfeProd.CFOP == 5919)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5919;
                    if (nfeDet.nfeProd.CFOP == 5920)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5920;
                    if (nfeDet.nfeProd.CFOP == 5921)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5921;
                    if (nfeDet.nfeProd.CFOP == 5922)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5922;
                    if (nfeDet.nfeProd.CFOP == 5923)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5923;
                    if (nfeDet.nfeProd.CFOP == 5924)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5924;
                    if (nfeDet.nfeProd.CFOP == 5925)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5925;
                    if (nfeDet.nfeProd.CFOP == 5926)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5926;
                    if (nfeDet.nfeProd.CFOP == 5927)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5927;
                    if (nfeDet.nfeProd.CFOP == 5928)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5928;
                    if (nfeDet.nfeProd.CFOP == 5929)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5929;
                    if (nfeDet.nfeProd.CFOP == 5931)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5931;
                    if (nfeDet.nfeProd.CFOP == 5932)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5932;
                    if (nfeDet.nfeProd.CFOP == 5933)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5933;
                    if (nfeDet.nfeProd.CFOP == 5934)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5934;
                    if (nfeDet.nfeProd.CFOP == 5949)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item5949;
                    if (nfeDet.nfeProd.CFOP == 6101)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6101;
                    if (nfeDet.nfeProd.CFOP == 6102)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6102;
                    if (nfeDet.nfeProd.CFOP == 6103)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6103;
                    if (nfeDet.nfeProd.CFOP == 6104)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6104;
                    if (nfeDet.nfeProd.CFOP == 6105)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6105;
                    if (nfeDet.nfeProd.CFOP == 6106)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6106;
                    if (nfeDet.nfeProd.CFOP == 6107)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6107;
                    if (nfeDet.nfeProd.CFOP == 6108)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6108;
                    if (nfeDet.nfeProd.CFOP == 6109)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6109;
                    if (nfeDet.nfeProd.CFOP == 6110)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6110;
                    if (nfeDet.nfeProd.CFOP == 6111)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6111;
                    if (nfeDet.nfeProd.CFOP == 6112)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6112;
                    if (nfeDet.nfeProd.CFOP == 6113)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6113;
                    if (nfeDet.nfeProd.CFOP == 6114)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6114;
                    if (nfeDet.nfeProd.CFOP == 6115)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6115;
                    if (nfeDet.nfeProd.CFOP == 6116)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6116;
                    if (nfeDet.nfeProd.CFOP == 6117)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6117;
                    if (nfeDet.nfeProd.CFOP == 6118)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6118;
                    if (nfeDet.nfeProd.CFOP == 6119)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6119;
                    if (nfeDet.nfeProd.CFOP == 6120)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6120;
                    if (nfeDet.nfeProd.CFOP == 6122)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6122;
                    if (nfeDet.nfeProd.CFOP == 6123)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6123;
                    if (nfeDet.nfeProd.CFOP == 6124)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6124;
                    if (nfeDet.nfeProd.CFOP == 6125)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6125;
                    if (nfeDet.nfeProd.CFOP == 6151)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6151;
                    if (nfeDet.nfeProd.CFOP == 6152)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6152;
                    if (nfeDet.nfeProd.CFOP == 6153)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6153;
                    if (nfeDet.nfeProd.CFOP == 6155)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6155;
                    if (nfeDet.nfeProd.CFOP == 6156)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6156;
                    if (nfeDet.nfeProd.CFOP == 6201)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6201;
                    if (nfeDet.nfeProd.CFOP == 6202)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6202;
                    if (nfeDet.nfeProd.CFOP == 6205)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6205;
                    if (nfeDet.nfeProd.CFOP == 6206)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6206;
                    if (nfeDet.nfeProd.CFOP == 6207)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6207;
                    if (nfeDet.nfeProd.CFOP == 6208)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6208;
                    if (nfeDet.nfeProd.CFOP == 6209)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6209;
                    if (nfeDet.nfeProd.CFOP == 6210)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6210;


                    if (nfeDet.nfeProd.CFOP == 6251)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6251;
                    if (nfeDet.nfeProd.CFOP == 6252)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6252;
                    if (nfeDet.nfeProd.CFOP == 6253)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6253;
                    if (nfeDet.nfeProd.CFOP == 6254)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6254;
                    if (nfeDet.nfeProd.CFOP == 6255)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6255;
                    if (nfeDet.nfeProd.CFOP == 6256)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6256;
                    if (nfeDet.nfeProd.CFOP == 6257)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6257;
                    if (nfeDet.nfeProd.CFOP == 6258)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6258;
                    if (nfeDet.nfeProd.CFOP == 6401)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6401;
                    if (nfeDet.nfeProd.CFOP == 6402)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6402;
                    if (nfeDet.nfeProd.CFOP == 6403)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6403;
                    if (nfeDet.nfeProd.CFOP == 6404)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6404;
                    if (nfeDet.nfeProd.CFOP == 6408)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6408;
                    if (nfeDet.nfeProd.CFOP == 6409)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6409;
                    if (nfeDet.nfeProd.CFOP == 6410)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6410;
                    if (nfeDet.nfeProd.CFOP == 6411)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6411;
                    if (nfeDet.nfeProd.CFOP == 6412)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6412;
                    if (nfeDet.nfeProd.CFOP == 6413)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6413;
                    if (nfeDet.nfeProd.CFOP == 6414)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6414;
                    if (nfeDet.nfeProd.CFOP == 6415)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6415;
                    if (nfeDet.nfeProd.CFOP == 6501)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6501;
                    if (nfeDet.nfeProd.CFOP == 6503)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6503;
                    if (nfeDet.nfeProd.CFOP == 6504)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6504;
                    if (nfeDet.nfeProd.CFOP == 6505)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6505;
                    if (nfeDet.nfeProd.CFOP == 6551)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6551;
                    if (nfeDet.nfeProd.CFOP == 6552)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6552;
                    if (nfeDet.nfeProd.CFOP == 6553)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6553;
                    if (nfeDet.nfeProd.CFOP == 6554)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6554;
                    if (nfeDet.nfeProd.CFOP == 6555)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6555;
                    if (nfeDet.nfeProd.CFOP == 6556)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6556;
                    if (nfeDet.nfeProd.CFOP == 6557)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6557;
                    if (nfeDet.nfeProd.CFOP == 6603)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6603;
                    if (nfeDet.nfeProd.CFOP == 6651)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6651;
                    if (nfeDet.nfeProd.CFOP == 6652)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6652;
                    if (nfeDet.nfeProd.CFOP == 6653)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6653;
                    if (nfeDet.nfeProd.CFOP == 6654)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6654;
                    if (nfeDet.nfeProd.CFOP == 6655)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6655;
                    if (nfeDet.nfeProd.CFOP == 6656)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6656;
                    if (nfeDet.nfeProd.CFOP == 6657)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6657;
                    if (nfeDet.nfeProd.CFOP == 6658)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6658;
                    if (nfeDet.nfeProd.CFOP == 6659)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6659;
                    if (nfeDet.nfeProd.CFOP == 6660)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6660;
                    if (nfeDet.nfeProd.CFOP == 6661)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6661;
                    if (nfeDet.nfeProd.CFOP == 6662)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6662;
                    if (nfeDet.nfeProd.CFOP == 6663)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6663;
                    if (nfeDet.nfeProd.CFOP == 6664)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6664;
                    if (nfeDet.nfeProd.CFOP == 6665)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6665;
                    if (nfeDet.nfeProd.CFOP == 6666)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6666;
                    if (nfeDet.nfeProd.CFOP == 6667)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6667;
                    if (nfeDet.nfeProd.CFOP == 6901)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6901;
                    if (nfeDet.nfeProd.CFOP == 6902)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6902;
                    if (nfeDet.nfeProd.CFOP == 6903)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6903;
                    if (nfeDet.nfeProd.CFOP == 6904)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6904;
                    if (nfeDet.nfeProd.CFOP == 6905)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6905;
                    if (nfeDet.nfeProd.CFOP == 6906)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6906;
                    if (nfeDet.nfeProd.CFOP == 6907)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6907;
                    if (nfeDet.nfeProd.CFOP == 6908)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6908;
                    if (nfeDet.nfeProd.CFOP == 6909)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6909;
                    if (nfeDet.nfeProd.CFOP == 6910)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6910;
                    if (nfeDet.nfeProd.CFOP == 6911)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6911;
                    if (nfeDet.nfeProd.CFOP == 6912)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6912;
                    if (nfeDet.nfeProd.CFOP == 6913)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6913;
                    if (nfeDet.nfeProd.CFOP == 6914)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6914;
                    if (nfeDet.nfeProd.CFOP == 6915)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6915;
                    if (nfeDet.nfeProd.CFOP == 6916)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6916;
                    if (nfeDet.nfeProd.CFOP == 6917)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6917;
                    if (nfeDet.nfeProd.CFOP == 6918)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6918;
                    if (nfeDet.nfeProd.CFOP == 6919)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6919;
                    if (nfeDet.nfeProd.CFOP == 6920)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6920;
                    if (nfeDet.nfeProd.CFOP == 6921)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6921;
                    if (nfeDet.nfeProd.CFOP == 6922)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6922;
                    if (nfeDet.nfeProd.CFOP == 6923)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6923;
                    if (nfeDet.nfeProd.CFOP == 6924)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6924;
                    if (nfeDet.nfeProd.CFOP == 6925)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6925;
                    if (nfeDet.nfeProd.CFOP == 6929)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6929;
                    if (nfeDet.nfeProd.CFOP == 6931)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6931;
                    if (nfeDet.nfeProd.CFOP == 6932)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6932;
                    if (nfeDet.nfeProd.CFOP == 6933)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6933;
                    if (nfeDet.nfeProd.CFOP == 6934)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6934;
                    if (nfeDet.nfeProd.CFOP == 6949)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item6949;
                    if (nfeDet.nfeProd.CFOP == 7101)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7101;
                    if (nfeDet.nfeProd.CFOP == 7102)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7102;
                    if (nfeDet.nfeProd.CFOP == 7105)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7105;
                    if (nfeDet.nfeProd.CFOP == 7106)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7106;
                    if (nfeDet.nfeProd.CFOP == 7127)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7127;
                    if (nfeDet.nfeProd.CFOP == 7201)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7201;
                    if (nfeDet.nfeProd.CFOP == 7202)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7202;
                    if (nfeDet.nfeProd.CFOP == 7205)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7205;
                    if (nfeDet.nfeProd.CFOP == 7206)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7206;
                    if (nfeDet.nfeProd.CFOP == 7207)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7207;
                    if (nfeDet.nfeProd.CFOP == 7210)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7210;
                    if (nfeDet.nfeProd.CFOP == 7211)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7211;
                    if (nfeDet.nfeProd.CFOP == 7251)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7251;
                    if (nfeDet.nfeProd.CFOP == 7501)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7501;
                    if (nfeDet.nfeProd.CFOP == 7551)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7551;
                    if (nfeDet.nfeProd.CFOP == 7553)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7553;
                    if (nfeDet.nfeProd.CFOP == 7556)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7556;
                    if (nfeDet.nfeProd.CFOP == 7651)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7651;
                    if (nfeDet.nfeProd.CFOP == 7654)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7654;
                    if (nfeDet.nfeProd.CFOP == 7667)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7667;
                    if (nfeDet.nfeProd.CFOP == 7930)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7930;
                    if (nfeDet.nfeProd.CFOP == 7949)
                        _nfe.infNFe.det[item].prod.CFOP = ValidateLib.enviNFe_ns.TCfop.Item7949;

                    #endregion

                    _nfe.infNFe.det[item].prod.cProd = nfeDet.nfeProd.cProd;

                    if (nfeDet.nfeProd.EXTIPI != null)
                        _nfe.infNFe.det[item].prod.EXTIPI = nfeDet.nfeProd.EXTIPI.ToString();

                    if (nfeDet.nfeProd.indTot == 0)
                        _nfe.infNFe.det[item].prod.indTot = ValidateLib.enviNFe_ns.TNFeInfNFeDetProdIndTot.Item0;
                    if (nfeDet.nfeProd.indTot == 1)
                        _nfe.infNFe.det[item].prod.indTot = ValidateLib.enviNFe_ns.TNFeInfNFeDetProdIndTot.Item1;

                    //_nfe.infNFe.det[item].prod.Items = null;
                    int contador = nfeDet.nItem - 1;

                    #region Importação
                    System.Data.DataTable dtDI = _conn.ExecQuery("SELECT * FROM ZNFEIMPORTA WHERE IDOUTBOX = ? AND NITEM = ? ", new object[] { nfeDet.IdOutbox, nfeDet.nItem });
                    if (dtDI.Rows.Count > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDI[] LISTADI = new ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDI[] { };
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDIAdi[] LISTAADI = new ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDIAdi[] { };
                        if (dtDI.Rows.Count > 0)
                        {
                            ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDI DI = new ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDI();
                            ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDIAdi ADI = new ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDIAdi();
                            DI.nDI = dtDI.Rows[0]["NDI"].ToString();
                            DI.dDI = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dtDI.Rows[0]["DDI"]));
                            DI.xLocDesemb = dtDI.Rows[0]["XLOCDESEMB"].ToString();
                            #region UFDESEMB
                            switch (dtDI.Rows[0]["UFDESEMB"].ToString())
                            {
                                case "AC":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.AC;
                                    break;
                                case "AL":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.AL;
                                    break;
                                case "AM":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.AM;
                                    break;
                                case "AP":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.AP;
                                    break;
                                case "BA":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.BA;
                                    break;
                                case "CE":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.CE;
                                    break;
                                case "DF":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.DF;
                                    break;
                                case "ES":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.ES;
                                    break;
                                case "GO":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.GO;
                                    break;
                                case "MA":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.MA;
                                    break;
                                case "MG":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.MG;
                                    break;
                                case "MS":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.MS;
                                    break;
                                case "MT":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.MT;
                                    break;
                                case "PA":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.PA;
                                    break;
                                case "PB":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.PB;
                                    break;
                                case "PE":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.PE;
                                    break;
                                case "PI":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.PI;
                                    break;
                                case "PR":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.PR;
                                    break;
                                case "RJ":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.RJ;
                                    break;
                                case "RN":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.RN;
                                    break;
                                case "RO":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.RO;
                                    break;
                                case "RR":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.RR;
                                    break;
                                case "RS":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.RS;
                                    break;
                                case "SC":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.SC;
                                    break;
                                case "SE":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.SE;
                                    break;
                                case "SP":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.SP;
                                    break;
                                case "TO":
                                    DI.UFDesemb = ValidateLib.enviNFe_ns.TUfEmi.TO;
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            DI.dDesemb = String.Format("{0:yyyy-MM-dd}", dtDI.Rows[0]["DDESEMB"]);
                            #region TPVIATRANSP
                            switch (dtDI.Rows[0]["TPVIATRANSP"].ToString())
                            {
                                case "1":
                                    DI.tpViaTransp = ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDITpViaTransp.Item1;
                                    break;
                                case "2":
                                    DI.tpViaTransp = ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDITpViaTransp.Item2;
                                    break;
                                case "3":
                                    DI.tpViaTransp = ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDITpViaTransp.Item3;
                                    break;
                                case "4":
                                    DI.tpViaTransp = ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDITpViaTransp.Item4;
                                    break;
                                case "5":
                                    DI.tpViaTransp = ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDITpViaTransp.Item5;
                                    break;
                                case "6":
                                    DI.tpViaTransp = ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDITpViaTransp.Item6;
                                    break;
                                case "7":
                                    DI.tpViaTransp = ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDITpViaTransp.Item7;
                                    break;
                                case "8":
                                    DI.tpViaTransp = ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDITpViaTransp.Item8;
                                    break;
                                case "9":
                                    DI.tpViaTransp = ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDITpViaTransp.Item9;
                                    break;
                                case "10":
                                    DI.tpViaTransp = ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDITpViaTransp.Item10;
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            DI.vAFRMM = dtDI.Rows[0]["vAFRMM"].ToString().Replace(",", ".");
                            #region TPINTERMEDIO
                            switch (dtDI.Rows[0]["TPINTERMEDIO"].ToString())
                            {
                                case "1":
                                    DI.tpIntermedio = ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDITpIntermedio.Item1;
                                    break;
                                case "2":
                                    DI.tpIntermedio = ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDITpIntermedio.Item2;
                                    break;
                                case "3":
                                    DI.tpIntermedio = ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDITpIntermedio.Item3;
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            DI.CNPJ = dtDI.Rows[0]["CNPJ"].ToString();
                            #region UFTERCEIRO
                            switch (dtDI.Rows[0]["UFTERCEIRO"].ToString())
                            {
                                case "AC":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.AC;
                                    break;
                                case "AL":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.AL;
                                    break;
                                case "AM":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.AM;
                                    break;
                                case "AP":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.AP;
                                    break;
                                case "BA":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.BA;
                                    break;
                                case "CE":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.CE;
                                    break;
                                case "DF":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.DF;
                                    break;
                                case "ES":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.ES;
                                    break;
                                case "GO":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.GO;
                                    break;
                                case "MA":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.MA;
                                    break;
                                case "MG":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.MG;
                                    break;
                                case "MS":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.MS;
                                    break;
                                case "MT":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.MT;
                                    break;
                                case "PA":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.PA;
                                    break;
                                case "PB":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.PB;
                                    break;
                                case "PE":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.PE;
                                    break;
                                case "PI":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.PI;
                                    break;
                                case "PR":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.PR;
                                    break;
                                case "RJ":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.RJ;
                                    break;
                                case "RN":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.RN;
                                    break;
                                case "RO":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.RO;
                                    break;
                                case "RR":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.RR;
                                    break;
                                case "RS":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.RS;
                                    break;
                                case "SC":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.SC;
                                    break;
                                case "SE":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.SE;
                                    break;
                                case "SP":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.SP;
                                    break;
                                case "TO":
                                    DI.UFTerceiro = ValidateLib.enviNFe_ns.TUfEmi.TO;
                                    break;
                                default:
                                    break;
                            }
                            #endregion
                            DI.cExportador = dtDI.Rows[0]["CEXPORTADOR"].ToString();
                            // ADI
                            ADI.nAdicao = dtDI.Rows[0]["NADICAO"].ToString();
                            ADI.nSeqAdic = nfeDet.nItem.ToString();
                            ADI.cFabricante = dtDI.Rows[0]["CFABRICANTE"].ToString();
                            ADI.vDescDI = dtDI.Rows[0]["VDESCDI"].ToString().Replace(",", ".");
                            LISTAADI = new ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDIAdi[] { ADI };
                            DI.adi = LISTAADI;
                            LISTADI = new ValidateLib.enviNFe_ns.TNFeInfNFeDetProdDI[] { DI };
                        }
                        _nfe.infNFe.det[item].prod.DI = LISTADI;
                    }
                    #endregion

                    _nfe.infNFe.det[item].prod.NCM = nfeDet.nfeProd.NCM;

                    if (!string.IsNullOrEmpty(nfeDet.nfeProd.CEST))
                    {
                        _nfe.infNFe.det[item].prod.CEST = nfeDet.nfeProd.CEST;
                    }
                    _nfe.infNFe.det[item].prod.nFCI = nfeDet.nfeProd.nFCI;
                    _nfe.infNFe.det[item].prod.nItemPed = nfeDet.nfeProd.nItemPed;


                    if (!string.IsNullOrEmpty(nfeDet.nfeProd.NVE))
                    {
                        string[] nve = nfeDet.nfeProd.NVE.Split(';');
                        _nfe.infNFe.det[item].prod.NVE = new string[nve.Length];
                        int nnve = 0;
                        foreach (string str in nve)
                        {
                            _nfe.infNFe.det[item].prod.NVE[nnve] = str;
                            nnve++;
                        }
                    }

                    _nfe.infNFe.det[item].prod.qCom = nfeDet.nfeProd.qCom.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.det[item].prod.qTrib = nfeDet.nfeProd.qTrib.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.det[item].prod.uCom = nfeDet.nfeProd.uCom;
                    if (nfeDet.nfeProd.uTrib == null)
                    {
                        _nfe.infNFe.det[item].prod.uTrib = "0";
                    }
                    else
                    {
                        _nfe.infNFe.det[item].prod.uTrib = nfeDet.nfeProd.uTrib.ToString().Replace(".", "").Replace(",", ".");
                    }

                    if (nfeDet.nfeProd.vDesc > 0)
                        _nfe.infNFe.det[item].prod.vDesc = nfeDet.nfeProd.vDesc.ToString().Replace(".", "").Replace(",", ".");
                    if (nfeDet.nfeProd.vFrete > 0)
                        _nfe.infNFe.det[item].prod.vFrete = nfeDet.nfeProd.vFrete.ToString().Replace(".", "").Replace(",", ".");
                    if (nfeDet.nfeProd.vOutro > 0)
                        _nfe.infNFe.det[item].prod.vOutro = nfeDet.nfeProd.vOutro.ToString().Replace(".", "").Replace(",", ".");
                    if (nfeDet.nfeProd.vSeg > 0)
                        _nfe.infNFe.det[item].prod.vSeg = nfeDet.nfeProd.vSeg.ToString().Replace(".", "").Replace(",", ".");

                    _nfe.infNFe.det[item].prod.vProd = nfeDet.nfeProd.vProd.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.det[item].prod.vUnCom = nfeDet.nfeProd.vUnCom.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.det[item].prod.vUnTrib = nfeDet.nfeProd.vUnTrib.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.det[item].prod.xPed = string.IsNullOrEmpty(nfeDet.nfeProd.xPed) ? null : nfeDet.nfeProd.xPed;
                    _nfe.infNFe.det[item].prod.xProd = string.IsNullOrEmpty(nfeDet.nfeProd.xProd) ? null : nfeDet.nfeProd.xProd;

                    #endregion

                    _nfe.infNFe.det[item].imposto = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImposto();

                    if (nfeDet.nfeImposto.vTotTrib > 0)
                        _nfe.infNFe.det[item].imposto.vTotTrib = nfeDet.nfeImposto.vTotTrib.ToString().Replace(".", "").Replace(",", ".");
                    List<object> limposto = new List<object>();

                    #region ICMS

                    ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMS tNFeInfNFeDetImpostoICMS = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMS();

                    #region ICMS00

                    if (nfeDet.nfeImposto.nfeICMS00.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS00 tNFeInfNFeDetImpostoICMSICMS00 = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS00();
                        tNFeInfNFeDetImpostoICMSICMS00.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS00CST.Item00;

                        if (nfeDet.nfeImposto.nfeICMS00.modBC == 0)
                            tNFeInfNFeDetImpostoICMSICMS00.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS00ModBC.Item0;
                        if (nfeDet.nfeImposto.nfeICMS00.modBC == 1)
                            tNFeInfNFeDetImpostoICMSICMS00.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS00ModBC.Item1;
                        if (nfeDet.nfeImposto.nfeICMS00.modBC == 2)
                            tNFeInfNFeDetImpostoICMSICMS00.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS00ModBC.Item2;
                        if (nfeDet.nfeImposto.nfeICMS00.modBC == 3)
                            tNFeInfNFeDetImpostoICMSICMS00.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS00ModBC.Item3;

                        if (nfeDet.nfeImposto.nfeICMS00.orig == 0)
                            tNFeInfNFeDetImpostoICMSICMS00.orig = ValidateLib.enviNFe_ns.Torig.Item0;
                        if (nfeDet.nfeImposto.nfeICMS00.orig == 1)
                            tNFeInfNFeDetImpostoICMSICMS00.orig = ValidateLib.enviNFe_ns.Torig.Item1;
                        if (nfeDet.nfeImposto.nfeICMS00.orig == 2)
                            tNFeInfNFeDetImpostoICMSICMS00.orig = ValidateLib.enviNFe_ns.Torig.Item2;
                        if (nfeDet.nfeImposto.nfeICMS00.orig == 3)
                            tNFeInfNFeDetImpostoICMSICMS00.orig = ValidateLib.enviNFe_ns.Torig.Item3;
                        if (nfeDet.nfeImposto.nfeICMS00.orig == 4)
                            tNFeInfNFeDetImpostoICMSICMS00.orig = ValidateLib.enviNFe_ns.Torig.Item4;
                        if (nfeDet.nfeImposto.nfeICMS00.orig == 5)
                            tNFeInfNFeDetImpostoICMSICMS00.orig = ValidateLib.enviNFe_ns.Torig.Item5;
                        if (nfeDet.nfeImposto.nfeICMS00.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMS00.orig = ValidateLib.enviNFe_ns.Torig.Item6;
                        if (nfeDet.nfeImposto.nfeICMS00.orig == 7)
                            tNFeInfNFeDetImpostoICMSICMS00.orig = ValidateLib.enviNFe_ns.Torig.Item7;
                        if (nfeDet.nfeImposto.nfeICMS00.orig == 8)
                            tNFeInfNFeDetImpostoICMSICMS00.orig = ValidateLib.enviNFe_ns.Torig.Item8;

                        tNFeInfNFeDetImpostoICMSICMS00.pICMS = nfeDet.nfeImposto.nfeICMS00.pICMS.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS00.vBC = nfeDet.nfeImposto.nfeICMS00.vBC.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS00.vICMS = nfeDet.nfeImposto.nfeICMS00.vICMS.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoICMS.Item = tNFeInfNFeDetImpostoICMSICMS00;
                        limposto.Add(tNFeInfNFeDetImpostoICMS);
                    }

                    #endregion

                    #region ICMS10

                    if (nfeDet.nfeImposto.nfeICMS10.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS10 tNFeInfNFeDetImpostoICMSICMS10 = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS10();
                        tNFeInfNFeDetImpostoICMSICMS10.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS10CST.Item10;

                        if (nfeDet.nfeImposto.nfeICMS10.modBC == 0)
                            tNFeInfNFeDetImpostoICMSICMS10.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS10ModBC.Item0;
                        if (nfeDet.nfeImposto.nfeICMS10.modBC == 1)
                            tNFeInfNFeDetImpostoICMSICMS10.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS10ModBC.Item1;
                        if (nfeDet.nfeImposto.nfeICMS10.modBC == 2)
                            tNFeInfNFeDetImpostoICMSICMS10.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS10ModBC.Item2;
                        if (nfeDet.nfeImposto.nfeICMS10.modBC == 3)
                            tNFeInfNFeDetImpostoICMSICMS10.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS10ModBC.Item3;

                        if (nfeDet.nfeImposto.nfeICMS10.modBCST == 0)
                            tNFeInfNFeDetImpostoICMSICMS10.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS10ModBCST.Item0;
                        if (nfeDet.nfeImposto.nfeICMS10.modBCST == 1)
                            tNFeInfNFeDetImpostoICMSICMS10.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS10ModBCST.Item1;
                        if (nfeDet.nfeImposto.nfeICMS10.modBCST == 2)
                            tNFeInfNFeDetImpostoICMSICMS10.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS10ModBCST.Item2;
                        if (nfeDet.nfeImposto.nfeICMS10.modBCST == 3)
                            tNFeInfNFeDetImpostoICMSICMS10.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS10ModBCST.Item3;
                        if (nfeDet.nfeImposto.nfeICMS10.modBCST == 4)
                            tNFeInfNFeDetImpostoICMSICMS10.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS10ModBCST.Item4;
                        if (nfeDet.nfeImposto.nfeICMS10.modBCST == 5)
                            tNFeInfNFeDetImpostoICMSICMS10.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS10ModBCST.Item5;

                        if (nfeDet.nfeImposto.nfeICMS10.orig == 0)
                            tNFeInfNFeDetImpostoICMSICMS10.orig = ValidateLib.enviNFe_ns.Torig.Item0;
                        if (nfeDet.nfeImposto.nfeICMS10.orig == 1)
                            tNFeInfNFeDetImpostoICMSICMS10.orig = ValidateLib.enviNFe_ns.Torig.Item1;
                        if (nfeDet.nfeImposto.nfeICMS10.orig == 2)
                            tNFeInfNFeDetImpostoICMSICMS10.orig = ValidateLib.enviNFe_ns.Torig.Item2;
                        if (nfeDet.nfeImposto.nfeICMS10.orig == 3)
                            tNFeInfNFeDetImpostoICMSICMS10.orig = ValidateLib.enviNFe_ns.Torig.Item3;
                        if (nfeDet.nfeImposto.nfeICMS10.orig == 4)
                            tNFeInfNFeDetImpostoICMSICMS10.orig = ValidateLib.enviNFe_ns.Torig.Item4;
                        if (nfeDet.nfeImposto.nfeICMS10.orig == 5)
                            tNFeInfNFeDetImpostoICMSICMS10.orig = ValidateLib.enviNFe_ns.Torig.Item5;
                        if (nfeDet.nfeImposto.nfeICMS10.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMS10.orig = ValidateLib.enviNFe_ns.Torig.Item6;
                        if (nfeDet.nfeImposto.nfeICMS10.orig == 7)
                            tNFeInfNFeDetImpostoICMSICMS10.orig = ValidateLib.enviNFe_ns.Torig.Item7;
                        if (nfeDet.nfeImposto.nfeICMS10.orig == 8)
                            tNFeInfNFeDetImpostoICMSICMS10.orig = ValidateLib.enviNFe_ns.Torig.Item8;

                        tNFeInfNFeDetImpostoICMSICMS10.pICMS = nfeDet.nfeImposto.nfeICMS10.pICMS.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS10.pICMSST = nfeDet.nfeImposto.nfeICMS10.pICMSST.ToString().Replace(".", "").Replace(",", ".");

                        if (nfeDet.nfeImposto.nfeICMS10.pMVAST > 0)
                            tNFeInfNFeDetImpostoICMSICMS10.pMVAST = nfeDet.nfeImposto.nfeICMS10.pMVAST.ToString().Replace(".", "").Replace(",", ".");
                        if (nfeDet.nfeImposto.nfeICMS10.pRedBCST > 0)
                            tNFeInfNFeDetImpostoICMSICMS10.pRedBCST = nfeDet.nfeImposto.nfeICMS10.pRedBCST.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoICMSICMS10.vBC = nfeDet.nfeImposto.nfeICMS10.vBC.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS10.vBCST = nfeDet.nfeImposto.nfeICMS10.vBCST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS10.vICMS = nfeDet.nfeImposto.nfeICMS10.vICMS.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS10.vICMSST = nfeDet.nfeImposto.nfeICMS10.vICMSST.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoICMS.Item = tNFeInfNFeDetImpostoICMSICMS10;
                        limposto.Add(tNFeInfNFeDetImpostoICMS);
                    }

                    #endregion

                    #region ICMS20

                    if (nfeDet.nfeImposto.nfeICMS20.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS20 tNFeInfNFeDetImpostoICMSICMS20 = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS20();
                        tNFeInfNFeDetImpostoICMSICMS20.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS20CST.Item20;

                        if (nfeDet.nfeImposto.nfeICMS20.modBC == 0)
                            tNFeInfNFeDetImpostoICMSICMS20.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS20ModBC.Item0;
                        if (nfeDet.nfeImposto.nfeICMS20.modBC == 1)
                            tNFeInfNFeDetImpostoICMSICMS20.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS20ModBC.Item1;
                        if (nfeDet.nfeImposto.nfeICMS20.modBC == 2)
                            tNFeInfNFeDetImpostoICMSICMS20.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS20ModBC.Item2;
                        if (nfeDet.nfeImposto.nfeICMS20.modBC == 3)
                            tNFeInfNFeDetImpostoICMSICMS20.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS20ModBC.Item3;

                        if (nfeDet.nfeImposto.nfeICMS20.orig == 0)
                            tNFeInfNFeDetImpostoICMSICMS20.orig = ValidateLib.enviNFe_ns.Torig.Item0;
                        if (nfeDet.nfeImposto.nfeICMS20.orig == 1)
                            tNFeInfNFeDetImpostoICMSICMS20.orig = ValidateLib.enviNFe_ns.Torig.Item1;
                        if (nfeDet.nfeImposto.nfeICMS20.orig == 2)
                            tNFeInfNFeDetImpostoICMSICMS20.orig = ValidateLib.enviNFe_ns.Torig.Item2;
                        if (nfeDet.nfeImposto.nfeICMS20.orig == 3)
                            tNFeInfNFeDetImpostoICMSICMS20.orig = ValidateLib.enviNFe_ns.Torig.Item3;
                        if (nfeDet.nfeImposto.nfeICMS20.orig == 4)
                            tNFeInfNFeDetImpostoICMSICMS20.orig = ValidateLib.enviNFe_ns.Torig.Item4;
                        if (nfeDet.nfeImposto.nfeICMS20.orig == 5)
                            tNFeInfNFeDetImpostoICMSICMS20.orig = ValidateLib.enviNFe_ns.Torig.Item5;
                        if (nfeDet.nfeImposto.nfeICMS20.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMS20.orig = ValidateLib.enviNFe_ns.Torig.Item6;
                        if (nfeDet.nfeImposto.nfeICMS20.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMS20.orig = ValidateLib.enviNFe_ns.Torig.Item7;
                        if (nfeDet.nfeImposto.nfeICMS20.orig == 8)
                            tNFeInfNFeDetImpostoICMSICMS20.orig = ValidateLib.enviNFe_ns.Torig.Item8;

                        tNFeInfNFeDetImpostoICMSICMS20.pICMS = nfeDet.nfeImposto.nfeICMS20.pICMS.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS20.pRedBC = nfeDet.nfeImposto.nfeICMS20.pRedBC.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS20.vBC = nfeDet.nfeImposto.nfeICMS20.vBC.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS20.vICMS = nfeDet.nfeImposto.nfeICMS20.vICMS.ToString().Replace(".", "").Replace(",", ".");

                        if (nfeDet.nfeImposto.nfeICMS20.vICMSDeson > 0)
                        {
                            tNFeInfNFeDetImpostoICMSICMS20.vICMSDeson = nfeDet.nfeImposto.nfeICMS20.vICMSDeson.ToString().Replace(".", "").Replace(",", ".");

                            if (nfeDet.nfeImposto.nfeICMS20.motDesICMS == 3)
                                tNFeInfNFeDetImpostoICMSICMS20.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS20MotDesICMS.Item3;
                            if (nfeDet.nfeImposto.nfeICMS20.motDesICMS == 9)
                                tNFeInfNFeDetImpostoICMSICMS20.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS20MotDesICMS.Item9;
                            if (nfeDet.nfeImposto.nfeICMS20.motDesICMS == 12)
                                tNFeInfNFeDetImpostoICMSICMS20.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS20MotDesICMS.Item12;
                        }

                        tNFeInfNFeDetImpostoICMS.Item = tNFeInfNFeDetImpostoICMSICMS20;
                        limposto.Add(tNFeInfNFeDetImpostoICMS);
                    }

                    #endregion

                    #region ICMS30

                    if (nfeDet.nfeImposto.nfeICMS30.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS30 tNFeInfNFeDetImpostoICMSICMS30 = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS30();
                        tNFeInfNFeDetImpostoICMSICMS30.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS30CST.Item30;

                        if (nfeDet.nfeImposto.nfeICMS30.modBCST == 0)
                            tNFeInfNFeDetImpostoICMSICMS30.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS30ModBCST.Item0;
                        if (nfeDet.nfeImposto.nfeICMS30.modBCST == 1)
                            tNFeInfNFeDetImpostoICMSICMS30.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS30ModBCST.Item1;
                        if (nfeDet.nfeImposto.nfeICMS30.modBCST == 2)
                            tNFeInfNFeDetImpostoICMSICMS30.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS30ModBCST.Item2;
                        if (nfeDet.nfeImposto.nfeICMS30.modBCST == 3)
                            tNFeInfNFeDetImpostoICMSICMS30.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS30ModBCST.Item3;
                        if (nfeDet.nfeImposto.nfeICMS30.modBCST == 4)
                            tNFeInfNFeDetImpostoICMSICMS30.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS30ModBCST.Item4;
                        if (nfeDet.nfeImposto.nfeICMS30.modBCST == 5)
                            tNFeInfNFeDetImpostoICMSICMS30.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS30ModBCST.Item5;

                        if (nfeDet.nfeImposto.nfeICMS30.orig == 0)
                            tNFeInfNFeDetImpostoICMSICMS30.orig = ValidateLib.enviNFe_ns.Torig.Item0;
                        if (nfeDet.nfeImposto.nfeICMS30.orig == 1)
                            tNFeInfNFeDetImpostoICMSICMS30.orig = ValidateLib.enviNFe_ns.Torig.Item1;
                        if (nfeDet.nfeImposto.nfeICMS30.orig == 2)
                            tNFeInfNFeDetImpostoICMSICMS30.orig = ValidateLib.enviNFe_ns.Torig.Item2;
                        if (nfeDet.nfeImposto.nfeICMS30.orig == 3)
                            tNFeInfNFeDetImpostoICMSICMS30.orig = ValidateLib.enviNFe_ns.Torig.Item3;
                        if (nfeDet.nfeImposto.nfeICMS30.orig == 4)
                            tNFeInfNFeDetImpostoICMSICMS30.orig = ValidateLib.enviNFe_ns.Torig.Item4;
                        if (nfeDet.nfeImposto.nfeICMS30.orig == 5)
                            tNFeInfNFeDetImpostoICMSICMS30.orig = ValidateLib.enviNFe_ns.Torig.Item5;
                        if (nfeDet.nfeImposto.nfeICMS30.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMS30.orig = ValidateLib.enviNFe_ns.Torig.Item6;
                        if (nfeDet.nfeImposto.nfeICMS30.orig == 7)
                            tNFeInfNFeDetImpostoICMSICMS30.orig = ValidateLib.enviNFe_ns.Torig.Item7;
                        if (nfeDet.nfeImposto.nfeICMS30.orig == 8)
                            tNFeInfNFeDetImpostoICMSICMS30.orig = ValidateLib.enviNFe_ns.Torig.Item8;

                        tNFeInfNFeDetImpostoICMSICMS30.pICMSST = nfeDet.nfeImposto.nfeICMS30.pICMSST.ToString().Replace(".", "").Replace(",", ".");

                        if (nfeDet.nfeImposto.nfeICMS30.pMVAST > 0)
                            tNFeInfNFeDetImpostoICMSICMS30.pMVAST = nfeDet.nfeImposto.nfeICMS30.pMVAST.ToString().Replace(".", "").Replace(",", ".");
                        if (nfeDet.nfeImposto.nfeICMS30.pRedBCST > 0)
                            tNFeInfNFeDetImpostoICMSICMS30.pRedBCST = nfeDet.nfeImposto.nfeICMS30.pRedBCST.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoICMSICMS30.vBCST = nfeDet.nfeImposto.nfeICMS30.vBCST.ToString().Replace(".", "").Replace(",", ".");

                        if (nfeDet.nfeImposto.nfeICMS30.vICMSDeson > 0)
                        {
                            tNFeInfNFeDetImpostoICMSICMS30.vICMSDeson = nfeDet.nfeImposto.nfeICMS30.vICMSDeson.ToString().Replace(".", "").Replace(",", ".");

                            if (nfeDet.nfeImposto.nfeICMS30.motDesICMS == 6)
                                tNFeInfNFeDetImpostoICMSICMS30.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS30MotDesICMS.Item6;
                            if (nfeDet.nfeImposto.nfeICMS30.motDesICMS == 7)
                                tNFeInfNFeDetImpostoICMSICMS30.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS30MotDesICMS.Item7;
                            if (nfeDet.nfeImposto.nfeICMS30.motDesICMS == 9)
                                tNFeInfNFeDetImpostoICMSICMS30.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS30MotDesICMS.Item9;
                        }

                        tNFeInfNFeDetImpostoICMSICMS30.vICMSST = nfeDet.nfeImposto.nfeICMS30.vICMSST.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoICMS.Item = tNFeInfNFeDetImpostoICMSICMS30;
                        limposto.Add(tNFeInfNFeDetImpostoICMS);
                    }

                    #endregion

                    #region ICMS40

                    if (nfeDet.nfeImposto.nfeICMS40.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS40 tNFeInfNFeDetImpostoICMSICMS40 = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS40();

                        if (nfeDet.nfeImposto.nfeICMS40.CST == "40")
                            tNFeInfNFeDetImpostoICMSICMS40.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS40CST.Item40;
                        if (nfeDet.nfeImposto.nfeICMS40.CST == "41")
                            tNFeInfNFeDetImpostoICMSICMS40.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS40CST.Item41;
                        if (nfeDet.nfeImposto.nfeICMS40.CST == "50")
                            tNFeInfNFeDetImpostoICMSICMS40.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS40CST.Item50;

                        if (nfeDet.nfeImposto.nfeICMS40.orig == 0)
                            tNFeInfNFeDetImpostoICMSICMS40.orig = ValidateLib.enviNFe_ns.Torig.Item0;
                        if (nfeDet.nfeImposto.nfeICMS40.orig == 1)
                            tNFeInfNFeDetImpostoICMSICMS40.orig = ValidateLib.enviNFe_ns.Torig.Item1;
                        if (nfeDet.nfeImposto.nfeICMS40.orig == 2)
                            tNFeInfNFeDetImpostoICMSICMS40.orig = ValidateLib.enviNFe_ns.Torig.Item2;
                        if (nfeDet.nfeImposto.nfeICMS40.orig == 3)
                            tNFeInfNFeDetImpostoICMSICMS40.orig = ValidateLib.enviNFe_ns.Torig.Item3;
                        if (nfeDet.nfeImposto.nfeICMS40.orig == 4)
                            tNFeInfNFeDetImpostoICMSICMS40.orig = ValidateLib.enviNFe_ns.Torig.Item4;
                        if (nfeDet.nfeImposto.nfeICMS40.orig == 5)
                            tNFeInfNFeDetImpostoICMSICMS40.orig = ValidateLib.enviNFe_ns.Torig.Item5;
                        if (nfeDet.nfeImposto.nfeICMS40.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMS40.orig = ValidateLib.enviNFe_ns.Torig.Item6;
                        if (nfeDet.nfeImposto.nfeICMS40.orig == 7)
                            tNFeInfNFeDetImpostoICMSICMS40.orig = ValidateLib.enviNFe_ns.Torig.Item7;
                        if (nfeDet.nfeImposto.nfeICMS40.orig == 8)
                            tNFeInfNFeDetImpostoICMSICMS40.orig = ValidateLib.enviNFe_ns.Torig.Item8;

                        if (nfeDet.nfeImposto.nfeICMS40.vICMSDeson > 0)
                        {
                            tNFeInfNFeDetImpostoICMSICMS40.vICMSDeson = nfeDet.nfeImposto.nfeICMS40.vICMSDeson.ToString().Replace(".", "").Replace(",", ".");

                            if (nfeDet.nfeImposto.nfeICMS40.motDesICMS == 1)
                                tNFeInfNFeDetImpostoICMSICMS40.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS40MotDesICMS.Item1;
                            if (nfeDet.nfeImposto.nfeICMS40.motDesICMS == 10)
                                tNFeInfNFeDetImpostoICMSICMS40.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS40MotDesICMS.Item10;
                            if (nfeDet.nfeImposto.nfeICMS40.motDesICMS == 11)
                                tNFeInfNFeDetImpostoICMSICMS40.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS40MotDesICMS.Item11;
                            if (nfeDet.nfeImposto.nfeICMS40.motDesICMS == 3)
                                tNFeInfNFeDetImpostoICMSICMS40.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS40MotDesICMS.Item3;
                            if (nfeDet.nfeImposto.nfeICMS40.motDesICMS == 4)
                                tNFeInfNFeDetImpostoICMSICMS40.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS40MotDesICMS.Item4;
                            if (nfeDet.nfeImposto.nfeICMS40.motDesICMS == 5)
                                tNFeInfNFeDetImpostoICMSICMS40.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS40MotDesICMS.Item5;
                            if (nfeDet.nfeImposto.nfeICMS40.motDesICMS == 6)
                                tNFeInfNFeDetImpostoICMSICMS40.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS40MotDesICMS.Item6;
                            if (nfeDet.nfeImposto.nfeICMS40.motDesICMS == 7)
                                tNFeInfNFeDetImpostoICMSICMS40.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS40MotDesICMS.Item7;
                            if (nfeDet.nfeImposto.nfeICMS40.motDesICMS == 8)
                                tNFeInfNFeDetImpostoICMSICMS40.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS40MotDesICMS.Item8;
                            if (nfeDet.nfeImposto.nfeICMS40.motDesICMS == 9)
                                tNFeInfNFeDetImpostoICMSICMS40.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS40MotDesICMS.Item9;
                        }

                        tNFeInfNFeDetImpostoICMS.Item = tNFeInfNFeDetImpostoICMSICMS40;
                        limposto.Add(tNFeInfNFeDetImpostoICMS);
                    }

                    #endregion

                    #region ICMS51

                    if (nfeDet.nfeImposto.nfeICMS51.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS51 tNFeInfNFeDetImpostoICMSICMS51 = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS51();
                        tNFeInfNFeDetImpostoICMSICMS51.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS51CST.Item51;

                        if (nfeDet.nfeImposto.nfeICMS51.modBC == 0)
                            tNFeInfNFeDetImpostoICMSICMS51.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS51ModBC.Item0;
                        if (nfeDet.nfeImposto.nfeICMS51.modBC == 1)
                            tNFeInfNFeDetImpostoICMSICMS51.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS51ModBC.Item1;
                        if (nfeDet.nfeImposto.nfeICMS51.modBC == 2)
                            tNFeInfNFeDetImpostoICMSICMS51.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS51ModBC.Item2;
                        if (nfeDet.nfeImposto.nfeICMS51.modBC == 3)
                            tNFeInfNFeDetImpostoICMSICMS51.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS51ModBC.Item3;

                        //tNFeInfNFeDetImpostoICMSICMS51.modBCSpecified = true;

                        if (nfeDet.nfeImposto.nfeICMS51.orig == 0)
                            tNFeInfNFeDetImpostoICMSICMS51.orig = ValidateLib.enviNFe_ns.Torig.Item0;
                        if (nfeDet.nfeImposto.nfeICMS51.orig == 1)
                            tNFeInfNFeDetImpostoICMSICMS51.orig = ValidateLib.enviNFe_ns.Torig.Item1;
                        if (nfeDet.nfeImposto.nfeICMS51.orig == 2)
                            tNFeInfNFeDetImpostoICMSICMS51.orig = ValidateLib.enviNFe_ns.Torig.Item2;
                        if (nfeDet.nfeImposto.nfeICMS51.orig == 3)
                            tNFeInfNFeDetImpostoICMSICMS51.orig = ValidateLib.enviNFe_ns.Torig.Item3;
                        if (nfeDet.nfeImposto.nfeICMS51.orig == 4)
                            tNFeInfNFeDetImpostoICMSICMS51.orig = ValidateLib.enviNFe_ns.Torig.Item4;
                        if (nfeDet.nfeImposto.nfeICMS51.orig == 5)
                            tNFeInfNFeDetImpostoICMSICMS51.orig = ValidateLib.enviNFe_ns.Torig.Item5;
                        if (nfeDet.nfeImposto.nfeICMS51.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMS51.orig = ValidateLib.enviNFe_ns.Torig.Item6;
                        if (nfeDet.nfeImposto.nfeICMS51.orig == 7)
                            tNFeInfNFeDetImpostoICMSICMS51.orig = ValidateLib.enviNFe_ns.Torig.Item7;
                        if (nfeDet.nfeImposto.nfeICMS51.orig == 8)
                            tNFeInfNFeDetImpostoICMSICMS51.orig = ValidateLib.enviNFe_ns.Torig.Item8;

                        tNFeInfNFeDetImpostoICMSICMS51.pDif = nfeDet.nfeImposto.nfeICMS51.pDif.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS51.pICMS = nfeDet.nfeImposto.nfeICMS51.pICMS.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS51.pRedBC = nfeDet.nfeImposto.nfeICMS51.pRedBC.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS51.vBC = nfeDet.nfeImposto.nfeICMS51.vBC.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS51.vICMS = nfeDet.nfeImposto.nfeICMS51.vICMS.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS51.vICMSDif = nfeDet.nfeImposto.nfeICMS51.vICMSDif.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS51.vICMSOp = nfeDet.nfeImposto.nfeICMS51.vICMSOp.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoICMS.Item = tNFeInfNFeDetImpostoICMSICMS51;
                        limposto.Add(tNFeInfNFeDetImpostoICMS);
                    }

                    #endregion

                    #region ICMS60

                    if (nfeDet.nfeImposto.nfeICMS60.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS60 tNFeInfNFeDetImpostoICMSICMS60 = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS60();
                        tNFeInfNFeDetImpostoICMSICMS60.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS60CST.Item60;

                        if (nfeDet.nfeImposto.nfeICMS60.orig == 0)
                            tNFeInfNFeDetImpostoICMSICMS60.orig = ValidateLib.enviNFe_ns.Torig.Item0;
                        if (nfeDet.nfeImposto.nfeICMS60.orig == 1)
                            tNFeInfNFeDetImpostoICMSICMS60.orig = ValidateLib.enviNFe_ns.Torig.Item1;
                        if (nfeDet.nfeImposto.nfeICMS60.orig == 2)
                            tNFeInfNFeDetImpostoICMSICMS60.orig = ValidateLib.enviNFe_ns.Torig.Item2;
                        if (nfeDet.nfeImposto.nfeICMS60.orig == 3)
                            tNFeInfNFeDetImpostoICMSICMS60.orig = ValidateLib.enviNFe_ns.Torig.Item3;
                        if (nfeDet.nfeImposto.nfeICMS60.orig == 4)
                            tNFeInfNFeDetImpostoICMSICMS60.orig = ValidateLib.enviNFe_ns.Torig.Item4;
                        if (nfeDet.nfeImposto.nfeICMS60.orig == 5)
                            tNFeInfNFeDetImpostoICMSICMS60.orig = ValidateLib.enviNFe_ns.Torig.Item5;
                        if (nfeDet.nfeImposto.nfeICMS60.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMS60.orig = ValidateLib.enviNFe_ns.Torig.Item6;
                        if (nfeDet.nfeImposto.nfeICMS60.orig == 7)
                            tNFeInfNFeDetImpostoICMSICMS60.orig = ValidateLib.enviNFe_ns.Torig.Item7;
                        if (nfeDet.nfeImposto.nfeICMS60.orig == 8)
                            tNFeInfNFeDetImpostoICMSICMS60.orig = ValidateLib.enviNFe_ns.Torig.Item8;

                        tNFeInfNFeDetImpostoICMSICMS60.vBCSTRet = nfeDet.nfeImposto.nfeICMS60.vBCSTRet.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS60.vICMSSTRet = nfeDet.nfeImposto.nfeICMS60.vICMSSTRet.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoICMS.Item = tNFeInfNFeDetImpostoICMSICMS60;
                        limposto.Add(tNFeInfNFeDetImpostoICMS);
                    }

                    #endregion

                    #region ICMS70

                    if (nfeDet.nfeImposto.nfeICMS70.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS70 tNFeInfNFeDetImpostoICMSICMS70 = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS70();
                        tNFeInfNFeDetImpostoICMSICMS70.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS70CST.Item70;

                        if (nfeDet.nfeImposto.nfeICMS70.modBC == 0)
                            tNFeInfNFeDetImpostoICMSICMS70.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS70ModBC.Item0;
                        if (nfeDet.nfeImposto.nfeICMS70.modBC == 1)
                            tNFeInfNFeDetImpostoICMSICMS70.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS70ModBC.Item1;
                        if (nfeDet.nfeImposto.nfeICMS70.modBC == 2)
                            tNFeInfNFeDetImpostoICMSICMS70.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS70ModBC.Item2;
                        if (nfeDet.nfeImposto.nfeICMS70.modBC == 3)
                            tNFeInfNFeDetImpostoICMSICMS70.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS70ModBC.Item3;

                        if (nfeDet.nfeImposto.nfeICMS70.modBCST == 0)
                            tNFeInfNFeDetImpostoICMSICMS70.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS70ModBCST.Item0;
                        if (nfeDet.nfeImposto.nfeICMS70.modBCST == 1)
                            tNFeInfNFeDetImpostoICMSICMS70.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS70ModBCST.Item1;
                        if (nfeDet.nfeImposto.nfeICMS70.modBCST == 2)
                            tNFeInfNFeDetImpostoICMSICMS70.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS70ModBCST.Item2;
                        if (nfeDet.nfeImposto.nfeICMS70.modBCST == 3)
                            tNFeInfNFeDetImpostoICMSICMS70.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS70ModBCST.Item3;
                        if (nfeDet.nfeImposto.nfeICMS70.modBCST == 4)
                            tNFeInfNFeDetImpostoICMSICMS70.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS70ModBCST.Item4;
                        if (nfeDet.nfeImposto.nfeICMS70.modBCST == 5)
                            tNFeInfNFeDetImpostoICMSICMS70.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS70ModBCST.Item5;

                        if (nfeDet.nfeImposto.nfeICMS70.motDesICMS == 3)
                            tNFeInfNFeDetImpostoICMSICMS70.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS70MotDesICMS.Item3;
                        if (nfeDet.nfeImposto.nfeICMS70.motDesICMS == 9)
                            tNFeInfNFeDetImpostoICMSICMS70.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS70MotDesICMS.Item9;
                        if (nfeDet.nfeImposto.nfeICMS70.motDesICMS == 12)
                            tNFeInfNFeDetImpostoICMSICMS70.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS70MotDesICMS.Item12;

                        if (nfeDet.nfeImposto.nfeICMS70.orig == 0)
                            tNFeInfNFeDetImpostoICMSICMS70.orig = ValidateLib.enviNFe_ns.Torig.Item0;
                        if (nfeDet.nfeImposto.nfeICMS70.orig == 1)
                            tNFeInfNFeDetImpostoICMSICMS70.orig = ValidateLib.enviNFe_ns.Torig.Item1;
                        if (nfeDet.nfeImposto.nfeICMS70.orig == 2)
                            tNFeInfNFeDetImpostoICMSICMS70.orig = ValidateLib.enviNFe_ns.Torig.Item2;
                        if (nfeDet.nfeImposto.nfeICMS70.orig == 3)
                            tNFeInfNFeDetImpostoICMSICMS70.orig = ValidateLib.enviNFe_ns.Torig.Item3;
                        if (nfeDet.nfeImposto.nfeICMS70.orig == 4)
                            tNFeInfNFeDetImpostoICMSICMS70.orig = ValidateLib.enviNFe_ns.Torig.Item4;
                        if (nfeDet.nfeImposto.nfeICMS70.orig == 5)
                            tNFeInfNFeDetImpostoICMSICMS70.orig = ValidateLib.enviNFe_ns.Torig.Item5;
                        if (nfeDet.nfeImposto.nfeICMS70.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMS70.orig = ValidateLib.enviNFe_ns.Torig.Item6;
                        if (nfeDet.nfeImposto.nfeICMS70.orig == 7)
                            tNFeInfNFeDetImpostoICMSICMS70.orig = ValidateLib.enviNFe_ns.Torig.Item7;
                        if (nfeDet.nfeImposto.nfeICMS70.orig == 8)
                            tNFeInfNFeDetImpostoICMSICMS70.orig = ValidateLib.enviNFe_ns.Torig.Item8;

                        tNFeInfNFeDetImpostoICMSICMS70.pICMS = nfeDet.nfeImposto.nfeICMS70.pICMS.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS70.pICMSST = nfeDet.nfeImposto.nfeICMS70.pICMSST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS70.pMVAST = nfeDet.nfeImposto.nfeICMS70.pMVAST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS70.pRedBC = nfeDet.nfeImposto.nfeICMS70.pRedBC.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS70.pRedBCST = nfeDet.nfeImposto.nfeICMS70.pRedBCST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS70.vBC = nfeDet.nfeImposto.nfeICMS70.vBC.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS70.vBCST = nfeDet.nfeImposto.nfeICMS70.vBCST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS70.vICMS = nfeDet.nfeImposto.nfeICMS70.vICMS.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS70.vICMSDeson = nfeDet.nfeImposto.nfeICMS70.vICMSDeson.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS70.vICMSST = nfeDet.nfeImposto.nfeICMS70.vICMSST.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoICMS.Item = tNFeInfNFeDetImpostoICMSICMS70;
                        limposto.Add(tNFeInfNFeDetImpostoICMS);
                    }

                    #endregion

                    #region ICMS90

                    if (nfeDet.nfeImposto.nfeICMS90.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS90 tNFeInfNFeDetImpostoICMSICMS90 = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS90();
                        tNFeInfNFeDetImpostoICMSICMS90.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS90CST.Item90;

                        if (nfeDet.nfeImposto.nfeICMS90.modBC == 0)
                            tNFeInfNFeDetImpostoICMSICMS90.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS90ModBC.Item0;
                        if (nfeDet.nfeImposto.nfeICMS90.modBC == 1)
                            tNFeInfNFeDetImpostoICMSICMS90.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS90ModBC.Item1;
                        if (nfeDet.nfeImposto.nfeICMS90.modBC == 2)
                            tNFeInfNFeDetImpostoICMSICMS90.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS90ModBC.Item2;
                        if (nfeDet.nfeImposto.nfeICMS90.modBC == 3)
                            tNFeInfNFeDetImpostoICMSICMS90.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS90ModBC.Item3;

                        if (nfeDet.nfeImposto.nfeICMS90.modBCST == 0)
                            tNFeInfNFeDetImpostoICMSICMS90.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS90ModBCST.Item0;
                        if (nfeDet.nfeImposto.nfeICMS90.modBCST == 1)
                            tNFeInfNFeDetImpostoICMSICMS90.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS90ModBCST.Item1;
                        if (nfeDet.nfeImposto.nfeICMS90.modBCST == 2)
                            tNFeInfNFeDetImpostoICMSICMS90.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS90ModBCST.Item2;
                        if (nfeDet.nfeImposto.nfeICMS90.modBCST == 3)
                            tNFeInfNFeDetImpostoICMSICMS90.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS90ModBCST.Item3;
                        if (nfeDet.nfeImposto.nfeICMS90.modBCST == 4)
                            tNFeInfNFeDetImpostoICMSICMS90.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS90ModBCST.Item4;
                        if (nfeDet.nfeImposto.nfeICMS90.modBCST == 5)
                            tNFeInfNFeDetImpostoICMSICMS90.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS90ModBCST.Item5;

                        //if (nfeDet.nfeImposto.nfeICMS90.motDesICMS == 3)
                        //    tNFeInfNFeDetImpostoICMSICMS90.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS90MotDesICMS.Item3;
                        //if (nfeDet.nfeImposto.nfeICMS90.motDesICMS == 9)
                        //    tNFeInfNFeDetImpostoICMSICMS90.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS90MotDesICMS.Item9;
                        //if (nfeDet.nfeImposto.nfeICMS90.motDesICMS == 12)
                        //    tNFeInfNFeDetImpostoICMSICMS90.motDesICMS = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMS90MotDesICMS.Item12;

                        if (nfeDet.nfeImposto.nfeICMS90.orig == 0)
                            tNFeInfNFeDetImpostoICMSICMS90.orig = ValidateLib.enviNFe_ns.Torig.Item0;
                        if (nfeDet.nfeImposto.nfeICMS90.orig == 1)
                            tNFeInfNFeDetImpostoICMSICMS90.orig = ValidateLib.enviNFe_ns.Torig.Item1;
                        if (nfeDet.nfeImposto.nfeICMS90.orig == 2)
                            tNFeInfNFeDetImpostoICMSICMS90.orig = ValidateLib.enviNFe_ns.Torig.Item2;
                        if (nfeDet.nfeImposto.nfeICMS90.orig == 3)
                            tNFeInfNFeDetImpostoICMSICMS90.orig = ValidateLib.enviNFe_ns.Torig.Item3;
                        if (nfeDet.nfeImposto.nfeICMS90.orig == 4)
                            tNFeInfNFeDetImpostoICMSICMS90.orig = ValidateLib.enviNFe_ns.Torig.Item4;
                        if (nfeDet.nfeImposto.nfeICMS90.orig == 5)
                            tNFeInfNFeDetImpostoICMSICMS90.orig = ValidateLib.enviNFe_ns.Torig.Item5;
                        if (nfeDet.nfeImposto.nfeICMS90.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMS90.orig = ValidateLib.enviNFe_ns.Torig.Item6;
                        if (nfeDet.nfeImposto.nfeICMS90.orig == 7)
                            tNFeInfNFeDetImpostoICMSICMS90.orig = ValidateLib.enviNFe_ns.Torig.Item7;
                        if (nfeDet.nfeImposto.nfeICMS90.orig == 8)
                            tNFeInfNFeDetImpostoICMSICMS90.orig = ValidateLib.enviNFe_ns.Torig.Item8;

                        tNFeInfNFeDetImpostoICMSICMS90.pICMS = nfeDet.nfeImposto.nfeICMS90.pICMS.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS90.pICMSST = nfeDet.nfeImposto.nfeICMS90.pICMSST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS90.pMVAST = nfeDet.nfeImposto.nfeICMS90.pMVAST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS90.pRedBC = nfeDet.nfeImposto.nfeICMS90.pRedBC.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS90.pRedBCST = nfeDet.nfeImposto.nfeICMS90.pRedBCST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS90.vBC = nfeDet.nfeImposto.nfeICMS90.vBC.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS90.vBCST = nfeDet.nfeImposto.nfeICMS90.vBCST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS90.vICMS = nfeDet.nfeImposto.nfeICMS90.vICMS.ToString().Replace(".", "").Replace(",", ".");
                        //tNFeInfNFeDetImpostoICMSICMS90.vICMSDeson = nfeDet.nfeImposto.nfeICMS90.vICMSDeson.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMS90.vICMSST = nfeDet.nfeImposto.nfeICMS90.vICMSST.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoICMS.Item = tNFeInfNFeDetImpostoICMSICMS90;
                        limposto.Add(tNFeInfNFeDetImpostoICMS);
                    }

                    #endregion

                    #region ICMSPart

                    if (nfeDet.nfeImposto.nfeICMSPart.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSPart tNFeInfNFeDetImpostoICMSICMSPart = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSPart();

                        if (nfeDet.nfeImposto.nfeICMSPart.CST == "10")
                            tNFeInfNFeDetImpostoICMSICMSPart.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSPartCST.Item10;
                        if (nfeDet.nfeImposto.nfeICMSPart.CST == "90")
                            tNFeInfNFeDetImpostoICMSICMSPart.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSPartCST.Item90;

                        if (nfeDet.nfeImposto.nfeICMSPart.modBC == 0)
                            tNFeInfNFeDetImpostoICMSICMSPart.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSPartModBC.Item0;
                        if (nfeDet.nfeImposto.nfeICMSPart.modBC == 1)
                            tNFeInfNFeDetImpostoICMSICMSPart.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSPartModBC.Item1;
                        if (nfeDet.nfeImposto.nfeICMSPart.modBC == 2)
                            tNFeInfNFeDetImpostoICMSICMSPart.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSPartModBC.Item2;
                        if (nfeDet.nfeImposto.nfeICMSPart.modBC == 3)
                            tNFeInfNFeDetImpostoICMSICMSPart.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSPartModBC.Item3;

                        if (nfeDet.nfeImposto.nfeICMSPart.modBCST == 0)
                            tNFeInfNFeDetImpostoICMSICMSPart.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSPartModBCST.Item0;
                        if (nfeDet.nfeImposto.nfeICMSPart.modBCST == 1)
                            tNFeInfNFeDetImpostoICMSICMSPart.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSPartModBCST.Item1;
                        if (nfeDet.nfeImposto.nfeICMSPart.modBCST == 2)
                            tNFeInfNFeDetImpostoICMSICMSPart.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSPartModBCST.Item2;
                        if (nfeDet.nfeImposto.nfeICMSPart.modBCST == 3)
                            tNFeInfNFeDetImpostoICMSICMSPart.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSPartModBCST.Item3;
                        if (nfeDet.nfeImposto.nfeICMSPart.modBCST == 4)
                            tNFeInfNFeDetImpostoICMSICMSPart.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSPartModBCST.Item4;
                        if (nfeDet.nfeImposto.nfeICMSPart.modBCST == 5)
                            tNFeInfNFeDetImpostoICMSICMSPart.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSPartModBCST.Item5;

                        if (nfeDet.nfeImposto.nfeICMSPart.orig == 0)
                            tNFeInfNFeDetImpostoICMSICMSPart.orig = ValidateLib.enviNFe_ns.Torig.Item0;
                        if (nfeDet.nfeImposto.nfeICMSPart.orig == 1)
                            tNFeInfNFeDetImpostoICMSICMSPart.orig = ValidateLib.enviNFe_ns.Torig.Item1;
                        if (nfeDet.nfeImposto.nfeICMSPart.orig == 2)
                            tNFeInfNFeDetImpostoICMSICMSPart.orig = ValidateLib.enviNFe_ns.Torig.Item2;
                        if (nfeDet.nfeImposto.nfeICMSPart.orig == 3)
                            tNFeInfNFeDetImpostoICMSICMSPart.orig = ValidateLib.enviNFe_ns.Torig.Item3;
                        if (nfeDet.nfeImposto.nfeICMSPart.orig == 4)
                            tNFeInfNFeDetImpostoICMSICMSPart.orig = ValidateLib.enviNFe_ns.Torig.Item4;
                        if (nfeDet.nfeImposto.nfeICMSPart.orig == 5)
                            tNFeInfNFeDetImpostoICMSICMSPart.orig = ValidateLib.enviNFe_ns.Torig.Item5;
                        if (nfeDet.nfeImposto.nfeICMSPart.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMSPart.orig = ValidateLib.enviNFe_ns.Torig.Item6;
                        if (nfeDet.nfeImposto.nfeICMSPart.orig == 7)
                            tNFeInfNFeDetImpostoICMSICMSPart.orig = ValidateLib.enviNFe_ns.Torig.Item7;
                        if (nfeDet.nfeImposto.nfeICMSPart.orig == 8)
                            tNFeInfNFeDetImpostoICMSICMSPart.orig = ValidateLib.enviNFe_ns.Torig.Item8;

                        tNFeInfNFeDetImpostoICMSICMSPart.pBCOp = nfeDet.nfeImposto.nfeICMSPart.pBCOp.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSPart.pICMS = nfeDet.nfeImposto.nfeICMSPart.pICMS.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSPart.pICMSST = nfeDet.nfeImposto.nfeICMSPart.pICMSST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSPart.pMVAST = nfeDet.nfeImposto.nfeICMSPart.pMVAST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSPart.pRedBC = nfeDet.nfeImposto.nfeICMSPart.pRedBC.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSPart.pRedBCST = nfeDet.nfeImposto.nfeICMSPart.pRedBCST.ToString().Replace(".", "").Replace(",", ".");

                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "AC")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.AC;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "AL")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.AL;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "AM")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.AM;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "AP")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.AP;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "BA")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.BA;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "CE")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.CE;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "DF")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.DF;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "ES")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.ES;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "EX")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.EX;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "GO")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.GO;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "MA")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.MA;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "MG")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.MG;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "MS")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.MS;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "MT")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.MT;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "PA")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.PA;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "PB")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.PB;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "PE")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.PE;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "PI")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.PI;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "PR")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.PR;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "RJ")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.RJ;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "RN")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.RN;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "RO")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.RO;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "RR")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.RR;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "RS")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.RS;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "SC")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.SC;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "SE")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.SE;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "SP")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.SP;
                        if (nfeDet.nfeImposto.nfeICMSPart.UFST == "TO")
                            tNFeInfNFeDetImpostoICMSICMSPart.UFST = ValidateLib.enviNFe_ns.TUf.TO;

                        tNFeInfNFeDetImpostoICMSICMSPart.pRedBCST = nfeDet.nfeImposto.nfeICMSPart.pRedBCST.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoICMS.Item = tNFeInfNFeDetImpostoICMSICMSPart;
                        limposto.Add(tNFeInfNFeDetImpostoICMS);
                    }

                    #endregion

                    #region ICMSSN101

                    if (nfeDet.nfeImposto.nfeICMSSN101.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN101 tNFeInfNFeDetImpostoICMSICMSSN101 = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN101();

                        tNFeInfNFeDetImpostoICMSICMSSN101.CSOSN = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN101CSOSN.Item101;

                        if (nfeDet.nfeImposto.nfeICMSSN101.orig == 0)
                            tNFeInfNFeDetImpostoICMSICMSSN101.orig = ValidateLib.enviNFe_ns.Torig.Item0;
                        if (nfeDet.nfeImposto.nfeICMSSN101.orig == 1)
                            tNFeInfNFeDetImpostoICMSICMSSN101.orig = ValidateLib.enviNFe_ns.Torig.Item1;
                        if (nfeDet.nfeImposto.nfeICMSSN101.orig == 2)
                            tNFeInfNFeDetImpostoICMSICMSSN101.orig = ValidateLib.enviNFe_ns.Torig.Item2;
                        if (nfeDet.nfeImposto.nfeICMSSN101.orig == 3)
                            tNFeInfNFeDetImpostoICMSICMSSN101.orig = ValidateLib.enviNFe_ns.Torig.Item3;
                        if (nfeDet.nfeImposto.nfeICMSSN101.orig == 4)
                            tNFeInfNFeDetImpostoICMSICMSSN101.orig = ValidateLib.enviNFe_ns.Torig.Item4;
                        if (nfeDet.nfeImposto.nfeICMSSN101.orig == 5)
                            tNFeInfNFeDetImpostoICMSICMSSN101.orig = ValidateLib.enviNFe_ns.Torig.Item5;
                        if (nfeDet.nfeImposto.nfeICMSSN101.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMSSN101.orig = ValidateLib.enviNFe_ns.Torig.Item6;
                        if (nfeDet.nfeImposto.nfeICMSSN101.orig == 7)
                            tNFeInfNFeDetImpostoICMSICMSSN101.orig = ValidateLib.enviNFe_ns.Torig.Item7;
                        if (nfeDet.nfeImposto.nfeICMSSN101.orig == 8)
                            tNFeInfNFeDetImpostoICMSICMSSN101.orig = ValidateLib.enviNFe_ns.Torig.Item8;

                        tNFeInfNFeDetImpostoICMSICMSSN101.pCredSN = nfeDet.nfeImposto.nfeICMSSN101.pCredSN.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN101.vCredICMSSN = nfeDet.nfeImposto.nfeICMSSN101.vCredICMSSN.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoICMS.Item = tNFeInfNFeDetImpostoICMSICMSSN101;
                        limposto.Add(tNFeInfNFeDetImpostoICMS);
                    }

                    #endregion

                    #region ICMSSN102

                    if (nfeDet.nfeImposto.nfeICMSSN102.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN102 tNFeInfNFeDetImpostoICMSICMSSN102 = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN102();

                        if (nfeDet.nfeImposto.nfeICMSSN102.CSOSN == 102)
                            tNFeInfNFeDetImpostoICMSICMSSN102.CSOSN = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN102CSOSN.Item102;
                        if (nfeDet.nfeImposto.nfeICMSSN102.CSOSN == 103)
                            tNFeInfNFeDetImpostoICMSICMSSN102.CSOSN = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN102CSOSN.Item103;
                        if (nfeDet.nfeImposto.nfeICMSSN102.CSOSN == 300)
                            tNFeInfNFeDetImpostoICMSICMSSN102.CSOSN = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN102CSOSN.Item300;
                        if (nfeDet.nfeImposto.nfeICMSSN102.CSOSN == 400)
                            tNFeInfNFeDetImpostoICMSICMSSN102.CSOSN = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN102CSOSN.Item400;

                        if (nfeDet.nfeImposto.nfeICMSSN102.orig == 0)
                            tNFeInfNFeDetImpostoICMSICMSSN102.orig = ValidateLib.enviNFe_ns.Torig.Item0;
                        if (nfeDet.nfeImposto.nfeICMSSN102.orig == 1)
                            tNFeInfNFeDetImpostoICMSICMSSN102.orig = ValidateLib.enviNFe_ns.Torig.Item1;
                        if (nfeDet.nfeImposto.nfeICMSSN102.orig == 2)
                            tNFeInfNFeDetImpostoICMSICMSSN102.orig = ValidateLib.enviNFe_ns.Torig.Item2;
                        if (nfeDet.nfeImposto.nfeICMSSN102.orig == 3)
                            tNFeInfNFeDetImpostoICMSICMSSN102.orig = ValidateLib.enviNFe_ns.Torig.Item3;
                        if (nfeDet.nfeImposto.nfeICMSSN102.orig == 4)
                            tNFeInfNFeDetImpostoICMSICMSSN102.orig = ValidateLib.enviNFe_ns.Torig.Item4;
                        if (nfeDet.nfeImposto.nfeICMSSN102.orig == 5)
                            tNFeInfNFeDetImpostoICMSICMSSN102.orig = ValidateLib.enviNFe_ns.Torig.Item5;
                        if (nfeDet.nfeImposto.nfeICMSSN102.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMSSN102.orig = ValidateLib.enviNFe_ns.Torig.Item6;
                        if (nfeDet.nfeImposto.nfeICMSSN102.orig == 7)
                            tNFeInfNFeDetImpostoICMSICMSSN102.orig = ValidateLib.enviNFe_ns.Torig.Item7;
                        if (nfeDet.nfeImposto.nfeICMSSN102.orig == 8)
                            tNFeInfNFeDetImpostoICMSICMSSN102.orig = ValidateLib.enviNFe_ns.Torig.Item8;

                        tNFeInfNFeDetImpostoICMS.Item = tNFeInfNFeDetImpostoICMSICMSSN102;
                        limposto.Add(tNFeInfNFeDetImpostoICMS);
                    }

                    #endregion

                    #region ICMSSN201

                    if (nfeDet.nfeImposto.nfeICMSSN201.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN201 tNFeInfNFeDetImpostoICMSICMSSN201 = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN201();

                        tNFeInfNFeDetImpostoICMSICMSSN201.CSOSN = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN201CSOSN.Item201;

                        if (nfeDet.nfeImposto.nfeICMSSN201.modBCST == 0)
                            tNFeInfNFeDetImpostoICMSICMSSN201.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN201ModBCST.Item0;
                        if (nfeDet.nfeImposto.nfeICMSSN201.modBCST == 1)
                            tNFeInfNFeDetImpostoICMSICMSSN201.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN201ModBCST.Item1;
                        if (nfeDet.nfeImposto.nfeICMSSN201.modBCST == 2)
                            tNFeInfNFeDetImpostoICMSICMSSN201.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN201ModBCST.Item2;
                        if (nfeDet.nfeImposto.nfeICMSSN201.modBCST == 3)
                            tNFeInfNFeDetImpostoICMSICMSSN201.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN201ModBCST.Item3;
                        if (nfeDet.nfeImposto.nfeICMSSN201.modBCST == 4)
                            tNFeInfNFeDetImpostoICMSICMSSN201.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN201ModBCST.Item4;
                        if (nfeDet.nfeImposto.nfeICMSSN201.modBCST == 5)
                            tNFeInfNFeDetImpostoICMSICMSSN201.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN201ModBCST.Item5;

                        if (nfeDet.nfeImposto.nfeICMSSN201.orig == 0)
                            tNFeInfNFeDetImpostoICMSICMSSN201.orig = ValidateLib.enviNFe_ns.Torig.Item0;
                        if (nfeDet.nfeImposto.nfeICMSSN201.orig == 1)
                            tNFeInfNFeDetImpostoICMSICMSSN201.orig = ValidateLib.enviNFe_ns.Torig.Item1;
                        if (nfeDet.nfeImposto.nfeICMSSN201.orig == 2)
                            tNFeInfNFeDetImpostoICMSICMSSN201.orig = ValidateLib.enviNFe_ns.Torig.Item2;
                        if (nfeDet.nfeImposto.nfeICMSSN201.orig == 3)
                            tNFeInfNFeDetImpostoICMSICMSSN201.orig = ValidateLib.enviNFe_ns.Torig.Item3;
                        if (nfeDet.nfeImposto.nfeICMSSN201.orig == 4)
                            tNFeInfNFeDetImpostoICMSICMSSN201.orig = ValidateLib.enviNFe_ns.Torig.Item4;
                        if (nfeDet.nfeImposto.nfeICMSSN201.orig == 5)
                            tNFeInfNFeDetImpostoICMSICMSSN201.orig = ValidateLib.enviNFe_ns.Torig.Item5;
                        if (nfeDet.nfeImposto.nfeICMSSN201.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMSSN201.orig = ValidateLib.enviNFe_ns.Torig.Item6;
                        if (nfeDet.nfeImposto.nfeICMSSN201.orig == 7)
                            tNFeInfNFeDetImpostoICMSICMSSN201.orig = ValidateLib.enviNFe_ns.Torig.Item7;
                        if (nfeDet.nfeImposto.nfeICMSSN201.orig == 8)
                            tNFeInfNFeDetImpostoICMSICMSSN201.orig = ValidateLib.enviNFe_ns.Torig.Item8;

                        tNFeInfNFeDetImpostoICMSICMSSN201.pCredSN = nfeDet.nfeImposto.nfeICMSSN201.pCredSN.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN201.pICMSST = nfeDet.nfeImposto.nfeICMSSN201.pICMSST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN201.pMVAST = nfeDet.nfeImposto.nfeICMSSN201.pMVAST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN201.pRedBCST = nfeDet.nfeImposto.nfeICMSSN201.pRedBCST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN201.vBCST = nfeDet.nfeImposto.nfeICMSSN201.vBCST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN201.vCredICMSSN = nfeDet.nfeImposto.nfeICMSSN201.vCredICMSSN.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN201.vICMSST = nfeDet.nfeImposto.nfeICMSSN201.vICMSST.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoICMS.Item = tNFeInfNFeDetImpostoICMSICMSSN201;
                        limposto.Add(tNFeInfNFeDetImpostoICMS);
                    }

                    #endregion

                    #region ICMSSN202

                    if (nfeDet.nfeImposto.nfeICMSSN202.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN202 tNFeInfNFeDetImpostoICMSICMSSN202 = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN202();

                        if (nfeDet.nfeImposto.nfeICMSSN202.CSOSN == 202)
                            tNFeInfNFeDetImpostoICMSICMSSN202.CSOSN = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN202CSOSN.Item202;
                        if (nfeDet.nfeImposto.nfeICMSSN202.CSOSN == 203)
                            tNFeInfNFeDetImpostoICMSICMSSN202.CSOSN = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN202CSOSN.Item203;

                        if (nfeDet.nfeImposto.nfeICMSSN202.modBCST == 0)
                            tNFeInfNFeDetImpostoICMSICMSSN202.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN202ModBCST.Item0;
                        if (nfeDet.nfeImposto.nfeICMSSN202.modBCST == 1)
                            tNFeInfNFeDetImpostoICMSICMSSN202.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN202ModBCST.Item1;
                        if (nfeDet.nfeImposto.nfeICMSSN202.modBCST == 2)
                            tNFeInfNFeDetImpostoICMSICMSSN202.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN202ModBCST.Item2;
                        if (nfeDet.nfeImposto.nfeICMSSN202.modBCST == 3)
                            tNFeInfNFeDetImpostoICMSICMSSN202.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN202ModBCST.Item3;
                        if (nfeDet.nfeImposto.nfeICMSSN202.modBCST == 4)
                            tNFeInfNFeDetImpostoICMSICMSSN202.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN202ModBCST.Item4;
                        if (nfeDet.nfeImposto.nfeICMSSN202.modBCST == 5)
                            tNFeInfNFeDetImpostoICMSICMSSN202.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN202ModBCST.Item5;

                        if (nfeDet.nfeImposto.nfeICMSSN202.orig == 0)
                            tNFeInfNFeDetImpostoICMSICMSSN202.orig = ValidateLib.enviNFe_ns.Torig.Item0;
                        if (nfeDet.nfeImposto.nfeICMSSN202.orig == 1)
                            tNFeInfNFeDetImpostoICMSICMSSN202.orig = ValidateLib.enviNFe_ns.Torig.Item1;
                        if (nfeDet.nfeImposto.nfeICMSSN202.orig == 2)
                            tNFeInfNFeDetImpostoICMSICMSSN202.orig = ValidateLib.enviNFe_ns.Torig.Item2;
                        if (nfeDet.nfeImposto.nfeICMSSN202.orig == 3)
                            tNFeInfNFeDetImpostoICMSICMSSN202.orig = ValidateLib.enviNFe_ns.Torig.Item3;
                        if (nfeDet.nfeImposto.nfeICMSSN202.orig == 4)
                            tNFeInfNFeDetImpostoICMSICMSSN202.orig = ValidateLib.enviNFe_ns.Torig.Item4;
                        if (nfeDet.nfeImposto.nfeICMSSN202.orig == 5)
                            tNFeInfNFeDetImpostoICMSICMSSN202.orig = ValidateLib.enviNFe_ns.Torig.Item5;
                        if (nfeDet.nfeImposto.nfeICMSSN202.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMSSN202.orig = ValidateLib.enviNFe_ns.Torig.Item6;
                        if (nfeDet.nfeImposto.nfeICMSSN202.orig == 7)
                            tNFeInfNFeDetImpostoICMSICMSSN202.orig = ValidateLib.enviNFe_ns.Torig.Item7;
                        if (nfeDet.nfeImposto.nfeICMSSN202.orig == 8)
                            tNFeInfNFeDetImpostoICMSICMSSN202.orig = ValidateLib.enviNFe_ns.Torig.Item8;

                        tNFeInfNFeDetImpostoICMSICMSSN202.pICMSST = nfeDet.nfeImposto.nfeICMSSN202.pICMSST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN202.pMVAST = nfeDet.nfeImposto.nfeICMSSN202.pMVAST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN202.pRedBCST = nfeDet.nfeImposto.nfeICMSSN202.pRedBCST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN202.vBCST = nfeDet.nfeImposto.nfeICMSSN202.vBCST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN202.vICMSST = nfeDet.nfeImposto.nfeICMSSN202.vICMSST.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoICMS.Item = tNFeInfNFeDetImpostoICMSICMSSN202;
                        limposto.Add(tNFeInfNFeDetImpostoICMS);
                    }

                    #endregion

                    #region ICMSSN500

                    if (nfeDet.nfeImposto.nfeICMSSN500.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN500 tNFeInfNFeDetImpostoICMSICMSSN500 = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN500();

                        tNFeInfNFeDetImpostoICMSICMSSN500.CSOSN = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN500CSOSN.Item500;

                        if (nfeDet.nfeImposto.nfeICMSSN500.orig == 0)
                            tNFeInfNFeDetImpostoICMSICMSSN500.orig = ValidateLib.enviNFe_ns.Torig.Item0;
                        if (nfeDet.nfeImposto.nfeICMSSN500.orig == 1)
                            tNFeInfNFeDetImpostoICMSICMSSN500.orig = ValidateLib.enviNFe_ns.Torig.Item1;
                        if (nfeDet.nfeImposto.nfeICMSSN500.orig == 2)
                            tNFeInfNFeDetImpostoICMSICMSSN500.orig = ValidateLib.enviNFe_ns.Torig.Item2;
                        if (nfeDet.nfeImposto.nfeICMSSN500.orig == 3)
                            tNFeInfNFeDetImpostoICMSICMSSN500.orig = ValidateLib.enviNFe_ns.Torig.Item3;
                        if (nfeDet.nfeImposto.nfeICMSSN500.orig == 4)
                            tNFeInfNFeDetImpostoICMSICMSSN500.orig = ValidateLib.enviNFe_ns.Torig.Item4;
                        if (nfeDet.nfeImposto.nfeICMSSN500.orig == 5)
                            tNFeInfNFeDetImpostoICMSICMSSN500.orig = ValidateLib.enviNFe_ns.Torig.Item5;
                        if (nfeDet.nfeImposto.nfeICMSSN500.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMSSN500.orig = ValidateLib.enviNFe_ns.Torig.Item6;
                        if (nfeDet.nfeImposto.nfeICMSSN500.orig == 7)
                            tNFeInfNFeDetImpostoICMSICMSSN500.orig = ValidateLib.enviNFe_ns.Torig.Item7;
                        if (nfeDet.nfeImposto.nfeICMSSN500.orig == 8)
                            tNFeInfNFeDetImpostoICMSICMSSN500.orig = ValidateLib.enviNFe_ns.Torig.Item8;

                        tNFeInfNFeDetImpostoICMSICMSSN500.vBCSTRet = nfeDet.nfeImposto.nfeICMSSN500.vBCSTRet.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN500.vICMSSTRet = nfeDet.nfeImposto.nfeICMSSN500.vICMSSTRet.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoICMS.Item = tNFeInfNFeDetImpostoICMSICMSSN500;
                        limposto.Add(tNFeInfNFeDetImpostoICMS);
                    }

                    #endregion

                    #region ICMSSN900

                    if (nfeDet.nfeImposto.nfeICMSSN900.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN900 tNFeInfNFeDetImpostoICMSICMSSN900 = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN900();

                        tNFeInfNFeDetImpostoICMSICMSSN900.CSOSN = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN900CSOSN.Item900;

                        if (nfeDet.nfeImposto.nfeICMSSN900.modBC == 0)
                            tNFeInfNFeDetImpostoICMSICMSSN900.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN900ModBC.Item0;
                        if (nfeDet.nfeImposto.nfeICMSSN900.modBC == 1)
                            tNFeInfNFeDetImpostoICMSICMSSN900.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN900ModBC.Item1;
                        if (nfeDet.nfeImposto.nfeICMSSN900.modBC == 2)
                            tNFeInfNFeDetImpostoICMSICMSSN900.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN900ModBC.Item2;
                        if (nfeDet.nfeImposto.nfeICMSSN900.modBC == 3)
                            tNFeInfNFeDetImpostoICMSICMSSN900.modBC = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN900ModBC.Item3;

                        if (nfeDet.nfeImposto.nfeICMSSN900.modBCST == 0)
                            tNFeInfNFeDetImpostoICMSICMSSN900.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN900ModBCST.Item0;
                        if (nfeDet.nfeImposto.nfeICMSSN900.modBCST == 1)
                            tNFeInfNFeDetImpostoICMSICMSSN900.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN900ModBCST.Item1;
                        if (nfeDet.nfeImposto.nfeICMSSN900.modBCST == 2)
                            tNFeInfNFeDetImpostoICMSICMSSN900.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN900ModBCST.Item2;
                        if (nfeDet.nfeImposto.nfeICMSSN900.modBCST == 3)
                            tNFeInfNFeDetImpostoICMSICMSSN900.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN900ModBCST.Item3;
                        if (nfeDet.nfeImposto.nfeICMSSN900.modBCST == 4)
                            tNFeInfNFeDetImpostoICMSICMSSN900.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN900ModBCST.Item4;
                        if (nfeDet.nfeImposto.nfeICMSSN900.modBCST == 5)
                            tNFeInfNFeDetImpostoICMSICMSSN900.modBCST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSN900ModBCST.Item5;

                        if (nfeDet.nfeImposto.nfeICMSSN900.orig == 0)
                            tNFeInfNFeDetImpostoICMSICMSSN900.orig = ValidateLib.enviNFe_ns.Torig.Item0;
                        if (nfeDet.nfeImposto.nfeICMSSN900.orig == 1)
                            tNFeInfNFeDetImpostoICMSICMSSN900.orig = ValidateLib.enviNFe_ns.Torig.Item1;
                        if (nfeDet.nfeImposto.nfeICMSSN900.orig == 2)
                            tNFeInfNFeDetImpostoICMSICMSSN900.orig = ValidateLib.enviNFe_ns.Torig.Item2;
                        if (nfeDet.nfeImposto.nfeICMSSN900.orig == 3)
                            tNFeInfNFeDetImpostoICMSICMSSN900.orig = ValidateLib.enviNFe_ns.Torig.Item3;
                        if (nfeDet.nfeImposto.nfeICMSSN900.orig == 4)
                            tNFeInfNFeDetImpostoICMSICMSSN900.orig = ValidateLib.enviNFe_ns.Torig.Item4;
                        if (nfeDet.nfeImposto.nfeICMSSN900.orig == 5)
                            tNFeInfNFeDetImpostoICMSICMSSN900.orig = ValidateLib.enviNFe_ns.Torig.Item5;
                        if (nfeDet.nfeImposto.nfeICMSSN900.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMSSN900.orig = ValidateLib.enviNFe_ns.Torig.Item6;
                        if (nfeDet.nfeImposto.nfeICMSSN900.orig == 7)
                            tNFeInfNFeDetImpostoICMSICMSSN900.orig = ValidateLib.enviNFe_ns.Torig.Item7;
                        if (nfeDet.nfeImposto.nfeICMSSN900.orig == 8)
                            tNFeInfNFeDetImpostoICMSICMSSN900.orig = ValidateLib.enviNFe_ns.Torig.Item8;

                        tNFeInfNFeDetImpostoICMSICMSSN900.pCredSN = nfeDet.nfeImposto.nfeICMSSN900.pCredSN.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN900.pICMS = nfeDet.nfeImposto.nfeICMSSN900.pICMS.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN900.pICMSST = nfeDet.nfeImposto.nfeICMSSN900.pICMSST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN900.pMVAST = nfeDet.nfeImposto.nfeICMSSN900.pMVAST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN900.pRedBC = nfeDet.nfeImposto.nfeICMSSN900.pRedBC.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN900.pRedBCST = nfeDet.nfeImposto.nfeICMSSN900.pRedBCST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN900.vBC = nfeDet.nfeImposto.nfeICMSSN900.vBC.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN900.vBCST = nfeDet.nfeImposto.nfeICMSSN900.vBCST.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN900.vCredICMSSN = nfeDet.nfeImposto.nfeICMSSN900.vCredICMSSN.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN900.vICMS = nfeDet.nfeImposto.nfeICMSSN900.vICMS.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSSN900.vICMSST = nfeDet.nfeImposto.nfeICMSSN900.vICMSST.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoICMS.Item = tNFeInfNFeDetImpostoICMSICMSSN900;
                        limposto.Add(tNFeInfNFeDetImpostoICMS);
                    }

                    #endregion

                    #region ICMSST

                    if (nfeDet.nfeImposto.nfeICMSST.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSST tNFeInfNFeDetImpostoICMSICMSST = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSST();

                        tNFeInfNFeDetImpostoICMSICMSST.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoICMSICMSSTCST.Item41;

                        if (nfeDet.nfeImposto.nfeICMSST.orig == 0)
                            tNFeInfNFeDetImpostoICMSICMSST.orig = ValidateLib.enviNFe_ns.Torig.Item0;
                        if (nfeDet.nfeImposto.nfeICMSST.orig == 1)
                            tNFeInfNFeDetImpostoICMSICMSST.orig = ValidateLib.enviNFe_ns.Torig.Item1;
                        if (nfeDet.nfeImposto.nfeICMSST.orig == 2)
                            tNFeInfNFeDetImpostoICMSICMSST.orig = ValidateLib.enviNFe_ns.Torig.Item2;
                        if (nfeDet.nfeImposto.nfeICMSST.orig == 3)
                            tNFeInfNFeDetImpostoICMSICMSST.orig = ValidateLib.enviNFe_ns.Torig.Item3;
                        if (nfeDet.nfeImposto.nfeICMSST.orig == 4)
                            tNFeInfNFeDetImpostoICMSICMSST.orig = ValidateLib.enviNFe_ns.Torig.Item4;
                        if (nfeDet.nfeImposto.nfeICMSST.orig == 5)
                            tNFeInfNFeDetImpostoICMSICMSST.orig = ValidateLib.enviNFe_ns.Torig.Item5;
                        if (nfeDet.nfeImposto.nfeICMSST.orig == 6)
                            tNFeInfNFeDetImpostoICMSICMSST.orig = ValidateLib.enviNFe_ns.Torig.Item6;
                        if (nfeDet.nfeImposto.nfeICMSST.orig == 7)
                            tNFeInfNFeDetImpostoICMSICMSST.orig = ValidateLib.enviNFe_ns.Torig.Item7;
                        if (nfeDet.nfeImposto.nfeICMSST.orig == 8)
                            tNFeInfNFeDetImpostoICMSICMSST.orig = ValidateLib.enviNFe_ns.Torig.Item8;

                        tNFeInfNFeDetImpostoICMSICMSST.vBCSTDest = nfeDet.nfeImposto.nfeICMSST.vBCSTDest.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSST.vBCSTRet = nfeDet.nfeImposto.nfeICMSST.vBCSTRet.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSST.vICMSSTDest = nfeDet.nfeImposto.nfeICMSST.vICMSSTDest.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoICMSICMSST.vICMSSTRet = nfeDet.nfeImposto.nfeICMSST.vICMSSTRet.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoICMS.Item = tNFeInfNFeDetImpostoICMSICMSST;
                        limposto.Add(tNFeInfNFeDetImpostoICMS);
                    }

                    #endregion

                    #endregion

                    #region IPI

                    if (nfeDet.nfeImposto.nfeIPI.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TIpi tIpi = new ValidateLib.enviNFe_ns.TIpi();
                        tIpi.cEnq = nfeDet.nfeImposto.nfeIPI.cEnq;
                        tIpi.clEnq = nfeDet.nfeImposto.nfeIPI.clEnq;
                        tIpi.CNPJProd = nfeDet.nfeImposto.nfeIPI.CNPJProd;
                        tIpi.cSelo = nfeDet.nfeImposto.nfeIPI.cSelo;
                        tIpi.qSelo = nfeDet.nfeImposto.nfeIPI.qSelo;

                        if (nfeDet.nfeImposto.nfeIPI.nfeIPINT.nItem > 0)
                        {
                            ValidateLib.enviNFe_ns.TIpiIPINT tIpiIPINT = new ValidateLib.enviNFe_ns.TIpiIPINT();

                            if (nfeDet.nfeImposto.nfeIPI.nfeIPINT.CST == "01")
                                tIpiIPINT.CST = ValidateLib.enviNFe_ns.TIpiIPINTCST.Item01;
                            if (nfeDet.nfeImposto.nfeIPI.nfeIPINT.CST == "02")
                                tIpiIPINT.CST = ValidateLib.enviNFe_ns.TIpiIPINTCST.Item02;
                            if (nfeDet.nfeImposto.nfeIPI.nfeIPINT.CST == "03")
                                tIpiIPINT.CST = ValidateLib.enviNFe_ns.TIpiIPINTCST.Item03;
                            if (nfeDet.nfeImposto.nfeIPI.nfeIPINT.CST == "04")
                                tIpiIPINT.CST = ValidateLib.enviNFe_ns.TIpiIPINTCST.Item04;
                            if (nfeDet.nfeImposto.nfeIPI.nfeIPINT.CST == "05")
                                tIpiIPINT.CST = ValidateLib.enviNFe_ns.TIpiIPINTCST.Item05;
                            if (nfeDet.nfeImposto.nfeIPI.nfeIPINT.CST == "51")
                                tIpiIPINT.CST = ValidateLib.enviNFe_ns.TIpiIPINTCST.Item51;
                            if (nfeDet.nfeImposto.nfeIPI.nfeIPINT.CST == "52")
                                tIpiIPINT.CST = ValidateLib.enviNFe_ns.TIpiIPINTCST.Item52;
                            if (nfeDet.nfeImposto.nfeIPI.nfeIPINT.CST == "53")
                                tIpiIPINT.CST = ValidateLib.enviNFe_ns.TIpiIPINTCST.Item53;
                            if (nfeDet.nfeImposto.nfeIPI.nfeIPINT.CST == "54")
                                tIpiIPINT.CST = ValidateLib.enviNFe_ns.TIpiIPINTCST.Item54;
                            if (nfeDet.nfeImposto.nfeIPI.nfeIPINT.CST == "55")
                                tIpiIPINT.CST = ValidateLib.enviNFe_ns.TIpiIPINTCST.Item55;

                            tIpi.Item = tIpiIPINT;
                        }

                        if (nfeDet.nfeImposto.nfeIPI.nfeIPITrib.nItem > 0)
                        {
                            ValidateLib.enviNFe_ns.TIpiIPITrib tIpiIPITrib = new ValidateLib.enviNFe_ns.TIpiIPITrib();

                            if (nfeDet.nfeImposto.nfeIPI.nfeIPITrib.CST == "00")
                                tIpiIPITrib.CST = ValidateLib.enviNFe_ns.TIpiIPITribCST.Item00;
                            if (nfeDet.nfeImposto.nfeIPI.nfeIPITrib.CST == "49")
                                tIpiIPITrib.CST = ValidateLib.enviNFe_ns.TIpiIPITribCST.Item49;
                            if (nfeDet.nfeImposto.nfeIPI.nfeIPITrib.CST == "50")
                                tIpiIPITrib.CST = ValidateLib.enviNFe_ns.TIpiIPITribCST.Item50;
                            if (nfeDet.nfeImposto.nfeIPI.nfeIPITrib.CST == "99")
                                tIpiIPITrib.CST = ValidateLib.enviNFe_ns.TIpiIPITribCST.Item99;

                            if (nfeDet.nfeImposto.nfeIPI.nfeIPITrib.pIPI > 0)
                            {
                                tIpiIPITrib.Items = new string[2];
                                tIpiIPITrib.Items[0] = nfeDet.nfeImposto.nfeIPI.nfeIPITrib.vBC.ToString().Replace(".", "").Replace(",", ".");
                                tIpiIPITrib.Items[1] = nfeDet.nfeImposto.nfeIPI.nfeIPITrib.pIPI.ToString().Replace(".", "").Replace(",", ".");

                                tIpiIPITrib.ItemsElementName = new ValidateLib.enviNFe_ns.ItemsChoiceType[2];
                                tIpiIPITrib.ItemsElementName[0] = ValidateLib.enviNFe_ns.ItemsChoiceType.vBC;
                                tIpiIPITrib.ItemsElementName[1] = ValidateLib.enviNFe_ns.ItemsChoiceType.pIPI;
                            }
                            else
                            {
                                tIpiIPITrib.Items = new string[2];
                                tIpiIPITrib.Items[0] = nfeDet.nfeImposto.nfeIPI.nfeIPITrib.qUnid.ToString().Replace(".", "").Replace(",", ".");
                                tIpiIPITrib.Items[1] = nfeDet.nfeImposto.nfeIPI.nfeIPITrib.vUnid.ToString().Replace(".", "").Replace(",", ".");

                                tIpiIPITrib.ItemsElementName = new ValidateLib.enviNFe_ns.ItemsChoiceType[2];
                                tIpiIPITrib.ItemsElementName[0] = ValidateLib.enviNFe_ns.ItemsChoiceType.qUnid;
                                tIpiIPITrib.ItemsElementName[1] = ValidateLib.enviNFe_ns.ItemsChoiceType.vUnid;
                            }

                            tIpi.Item = tIpiIPITrib;

                            tIpiIPITrib.vIPI = nfeDet.nfeImposto.nfeIPI.nfeIPITrib.vIPI.ToString().Replace(".", "").Replace(",", ".");
                        }
                        limposto.Add(tIpi);
                    }

                    #endregion

                    #region II

                    if (nfeDet.nfeImposto.nfeII.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoII tNFeInfNFeDetImpostoII = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoII();
                        tNFeInfNFeDetImpostoII.vBC = nfeDet.nfeImposto.nfeII.vBC.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoII.vDespAdu = nfeDet.nfeImposto.nfeII.vDespAdu.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoII.vII = nfeDet.nfeImposto.nfeII.vII.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoII.vIOF = nfeDet.nfeImposto.nfeII.vIOF.ToString().Replace(".", "").Replace(",", ".");
                        limposto.Add(tNFeInfNFeDetImpostoII);
                    }

                    #endregion

                    #region ISSQN

                    if (nfeDet.nfeImposto.nfeISSQN.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoISSQN tNFeInfNFeDetImpostoISSQN = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoISSQN();


                        limposto.Add(tNFeInfNFeDetImpostoISSQN);
                    }

                    #endregion

                    if (limposto.Count > 0)
                    {
                        _nfe.infNFe.det[item].imposto.Items = new object[limposto.Count];
                        int nimp = 0;
                        foreach (object imposto in limposto)
                        {
                            _nfe.infNFe.det[item].imposto.Items[nimp] = imposto;
                            nimp++;
                        }
                    }

                    #region PIS

                    ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPIS tNFeInfNFeDetImpostoPIS = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPIS();

                    if (nfeDet.nfeImposto.nfePIS.nfePISAliq.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISAliq tNFeInfNFeDetImpostoPISPISAliq = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISAliq();

                        if (nfeDet.nfeImposto.nfePIS.nfePISAliq.CST == "01")
                            tNFeInfNFeDetImpostoPISPISAliq.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISAliqCST.Item01;
                        if (nfeDet.nfeImposto.nfePIS.nfePISAliq.CST == "02")
                            tNFeInfNFeDetImpostoPISPISAliq.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISAliqCST.Item02;

                        tNFeInfNFeDetImpostoPISPISAliq.pPIS = nfeDet.nfeImposto.nfePIS.nfePISAliq.pPIS.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoPISPISAliq.vBC = nfeDet.nfeImposto.nfePIS.nfePISAliq.vBC.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoPISPISAliq.vPIS = nfeDet.nfeImposto.nfePIS.nfePISAliq.vPIS.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoPIS.Item = tNFeInfNFeDetImpostoPISPISAliq;
                        _nfe.infNFe.det[item].imposto.PIS = tNFeInfNFeDetImpostoPIS;
                    }

                    if (nfeDet.nfeImposto.nfePIS.nfePISNT.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISNT tNFeInfNFeDetImpostoPISPISNT = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISNT();

                        if (nfeDet.nfeImposto.nfePIS.nfePISNT.CST == "04")
                            tNFeInfNFeDetImpostoPISPISNT.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISNTCST.Item04;
                        if (nfeDet.nfeImposto.nfePIS.nfePISNT.CST == "05")
                            tNFeInfNFeDetImpostoPISPISNT.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISNTCST.Item05;
                        if (nfeDet.nfeImposto.nfePIS.nfePISNT.CST == "06")
                            tNFeInfNFeDetImpostoPISPISNT.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISNTCST.Item06;
                        if (nfeDet.nfeImposto.nfePIS.nfePISNT.CST == "07")
                            tNFeInfNFeDetImpostoPISPISNT.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISNTCST.Item07;
                        if (nfeDet.nfeImposto.nfePIS.nfePISNT.CST == "08")
                            tNFeInfNFeDetImpostoPISPISNT.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISNTCST.Item08;
                        if (nfeDet.nfeImposto.nfePIS.nfePISNT.CST == "09")
                            tNFeInfNFeDetImpostoPISPISNT.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISNTCST.Item09;

                        tNFeInfNFeDetImpostoPIS.Item = tNFeInfNFeDetImpostoPISPISNT;
                        _nfe.infNFe.det[item].imposto.PIS = tNFeInfNFeDetImpostoPIS;
                    }

                    if (nfeDet.nfeImposto.nfePIS.nfePISOutr.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutr tNFeInfNFeDetImpostoPISPISOutr = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutr();

                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "49")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item49;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "50")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item50;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "51")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item51;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "52")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item52;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "53")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item53;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "54")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item54;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "55")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item55;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "56")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item56;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "60")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item60;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "61")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item61;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "62")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item62;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "63")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item63;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "64")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item64;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "65")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item65;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "66")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item66;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "67")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item67;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "70")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item70;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "71")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item71;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "72")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item72;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "73")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item73;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "74")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item74;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "75")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item75;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "98")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item98;
                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.CST == "99")
                            tNFeInfNFeDetImpostoPISPISOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISOutrCST.Item99;

                        if (nfeDet.nfeImposto.nfePIS.nfePISOutr.pPIS > 0)
                        {
                            tNFeInfNFeDetImpostoPISPISOutr.Items = new string[2];
                            tNFeInfNFeDetImpostoPISPISOutr.Items[0] = nfeDet.nfeImposto.nfePIS.nfePISOutr.vBC.ToString().Replace(".", "").Replace(",", ".");
                            tNFeInfNFeDetImpostoPISPISOutr.Items[1] = nfeDet.nfeImposto.nfePIS.nfePISOutr.pPIS.ToString().Replace(".", "").Replace(",", ".");

                            tNFeInfNFeDetImpostoPISPISOutr.ItemsElementName = new ValidateLib.enviNFe_ns.ItemsChoiceType1[2];
                            tNFeInfNFeDetImpostoPISPISOutr.ItemsElementName[0] = ValidateLib.enviNFe_ns.ItemsChoiceType1.vBC;
                            tNFeInfNFeDetImpostoPISPISOutr.ItemsElementName[1] = ValidateLib.enviNFe_ns.ItemsChoiceType1.pPIS;
                        }
                        else
                        {
                            tNFeInfNFeDetImpostoPISPISOutr.Items = new string[2];
                            tNFeInfNFeDetImpostoPISPISOutr.Items[0] = nfeDet.nfeImposto.nfePIS.nfePISOutr.qBCProd.ToString().Replace(".", "").Replace(",", ".");
                            tNFeInfNFeDetImpostoPISPISOutr.Items[1] = nfeDet.nfeImposto.nfePIS.nfePISOutr.vAliqProd.ToString().Replace(".", "").Replace(",", ".");

                            tNFeInfNFeDetImpostoPISPISOutr.ItemsElementName = new ValidateLib.enviNFe_ns.ItemsChoiceType1[2];
                            tNFeInfNFeDetImpostoPISPISOutr.ItemsElementName[0] = ValidateLib.enviNFe_ns.ItemsChoiceType1.qBCProd;
                            tNFeInfNFeDetImpostoPISPISOutr.ItemsElementName[1] = ValidateLib.enviNFe_ns.ItemsChoiceType1.vAliqProd;
                        }

                        tNFeInfNFeDetImpostoPISPISOutr.vPIS = nfeDet.nfeImposto.nfePIS.nfePISOutr.vPIS.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoPIS.Item = tNFeInfNFeDetImpostoPISPISOutr;
                        _nfe.infNFe.det[item].imposto.PIS = tNFeInfNFeDetImpostoPIS;
                    }

                    if (nfeDet.nfeImposto.nfePIS.nfePISQtde.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISQtde tNFeInfNFeDetImpostoPISPISQtde = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISQtde();

                        tNFeInfNFeDetImpostoPISPISQtde.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISPISQtdeCST.Item03;
                        tNFeInfNFeDetImpostoPISPISQtde.qBCProd = nfeDet.nfeImposto.nfePIS.nfePISQtde.qBCProd.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoPISPISQtde.vAliqProd = nfeDet.nfeImposto.nfePIS.nfePISQtde.vAliqProd.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoPISPISQtde.vPIS = nfeDet.nfeImposto.nfePIS.nfePISQtde.vPIS.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoPIS.Item = tNFeInfNFeDetImpostoPISPISQtde;
                        _nfe.infNFe.det[item].imposto.PIS = tNFeInfNFeDetImpostoPIS;
                    }

                    if (nfeDet.nfeImposto.nfePIS.nfePISST.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISST tNFeInfNFeDetImpostoPISST = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoPISST();

                        if (nfeDet.nfeImposto.nfePIS.nfePISST.pPIS > 0)
                        {
                            tNFeInfNFeDetImpostoPISST.Items = new string[2];
                            tNFeInfNFeDetImpostoPISST.Items[0] = nfeDet.nfeImposto.nfePIS.nfePISST.vBC.ToString().Replace(".", "").Replace(",", ".");
                            tNFeInfNFeDetImpostoPISST.Items[1] = nfeDet.nfeImposto.nfePIS.nfePISST.pPIS.ToString().Replace(".", "").Replace(",", ".");

                            tNFeInfNFeDetImpostoPISST.ItemsElementName = new ValidateLib.enviNFe_ns.ItemsChoiceType2[2];
                            tNFeInfNFeDetImpostoPISST.ItemsElementName[0] = ValidateLib.enviNFe_ns.ItemsChoiceType2.vBC;
                            tNFeInfNFeDetImpostoPISST.ItemsElementName[1] = ValidateLib.enviNFe_ns.ItemsChoiceType2.pPIS;
                        }
                        else
                        {
                            tNFeInfNFeDetImpostoPISST.Items = new string[2];
                            tNFeInfNFeDetImpostoPISST.Items[0] = nfeDet.nfeImposto.nfePIS.nfePISST.qBCProd.ToString().Replace(".", "").Replace(",", ".");
                            tNFeInfNFeDetImpostoPISST.Items[1] = nfeDet.nfeImposto.nfePIS.nfePISST.vAliqProd.ToString().Replace(".", "").Replace(",", ".");

                            tNFeInfNFeDetImpostoPISST.ItemsElementName = new ValidateLib.enviNFe_ns.ItemsChoiceType2[2];
                            tNFeInfNFeDetImpostoPISST.ItemsElementName[0] = ValidateLib.enviNFe_ns.ItemsChoiceType2.qBCProd;
                            tNFeInfNFeDetImpostoPISST.ItemsElementName[1] = ValidateLib.enviNFe_ns.ItemsChoiceType2.vAliqProd;
                        }

                        tNFeInfNFeDetImpostoPISST.vPIS = nfeDet.nfeImposto.nfePIS.nfePISST.vPIS.ToString().Replace(".", "").Replace(",", ".");

                        _nfe.infNFe.det[item].imposto.PISST = tNFeInfNFeDetImpostoPISST;
                    }

                    #endregion

                    #region COFINS

                    ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINS tNFeInfNFeDetImpostoCOFINS = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINS();

                    if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSAliq.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSAliq tNFeInfNFeDetImpostoCOFINSCOFINSAliq = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSAliq();

                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSAliq.CST == "01")
                            tNFeInfNFeDetImpostoCOFINSCOFINSAliq.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSAliqCST.Item01;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSAliq.CST == "02")
                            tNFeInfNFeDetImpostoCOFINSCOFINSAliq.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSAliqCST.Item02;

                        tNFeInfNFeDetImpostoCOFINSCOFINSAliq.pCOFINS = nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSAliq.pCOFINS.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoCOFINSCOFINSAliq.vBC = nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSAliq.vBC.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoCOFINSCOFINSAliq.vCOFINS = nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSAliq.vCOFINS.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoCOFINS.Item = tNFeInfNFeDetImpostoCOFINSCOFINSAliq;
                        _nfe.infNFe.det[item].imposto.COFINS = tNFeInfNFeDetImpostoCOFINS;
                    }


                    if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSNT.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSNT tNFeInfNFeDetImpostoCOFINSCOFINSNT = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSNT();

                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSNT.CST == "04")
                            tNFeInfNFeDetImpostoCOFINSCOFINSNT.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSNTCST.Item04;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSNT.CST == "05")
                            tNFeInfNFeDetImpostoCOFINSCOFINSNT.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSNTCST.Item05;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSNT.CST == "06")
                            tNFeInfNFeDetImpostoCOFINSCOFINSNT.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSNTCST.Item06;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSNT.CST == "07")
                            tNFeInfNFeDetImpostoCOFINSCOFINSNT.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSNTCST.Item07;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSNT.CST == "08")
                            tNFeInfNFeDetImpostoCOFINSCOFINSNT.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSNTCST.Item08;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSNT.CST == "09")
                            tNFeInfNFeDetImpostoCOFINSCOFINSNT.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSNTCST.Item09;

                        tNFeInfNFeDetImpostoCOFINS.Item = tNFeInfNFeDetImpostoCOFINSCOFINSNT;
                        _nfe.infNFe.det[item].imposto.COFINS = tNFeInfNFeDetImpostoCOFINS;
                    }

                    if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutr tNFeInfNFeDetImpostoCOFINSCOFINSOutr = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutr();

                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "49")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item49;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "50")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item50;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "51")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item51;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "52")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item52;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "53")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item53;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "54")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item54;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "55")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item55;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "56")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item56;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "60")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item60;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "61")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item61;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "62")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item62;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "63")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item63;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "64")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item64;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "65")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item65;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "66")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item66;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "67")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item67;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "70")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item70;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "71")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item71;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "72")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item72;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "73")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item73;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "74")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item74;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "75")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item75;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "98")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item98;
                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.CST == "99")
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSOutrCST.Item99;

                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.pCOFINS > 0)
                        {
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.Items = new string[2];
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.Items[0] = nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.vBC.ToString().Replace(".", "").Replace(",", ".");
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.Items[1] = nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.pCOFINS.ToString().Replace(".", "").Replace(",", ".");

                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.ItemsElementName = new ValidateLib.enviNFe_ns.ItemsChoiceType3[2];
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.ItemsElementName[0] = ValidateLib.enviNFe_ns.ItemsChoiceType3.vBC;
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.ItemsElementName[1] = ValidateLib.enviNFe_ns.ItemsChoiceType3.pCOFINS;
                        }
                        else
                        {
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.Items = new string[2];
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.Items[0] = nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.qBCProd.ToString().Replace(".", "").Replace(",", ".");
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.Items[1] = nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.vAliqProd.ToString().Replace(".", "").Replace(",", ".");

                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.ItemsElementName = new ValidateLib.enviNFe_ns.ItemsChoiceType3[2];
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.ItemsElementName[0] = ValidateLib.enviNFe_ns.ItemsChoiceType3.qBCProd;
                            tNFeInfNFeDetImpostoCOFINSCOFINSOutr.ItemsElementName[1] = ValidateLib.enviNFe_ns.ItemsChoiceType3.vAliqProd;
                        }

                        tNFeInfNFeDetImpostoCOFINSCOFINSOutr.vCOFINS = nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSOutr.vCOFINS.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoCOFINS.Item = tNFeInfNFeDetImpostoCOFINSCOFINSOutr;
                        _nfe.infNFe.det[item].imposto.COFINS = tNFeInfNFeDetImpostoCOFINS;
                    }

                    if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSQtde.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSQtde tNFeInfNFeDetImpostoCOFINSCOFINSQtde = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSQtde();

                        tNFeInfNFeDetImpostoCOFINSCOFINSQtde.CST = ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSCOFINSQtdeCST.Item03;
                        tNFeInfNFeDetImpostoCOFINSCOFINSQtde.qBCProd = nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSQtde.qBCProd.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoCOFINSCOFINSQtde.vAliqProd = nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSQtde.vAliqProd.ToString().Replace(".", "").Replace(",", ".");
                        tNFeInfNFeDetImpostoCOFINSCOFINSQtde.vCOFINS = nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSQtde.vCOFINS.ToString().Replace(".", "").Replace(",", ".");

                        tNFeInfNFeDetImpostoCOFINS.Item = tNFeInfNFeDetImpostoCOFINSCOFINSQtde;
                        _nfe.infNFe.det[item].imposto.COFINS = tNFeInfNFeDetImpostoCOFINS;
                    }

                    if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSST.nItem > 0)
                    {
                        ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSST tNFeInfNFeDetImpostoCOFINSST = new ValidateLib.enviNFe_ns.TNFeInfNFeDetImpostoCOFINSST();

                        if (nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSST.pCOFINS > 0)
                        {
                            tNFeInfNFeDetImpostoCOFINSST.Items = new string[2];
                            tNFeInfNFeDetImpostoCOFINSST.Items[0] = nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSST.vBC.ToString().Replace(".", "").Replace(",", ".");
                            tNFeInfNFeDetImpostoCOFINSST.Items[1] = nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSST.pCOFINS.ToString().Replace(".", "").Replace(",", ".");

                            tNFeInfNFeDetImpostoCOFINSST.ItemsElementName = new ValidateLib.enviNFe_ns.ItemsChoiceType4[2];
                            tNFeInfNFeDetImpostoCOFINSST.ItemsElementName[0] = ValidateLib.enviNFe_ns.ItemsChoiceType4.vBC;
                            tNFeInfNFeDetImpostoCOFINSST.ItemsElementName[1] = ValidateLib.enviNFe_ns.ItemsChoiceType4.pCOFINS;
                        }
                        else
                        {
                            tNFeInfNFeDetImpostoCOFINSST.Items = new string[2];
                            tNFeInfNFeDetImpostoCOFINSST.Items[0] = nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSST.qBCProd.ToString().Replace(".", "").Replace(",", ".");
                            tNFeInfNFeDetImpostoCOFINSST.Items[1] = nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSST.vAliqProd.ToString().Replace(".", "").Replace(",", ".");

                            tNFeInfNFeDetImpostoCOFINSST.ItemsElementName = new ValidateLib.enviNFe_ns.ItemsChoiceType4[2];
                            tNFeInfNFeDetImpostoCOFINSST.ItemsElementName[0] = ValidateLib.enviNFe_ns.ItemsChoiceType4.qBCProd;
                            tNFeInfNFeDetImpostoCOFINSST.ItemsElementName[1] = ValidateLib.enviNFe_ns.ItemsChoiceType4.vAliqProd;
                        }

                        tNFeInfNFeDetImpostoCOFINSST.vCOFINS = nfeDet.nfeImposto.nfeCOFINS.nfeCOFINSST.vCOFINS.ToString().Replace(".", "").Replace(",", ".");

                        _nfe.infNFe.det[item].imposto.COFINSST = tNFeInfNFeDetImpostoCOFINSST;
                    }

                    #endregion

                    //_nfe.infNFe.det[item].impostoDevol
                    #region difal
                    //Verifica Difal
                    System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery("SELECT * FROM ZNFEDIFAL WHERE IDOUTBOX = ?", new object[] { OutBoxPar.IdOutbox });
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ValidateLib.enviNFe_ns.TNFeDifal difal = new ValidateLib.enviNFe_ns.TNFeDifal();
                            difal.vBCUFDest = string.IsNullOrEmpty(dt.Rows[i]["VBCUFDEST"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[i]["VBCUFDEST"]);
                            difal.pFCPUFDest = string.IsNullOrEmpty(dt.Rows[i]["PFCPUFDEST"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[i]["PFCPUFDEST"]);
                            difal.pICMSInter = string.IsNullOrEmpty(dt.Rows[i]["PICMSINTER"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[i]["PICMSINTER"]);
                            difal.pICMSUFDest = string.IsNullOrEmpty(dt.Rows[i]["PICMSUFDEST"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[i]["PICMSUFDEST"]);
                            difal.pICMSInterPart = string.IsNullOrEmpty(dt.Rows[i]["PICMSINTERPART"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[i]["PICMSINTERPART"]);
                            difal.vFCPUFDest = string.IsNullOrEmpty(dt.Rows[i]["VFCPUFDEST"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[i]["VFCPUFDEST"]);
                            difal.vICMSUFDest = string.IsNullOrEmpty(dt.Rows[i]["VICMSUFDEST"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[i]["VICMSUFDEST"]);
                            difal.vICMSUFRemet = string.IsNullOrEmpty(dt.Rows[i]["VICMSUFREMET"].ToString()) ? 0 : Convert.ToDecimal(dt.Rows[i]["VICMSUFREMET"]);
                            _nfe.infNFe.det[item].imposto.ICMSUFDest = difal;
                        }
                    }
                    #endregion
                    _nfe.infNFe.det[item].infAdProd = nfeDet.infAdProd;

                    item++;
                }

                #endregion

                #region Total

                _nfe.infNFe.total = new ValidateLib.enviNFe_ns.TNFeInfNFeTotal();
                _nfe.infNFe.total.ICMSTot = new ValidateLib.enviNFe_ns.TNFeInfNFeTotalICMSTot();
                _nfe.infNFe.total.ICMSTot.vBC = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vBC.ToString().Replace(".", "").Replace(",", ".");
                _nfe.infNFe.total.ICMSTot.vFCPUFDest = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vFCPUFDest;
                _nfe.infNFe.total.ICMSTot.vICMSUFDest = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vICMSUFDest;
                _nfe.infNFe.total.ICMSTot.vICMSUFRemet = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vICMSUFRemet;
                _nfe.infNFe.total.ICMSTot.vBCST = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vBCST.ToString().Replace(".", "").Replace(",", ".");
                _nfe.infNFe.total.ICMSTot.vCOFINS = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vCOFINS.ToString().Replace(".", "").Replace(",", ".");
                _nfe.infNFe.total.ICMSTot.vDesc = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vDesc.ToString().Replace(".", "").Replace(",", ".");
                _nfe.infNFe.total.ICMSTot.vFrete = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vFrete.ToString().Replace(".", "").Replace(",", ".");
                _nfe.infNFe.total.ICMSTot.vICMS = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vICMS.ToString().Replace(".", "").Replace(",", ".");
                _nfe.infNFe.total.ICMSTot.vICMSDeson = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vICMSDeson.ToString().Replace(".", "").Replace(",", ".");
                _nfe.infNFe.total.ICMSTot.vII = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vII.ToString().Replace(".", "").Replace(",", ".");
                _nfe.infNFe.total.ICMSTot.vIPI = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vIPI.ToString().Replace(".", "").Replace(",", ".");
                _nfe.infNFe.total.ICMSTot.vNF = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vNF.ToString().Replace(".", "").Replace(",", ".");
                _nfe.infNFe.total.ICMSTot.vOutro = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vOutro.ToString().Replace(".", "").Replace(",", ".");
                _nfe.infNFe.total.ICMSTot.vPIS = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vPIS.ToString().Replace(".", "").Replace(",", ".");
                _nfe.infNFe.total.ICMSTot.vProd = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vProd.ToString().Replace(".", "").Replace(",", ".");
                _nfe.infNFe.total.ICMSTot.vSeg = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vSeg.ToString().Replace(".", "").Replace(",", ".");
                _nfe.infNFe.total.ICMSTot.vST = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vST.ToString().Replace(".", "").Replace(",", ".");

                if (OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vTotTrib > 0)
                    _nfe.infNFe.total.ICMSTot.vTotTrib = OutBoxPar.nfeDoc.nfeTotal.nfeICMSTot.vTotTrib.ToString().Replace(".", "").Replace(",", ".");

                if (OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.IdOutbox > 0)
                {
                    _nfe.infNFe.total.ISSQNtot = new ValidateLib.enviNFe_ns.TNFeInfNFeTotalISSQNtot();
                    if (OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.cRegTrib == 1)
                        _nfe.infNFe.total.ISSQNtot.cRegTrib = ValidateLib.enviNFe_ns.TNFeInfNFeTotalISSQNtotCRegTrib.Item1;
                    if (OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.cRegTrib == 2)
                        _nfe.infNFe.total.ISSQNtot.cRegTrib = ValidateLib.enviNFe_ns.TNFeInfNFeTotalISSQNtotCRegTrib.Item2;
                    if (OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.cRegTrib == 3)
                        _nfe.infNFe.total.ISSQNtot.cRegTrib = ValidateLib.enviNFe_ns.TNFeInfNFeTotalISSQNtotCRegTrib.Item3;
                    if (OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.cRegTrib == 4)
                        _nfe.infNFe.total.ISSQNtot.cRegTrib = ValidateLib.enviNFe_ns.TNFeInfNFeTotalISSQNtotCRegTrib.Item4;
                    if (OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.cRegTrib == 5)
                        _nfe.infNFe.total.ISSQNtot.cRegTrib = ValidateLib.enviNFe_ns.TNFeInfNFeTotalISSQNtotCRegTrib.Item5;
                    if (OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.cRegTrib == 6)
                        _nfe.infNFe.total.ISSQNtot.cRegTrib = ValidateLib.enviNFe_ns.TNFeInfNFeTotalISSQNtotCRegTrib.Item6;

                    _nfe.infNFe.total.ISSQNtot.dCompet = OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.dCompet;
                    _nfe.infNFe.total.ISSQNtot.vBC = OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.vBC.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.total.ISSQNtot.vCOFINS = OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.vCOFINS.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.total.ISSQNtot.vDeducao = OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.vDeducao.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.total.ISSQNtot.vDescCond = OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.vDescCond.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.total.ISSQNtot.vDescIncond = OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.vDescIncond.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.total.ISSQNtot.vISS = OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.vISS.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.total.ISSQNtot.vISSRet = OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.vISSRet.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.total.ISSQNtot.vOutro = OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.vOutro.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.total.ISSQNtot.vPIS = OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.vPIS.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.total.ISSQNtot.vServ = OutBoxPar.nfeDoc.nfeTotal.nfeISSQNtot.vServ.ToString().Replace(".", "").Replace(",", ".");
                }

                if (OutBoxPar.nfeDoc.nfeTotal.nferetTrib.IdOutbox > 0)
                {
                    _nfe.infNFe.total.retTrib = new ValidateLib.enviNFe_ns.TNFeInfNFeTotalRetTrib();
                    _nfe.infNFe.total.retTrib.vBCIRRF = OutBoxPar.nfeDoc.nfeTotal.nferetTrib.vBCIRRF.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.total.retTrib.vBCRetPrev = OutBoxPar.nfeDoc.nfeTotal.nferetTrib.vBCRetPrev.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.total.retTrib.vIRRF = OutBoxPar.nfeDoc.nfeTotal.nferetTrib.vIRRF.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.total.retTrib.vRetCOFINS = OutBoxPar.nfeDoc.nfeTotal.nferetTrib.vRetCOFINS.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.total.retTrib.vRetCSLL = OutBoxPar.nfeDoc.nfeTotal.nferetTrib.vRetCSLL.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.total.retTrib.vRetPIS = OutBoxPar.nfeDoc.nfeTotal.nferetTrib.vRetPIS.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.total.retTrib.vRetPrev = OutBoxPar.nfeDoc.nfeTotal.nferetTrib.vRetPrev.ToString().Replace(".", "").Replace(",", ".");
                }

                #endregion

                #region transp

                _nfe.infNFe.transp = new ValidateLib.enviNFe_ns.TNFeInfNFeTransp();
                if (OutBoxPar.nfeDoc.nfetransp.modFrete == 0)
                    _nfe.infNFe.transp.modFrete = ValidateLib.enviNFe_ns.TNFeInfNFeTranspModFrete.Item0;
                if (OutBoxPar.nfeDoc.nfetransp.modFrete == 1)
                    _nfe.infNFe.transp.modFrete = ValidateLib.enviNFe_ns.TNFeInfNFeTranspModFrete.Item1;
                if (OutBoxPar.nfeDoc.nfetransp.modFrete == 2)
                    _nfe.infNFe.transp.modFrete = ValidateLib.enviNFe_ns.TNFeInfNFeTranspModFrete.Item2;
                if (OutBoxPar.nfeDoc.nfetransp.modFrete == 9)
                    _nfe.infNFe.transp.modFrete = ValidateLib.enviNFe_ns.TNFeInfNFeTranspModFrete.Item9;

                #region retTransp

                if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.IdOutbox > 0)
                {
                    _nfe.infNFe.transp.retTransp = new ValidateLib.enviNFe_ns.TNFeInfNFeTranspRetTransp();
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "5351")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item5351;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "5352")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item5352;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "5353")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item5353;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "5354")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item5354;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "5355")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item5355;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "5356")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item5356;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "5357")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item5357;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "5359")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item5359;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "5360")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item5360;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "5931")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item5931;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "5932")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item5932;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "6351")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item6351;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "6352")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item6352;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "6353")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item6353;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "6354")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item6354;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "6355")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item6355;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "6356")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item6356;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "6357")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item6357;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "6359")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item6359;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "6360")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item6360;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "6931")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item6931;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "6932")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item6932;
                    if (OutBoxPar.nfeDoc.nfetransp.nferetTransp.CFOP == "7358")
                        _nfe.infNFe.transp.retTransp.CFOP = ValidateLib.enviNFe_ns.TCfopTransp.Item7358;

                    _nfe.infNFe.transp.retTransp.cMunFG = OutBoxPar.nfeDoc.nfetransp.nferetTransp.cMunFG;
                    _nfe.infNFe.transp.retTransp.pICMSRet = OutBoxPar.nfeDoc.nfetransp.nferetTransp.pICMSRet.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.transp.retTransp.vBCRet = OutBoxPar.nfeDoc.nfetransp.nferetTransp.vBCRet.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.transp.retTransp.vICMSRet = OutBoxPar.nfeDoc.nfetransp.nferetTransp.vICMSRet.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.transp.retTransp.vServ = OutBoxPar.nfeDoc.nfetransp.nferetTransp.vServ.ToString().Replace(".", "").Replace(",", ".");
                }

                #endregion

                #region transporta

                _nfe.infNFe.transp.transporta = new ValidateLib.enviNFe_ns.TNFeInfNFeTranspTransporta();
                //Verificar se pode colocar um replace aqui para retirar os .,/-
                _nfe.infNFe.transp.transporta.IE = OutBoxPar.nfeDoc.nfetransp.nfetransporta.IE;

                if (string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfetransp.nfetransporta.CNPJ))
                {
                    _nfe.infNFe.transp.transporta.Item = OutBoxPar.nfeDoc.nfetransp.nfetransporta.CPF;
                    _nfe.infNFe.transp.transporta.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType6.CPF;
                }

                if (string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfetransp.nfetransporta.CPF))
                {
                    _nfe.infNFe.transp.transporta.Item = OutBoxPar.nfeDoc.nfetransp.nfetransporta.CNPJ;
                    _nfe.infNFe.transp.transporta.ItemElementName = ValidateLib.enviNFe_ns.ItemChoiceType6.CNPJ;
                }

                _nfe.infNFe.transp.transporta.UFSpecified = true;

                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "AC")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.AC;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "AL")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.AL;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "AM")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.AM;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "AP")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.AP;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "BA")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.BA;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "CE")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.CE;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "DF")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.DF;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "ES")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.ES;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "GO")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.GO;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "MA")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.MA;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "MG")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.MG;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "MS")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.MS;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "MT")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.MT;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "PA")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.PA;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "PB")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.PB;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "PE")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.PE;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "PI")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.PI;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "PR")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.PR;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "RJ")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.RJ;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "RN")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.RN;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "RO")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.RO;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "RR")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.RR;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "RS")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.RS;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "SC")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.SC;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "SE")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.SE;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "SP")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.SP;
                if (OutBoxPar.nfeDoc.nfetransp.nfetransporta.UF == "TO")
                    _nfe.infNFe.transp.transporta.UF = ValidateLib.enviNFe_ns.TUf.TO;

                _nfe.infNFe.transp.transporta.xEnder = OutBoxPar.nfeDoc.nfetransp.nfetransporta.xEnder;
                _nfe.infNFe.transp.transporta.xMun = OutBoxPar.nfeDoc.nfetransp.nfetransporta.xMun;
                _nfe.infNFe.transp.transporta.xNome = OutBoxPar.nfeDoc.nfetransp.nfetransporta.xNome;

                #endregion

                #region vol

                _nfe.infNFe.transp.vol = new ValidateLib.enviNFe_ns.TNFeInfNFeTranspVol[OutBoxPar.nfeDoc.nfetransp.nfevol.Count];
                int nvol = 0;
                foreach (NFevol nfevol in OutBoxPar.nfeDoc.nfetransp.nfevol)
                {
                    _nfe.infNFe.transp.vol[nvol] = new ValidateLib.enviNFe_ns.TNFeInfNFeTranspVol();
                    _nfe.infNFe.transp.vol[nvol].esp = nfevol.esp;

                    _nfe.infNFe.transp.vol[nvol].lacres = new ValidateLib.enviNFe_ns.TNFeInfNFeTranspVolLacres[nfevol.lacres.Count];
                    int nlacres = 0;
                    foreach (NFelacres nfelacres in nfevol.lacres)
                    {
                        _nfe.infNFe.transp.vol[nvol].lacres[nlacres] = new ValidateLib.enviNFe_ns.TNFeInfNFeTranspVolLacres();
                        _nfe.infNFe.transp.vol[nvol].lacres[nlacres].nLacre = nfelacres.nLacre;
                        nlacres++;
                    }

                    _nfe.infNFe.transp.vol[nvol].marca = nfevol.marca;
                    _nfe.infNFe.transp.vol[nvol].nVol = nfevol.nVol;
                    _nfe.infNFe.transp.vol[nvol].pesoB = nfevol.pesoB.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.transp.vol[nvol].pesoL = nfevol.pesoL.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.transp.vol[nvol].qVol = nfevol.qVol;

                    nvol++;
                }

                #endregion

                int ntransp = 0;
                if (!string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfetransp.balsa))
                {
                    ntransp++;
                }

                if (!string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfetransp.vagao))
                {
                    ntransp++;
                }

                if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.IdOutbox > 0)
                {
                    ntransp++;
                }

                if (OutBoxPar.nfeDoc.nfetransp.nfereboque.Count > 0)
                {
                    ntransp = ntransp + OutBoxPar.nfeDoc.nfetransp.nfereboque.Count;
                }

                if (ntransp > 0)
                {
                    _nfe.infNFe.transp.Items = new object[ntransp];
                    _nfe.infNFe.transp.ItemsElementName = new ValidateLib.enviNFe_ns.ItemsChoiceType5[ntransp];

                    ntransp = 0;
                    if (!string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfetransp.balsa))
                    {
                        _nfe.infNFe.transp.Items[ntransp] = OutBoxPar.nfeDoc.nfetransp.balsa;
                        _nfe.infNFe.transp.ItemsElementName[ntransp] = ValidateLib.enviNFe_ns.ItemsChoiceType5.balsa;
                        ntransp++;
                    }

                    if (!string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfetransp.vagao))
                    {
                        _nfe.infNFe.transp.Items[ntransp] = OutBoxPar.nfeDoc.nfetransp.vagao;
                        _nfe.infNFe.transp.ItemsElementName[ntransp] = ValidateLib.enviNFe_ns.ItemsChoiceType5.vagao;
                        ntransp++;
                    }

                    if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.IdOutbox > 0)
                    {
                        ValidateLib.enviNFe_ns.TVeiculo tveiculo = new ValidateLib.enviNFe_ns.TVeiculo();

                        tveiculo.placa = OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.placa;
                        tveiculo.RNTC = OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.RNTC;

                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "AC")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.AC;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "AL")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.AL;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "AM")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.AM;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "AP")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.AP;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "BA")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.BA;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "CE")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.CE;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "DF")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.DF;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "ES")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.ES;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "GO")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.GO;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "MA")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.MA;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "MG")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.MG;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "MS")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.MS;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "MT")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.MT;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "PA")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.PA;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "PB")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.PB;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "PE")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.PE;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "PI")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.PI;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "PR")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.PR;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "RJ")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.RJ;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "RN")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.RN;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "RO")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.RO;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "RR")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.RR;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "RS")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.RS;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "SC")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.SC;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "SE")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.SE;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "SP")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.SP;
                        if (OutBoxPar.nfeDoc.nfetransp.nfeveicTransp.UF == "TO")
                            tveiculo.UF = ValidateLib.enviNFe_ns.TUf.TO;

                        _nfe.infNFe.transp.Items[ntransp] = tveiculo;
                        _nfe.infNFe.transp.ItemsElementName[ntransp] = ValidateLib.enviNFe_ns.ItemsChoiceType5.veicTransp;
                        ntransp++;
                    }

                    if (OutBoxPar.nfeDoc.nfetransp.nfereboque.Count > 0)
                    {
                        foreach (NFereboque nfereboque in OutBoxPar.nfeDoc.nfetransp.nfereboque)
                        {
                            ValidateLib.enviNFe_ns.TVeiculo tveiculo = new ValidateLib.enviNFe_ns.TVeiculo();

                            tveiculo.placa = nfereboque.placa;
                            tveiculo.RNTC = nfereboque.RNTC;

                            if (nfereboque.UF == "AC")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.AC;
                            if (nfereboque.UF == "AL")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.AL;
                            if (nfereboque.UF == "AM")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.AM;
                            if (nfereboque.UF == "AP")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.AP;
                            if (nfereboque.UF == "BA")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.BA;
                            if (nfereboque.UF == "CE")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.CE;
                            if (nfereboque.UF == "DF")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.DF;
                            if (nfereboque.UF == "ES")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.ES;
                            if (nfereboque.UF == "GO")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.GO;
                            if (nfereboque.UF == "MA")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.MA;
                            if (nfereboque.UF == "MG")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.MG;
                            if (nfereboque.UF == "MS")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.MS;
                            if (nfereboque.UF == "MT")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.MT;
                            if (nfereboque.UF == "PA")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.PA;
                            if (nfereboque.UF == "PB")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.PB;
                            if (nfereboque.UF == "PE")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.PE;
                            if (nfereboque.UF == "PI")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.PI;
                            if (nfereboque.UF == "PR")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.PR;
                            if (nfereboque.UF == "RJ")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.RJ;
                            if (nfereboque.UF == "RN")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.RN;
                            if (nfereboque.UF == "RO")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.RO;
                            if (nfereboque.UF == "RR")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.RR;
                            if (nfereboque.UF == "RS")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.RS;
                            if (nfereboque.UF == "SC")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.SC;
                            if (nfereboque.UF == "SE")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.SE;
                            if (nfereboque.UF == "SP")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.SP;
                            if (nfereboque.UF == "TO")
                                tveiculo.UF = ValidateLib.enviNFe_ns.TUf.TO;

                            _nfe.infNFe.transp.Items[ntransp] = tveiculo;
                            _nfe.infNFe.transp.ItemsElementName[ntransp] = ValidateLib.enviNFe_ns.ItemsChoiceType5.reboque;
                            ntransp++;
                        }
                    }
                }

                #endregion

                #region cobr

                _nfe.infNFe.cobr = new ValidateLib.enviNFe_ns.TNFeInfNFeCobr();

                if (OutBoxPar.nfeDoc.nfecobr.nfefat.IdOutbox > 0)
                {
                    _nfe.infNFe.cobr.fat = new ValidateLib.enviNFe_ns.TNFeInfNFeCobrFat();
                    _nfe.infNFe.cobr.fat.nFat = OutBoxPar.nfeDoc.nfecobr.nfefat.nFat;
                    _nfe.infNFe.cobr.fat.vDesc = OutBoxPar.nfeDoc.nfecobr.nfefat.vDesc.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.cobr.fat.vLiq = OutBoxPar.nfeDoc.nfecobr.nfefat.vLiq.ToString().Replace(".", "").Replace(",", ".");
                    _nfe.infNFe.cobr.fat.vOrig = OutBoxPar.nfeDoc.nfecobr.nfefat.vOrig.ToString().Replace(".", "").Replace(",", ".");
                }

                if (OutBoxPar.nfeDoc.nfecobr.nfedup.Count > 0)
                {
                    _nfe.infNFe.cobr.dup = new ValidateLib.enviNFe_ns.TNFeInfNFeCobrDup[OutBoxPar.nfeDoc.nfecobr.nfedup.Count];
                    int ndup = 0;
                    foreach (NFedup nfedup in OutBoxPar.nfeDoc.nfecobr.nfedup)
                    {
                        _nfe.infNFe.cobr.dup[ndup] = new ValidateLib.enviNFe_ns.TNFeInfNFeCobrDup();
                        _nfe.infNFe.cobr.dup[ndup].dVenc = nfedup.dVenc;
                        _nfe.infNFe.cobr.dup[ndup].nDup = nfedup.nDup;
                        _nfe.infNFe.cobr.dup[ndup].vDup = nfedup.vDup.ToString().Replace(".", "").Replace(",", ".");

                        ndup++;
                    }
                }

                #endregion

                #region infAdic

                _nfe.infNFe.infAdic = new ValidateLib.enviNFe_ns.TNFeInfNFeInfAdic();
                if (!string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfeinfAdic.infAdFisco))
                {
                    _nfe.infNFe.infAdic.infAdFisco = OutBoxPar.nfeDoc.nfeinfAdic.infAdFisco;
                }
                if (!string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfeinfAdic.infCpl))
                {
                    _nfe.infNFe.infAdic.infCpl = OutBoxPar.nfeDoc.nfeinfAdic.infCpl;
                }



                if (OutBoxPar.nfeDoc.nfeinfAdic.nfeobsCont.Count > 0)
                {
                    _nfe.infNFe.infAdic.obsCont = new ValidateLib.enviNFe_ns.TNFeInfNFeInfAdicObsCont[OutBoxPar.nfeDoc.nfeinfAdic.nfeobsCont.Count];
                    int nobsCont = 0;
                    foreach (NFeobsCont nfeobsCont in OutBoxPar.nfeDoc.nfeinfAdic.nfeobsCont)
                    {
                        _nfe.infNFe.infAdic.obsCont[nobsCont] = new ValidateLib.enviNFe_ns.TNFeInfNFeInfAdicObsCont();
                        _nfe.infNFe.infAdic.obsCont[nobsCont].xCampo = nfeobsCont.xCampo;
                        _nfe.infNFe.infAdic.obsCont[nobsCont].xTexto = nfeobsCont.xTexto;
                        nobsCont++;
                    }
                }

                if (OutBoxPar.nfeDoc.nfeinfAdic.nfeobsFisco.Count > 0)
                {
                    _nfe.infNFe.infAdic.obsFisco = new ValidateLib.enviNFe_ns.TNFeInfNFeInfAdicObsFisco[OutBoxPar.nfeDoc.nfeinfAdic.nfeobsFisco.Count];
                    int nobsFisco = 0;
                    foreach (NFeobsFisco nfeobsFisco in OutBoxPar.nfeDoc.nfeinfAdic.nfeobsFisco)
                    {
                        _nfe.infNFe.infAdic.obsFisco[nobsFisco] = new ValidateLib.enviNFe_ns.TNFeInfNFeInfAdicObsFisco();
                        _nfe.infNFe.infAdic.obsFisco[nobsFisco].xCampo = nfeobsFisco.xCampo;
                        _nfe.infNFe.infAdic.obsFisco[nobsFisco].xTexto = nfeobsFisco.xTexto;
                        nobsFisco++;
                    }
                }

                #endregion

                #region Exportação

                if (_nfe.infNFe.dest.enderDest.UF == ValidateLib.enviNFe_ns.TUf.EX)
                {
                    if (_nfe.infNFe.ide.tpNF == ValidateLib.enviNFe_ns.TNFeInfNFeIdeTpNF.Item1)
                    {

                        _nfe.infNFe.exporta = new ValidateLib.enviNFe_ns.TNFeInfNFeExporta();

                        switch (OutBoxPar.nfeDoc.nfeExporta.UFSAIDAPAIS)
                        {
                            case "AC":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.AC;
                                break;
                            case "AL":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.AL;
                                break;
                            case "AM":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.AM;
                                break;
                            case "AP":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.AP;
                                break;
                            case "BA":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.BA;
                                break;
                            case "CE":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.CE;
                                break;
                            case "DF":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.DF;
                                break;
                            case "ES":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.ES;
                                break;
                            case "GO":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.GO;
                                break;
                            case "MA":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.MA;
                                break;
                            case "MG":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.MG;
                                break;
                            case "MS":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.MS;
                                break;
                            case "MT":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.MT;
                                break;
                            case "PA":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.PA;
                                break;
                            case "PB":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.PB;
                                break;
                            case "PE":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.PE;
                                break;
                            case "PI":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.PI;
                                break;
                            case "PR":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.PR;
                                break;
                            case "RJ":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.RJ;
                                break;
                            case "RN":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.RN;
                                break;
                            case "RO":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.RO;
                                break;
                            case "RR":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.RR;
                                break;
                            case "RS":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.RS;
                                break;
                            case "SC":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.SC;
                                break;
                            case "SE":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.SE;
                                break;
                            case "SP":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.SP;
                                break;
                            case "TO":
                                _nfe.infNFe.exporta.UFSaidaPais = ValidateLib.enviNFe_ns.TUfEmi.TO;
                                break;
                            default:
                                break;
                        }
                        if (!string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfeExporta.LOCEXPORTA))
                        {
                            _nfe.infNFe.exporta.xLocExporta = OutBoxPar.nfeDoc.nfeExporta.LOCEXPORTA;
                        }

                        if (!string.IsNullOrEmpty(OutBoxPar.nfeDoc.nfeExporta.LOCDESPACHO))
                        {
                            _nfe.infNFe.exporta.xLocDespacho = OutBoxPar.nfeDoc.nfeExporta.LOCDESPACHO;
                        }
                    }

                }
                #endregion

                #endregion
                _enviNFe.NFe[cont] = _nfe;
            }

            _objetoXML = new AppLib.Util.ObjetoXML();
            envioXML = _objetoXML.Escrever(_enviNFe);
            //Necessário remover manualmente devido a classe gerada pelo xsd.exe não aceitar a tag vazia
            envioXML = envioXML.Replace("<motDesICMS>6</motDesICMS>", "");
            envioXML = envioXML.Replace("<motDesICMS>1</motDesICMS>", "");
            envioXML = envioXML.Replace("<motDesICMS>3</motDesICMS>", "");

            envioXML = ValidateLib.Util.AdicionarAtributoTag(envioXML, "NFe");
            envioXML = ValidateLib.Util.RemoveAtributoInvalido(envioXML);
            envioXML = ValidateLib.Util.UTF16toUTF8(envioXML);

            //dirlei
            //ValidateLib.AssinaturaDigital _assinatura = new ValidateLib.AssinaturaDigital();

            

            //dirlei
            ///envioXML = _assinatura.Assinar(envioXML, "NFe", "infNFe", EmpresaPar.x509Certificate2);

            //dirlei
            //bool Flag = _assinatura.ValidarAssinatura(envioXML, "NFe");

            

            //dirlei
            //ValidateLib.Util.ValidaSchema(WebServicePar.CodEstrutura, WebServicePar.Versao, ValidateLib.Contexto.Session.DiretorioSchemas, WebServicePar.Schema, envioXML);
            envioXML = OBJETOS_VALIDATESERVICE.FuncoesAuxiliares.excluirTagsXML(envioXML);

            xdoc = new System.Xml.XmlDocument();
            xdoc.PreserveWhitespace = false;
            xdoc.LoadXml(envioXML); return envioXML;

           
        }
    }
}
