﻿using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public T Instance<T>(string path, Vector3 at, Quaternion rotation)
            where T : Object
        {
            var prefab = LoadPrefab<T>(path);
            return Object.Instantiate(prefab, at,rotation);
        }
        public T Instance<T>(string path, Transform parent)
            where T : Object
        {
            var prefab = LoadPrefab<T>(path);
            return Object.Instantiate(prefab, parent);
        }
        public T Instance<T>(string path) where T : Object
        {
            var prefab = LoadPrefab<T>(path);
            return Object.Instantiate(prefab);
        }

        private T LoadPrefab<T>(string path)
                        where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}
