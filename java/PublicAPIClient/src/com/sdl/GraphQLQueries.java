package com.sdl;

import java.io.InputStream;
import java.util.List;
import java.util.Properties;

public class GraphQLQueries {
    String Query="";
    String Variables ="";

    public void Load(InputItemFilter filter, IPagination pagination) {
        Query = LoadItemTypeFilter(filter);
    }

    public String LoadItems() {
        Properties properties = new Properties();
        InputStream input = null;
        try {
            String filename = "Query.properties";
            input = GraphQLQueries.class.getClassLoader().getResourceAsStream(filename);
            properties.load(input);
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        return properties.getProperty("ItemType");
    }

    public String LoadItemTypeFilter(InputItemFilter filter) {

        List<ItemType> itemtypes = filter.getItemTypes();

        String SubQueries = LoadItems();

        Properties properties = new Properties();
        InputStream input = null;
        try {
            String filename = "ItemTypeSubQuery.properties";
            input = GraphQLQueries.class.getClassLoader().getResourceAsStream(filename);
            properties.load(input);
        } catch (Exception ex) {
            ex.printStackTrace();
        }

        for (ItemType itemType : itemtypes) {
            SubQueries += properties.getProperty(itemType.toString());
        }
        return SubQueries;
    }

    public String LoadVariables(InputItemFilter filter, IPagination pagination){

        List<Integer> nameSpaceIds = filter.getNamespaceIds();
        List<Integer> publicationIds = filter.getPublicationIds();
        InputKeywordCriteria keywordCriteria = filter.getKeyword();
        InputCustomMetaCriteria inputCustomMetaCriteria = filter.getCustomMeta();




    }
}