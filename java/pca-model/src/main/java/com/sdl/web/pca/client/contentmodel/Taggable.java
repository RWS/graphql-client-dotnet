package com.sdl.web.pca.client.contentmodel;

import java.util.List;

/**
*Represents a related set of entities.
*/
public interface Taggable{
		

		List<TaxonomyItem> getTaxonomies();
		void setTaxonomies(List<TaxonomyItem> taxonomies);	
}
