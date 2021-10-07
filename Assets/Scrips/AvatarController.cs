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

    }
}
