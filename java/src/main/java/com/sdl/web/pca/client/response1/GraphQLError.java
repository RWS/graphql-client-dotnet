package com.sdl.web.pca.client.response;

import java.util.List;

public class GraphQLError {
    public String Message;
    public List<Object> Path;
    public List<GraphQLErrorLocation> Locations;
    public GraphQLExtensions Extensions;

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

    public void setLocations(List<GraphQLErrorLocation> locations) {
        Locations = locations;
    }

    public List<GraphQLErrorLocation> getLocations() {
        return Locations;
    }

    public void setExtensions(GraphQLExtensions extensions) {
        Extensions = extensions;
    }

    public GraphQLExtensions getExtensions() {
        return Extensions;
    }
}
