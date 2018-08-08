package com.sdl.web.pca.client;


import com.sdl.web.pca.client.request.GraphQLRequest;
import com.sdl.web.pca.client.request.IGraphQLRequest;
import org.apache.commons.io.IOUtils;
import com.sdl.web.pca.client.contentmodel1.*;
import java.io.IOException;
import java.util.HashMap;

public class PublicContentApi implements IPublicContentApi {

    public IGraphQLClient _client;

    public PublicContentApi(IGraphQLClient graphQLClient) {
        _client = graphQLClient;
    }

    private String LoadQueryFromResourcefile(String filename) throws IOException {
        String query = IOUtils.toString(this.getClass().getResourceAsStream(filename+".graphql"),"UTF-8");
        return query;
    }

    public <T> T ExecuteItemQuery(InputItemFilter filter, IPagination pagination) throws IOException {

        String customMetaFilter ="";
        String query = LoadQueryFromResourcefile("ItemQuery");
        query += LoadQueryFromResourcefile( "ItemFieldsFragment");
        query += LoadQueryFromResourcefile( "CustomMetaFieldsFragment");

        // We only include the fragments that will be required based on the item types in the
        // input item filter
        if (filter.getItemTypes() != null)
        {
            String fragmentList = "";
            for (ItemType itemType : filter.getItemTypes())
            {
                String fragment = itemType.name().toUpperCase()+"Fields";
                query +=  LoadQueryFromResourcefile(fragment + "Fragment");
                fragmentList += "..."+fragment+"\n";
            }
            // Just a quick and easy way to replace markers in our queries with vars here.
            query = query.replace("@fragmentList", fragmentList);
            query = query.replace("@customMetaFilter", "\""+customMetaFilter+"\"");
        }

        InputClaimValue[] inputClaimValues = new InputClaimValue[0];

        HashMap<String, Object> variables = new HashMap<String, Object>();
        variables.put("first", pagination.getFirst());
        variables.put("after", pagination.getAfter());
        variables.put("filter", filter);
        variables.put("contextData", inputClaimValues);

        IGraphQLRequest graphQLRequest =new GraphQLRequest();
        graphQLRequest.setQuery(query);
        graphQLRequest.setVariables(variables);

       String contentQuery = _client.execute(graphQLRequest);

        /* ObjectMapper objectMapper = new ObjectMapper();

        ContentComponent contentComponent = objectMapper.readValue(contentQuery, ContentComponent.class);


        return (T) contentComponent;*/
        return null;
    }
}
