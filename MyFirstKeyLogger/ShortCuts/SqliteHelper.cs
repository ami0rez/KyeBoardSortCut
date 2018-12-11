using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MyFirstKeyLogger
{
    /// <summary>
    /// this class allows to control an Sqlite Database (Insert, Update, Delete, Select)
    /// </summary>
    class SqliteHelper : IDisposable
    {
        SQLiteConnection m_dbConnection;
        SQLiteCommand command;

        public SqliteHelper()
        {
            ConnectToDatabase();
        }

        private void ConnectToDatabase()
        {
            if (!System.IO.File.Exists(@"shortcuts"))
            {
                SQLiteConnection.CreateFile("shortcuts");
                m_dbConnection = new SQLiteConnection("Data Source=shortcuts;Version=3;");
                command = new SQLiteCommand("", m_dbConnection);
                addTables();
            }
            else
            {
                m_dbConnection = new SQLiteConnection("Data Source=shortcuts;Version=3;");
                command = new SQLiteCommand("", m_dbConnection);
            }


        }
        private void addTables()
        {
            string tableCommand = @"CREATE TABLE `command` (
	                                `id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	                                `button`	INTEGER NOT NULL ,
	                                `path`	TEXT NOT NULL,
	                                `description`	BLOB NOT NULL,
	                                `fixed`	TEXT NOT NULL DEFAULT 0
                                  );";
            executeCommand(tableCommand);
        }

        private void executeCommand(string str)
        {
            if(m_dbConnection.State == System.Data.ConnectionState.Closed)
                m_dbConnection.Open();
            command.CommandText = str;
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }

         
        //
        // Command
        //
        public List<Command> getCommands()
        {
            List<Command> contents = new List<Command>();
            string sql = "select * from command";
            command.CommandText = sql;
            m_dbConnection.Open();
            SQLiteDataReader r = command.ExecuteReader();
            while (r.Read())
            {
                contents.Add(new Command()
                {
                    id = int.Parse(Convert.ToString(r["id"])),
                    button = int.Parse(Convert.ToString(r["button"])),
                    path = Convert.ToString(r["path"]),
                    description = Convert.ToString(r["description"]),
                    _fixed = int.Parse(Convert.ToString(r["fixed"])) == 1
                });
            }
            r.Close();
            m_dbConnection.Close();
            return contents;
        }

        public void addCommand(Command command)
        {
            string sql = string.Format("insert into command(button, path, description, fixed) values(\"{0}\", \"{1}\", \"{2}\", \"{3}\")",
                command.button, command.path, command.description, command._fixed?1:0);
            executeCommand(sql);
        }

        public void removeCommand(int commandId)
        {
            string sql = string.Format("delete from command where id = '{0}'", commandId);
            executeCommand(sql);
        }

        public void updateCommand(Command command)
        {
            string sql = string.Format("update command set button = \"{0}\", path = \"{1}\", description = \"{2}\", fixed = \"{3}\" where id = \"{4}\"",
                command.button, command.path, command.description, command._fixed?1:0, command.id);
            executeCommand(sql);
        }

        public bool CommandExist(int buttonValue)
        {
            Command cmd = new Command();
            string sql = "select * from command where button = '"+buttonValue+"'";
            command.CommandText = sql;
            m_dbConnection.Open();
            SQLiteDataReader r = command.ExecuteReader();
            bool b =  r.HasRows;
            r.Close();
            m_dbConnection.Close();
            return b;
        }

        public int CommandId(Command cmd)
        {
            string sql = "select * from command where button = '" + cmd.button + "'";
            command.CommandText = sql;
            m_dbConnection.Open();
            SQLiteDataReader r = command.ExecuteReader();
            if (r.HasRows)
            {
                r.Close();
                m_dbConnection.Close();
                return -1;
            }
                
            r.Read();
            int id = int.Parse(Convert.ToString(r["id"]));
            r.Close();
            m_dbConnection.Close();
            return id;
        }

        public void Dispose()
        {
            m_dbConnection.Dispose();
            command.Dispose();
        }
    }
}