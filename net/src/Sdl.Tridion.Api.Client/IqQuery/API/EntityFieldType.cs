using System;
using Sdl.Tridion.Api.IqQuery.Model.Field;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Sdl.Tridion.Api.IqQuery
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EntityFieldType
    {
        [EnumMember(Value = "STRING")]
        String,

        [EnumMember(Value = "INTEGER")]
        Integer,

        [EnumMember(Value = "LONG")]
        Long,

        [EnumMember(Value = "FLOAT")]
        Float,

        [EnumMember(Value = "DOUBLE")]
        Double,

        [EnumMember(Value = "BOOLEAN")]
        Boolean,

        [EnumMember(Value = "DATE")]
        Date,

        [EnumMember(Value = "BINARY")]
        Binary,

        [EnumMember(Value = "ARRAY")]
        Array
    }

    public static class FieldUtils
    {
        public static EntityFieldType DetectFieldType(object fieldValue)
        {
            if (fieldValue == null)
                throw new FieldException("Field is null");

            if (fieldValue is ITermValue)
            {
                fieldValue = ((ITermValue) fieldValue).Value;
            }

            if (fieldValue is string)
                return EntityFieldType.String;

            if (fieldValue is DateTime || fieldValue is DateTimeOffset)
                return EntityFieldType.Date;

            if (fieldValue is int)
                return EntityFieldType.Integer;

            if (fieldValue is long)
                return EntityFieldType.Long;

            if (fieldValue is float)
                return EntityFieldType.Float;

            if (fieldValue is double)
                return EntityFieldType.Double;

            if (fieldValue is bool)
                return EntityFieldType.Boolean;

            if (fieldValue.GetType().IsArray)
                return EntityFieldType.Array;

            throw new FieldException($"Unhandled type. Class {fieldValue.GetType()}");
        }
    }
}