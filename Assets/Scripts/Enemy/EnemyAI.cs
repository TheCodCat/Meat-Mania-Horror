using Assets.Scripts.Enemy;
using Assets.Scripts.Models;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public LockerController[] LockerControllers;
    public AudioSource AudioSource;
    public AudioClip AudioClipWalk;
    public AudioClip AudioClipRunning;
    public FieldOfView FieldOfView;
    public Animator Animator;
    public NavMeshAgent AI_Agent;
    public GameObject Player;
    public DeadPlayer DeadPlayer;
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
    public float MinRadius;
    public LayerMask LayerMask;

    public const string PATROLL_KEY = "Patroll";
    public const string CHASE_KEY = "Move";
    public const string LOOK_KEY = "Look";
    public const string ATTACK_KEY = "Attack";

    public const float PatrollSpeed = 1f;
    public const float RunningSpeed = 3f;

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
        if(Physics.CheckSphere(transform.position,MinRadius, LayerMask))
        {
            if (LockerControllers.ToList().Contains(LockerControllers.SingleOrDefault(x => x.IsLocker)))
            {
               transform.LookAt(Player.transform);
               DeadPlayer.PlayDead();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MinRadius);
    }
}
    public enum AI_State { Patrol, Stay, Chase};
