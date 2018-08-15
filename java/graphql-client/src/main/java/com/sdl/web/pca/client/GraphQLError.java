package com.sdl.web.pca.client;

import java.util.List;

public class GraphQLError {
    public String Message;
    public List<Object> Path;
    public Object locations;
    public Object errorType;
    public Object extensions;

    public void setMessage(String message) {
        Message = message;
    }

    public String getMessage() {
        return Message;
    }

    public void setPath(List<Object> path) {
        Path = path;
    }

    public List<Object> getPath() {
        return Path;
    }
}
