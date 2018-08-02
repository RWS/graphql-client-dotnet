package com.sdl.web.pca.client;

import java.io.IOException;
import java.util.Map;

/**
 * This interface enables java clients to connect to the GraphQL Service
 * @author Rajesh
 * @version 1.0.0
 */
public interface IGraphQLClient {

    /**
     * This method is used to execute the graphQL queries.
     * @param jsonEntity graphql query which needs to be executed against the graphql server
     * @param timeout  specify the timeout period in milliseconds
     * @return The GraphQL response with data and errors if any.
     */
    String execute(String jsonEntity, int timeout) throws IOException;

    /**
     * This method is used to execute the graphQL queries with no timeout.
     * @param jsonEntity graphql query which needs to be executed against the graphql server
     * @return The GraphQL response with data and errors if any.
     */
    String execute(String jsonEntity) throws IOException;
}
