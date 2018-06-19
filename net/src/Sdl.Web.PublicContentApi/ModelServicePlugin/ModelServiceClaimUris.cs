namespace Sdl.Web.PublicContentApi.ModelServicePlugin
{
    /// <summary>
    /// Claim Values for PCA Model Service Plugin
    /// These are used to control the plugin behaviour 
    /// </summary>
    public static class ModelServiceClaimUris
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
    }
}
