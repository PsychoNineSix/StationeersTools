using ConsoleTools;
using System;
using System.IO;
using System.Xml;
using WorldUtils;

namespace StationeersTools
{
    class Program
    {
        private static bool keepRunning = true;

        public static Saves binfile;
        public static XmlDocument xmlfile;
        public static FileInfo raw_binfile;
        public static FileInfo raw_xmlfile;

        static void Main (string[] args)
        {
            Console.Clear();
            PrintUtils.PrepareConsole();
            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e) {
                e.Cancel = true;
                keepRunning = false;
            };

            Console.WriteLine("Welcome to StationeersTools 2019 - Made by The Psycho");
            Console.WriteLine("You can always press ctrl+c to shut down, this will cancel unsaved changes.");
            Console.WriteLine("Changes will always be written to the original file");
            PrintUtils.PrintLine("Your original file will be backed up, but PLEASE make your own!", ConsoleColor.Red);
            PrintUtils.PrintLine("Although very unlikely, I am not responsible for any damage after game updates.", ConsoleColor.Red);

            if (args.Length > 0)
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(args[0]);
                    checkDir(dir);
                }
                catch (Exception ex)
                {
                    PrintUtils.PrintLine("Incorrect path!", ConsoleColor.Red);
                }
            }
            else
            {
                PrintUtils.PrintLine("Please input the full path to the save folder :)", ConsoleColor.White);

                while(raw_binfile == null || raw_xmlfile == null)
                {
                    try
                    {
                        string dirpath = PrintUtils.RequestInputLine("Directory");

                        DirectoryInfo dir = new DirectoryInfo(dirpath);
                        checkDir(dir);
                    }
                    catch (Exception ex)
                    {
                        PrintUtils.PrintLine("Incorrect path!", ConsoleColor.Red);
                    }
                }
            }

            string datetime = DateTime.Now.ToString("yyyyMMddHmmss");

            raw_binfile.CopyTo(raw_binfile.FullName + "." + datetime + ".original");
            raw_xmlfile.CopyTo(raw_xmlfile.FullName + "." + datetime + ".original");

            LoadFiles();

            while (keepRunning)
            {
                Menus.ShowMenus();
    
                Console.Clear();
                Console.WriteLine("test");
                Console.ReadKey();
            }

            Console.WriteLine("Graceful Exit");
        }

        public static void LoadFiles()
        {
            binfile = new Saves();
            binfile.LoadSave(raw_binfile.FullName);
            VoxelUtils.ChunkSize = binfile.ChunkSize;

            xmlfile = new XmlDocument();
            xmlfile.Load(raw_xmlfile.FullName);
        }

        private static void checkDir (DirectoryInfo dir)
        {
            if (dir.Exists)
            {
                Console.Title = "StationeersTools - " + dir.Name;

                foreach (FileInfo file in dir.GetFiles())
                {
                    if (file.Name == "world.bin") { raw_binfile = file; }
                    if (file.Name == "world.xml") { raw_xmlfile = file; }
                }

                if (raw_binfile == null || raw_xmlfile == null)
                {
                    PrintUtils.PrintLine("Could not find both world files!", ConsoleColor.Red);
                }
            }
            else
            {
                PrintUtils.PrintLine("Incorrect path!", ConsoleColor.Red);
            }
        }
    }
}
