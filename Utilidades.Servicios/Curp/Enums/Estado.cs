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

        [EnumMember(Value = "Baja California Sur")]
        Baja_California_Sur = 3,

        [EnumMember(Value = "Campeche")]
        Campeche = 4,

        [EnumMember(Value = "Coahuila")]
        Coahuila = 5,

        [EnumMember(Value = "Colima")]
        Colima = 6,

        [EnumMember(Value = "Chiapas")]
        Chiapas = 7,

        [EnumMember(Value = "Chihuahua")]
        Chihuahua = 8,

        [EnumMember(Value = "Ciudad de México")]
        Distrito_Federal = 9,

        [EnumMember(Value = "Durango")]
        Durango = 10,

        [EnumMember(Value = "Guanajuato")]
        Guanajuato = 11,

        [EnumMember(Value = "Guerrero")]
        Guerrero = 12,

        [EnumMember(Value = "Hidalgo")]
        Hidalgo = 13,

        [EnumMember(Value = "Jalisco")]
        Jalisco = 14,

        [EnumMember(Value = "Mexico")]
        Mexico = 15,

        [EnumMember(Value = "Michoacan")]
        Michoacan = 16,
        [EnumMember(Value = "Morelos")]
        Morelos = 17,

        [EnumMember(Value = "Nayarit")]
        Nayarit = 18,

        [EnumMember(Value = "Nuevo_Leon")]
        Nuevo_Leon = 19,

        [EnumMember(Value = "Oaxaca")]
        Oaxaca = 20,

        [EnumMember(Value = "Puebla")]
        Puebla = 21,

        [EnumMember(Value = "Queretaro")]
        Queretaro = 22,

        [EnumMember(Value = "Quintana_Roo")]
        Quintana_Roo = 23,

        [EnumMember(Value = "San_Luis_Potosi")]
        San_Luis_Potosi = 24,

        [EnumMember(Value = "Sinaloa")]
        Sinaloa = 25,

        [EnumMember(Value = "Sonora")]
        Sonora = 26,

        [EnumMember(Value = "Tabasco")]
        Tabasco = 27,

        [EnumMember(Value = "Tamaulipas")]
        Tamaulipas = 28,

        [EnumMember(Value = "Tlaxcala")]
        Tlaxcala = 29,

        [EnumMember(Value = "Veracruz")]
        Veracruz = 30,

        [EnumMember(Value = "Yucatan")]
        Yucatan = 31,

        [EnumMember(Value = "Zacatecas")]
        Zacatecas = 32,

        [EnumMember(Value = "Extranjero")]
        Extranjero = 33
    }
}