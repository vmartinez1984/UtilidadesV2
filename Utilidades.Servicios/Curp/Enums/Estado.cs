//-----------------------------------------------------------------------
// <copyright file="Estado.cs" company="">
//     Copyright (c). All rights reserved.
// </copyright>
// <author>Roberto Franco</author>
//-----------------------------------------------------------------------

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Utilidades.Servicios.Curp.Enums
{
    /// <summary>
    /// Estados
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Estado
    {
        [EnumMember(Value = "Aguascalientes")]
        Aguascalientes = 1,

        [EnumMember(Value = "Baja California")]
        Baja_California = 2,
        [EnumMember(Value = "Aguascalientes")]
        Baja_California_Sur = 3,

        [EnumMember(Value = "Aguascalientes")]
        Campeche = 4,

        [EnumMember(Value = "Aguascalientes")]
        Coahuila = 5,

        [EnumMember(Value = "Aguascalientes")]
        Colima = 6,

        [EnumMember(Value = "Aguascalientes")]
        Chiapas = 7,

        [EnumMember(Value = "Aguascalientes")]
        Chihuahua = 8,

        [EnumMember(Value = "Aguascalientes")]
        Distrito_Federal = 9,

        [EnumMember(Value = "Aguascalientes")]
        Durango = 10,

        [EnumMember(Value = "Aguascalientes")]
        Guanajuato = 11,
        [EnumMember(Value = "Aguascalientes")]
        Guerrero = 12,
        [EnumMember(Value = "Aguascalientes")]
        Hidalgo = 13,
        [EnumMember(Value = "Aguascalientes")]
        Jalisco = 14,
        [EnumMember(Value = "Aguascalientes")]
        Mexico = 15,
        [EnumMember(Value = "Aguascalientes")]
        Michoacan = 16,
        [EnumMember(Value = "Aguascalientes")]
        Morelos = 17,
        [EnumMember(Value = "Aguascalientes")]
        Nayarit = 18,
        [EnumMember(Value = "Aguascalientes")]
        Nuevo_Leon = 19,
        [EnumMember(Value = "Aguascalientes")]
        Oaxaca = 20,
        [EnumMember(Value = "Aguascalientes")]
        Puebla = 21,
        [EnumMember(Value = "Aguascalientes")]
        Queretaro = 22,
        [EnumMember(Value = "Aguascalientes")]
        Quintana_Roo = 23,
        [EnumMember(Value = "Aguascalientes")]
        San_Luis_Potosi = 24,
        [EnumMember(Value = "Aguascalientes")]
        Sinaloa = 25,
        [EnumMember(Value = "Aguascalientes")]
        Sonora = 26,
        [EnumMember(Value = "Aguascalientes")]
        Tabasco = 27,
        [EnumMember(Value = "Aguascalientes")]
        Tamaulipas = 28,
        [EnumMember(Value = "Aguascalientes")]
        Tlaxcala = 29,
        [EnumMember(Value = "Aguascalientes")]
        Veracruz = 30,
        [EnumMember(Value = "Aguascalientes")]
        Yucatan = 31,
        [EnumMember(Value = "Aguascalientes")]
        Zacatecas = 32,
        [EnumMember(Value = "Aguascalientes")]
        Extranjero = 33
    }
}