using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MyFirstKeyLogger
{
    /// <summary>
    /// the main form that containes Keyboard buttons and theire events
    /// When the user clicks on AltGr and another button from the ones specified
    /// on the form an event will launch -if exists- and will launche a file, if the 
    /// file is a dll file the a method named "run" from the "Main" class will be called
    /// </summary>
    public partial class ShortCut : Form
    {
        //
        // Fields
        //

        //A class to list Items in the winform
        //constructor
        public ShortCut()
        {
            InitializeComponent();
            Config.database = new SqliteHelper();
            Config.notifier = new Notification();
            Config.Main = this;


            Config.listener = new Listener(statelbl);
            ButtonList btnlist = new ButtonList(alphaPadFLP, numPadFLP);
            btnlist.fill();

            addtoStartUp();
        }

        private void addtoStartUp()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if(string.IsNullOrEmpty((string)rk.GetValue("SortCuts")))
            {
                rk.SetValue("SortCuts", Application.ExecutablePath);
            }

        }

        //
        // winform Event
        //

        //Start Listening
        private void button1_Click(object sender, EventArgs e)
        {
            Config.listener.start();
        }

        //Stop Listening
        private void button2_Click(object sender, EventArgs e)
        {
            Config.listener.stop();
        }

        //add new Event
        private void button_Click(object sender, EventArgs e)
        {
            int btnVal = 96 + int.Parse(((Button)sender).Text);
            Command cmd = Config.commands.SingleOrDefault(c => c.button == btnVal);
            if(cmd == null)
                cmd = new Command()
            {
                id = -1,
                button = btnVal,
                path = "Choose Path",
                description = "Description..."
            };
            new Item(cmd).ShowDialog();
        }

        //prevents form from closing and hides it
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        //Starts and stops the shortcut listener
        private void statelbl_Click(object sender, EventArgs e)
        {
            if (Config.state)
                Config.listener.stop();
            else
                Config.listener.start();
        }
    }
}
