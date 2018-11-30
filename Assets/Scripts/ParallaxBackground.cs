using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public bool scrolling, paralax;

    public float backgroundSize = 2048f;
    public float paralaxSpeed;
    
    private Transform cameraTransform;
    private Transform[] layers;
    public float viewZone = 10;
    private int leftIndex;
    private int rightIndex;

    public float StartY = 0f;

    private float lastCameraX;
    
    // Start is called before the first frame update
    void Start()
    {
        // Any starting values need to be multiplied by our parent's scalar
        float mult = transform.parent.transform.localScale.x;
        viewZone *= mult;
        backgroundSize *= mult;
        
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        layers = new Transform[transform.childCount];
        if(scrolling) StartY = transform.GetChild(0).position.y;
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (paralax)
        {
            float deltaX = cameraTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * paralaxSpeed);
        }

        lastCameraX = cameraTransform.position.x;

        if (scrolling)
        {
            if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
            {
                ScrollLeft();
            }

            if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
            {
                ScrollRight();
            }
        }

        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].SetPositionAndRotation(new Vector3(layers[i].position.x, StartY, 0f), layers[i].rotation);
        }
    }

    private void ScrollLeft()
    {
        int lastRight = rightIndex;
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        layers[rightIndex].SetPositionAndRotation(new Vector3(layers[rightIndex].position.x, StartY, 0f), layers[rightIndex].rotation);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }
    }

    private void ScrollRight()
    {
        int lastLeft = leftIndex;
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        layers[leftIndex].SetPositionAndRotation(new Vector3(layers[leftIndex].position.x, StartY, 0f), layers[leftIndex].rotation);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
            leftIndex = 0;
    }
}
