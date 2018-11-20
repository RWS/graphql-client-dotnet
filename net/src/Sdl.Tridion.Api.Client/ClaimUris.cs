namespace Sdl.Tridion.Api.Client
{
    /// <summary>
    /// Claim Values for PCA Model Service Plugin
    /// These are used to control the plugin behaviour 
    /// </summary>
    public static class ClaimUris
    {
        /// <summary>
        /// Claim Uri for content type argument.
        /// </summary>
        public static readonly string ContentType = "dxa:modelservice:content:type";

        /// <summary>
        /// Claim Uri for model type argument
        /// </summary>
        public static readonly string ModelType = "dxa:modelservice:model:type";

        /// <summary>
        /// Claim Uri for page include argument
        /// </summary>
        public static readonly string PageIncludeRegions = "dxa:modelservice:model:page:include";

        /// <summary>
        /// Claim Uri for page expansion depth argument. The number should be greater than 0.
        /// </summary>
        public static readonly string PageExpansionDepth = "dxa:modelservice:model:page:depth";

        /// <summary>
        /// Claim Uri for component presentation link expansion when converting DD4T to R2.
        /// </summary>
        public static readonly string EntityResolveLinks = "dxa:modelservice:model:entity:resolvelinks";

        /// <summary>
        /// Claim Uri for strategy of DCP template resolving is template ID is missing.
        /// </summary>
        public static readonly string EntityDcpType = "dxa:modelservice:model:entity:dcptype";

        /// <summary>
        /// Claim Uri for controlling how the model-service plugin renders links
        /// </summary>
        public static readonly string ModelServiceLinkRendering = "dxa:modelservice:model:entity:relativelinks";

        /// <summary>
        /// Claim Uri for controlling how tcdl links get rendered
        /// </summary>
        public static readonly string TcdlLinkRendering = "taf:tcdl:render:link:relative";

        /// <summary>
        /// Claim Uri for tcdl link url prefix
        /// </summary>
        public static readonly string TcdlLinkUrlPrefix = "taf:tcdl:render:link:urlprefix";

        /// <summary>
        /// Claim Uri for tcdl binary link url prefix
        /// </summary>
        public static readonly string TcdlBinaryLinkUrlPrefix = "taf:tcdl:render:link:binaryUrlPrefix";
    }
}
