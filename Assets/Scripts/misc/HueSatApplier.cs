using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HueSatApplier : MonoBehaviour
{
    public SpriteRenderer[] spriteRenderers;
    public TilemapRenderer[] tilemapRenderers;
    public Image[] images;

    [Range(0, 1)] public float hue = 0f;
    [Range(0, 2)] public float saturation = 1f;

    [Range(0, 2)] public float lightness = 1f;

    MaterialPropertyBlock _mpb;

    private void OnEnable() => Apply();
    private void OnValidate() => Apply();  // live editor updates

    public void Apply()
    {
        if (_mpb == null)
            _mpb = new MaterialPropertyBlock();

        // SPRITE RENDERERS
        foreach (var sr in spriteRenderers)
        {
            if (!sr) continue;
            sr.GetPropertyBlock(_mpb);
            _mpb.SetFloat("_Hue", hue);
            _mpb.SetFloat("_Saturation", saturation);
            _mpb.SetFloat("_Lightness", lightness);
            sr.SetPropertyBlock(_mpb);
        }

        // TILEMAP RENDERERS
        foreach (var tr in tilemapRenderers)
        {
            if (!tr) continue;
            tr.GetPropertyBlock(_mpb);
            _mpb.SetFloat("_Hue", hue);
            _mpb.SetFloat("_Saturation", saturation);
            _mpb.SetFloat("_Lightness", lightness);
            tr.SetPropertyBlock(_mpb);
        }

        // UI IMAGES
        foreach (var im in images)
        {
            if (!im) continue;
            im.material.SetFloat("_Hue", hue);
            im.material.SetFloat("_Saturation", saturation);
            im.material.SetFloat("_Lightness", lightness);
        }
    }
}