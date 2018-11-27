//-----------------------------------------------------------------------
// <copyright file="Utilities.cs" company="Rainfall">
// Copyright (c) Rainfall. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StationeerUtilities
{
    public static class Utilities
    {
        private static short[,,] indexArray;

        public static short GetIndex(int x, int y, int z, int chunkSize)
        {
            if (indexArray == null)
            {
                indexArray = new Int16[chunkSize, chunkSize, chunkSize];
                for (Int16 index = 0; index < Math.Pow(chunkSize, 3); index++)
                {
                    Vector3 position = Index2Vector(index, chunkSize);
                    indexArray[(int)position.x, (int)position.y, (int)position.z] = index;
                }
            }

            return indexArray[x, y, z];
        }

        public static Vector3 Index2Vector(int index, int chunkSize)
        {
            int num = chunkSize * chunkSize;
            Vector3 indexVector = default(Vector3);
            indexVector.z = (float)(index / num);
            index -= (int)(indexVector.z * (float)num);
            indexVector.y = (float)(index / chunkSize);
            indexVector.x = (float)(index % chunkSize);
            return indexVector;
        }

    }
}
