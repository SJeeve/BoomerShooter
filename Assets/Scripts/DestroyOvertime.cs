using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOvertime : MonoBehaviour
{
    public float lifeTime;
    void Start()
    {
        if(lifeTime <= 0)
        {
            Debug.LogWarning(transform.name + "'s lifetime has not been declared/is invalid.");
            lifeTime = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}
