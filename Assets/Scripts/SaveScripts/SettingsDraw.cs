using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "New Settings")]
public class SettingsDraw : ScriptableObject
{
    public int BrushSuze;
    public Vector2Int SizeTexture;
    [Range(0, 1)] public float FillAmound;
}
