using System;
using System.Dynamic;
using Newtonsoft.Json;
using Sdl.Web.PublicContentApi.ContentModel;

namespace Sdl.Web.PublicContentApi
{
    public class ItemConvertor : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            try
            {
                var r = (dynamic)serializer.Deserialize(reader, typeof(ExpandoObject));
                int itemType = (int)r.itemType;
                switch (itemType)
                {
                    case (int)ItemType.Publication:
                        return JsonConvert.DeserializeObject<Publication>(JsonConvert.SerializeObject(r));
                    case (int)ItemType.Component:
                        return JsonConvert.DeserializeObject<Component>(JsonConvert.SerializeObject(r));
                    case (int)ItemType.Keyword:
                        return JsonConvert.DeserializeObject<Keyword>(JsonConvert.SerializeObject(r));
                    case (int)ItemType.Page:
                        return JsonConvert.DeserializeObject<Page>(JsonConvert.SerializeObject(r));
                    case (int)ItemType.StructureGroup:
                        return JsonConvert.DeserializeObject<StructureGroup>(JsonConvert.SerializeObject(r));
                    case (int)ItemType.ComponentTemplate:
                        return JsonConvert.DeserializeObject<Template>(JsonConvert.SerializeObject(r));
                }
            }
            catch
            {
            }
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IItem) == objectType;
        }
    }
}
