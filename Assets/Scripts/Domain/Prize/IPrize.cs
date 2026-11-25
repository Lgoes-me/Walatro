using UnityEngine;
using Cysharp.Threading.Tasks;

public interface IPrize: IInfoDescriptor
{
    Texture Texture { get; }
    Rarity Rarity { get; }
}