using UnityEngine;

using static UnityEngine.GraphicsBuffer;

public class Grenade : Projectile
{
    public Explosion explosion;
    public const float GRAVITY_ACCELERATION = 9.8f/*9.8f*/;
    

    [SerializeField] private GameObject _grenadeVisual;
    [SerializeField] private GameObject _grenadeShadow;
    [SerializeField] private float _horisontalVelocity;
    [SerializeField] private float _currentVerticalVelocity;
    [SerializeField] private Vector3 _destination;

    private bool _isMoving = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isMoving)
            return;
        GrenadeMovementHorisontal();
        GrenadeMovementVertical();
    }

    public void GrenadeMovementHorisontal()
    {
        transform.position = Vector3.MoveTowards(transform.position, _destination, _horisontalVelocity * Time.deltaTime);

    }
    public void GrenadeMovementVertical()
    {
        _currentVerticalVelocity -= (GRAVITY_ACCELERATION * Time.deltaTime);
        float grenadeVisualPosition = _grenadeVisual.transform.position.y;
        grenadeVisualPosition += (_currentVerticalVelocity * Time.deltaTime);
        _grenadeVisual.transform.position = new Vector3(_grenadeVisual.transform.position.x,grenadeVisualPosition, _grenadeVisual.transform.position.z);

        if ((-1 * _currentVerticalVelocity) > _horisontalVelocity)
        {
            explosion.damage = damage;
            explosion.gameObject.SetActive(true);
            _isMoving = false;
            _grenadeVisual.gameObject.SetActive(false);
            _grenadeShadow.gameObject.SetActive(false);
        }
    }
    public void CalcBalistics(Vector2 destination)
    {
        _destination = destination;
        //this.gameObject.transform.Get

        float distance = Vector3.Distance(transform.position,destination);
        //Debug.Log("distance: " + distance);
        //45Degree launch, simplified
        float initialVelocity = (distance * GRAVITY_ACCELERATION);
        //we should use square root of distance*GravityAcceleration
        //that would be real velocity, and then initVelcoity squared /2
        //which is then square rooted but to save cpu.... we bypass
        
        float verVelocity = Mathf.Sqrt((initialVelocity /* * initialVelocity*/) / 2);
        //vertical and horisontal are same
        //Debug.Log(verVelocity);
        //Debug.Log("initialVelocity "+initialVelocity);
        //Debug.Log("Vert/Hor speed is "+verVelocity);
        _horisontalVelocity = verVelocity;
        _currentVerticalVelocity = verVelocity;
        _isMoving = true;
    }
}
