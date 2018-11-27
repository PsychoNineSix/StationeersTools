using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Text;

namespace StationeersTools
{
    public static class Menus
    {
        public static void ShowMenus()
        {
            var voxelMenu = new ConsoleMenu()
                    //.Add("Trim world", () => trimMenu.Show())
                    .Add("Flatten surface", () => BinUtils.FlattenSurface())
                    .Add("Create underground space", () => BinUtils.CreateUndergroundSpace())
                    .Add("Back to main menu", ConsoleMenu.Close)
                    .Configure(config => {
                        config.WriteHeaderAction = () => Console.WriteLine("What do you want to do?");
                        config.SelectedItemBackgroundColor = ConsoleColor.DarkYellow;
                    });

            var xmlMenu = new ConsoleMenu()
                .Add("Battery utilities", () => Console.Write(""))
                //.Add("Character utilities", () => Console.Write(""))
                //.Add("Tank utilities", () => Console.Write(""))
                .Add("Item utilities", () => Console.Write(""))
                .Add("Back to main menu", ConsoleMenu.Close)
                .Configure(config => {
                    config.WriteHeaderAction = () => Console.WriteLine("What do you want to do?");
                    config.SelectedItemBackgroundColor = ConsoleColor.DarkYellow;
                });

            var mainMenu = new ConsoleMenu()
                .Add("Voxel world file (world.bin)", () => voxelMenu.Show())
                //.Add("Things file (world.xml)", () => xmlMenu.Show())                    
                .Add("Reload Savefiles", () => Program.LoadFiles())
                .Add("Exit", () => Environment.Exit(0))
                .Configure(config => {
                    config.WriteHeaderAction = () => Console.WriteLine("Where do you want to go?");
                    config.SelectedItemBackgroundColor = ConsoleColor.DarkYellow;
                    config.Selector = "Space! > ";
                });

            mainMenu.Show();
        }

        public static void ShowVoxelSelectMenu(bool heightrequired)
        {
            Console.Clear();
            Console.WriteLine("How would you like to pick the affected area?");

            string[][] options =
            {
                PrintUtils.BuildRow("1", "Circle around point"),
                PrintUtils.BuildRow("2", "Square around point"),
                PrintUtils.BuildRow("3", "Point to point")
            };

            PrintUtils.PrintColumns(options, 1);
            Console.WriteLine();

            int selected = 100;

            while (selected == 100)
            {
                string input = PrintUtils.RequestInputLine("Option");
                int.TryParse(input, out selected);
            }

            BinUtils.StartSelect(selected, heightrequired);
        }
    }
}
