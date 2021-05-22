using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform snake;
    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, snake.rotation, Time.deltaTime * 2f);
    }
}
