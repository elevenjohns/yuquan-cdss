using System;
using System.Activities;
using System.Reflection;
using System.Reflection.Emit;

namespace WuKeSong.Workflows
{

    public sealed class ReflectionEmitCodeActivity : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> Text { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            string text = context.GetValue(this.Text);

            // May use struct or xml string of app spec
            GenerateApplication(text);
        }

        // The service generates specific application based on user request
        private void GenerateApplication(string appSpec)
        {
            // create a dynamic assembly and module 
            AssemblyName assemblyName = new AssemblyName("MyApp");
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave,@"c:\"); // define output dir
            ModuleBuilder module = assemblyBuilder.DefineDynamicModule("MyApp.exe");

            // create a new type to hold our Main method
            TypeBuilder typeBuilder = module.DefineType("MyType", TypeAttributes.Public | TypeAttributes.Class);

            // create the Main(string[] args) method
            MethodBuilder methodbuilder = typeBuilder.DefineMethod("Main", MethodAttributes.HideBySig | MethodAttributes.Static | MethodAttributes.Public, typeof(void), new Type[] { typeof(string[]) });

            // generate the IL for the Main method
            ILGenerator ilGenerator = methodbuilder.GetILGenerator();

            // Parse app spec
            ilGenerator.EmitWriteLine(appSpec);
            ilGenerator.Emit(OpCodes.Ret);

            /* Add method implemented by Emit
            ilGenerator.DeclareLocal(typeof(System.Single));
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Ldarg_2);
            ilGenerator.Emit(OpCodes.Add_Ovf);
            ilGenerator.Emit(OpCodes.Conv_R4);
            ilGenerator.Emit(OpCodes.Stloc_0);
            ilGenerator.Emit(OpCodes.Ldloc_0);
            ilGenerator.Emit(OpCodes.Ret);
            */

            // bake it
            Type MyType = typeBuilder.CreateType();

            // run it
            MyType.GetMethod("Main").Invoke(null, new string[] { null });

            // set the entry point for the application and save it
            assemblyBuilder.SetEntryPoint(methodbuilder, PEFileKinds.ConsoleApplication);
            assemblyBuilder.Save("MyApp.exe");
        }
    }
}
