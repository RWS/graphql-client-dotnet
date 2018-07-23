package com.sdl;

import com.google.gson.JsonObject;

import java.io.IOException;

public interface IPublicContentApi {

    <T> T getComponent(String query, JsonObject variables) throws IOException, GraphQLException;
}
