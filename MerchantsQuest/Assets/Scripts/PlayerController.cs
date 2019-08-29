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

    public bool lerping;
    float stepSpeed;
    const float steptime = 0.015625f;
    Vector2 targetPosition;
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        spriteController = GetComponent<SpriteRenderer>();
        canMove = true;
        maxTime = timeTllNextInput;
        stepSpeed = 5;
        timeTllNextInput = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        if(canMove)
        {
            if(timeTllNextInput<=0)
            {
                movePlayer();
            }
            timeTllNextInput-=Time.deltaTime;
            if(lerping)
            {
                playerTransform.position = Vector2.MoveTowards(transform.position, targetPosition, stepSpeed * steptime);
                if(((Vector2)playerTransform.position - targetPosition).magnitude < 0.1)
                {
                    lerping = false;
                    playerTransform.position = targetPosition;
                }
            }
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
                takenInput = false;

            }
        }

        if(takenInput)
        {
            if(alterPos.x >0)
            {
                spriteController.flipX = false;
                Debug.Log(alterPos.x);
            }
            if(alterPos.x <0)
            {
                spriteController.flipX = true;
                Debug.Log(alterPos.x);
            }

            //playerTransform.Translate(alterPos);
            Vector3 checkPos = new Vector3(playerTransform.position.x + alterPos.x, playerTransform.position.y + alterPos.y,
             playerTransform.position.z);
            
            checkPos.x = Mathf.FloorToInt(checkPos.x) + 0.5f;
            checkPos.y = Mathf.FloorToInt(checkPos.y) + 0.5f;
            targetPosition = checkPos;
            lerping = true;
            //playerTransform.position = Vector2.MoveTowards(transform.position, checkPos, maxTime);

            timeTllNextInput = maxTime;
            GameManager.instance?.Step(this);

        }

        
    }
}
