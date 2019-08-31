using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    public Animator titleScreen;

    bool transitionStarted = false;

    private void Start()
    {
        GameManager.instance.player.canMove = false;
    }

    private void FixedUpdate()
    {
        if(titleScreen.GetCurrentAnimatorStateInfo(0).IsName("hold") && !transitionStarted && Input.anyKey)
        {
            titleScreen.SetBool("zoom", true);
            transitionStarted = true;
            StartCoroutine(WaitThenDisable());
        }
    }

    IEnumerator WaitThenDisable()
    {

        yield return new WaitForSeconds(2.0f);
        titleScreen.transform.parent.gameObject.SetActive(false);

        GameManager.instance.transition.BeginHide();
        GameManager.instance.player.canMove = true;
        AudioManager.Instance.TransitionToOverworldBGM();
    }
}
