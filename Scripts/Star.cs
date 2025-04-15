using UnityEngine;

public class Star : Item
{
    [SerializeField] private ParticleSystem _particle;

    private void Update()
    {
        Rotation();
    }

    public override void GetItem(ItemCollector collector)
    {
        base.GetItem(collector);
        _particle.Play();
        StarManager.Instance.IsGetRareItem = true;
    }
}
