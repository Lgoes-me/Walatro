using System;
using System.Collections.Generic;
using System.Reflection;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class Extensions
{
    public static List<KeyValuePair<int, List<T>>> PrepareServices<T>(this Dictionary<Type, object> applicationServices, string methodName)
    {
        var servicesDict = new Dictionary<int, List<T>>();

        foreach (var (_, service) in applicationServices)
        {
            if (service is not T typedInterface)
                continue;

            var type = typedInterface.GetType();
            var method = type.GetMethod(methodName);
            var attribute = method?.GetCustomAttribute<CallbackOrderAttribute>();
            
            var priority = attribute?.CallbackOrder ?? 1;

            if (servicesDict.ContainsKey(priority))
            {
                servicesDict[priority].Add(typedInterface);
            }
            else
            {
                servicesDict.Add(priority, new List<T>() { typedInterface });
            }
        }

        var services = new List<KeyValuePair<int, List<T>>>(servicesDict);

        services.Sort((pair1, pair2) => pair1.Key.CompareTo(pair2.Key));
        
        return services;
    }
    
    public static Dictionary<string, T> ToAssetReferenceDictionary<T>(this List<T> list) where T : AssetReference
    {
        var dictionary = new Dictionary<string, T>();

        foreach (var item in list)
        {
            dictionary.Add(item.AssetGUID, item);
        }
        
        return dictionary;
    }

    public static async UniTask<TObject> LoadAssetAsyncIfValid<TObject>(this AssetReferenceT<TObject> assetReference) where TObject : Object
    {
        AsyncOperationHandle op = assetReference.OperationHandle;
        TObject result;

        if (assetReference.IsValid() && op.IsValid())
        {
            // Increase the usage counter & Convert.
            Addressables.ResourceManager.Acquire(op);
            result = await op.Convert<TObject>();
        }
        else
        {
            result = await assetReference.LoadAssetAsync<TObject>();
        }

        return result;
    }

}