﻿//-----------------------------------------------------------------------
// <copyright file="Voxel.cs" company="Rainfall">
// Copyright (c) Rainfall. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace WorldUtils
{
    public class Saves
    {
        private List<Asteroid> chunks;

        private int chunkSize;

        private Vector3 lowerBounds;
        private Vector3 upperBounds;

        public int ChunkSize
        {
            get { return this.chunkSize; }
        }

        public bool LoadSave(string path)
        {
            var fs = new FileStream(path, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);

            int chunkCount;

            var expectedUncompressedLength = br.ReadInt32();

            byte[] array_compressed = null;
            using (MemoryStream ms = new MemoryStream())
            {
                fs.CopyTo(ms);
                array_compressed = ms.ToArray();
            }

            br.Close();

            byte[] array_uncompressed = LZ4.LZ4Codec.Unwrap(array_compressed, 0);
            if (array_uncompressed.Length != expectedUncompressedLength)
            {
                throw new Exception("The decompressed save is not the same length as expected");
            }

            this.chunks = new List<Asteroid>();

            using (MemoryStream ms = new MemoryStream(array_uncompressed))
            {
                br = new BinaryReader(ms);

                chunkCount = br.ReadInt32();
                this.chunkSize = br.ReadInt32();

                do
                {
                    var a = new Asteroid(this.chunkSize);
                    a.DeserializeBytes(br);
                    this.chunks.Add(a);
                } while (ms.Position < ms.Length);

                if (this.chunks.Count != chunkCount)
                {
                    throw new Exception("The number of chunks found does not match what was expected");
                }

                return true;
            }
        }

        public void Save(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);
            bw.Write(this.chunks.Count);
            bw.Write(this.chunkSize);
            foreach (Asteroid a in this.chunks)
            {
                a.SerializeBytes(bw);
            }
            byte[] uncompressed = ms.ToArray();
            byte[] compressed = LZ4.LZ4Codec.Wrap(uncompressed, 0, 2147483647);

            bw.Close();
            bw = new BinaryWriter(fs);
            fs.Write(BitConverter.GetBytes(uncompressed.Length), 0, 4);
            fs.Write(compressed, 0, compressed.Length);

            fs.Close();
        }

        public Vector3 GetDimensions()
        {
            this.lowerBounds = default(Vector3);
            this.upperBounds = default(Vector3);
            foreach (Asteroid chunk in this.chunks)
            {
                if (chunk.Position.x < this.lowerBounds.x) this.lowerBounds.x = chunk.Position.x;
                if (chunk.Position.y < this.lowerBounds.y) this.lowerBounds.y = chunk.Position.y;
                if (chunk.Position.z < this.lowerBounds.z) this.lowerBounds.z = chunk.Position.z;

                if (chunk.Position.x > this.upperBounds.x) this.upperBounds.x = chunk.Position.x;
                if (chunk.Position.y > this.upperBounds.y) this.upperBounds.y = chunk.Position.y;
                if (chunk.Position.z > this.upperBounds.z) this.upperBounds.z = chunk.Position.z;
            }
            return new Vector3(this.upperBounds.x - this.lowerBounds.x, this.upperBounds.y - this.lowerBounds.y, this.upperBounds.z - this.lowerBounds.z);
        }

        public Vector3[] GetBounds()
        {
            return new Vector3[] { this.lowerBounds, this.upperBounds };
        }

        public void TrimRadius(int radius, int centerX, int centerZ)
        {
            double r2 = Math.Pow(radius, 2);
            List<Asteroid> chunksToRemove = new List<Asteroid>();

            foreach (Asteroid chunk in this.GetChunks())
            {
                int X = (int)Math.Floor(chunk.Position.x);
                int Z = (int)Math.Floor(chunk.Position.z);

                int distX = Math.Abs(centerX - X);
                int distZ = Math.Abs(centerZ - Z);

                double distance = Math.Pow(distX, 2) + Math.Pow(distZ, 2);
                if (distance > r2)
                {
                    chunksToRemove.Add(chunk);
                }
            }

            foreach (Asteroid rChunk in chunksToRemove)
            {
                this.chunks.Remove(rChunk);
            }
        }

        public List<Asteroid> GetChunks()
        {
            return this.chunks;
        }
    }
}
