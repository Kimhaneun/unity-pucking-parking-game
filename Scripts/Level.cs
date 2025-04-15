using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoSingleton<Level>
{
    [field: SerializeField] public LevelData LevelData { get; private set; }

    private List<Item> _items;
    private int _itemCount;

    public event Action LevelStartEvent;
    public event Action LevelOverEvent;

    private void OnEnable()
    {
        // SetUpItem();

        LevelStartEvent += HandheldeLevelStart;
        // LevelClearedEvent += HandheldeLevelCleared;
        LevelOverEvent += HandheldeGameOver;
    }

    private void OnDisable()
    {
        // SetOutItem();

        LevelStartEvent -= HandheldeLevelStart;
        // LevelClearedEvent -= HandheldeLevelCleared;
        LevelOverEvent -= HandheldeGameOver;
    }

    private void SetUpItem()
    {
        Transform itemParentTransform = transform.Find("Items");
        _items = itemParentTransform.GetComponentsInChildren<Item>().ToList();

        _itemCount = _items.Count;
        // _items.ForEach(item => item.OnItemCollected += HandheldeItemCollectd);
    }

    private void SetOutItem()
    {
        Transform itemParentTransform = transform.Find("Items");
        _items = itemParentTransform.GetComponentsInChildren<Item>().ToList();

        _itemCount = _items.Count;
        // _items.ForEach(item => item.OnItemCollected -= HandheldeItemCollectd);
    }


    private void HandheldeLevelStart()
    {
        // DataManager.Instance.SetStarData(); // �� ������ �ʱ�ȭ
    }

    private void HandheldeLevelCleared()
    {
        ShowLevelClearPanel();
    }

    private void HandheldeGameOver()
    {
    }

    private void HandheldeItemCollectd(Item item)
    {
        _itemCount--;
    }

    // �ӽ÷� ������ �޼��� 
    // �Ű������� UI... ���� ���� �� �ְ���
    private void ShowLevelClearPanel()
    {
        // ���⼭ ���� �������� true��� ���� ���� ��
        //StarManager.Instance.IsClear();
        //StarManager.Instance.IsGetRareItem();
        //StarManager.Instance.IsTimeOut();
    }
}
