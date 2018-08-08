package com.sdl.web.pca.client.contentmodel1;

/// <summary>
/// Represents the type of claim value.
/// </summary>
/*[JsonConverter(typeof(StringEnumConverter))]*/
enum ClaimValueType
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
