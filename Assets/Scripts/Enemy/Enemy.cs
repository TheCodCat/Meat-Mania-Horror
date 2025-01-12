using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent Agent;
    public CharacterController Player { get; set; }
    public LayerMask LayerMask;
    public EnemyParametrs EnemyParametrs;
    public Transform Head;
    public FieldOfView FieldOfView;
    public const string IDLE_KEY = "Idle";
    public const string WALK_KEY = "IsTarget";
    public const string LOOK_KEY = "Look";
    public const string Patroll_KEY = "Patroll";
    public const string ATTACK_KEY = "Attack";

    public Transform[] WPF;

    [Inject]
    public void Construct(CharacterController player)
    {
        Debug.Log("все ок");
        Player = player;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyParametrs.MinRadius);

        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, EnemyParametrs.MaxRadius);
    }

    public void SetTarget(CharacterController player)
    {
        Player = player;
    }
}
