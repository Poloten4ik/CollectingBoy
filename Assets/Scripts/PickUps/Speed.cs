using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public float speed;
    public Vector2 direction;

    private void Update()
    {
        transform.Translate(speed * direction * Time.deltaTime,Space.World);
    } 
}
