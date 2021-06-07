using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //vars to be activated
    [SerializeField]
    PlayerHealth fire;
    [SerializeField]
    GameObject controlUI;
    [SerializeField]
    GameObject managers;
    [SerializeField]
    GameObject UI;
    //default
    public Transform[] views;
    public float transitionSpeed;
    Transform currentView;
    Transform tempPos;

    public void ChangeToMain()
	{
        //sets the value
        currentView = views[0];
        tempPos = currentView;
    }
    void LateUpdate()
    {
		if (currentView != null)
		{
            //if the menu camera still hasnt reached the play camera -> move it and rotate it
			if (transform.position != tempPos.position&&transform.rotation!=tempPos.rotation)
			{
                transform.position = Vector3.Lerp(transform.position, tempPos.position, Time.deltaTime * transitionSpeed);
                //ransform.rotation = currentView.transform.rotation;
                Vector3 currentAngle = new Vector3(
                    Mathf.LerpAngle(transform.rotation.eulerAngles.x, tempPos.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
                    Mathf.LerpAngle(transform.rotation.eulerAngles.y, tempPos.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
                    Mathf.LerpAngle(transform.rotation.eulerAngles.z, tempPos.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));
                transform.eulerAngles = currentAngle;
            }
			else
			{
                //disable the menu camera, enable the play camera, start the game
                gameObject.SetActive(false);
                fire.EnableStop();
                controlUI.SetActive(true);
                managers.SetActive(true);
                UI.SetActive(true);
            }
        }
    }
}
