using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ContentService : MonoBehaviour, IService, IPreparable
{
    [field: SerializeField] public List<GameBoxScriptableObject> GameBoxes { get; set; }
    [field: SerializeField] public List<GameDiscScriptableObject> GameDiscs { get; set; }

    [field: SerializeField] public List<ModifierDataAssetReference> Modifiers { get; private set; }
    
    
    private Dictionary<string, ModifierDataAssetReference> AvailableModifiers { get; set; }
    private Dictionary<string, GameBoxScriptableObject> AvailableGameBoxes { get; set; }
    private Dictionary<string, GameDiscScriptableObject> AvailableGameDiscs { get; set; }

    public UniTask Prepare()
    {
        AvailableGameBoxes = GameBoxes.ToIdentifiableDictionary();
        AvailableGameDiscs = GameDiscs.ToIdentifiableDictionary();

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

        throw new Exception("Invalid Modifier");
    }

    public async UniTask<GameDisc> GetGameDiscsFromModel(GameDiscModel gameDiscModel)
    {
        if (AvailableGameDiscs.TryGetValue(gameDiscModel.Id, out var gameDiscScriptableObject))
        {
            return gameDiscScriptableObject.ToDomain();
        }

        throw new Exception("Invalid gameDiscModel");
    }

#if UNITY_EDITOR
    [ContextMenu("ForceLoadContent")]
    private void ForceLoadContent()
    {
        //Modifiers = Extensions.FindAllScriptableObjectsOfType<ModifierData>("Assets/Datas/Modifiers");

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
#endif
}