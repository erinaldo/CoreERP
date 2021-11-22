using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace PS.Lib
{
    public class PSInstance
    {
        private PS.Lib.Global gb = new Global();

        public string AssemblyName { get; set; }
        public string NameSpace { get; set; }
        public string TypeName { get; set; }

        public PSInstance()
        { 

        }

        private void CarregaReferenciaClasse(string TypeName)
        {
            for (int i = 0; i < PS.Lib.Contexto.PSPartList.Count; i++)
            {
                if (PS.Lib.Contexto.PSPartList[i].TypeName == TypeName)
                {
                    this.AssemblyName = PS.Lib.Contexto.PSPartList[i].AssemblyName;
                    this.NameSpace = PS.Lib.Contexto.PSPartList[i].NameSpace;
                    this.TypeName = PS.Lib.Contexto.PSPartList[i].TypeName;
                }
            }

            if (this.AssemblyName == null && this.NameSpace == null && this.TypeName == null)
            {
                throw new Exception("Não foi possível carregar os dados da classe.");
            }

            if (this.AssemblyName == string.Empty && this.NameSpace == string.Empty && this.TypeName == string.Empty)
            {
                throw new Exception("Não foi possível carregar os dados da classe.");                            
            }
        }

        public object CreateInstance(string AssemblyName, string NameSpace, string TypeName)
        {
            string dir = Convert.ToString(AppDomain.CurrentDomain.BaseDirectory);

            Assembly myDllAssembly = Assembly.LoadFile(string.Concat(dir, "\\", AssemblyName,".dll"));
            System.Type type = myDllAssembly.GetType(string.Concat(NameSpace,".", TypeName));
            return Activator.CreateInstance(type);
        }

        public object CreateInstance(string Folder, string AssemblyName, string NameSpace, string TypeName)
        {
            string dir = string.Concat(Convert.ToString(AppDomain.CurrentDomain.BaseDirectory), "\\", Folder);

            Assembly myDllAssembly = Assembly.LoadFile(string.Concat(dir, "\\", AssemblyName));
            System.Type type = myDllAssembly.GetType(string.Concat(NameSpace, ".", TypeName));
            return Activator.CreateInstance(type);
        }

        public object CreateInstance(string TypeName)
        {
            string dir = Convert.ToString(AppDomain.CurrentDomain.BaseDirectory);

            CarregaReferenciaClasse(TypeName);

            Assembly myDllAssembly = Assembly.LoadFile(string.Concat(dir, "\\", this.AssemblyName));
            System.Type type = myDllAssembly.GetType(string.Concat(this.NameSpace,".", this.TypeName));
            return Activator.CreateInstance(type);
        }

        public object CreateInstanceFormApp(string FormName)
        {
            string dir = Convert.ToString(AppDomain.CurrentDomain.BaseDirectory);

            CarregaReferenciaClasse(FormName.Replace("Frm", ""));

            Assembly myDllAssembly = Assembly.LoadFile(string.Concat(dir, "\\", this.AssemblyName));
            System.Type type = myDllAssembly.GetType(string.Concat(this.NameSpace, ".", FormName));
            return Activator.CreateInstance(type);
        }

        public object CreateInstanceFormEdit(string FormName)
        {
            string dir = Convert.ToString(AppDomain.CurrentDomain.BaseDirectory);

            CarregaReferenciaClasse(FormName.Replace("Edit",""));

            Assembly myDllAssembly = Assembly.LoadFile(string.Concat(dir, "\\", this.AssemblyName));
            System.Type type = myDllAssembly.GetType(string.Concat(this.NameSpace, ".", FormName));
            return Activator.CreateInstance(type);
        }
    }
}
