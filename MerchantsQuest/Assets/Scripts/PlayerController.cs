using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    
    Transform playerTransform;

    public LayerMask onlyCollideWith;

    public float timeTllNextInput;
    private float maxTime;
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        maxTime = timeTllNextInput;
        timeTllNextInput = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeTllNextInput<=0)
        {
            movePlayer();
        }
        timeTllNextInput-=Time.deltaTime;
    }


    void movePlayer()
    {
        bool takenInput = false;

        Vector3 alterPos = new Vector3(0,0,0);
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            // +1 = right -1 = left
            alterPos.x = Input.GetAxisRaw("Horizontal");
            takenInput = true;
        }
        else if(Input.GetAxisRaw("Vertical") != 0)
        {
            // -1 = down  +1 = up
            alterPos.y = Input.GetAxisRaw("Vertical");
            takenInput = true;
        }

        if(takenInput)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, alterPos, onlyCollideWith, 1);

            if(hit.collider != null)
            {
                Debug.Log("Collision is happening?");
            }
        }

        if(takenInput)
        {
            playerTransform.Translate(alterPos);
            timeTllNextInput = maxTime;
        }

        
    }
}
