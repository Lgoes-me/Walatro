using System;
using System.Collections.Generic;
using System.Reflection;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
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
}