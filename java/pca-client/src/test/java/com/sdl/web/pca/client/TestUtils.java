package com.sdl.web.pca.client;

import org.apache.commons.io.IOUtils;

import java.io.IOException;
import java.io.InputStream;

import static java.nio.charset.StandardCharsets.UTF_8;
import static org.junit.Assert.assertEquals;

public class TestUtils {
    private TestUtils() {
    }

    public static String loadFromResource(String fileName) {
        try {
            InputStream fileInputStream = TestUtils.class.getResourceAsStream("/" + fileName);
            return IOUtils.toString(fileInputStream, UTF_8);
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }

    public static void assertEqualsIgnoreSpaces(String expected, String actual) {
        assertEquals(expected.replaceAll("\\s+", " ").trim(),
                actual.replaceAll("\\s+", " ").trim());
    }

}
