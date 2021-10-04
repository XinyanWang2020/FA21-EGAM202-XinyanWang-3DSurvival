using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RabbitBrainWaterOnly : MonoBehaviour
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
    public float Food = 50, FoodLostPerSecond = 1, EatingSpeed = 10;

    public RabbitStateT currentState;

    NavMeshAgent thisNavMeshAgent;
    void Start()
    {
        Water = Random.Range(40f, 60f);
        Food = Random.Range(40f, 60f);
        thisNavMeshAgent = GetComponent<NavMeshAgent>();

        currentState = RabbitStateT.DecidingWhatToDoNext;
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
            //case RabbitStateT.EattingTillFull:
                //EatFromFood();
                //break;
            //case RabbitStateT.Clone:
                //CloneItself();
                //break;


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

    }

    public void SeekWater()
    {
        GameObject[] wateryObjects = GameObject.FindGameObjectsWithTag("Water");
        GameObject targetWateryObject = wateryObjects[0];
        Debug.Log("Rabbit is going to" + targetWateryObject.name);

        //change state to "movingToWater"
        thisNavMeshAgent.SetDestination(targetWateryObject.transform.position);
        currentState = RabbitStateT.MovingToWater;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Rabbit EnterTrigger");
        if(currentState == RabbitStateT.MovingToWater && other.tag == "Water")
        {
            currentState = RabbitStateT.DrinkingTillFull;
        }
    }

    public void DrinkFromWater()
    {
        Water += DrinkingSpeed * Time.deltaTime;

        if (Water > 100)
            currentState = RabbitStateT.DecidingWhatToDoNext;
    }

    public void SeekFood()
    {

    }
}
