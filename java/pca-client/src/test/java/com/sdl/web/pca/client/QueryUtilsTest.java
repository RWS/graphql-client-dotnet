package com.sdl.web.pca.client;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class QueryUtilsTest {
    private static final String RECURSE_FRAGMENT = "\uFEFFrfragment RecurseItems on TaxonomySitemapItem {  \n" +
            "\t...TaxonomyItemFields\n" +
            "    items {\t\t  \n" +
            "\t\t...on TaxonomySitemapItem {\n" +
            "\t\t\t...TaxonomyItemFields\t\t\n" +
            "\t\t\t...RecurseItems\n" +
            "\t\t}\n" +
            "\t\t...on PageSitemapItem {\n" +
            "\t\t\t...TaxonomyPageFields\n" +
            "\t\t}\n" +
            "    }\n" +
            "}";
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
        String expected = "query sitemap($namespaceId: Int!, $publicationId: Int!) {          \n" +
                "    sitemap(namespaceId: $namespaceId, publicationId: $publicationId) {\n" +
                "                  \n" +
                "\t...TaxonomyItemFields\n" +
                "    items {\t\t  \n" +
                "\t\t...on TaxonomySitemapItem {\n" +
                "\t\t\t...TaxonomyItemFields\t\t\n" +
                "\t\t\t  \n" +
                "\t...TaxonomyItemFields\n" +
                "    items {\t\t  \n" +
                "\t\t...on TaxonomySitemapItem {\n" +
                "\t\t\t...TaxonomyItemFields\t\t\n" +
                "\t\t\t\n" +
                "\t\t}\n" +
                "\t\t...on PageSitemapItem {\n" +
                "\t\t\t...TaxonomyPageFields\n" +
                "\t\t}\n" +
                "    }\n" +
                "\n" +
                "\t\t}\n" +
                "\t\t...on PageSitemapItem {\n" +
                "\t\t\t...TaxonomyPageFields\n" +
                "\t\t}\n" +
                "    }\n" +
                "\n" +
                "    }\n" +
                "}";
        int descendantLevel = 2;
        String result = QueryUtils.expandRecursively(QUERY, RECURSE_FRAGMENT, descendantLevel);

        assertEquals(expected, result);
    }

    @Test
    public void testGetFragmentName() {
        assertEquals("RecurseItems", QueryUtils.getFragmentName(RECURSE_FRAGMENT));
    }

    @Test
    public void testGetFragmentBody() {
        String expected = "  \n" +
                "\t...TaxonomyItemFields\n" +
                "    items {\t\t  \n" +
                "\t\t...on TaxonomySitemapItem {\n" +
                "\t\t\t...TaxonomyItemFields\t\t\n" +
                "\t\t\t...RecurseItems\n" +
                "\t\t}\n" +
                "\t\t...on PageSitemapItem {\n" +
                "\t\t\t...TaxonomyPageFields\n" +
                "\t\t}\n" +
                "    }\n";

        assertEquals(expected, QueryUtils.getFragmentBody(RECURSE_FRAGMENT));
    }

}