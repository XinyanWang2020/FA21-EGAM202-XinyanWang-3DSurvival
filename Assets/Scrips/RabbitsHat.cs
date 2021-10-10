using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitsHat : Item
{
    public int currentItemIndex;

    public GameObject Hat;

    public override void Wear()
    {
        //activate the ture hat
        Hat.gameObject.SetActive(true);
        Debug.Log("Now you are one of the rabbits.");

        //remove from avatar
        transform.parent.GetComponent<AvatarController>().Inventory[currentItemIndex] = null;

        //destroy itself
        Destroy(this.gameObject);
    }
}
