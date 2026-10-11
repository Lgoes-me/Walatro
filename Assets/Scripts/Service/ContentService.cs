using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ContentService : MonoBehaviour, IService, IPreparable
{
    [field: SerializeField] public List<ModifierDataAssetReference> Modifiers { get; private set; }
    
    private Dictionary<string, ModifierDataAssetReference> AvailableModifiers { get; set; }

    public UniTask Prepare()
    {
        AvailableModifiers = Modifiers.ToAssetReferenceDictionary();
        return UniTask.CompletedTask;
    }
    
    public async UniTask<Modifier> GetModifierFromInfo(ModifierInfo info)
    {
        if (AvailableModifiers.TryGetValue(info.Id, out var modifierDataAsset))
        {
            var modifierData = await modifierDataAsset.LoadAssetAsyncIfValid();
            return modifierData.ToDomain(info);
        }

        throw new Exception("Invalid Modifier");
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