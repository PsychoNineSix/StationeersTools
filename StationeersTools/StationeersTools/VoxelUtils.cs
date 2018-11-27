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

        public static void CountTypes (List<Voxel> voxels)
        {
            for (int i = 0; i < voxels.Count; i++)
            {
                if (voxels[i] != null)
                {
                    addCounter(voxels[i].Type);
                }
            }
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
                                selectedvoxels.Add(vox, voxpos);
                            }
                        }
                    }
                }
            }

            return selectedvoxels;
        }

        public static List<Voxel> SelectVoxels (Saves save, Vector3 min, Vector3 max)
        {
            List<Voxel> selectedvoxels = new List<Voxel>();
            List<Asteroid> chunks = save.GetChunks();

            Dictionary<Voxel, Vector3> preselected = PreselectVoxels(chunks);

            foreach(var item in preselected)
            {
                Voxel vox = item.Key;
                Vector3 voxpos = item.Value;

                if (voxpos.WithinX(min.x, max.x) && voxpos.WithinY(min.y, max.y) && voxpos.WithinZ(min.z, max.z))
                {
                    selectedvoxels.Add(vox);
                }
            }

            return selectedvoxels;       
        }

        public static List<Voxel> SelectVoxels(Saves save, Vector3 center, float radius, float ylevel)
        {
            List<Voxel> selectedvoxels = new List<Voxel>();
            List<Asteroid> chunks = save.GetChunks();

            Dictionary<Voxel, Vector3> preselected = PreselectVoxels(chunks);

            foreach (var item in preselected)
            {
                Voxel vox = item.Key;
                Vector3 voxpos = item.Value;

                if (voxpos.GetDistanceTo(center) < radius && voxpos.y > ylevel)
                {
                    selectedvoxels.Add(vox);
                }
            }

            return selectedvoxels;
        }

        public static List<Voxel> SelectVoxels(Saves save, Vector3 center, float radius, float ymin, float ymax)
        {
            List<Voxel> selectedvoxels = new List<Voxel>();
            List<Asteroid> chunks = save.GetChunks();

            Dictionary<Voxel, Vector3> preselected = PreselectVoxels(chunks);

            foreach (var item in preselected)
            {
                Voxel vox = item.Key;
                Vector3 voxpos = item.Value;

                if (voxpos.GetDistanceTo(center) < radius && voxpos.y > ymin && voxpos.y < ymax)
                {
                    selectedvoxels.Add(vox);
                }
            }

            return selectedvoxels;
        }
    }
}
