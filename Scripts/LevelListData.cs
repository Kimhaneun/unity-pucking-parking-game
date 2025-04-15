using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Level/ListData")]
public class LevelListData : ScriptableObject
{
    public List<LevelData> levelList;

    private void OnValidate()
    {
        if (levelList == null)
            return;

        for (int i = 0; i < levelList.Count; i++)
        {
            if (levelList[i] != null)
                levelList[i].level = i + 1;
        }
    }
}
