using UnityEngine;

public class BallControllerX : MonoBehaviour
{
    private bool ignoreNextCollision;

    public Rigidbody rb;
    public float impulseForce = 5.0f;

    private Vector3 startPos;

    public int perfectPass =0;
    public bool isSuperSpeedActive;
    public int dropSpeed =10;



    // Start is called before the first frame update
    void Awake()
    {
        startPos = transform.position;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
        { return; }

        if (isSuperSpeedActive)
        {
            if (!collision.transform.GetComponent<GoalX>())
            {
                Destroy(collision.transform.parent.gameObject, 0.4f);
                Debug.Log("Destroying platform");




            }

        }
        else 
        {
            
        
        }


        //to ensure that the deathparts are accessed through the ball collision, create a deathpart reference of the deathpartx script 
        //check that that object you collide with has the deathpartx component
        DeathPartX deathPart = collision.transform.GetComponent<DeathPartX>();
        if (deathPart)
        {
            deathPart.HitDeathPart();
        
        }

        //Debug.Log("Log touched Something");

        rb.velocity = Vector3.zero;

        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

        ignoreNextCollision = true;
        Invoke("AllowCollision", 0.2f);

        perfectPass = 0;
        isSuperSpeedActive = false;

        

    }

    private void Update()
    {
        if (perfectPass >= 3 && !isSuperSpeedActive) 
        { 
            isSuperSpeedActive=true;
            rb.AddForce(Vector3.down * dropSpeed, ForceMode.Impulse);
        
        }
        
    }

    private void AllowCollision()
    {
        ignoreNextCollision = false;

    }

    // Update is called once per frame
    public void ResetBall()
    { 
        transform.position = startPos;
    
    }
}
