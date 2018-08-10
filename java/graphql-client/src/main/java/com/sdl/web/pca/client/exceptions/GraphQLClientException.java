package com.sdl.web.pca.client.exceptions;

public class GraphQLClientException extends RuntimeException {

    public GraphQLClientException() {
    }

    public GraphQLClientException(String message) {
        super(message);
    }

    public GraphQLClientException(String message, Throwable cause) {
        super(message, cause);
    }

    public GraphQLClientException(Throwable cause) {
        super(cause);
    }

    public GraphQLClientException(String message, Throwable cause, boolean enableSuppression, boolean writableStackTrace) {
        super(message, cause, enableSuppression, writableStackTrace);
    }
}
