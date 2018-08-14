package com.sdl.web.pca.client.contentmodel;

/// <summary>
/// Represents a component presentation which has a component associated with a template.
/// </summary>
class ComponentPresentation implements IItem
{
    private IContentComponent component;
    private Template componentTemplate;
    private IContent content;
    private String creationDate;
    private CustomMetaConnection customMetas;
    private String id;
    private String initialPublishDate;
    private int itemId;
    private int itemType;
    private String lastPublishDate;
    private int namespaceId;
    private int owningPublicationId;
    private int publicationId;
    private RawContent rawContent;
    private String title;
    private String updatedDate;

     public IContentComponent getComponent()
     {
         return component;
     }
     public void setComponent(IContentComponent component)
     {
         this.component = component;
     }

     public Template getComponentTemplate()
     {
         return componentTemplate;
     }
     public void setComponentTemplate(Template componentTemplate)
     {
         this.componentTemplate = componentTemplate;
     }

     public IContent getContent()
     {
         return content;
     }
     public void setContent(IContent content)
     {
         this.content = content;
     }

     public String getCreationDate()
     {
         return creationDate;
     }
     public void setCreationDate(String creationDate)
     {
         this.creationDate = creationDate;
     }

     public CustomMetaConnection getCustomMetas()
     {
         return customMetas;
     }
     public void setCustomMetas(CustomMetaConnection customMetas)
     {
         this.customMetas = customMetas;
     }

     public String getId()
     {
         return id;
     }
     public void setId(String id)
     {
         this.id = id;
     }

     public String getInitialPublishDate()
     {
         return initialPublishDate;
     }
     public void setInitialPublishDate(String initialPublishDate)
     {
         this.initialPublishDate = initialPublishDate;
     }

     public int getItemId()
     {
         return itemId;
     }
     public void setItemId(int itemId)
     {
         this.itemId = itemId;
     }

     public int getItemType()
     {
         return itemType;
     }
     public void setItemType(int itemType)
     {
         this.itemType = itemType;
     }

     public String getLastPublishDate()
     {
         return lastPublishDate;
     }
     public void setLastPublishDate(String lastPublishDate)
     {
         this.lastPublishDate = lastPublishDate;
     }

     public int getNamespaceId()
     {
         return namespaceId;
     }
     public void setNamespaceId(int namespaceId)
     {
         this.namespaceId = namespaceId;
     }

     public int getOwningPublicationId()
     {
         return owningPublicationId;
     }
     public void setOwningPublicationId(int owningPublicationId)
     {
         this.owningPublicationId = owningPublicationId;
     }

     public int getPublicationId()
     {
         return publicationId;
     }
     public void setPublicationId(int publicationId)
     {
         this.publicationId = publicationId;
     }

     public RawContent getRawContent()
     {
         return rawContent;
     }
     public void setRawContent(RawContent rawContent)
     {
         this.rawContent = rawContent;
     }

     public String getTitle()
     {
         return title;
     }
     public void setTitle(String title)
     {
         this.title = title;
     }

     public String getUpdatedDate()
     {
         return updatedDate;
     }
     public void setUpdatedDate(String updatedDate)
     {
         this.updatedDate = updatedDate;
     }
}
