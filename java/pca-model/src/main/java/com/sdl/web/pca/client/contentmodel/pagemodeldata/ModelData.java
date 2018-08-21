package com.sdl.web.pca.client.contentmodel.pagemodeldata;


import java.util.List;

public class ModelData {

    private String Id;
    private String Title;
    private Pagetemplate PageTemplate;
    private String StructureGroupId;
    private String UrlPath;
    private Meta Meta;
    private List<Regions> Regions;
    private Mvcdata MvcData;
    private Xpmmetadata XpmMetadata;

    public String getId() {
        return Id;
    }

    public void setId(String id) {
        Id = id;
    }

    public String getTitle() {
        return Title;
    }

    public void setTitle(String title) {
        Title = title;
    }

    public Pagetemplate getPageTemplate() {
        return PageTemplate;
    }

    public void setPageTemplate(Pagetemplate pageTemplate) {
        PageTemplate = pageTemplate;
    }

    public String getStructureGroupId() {
        return StructureGroupId;
    }

    public void setStructureGroupId(String structureGroupId) {
        StructureGroupId = structureGroupId;
    }

    public String getUrlPath() {
        return UrlPath;
    }

    public void setUrlPath(String urlPath) {
        UrlPath = urlPath;
    }

    public com.sdl.web.pca.client.contentmodel.pagemodeldata.Meta getMeta() {
        return Meta;
    }

    public void setMeta(com.sdl.web.pca.client.contentmodel.pagemodeldata.Meta meta) {
        Meta = meta;
    }

    public List<com.sdl.web.pca.client.contentmodel.pagemodeldata.Regions> getRegions() {
        return Regions;
    }

    public void setRegions(List<com.sdl.web.pca.client.contentmodel.pagemodeldata.Regions> regions) {
        Regions = regions;
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
}
