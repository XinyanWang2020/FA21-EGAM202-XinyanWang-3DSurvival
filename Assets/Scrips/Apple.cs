using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Item
{
    public int currentItemIndex;
    public override void Eat()
    {
        //increase the size of the avatar
        transform.parent.transform.localScale *= 0.5f;
        Debug.Log("You eat the apple, the world looks bigger, now you need to run faster.");

        //remove from avatar
        transform.parent.GetComponent<AvatarController>().Inventory[currentItemIndex] = null;

        //destroy itself
        Destroy(this.gameObject);
    }
}
