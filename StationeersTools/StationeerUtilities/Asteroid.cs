//-----------------------------------------------------------------------
// <copyright file="Asteroid.cs" company="Rainfall">
// Copyright (c) Rainfall. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;

namespace StationeerUtilities
{
    public class Asteroid
    {
        public Voxel[,,] voxelArray;

        public Vector3 Position;

        int chunkSize;

        public Asteroid(int chunkSize)
        {
            this.chunkSize = chunkSize;

            this.voxelArray = new Voxel[chunkSize, chunkSize, chunkSize];
        }

        public Vector3 Index2Vector(int index)
        {
            int num = this.chunkSize * this.chunkSize;
            Vector3 indexVector = default(Vector3);
            indexVector.z = (float)(index / num);
            index -= (int)(indexVector.z * (float)num);
            indexVector.y = (float)(index / this.chunkSize);
            indexVector.x = (float)(index % this.chunkSize);
            return indexVector;
        }

        public void DeserializeBytes(BinaryReader reader)
        {
            float x = reader.ReadSingle();
            float y = reader.ReadSingle();
            float z = reader.ReadSingle();
            this.Position = new Vector3(x, y, z);

            int num = reader.ReadInt32();
            for (int i = 0; i < num; i++)
            {
                short index = reader.ReadInt16();
                Vector3 vector = this.Index2Vector((int)index);
                byte b = reader.ReadByte();
                byte density = reader.ReadByte();
                this.voxelArray[(int)vector.x, (int)vector.y, (int)vector.z] = new Voxel(b, density);
            }
        }

        public void SerializeBytes(BinaryWriter writer)
        {
            writer.Write(this.Position.x);
            writer.Write(this.Position.y);
            writer.Write(this.Position.z);
            int num = 0;
            List<byte> list = new List<byte>();
            for (int x = 0; x < this.chunkSize; x++)
            {
                for (int y = 0; y < this.chunkSize; y++)
                {
                    for (int z = 0; z < this.chunkSize; z++)
                    {
                        Voxel voxel = this.voxelArray[x, y, z];
                        if (voxel == null) continue;
                        if (voxel.Type != 0)
                        {
                            list.AddRange(BitConverter.GetBytes(Utilities.GetIndex(x, y, z, chunkSize)));
                            list.Add(voxel.Type);
                            list.Add(voxel.Density);
                            num++;
                        }
                    }
                }
            }

            writer.Write(num);
            writer.Write(list.ToArray());
        }
    }
}
