//-----------------------------------------------------------------------
// <copyright file="Voxel.cs" company="Rainfall">
// Copyright (c) Rainfall. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
namespace StationeerUtilities
{
    public class Voxel
    {
        public Voxel(byte type, byte density)
        {
            this.Type = type;
            this.Density = density;
        }

        public byte Type { get; set; }

        public byte Density { get; set; }
    }
}
