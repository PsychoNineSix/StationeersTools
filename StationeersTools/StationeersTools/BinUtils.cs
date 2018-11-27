using System;
using System.Collections.Generic;
using System.Text;
using WorldUtils;

namespace StationeersTools
{
    public static class BinUtils
    {
        public static List<Voxel> ActiveVoxels;
        public static List<Voxel> FloorVoxels;

        public static void CreateUndergroundSpace()
        {
            Menus.ShowVoxelSelectMenu(true);

            VoxelUtils.ClearVoxelList(ActiveVoxels);
            Console.Write("Cleared voxels: ");
            PrintUtils.PrintLine(ActiveVoxels.Count.ToString(), ConsoleColor.DarkYellow);

            VoxelUtils.SetVoxelList(FloorVoxels, 1, 255);

            Console.WriteLine("Saving to file...");
            Program.binfile.Save(Program.raw_binfile.FullName);
            Console.Write("Saved! Press any key to continue.");
            Console.Read();
        }

        public static void FlattenSurface()
        {
            Menus.ShowVoxelSelectMenu(false);

            VoxelUtils.ClearVoxelList(ActiveVoxels);
            Console.Write("Cleared voxels: ");
            PrintUtils.PrintLine(ActiveVoxels.Count.ToString(), ConsoleColor.DarkYellow);

            VoxelUtils.SetVoxelList(FloorVoxels, 1, 255);

            Console.WriteLine("Saving to file...");
            Program.binfile.Save(Program.raw_binfile.FullName);
            Console.Write("Saved! Press any key to continue.");
            Console.Read();
        }

        public static void StartSelect(int type, bool heightrequired)
        {
            if (type == 1)
            {
                Console.Clear();

                Vector3 point = PrintUtils.RequestVector("Enter coördinates of the center point");
                float radius = PrintUtils.RequestFloat("Enter radius (length of center to edge)");

                Console.Clear();
                Console.WriteLine("Reading voxels");

                if (heightrequired)
                {
                    float height = PrintUtils.RequestFloat("Please input height");
                    var res = VoxelUtils.SelectVoxels(Program.binfile, point, radius, point.y, point.y + height);
                    ActiveVoxels = res.Item1;
                    FloorVoxels = res.Item2;
                }
                else
                {
                    var res = VoxelUtils.SelectVoxels(Program.binfile, point, radius);
                    ActiveVoxels = res.Item1;
                    FloorVoxels = res.Item2;
                }
            }
            else if (type == 2)
            {
                Console.Clear();

                Vector3 point = PrintUtils.RequestVector("Enter coördinates of the center point");
                float range = PrintUtils.RequestFloat("Enter range (length of center to edges)");

                Vector3 min = VectorUtils.GetMinVector(point, range);
                Vector3 max = VectorUtils.GetMaxVector(point, range);
                max.y = 150;

                if (heightrequired)
                {
                    float height = PrintUtils.RequestFloat("Please input height");
                    max.y = min.y + height;
                    var res = VoxelUtils.SelectVoxels(Program.binfile, min, max);
                    ActiveVoxels = res.Item1;
                    FloorVoxels = res.Item2;
                }
                else
                {
                    var res = VoxelUtils.SelectVoxels(Program.binfile, min, max);
                    ActiveVoxels = res.Item1;
                    FloorVoxels = res.Item2;
                }

                Console.Clear();
                Console.WriteLine("Reading voxels");
            }
            else if (type == 3)
            {
                Vector3 startpoint = PrintUtils.RequestVector("Enter coördinates of the start point");
                Vector3 endpoint = PrintUtils.RequestVector("Enter coördinates of the end point");

                Console.Clear();
                Console.WriteLine("Reading voxels");
                var res = VoxelUtils.SelectVoxels(Program.binfile, startpoint, endpoint);
                ActiveVoxels = res.Item1;
                FloorVoxels = res.Item2;
            }
            else
            {
                Menus.ShowMenus();                
            }

            Console.SetCursorPosition(0, 4);
            Console.WriteLine("All applicable voxels found!");
        }
    }
}
