using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Item
{
    public int currentItemIndex;

    public override void Eat()
    {
        //increase the size of the avatar
        transform.parent.transform.localScale *= 1.5f;
        Debug.Log("You eat the carrot, the world looks smaller, the rabbits won't be happy with you.");

        //remove from avatar
        transform.parent.GetComponent<AvatarController>().Inventory[currentItemIndex] = null;

        //destroy itself
        Destroy(this.gameObject);
    }
}
