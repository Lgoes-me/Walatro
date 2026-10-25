using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class ModifierDataAssetReference : AssetReferenceT<ModifierData>
{
    public ModifierDataAssetReference(string guid) : base(guid) { }
}

[Serializable]
public class GameBoxDataAssetReference : AssetReferenceT<GameBoxData>
{
    public GameBoxDataAssetReference(string guid) : base(guid) { }
}

[Serializable]
public class GameDiscDataAssetReference : AssetReferenceT<GameDiscData>
{
    public GameDiscDataAssetReference(string guid) : base(guid) { }
}

