using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalController : MonoBehaviour
{
    [field: SerializeField] private DecalProjector DecalProjector { get; set; }

    public void Show(Modifier modifier)
    {
        DecalProjector.material = modifier.Material;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
