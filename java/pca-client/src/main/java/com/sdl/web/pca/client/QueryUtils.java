package com.sdl.web.pca.client;


import org.slf4j.Logger;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

import static java.util.regex.Pattern.DOTALL;
import static java.util.regex.Pattern.MULTILINE;
import static org.slf4j.LoggerFactory.getLogger;

public class QueryUtils {
    private static final Logger LOG = getLogger(QueryUtils.class);

    private static final Pattern RECURSE_FRAGMENT_NAME = Pattern.compile("^.*rfragment\\s+(?<fragmentName>\\w*).*\\{");
    private static final Pattern RECURSE_FRAGMENT_BODY = Pattern.compile("^[^\\{]*\\{(?<fragmentBody>.*)\\}$",
            DOTALL | MULTILINE);

    public static String expandRecursively(String query, String recurseFragment, int descendantLevel) {
        String fragmentName = getFragmentName(recurseFragment);
        String fragmentBody = getFragmentBody(recurseFragment);
        String result = query;
        for (int i = 0; i < descendantLevel; i++) {
            result = result.replace("..." + fragmentName, fragmentBody);
        }
        return result.replace("..." + fragmentName, "");
    }

    public static String injectRenderContentArgs(String query, boolean renderContent) {
        return query.replace("@renderContentArgs", "(renderContent: " + renderContent + ")");
    }

    public static String injectVariantsArgs(String query, String url) {
        return query.replace("@variantsArgs", isNullOrEmpty(url) ? "" : ("(url: \"" + url + "\")"));
   }

    public static String injectCustomMetaFilter(String query, String customMetaFilter) {
        return query.replace("@customMetaArgs", isNullOrEmpty(customMetaFilter) ? "" : ("filter: " + customMetaFilter));
    }

    static String getFragmentName(String fragment) {
        Matcher matcher = RECURSE_FRAGMENT_NAME.matcher(fragment);
        if (matcher.find()) {
            return matcher.group("fragmentName");
        }
        LOG.error("Unable to parse name for fragment: {}", fragment);
        return null;
    }

    static String getFragmentBody(String fragment) {
        Matcher matcher = RECURSE_FRAGMENT_BODY.matcher(fragment);
        if (matcher.find()) {
            return matcher.group("fragmentBody");
        }
        LOG.error("Unable to parse body for fragment: {}", fragment);
        return null;
    }

    private static boolean isNullOrEmpty(String str) {
        return str == null || str.isEmpty();
    }



}
