using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class VegetationJustWind : MonoBehaviour
{
    [SerializeField] private float yOffset = 0f;

    private Renderer rend;
    private MaterialPropertyBlock block;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        block = new MaterialPropertyBlock();
        block.SetFloat("_BaseYValue", transform.localPosition.y + yOffset);
    }
}