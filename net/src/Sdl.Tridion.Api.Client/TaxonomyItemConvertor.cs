using System;
using Newtonsoft.Json;
using Sdl.Tridion.Api.Client.ContentModel;

namespace Sdl.Tridion.Api.Client
{
    public class TaxonomyItemConvertor : JsonConverter
    {
        public override bool CanConvert(Type objectType) 
            => typeof (ISitemapItem).IsAssignableFrom(objectType);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var result = new TaxonomySitemapItem();
            try
            {
                var target = serializer.Deserialize<Newtonsoft.Json.Linq.JObject>(reader);
                if (target != null)
                {
                    serializer.Populate(target.CreateReader(), result);
                    switch (result.Type)
                    {
                        case "Page":
                            PageSitemapItem pageItem = new PageSitemapItem();
                            serializer.Populate(target.CreateReader(), pageItem);
                            return pageItem;
                    }
                }
            }
            catch
            {
                // ignore
            }
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }   
}
