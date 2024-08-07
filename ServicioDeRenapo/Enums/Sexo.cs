//-----------------------------------------------------------------------
// <copyright file="Sexo.cs" company="">
//     Copyright (c). All rights reserved.
// </copyright>
// <author>Roberto Franco</author>
//-----------------------------------------------------------------------

using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace Renapo.Enums
{
    /// <summary>
    ///     El sexo enum.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Sexo
    {
        Hombre = 'H',

        Mujer = 'M'
    }
}
