using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _targetScale = 13.5f;
    [SerializeField] private float _explosionSpeed = 1f;
    [SerializeField] private Collider2D _myCollider;

    private void Awake()
    {
        _myCollider = GetComponent<Collider2D>();
    }
    private void Update()
    {
        Exploding();
    }
    void Exploding()
    {
        if (transform.localScale.x < _targetScale)
        {
        float scale = this.transform.localScale.x;
        float time = Time.deltaTime;
        scale += (_explosionSpeed * time);
        //Debug.Log("exploding scale is " + scale.ToString() + "Time elapsed "+time);

        
        transform.localScale+=new Vector3(scale,scale, transform.localScale.z);
        }
    }
}
