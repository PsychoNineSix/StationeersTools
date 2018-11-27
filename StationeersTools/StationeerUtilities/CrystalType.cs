//-----------------------------------------------------------------------
// <copyright file="CrystalType.cs" company="Rainfall">
// Copyright (c) Rainfall. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
//-----------------------------------------------------------------------
using System.ComponentModel;

namespace StationeerUtilities
{
    public enum CrystalType
    {
        [Description("None")]
        None,
        Stone,
        Iron,
        Ice,
        Gold,
        Coal,
        Copper,
        Uranium,
        Nickel,
        Lead,
        Silver,
        Silicon,
        Oxite,
        Volatiles,
        Crater = 254,
        Bedrock
    }
}
