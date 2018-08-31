package com.sdl.web.pca.client.contentmodel;

/// <summary>
/// Represents the type of claim value.
/// </summary>
/*[JsonConverter(typeof(StringEnumConverter))]*/
public enum ClaimValueType
{
    /// <summary>
    /// A string value.
    /// </summary>
    STRING,

    /// <summary>
    /// A date value.
    /// </summary>
    DATE,

    /// <summary>
    /// A float value.
    /// </summary>
    FLOAT,

    /// <summary>
    /// A number value.
    /// </summary>
    NUMBER,

    BOOLEAN
}
