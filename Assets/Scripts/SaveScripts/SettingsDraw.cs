using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "New Settings")]
public class SettingsDraw : ScriptableObject
{
    public int BrushSuze;
    public Texture2D Texture2D;
    [Range(0, 1)] public float FillAmound;
}
