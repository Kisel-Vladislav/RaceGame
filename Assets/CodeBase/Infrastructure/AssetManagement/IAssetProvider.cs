﻿using Assets.CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        T Instance<T>(string prefabPath)
            where T : Object;
        T Instance<T>(string prefabPath, Vector3 at, Quaternion rotation) 
            where T : Object;
        T Instance<T>(string path, Transform parent)
            where T : Object;
    }
}