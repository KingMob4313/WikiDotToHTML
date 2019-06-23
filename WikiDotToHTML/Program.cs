using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WikiDotToHTML
{
    class Program
    {
        static readonly string StTag = "Niv:\t##666666|";
        static readonly string YaraTag = "YaraSherred:\t##666666|";
        static readonly string KaiTag = "Kai_Tukov:\t##110481|";
        //static readonly string DamianTag = "DamianStark:\t##208020|";
        static readonly string DamianTag = "DamianStark:\t##800000|";
        static readonly string BlacksmithTag = "BlackSmithST:\t##008000|";
        static readonly string BlacksmithTag2 = "BlacksmithST:\t##800000|";
        static readonly string GuylianTag = "Guylian:\t##336633|";
        static readonly string endLineTag = "##";

        static void Main(string[] args)
        {
            ProcessChatFile("ChatFile.txt");
        }

        private static void ProcessChatFile(string fileName)
        {
            List<string> chatFileLines = new List<string>();

            using (var fileStream = File.OpenRead(fileName))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.GetEncoding(1252), true, 1024))
                {
                    string line;

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        line = ProcessLine(line);
                        chatFileLines.Add(line);
                    }
                }
            }
            Console.Write(chatFileLines.ToString());
            WriteCleanedFile(chatFileLines);
        }

        private static void WriteCleanedFile(List<string> chatFileLines)
        {
            using (StreamWriter writer = new StreamWriter("CleanedChatFile.txt", false, Encoding.UTF8))
            {
                foreach (string line in chatFileLines)
                {
                    writer.WriteLine(line);
                }
            }
        }

        private static string ProcessLine(string line)
        {
            if ((line.Contains("\t((") && (line.EndsWith("))##") || line.EndsWith(")) ## "))) || line.Contains("\t[[") && (line.EndsWith("]]##") || line.EndsWith("]] ## "))) 
            {
                line = string.Empty;
            }
            else
            {
                line = ReservedCharacterChangePass(line);
                line = line.Replace(StTag, "<p style='color:#666666;'><span style='font-weight: bold; color:#000000;'>NivST:\t</span>");
                line = line.Replace(YaraTag, "<p style='color:#666666;'><span style='font-weight: bold; color:#000000;'>YaraSherred:\t</span>");
                line = line.Replace(KaiTag, "<p style='color:#110481;'><span style='font-weight: bold; color:#000000; font-family: Lucida Console, Monaco, monospace; letter - spacing: 0.07em;'>KaiTukov:\t</span>");
                line = line.Replace(DamianTag, "<p style='color:#800000;'><span style='font-weight: bold; color:#000000;'>DamianStark:\t</span>");
                //line = line.Replace(DamianTag, "<p style='color:#208020;'><span style='font-weight: bold; color:#000000;'>DamianStark:\t</span>");
                line = line.Replace(GuylianTag, "<p style='color:#336633;'><span style='font-weight: bold; color:#000000;'>Guylian:\t</span>");
                line = line.Replace(BlacksmithTag, "<p style='color:#800000;'><span style='font-weight: bold; color:#000000;'>BlackSmith:\t</span>");
                line = line.Replace(BlacksmithTag2, "<p style='color:#800000;'><span style='font-weight: bold; color:#000000;'>BlackSmith:\t</span>");
                line = line.Replace(endLineTag, "</p>");
            }
            return line;
        }

        private static string ReservedCharacterChangePass(string changedHTMLLine)
        {
            string tempLine = changedHTMLLine.Replace('*', '✳');
            tempLine = tempLine.Replace("\"", "“");
            return tempLine.Replace("~", "〰️");
        }
    }
}
