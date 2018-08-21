package com.sdl.web.pca.client.exception;

public class PublicContentApiException extends RuntimeException {
    public PublicContentApiException() {
    }

    public PublicContentApiException(String message) {
        super(message);
    }

    public PublicContentApiException(String message, Throwable cause) {
        super(message, cause);
    }

    public PublicContentApiException(Throwable cause) {
        super(cause);
    }

    public PublicContentApiException(String message, Throwable cause, boolean enableSuppression, boolean writableStackTrace) {
        super(message, cause, enableSuppression, writableStackTrace);
    }
}
