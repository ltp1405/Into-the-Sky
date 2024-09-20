using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Playtime : MonoBehaviour
{
    public float playtime = 0;

    void Update()
    {
        playtime += Time.deltaTime;
    }
}
