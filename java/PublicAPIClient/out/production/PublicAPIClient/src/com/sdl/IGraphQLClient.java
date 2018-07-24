package com.sdl;


import com.fasterxml.jackson.core.JsonProcessingException;

import java.io.IOException;

public interface IGraphQLClient {

    String Execute(IGraphQLRequest request) throws JsonProcessingException, IOException;
}
