using UnityEngine;

public class DeviceBrowser : MonoBehaviour
{    
    private DeviceInfoLoader deviceInfoLoader;
    
    async void Start()
    {
        deviceInfoLoader = new DeviceInfoLoader();                
        await deviceInfoLoader.LoadAsync();

        ShowDeviceDescs();
    }

    private void ShowDeviceDescs()
    {
        foreach (var deviceInfo in deviceInfoLoader.GetList())
        {
            Debug.Log(deviceInfo.ToString());
        }

        foreach (var deviceInfo in deviceInfoLoader.GetListByCategory("Appliance2"))
        {
            Debug.Log(deviceInfo.ToString());
        }

        foreach (var deviceInfo in deviceInfoLoader.GetCategoryList())
        {
            Debug.Log(deviceInfo.ToString());
        }
    }
}
