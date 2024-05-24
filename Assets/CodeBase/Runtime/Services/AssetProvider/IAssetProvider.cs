using UnityEngine;

public interface IAssetProvider
{
    T Load<T>(string path) where T : Object;
}
