package com.sdl.web.pca.client.jsonmapper;

import com.fasterxml.jackson.core.JsonParser;
import com.fasterxml.jackson.databind.DeserializationContext;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.deser.std.StdDeserializer;
import com.sdl.web.pca.client.contentmodel.generated.PageSitemapItem;
import com.sdl.web.pca.client.contentmodel.generated.SitemapItem;
import com.sdl.web.pca.client.contentmodel.generated.TaxonomySitemapItem;

import java.io.IOException;

public class SitemapDeserializer extends StdDeserializer<SitemapItem> {
    private ObjectMapper mapper;

    public SitemapDeserializer(Class<SitemapItem> itemClass, ObjectMapper mapper) {
        super(itemClass);
        this.mapper = mapper;
    }

    @Override
    public SitemapItem deserialize(JsonParser parser, DeserializationContext context) throws IOException {
        JsonNode node = parser.getCodec().readTree(parser);

        String type = node.get("type").asText();
        switch (type) {
            case "TaxonomyNode":
                return mapper.treeToValue(node, TaxonomySitemapItem.class);
            case "Page":
                return mapper.treeToValue(node, PageSitemapItem.class);
            default:
                throw new JsonMappingException(parser, "Unable to deserialize SitemapItem");
        }
    }
}
