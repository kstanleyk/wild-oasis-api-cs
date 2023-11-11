using Newtonsoft.Json;

namespace Core.Common.Core;

public static class JsonPropertyMapper<TSrc, TDest>
    where TSrc : class
    where TDest : class
{
    public static TDest PropertyMap(TSrc source)
    {
        return JsonConvert.DeserializeObject<TDest>(JsonConvert.SerializeObject(source, Formatting.Indented, new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.All
        }));
    }

    public static TSrc PropertyMap(TDest destination)
    {
        return JsonConvert.DeserializeObject<TSrc>(JsonConvert.SerializeObject(destination, Formatting.Indented, new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.All
        }));
    }
}

public class JsonMapper<TSrc, TDest>
    where TSrc : class
    where TDest : class
{
    public TDest CreateObject(TSrc source)
    {
        return JsonConvert.DeserializeObject<TDest>(JsonConvert.SerializeObject(source, Formatting.Indented, new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.All
        }));
    }

    public TSrc CreateObject(TDest destination)
    {
        return JsonConvert.DeserializeObject<TSrc>(JsonConvert.SerializeObject(destination, Formatting.Indented, new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.All
        }));
    }
}