using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FarForFolderAndFiles
{
    class CustomFolderInfo
    {
        public CustomFolderInfo prev;
        public FileSystemInfo[] objs;

        public CustomFolderInfo(CustomFolderInfo prev, FileSystemInfo[] list)
        {
            this.prev = prev;
            this.objs = list;
        }

        public CustomFolderInfo GetNextItem(int k)
        {
            FileSystemInfo active = objs[k];
            List<FileSystemInfo> list = new List<FileSystemInfo>();
            DirectoryInfo d = active as DirectoryInfo;
            list.AddRange(d.GetDirectories());
            list.AddRange(d.GetFiles());
            CustomFolderInfo x = new CustomFolderInfo(this, list.ToArray());
            return x;
        }


    }
    class Program
    {

        static void Zapis(CustomFolderInfo item)
        {
            for (int i = 0; i < item.objs.Length; ++i)
            {
                if (item.objs[i].GetType() == typeof(FileInfo))
                {
                    if (item.objs[i].Extension == ".txt")
                    {

                        FileInfo d = (FileInfo)item.objs[i];
                      ///  string mydocpath = @"C:\Users\Lenovo\Desktop\Новая папка";
                        string line;
                        string s = d.FullName;

                        string res = Path.GetFileName(s);
                        StreamReader file = new StreamReader(s);
                        while ((line = file.ReadLine()) != null)
                        {

                         ///   using (StreamWriter outputFile = new StreamWriter((mydocpath + @"\lol.txt"), true))
                         ///   {

                                Tuple<string, string> pair = new Tuple<string, string>(res,line);
                                Global.tuples.Add(pair);

                                 ///   outputFile.WriteLine(line);
                               
                           /// }
                        }
                    }
                }

                else
                {
                    CustomFolderInfo newItem = item.GetNextItem(i);
                    Zapis(newItem);
                }
            }
        }


        static void Main(string[] args)
        {
           
     
            Console.WriteLine("Please enter the word you need to find");
            string k = Console.ReadLine();
            Console.ForegroundColor= ConsoleColor.Blue;
            List<FileSystemInfo> list = new List<FileSystemInfo>();
            var d = new DirectoryInfo(@"C:\Users\Lenovo\Desktop\pois");
            list.AddRange(d.GetDirectories());
            list.AddRange(d.GetFiles());
            int num = 0;
            CustomFolderInfo test = new CustomFolderInfo(null, list.ToArray());
            Zapis(test);
            Console.WriteLine("The word has been found in:");
            for (int i = 0; i < Global.tuples.Count; i++)
            {
                string[] ss = Global.tuples[i].Item2.Split(' ');
                for (int j = 0; j < ss.Length; j++)
                {
                    if (ss[j] == k) {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine(Global.tuples[i].Item1);
                    num++;}
                }
             
            }
            if (num == 0) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("File with this word doesn't exist"); }
        }
    }
}

