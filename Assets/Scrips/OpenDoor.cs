using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour

{
    public GameObject Button;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("Door").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Open");
        }
    }
}
