using UnityEngine;


public class LockerController : MonoBehaviour
{
    private bool _isLocker;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.transform.TryGetComponent(out PlayerController component))
        {
            _isLocker = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.transform.TryGetComponent(out PlayerController component))
        {
            _isLocker = false;
        }
    }
}
