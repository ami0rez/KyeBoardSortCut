using System;
using System.Linq;
using System.Windows.Forms;

namespace MyFirstKeyLogger
{
    /// <summary>
    /// this class allows to modify the command data ( path and description)
    /// </summary>
    public partial class Item : Form
    {
        private Command cmd;
        private SqliteHelper dataBase;

        public static bool actionDone;

        public Item(object command)
        {
            InitializeComponent();
            dataBase = Config.database;
            cmd = (Command)command;
            init();
            actionDone = false;

        }

        private void init()
        {
            if (cmd == null)
            {
                this.Close();
                return;
            }
            cmdNumber.Text = ((Keys)cmd.button)+"";
            cmdPath.Text = cmd.path;
            cmdDescription.Text = cmd.description;
            if (cmd.id == -1)
            {
                ActionBtn.Text = "Add";
                removeBtn.Visible = false;
            }

            else
            {
                ActionBtn.Text = "Save";
                removeBtn.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd.path = cmdPath.Text;
            if (cmd.path.EndsWith(".dll"))
                cmd.path = DllManaer.add(cmd.path);
            cmd.description = cmdDescription.Text;
            if (cmd.id == -1)
            {
                dataBase.addCommand(cmd);
                cmd.id = Config.database.CommandId(cmd);
                Config.commands.Add(cmd);
            }
            else
            {
                dataBase.updateCommand(cmd);
                Command com = Config.commands.SingleOrDefault(c => c.id == cmd.id);
                com.path = cmd.path;
                com.description = cmd.description;
            }
            actionDone = true;

            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();

            if(file.ShowDialog() == DialogResult.OK)
            {
                cmdPath.Text = file.FileName;
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            actionDone = false;
        }

        private void remove_click(object sender, EventArgs e)
        {
            dataBase.removeCommand(cmd.id);
            Config.commands.Remove(Config.commands.SingleOrDefault(c => c.id == cmd.id));
            actionDone = true;
            this.Close();
        }
    }
}
