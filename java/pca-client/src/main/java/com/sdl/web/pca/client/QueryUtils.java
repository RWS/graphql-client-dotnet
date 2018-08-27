package com.sdl.web.pca.client;


public class QueryUtils {

    public static String injectRenderContentArgs(String query, boolean renderContent) {
        return query.replace("@renderContentArgs", "(renderContent: " + renderContent + ")");
    }

    public static String injectVariantsArgs(String query, String url) {
        return query.replace("@variantsArgs", isNullOrEmpty(url) ? "" : ("url: " + url));
   }

    public static String injectCustomMetaFilter(String query, String customMetaFilter) {
        return query.replace("@customMetaArgs", isNullOrEmpty(customMetaFilter) ? "" : ("filter: " + customMetaFilter));
    }

    private static boolean isNullOrEmpty(String str) {
        return str == null || str.isEmpty();
    }


}
