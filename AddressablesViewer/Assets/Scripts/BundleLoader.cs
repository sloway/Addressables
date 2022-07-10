using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Linq;
using System.Reflection;
using System.IO;
using System;

public class BundleLoader : MonoBehaviour
{
    Type myComponent;
    private void Start()
    {
        var dllPath = "d:\\AddressablesDLL.dll";
        ListUp(AddAssemblyToDomain(dllPath));
    }
        
    Assembly AddAssemblyToDomain(string AssemblyPath)
    {
        byte[] rawAssembly = LoadFile(AssemblyPath);
        return AppDomain.CurrentDomain.Load(rawAssembly);
    }
    byte[] LoadFile(string filename)
    {
        var fs = new FileStream(filename, FileMode.Open);
        byte[] buffer = new byte[(int)fs.Length];
        fs.Read(buffer, 0, buffer.Length);
        fs.Close();
        return buffer;
    }

    void ListUp(Assembly assembly)
    {
        foreach(var type in assembly.GetTypes())
        {
            if(type.Name == "MyComponent")
            {
                myComponent = type;
            }
            Debug.Log(type);
        }      
    }

    public async void OnClick()
    {
        await LoadBundlePrefab();
    }

    public async Task LoadBundlePrefab()
    {
        var catalogPath = "http://127.0.0.1:5500/StandaloneWindows64/catalog_2022.07.10.07.31.10.json";
        var key = "Workbench";

        await Addressables.LoadContentCatalogAsync(catalogPath).Task;
        var prefabs = await Addressables.LoadResourceLocationsAsync(key, typeof(GameObject)).Task;
        var prefab = prefabs.First(item => item.InternalId.EndsWith(".prefab"));

        Debug.Log(prefab);

        var gameObject = await Addressables.InstantiateAsync(prefab).Task;
        gameObject.AddComponent(myComponent);

        Debug.Log("Done");
    }



}
