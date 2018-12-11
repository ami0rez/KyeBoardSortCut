using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MyFirstKeyLogger
{
    /// <summary>
    /// this class renders the buttons (alphabet, numbers) and theire events
    /// </summary>
    class ButtonList
    {
        FlowLayoutPanel aplhaParent, numParent;

        string alpha = "AZERTYUIOPQSDFGHJKLMWXCVBN";
        string num = "7894561230";

        public ButtonList(FlowLayoutPanel alphaParent, FlowLayoutPanel numParent)
        {
            this.aplhaParent = alphaParent;
            this.numParent = numParent;
            makeTooltip();
        }

        private Panel makeButton(Icon icon, string key, string desc)
        {
            Panel button = new Panel();
            PictureBox pictureKey = new PictureBox();
            Label buttonKey = new Label();
            // 
            // button
            // 
            button.BorderStyle = BorderStyle.FixedSingle;
            button.Controls.Add(pictureKey);
            button.Controls.Add(buttonKey);
            button.Location = new System.Drawing.Point(5, 10);
            button.Margin = new Padding(3, 6, 3, 6);
            button.Size = new Size(58, 56);
            button.AllowDrop = true;
            button.DragEnter += buttonDrag_Enter;
            button.DragLeave += buttonDrag_Leave;
            button.DragDrop += buttonDrag_Drop;


            switch (key)
            {
                case "Q":
                        button.Margin = new Padding(25, 6, 3, 6);
                    break;
                case "W":
                        button.Margin = new Padding(45, 6, 3, 6);
                    break;
                case "0":
                        button.Margin = new Padding(75, 6, 3, 6);
                    break;
                }

            // 
            // pictureKey
            // 
            pictureKey.Location = new System.Drawing.Point(32, 37);
            pictureKey.Name = "pictureKey";
            if (icon != null)
                pictureKey.Image = icon.ToBitmap();
            pictureKey.Size = new System.Drawing.Size(24, 17);
            pictureKey.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureKey.TabIndex = 1;
            pictureKey.TabStop = false;
            pictureKey.Click += button_click;

            // 
            // buttonKey
            // 
            buttonKey.Dock = System.Windows.Forms.DockStyle.Fill;
            buttonKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            buttonKey.Location = new System.Drawing.Point(0, 0);
            buttonKey.Name = "buttonKey";
            buttonKey.Size = new System.Drawing.Size(56, 54);
            buttonKey.TabIndex = 0;
            buttonKey.Text = key; 
            buttonKey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            buttonKey.Click += button_click;
            Config.infott.SetToolTip(buttonKey, desc);


            button.Controls.Add(buttonKey);
            button.Controls.Add(pictureKey);
            pictureKey.BringToFront();

            return button;
        }

        private void buttonDrag_Drop(object sender, DragEventArgs e)
        {
            Config.Main.Focus();
            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            int btnVal = getButtonVal(sender);

            string file = fileList[0];

                Command cmd = Config.commands.SingleOrDefault(c => c.button == btnVal);
                if (cmd == null)
                    cmd = new Command()
                    {
                        id = -1,
                        button = btnVal,
                    };
                cmd.path = file;
                cmd.description = Path.GetFileNameWithoutExtension(file);
                
                if (cmd.id < 0)
                {
                    Config.database.addCommand(cmd);
                    Config.commands.Add(cmd);
                }
                else
                {
                    Config.database.updateCommand(cmd);
                }

                updateButton(sender, cmd);

            ((Panel)sender).BackColor = Color.White;
        }

        private void buttonDrag_Leave(object sender, EventArgs e)
        {
            ((Panel)sender).BackColor = Color.White;
        }

        private void buttonDrag_Enter(object sender, DragEventArgs e)
        {
            ((Panel)sender).BackColor = Color.LightBlue;
            e.Effect = DragDropEffects.Copy;
        }

        private void button_click(object sender, EventArgs e)
        {
            int btnVal = getButtonVal(sender);

            Command cmd = Config.commands.SingleOrDefault(c => c.button == btnVal);
            if (cmd == null)
                cmd = new Command()
                {
                    id = -1,
                    button = btnVal,
                    path = "Choose Path",
                    description = "Description..."
                };
            new Item(cmd).ShowDialog();
            if (Item.actionDone)
            {
                PictureBox picbox = ((Control)sender).Parent.Controls.OfType<PictureBox>().First();
                Command associaltedCmd = Config.commands.SingleOrDefault(c => c.button == cmd.button);
                updateButton(sender, associaltedCmd);

            }
        }

        public void addItem(Icon icon, string key,string desc, bool alpha)
        {
            Panel button = makeButton(icon, key, desc);
            if(alpha)
                aplhaParent.Controls.Add(button);
            else
                numParent.Controls.Add(button);

        }

        public void fill()
        {
            fillAlpha();
            fillNumPad();
        }

        public void fillAlpha()
        {
            Command cmd;
            Icon ico;
            foreach (char c in alpha)
            {
                if ((cmd = Config.commands.SingleOrDefault(cm => (c + "") == ((Keys)cm.button) + "")) != null)
                {
                    
                    try
                    {
                        //MessageBox.Show(((Keys)cmd.button) + ": " + cmd.path);
                        ico = Icon.ExtractAssociatedIcon(cmd.path);
                        addItem(ico, c + "", cmd.description, true);
                    }
                    catch
                    {
                        addItem(null, c + "", cmd.description, true);
                    }
                }
                else
                {
                    addItem(null, c + "", "Not yet configured", true);
                }
            }
        }

        public void fillNumPad()
        {
            Command cmd;
            Icon ico;
            foreach (char c in num)
            {
                if ((cmd = Config.commands.SingleOrDefault(cm => (c+"") == (cm.button - 96)+"")) != null)
                {
                    ico = Icon.ExtractAssociatedIcon(cmd.path);
                    try
                    {
                        addItem(ico, c + "", cmd.description, false);
                    }
                    catch
                    {
                        addItem(null, c + "", cmd.description, false);
                    }
                }
                else
                {
                    addItem(null, c + "", "Not yet configured", false);
                }
            }
        }

        private int getButtonVal(object sender)
        {
            Label lbl;
            if (sender is Label)
                lbl = (Label)sender;
            else if(sender is PictureBox)
                lbl = ((PictureBox)sender).Parent.Controls.OfType<Label>().First();
            else
                lbl = ((Panel)sender).Controls.OfType<Label>().First();


            int btnVal;
            if (num.Contains(lbl.Text[0]))
                btnVal = 96 + int.Parse(lbl.Text);
            else
                btnVal = lbl.Text[0];

            return btnVal;
        }

        private void updateButton(object sender, Command cmd)
        {
            PictureBox picbox;
            Label lbl;
            if (sender is Panel)
            { 
                picbox = ((Control)sender).Controls.OfType<PictureBox>().First();
                lbl = ((Control)sender).Controls.OfType<Label>().First();
            }
            else if(sender is Label)
            {
                picbox = ((Control)sender).Parent.Controls.OfType<PictureBox>().First();
                lbl = ((Label)sender);
            }
            else
            {
                picbox = ((PictureBox)sender);
                lbl = ((PictureBox)sender).Parent.Controls.OfType<Label>().First();
            }


            try
            {
                picbox.Image = Icon.ExtractAssociatedIcon(cmd.path).ToBitmap();
                picbox.BackColor = Color.White;
                Config.infott.SetToolTip(lbl, cmd.description);
            }
            catch
            {
                picbox.Image = null;
                Config.infott.SetToolTip(lbl, "Not yet configured");
            }
        }

        private void makeTooltip()
        {
            Config.infott = new ToolTip();
            Config.infott.ToolTipTitle = "ShortCut";
            Config.infott.UseFading = true;
            Config.infott.UseAnimation = true;
            Config.infott.IsBalloon = true;
            Config.infott.ShowAlways = true;
            Config.infott.AutoPopDelay = 3000;
            Config.infott.InitialDelay = 1000;
            Config.infott.ReshowDelay = 500;
        }
    }
}
