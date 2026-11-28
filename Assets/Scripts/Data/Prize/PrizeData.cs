using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class PrizeData<T> : ScriptableObject, IIdentifiable where T : IPrize
{
    [field: SerializeField] public string Id { get; private set; }

    public abstract T ToDomain(); 

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(Id))
            return;

        if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out var guid, out _))
        {
            Id = guid;
        }
    }
#endif
}