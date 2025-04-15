using System;
using UnityEngine;

public class Coin : Item
{
    private const int VALUE = 100;

    private void Update()
    {
        Rotation();
    }

    public override void GetItem(ItemCollector collector)
    {
        base.GetItem(collector);
        AddCoin(collector, VALUE);
    }

    private void AddCoin(ItemCollector collector, int value)
    {
        collector.CurrentCoinCount += value;
    }
}
