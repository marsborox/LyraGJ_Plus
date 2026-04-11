using UnityEngine;
using UnityEngine.Rendering.Universal;

[ExecuteAlways]

public class SpotlightFollowing : MonoBehaviour
{
    public GameObject lightCone;
    public GameObject lightSpot;
    public Color lightColor = new Color(1, 0, 0);
    public Transform targetToFollow;
    public Vector2 offscreenOrigin = new Vector2(0, 0);

    private Light2D light;
    private SpriteRenderer coneRenderer;
    private SpriteRenderer spotRenderer;
    private MaterialPropertyBlock propertyBlock;
    private Color lastColor;

    void OnEnable()
    {
        light = GetComponent<Light2D>();

        if (lightCone != null) {
            coneRenderer = lightCone.GetComponent<SpriteRenderer>();
        }
        if (lightSpot != null) {
            spotRenderer = lightSpot.GetComponent<SpriteRenderer>();
        }
        propertyBlock = new MaterialPropertyBlock();
    }
    void Update()
    {
        FollowTarget();

        if (lightCone == null || coneRenderer == null || lightSpot == null || spotRenderer == null) return;
        
        Vector2 spotPos = transform.position;
        
        // calculate direction from offscreen origin to spotlight
        Vector2 direction = spotPos - offscreenOrigin;
        float distance = direction.magnitude;
        
        // position at the offscreen origin point first
        lightCone.transform.position = offscreenOrigin;
        
        // rotate to point from origin to spotlight
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        lightCone.transform.rotation = Quaternion.Euler(0, 0, angle - 90); // -90 if cone points "up" in sprite
        
        // scale length to match distance (keep width unchanged)
        float originalHeight = coneRenderer.sprite.bounds.size.y;
        float originalWidth = coneRenderer.sprite.bounds.size.x;
        Vector3 scale = lightCone.transform.localScale;
        scale.y = distance / originalHeight;

        if (light != null)
        {
            // calculate correct width for cone and spot
            float targetWidth = light.pointLightOuterRadius * 2f; // diameter
            scale.x = targetWidth / originalWidth;

            lightSpot.transform.localScale = new Vector2(targetWidth / spotRenderer.sprite.bounds.size.x, targetWidth / spotRenderer.sprite.bounds.size.y);

            // to not call it each Update pass
            if (lightColor != lastColor)
            {
                lastColor = lightColor;
                UpdateColors();
            }
        }
        
        lightCone.transform.localScale = scale;
    }
    private void UpdateColors()
    {
        if (lightCone == null || coneRenderer == null || lightSpot == null || spotRenderer == null) return;

        if (light != null)
        {
            // match the colors
            Color color = lightColor;
            color.a = 0.1f;
            coneRenderer.color = color;
            spotRenderer.color = color;

            // modify even shader colors
            propertyBlock.SetColor("_Color", color);
            coneRenderer.SetPropertyBlock(propertyBlock);
            spotRenderer.SetPropertyBlock(propertyBlock);
        }
    }
    private void FollowTarget()
    {
        if (targetToFollow == null) return;

        transform.position = Vector3.Lerp(transform.position, targetToFollow.position, 30f * Time.unscaledDeltaTime);
    }
}