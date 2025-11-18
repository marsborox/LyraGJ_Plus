using UnityEngine;

public class ProximityMusic : MonoBehaviour
{
    public Transform player;
    public AudioSource audioSource;
    
    [Header("Distance Settings")]
    public float maxDistance = 10f;
    public float minDistance = 1f;
    
    [Header("Volume Settings")]
    public float minVolume = 0f;
    
    [Header("Fade Settings")]
    public float fadeSpeed = 2f; // How quickly volume changes
    
    private float targetVolume;
    
    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        
        if (audioSource != null)
        {
            audioSource.loop = true;
            audioSource.volume = minVolume;
            audioSource.Play();
        }
    }
    
    void Update()
    {
        if (player == null || audioSource == null) return;
        
        // Calculate distance to player
        float distance = Vector3.Distance(transform.position, player.position);
        
        // Calculate target volume based on distance
        if (distance <= minDistance)
        {
            targetVolume = MySoundManager.instance.musicVolume;
        }
        else if (distance >= maxDistance)
        {
            targetVolume = minVolume;
        }
        else
        {
            float t = (distance - minDistance) / (maxDistance - minDistance);
            targetVolume = Mathf.Lerp(MySoundManager.instance.musicVolume, minVolume, t);
        }
        
        audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, Time.deltaTime * fadeSpeed);
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minDistance);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}