//-----------------------------------------------------------------------
// <copyright file="Voxel.cs" company="Rainfall">
// Copyright (c) Rainfall. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------

using System.ComponentModel;

namespace WorldUtils
{
    public enum CrystalType
    {
        [Description("None")]
        None,
        [Description("Stone")]
        Stone,
        [Description("Iron")]
        Iron,
        [Description("Ice")]
        Ice,
        [Description("Gold")]
        Gold,
        [Description("Coal")]
        Coal,
        [Description("Copper")]
        Copper,
        [Description("Uranium")]
        Uranium,
        [Description("Nickel")]
        Nickel,
        [Description("Lead")]
        Lead,
        [Description("Silver")]
        Silver,
        [Description("Silicon")]
        Silicon,
        [Description("Oxite")]
        Oxite,
        [Description("Volatiles")]
        Volatiles,
        [Description("Crater")]
        Crater = 254,
        [Description("Bedrock")]
        Bedrock
    }

    public static class EnumHelper
    {
        public static T GetAttributeOfType<T>(this CrystalType enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
    }
}
