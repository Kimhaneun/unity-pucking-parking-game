using UnityEngine;
using UnityEngine.UI;

public class CarSkinUIManager : MonoBehaviour, IBuyable
{
    private ShopCarData _carData;
    private ItemCollector _itemCollector;

    [SerializeField] private Button[] _carSelectionButtons;
    [SerializeField] private GameObject[] _cars;

    [SerializeField] private GameObject _selectButton;
    [SerializeField] private GameObject _buyButton;

    private CarSkin _carSkin;

    private void Awake()
    {
        // �ڵ��� ������Ʈ�� �ڵ��� ������Ʈ�� �ڵ��� �������� ���ݰ� isUnlock�� ������
        _carData = new ShopCarData();

        for (int i = 0; i < _carSelectionButtons.Length; i++)
        {
            int index = i; // ���� ������ ĸó
            _carSelectionButtons[i].onClick.AddListener(() => SelectCarSkin(index));
        }
    }

    private void SelectCarSkin(int index)
    {
        for (int i = 0; i < _cars.Length; i++)
        {
            _cars[i].SetActive(false);
        }
        if (index >= 0 && index < _cars.Length)
        {
            _cars[index].SetActive(true);

            _carSkin = _cars[index].GetComponent<CarSkin>();
            SetButtonStates(_carSkin.CarData);
        }
    }

    private void SetButtonStates(ShopCarData carData)
    {
        if (!carData.isUnlock)
        {
            _buyButton.SetActive(true);
            _selectButton.SetActive(false);
        }
        else if (carData.isUnlock)
        {
            _buyButton.SetActive(false);
            _selectButton.SetActive(true);
        }
    }

    private void BuyCarSkin()
    {
        ShopCarData carData = _carSkin.CarData;
        int cost = carData.Cost; // �ڵ����� ������ �����ɴϴ�

        if (!CanBuyCarSkin(cost))
            Debug.Log("�� ���� ������");
        else
        {
            InventoryManager.Instance.Coin -= cost;
            carData.isUnlock = true;
            SetButtonStates(carData);
        }
    }

    public void OnBuyButtonClick()
    {
        BuyCarSkin();
        // _carSelectionButtons[0].onClick.Invoke();
    }

    public bool CanBuyCarSkin(int cost)
    {
        if (InventoryManager.Instance.Coin >= cost)
            return true;
        else
            return false;
    }

    // public bool CanBuyCarSkin(int costCoinAmount)
    // {
    //     if (costCoinAmount <= _currentCoinCount)
    //     {
    //         _currentCoinCount -= costCoinAmount;
    //         return true;
    //     }
    //     else
    //         return false;
    // }
}
