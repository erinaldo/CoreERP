using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class PastaParams : ParamsBase
    {
        private AppLib.Data.Connection _conn;

        private int _idConfigDIR;
        private string _nome;
        private string _caminho;
        private int _ativo;

        [ParamsAttribute("IDCONFIGDIR")]
        [DataMember]
        public int IdConfigDIR
        {
            get
            {
                return this._idConfigDIR;
            }
            set
            {
                this._idConfigDIR = value;
            }
        }

        [ParamsAttribute("NOME")]
        [DataMember]
        public string Nome
        {
            get
            {
                return this._nome;
            }
            set
            {
                this._nome = value;
            }
        }

        [ParamsAttribute("CAMINHO")]
        [DataMember]
        public string Caminho
        {
            get
            {
                return this._caminho;
            }
            set
            {
                this._caminho = value;
            }
        }

        [ParamsAttribute("ATIVO")]
        [DataMember]
        public int Ativo
        {
            get
            {
                return this._ativo;
            }
            set
            {
                this._ativo = value;
            }
        }

        public static PastaParams ReadByIdConfigDIR(params object[] parameters)
        {
            string sSql = @"SELECT * FROM ZCONFIGDIR WHERE IDCONFIGDIR = ?";
            PastaParams _pastaParams = new PastaParams();
            _pastaParams.ReadFromCommand(sSql, parameters);
            return _pastaParams;
        }

        public void Execute()
        {
            this.Execute(Contexto.Session.DiretorioDefault + this.Caminho);
        }

        private void Execute(string caminho)
        {
            String[] arquivos = System.IO.Directory.GetFiles(caminho);
            for (int i = 0; i < arquivos.Length; i++)
            {
                System.Console.WriteLine("arquivo: " + arquivos[i]);
                System.IO.FileInfo fileInfo = new FileInfo(arquivos[i]);
                System.IO.File.Move(arquivos[i], "Anexos\\" + fileInfo.Name);

                new Descompactar("DIR", this.IdConfigDIR);
                new Importar().ImportarArquivo("DIR", this.IdConfigDIR);
            }

            String[] pastas = System.IO.Directory.GetDirectories(caminho);
            for (int i = 0; i < pastas.Length; i++)
            {
                System.Console.WriteLine("pastas: " + pastas[i]);
                this.Execute(pastas[i]);

                try
                {
                    System.IO.Directory.Delete(pastas[i]);
                }
                catch (Exception) { }
            }
        }
    }
}
