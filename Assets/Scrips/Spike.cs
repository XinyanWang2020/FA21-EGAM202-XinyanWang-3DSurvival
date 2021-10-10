using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Item
{
 public int currentItemIndex;

    public override void Use()
    {
        GetComponent<Animator>().SetTrigger("Poke");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Sun"))
            Destroy(other.gameObject);
    }
}
