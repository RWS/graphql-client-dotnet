using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sdl.Web.PublicContentApi.ContentModel;

namespace Sdl.Web.PublicContentApi.Tests
{
    [TestClass]
    public class TestContentModelSerialization : TestClass
    {      
        [TestMethod]
        public void ItemQuery1_Success()
        {
            var client = CreateClient(LoadResource("ItemQuery1_Success"));
          
            var filter = new InputItemFilter
            {
                NamespaceIds = new List<ContentNamespace> { ContentNamespace.Docs },
                ItemTypes = new List<ContentModel.ItemType> { ContentModel.ItemType.PAGE }

            };

            IContextData contextData = new ContextData();
            contextData.ClaimValues.Add(new ClaimValue
            {
                Uri = ModelServiceClaimUris.ModelType,
                Type = ClaimValueType.STRING,
                Value = Enum.GetName(typeof(ContentType), ContentType.RAW)
            });

            contextData.ClaimValues.Add(new ClaimValue
            {
                Uri = ModelServiceClaimUris.ModelType,
                Type = ClaimValueType.STRING,
                Value = Enum.GetName(typeof(DataModelType), DataModelType.R2)
            });

            contextData.ClaimValues.Add(new ClaimValue
            {
                Uri = ModelServiceClaimUris.PageIncludeRegions,
                Type = ClaimValueType.STRING,
                Value = Enum.GetName(typeof(PageInclusion), PageInclusion.INCLUDE)
            });

            ItemConnection query = client.ExecuteItemQuery(filter, null, new Pagination { First = 10 }, null, null, false);
            

            Assert.AreEqual(query.Edges.Count, 10);
        }      
    }
}
