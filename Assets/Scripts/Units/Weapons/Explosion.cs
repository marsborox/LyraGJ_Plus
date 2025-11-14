using UnityEngine;
using System.Collections;
using TMPro;
public class Explosion : MonoBehaviour
{
    public int damage;
    public float pushBackSpeed;
    public float pushBackForce;
    public float pushBackDuration;

    [SerializeField] private float _targetScale = 13.5f;
    [SerializeField] private float _explosionSpeed = 1f;
    [SerializeField] private float _postExplosionTime=2;
    [SerializeField] private float _alphaDecrement;
    [SerializeField] private Collider2D _myCollider;
    [SerializeField] private SpriteRenderer _thisSprite;

    [SerializeField] private bool _isExploding;
    [SerializeField] private bool _isFading = false;

    public GameObject grenade;
    private void Awake()
    {
        _myCollider = GetComponent<Collider2D>();
        _thisSprite = GetComponent<SpriteRenderer>();
        _isExploding = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hitting " + other.tag);
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            Unit unitWeHit = other.gameObject.GetComponent<Unit>();
            unitWeHit.TakeDamage(damage);
            unitWeHit.GetPushedBack(this.transform.position,pushBackForce,pushBackDuration);
        }
    }
    private void Start()
    {
        CountAlphaDecrement();
    }
    private void FixedUpdate()
    {
        if (_isExploding)
        {
            Exploding();
            return;
        }
        if (_isFading)
        {
            Fading();
        }
    }

    void CountAlphaDecrement()
    { 
        _alphaDecrement = 1/_postExplosionTime;
    }
    void Exploding()
    {
        if (transform.localScale.x < _targetScale)
        {
            float scale = this.transform.localScale.x;
            float time = Time.fixedDeltaTime;
            scale += (_explosionSpeed * time);
            //Debug.Log("exploding scale is " + scale.ToString() + "Time elapsed "+time);
            //Debug.Log("Scale: "+scale + " time: "+time);

            transform.localScale = new Vector3(scale, scale, transform.localScale.z);
        }
        else 
        {
            _isExploding=false;
            _myCollider.enabled = false;
            _isFading=true;

        }
    }
    void Fading()
    {
        float alpha = _thisSprite.color.a;
        float alphaDecrement = _alphaDecrement * Time.fixedDeltaTime;
        Color color = _thisSprite.color;

        //Debug.Log(alphaDecrement);
        color.a -= alphaDecrement;
        _thisSprite.color = color;

        if (alpha < 0)
        {
            Destroy(grenade);
            Destroy(this.gameObject);//must destroy grenade too
        }
        //_thisSprite.color.a = alpha;
    }

}
