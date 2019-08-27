using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    
    Transform playerTransform;

    public LayerMask onlyCollideWith;
    SpriteRenderer spriteController;
    public float timeTllNextInput;
    private float maxTime;

    public Camera combatCamera;
    public bool canMove;
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        spriteController = GetComponent<SpriteRenderer>();
        canMove = true;
        maxTime = timeTllNextInput;
        timeTllNextInput = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            if(timeTllNextInput<=0)
            {
                movePlayer();
            }
            timeTllNextInput-=Time.deltaTime;
        }
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
            RaycastHit2D hit = Physics2D.Raycast(transform.position, alterPos,1, onlyCollideWith);

            if(hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                takenInput = false;

            }
        }

        if(takenInput)
        {
            if(alterPos.x >0)
            {
                spriteController.flipX = false;
            }
            if(alterPos.x <0)
            {
                spriteController.flipX = true;
            }

            playerTransform.Translate(alterPos);
            timeTllNextInput = maxTime;
            GameManager.instance?.Step(this);
        }

        
    }
}
