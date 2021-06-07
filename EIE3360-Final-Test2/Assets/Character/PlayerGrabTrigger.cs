using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabTrigger : MonoBehaviour
{
    public float GrabRadius = 0.3f;
    public Rigidbody GrabbedObject;
    public AudioClip ChopTreeClip;
    public AudioClip GrabShroomSound;
    public AudioClip GrabSound;
    AudioSource GrabAudio;
    private int grabLayerMask;
    //Playerhp
    GameObject fire;
    PlayerHealth firehp;

    private void Start()
    {
        grabLayerMask = LayerMask.GetMask("Tree", "Mushroom","Woods","Enemy");
        fire = GameObject.FindGameObjectWithTag("Fire");
        firehp = fire.GetComponent<PlayerHealth>();
        GrabAudio = GetComponent<AudioSource>();
    }

    public void Grab()
    {
        Collider[] inGrabTrigger = Physics.OverlapSphere(transform.position, GrabRadius, grabLayerMask);
        if (inGrabTrigger.Length > 0)
        {
            Rigidbody target = inGrabTrigger[0].attachedRigidbody;
            if (target.gameObject.layer == LayerMask.NameToLayer("Tree") && target.isKinematic)
            {
                // That's static tree, cut it 
                target.isKinematic = false;
                target.transform.SetParent(null, true);
                target.AddForceAtPosition(
                    (transform.position - target.position).normalized * 15,
                    target.centerOfMass + Vector3.up * 3);
                GrabAudio.clip = ChopTreeClip;
                GrabAudio.Play();
            }
            else if (target.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                //any enemy detected    
                EnemyHealth hp = target.gameObject.GetComponent<EnemyHealth>();
                hp.TakeDamage(100);
            }
            else if (!target.isKinematic && (target.gameObject.layer != LayerMask.NameToLayer("Enemy")) || target.gameObject.layer == LayerMask.NameToLayer("Woods") || target.gameObject.layer == LayerMask.NameToLayer("Mushroom"))
            {
                //any pickkable object
                GrabbedObject = target;
                GrabbedObject.isKinematic = true;
                GrabbedObject.transform.SetParent(transform, true);
                GrabbedObject.transform.Translate(-transform.forward * 0.5f + transform.up, Space.World);
                if (target.gameObject.layer == LayerMask.NameToLayer("Mushroom"))
                {
                    //if mushroom
                    firehp.Heal(0.4f);
                    Destroy(target.gameObject);
                    GrabAudio.clip = GrabShroomSound;
                    GrabAudio.Play();
                }
                else
                {
                    GrabAudio.clip = GrabSound;
                    GrabAudio.Play();
                }
            }
        }
    }

    public void Release()
    {
        if (GrabbedObject != null)
        {
            GrabbedObject.transform.SetParent(null, true);
            GrabbedObject.isKinematic = false;
            GrabbedObject = null;
        }
    }
}
