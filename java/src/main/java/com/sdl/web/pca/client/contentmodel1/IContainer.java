package com.sdl.web.pca.client.contentmodel1;

import java.util.List;

/// <summary>
	/// Represents a container for items.
	/// </summary>
	interface IContainer
	{
		List<IItem> containerItems = null;


		List<IItem> getContainerItems();
		void setContainerItems(List<IItem> containerItems);
	}
