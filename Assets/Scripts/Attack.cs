using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public GameObject MagicBolt;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Attack
            GetComponent<Animator>().SetTrigger("ForwardFire");
            GameObject go = Instantiate(MagicBolt);
            go.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
        }
    }
    
}
