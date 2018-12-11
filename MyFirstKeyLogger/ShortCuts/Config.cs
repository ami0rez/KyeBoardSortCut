using System.Collections.Generic;

namespace MyFirstKeyLogger
{
    class Config
    {
        private static SqliteHelper db;

        public static SqliteHelper database
        {
            get
            {
                return db;
            }
            set
            {
                db = value;
                commands = db.getCommands();
            }
        }

        public static List<Command> commands;

        public static Listener listener;

        public static ShortCut Main;

        public static Notification notifier;

        public static bool state;

        public static System.Windows.Forms.ToolTip infott;


    }
}
