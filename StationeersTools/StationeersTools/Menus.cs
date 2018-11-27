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
            var trimMenu = new ConsoleMenu()
                .Add("Circle around player", () => Console.WriteLine("Sub_One"))
                .Add("Circle around point", () => Console.WriteLine("Sub_One"))
                .Add("Back to previous menu", ConsoleMenu.Close)
                .Configure(config => {
                    config.WriteHeaderAction = () => Console.WriteLine("What do you want to do?");
                    config.SelectedItemBackgroundColor = ConsoleColor.DarkYellow;
                });

            var voxelMenu = new ConsoleMenu()
                    .Add("Trim world", () => trimMenu.Show())
                    .Add("Flatten surface", () => Console.WriteLine("Sub_Two"))
                    .Add("Create underground space", () => Console.WriteLine("Sub_Three"))
                    .Add("Check ore count", () => Console.WriteLine("Sub_Four"))
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
    }
}
