package com.sdl.web.pca.client;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import com.sdl.web.pca.client.contentmodel.*;

import java.io.IOException;
import java.net.URL;
import java.util.*;

public class Program {

    public static void main(String s[]) throws IOException {

        IGraphQLClient graphQLClient = new GraphQLClient("http://localhost:8081/udp/content/", null);

        PublicContentApi client = new PublicContentApi(graphQLClient);

        InputItemFilter filter = new InputItemFilter();
        filter.setNamespaceIds(Collections.singletonList(1));
        filter.setItemTypes(Collections.singletonList(ItemType.PAGE));
        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        Pagination pagination = new Pagination();
        pagination.setFirst(2);


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

        ContentComponent contentComponent = client.ExecuteItemQuery(filter, pagination);
    }
}




