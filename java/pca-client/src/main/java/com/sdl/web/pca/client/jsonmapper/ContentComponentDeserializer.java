package com.sdl.web.pca.client.jsonmapper;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.deser.std.StdDeserializer;
import com.sdl.web.pca.client.contentmodel.generated.Component;
import com.sdl.web.pca.client.contentmodel.generated.ContentComponent;

import java.io.IOException;

public class ContentComponentDeserializer extends StdDeserializer<ContentComponent> {

    private ObjectMapper mapper;

    public ContentComponentDeserializer(Class<ContentComponent> contentComponentClass, ObjectMapper mapper) {
        super(contentComponentClass);
        this.mapper = mapper;
    }

    @Override
    public ContentComponent deserialize(JsonParser parser, DeserializationContext ctxt) throws IOException {
        JsonNode node = parser.getCodec().readTree(parser);

        return mapper.treeToValue(node, Component.class);
    }
}
