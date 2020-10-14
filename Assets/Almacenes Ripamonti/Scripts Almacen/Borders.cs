using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    [SerializeField] Transform left;
    [SerializeField] Transform right;

    // Start is called before the first frame update
    void Start()
    {
        Camera camera = Camera.main;
        float OrthoWidth = camera.orthographicSize * camera.aspect;
        left.position = new Vector3(-(OrthoWidth + .5f),0,0);
        right.position = new Vector3(OrthoWidth + .5f,0,0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
