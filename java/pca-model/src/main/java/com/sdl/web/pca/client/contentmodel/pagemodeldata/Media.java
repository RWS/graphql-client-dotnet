
package com.sdl.web.pca.client.contentmodel.pagemodeldata;

public class Media {

    private String $type;
    private String Id;
    private Componenttemplate ComponentTemplate;
    private Folder Folder;
    private Binarycontent BinaryContent;
    private String SchemaId;

    public String get$type() {
        return $type;
    }

    public void set$type(String $type) {
        this.$type = $type;
    }

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

    public Binarycontent getBinaryContent() {
        return BinaryContent;
    }

    public void setBinaryContent(Binarycontent binaryContent) {
        BinaryContent = binaryContent;
    }

    public String getSchemaId() {
        return SchemaId;
    }

    public void setSchemaId(String schemaId) {
        SchemaId = schemaId;
    }
}