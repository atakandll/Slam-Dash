using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Runtime.Custom
{
    [Serializable]
    public class AssetReferenceScriptableObject : AssetReferenceT<ScriptableObject>
    {
        public AssetReferenceScriptableObject(string guid) : base(guid)
        {
        }
    }
}