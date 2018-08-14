
package com.sdl.web.pca.client.contentmodel.pagemodeldata;
import java.util.List;


public class Regions {

    private String Name;
    private List<Entities> Entities;
    private Mvcdata MvcData;

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public List<com.sdl.web.pca.client.contentmodel.pagemodeldata.Entities> getEntities() {
        return Entities;
    }

    public void setEntities(List<com.sdl.web.pca.client.contentmodel.pagemodeldata.Entities> entities) {
        Entities = entities;
    }

    public Mvcdata getMvcData() {
        return MvcData;
    }

    public void setMvcData(Mvcdata mvcData) {
        MvcData = mvcData;
    }
}