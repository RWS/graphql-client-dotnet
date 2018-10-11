package com.sdl.web.pca.client.contentmodel.generated;

import java.util.List;

/**
*Filter for the componentPresentation root query. When the filter is applied, only the component presentations matching the filter will be in the query result
*/
public class InputComponentPresentationFilter {
		private List<InputComponentPresentationFilter> and;
		private InputCustomMetaCriteria customMeta;
		private InputDateRangeCriteriaImpl dateRange;
		private InputKeywordCriteria keyword;
		private List<InputComponentPresentationFilter> or;
		private InputSchemaCriteria schema;
		private InputTemplateCriteria template;


		public List<InputComponentPresentationFilter> getAnd(){
			return and;
		}
		public void setAnd(List<InputComponentPresentationFilter> and){
			this.and = and;
		}


		public InputCustomMetaCriteria getCustomMeta(){
			return customMeta;
		}
		public void setCustomMeta(InputCustomMetaCriteria customMeta){
			this.customMeta = customMeta;
		}


		public InputDateRangeCriteriaImpl getDateRange(){
			return dateRange;
		}
		public void setDateRange(InputDateRangeCriteriaImpl dateRange){
			this.dateRange = dateRange;
		}


		public InputKeywordCriteria getKeyword(){
			return keyword;
		}
		public void setKeyword(InputKeywordCriteria keyword){
			this.keyword = keyword;
		}


		public List<InputComponentPresentationFilter> getOr(){
			return or;
		}
		public void setOr(List<InputComponentPresentationFilter> or){
			this.or = or;
		}


		public InputSchemaCriteria getSchema(){
			return schema;
		}
		public void setSchema(InputSchemaCriteria schema){
			this.schema = schema;
		}


		public InputTemplateCriteria getTemplate(){
			return template;
		}
		public void setTemplate(InputTemplateCriteria template){
			this.template = template;
		}
	
}
