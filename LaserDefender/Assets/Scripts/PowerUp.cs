using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private int powerQuantity = 100;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private AudioClip catchSound;
    [SerializeField][Range(0,1)] private float catchSoundVolume = 0.5f;
    
    public int GetPowerQuantity()
    {
        return powerQuantity;
    }
    
    public float GetSpeed()
    {
        return speed;
    }
    
    public void Hit()
    {
        Destroy(gameObject, 0.05f);
        AudioSource.PlayClipAtPoint(catchSound, Camera.main.transform.position, catchSoundVolume);
    }
}
