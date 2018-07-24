package com.sdl;

import java.io.IOException;
import java.util.Collections;

public class Main {

    public static void main(String[] args) throws IOException {

        IGraphQLClient graphQLClient = new GraphQLClient("http://localhost:8081/udp/content/");

        PublicContentApi client = new PublicContentApi(graphQLClient);

        InputItemFilter filter = new InputItemFilter();
        Pagination pagination = new Pagination();
        pagination.setFirst(10);

        filter.setNamespaceIds(Collections.singletonList(1));
        filter.setItemTypes(Collections.singletonList(ItemType.PAGE));

        ItemConnection itemQueryExample = client.ExecuteItemQuery(filter, pagination);

    }
}
