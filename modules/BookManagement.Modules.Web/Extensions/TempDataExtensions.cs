using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BookManagement.Modules.Web.Extensions;

public static class TempDataExtensions
{
    public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
    {
        tempData.TryAdd(key, JsonConvert.SerializeObject(value));
    }

    public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
    {
        tempData.TryGetValue(key, out var obj);
        return obj == null ? null : JsonConvert.DeserializeObject<T>((string) obj);
    }
}