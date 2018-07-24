package com.sdl;

import java.util.List;

public class GraphQLError {
    private String Message;
    private List<Object> Path;
    private List<GraphQLErrorLocation> Locations;
    private GraphQLExtensions Extensions;

    public String getMessage() {
        return Message;
    }

    public void setMessage(String message) {
        Message = message;
    }

    public List<Object> getPath() {
        return Path;
    }

    public void setPath(List<Object> path) {
        Path = path;
    }

    public List<GraphQLErrorLocation> getLocations() {
        return Locations;
    }

    public void setLocations(List<GraphQLErrorLocation> locations) {
        Locations = locations;
    }

    public GraphQLExtensions getExtensions() {
        return Extensions;
    }

    public void setExtensions(GraphQLExtensions extensions) {
        Extensions = extensions;
    }
}
