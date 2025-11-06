using UnityEngine;

public class PlayerMovement : UnitMovement
{
    public float moveSpeed = 100f;
    private Rigidbody2D _myRigidbody2D;


    

    private void Awake()
    {
        _myRigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        
    }
    public void Move(Vector2 rawInput)
    {
        Vector3 delta = (rawInput * moveSpeed * Time.deltaTime);
        //transform.position += delta;
        _myRigidbody2D.linearVelocity = delta;
        currentUnitVisual.Animate(Time.deltaTime);
    }


}
