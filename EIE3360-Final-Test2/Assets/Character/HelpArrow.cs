using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpArrow : MonoBehaviour
{
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private Transform fire;
    [SerializeField]
    private float StartAppearingRange = 1;
    [SerializeField]
    private float EndAppearingRange = 100;
    //public Image imageComponent;
    public SpriteRenderer imageComponent;
    private void Update()
    {
        //color
        float distance = (Player.position-fire.position).magnitude;
        float a = Mathf.Clamp01((distance - StartAppearingRange) / (EndAppearingRange - StartAppearingRange));
        imageComponent.color = new Color(0.96f, 0.77f, 0.41f, a);
        //rot
        Vector3 targetPosition = fire.transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);
    }
}
