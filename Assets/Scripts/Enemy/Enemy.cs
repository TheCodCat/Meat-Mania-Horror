using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent Agent;
    public CharacterController Player { get; set; }
    public LayerMask LayerMask;
    public EnemyParametrs EnemyParametrs;

    public const string IDLE_KEY = "Idle";
    public const string WALK_KEY = "Walk";

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

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, EnemyParametrs.MaxRadius);
    }
}
