using UnityEngine;

[CreateAssetMenu(menuName = "SO/Level/LevelData")]
public class LevelData : ScriptableObject
{
    public int level;
    public Vector3 timer;
    public CarSpawner levelPrefab;
}
