using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBrain : MonoBehaviour
{
    public enum SunfieldStateT { Seed, Seeding, Adult, Flowering, Dead }
    public SunfieldStateT currentState;

    public int SunfieldID;
    public float Food, SeedFood, FoodGainedPerSecond, MaxFood;
    public float CrowdingDistance, MaxDispersalDistance;
    public float BirthTime, Age;

    public GameObject SunfieldPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
