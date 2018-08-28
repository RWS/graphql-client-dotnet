package com.sdl.web.pca.client.contentmodel;

import java.util.List;

/**
*Represents a container for items.
*/
public interface Container{
		List<Item> containerItems = null;


		List<Item> getContainerItems();
		void setContainerItems(List<Item> containerItems);	
}
