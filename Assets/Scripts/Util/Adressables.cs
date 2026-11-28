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

[Serializable]
public class ItemDataAssetReference : AssetReferenceT<ItemData>
{
    public ItemDataAssetReference(string guid) : base(guid) { }
}

[Serializable]
public class TarotDataAssetReference : AssetReferenceT<TarotData>
{
    public TarotDataAssetReference(string guid) : base(guid) { }
}

[Serializable]
public class PlanetDataAssetReference : AssetReferenceT<PlanetData>
{
    public PlanetDataAssetReference(string guid) : base(guid) { }
}