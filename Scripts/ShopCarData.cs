using UnityEngine;

[CreateAssetMenu(menuName = "SO/Shop/CarData")]
public class ShopCarData : ScriptableObject
{
    public int Cost;
    public bool isUnlock = false;
    public Material CarMAT;
}
