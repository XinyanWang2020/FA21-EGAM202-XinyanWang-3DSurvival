using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    public GameObject MouseBox;
    public UnityEngine.AI.NavMeshAgent thisNavMeshAgent;

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

        //Tell Avatar's NavMeshAgent to go to MouseBox
        if (Input.GetMouseButtonDown(0))
            thisNavMeshAgent.SetDestination(MouseBox.transform.position);
    }
}
