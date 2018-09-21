package com.sdl.web.pca.client.util;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class CmUriTest {

    @Test
    public void testCmUriCrate() {
        CmUri uri = new CmUri("ish:12-34-512-v123");
        assertEquals("ish", uri.getNamespace());
        assertEquals(12, uri.getPublicationId());
        assertEquals(34, uri.getItemId());
        assertEquals(512, uri.getItemType());
        assertEquals(123, uri.getVersion());

        uri = new CmUri("tcm:12-34");
        assertEquals("tcm", uri.getNamespace());
        assertEquals(12, uri.getPublicationId());
        assertEquals(34, uri.getItemId());
        assertEquals(16, uri.getItemType());
        assertEquals(-1, uri.getVersion());

        uri = new CmUri("tcm:12-34-256");
        assertEquals("tcm", uri.getNamespace());
        assertEquals(12, uri.getPublicationId());
        assertEquals(34, uri.getItemId());
        assertEquals(256, uri.getItemType());
        assertEquals(-1, uri.getVersion());

        uri = new CmUri("ish:12-34-v111");
        assertEquals("ish", uri.getNamespace());
        assertEquals(12, uri.getPublicationId());
        assertEquals(34, uri.getItemId());
        assertEquals(16, uri.getItemType());
        assertEquals(111, uri.getVersion());
    }

    @Test(expected = IllegalArgumentException.class)
    public void testIncorrectNamespace() {
        new CmUri("incorrect:12-34-128");
    }

    @Test(expected = IllegalArgumentException.class)
    public void testIncorrectItemType() {
        new CmUri("ish:12-34-555");
    }

    @Test
    public void testToString() {
        CmUri uri = new CmUri("ish:12-34");
        assertEquals("ish:12-34-16", uri.toString());

        uri = new CmUri("tcm:12-34-v33");
        assertEquals("tcm:12-34-16-v33", uri.toString());
    }

}