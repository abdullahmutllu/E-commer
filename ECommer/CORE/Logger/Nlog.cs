using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Logger
{
    public class Nlog
    {
        public static void LogErrorFile(string message, string path)
        {
            //if (Directory.Exists("ErrorLog"))
            //    Directory.CreateDirectory("ErrorLog");
            var currentDirectory = Directory.GetCurrentDirectory();
            StreamWriter streamWriter = new StreamWriter(path == null ? "/ErrorLog/Error.txt" : path, true);
            streamWriter.WriteLine(message);
            streamWriter.Flush();
            streamWriter.Close();
        }
    }
}
