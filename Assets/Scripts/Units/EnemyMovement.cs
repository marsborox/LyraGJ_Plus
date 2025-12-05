
using UnityEngine;
using UnityEngine.AI;

using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : UnitMovement
{
    public Enemy thisEnemy;

    [SerializeField] private EnemyCombat enemyCombat;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private AnimationController _animationController;
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
        //bool isInRange = false;
        //redo movement direction here
        //Debug.Log("movingToTarget");
        float horizontalDistance = Mathf.Abs(player.transform.position.x - transform.position.x);
        float verticalDistance = Mathf.Abs(player.transform.position.y - transform.position.y);

        /*if (horizontalDistance >= verticalDistance)
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
        }*/
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        //Debug.Log("TryingToMove");
        _agent.SetDestination(player.transform.position);
        //Debug.Log("pre animation");
        //_animationController.HandleAnimation();//temporarily Shut Down
        //Debug.Log("post animation");
    }
    void DirectionChecker()
    {
        //float horizontalDistance = Mathf.Abs(_agent.nextPosition.x - transform.position.x);
        //float verticalDistance = Mathf.Abs(_agent.nextPosition.y - transform.position.y);
        /*_agent.path.corners*/
        float horizontalDistance = Mathf.Abs(_agent.steeringTarget.x - transform.position.x);
        float verticalDistance = Mathf.Abs(_agent.steeringTarget.y - transform.position.y);

        //Debug.Log("VerticalDistance: "+ verticalDistance + " horizontalDistance: "+horizontalDistance);
        
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
