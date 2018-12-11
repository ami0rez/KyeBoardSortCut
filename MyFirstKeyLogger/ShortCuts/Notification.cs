using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyFirstKeyLogger
{
    /// <summary>
    /// this class handles the NotifyIcon:
    /// - a NotifyIcon apears in the taskBar
    /// - the notifyIcon has a menu(Show, Start, Stop, Exit)
    ///     - Start and Stop to control the listenr
    ///     - Exit to exit the program
    /// - a method to send notifications to the user
    /// </summary>
    class Notification : IDisposable
    {
        NotifyIcon icon;

        //Constructor
        public Notification()
        {
            icon = new NotifyIcon();
            setNotificationIcon();
        }

        //TO Initialize the NotifyIcon
        private void setNotificationIcon()
        {
            ContextMenuStrip cms = new ContextMenuStrip();
            ToolStripItem show = new ToolStripMenuItem("Show");
            ToolStripItem start = new ToolStripMenuItem("Start");
            ToolStripItem stop = new ToolStripMenuItem("Stop");
            ToolStripItem exit = new ToolStripMenuItem("Exit");

            icon.DoubleClick += show_clicked;
            show.Click += show_clicked;
            start.Click += start_clicked;
            stop.Click += stop_clicked;
            exit.Click += exit_clicked;

            cms.Items.Add(show);
            cms.Items.Add(start);
            cms.Items.Add(stop);
            cms.Items.Add(exit);

            icon.ContextMenuStrip = cms;
            icon.Icon = new Icon(@"C:\Users\Amine\Downloads\Temp\search.ico");
            icon.Text = "ShortCuts";
            icon.Visible = true;
        }

        private void exit_clicked(object sender, EventArgs e)
        {
            icon.Visible = false;
            Environment.Exit(0);
        }

        private void stop_clicked(object sender, EventArgs e)
        {
            Config.listener.stop();
        }

        private void start_clicked(object sender, EventArgs e)
        {
            Config.listener.start();
        }

        private void show_clicked(object sender, EventArgs e)
        {
            Config.Main.Show();
        }

        //send notifications to the user
        public void notify(string info)
        {
            icon.BalloonTipIcon = ToolTipIcon.Info;
            icon.BalloonTipText = info;
            icon.BalloonTipTitle = "ShortCuts";
            icon.ShowBalloonTip(1000);
        }

        public void Dispose()
        {
            icon.Dispose();
        }
    }
}
