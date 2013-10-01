using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace RuleManager
{
    public class Rule
    {
        public string Name { get; set; }
        public string ModifierString { get; private set; }
        public Output OutputVal { get; private set; }
        public RuleCollection ParentCollection { get; set; }

        /// <summary>
        /// The method to append to a previous set value. 
        /// If a <code>RuleCollection</code> has multiple instances of a single output, this governs how the value should be appended onto the previous value.
        /// </summary>
        public AppendMethod AppendMode { get; private set; }

        private MethodInfo compiledModifiers;
        public bool SetModiferString(string str)
        {
            ModifierString = str;
            try
            {
                BuildClass(ref str);
                Assembly asm = BuildAssembly(str);
                Type type = asm.GetType(GetType().Namespace + ".Calculator");
                compiledModifiers = type.GetMethod("Calculate");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show( "Failed to compile modifier string.\n" + ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                return false;
            }

            return true;
        }
      
        public void SetAppendMode(AppendMethod method)
        {
            this.AppendMode = method;
        }

        public void SetOutput(Output output)
        {
            this.OutputVal = output;
        }

        /// <summary>
        /// Execute the compiled modifiers with a specific input value
        /// </summary>
        /// <param name="input">The input value to run through the compiled modifiers</param>
        /// <returns>Output value calculated by the modifier</returns>
        public float ExecuteModifiers()
        {
            if (compiledModifiers == null) { return 0; }
            this.ParentCollection.Util.CurTime();
            return (float)compiledModifiers.Invoke(null, new object[] { this.ParentCollection.Util} );
        }

        private void BuildClass( ref string code )
        {
            StringBuilder classbuilder = new StringBuilder("using System;");
            classbuilder.Append("using System.Dynamic;");
            classbuilder.Append("using System.ComponentModel;");
            classbuilder.Append("using Microsoft.CSharp;");
            classbuilder.AppendFormat("namespace {0}\r\n{{\r\n", this.GetType().Namespace);
            classbuilder.Append("public class Calculator\r\n{\r\n");
            classbuilder.Append("public static float Calculate(dynamic util )\r\n{\r\n");
            classbuilder.Append(code);
            classbuilder.Append("\r\n}\r\n}\r\n}");
            code = classbuilder.ToString();
        }

        private Assembly BuildAssembly(string code)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CompilerParameters compilerparams = new CompilerParameters();
            compilerparams.GenerateExecutable = false;
            compilerparams.GenerateInMemory = true;
            compilerparams.ReferencedAssemblies.Add("System.dll");
            compilerparams.ReferencedAssemblies.Add("Microsoft.CSharp.dll");
            compilerparams.ReferencedAssemblies.Add("System.Core.dll");
            compilerparams.ReferencedAssemblies.Add("System.Data.dll");
            CompilerResults results = provider.CompileAssemblyFromSource(compilerparams, code);
            if (results.Errors.HasErrors)
            {
                StringBuilder errors = new StringBuilder("Compiler Errors :\r\n");
                foreach (CompilerError error in results.Errors)
                {
                    errors.AppendFormat("Line {0},{1}\t: {2}\n", error.Line, error.Column, error.ErrorText);
                }             
                throw new Exception(errors.ToString());
            }
            else
            {
                return results.CompiledAssembly;
            }
        }

        private object ExecuteCode(string code, string namespacename, string classname, string functionname, bool isstatic, params object[] args)
        {
            object returnval = null;
            Assembly asm = BuildAssembly(code);
            object instance = null;
            Type type = null;
            if (isstatic)
            {
                type = asm.GetType(namespacename + "." + classname);
            }
            else
            {
                instance = asm.CreateInstance(namespacename + "." + classname);
                type = instance.GetType();
            }
            MethodInfo method = type.GetMethod(functionname);
            returnval = method.Invoke(instance, args);
            return returnval;
        }
    }

    public class Output
    {
        /// <summary>
        /// The unique name of this output
        /// </summary>
        public string Name { get; private set; }

        public Output(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    /// <summary>
    /// An enumeration of the types of ways an output value can be appended onto a previous output value of the same type
    /// </summary>
    public enum AppendMethod
    {
        /// <summary>
        /// Set the value with no regard of any previous value
        /// </summary>
        Set,
        /// <summary>
        /// Add this output's value onto the previous output's value
        /// </summary>
        Add,
        /// <summary>
        /// Multiply this output's value to the previous output's value
        /// </summary>
        Multiply
    }
}
