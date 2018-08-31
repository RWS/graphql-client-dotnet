package com.sdl.web.pca.client.contentmodel;

class InputKeywordCriteria
{
    private int categoryId;
    private String categoryName;
    private String key;
    private int keywordId;

     public int getCategoryId()
     {
         return categoryId;
     }
     public void setCategoryId(int categoryId)
     {
         this.categoryId = categoryId;
     }

     public String getCategoryName()
     {
         return categoryName;
     }
     public void setCategoryName(String categoryName)
     {
         this.categoryName = categoryName;
     }

     public String getKey()
     {
         return key;
     }
     public void setKey(String key)
     {
         this.key = key;
     }

     public int getKeywordId()
     {
         return keywordId;
     }
     public void setKeywordId(int keywordId)
     {
         this.keywordId = keywordId;
     }
}
