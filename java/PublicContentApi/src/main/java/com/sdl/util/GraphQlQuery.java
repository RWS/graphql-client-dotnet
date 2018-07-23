package com.sdl.util;

import java.io.InputStream;
import java.util.Properties;

public class GraphQlQuery {
    public static String getGraphQlQuery(){

        Properties properties = new Properties();
        InputStream input = null;
        try {
            String filename = "Query.properties";
            input = GraphQlQuery.class.getClassLoader().getResourceAsStream(filename);
            properties.load(input);
        }catch (Exception ex){ex.printStackTrace();}

        return properties.getProperty("ItemType_Component");
    }
}
