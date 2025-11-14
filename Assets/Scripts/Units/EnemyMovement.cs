using UnityEngine;

using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : UnitMovement
{
    public Enemy thisEnemy;

    [SerializeField] private EnemyCombat enemyCombat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    public void MoveToTarget(Player player)
    {
        //bool isInRange = false;
        
        float horizontalDistance = Mathf.Abs(player.transform.position.x - transform.position.x);
        float verticalDistance = Mathf.Abs(player.transform.position.y - transform.position.y);

        if (horizontalDistance >= verticalDistance)
        {
            if (player.transform.position.x < transform.position.x)
            {
                currentDirection = Direction.LEFT;
            }
            else if (player.transform.position.x >= transform.position.x)
            {
                currentDirection = Direction.RIGHT;
            }
        }
        else
        {
            if (player.transform.position.y > transform.position.y)
            {
                currentDirection = Direction.UP;
            }
            else if (player.transform.position.y <= transform.position.y)
            {
                currentDirection = Direction.DOWN;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
    }
    public void Move(Unit target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
    }
}
