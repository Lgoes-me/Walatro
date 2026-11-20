using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class GameDiscCotroller : MonoBehaviour
{
    [field: SerializeField] private Material Material { get; set; }
    [field: SerializeField] private DecalProjector DecalProjector { get; set; }

    private GameDisc GameDisc { get; set; }

    public void Show(GameDisc gameDisc)
    {
        GameDisc = gameDisc;
        
        var instancedMaterial = new Material(Material);
        DecalProjector.material = instancedMaterial;
        DecalProjector.material.SetTexture("Base_Map", GameDisc.Texture); 

        GameDisc.GameDiscTriggered += GiveFeedback;

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        Destroy(DecalProjector.material);

        GameDisc.GameDiscTriggered -= GiveFeedback;
        GameDisc = null;
    }

    private void GiveFeedback()
    {
        transform.DOShakePosition(0.15f, 0.15f);
    }
}
