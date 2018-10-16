using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// Player movement
// Player waypoint
// multiple bool win & lose
// win & lose condition trigger enter
// particle win & lose
// mobile input

public class PlayerController : MonoBehaviour {

    public Transform targetParent; //parent of the target

    //public Transform[] target;

    public List<Transform> target; // child of target parent (waypoint)

    public Transform[] path; //right path
    //public Transform redPlatformPos;

    [SerializeField]
    private Rigidbody rb;

    public GameObject winParticleB; //win particle blue
    public GameObject winParticleG; //win particle green
    public GameObject winParticleR; //win particle red
    public GameObject loseParticle; //lose particle
    public GameController GC; // access to game controller script

    public int currentPath; 

    public float speed; //player's speed
    public int current;
    

    public bool tap;
    public bool goingRightPoint;
    public bool winBool;
    public bool loseBool;

    void Start ()
    {
        target = new List<Transform>();
        foreach (Transform t in targetParent)
        {
            target.Add(t);
        }

        rb = GetComponent<Rigidbody>();
        tap = false;
        winParticleB.SetActive(false);
        winParticleG.SetActive(false);
        winParticleR.SetActive(false);
        loseParticle.SetActive(false);
    }
	
	void Update ()
    {
        TapToMove();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (tap == false)
        {
            //going left and right
            if (transform.position != target[current].position)
            {
                Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
                rb.MovePosition(pos);
            }
            //else current = (current + 1) % target.Length;
            else current = (current + 1) % target.Count;

            GC.gameTimerBool = true;
        }

        if (tap == true)
        {
            if(goingRightPoint == true)
            {
                //going to path
                Vector3 pos = Vector3.MoveTowards(transform.position, path[currentPath].position, speed * Time.deltaTime);
                rb.MovePosition(pos);
            }
        }

         if (tap == true)
        {
            if (goingRightPoint == false)
            {
                //going to wrong path
                Vector3 pos = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, 1), speed * Time.deltaTime);
                rb.MovePosition(pos);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PathCollider")
        {
            goingRightPoint = true;
            if (other.GetComponent<PathBehaviour>() != null)
            {
                currentPath = other.GetComponent<PathBehaviour>().pathIndex;
            }
        }

        if (other.gameObject.tag == "BluePath" && GC.Player.tag == "PlayerB")
        {
            Vector3 pos = GC.Player.transform.position;
            winParticleB.transform.position = pos;
            winParticleB.SetActive(true);
            GC.stopGameTimer();
            winBool = true;
        }

        if (other.gameObject.tag == "GreenPath" && GC.Player.tag == "PlayerG")
        {
            Vector3 pos = GC.Player.transform.position;
            winParticleG.transform.position = pos;
            winParticleG.SetActive(true);
            GC.stopGameTimer();
            winBool = true;
        }

        if (other.gameObject.tag == "RedPath" && GC.Player.tag == "PlayerR")
        {
            Vector3 pos = GC.Player.transform.position;
            winParticleR.transform.position = pos;
            winParticleR.SetActive(true);
            GC.stopGameTimer();
            winBool = true;
        }

        if (other.gameObject.tag == "BluePath" && GC.Player.tag != "PlayerB")
        {
            loseParticle.SetActive(true);
            GC.stopGameTimer();
            loseBool = true;
        }

        if (other.gameObject.tag == "GreenPath" && GC.Player.tag != "PlayerG")
        {
            loseParticle.SetActive(true);
            GC.stopGameTimer();
            loseBool = true;
        }

        if (other.gameObject.tag == "RedPath" && GC.Player.tag != "PlayerR")
        {
            loseParticle.SetActive(true);
            GC.stopGameTimer();
            loseBool = true;
        }

        if(other.gameObject.tag == "LoseCollider")
        {
            loseParticle.SetActive(true);
            GC.stopGameTimer();
            loseBool = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PathCollider")
        {
            currentPath = 100;
            goingRightPoint = false;
        }
    }

    void TapToMove()
    {
#if UNITY_STANDALONE

        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
        }

#elif UNITY_IOS || UNITY_ANDROID

        Touch myTouch = Input.GetTouch(0);

        if (myTouch.phase == TouchPhase.Began)
        {
            tap = true;
        }
#endif
    }
}
