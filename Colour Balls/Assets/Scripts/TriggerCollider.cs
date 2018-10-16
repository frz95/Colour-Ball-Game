using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollider : MonoBehaviour {

    
    //public GameController GC;
    

    void Start ()
    {
        
    }
	
	void Update ()
    {
        
	}
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "GreenBlock")
        {
            transform.tag = "PlayerG";
        }
        if (other.gameObject.tag == "BlueBlock")
        {
            transform.tag = "PlayerB";
        }
        if (other.gameObject.tag == "RedBlock")
        {
            transform.tag = "PlayerR";
        }
        if (other.gameObject.tag == "WhiteBlock")
        {
            transform.tag = "Player";
        }
    }
}
