package com.sdl.web.pca.client.contentmodel;

/// <summary>
/// Criteria for Custom Meta
/// </summary>
class InputCustomMetaCriteria
{
    private String key;
    private CriteriaScope scope;
    private String value;
    private CustomMetaValueType valueType;

     public String getKey()
     {
         return key;
     }
     public void setKey(String key)
     {
         this.key = key;
     }

     public CriteriaScope getScope()
     {
         return scope;
     }
     public void setScope(CriteriaScope scope)
     {
         this.scope = scope;
     }

     public String getValue()
     {
         return value;
     }
     public void setValue(String value)
     {
         this.value = value;
     }

     public CustomMetaValueType getValueType()
     {
         return valueType;
     }
     public void setValueType(CustomMetaValueType valueType)
     {
         this.valueType = valueType;
     }
}
