using System;
using System.Collections.Generic;
using WorldUtils;
using System.Text;

namespace StationeersTools
{
    public static class VectorUtils
    {
        public static Vector3 GetMinVector(Vector3 center, float range)
        {
            return new Vector3(center.x - range, center.y, center.z - range);
        }

        public static Vector3 GetMaxVector(Vector3 center, float range)
        {
            return new Vector3(center.x + range, center.y, center.z + range);
        }
    }
}
