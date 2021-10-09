using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public GameObject MouseBox;
    public Animator thisAnimator;
    public UnityEngine.AI.NavMeshAgent thisNavMeshAgent;

    public Item ItemInHands, ItemOnHead;

    public enum MovementStateT { StandStill, walking }
    public MovementStateT currentState;

    private void Start()
    {
        thisNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        currentState = MovementStateT.StandStill;
    }

    // Update is called once per frame
    void Update()
    {
        Ray rayFromCameraToMouse;
        RaycastHit closestClickableGroundOnRay;

        //Make MouseBox follow mouse pointer
        rayFromCameraToMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(rayFromCameraToMouse, out closestClickableGroundOnRay, Mathf.Infinity, LayerMask.GetMask("ClickableGround"));
        MouseBox.transform.position = closestClickableGroundOnRay.point;

        //Make MouseBox change size based on mouse button state
        if (Input.GetMouseButton(0))
            //When left button is down
            MouseBox.transform.localScale = new Vector3(10, 10, 10);
        else
            //When left button is up
            MouseBox.transform.localScale = new Vector3(5, 5, 5);

        //Tell Avatar's NavMeshAgent to go to MouseBox
        if (Input.GetMouseButtonDown(0))
            thisNavMeshAgent.SetDestination(MouseBox.transform.position);

        //Make animation speed match movement speed
        GetComponent<Animator>().SetFloat("WalkSpeed", .2f * thisNavMeshAgent.velocity.magnitude);

        if(Input .GetKeyDown(KeyCode.G))
        {
            //if already holding a item, do nothing
            if (ItemInHands != null)
                Debug.Log("You can't get anything new, because your hands are full.");
            else
            {
                //look for an item.
                Collider[] overlappingItems;
                //Make sure you adjust vectors to fit the size of your avatar
                overlappingItems = Physics.OverlapBox(transform.position + 2 * Vector3.forward, 3 * Vector3.one, Quaternion.identity,
                    LayerMask.GetMask("Item"));

                //if no items found in front of you
                if (overlappingItems.Length == 0)
                    Debug.Log("There is no items in front of you.");
                else
                {
                    //else handle pick up of item
                    //if have an item in hand, drop it.
                    if(ItemInHands != null)
                    {
                        ItemInHands.transform.SetParent(null);
                        ItemInHands = null;
                    }
                    //then, pick up the first overlapping item
                    ItemInHands = overlappingItems[0].GetComponent<Item>();
                    ItemInHands.transform.SetParent(gameObject.transform);
                    ItemInHands.transform.localPosition = new Vector3(0, 25, 1);
                    Debug.Log("You picked up a " + ItemInHands.name);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //if holding item, drop it.
            if (ItemInHands != null)
            {
                //item will not stay in the air
                ItemInHands.transform.localPosition = new Vector3(0, 0, 1);
                ItemInHands.transform.SetParent(null);
                Debug.Log("You drooped the" + ItemInHands.name);
                ItemInHands = null;
            }
            else
                Debug.Log("You can't drop an item, because you're not holding anything.");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //if holding item, try to use it.
            if (ItemInHands != null)
                ItemInHands.GetComponent<Item>().Use();
            else
                Debug.Log("You can't use an item, because you're not holding anything.");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //if holding item, try to wear it.
            if (ItemInHands != null)
                ItemInHands.GetComponent<Item>().Wear();
            else
                Debug.Log("You can't wear an item, because you're not holdiing anything.");
        }
        if (Input .GetKeyDown(KeyCode.E))
        {
            //if holding item, try to wear it.
            if (ItemInHands != null)
                ItemInHands.GetComponent<Item>().Eat();
            else
                Debug.Log("You can't eat an item, because you're not holding anything.");
        }

    }
}
