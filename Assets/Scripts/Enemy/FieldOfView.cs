using System;
using UnityEngine;
using Zenject;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    [SerializeField] private float _threslod;
    public Transform playerRef { get; set; }

    public LayerMask targetMask;
    public LayerMask ObstacleMask;
    public bool canSeePlayer;
    private LoudnessToMicrophone _loudnessToMicrophone;

    [Inject]
    public void Construct(CharacterController characterController, LoudnessToMicrophone loudnessToMicrophone)
    {
        playerRef = characterController.transform;
        _loudnessToMicrophone = loudnessToMicrophone;
    }

    public bool FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);




        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, ObstacleMask))
                    canSeePlayer = true;

            else
            {
                if (playerRef.GetComponent<PlayerMover>().IsSprint == true)
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
                    
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;

        if (_loudnessToMicrophone.Loudness > _threslod)
            canSeePlayer = true;

        return canSeePlayer;
    }
}
