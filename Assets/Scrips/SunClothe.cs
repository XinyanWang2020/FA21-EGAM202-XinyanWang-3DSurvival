using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunClothe : Item
{
    public int currentItemIndex;
    public GameObject Avatar;

    public Renderer rend;
    public Material MaterialForAvatar;
    void Start()
    {

    }
    public override void Wear()
    {
        rend.material = MaterialForAvatar;
        rend = Avatar.GetComponent<Renderer>();
        Debug.Log("Now you are shiny like a sun.");

        //remove from avatar
        transform.parent.GetComponent<AvatarController>().Inventory[currentItemIndex] = null;

        //destroy itself
        Destroy(this.gameObject);
    }
}
