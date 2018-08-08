package com.sdl.web.pca.client.contentmodel1;

import java.util.Date;

/// <summary>
/// Represents a node.
/// </summary>
class Node
{
    private String id;
    private int itemId;
    private int itemType;
    private int namespaceId;
    private int owningPublicationId;
    private int publicationId;
    private String title;
    private Date lastPublishDate;
    private Date creationDate;
    private Date initialPublishDate;
    private Date updatedDate;
    private CustomMetas customMetas;
    private int schemaId;
    private boolean multiMedia;
    private String key;
    private String value;
    private String valueType;
    private String url;
    private String publicationPath;
    private String publicationUrl;
    private String navigable;
    private String usedForIdentification;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public int getItemId() {
        return itemId;
    }

    public void setItemId(int itemId) {
        this.itemId = itemId;
    }

    public int getItemType() {
        return itemType;
    }

    public void setItemType(int itemType) {
        this.itemType = itemType;
    }

    public int getNamespaceId() {
        return namespaceId;
    }

    public void setNamespaceId(int namespaceId) {
        this.namespaceId = namespaceId;
    }

    public int getOwningPublicationId() {
        return owningPublicationId;
    }

    public void setOwningPublicationId(int owningPublicationId) {
        this.owningPublicationId = owningPublicationId;
    }

    public int getPublicationId() {
        return publicationId;
    }

    public void setPublicationId(int publicationId) {
        this.publicationId = publicationId;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public Date getLastPublishDate() {
        return lastPublishDate;
    }

    public void setLastPublishDate(Date lastPublishDate) {
        this.lastPublishDate = lastPublishDate;
    }

    public Date getCreationDate() {
        return creationDate;
    }

    public void setCreationDate(Date creationDate) {
        this.creationDate = creationDate;
    }

    public Date getInitialPublishDate() {
        return initialPublishDate;
    }

    public void setInitialPublishDate(Date initialPublishDate) {
        this.initialPublishDate = initialPublishDate;
    }

    public Date getUpdatedDate() {
        return updatedDate;
    }

    public void setUpdatedDate(Date updatedDate) {
        this.updatedDate = updatedDate;
    }

    public CustomMetas getCustomMetas() {
        return customMetas;
    }

    public void setCustomMetas(CustomMetas customMetas) {
        this.customMetas = customMetas;
    }

    public int getSchemaId() {
        return schemaId;
    }

    public void setSchemaId(int schemaId) {
        this.schemaId = schemaId;
    }

    public boolean isMultiMedia() {
        return multiMedia;
    }

    public void setMultiMedia(boolean multiMedia) {
        this.multiMedia = multiMedia;
    }

    public String getKey() {
        return key;
    }

    public void setKey(String key) {
        this.key = key;
    }

    public String getValue() {
        return value;
    }

    public void setValue(String value) {
        this.value = value;
    }

    public String getValueType() {
        return valueType;
    }

    public void setValueType(String valueType) {
        this.valueType = valueType;
    }

    public String getUrl() {
        return url;
    }

    public void setUrl(String url) {
        this.url = url;
    }

    public String getPublicationPath() {
        return publicationPath;
    }

    public void setPublicationPath(String publicationPath) {
        this.publicationPath = publicationPath;
    }

    public String getPublicationUrl() {
        return publicationUrl;
    }

    public void setPublicationUrl(String publicationUrl) {
        this.publicationUrl = publicationUrl;
    }

    public String getNavigable() {
        return navigable;
    }

    public void setNavigable(String navigable) {
        this.navigable = navigable;
    }

    public String getUsedForIdentification() {
        return usedForIdentification;
    }

    public void setUsedForIdentification(String usedForIdentification) {
        this.usedForIdentification = usedForIdentification;
    }
}
