using UnityEngine;


public class Health : MonoBehaviour, IHit
{
    public int health = 10;

    public void Hit(GameObject other)
    {
        health -= 1;
        Debug.Log("I've been hit!");
    }
}