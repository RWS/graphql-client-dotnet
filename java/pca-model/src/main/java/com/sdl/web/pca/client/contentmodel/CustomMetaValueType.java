package com.sdl.web.pca.client.contentmodel;

/*[JsonConverter(typeof(StringEnumConverter))]*/
	enum CustomMetaValueType
	{
		/// <summary>
		/// STRING
		/// </summary>
		STRING,

		/// <summary>
		/// DATE
		/// </summary>
		DATE,

		/// <summary>
		/// FLOAT
		/// </summary>
		FLOAT,

		/// <summary>
		/// NUMBER
		/// </summary>
		NUMBER,

		UNKNOWN
	}