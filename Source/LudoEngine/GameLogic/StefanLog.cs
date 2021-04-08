using System;
using System.Collections.Generic;
using System.IO;
using LudoEngine.BoardUnits.Intefaces;
using LudoEngine.BoardUnits.Main;
using LudoEngine.Enum;
using LudoEngine.Models;

namespace LudoEngine.GameLogic
{
    public class StefanLog
    {
        private StreamWriter Logger;
        public StefanLog(TeamColor color)
        {
            int number = 0;
            if (!Directory.Exists(Environment.CurrentDirectory + @"\StephanLogs")) Directory.CreateDirectory(Environment.CurrentDirectory + @"\StephanLogs");
            foreach (FileInfo finf in new DirectoryInfo(Environment.CurrentDirectory + @"\StephanLogs").GetFiles())
            {
                if (finf.Name.StartsWith($"stephan_{color.ToString()}") && finf.Extension == ".log")
                {
                    number++;
                }
            }
            Logger = new StreamWriter($@"{Environment.CurrentDirectory}\StephanLogs\stephan_{color.ToString()}{number.ToString()}.log");
        }
        public void WriteLogging(string input)
        {
            Logger.Write(input);
            Logger.WriteLine("");
            Logger.Flush();
        }
    }
}