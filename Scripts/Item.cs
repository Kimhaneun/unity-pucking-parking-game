using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Accessibility;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private AudioClip _sound;

    public float RotateSpeed => _rotateSpeed;
    public AudioClip Sound => _sound;

    protected void Rotation()
    {
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }

    protected void PlaySound()
    {
        // Debug.Log(_sound.name);
    }

    protected void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public virtual void GetItem(ItemCollector collector)
    {
        PlaySound();
        Deactivate();
    }
}
