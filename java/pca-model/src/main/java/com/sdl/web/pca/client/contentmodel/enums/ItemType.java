package com.sdl.web.pca.client.contentmodel.enums;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.sdl.web.pca.client.contentmodel.ItemTypes;

public enum ItemType {
	//pragma warning disable 1591 // Disable missing XML comment warning

	None(0x0),
	Publication(0x1),
	Folder(0x2),
	StructureGroup(0x4),
	Schema(0x8),
	Component(0x10),
	ComponentTemplate(0x20),
	Page(0x40),
	PageTemplate(0x80),
	TargetGroup(0x100),
	Category(0x200),
	Keyword(0x400),
	TemplateBuildingBlock(0x800),
	VirtualFolder(0x2000),
	PublicationTarget(ItemTypes.baseSystemWideItems + 0x1),
	TargetType(ItemTypes.baseSystemWideItems + 0x2),
	TargetDestination(ItemTypes.baseSystemWideItems + 0x4),
	MultimediaType(ItemTypes.baseSystemWideItems + 0x8),
	User(ItemTypes.baseSystemWideItems + 0x10),
	Group(ItemTypes.baseSystemWideItems + 0x20),
	DirectoryService(ItemTypes.baseSystemWideItems + 0x80),
	DirectoryGroupMapping(ItemTypes.baseSystemWideItems + 0x100),
	Batch(ItemTypes.baseSystemWideItems + 0x200);

	private int itemType;

	ItemType(int itemType){
		this.itemType = itemType;
	}

	@JsonCreator
	public int getNameSpaceValue(){
		return this.itemType;
	}

	//pragma warning restore 1591
}
