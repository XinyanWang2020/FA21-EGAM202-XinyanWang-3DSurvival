using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RabbitBrain : MonoBehaviour
{
    public enum RabbitStateT
    {
        DecidingWhatToDoNext,
        SeekingWater, MovingToWater,DrinkingTillFull,
        SeekingFood, MovingToFood, EattingTillFull,
        Clone,
        Resting,
    }

    public float Water = 50, WaterLostPerSecond = 1, DrinkingSpeed = 10;
    public float Food = 80, FoodLostPerSecond = 1, EatingSpeed = 10;
    public float MaxDispersalDistance;

    public RabbitStateT currentState;

    public GameObject RabbitPrefab;

    NavMeshAgent thisNavMeshAgent;
    void Start()
    {
        float terrinHeight = GameObject.Find("Terrain").GetComponent<Terrain>().SampleHeight(transform.position);

        Water = Random.Range(40f, 60f);
        Food = Random.Range(40f, 60f);
        thisNavMeshAgent = GetComponent<NavMeshAgent>();

        currentState = RabbitStateT.DecidingWhatToDoNext;

        transform.position = new Vector3(transform.position.x, terrinHeight, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //Sun water and food levels decrease
        Water -= WaterLostPerSecond * Time.deltaTime;
        Food -= FoodLostPerSecond * Time.deltaTime;

        //Dose sun have enough food and water to live?
        if (Food < 0 || Water <0)
        {
            Destroy(this.gameObject);
        }

        switch (currentState)
        {
            case RabbitStateT.DecidingWhatToDoNext:
                DecideWhatToDoNext();
                break;
            case RabbitStateT.SeekingWater:
                SeekWater();
                break;
            case RabbitStateT.MovingToWater:
                //nothing needs to do
                break;
            case RabbitStateT.DrinkingTillFull:
                DrinkFromWater();
                break;
            case RabbitStateT.SeekingFood:
                SeekFood();
                break;
            case RabbitStateT.MovingToFood:
                //nothing need to do
                break;
            case RabbitStateT.EattingTillFull:
                EatFromFood();
                break;
            case RabbitStateT.Clone:
                CloneItself();
                break;


            default:
                throw new System.NotImplementedException("Whoops. Ran off the end of the case statement....");
        }
    }

    public void DecideWhatToDoNext()
    {
        if (Water < 50)
        {
            currentState = RabbitStateT.SeekingWater;
            return;
        }

        if (Food < 50)
        {
            currentState = RabbitStateT.SeekingFood;
            return;
        }

        if(Food > 120 && Water > 80)
        {
            currentState = RabbitStateT.Clone;
            return;
        }

    }

    public void SeekWater()
    {
        //GameObject[] wateryObjects = GameObject.FindGameObjectsWithTag("Water");
        GameObject targetWateryObject = FindClosestObjectWithTag("Water");
        //Debug.Log("Rabbit is going to" + targetWateryObject.name);

        //change state to "movingToWater"
        thisNavMeshAgent.SetDestination(targetWateryObject.transform.position);
        currentState = RabbitStateT.MovingToWater;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Rabbit EnterTrigger");
        if(currentState == RabbitStateT.MovingToWater && other.tag == "Water")
        {
            currentState = RabbitStateT.DrinkingTillFull;
        }

        if (currentState == RabbitStateT.MovingToFood && other.tag == "Food")
        {
            currentState = RabbitStateT.EattingTillFull;
        }
    }

    public void DrinkFromWater()
    {
        Water += DrinkingSpeed * Time.deltaTime;

        if (Water > 120)
            currentState = RabbitStateT.DecidingWhatToDoNext;
    }

    public void SeekFood()
    {
        GameObject targetFoodObject = FindClosestObjectWithTag("Food");

        //change state to "movingToFood"
        thisNavMeshAgent.SetDestination(targetFoodObject.transform.position);
        currentState = RabbitStateT.MovingToFood;
    }

    public void EatFromFood()
    {
        Food += EatingSpeed * Time.deltaTime;

        if (Food > 150)
            currentState = RabbitStateT.DecidingWhatToDoNext;
    }

    public void CloneItself()
    {
        Vector3 randomNearbyPosition;

        randomNearbyPosition = transform.position + MaxDispersalDistance * Random.insideUnitSphere;

        //place a new Rabbit at that place
        GameObject newRabbit = Instantiate(RabbitPrefab, randomNearbyPosition, Quaternion.identity, transform.parent);
        newRabbit.GetComponent<RabbitBrain>().Food = 50;

        //lose some food
        Food -= 80;

        if(Food < 100)
        {
            currentState = RabbitStateT.DecidingWhatToDoNext;
        }
    }

    public GameObject FindClosestObjectWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        if (objectsWithTag.Length == 0)
            return null;

        GameObject closestObject = objectsWithTag[0];
        float distanceToClosestObject = 1e6f,
            distanceToCurrentObject;
        for (int i = 0; i < objectsWithTag.Length; i++)
        {
            Vector3 vectorToCurrentObject;
            GameObject currentObject;
            currentObject = objectsWithTag[i];
            vectorToCurrentObject = currentObject.transform.position - transform.position;
            distanceToCurrentObject = vectorToCurrentObject.magnitude;
            if (distanceToCurrentObject < distanceToClosestObject)
            {
                closestObject = objectsWithTag[i];
                distanceToClosestObject = distanceToCurrentObject;
            }
        }
        return closestObject;

    }
}
