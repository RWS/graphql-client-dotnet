package com.sdl;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class Main {

    public static void main(String[] args) {

        IGraphQLClient graphQLClient = new GraphQLClient("http://localhost:8081/udp/content/");

        PublicContentApi client = new PublicContentApi(graphQLClient);

        InputItemFilter filter = new InputItemFilter();
        Pagination pagination = new Pagination();
        pagination.setFirst(10);

        filter.setNamespaceIds(Collections.singletonList(1));
        filter.setItemTypes(Collections.singletonList(ItemType.COMPONENT));

        ItemConnection itemQueryExample = client.ExecuteItemQuery(filter, pagination);

    }
}
