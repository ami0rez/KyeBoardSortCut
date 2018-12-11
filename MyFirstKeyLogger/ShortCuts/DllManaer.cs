using System;
using System.IO;
using System.Reflection;

namespace MyFirstKeyLogger
{
    /// <summary>
    /// Manages dll files
    /// </summary>
    class DllManaer
    {
        //Add dll file to DLL folder
        public static string add(string path)
        {
            if (!Directory.Exists("DLL"))
                Directory.CreateDirectory("DLL");
            string filename = "Dll\\" + Path.GetFileName(path);
            File.Copy(path, filename);

            return filename;
        }

        //To Run the run function of the Main class in the dll file 
        public static void runDll(string name)
        {
            string path = name;
            var DLL = Assembly.LoadFile(Path.GetFullPath(path));

            foreach (Type type in DLL.GetExportedTypes())
            {
                if (type.Name == "Main")
                {
                    var c = Activator.CreateInstance(type);
                    type.InvokeMember("run", BindingFlags.InvokeMethod, null, c, null);
                    break;
                }
            }

            Console.ReadLine();
        }
    }
}
