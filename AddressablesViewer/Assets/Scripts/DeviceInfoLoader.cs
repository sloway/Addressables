using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class DeviceInfo
{
    public string category;
    public string type;
    public string name;
    public string author;
    public string license;
    public string repositoryUrl;
    public string url;
    public List<string> optionalPermissions;
    public string description;
    public string thumbnail;

    public override string ToString()
    {
        return $"{category} / {type} / {name} / {thumbnail}";
    }
}

// JsonUtility.FromJson doesn't support deserialize List type.
// So there's no other choice to wrap by this class
public class DeviceInfos
{
    public List<DeviceInfo> list;
}

public class DeviceInfoLoader
{
    private const string DeviceDescsURL = "http://127.0.0.1:5500/deviceInfo2.json";
    private IList<DeviceInfo> deviceInfos;

    public async Task LoadAsync()
    {
        var json = await DownloadJson(DeviceDescsURL);
        deviceInfos = DeserializeJson(json);
    }

    private static async Task<string> DownloadJson(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        var sendOperation = www.SendWebRequest();

        while (!sendOperation.isDone)
        {            
            await Task.Yield();
        }

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log($"Failed to load device descs from server : {www.error}");
            return "[]";
        }

        return www.downloadHandler.text;
    }

    private static IList<DeviceInfo> DeserializeJson(string json)
    {
        try
        {
            return JsonUtility.FromJson<DeviceInfos>(json).list;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return new List<DeviceInfo>();
        }
    }

    public IEnumerable<DeviceInfo> GetList()
    {
        return deviceInfos ?? new List<DeviceInfo>();
    }

    public IEnumerable<string> GetCategoryList()
    {
        var categoryList = from deviceInfo in GetList() select deviceInfo.category;
        return categoryList.Distinct<string>();
    }

    public IEnumerable<DeviceInfo> GetListByCategory(string category)
    {
        return from deviceInfo in GetList()
               where deviceInfo.category == category
               select deviceInfo;
    }

    public void Clear()
    {
        deviceInfos?.Clear();
    }
}