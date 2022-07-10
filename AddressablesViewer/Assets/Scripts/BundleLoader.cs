using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Linq;

public class BundleLoader : MonoBehaviour
{
    private async void Start()
    {
        var catalogPath = "http://127.0.0.1:5500/StandaloneWindows64/catalog_2022.07.10.06.57.36.json";
        var key = "Workbench";
        await LoadBundlePrefab(catalogPath, key);
    }

    private async Task LoadBundlePrefab(string catalogPath, string key)
    {
        await Addressables.LoadContentCatalogAsync(catalogPath).Task;

        var prefabs = await Addressables.LoadResourceLocationsAsync(key, typeof(GameObject)).Task;

        var prefab = prefabs.First(item => item.InternalId.EndsWith(".prefab"));

        Debug.Log(prefab);

        await Addressables.InstantiateAsync(prefab).Task;

        Debug.Log("Done");
    }

}
