using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ContentService : MonoBehaviour, IService, IPreparable
{
    [field: SerializeField] public List<GameBoxDataAssetReference> GameBoxes { get; set; }
    [field: SerializeField] public List<GameDiscDataAssetReference> GameDiscs { get; set; }
    [field: SerializeField] public List<ModifierDataAssetReference> Modifiers { get; private set; }
    
    private Dictionary<string, GameBoxDataAssetReference> AvailableGameBoxes { get; set; }
    private Dictionary<string, GameDiscDataAssetReference> AvailableGameDiscs { get; set; }
    private Dictionary<string, ModifierDataAssetReference> AvailableModifiers { get; set; }

    public UniTask Prepare()
    {
        AvailableGameBoxes = GameBoxes.ToAssetReferenceDictionary();
        AvailableGameDiscs = GameDiscs.ToAssetReferenceDictionary();
        AvailableModifiers = Modifiers.ToAssetReferenceDictionary();

        return UniTask.CompletedTask;
    }
    
    public async UniTask<Modifier> GetModifierFromModel(ModifierModel info)
    {
        if (AvailableModifiers.TryGetValue(info.Id, out var modifierDataAsset))
        {
            var modifierData = await modifierDataAsset.LoadAssetAsyncIfValid();
            return modifierData.ToDomain(info);
        }

        throw new Exception("Invalid Modifier info");
    }

    public async UniTask<GameBox> GetGameBoxFromId(string id)
    {
        if (AvailableGameBoxes.TryGetValue(id, out var gameBoxDataAsset))
        {
            var gameBoxData = await gameBoxDataAsset.LoadAssetAsyncIfValid();
            return gameBoxData.ToDomain();
        }

        throw new Exception("Invalid gameBox id");
    }


    public async UniTask<GameDisc> GetGameDiscsFromModel(GameDiscModel gameDiscModel)
    {
        if (AvailableGameDiscs.TryGetValue(gameDiscModel.Id, out var gameDiscDataAsset))
        {
            var gameDiscData = await gameDiscDataAsset.LoadAssetAsyncIfValid();
            return gameDiscData.ToDomain();
        }

        throw new Exception("Invalid gameDiscModel");
    }

#if UNITY_EDITOR
    [ContextMenu("ForceLoadContent")]
    private void ForceLoadContent()
    {
        GameBoxes = Extensions.LoadAllAdressablesOfType<GameBoxDataAssetReference, GameBoxData>("Assets/Datas/Boxes");
        GameDiscs = Extensions.LoadAllAdressablesOfType<GameDiscDataAssetReference, GameDiscData>("Assets/Datas/Discs");
        Modifiers = Extensions.LoadAllAdressablesOfType<ModifierDataAssetReference, ModifierData>("Assets/Datas/Modifiers");

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
#endif
}