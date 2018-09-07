package com.sdl.web.pca.client.jsonmapper;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.deser.std.StdDeserializer;
import com.sdl.web.pca.client.contentmodel.generated.Component;
import com.sdl.web.pca.client.contentmodel.generated.ComponentPresentation;
import com.sdl.web.pca.client.contentmodel.generated.Item;
import com.sdl.web.pca.client.contentmodel.generated.Keyword;
import com.sdl.web.pca.client.contentmodel.generated.Page;
import com.sdl.web.pca.client.contentmodel.generated.Publication;
import com.sdl.web.pca.client.contentmodel.generated.StructureGroup;
import com.sdl.web.pca.client.contentmodel.generated.Template;
import com.sdl.web.pca.client.util.ItemTypes;

import java.io.IOException;

import static com.sdl.web.pca.client.util.ItemTypes.*;

public class ItemDeserializer extends StdDeserializer<Item> {
    private ObjectMapper mapper;

    public ItemDeserializer(Class<Item> itemClass, ObjectMapper mapper) {
        super(itemClass);
        this.mapper = mapper;
    }

    @Override
    public Item deserialize(JsonParser parser, DeserializationContext ctxt) throws IOException {
        JsonNode node = parser.getCodec().readTree(parser);

        ItemTypes type = ItemTypes.getById(node.get("itemType").asInt());

        switch (type) {
            case PUBLICATION:
                return mapper.treeToValue(node, Publication.class);
            case COMPONENT:
                return mapper.treeToValue(node, Component.class);
            case PUBLICATION_TARGET:
            case KEYWORD:
                return mapper.treeToValue(node, Keyword.class);
            case PAGE:
                return mapper.treeToValue(node, Page.class);
            case STRUCTURE_GROUP:
                return mapper.treeToValue(node, StructureGroup.class);
            case COMPONENT_TEMPLATE:
                return mapper.treeToValue(node, Template.class);
            case COMPONENT_PRESENTATION:
                return mapper.treeToValue(node, ComponentPresentation.class);
            default:
                throw new JsonMappingException(parser, "Unable to deserialize Item");
        }
    }
}
