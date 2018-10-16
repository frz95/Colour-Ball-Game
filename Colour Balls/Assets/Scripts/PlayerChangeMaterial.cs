using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeMaterial : MonoBehaviour {

    public Material[] material;
    Renderer rend;
    public GameObject greenGlow;
    public GameObject blueGlow;
    public GameObject redGlow;

    public Animator greenA, blueA, redA, whiteA;

    void Start ()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GreenBlock")
        {
            rend.sharedMaterial = material[1];
            //play animation green
            greenA.Play("PlayColorG");
        }

        if (other.gameObject.tag == "BlueBlock")
        {
            rend.sharedMaterial = material[2];
            //play animation blue
            blueA.Play("PlayColorB");
        }

        if (other.gameObject.tag == "RedBlock")
        {
            rend.sharedMaterial = material[3];
            //play animation red
            redA.Play("PlayColorR");
        }

        if (other.gameObject.tag == "WhiteBlock")
        {
            rend.sharedMaterial = material[0];
            //play animation white
            whiteA.Play("PlayColorW");
        }
    }
}
