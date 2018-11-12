package com.sdl.web.pca.client.modelserviceplugin;

public class ModelServiceClaimUris {
    /**
     * Claim Uri for content type argument.
     */
    public static final String CONTENT_TYPE = "dxa:modelservice:content:type";

    /**
     * Claim Uri for model type argument.
     */
    public static final String MODEL_TYPE = "dxa:modelservice:model:type";

    /**
     * Claim Uri for page include argument.
     */
    public static final String PAGE_INCLUDE_REGIONS = "dxa:modelservice:model:page:include";

    /**
     * Claim Uri for page expansion depth argument. The number should be greater than 0.
     */
    public static final String PAGE_EXPANSION_DEPTH = "dxa:modelservice:model:page:depth";

    /**
     * Claim Uri for component presentation link expansion when converting DD4T to R2.
     */
    public static final String ENTITY_RESOLVE_LINKS = "dxa:modelservice:model:entity:resolvelinks";

    /**
     * Claim Uri for strategy of DCP template resolving is template ID is missing.
     */
    public static final String ENTITY_DCP_TYPE = "dxa:modelservice:model:entity:dcptype";

    /**
     * Claim Uri for controlling how the model-service plugin renders links.
     */
    public static final String MODEL_SERVICE_LINK_RENDERING = "dxa:modelservice:model:entity:relativelinks";

    /**
     * Claim Uri for controlling how tcdl links get rendered.
     */
    public static final String TCDL_LINK_RENDERING = "taf:tcdl:render:link:relative";

    /**
     * Claim Uri for tcdl link url prefix.
     */
     public static final String TCDL_LINK_URL_PREFIX = "taf:tcdl:render:link:urlprefix";

    /**
     * Claim Uri for tcdl binary link url prefix.
     */
    public static final String TCDL_BINARY_LINK_URL_PREFIX = "taf:tcdl:render:link:binaryUrlPrefix";

}
