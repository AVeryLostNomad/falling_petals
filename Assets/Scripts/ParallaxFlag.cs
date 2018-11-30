using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxFlag : MonoBehaviour
{

    public GameObject[] ItemToParallax;
    public GameObject[] ItemToStopParallax;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach(GameObject itp in ItemToParallax)
        {
            foreach (Object o in itp.transform)
            {
                GameObject child = ((Transform)o).gameObject;
                if (child.GetComponent<ParallaxBackground>() != null)
                {
                    // We have a parllax background component
                    ParallaxBackground pb = child.GetComponent<ParallaxBackground>();
                    pb.paralax = true;
                }
            }
        }

        foreach(GameObject itp in ItemToStopParallax)
        {
            foreach (Object o in itp.transform)
            {
                GameObject child = ((Transform)o).gameObject;
                if (child.GetComponent<ParallaxBackground>() != null)
                {
                    // We have a parllax background component
                    ParallaxBackground pb = child.GetComponent<ParallaxBackground>();
                    pb.paralax = true;
                }
            }
        }
    }
    
}
