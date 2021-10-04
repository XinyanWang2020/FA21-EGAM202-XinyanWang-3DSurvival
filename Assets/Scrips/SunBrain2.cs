using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBrain2 : MonoBehaviour
{
    public Animator anim;
    public enum SunfieldStateT { Seed, Seeding, Adult, Grow, Flowering, Dead }
    public SunfieldStateT currentState;

    public int SunfieldID;
    public float Food, SeedFood, FoodGainedPerSecond, MaxFood;
    public float CrowdingDistance, MaxDispersalDistance;
    public float BirthTime, Age;

    public GameObject SunfieldPrefab;

    private Collider[] sunfieldHits;
    void Start()
    {
        anim = GameObject.Find("SunfiledPrefab").GetComponent<Animator>();

        float terrinHeight = GameObject.Find("Terrain").GetComponent<Terrain>().SampleHeight(transform.position);

        sunfieldHits = Physics.OverlapSphere(transform.position, CrowdingDistance, LayerMask.GetMask("Sun"));
        if (sunfieldHits.Length > 1)
        {
            gameObject.SetActive(false);
            Object.Destroy(this.gameObject);

            return;
        }
        BirthTime = Time.time;
        Age = 0;
        currentState = SunfieldStateT.Seed;
        //transform.localScale = new Vector3(1, 1, 1);

        transform.position = new Vector3(transform.position.x, terrinHeight, transform.position.z);

        Food = SeedFood;

        anim.SetTrigger("Restart");
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case SunfieldStateT.Seed:
                SeedUpdate();
                break;
            case SunfieldStateT.Seeding:
                SeedingUpdate();
                break;
            case SunfieldStateT.Adult:
                AdultUpdate();
                break;
            case SunfieldStateT.Flowering:
                FloweringUpdate();
                break;
            case SunfieldStateT.Dead:
                DeadUpdate();
                break;
        }
    }

    public void SeedUpdate()
    {
        Food += FoodGainedPerSecond * Time.deltaTime;

        Age = Time.time - BirthTime;

        if (Age > 5)
        {
            //grow into seeding
            currentState = SunfieldStateT.Seeding;
        }
    }

    public void SeedingUpdate()
    {
        Food += FoodGainedPerSecond * Time.deltaTime;

        Age = Time.time - BirthTime;

        if (Age > 10)
        {
            anim.SetTrigger("Seeding");
            currentState = SunfieldStateT.Adult;
        }
    }

    public void AdultUpdate()
    {
        Food += FoodGainedPerSecond * Time.deltaTime;

        Age = Time.time - BirthTime;

        if (Food > MaxFood)
        {
            anim.SetTrigger("Adult");

            Vector3 randomNearbyPosition;

            randomNearbyPosition = transform.position + MaxDispersalDistance * Random.insideUnitSphere;

            //place a new sun at that place
            GameObject newSunfield = Instantiate(SunfieldPrefab, randomNearbyPosition, Quaternion.identity, transform.parent);
            newSunfield.GetComponent<SunBrain>().Food = SeedFood;
            newSunfield.GetComponent<SunBrain>().Age = 0;


            //Lose food
            Food -= 2f * SeedFood;
        }
        if (Age > 40)
        {
            currentState = SunfieldStateT.Flowering;
        }
    }

    public void FloweringUpdate()
    {
        Food += FoodGainedPerSecond * Time.deltaTime;

        Age = Time.time - BirthTime;

        if (Age > 50 && Food > 50)
        {
            anim.SetTrigger("Flowering");

            currentState = SunfieldStateT.Dead;
        }
    }

    public void DeadUpdate()
    {
        Food += FoodGainedPerSecond * Time.deltaTime;

        Age = Time.time - BirthTime;

        if (Age > 60)
        {
            anim.SetTrigger("Dead");

            //destory this sun after 3 second
            Destroy(gameObject, 3);
        }
    }
}
