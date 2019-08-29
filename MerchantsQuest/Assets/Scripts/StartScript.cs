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

        yield return new WaitForSeconds(1.0f);
        CameraManager.instance.ShowTransitionCam();
        yield return new WaitForSeconds(1.0f);
        titleScreen.transform.parent.gameObject.SetActive(false);

        GameManager.instance.transition.BeginHide();
    }
}
