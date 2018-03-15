using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    [SerializeField]
    private float xMax;
    [SerializeField]
    private float yMax;
    [SerializeField]
    private float xMin;
    [SerializeField]
    private float yMin;

    private Transform target;

    private void Start()
    {
        
        target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y+5, yMin, yMax));
        
    }

}

