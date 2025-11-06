using UnityEngine;

public class EnemyMovement : UnitMovement
{
    public Enemy thisEnemy;
    [SerializeField]private EnemyCombat enemyCombat;
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
        float distance = Vector3.Distance(player.transform.position, transform.position);
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
        if (distance < enemyCombat.range)
        {
            thisEnemy.Attack();
        }
        else
        {
            Move(player);
        }
    }
    public void Move(Unit target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, movementSpeed * Time.deltaTime);
    }
}
