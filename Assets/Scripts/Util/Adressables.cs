using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class ModifierDataAssetReference : AssetReferenceT<ModifierData>
{
    public ModifierDataAssetReference(string guid) : base(guid) { }
}

