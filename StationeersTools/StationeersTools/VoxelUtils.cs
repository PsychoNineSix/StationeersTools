using System;
using System.Collections.Generic;
using System.Text;
using WorldUtils;

namespace StationeersTools
{
    public static class VoxelUtils
    {
        static Dictionary<int, int> counter = new Dictionary<int, int>();
        static int totalcounter = 0;
        public static int ChunkSize = 7;

        static void addCounter(int type)
        {
            counter[type]++;
            totalcounter++;
        }

        public static Dictionary<int, int> CountTypes (List<Voxel> voxels)
        {
            for (int i = 0; i <= 13; i++)
            {
                counter.Add(i, 0);
            }

            counter.Add(254, 0);
            counter.Add(255, 0);

            for (int i = 0; i < voxels.Count; i++)
            {
                if (voxels[i] != null)
                {
                    addCounter(voxels[i].Type);
                }
            }

            return counter;
        }

        public static void ClearVoxelList (List<Voxel> voxels)
        {
            for (int i = 0; i < voxels.Count; i++)
            {
                ClearVoxel(voxels[i]);
            }
        }

        public static bool ClearVoxel (Voxel voxel)
        {
            if(voxel != null)
            {
                voxel.Type = 1;
                voxel.Density = 0;
                return true;
            }
            return false;
        }

        public static void SetVoxelList(List<Voxel> voxels, byte type, byte density)
        {
            for (int i = 0; i < voxels.Count; i++)
            {
                SetVoxel(voxels[i], type, density);
            }
        }


        public static bool SetVoxel (Voxel voxel, byte type, byte density)
        {
            if (voxel != null)
            {
                voxel.Type = type;
                voxel.Density = density;
                return true;
            }
            return false;
        }

        private static Dictionary<Voxel, Vector3> PreselectVoxels (List<Asteroid> chunks)
        {
            Dictionary<Voxel, Vector3> selectedvoxels = new Dictionary<Voxel, Vector3>();

            for (int chunky = 0; chunky < chunks.Count; chunky++)
            {
                Asteroid chunk = chunks[chunky];
                Vector3 chunkpos = chunk.Position;

                for (int i = 0; i < ChunkSize; i++)
                {
                    for (int ii = 0; ii < ChunkSize; ii++)
                    {
                        for (int iii = 0; iii < ChunkSize; iii++)
                        {
                            Voxel vox = chunk.voxelArray[i, ii, iii];
                            Vector3 voxpos = new Vector3((float)Math.Floor(chunkpos.x) + i, (float)Math.Floor(chunkpos.y) + ii, (float)Math.Floor(chunkpos.z) + iii);

                            if (vox != null)
                            {
                                if(vox.Type != 0 && vox.Type != 254 && vox.Type != 255 && vox.Density != 0)
                                {
                                    selectedvoxels.Add(vox, voxpos);
                                }
                            }
                        }
                    }
                }

                Console.SetCursorPosition(0, 1);
                Console.Write("Found: {0} Voxels", selectedvoxels.Count);
            }

            return selectedvoxels;
        }

        public static Tuple<List<Voxel>, List<Voxel>> SelectVoxels (Saves save, Vector3 min, Vector3 max)
        {
            List<Voxel> selectedvoxels = new List<Voxel>();
            List<Voxel> floorvoxels = new List<Voxel>();

            List<Asteroid> chunks = save.GetChunks();
            List<Asteroid> filteredchunks = new List<Asteroid>();

            foreach (Asteroid chunk in chunks)
            {
                Vector3 pos = chunk.Position;
                if((pos.WithinX(min.x - 10, max.x + 10)) && (pos.WithinZ(min.z - 10, max.z + 10)))
                {
                    filteredchunks.Add(chunk);
                }
            }

            Dictionary<Voxel, Vector3> preselected = PreselectVoxels(filteredchunks);

            foreach(var item in preselected)
            {
                Voxel vox = item.Key;
                Vector3 voxpos = item.Value;

                if (voxpos.WithinX(min.x, max.x) && voxpos.WithinY(min.y, max.y) && voxpos.WithinZ(min.z, max.z))
                {
                    selectedvoxels.Add(vox);
                }
                else if (voxpos.WithinX(min.x, max.x) && voxpos.y == min.y - 1 && voxpos.WithinZ(min.z, max.z))
                {
                    floorvoxels.Add(vox);
                }

                Console.SetCursorPosition(0, 2);
                Console.Write("Found: {0} Voxels", selectedvoxels.Count);
            }

            return Tuple.Create(selectedvoxels, floorvoxels);       
        }

        public static Tuple<List<Voxel>, List<Voxel>> SelectVoxels(Saves save, Vector3 center, float radius)
        {
            List<Voxel> selectedvoxels = new List<Voxel>();
            List<Voxel> floorvoxels = new List<Voxel>();

            List<Asteroid> chunks = save.GetChunks();
            List<Asteroid> filteredchunks = new List<Asteroid>();

            foreach (Asteroid chunk in chunks)
            {
                Vector3 pos = chunk.Position;
                if ((pos.WithinX(center.x - radius, center.x + radius)) && (pos.WithinZ(center.z - radius, center.z + radius)))
                {
                    filteredchunks.Add(chunk);
                }
            }

            Dictionary<Voxel, Vector3> preselected = PreselectVoxels(filteredchunks);

            foreach (var item in preselected)
            {
                Voxel vox = item.Key;
                Vector3 voxpos = item.Value;

                if (voxpos.GetDistanceTo(center) <= radius && voxpos.y >= center.y)
                {
                    selectedvoxels.Add(vox);
                }
                else if (voxpos.GetDistanceTo(center) <= radius && voxpos.y == center.y - 1)
                {
                    floorvoxels.Add(vox);
                }

                Console.SetCursorPosition(0, 2);
                Console.Write("Found: {0} Voxels", selectedvoxels.Count);
            }

            return Tuple.Create(selectedvoxels, floorvoxels);
        }

        public static Tuple<List<Voxel>, List<Voxel>> SelectVoxels(Saves save, Vector3 center, float radius, float ymin, float ymax)
        {
            List<Voxel> selectedvoxels = new List<Voxel>();
            List<Voxel> floorvoxels = new List<Voxel>();

            List<Asteroid> chunks = save.GetChunks();
            List<Asteroid> filteredchunks = new List<Asteroid>();

            foreach (Asteroid chunk in chunks)
            {
                Vector3 pos = chunk.Position;
                if ((pos.WithinX(center.x - radius - ChunkSize, center.x + radius + ChunkSize)) && (pos.WithinZ(center.z - radius - ChunkSize, center.z + radius + ChunkSize)))
                {
                    filteredchunks.Add(chunk);
                }
            }

            Dictionary<Voxel, Vector3> preselected = PreselectVoxels(filteredchunks);

            foreach (var item in preselected)
            {
                Voxel vox = item.Key;
                Vector3 voxpos = item.Value;

                if (voxpos.GetDistanceTo(center) <= radius && voxpos.y >= ymin && voxpos.y <= ymax)
                {
                    selectedvoxels.Add(vox);
                }
                else if (voxpos.GetDistanceTo(center) <= radius && voxpos.y == ymin - 1)
                {
                    floorvoxels.Add(vox);
                }

                Console.SetCursorPosition(0, 2);
                Console.Write("Found: {0} Voxels", selectedvoxels.Count);
            }

            return Tuple.Create(selectedvoxels, floorvoxels);
        }
    }
}
