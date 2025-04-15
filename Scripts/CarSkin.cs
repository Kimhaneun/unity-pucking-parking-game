using UnityEngine;

public class CarSkin : MonoBehaviour
{
    [field: SerializeField] public ShopCarData CarData { get; private set; }

    private void OnEnable()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material = CarData.CarMAT;
    }
}
