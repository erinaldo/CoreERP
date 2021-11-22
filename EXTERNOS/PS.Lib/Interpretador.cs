using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class Interpretador
    {
        // VARIAVEIS
        private System.CodeDom.Compiler.ICodeCompiler compilador;
        private Microsoft.CSharp.CSharpCodeProvider compiladorCSharp;
        private System.CodeDom.Compiler.CompilerParameters parametros;
        private System.CodeDom.Compiler.CompilerResults resultado;
        private System.Reflection.Assembly assembly;
        private Object objeto;
        private Object[] parametrosCodigo;
        private Object resultadoCodigo;

        // PROPRIEDADE(S)
        public String comando { get; set; }

        // CONSTRUTOR
        public Interpretador()
        {
            compiladorCSharp = new Microsoft.CSharp.CSharpCodeProvider();
            compilador = compiladorCSharp.CreateCompiler();

            parametros = new System.CodeDom.Compiler.CompilerParameters();
            parametros.ReferencedAssemblies.Add("System.dll");
            parametros.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            parametros.ReferencedAssemblies.Add("PS.Lib.dll");
            //parametros.ReferencedAssemblies.Add("PS.Lib.Winforms.dll");
            parametros.ReferencedAssemblies.Add("PS.Glb.dll");
            parametros.GenerateInMemory = true;
        }

        // MÉTODO COMPILAR
        public void Compilar()
        {
            string msgErro = string.Empty;
            resultado = compilador.CompileAssemblyFromSource(parametros, comando);

            if (resultado.Errors.HasErrors)
            {
                msgErro = resultado.Errors.Count.ToString() + "Erro: ";

                for (int x = 0; x < resultado.Errors.Count; x++)
                {
                    msgErro = msgErro + "\r\nLinha: " + resultado.Errors[x].Line.ToString() + " -> " + resultado.Errors[x].ErrorText;
                }
            }

            if (!string.IsNullOrEmpty(msgErro))
            {
                throw new Exception(msgErro);
            }
        }

        // MÉTODO EXECUTAR
        public object Executar(params object[] Parameters)
        {
            try
            {
                Compilar();

                assembly = resultado.CompiledAssembly;
                objeto = assembly.CreateInstance("TempNamespace.TempClass");

                parametrosCodigo = new object[0];

                resultadoCodigo = objeto.GetType().InvokeMember("TempMethod", System.Reflection.BindingFlags.InvokeMethod, null, objeto, parametrosCodigo);
                return resultadoCodigo.ToString();
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception(err);
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception(err);
            }
        }
    }
}
