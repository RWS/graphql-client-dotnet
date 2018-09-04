package com.sdl.web.pca.client.contentmodel.generated;


/**
*Represents a typical sort type. When used, the default sorting mechanism (by namespace and by id) is overridden
*/
public class InputSortParam {
		private SortOrderType order;
		private SortFieldType sortBy;


		public SortOrderType getOrder(){
			return order;
		}
		public void setOrder(SortOrderType order){
			this.order = order;
		}


		public SortFieldType getSortBy(){
			return sortBy;
		}
		public void setSortBy(SortFieldType sortBy){
			this.sortBy = sortBy;
		}
	
}
