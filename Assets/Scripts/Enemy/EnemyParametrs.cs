using UnityEngine;

[CreateAssetMenu(fileName = "New ParamEnemy", menuName = "EnemyParametrs")]
public class EnemyParametrs : ScriptableObject
{
    public float Speed;
    public float MinRadius;
    public float MaxRadius;
}
