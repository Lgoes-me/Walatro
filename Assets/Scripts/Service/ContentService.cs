using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ContentService : MonoBehaviour, IService, IPreparable
{
    [field: SerializeField] public List<GameBoxDataAssetReference> GameBoxes { get; private set; }
    [field: SerializeField] public List<GameDiscDataAssetReference> GameDiscs { get; private set; }
    [field: SerializeField] public List<ModifierDataAssetReference> Modifiers { get; private set; }
    [field: SerializeField] public List<ItemDataAssetReference> Items { get; private set; }
    [field: SerializeField] public List<TarotDataAssetReference> Tarots { get; private set; }
    [field: SerializeField] public List<PlanetDataAssetReference> Planets { get; private set; }
    
    private Dictionary<string, GameBoxDataAssetReference> AvailableGameBoxes { get; set; }
    private Dictionary<string, GameDiscDataAssetReference> AvailableGameDiscs { get; set; }
    private Dictionary<string, ModifierDataAssetReference> AvailableModifiers { get; set; }
    private Dictionary<string, ItemDataAssetReference> AvailableItems { get; set; }
    private Dictionary<string, TarotDataAssetReference> AvailableTarots { get; set; }
    private Dictionary<string, PlanetDataAssetReference> AvailablePlanets { get; set; }

    public UniTask Prepare()
    {
        AvailableGameBoxes = GameBoxes.ToAssetReferenceDictionary();
        AvailableGameDiscs = GameDiscs.ToAssetReferenceDictionary();
        AvailableModifiers = Modifiers.ToAssetReferenceDictionary();
        AvailableItems = Items.ToAssetReferenceDictionary();
        AvailableTarots = Tarots.ToAssetReferenceDictionary();
        AvailablePlanets = Planets.ToAssetReferenceDictionary();

        return UniTask.CompletedTask;
    }
    
    public async UniTask<Modifier> GetModifierFromModel(ModifierModel modifierModel)
    {
        if (AvailableModifiers.TryGetValue(modifierModel.Id, out var modifierDataAsset))
        {
            var modifierData = await modifierDataAsset.LoadAssetAsyncIfValid();
            return modifierData.ToDomain(modifierModel);
        }

        throw new Exception("Invalid Modifier");
    }

    public async UniTask<List<GameDisc>> GetGameBoxFromId(string id)
    {
        if (AvailableGameBoxes.TryGetValue(id, out var gameBoxDataAsset))
        {
            var gameBoxData = await gameBoxDataAsset.LoadAssetAsyncIfValid();
            return gameBoxData.ToDomain();
        }

        throw new Exception("Invalid gameBox");
    }


    public async UniTask<GameDisc> GetGameDiscsFromModel(GameDiscModel gameDiscModel)
    {
        if (AvailableGameDiscs.TryGetValue(gameDiscModel.Id, out var gameDiscDataAsset))
        {
            var gameDiscData = await gameDiscDataAsset.LoadAssetAsyncIfValid();
            return gameDiscData.ToDomain(gameDiscModel);
        }

        throw new Exception("Invalid gameDisc");
    }

#if UNITY_EDITOR
    [ContextMenu("ForceLoadContent")]
    private void ForceLoadContent()
    {
        GameBoxes = Extensions.LoadAllAdressablesOfType<GameBoxDataAssetReference, GameBoxData>("Assets/Datas/Boxes");
        GameDiscs = Extensions.LoadAllAdressablesOfType<GameDiscDataAssetReference, GameDiscData>("Assets/Datas/Discs");
        Modifiers = Extensions.LoadAllAdressablesOfType<ModifierDataAssetReference, ModifierData>("Assets/Datas/Modifiers");
        Items = Extensions.LoadAllAdressablesOfType<ItemDataAssetReference, ItemData>("Assets/Datas/Items");
        Tarots = Extensions.LoadAllAdressablesOfType<TarotDataAssetReference, TarotData>("Assets/Datas/Tarots");
        Planets = Extensions.LoadAllAdressablesOfType<PlanetDataAssetReference, PlanetData>("Assets/Datas/Planets");

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
#endif
}