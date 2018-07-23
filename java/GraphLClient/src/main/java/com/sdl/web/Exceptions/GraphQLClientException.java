package com.sdl.web.Exceptions;

import com.sdl.web.Response.IGraphQLResponse;

public class GraphQLClientException extends Exception{

    public int StatusCode;
    public IGraphQLResponse Response;

    public int getStatusCode() {
        return StatusCode;
    }

    public IGraphQLResponse getResponse() {
        return Response;
    }

    public GraphQLClientException() {
    }
    public GraphQLClientException(String msg) {
    }
    public GraphQLClientException(String msg, Exception ex) {
    }
    public GraphQLClientException(IGraphQLResponse response){
        Response = response;
    }
    public GraphQLClientException(IGraphQLResponse response, String msg)
    {
        Response = response;
    }
    public GraphQLClientException(IGraphQLResponse response, String msg, Exception ex)
    {
        Response = response;
    }
    public GraphQLClientException(IGraphQLResponse response, String msg, Exception ex, int statusCode){
        StatusCode = statusCode;
        Response = response;
    }
}
