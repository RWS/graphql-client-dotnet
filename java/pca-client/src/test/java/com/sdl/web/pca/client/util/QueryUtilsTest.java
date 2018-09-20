package com.sdl.web.pca.client.util;

import org.junit.Test;

import static com.sdl.web.pca.client.TestUtils.assertEqualsIgnoreSpaces;
import static com.sdl.web.pca.client.TestUtils.loadFromResource;
import static org.junit.Assert.assertEquals;

public class QueryUtilsTest {
    private static final String RECURSE_FRAGMENT = loadFromResource("RecurseFragment");
    private static final String QUERY = "query sitemap($namespaceId: Int!, $publicationId: Int!) {          \n" +
            "    sitemap(namespaceId: $namespaceId, publicationId: $publicationId) {\n" +
            "                ...RecurseItems\n" +
            "    }\n" +
            "}";

    @Test
    public void expandRecursivelyZeroDescendant() {
        String expected = "query sitemap($namespaceId: Int!, $publicationId: Int!) {          \n" +
                "    sitemap(namespaceId: $namespaceId, publicationId: $publicationId) {\n" +
                "                \n" +
                "    }\n" +
                "}";
        int descendantLevel = 0;
        String result = QueryUtils.expandRecursively(QUERY, RECURSE_FRAGMENT, descendantLevel);

        assertEquals(expected, result);
    }

    @Test
    public void expandRecursivelyPositiveDescendant() {
        String expected = loadFromResource("expectedExpandRecursivelyPositive");
        int descendantLevel = 2;
        String result = QueryUtils.expandRecursively(QUERY, RECURSE_FRAGMENT, descendantLevel);

        assertEqualsIgnoreSpaces(expected, result);
    }

    @Test
    public void testGetFragmentName() {
        assertEquals("RecurseItems", QueryUtils.getFragmentName(RECURSE_FRAGMENT));
    }

    @Test
    public void testGetFragmentBody() {
        String expected = loadFromResource("expectedFragmentBody");

        assertEquals(expected, QueryUtils.getFragmentBody(RECURSE_FRAGMENT));
    }

}