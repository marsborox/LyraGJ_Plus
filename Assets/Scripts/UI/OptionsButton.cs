using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.unscaledDeltaTime);
    }
}
