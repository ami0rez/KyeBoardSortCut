using ShortCuts;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MyFirstKeyLogger
{
    /// <summary>
    /// allows to listen to the keyboard and launch events :
    ///    - if altgr is clicked a boolean named "shouldExecute" becomes true
    ///      and the listener waits for another button to be clicked
    ///         - if another button is clicked before 1sec is passed the 
    ///           associated event( file(exe,dll,...)), if exists, will be launched
    /// 
    ///     if no button has been clicked for 1sec "shouldExecute" returns to its 
    ///     default value whitch is false
    /// 
    /// 
    /// </summary>
    class Listener : IDisposable
    {
        //this variable allows to start and stop the thread
        private ManualResetEvent _event = new ManualResetEvent(true);

        

        //a thread to keep listening to keyboard without freezing the Form
        private Thread listenerThrad;

        //this variable says should start listening to the keyboard to execute a command or naot
        public bool shouldExecute { get; private set; }

        //to executes tasks
        private Tasks tasks;

        Label statelbl;

        //Constructor
        public Listener(Label satate)
        {
            statelbl = satate;
            listenerThrad = new Thread(new ThreadStart(listen));
            listenerThrad.Start();
            stop();
            tasks = new Tasks();

        }

        //starts listening
        public void start()
        {
            //To Start the thread  
            _event.Set();
            Config.state = true;
            statelbl.ForeColor = System.Drawing.Color.Lime;
        }

        //stopes listening
        public void stop()
        {
            //To suspend the thread
            _event.Reset();
            Config.state = false;
            statelbl.ForeColor = System.Drawing.Color.DarkRed;
        }

        // To listen to what is beeing pressed in the keyboard
        private void listen()
        {
            int key = 0;
            while (true)
            {
                key = -1;
                _event.WaitOne();
                for (int i = 0; i < 255; i++)
                {
                    int keyState = NativeMethods.GetAsyncKeyState(i);

                    if (keyState == 1 || keyState == -32767)
                    {
                        key = i;
                        break;
                    }
                }
                if(key != -1)
                if(shouldExecute)
                    executeCommand(key);
                else
                    checkForCommand(key);
                Thread.Sleep(5);
            }
        }

        //check if AltGr is pressed so it will start listening
        private void checkForCommand(int keyval)
        {
            if (keyval == 165)
            {
                shouldExecute = true;
                new Thread(launchTimer).Start();
            }
        }

        //execute the command associated to the pressed buton
        private void executeCommand(int keyval)
        {
            if (Keys.RMenu == (Keys)keyval) return;
            AppendTextBox("AltGr + " + (Keys)keyval + "\n");
            if ((keyval>64 && keyval < 91) || (keyval < 106 && keyval > 95))
            {
                tasks.execute(keyval);
            }
            shouldExecute = false;
        }

        //add to Log
        private void AppendTextBox(string value)
        {
            Log.WriteToFile(DateTime.Now.ToShortTimeString() + ": " + value);
        }

        //kils the listener thread
        public void Dispose()
        {
            listenerThrad.Abort();
            _event.Dispose();
        }

        //gives one second to click on a button otherwise it stopes listening
        private void launchTimer()
        {
            int i = 0;
            while (shouldExecute && i < 10)
            {
                Thread.Sleep(100);
                i++;
            }
            shouldExecute = false;
            Thread.CurrentThread.Abort();
        }

    }
}
