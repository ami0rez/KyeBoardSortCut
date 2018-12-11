using System.Linq;
using System.Diagnostics;
using System.IO;

namespace MyFirstKeyLogger
{
    /// <summary>
    /// to execute events or tasks
    /// </summary>
    class Tasks
    {
        public Tasks()
        {

        }

        //get the tight path from shortcuts and run it
        public void execute(int key)
        {

            Command command = Config.commands.SingleOrDefault(x => x.button == key);
            if (command == null) return;
            if (command.path.EndsWith(".dll"))
                DllManaer.runDll(command.path);
            else
            {
                if (File.Exists(command.path))
                    Process.Start(command.path);
                else
                    Config.notifier.notify("File does not Exists");
            }
                
        }

    }
}
