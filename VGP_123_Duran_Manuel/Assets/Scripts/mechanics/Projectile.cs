using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField, Range(1, 50)] private float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject, lifetime);
    }

    // Update is called once per frame
    public void SetVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
