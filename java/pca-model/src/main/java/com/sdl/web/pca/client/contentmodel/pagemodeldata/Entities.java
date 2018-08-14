
package com.sdl.web.pca.client.contentmodel.pagemodeldata;

public class Entities {

    private String Id;
    private Componenttemplate ComponentTemplate;
    private Folder Folder;
    private Content Content;
    private Mvcdata MvcData;
    private Xpmmetadata XpmMetadata;
    private String SchemaId;

    public String getId() {
        return Id;
    }

    public void setId(String id) {
        Id = id;
    }

    public Componenttemplate getComponentTemplate() {
        return ComponentTemplate;
    }

    public void setComponentTemplate(Componenttemplate componentTemplate) {
        ComponentTemplate = componentTemplate;
    }

    public com.sdl.web.pca.client.contentmodel.pagemodeldata.Folder getFolder() {
        return Folder;
    }

    public void setFolder(com.sdl.web.pca.client.contentmodel.pagemodeldata.Folder folder) {
        Folder = folder;
    }

    public com.sdl.web.pca.client.contentmodel.pagemodeldata.Content getContent() {
        return Content;
    }

    public void setContent(com.sdl.web.pca.client.contentmodel.pagemodeldata.Content content) {
        Content = content;
    }

    public Mvcdata getMvcData() {
        return MvcData;
    }

    public void setMvcData(Mvcdata mvcData) {
        MvcData = mvcData;
    }

    public Xpmmetadata getXpmMetadata() {
        return XpmMetadata;
    }

    public void setXpmMetadata(Xpmmetadata xpmMetadata) {
        XpmMetadata = xpmMetadata;
    }

    public String getSchemaId() {
        return SchemaId;
    }

    public void setSchemaId(String schemaId) {
        SchemaId = schemaId;
    }
}