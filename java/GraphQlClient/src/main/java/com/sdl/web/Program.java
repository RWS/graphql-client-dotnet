package com.sdl.web;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.sdl.GraphQLException;
import com.sdl.PublicContentApi;
import com.sdl.util.GraphQlQuery;
import java.io.IOException;
import java.net.URL;
import java.util.*;

public class Program {

    public static void main(String s[]) throws IOException, GraphQLException {

        String after = null;

        URL graphqlEndpoint = new URL("http://localhost:8081/udp/content");

        Map<String, String> headers = new HashMap<>();
        headers.put("Authentication", "Bearer: 123456");

        String query = GraphQlQuery.getGraphQlQuery();

        PublicContentApi client = new PublicContentApi(graphqlEndpoint, headers);

        JsonObject variables = new JsonObject();

        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        InputItemFilter inputItemFilter = new InputItemFilter();
        inputItemFilter.setItemTypes(new ArrayList<ItemType>(Arrays.asList(ItemType.COMPONENT)));
        inputItemFilter.setNamespaceIds(new ArrayList<Integer>(Arrays.asList(1)));
        /*inputItemFilter.setPublicationIds(new ArrayList<Integer>(Arrays.asList(5)));*/

        Publication publication = new Publication();
        publication.setNamespaceId(1);
        publication.setPublicationId(5);

        BinaryComponent binaryComponent = new BinaryComponent();
        binaryComponent.setNamespaceId(1);
        binaryComponent.setPublicationId(5);

        variables.add("contextData", new Gson().toJsonTree(inputClaimValues));

        /**
         * Item Type
         */

        variables.add("first", new Gson().toJsonTree(10));
        variables.add("after", new Gson().toJsonTree(after));
        variables.add("filter", new Gson().toJsonTree(inputItemFilter));

        /**
         * Publication
         */

        /* variables.add("namespaceId", new Gson().toJsonTree(publication.getNamespaceId()));
         variables.add("publicationId", new Gson().toJsonTree(publication.getPublicationId()));*/

        /**
         * Binary Component
         */

        /*variables.add("namespaceId", new Gson().toJsonTree(binaryComponent.getNamespaceId()));
        variables.add("publicationId", new Gson().toJsonTree(binaryComponent.getPublicationId()));
        variables.add("cmUri", new Gson().toJsonTree("tcm:5-168"));*/

       client.getComponent(query, variables);
    }
}




