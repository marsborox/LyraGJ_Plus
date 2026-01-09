
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : UnitMovement
{
    
    public Enemy thisEnemy;

    [SerializeField] private EnemyCombat enemyCombat;
    [SerializeField] private NavMeshAgent _agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = movementSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        base.Update();
        DirectionChecker();
    }
    public void MoveToTarget(Player player)
    {
        float horizontalDistance = Mathf.Abs(player.transform.position.x - transform.position.x);
        float verticalDistance = Mathf.Abs(player.transform.position.y - transform.position.y);

        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        //Debug.Log("TryingToMove");
        _agent.SetDestination(player.transform.position);
        animationController.HandleMovementAnimation();
        
        //Debug.Log("pre animation");
        //animationController.HandleAnimation();
        //animationController.HandleAnimation();
        //Debug.Log("post animation");
    }
    public void StopMovement()
    {
        _agent.SetDestination(transform.position);
    }
    void DirectionChecker()
    {
        float horizontalDistance = Mathf.Abs(_agent.steeringTarget.x - transform.position.x);
        float verticalDistance = Mathf.Abs(_agent.steeringTarget.y - transform.position.y);

        if (horizontalDistance >= verticalDistance)
        {
            if (_agent.steeringTarget.x < transform.position.x)
            {
                currentDirection = Direction.LEFT;
            }
            else if (_agent.steeringTarget.x >= transform.position.x)
            {
                currentDirection = Direction.RIGHT;
            }
        }
        else
        {
            if (_agent.steeringTarget.y > transform.position.y)
            {
                currentDirection = Direction.UP;
            }
            else if (_agent.steeringTarget.y <= transform.position.y)
            {
                currentDirection = Direction.DOWN;
            }
        }
    }
}
