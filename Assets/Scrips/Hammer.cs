using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Item
{
    public int currentItemIndex;

    public GameObject Things;

    public float MagicPoints = 3;
    public float MaxDispersalDistance;

    public override void Use()
    {
        GetComponent<Animator>().SetTrigger("Smash");
    }

    void Update()
    {
        if (MagicPoints <0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Rabbit"))
        {
            Vector3 NearbyPosition;
            NearbyPosition = transform.position + MaxDispersalDistance * Random.insideUnitSphere;

            float terrainHeight = GameObject.Find("Terrain").GetComponent<Terrain>().SampleHeight(NearbyPosition);

            NearbyPosition = new Vector3(NearbyPosition.x, terrainHeight, NearbyPosition.z);
            GameObject newRabbit = Instantiate(Things, NearbyPosition, Quaternion.identity);
            MagicPoints -= 1;
            Debug.Log("The magic from the hammer creates a new rabbit.");
        }
    }
}
