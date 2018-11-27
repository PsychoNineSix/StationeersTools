//-----------------------------------------------------------------------
// <copyright file="Vector3.cs" company="Rainfall">
// Copyright (c) Rainfall. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace WorldUtils
{
    public struct Vector3
    {
        // Token: 0x040000D0 RID: 208
        public float x;

        // Token: 0x040000D1 RID: 209
        public float y;

        // Token: 0x040000D2 RID: 210
        public float z;

        // Token: 0x06000898 RID: 2200 RVA: 0x0000A328 File Offset: 0x00008528
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        // Token: 0x06000899 RID: 2201 RVA: 0x0000A340 File Offset: 0x00008540
        public Vector3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0f;
        }

        public bool WithinX(float min, float max)
        {
            return this.x >= min && this.x <= max;
        }

        public bool WithinY(float min, float max)
        {
            return this.y >= min && this.y <= max;
        }

        public bool WithinZ(float min, float max)
        {
            return this.z >= min && this.z <= max;
        }

        public double GetDistanceTo(Vector3 target)
        {
            return Math.Sqrt(Math.Pow(x - target.x, 2) + Math.Pow(z - target.z, 2));
        }
    }
}
