using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Infrastructure.Core
{
    public class JsonTextSerializer : ITextSerializer
    {
        private readonly JsonSerializer _serializer;

        public JsonTextSerializer()
            : this(JsonSerializer.Create(new JsonSerializerSettings
            {
                // Allows deserializing to the actual runtime type
                TypeNameHandling = TypeNameHandling.All,
                // In a version resilient way
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            }))
        {
        }

        public JsonTextSerializer(JsonSerializer serializer)
        {
            this._serializer = serializer;
        }

        public string Serialize(object objectToSerialize, bool includePrivateProperties = true)
        {
            if (objectToSerialize == null)
            {
                return null;
            }

            SetupToIncludePrivateProperties(includePrivateProperties);

            var stringWriter = new StringWriter();
            var jsonWriter = new JsonTextWriter(stringWriter);
#if DEBUG
            jsonWriter.Formatting = Formatting.Indented;
#endif
            _serializer.Serialize(jsonWriter, objectToSerialize);
            stringWriter.Flush();
            return stringWriter.ToString();
        }


        public object Deserialize(string payload, bool includePrivateProperties = true)
        {
            try
            {
                SetupToIncludePrivateProperties(includePrivateProperties);
                using (var reader = new StringReader(payload))
                {
                    var jsonReader = new JsonTextReader(reader);
                    return _serializer.Deserialize(jsonReader);
                }
            }
            catch (JsonSerializationException e)
            {
                // Wrap in a standard .NET exception.
                throw new SerializationException(e.Message, e);
            }
        }

        private void SetupToIncludePrivateProperties(bool includePrivateProperties)
        {
            if (includePrivateProperties)
            {
                this._serializer.ContractResolver = new PrivateSetterResolver();
            }
        }
    }

    public class PrivateSetterResolver : DefaultContractResolver
    {
        //protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        //{
        //    var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        //    MemberInfo[] fields = objectType.GetFields(flags);
        //    var allFields = fields
        //        .Concat(objectType.GetProperties(flags).Where(propInfo => propInfo.CanWrite))
        //        .ToList();
        //    return allFields;
        //}

        //protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        //{
        //    var props = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        //        .Select(p => base.CreateProperty(p, memberSerialization))
        //        .Union(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        //            .Select(f => base.CreateProperty(f, memberSerialization)))
        //        .ToList();
        //    props.ForEach(p => { p.Writable = true; p.Readable = true; });
        //    return props;
        //}

        protected override JsonProperty CreateProperty(
            MemberInfo member,
            MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);

            if (!prop.Writable)
            {
                var property = member as PropertyInfo;
                if (property != null)
                {
                    var hasPrivateSetter = property.GetSetMethod(true) != null;
                    prop.Writable = hasPrivateSetter;
                }
            }

            return prop;
        }
    }
}
