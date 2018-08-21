package com.sdl.web.pca.client.contentmodel.enums;

/**
 * Strategy of DCP template resolving is template ID is missing in request.
 */
public enum DcpType {
    /**
     * If template is not set, then load a default DXA Data Presentation.
     */
    DEFAULT,

    /**
     * If template is not set, then load a Component Presentation with the highest priority.
     */
    HIGHEST_PRIORITY
}
