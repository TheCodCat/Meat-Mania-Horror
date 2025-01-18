using Assets.Scripts.Enemy;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public FieldOfView FieldOfView;
    public Animator Animator;
    public NavMeshAgent AI_Agent;
    public GameObject Player;

    public Transform[] WayPoints;
    public int Current_Patch;

    public AI_State AI_Enemy;

    public Vector3 Last_point { get; set; }
    public bool Check_LastPoint;
    float i_stay;

    public StateMachine StateMachine = new StateMachine();
    public PatrollState PatrollState;
    public LookState LookState;
    public ChaseState ChaseState;

    public const string PATROLL_KEY = "Patroll";
    public const string CHASE_KEY = "Move";
    public const string LOOK_KEY = "Look";

    public const float PatrollSpeed = 0.5f;
    public const float RunningSpeed = 2f;

    private void Start()
    {
        PatrollState = new PatrollState(this);
        LookState = new LookState(this);
        ChaseState = new ChaseState(this);

        StateMachine.SetStartState(PatrollState);
    }
    private void Update()
    {
        StateMachine.UpdateState();
    }

    //void FixedUpdate()
    //{
    //    if (Check_LastPoint == false)
    //    {
    //        if (AI_Enemy == AI_State.Patrol)
    //        {
    //            Animator.SetTrigger("Patroll");
    //            AI_Agent.SetDestination(WayPoints[Current_Patch].transform.position);
    //            if (AI_Agent.remainingDistance < 2)
    //            {
    //                Current_Patch++;
    //                Current_Patch = Current_Patch % WayPoints.Length;
    //            }
    //        }
    //        if (AI_Enemy == AI_State.Stay)
    //        {
    //            Animator.SetTrigger("Look");
    //            AI_Agent.SetDestination(gameObject.transform.position);
    //        }
    //        if (AI_Enemy == AI_State.Chase)
    //        {
    //            Animator.SetTrigger("Move");
    //            if (gameObject.GetComponent<FieldOfView>().canSeePlayer == false)
    //            {
    //                Last_point = Player.transform;
    //                Check_LastPoint = true;
    //                AI_Enemy = AI_State.Stay;
    //            }
    //            else
    //            {
    //                AI_Agent.SetDestination(Player.transform.position);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Animator.SetTrigger("Look");
    //        i_stay += Time.deltaTime;
    //        float Point_Dist = Vector3.Distance(Last_point.transform.position, gameObject.transform.position);
    //        if (Point_Dist < 1 || i_stay >= 7)
    //        {
    //            Check_LastPoint = false;
    //            AI_Enemy = AI_State.Patrol;
    //            i_stay = 0;
    //        }
    //        else
    //        {
    //            Animator.SetTrigger("Look");
    //        }
    //    }
    //    float Dist_Player = Vector3.Distance(Player.transform.position, gameObject.transform.position);
    //    if (Dist_Player < 2)
    //    {
    //        Debug.Log("Попался");
    //    }
    //}
}
    public enum AI_State { Patrol, Stay, Chase};
