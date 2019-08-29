using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    public Animator titleScreen;

    bool transitionStarted = false;

    private void FixedUpdate()
    {
        if(!transitionStarted && Input.anyKey)
        {
            titleScreen.SetBool("zoom", true);
            transitionStarted = true;
            StartCoroutine(WaitThenDisable());
        }
    }

    IEnumerator WaitThenDisable()
    {

        yield return new WaitForSeconds(2.0f);
        titleScreen.gameObject.SetActive(false);
    }
}
