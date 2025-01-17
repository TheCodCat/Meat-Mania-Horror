using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsPaperGiv { get; private set; }

    public void GivePaperClip()
    {
        IsPaperGiv = true;
    }
    public void SetGiveKey(bool give)
    {
        IsPaperGiv = give;
    }
}
