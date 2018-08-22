package com.sdl.web.pca.client;

public class QueryUtils {

    public static String injectRenderContentArgs(String query, boolean renderContent) {
        return query.replace("@renderContentArgs", "(renderContent: " + renderContent + ")");
    }

    public static String removeCRLFTabs(String query) {
        return query.replace("\n", "").replace("\t", "");
    }
}
