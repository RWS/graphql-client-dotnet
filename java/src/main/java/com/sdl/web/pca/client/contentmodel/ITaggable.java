package com.sdl.web.pca.client.contentmodel;

import java.util.List;

/// <summary>
	/// Represents a related set of entities.
	/// </summary>
	interface ITaggable
	{
		List<ITaxonomyItem> taxonomies = null;


		List<ITaxonomyItem> getTaxonomies();
		void setTaxonomies(List<ITaxonomyItem> taxonomies);
	}
