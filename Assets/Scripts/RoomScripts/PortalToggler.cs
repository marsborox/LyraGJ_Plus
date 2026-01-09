using UnityEngine;

public class PortalToggler : MonoBehaviour
{
    [SerializeField] private bool enable;
    [SerializeField] private Animator portalAnimator;
    [SerializeField] private CircleCollider2D portalCollider;
    [SerializeField] private HueSatApplier portalHueSatApplier;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (enable)
        {            
            portalHueSatApplier.hue = 0.5f;
            portalHueSatApplier.saturation = 1;
        }
        else
        {
            portalHueSatApplier.hue = 0;
            portalHueSatApplier.saturation = 0;
        }

        portalHueSatApplier.lightness = 1f;
        portalCollider.isTrigger = enable;
        portalAnimator.enabled = enable;

        portalHueSatApplier.Apply();
    }
}
