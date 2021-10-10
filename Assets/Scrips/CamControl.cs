using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public static CamControl Instance = null;

    private Vector3 dirVector3;
    private Vector3 rotaVector3;
    private float paramater = 1.0f;
    //��ת����
    private float xspeed = 50;
    private float yspeed = 50;

    private float dis;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rotaVector3 = transform.localEulerAngles;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //��ת
        if (Input.GetMouseButton(1))
        {
            rotaVector3.y += Input.GetAxis("Horizontal") * yspeed;
            rotaVector3.x += Input.GetAxis("Vertical") * xspeed;
            transform.rotation = Quaternion.Euler(rotaVector3);
        }

        //�ƶ�
        dirVector3 = Vector3.zero;

        if (Input.GetKey(KeyCode.Keypad5))
        {
            if (Input.GetKey(KeyCode.LeftShift)) dirVector3.z = 3;
            else dirVector3.z = 1;
        }
        if (Input.GetKey(KeyCode.Keypad0))
        {
            if (Input.GetKey(KeyCode.LeftShift)) dirVector3.z = -3;
            else dirVector3.z = -1;
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            if (Input.GetKey(KeyCode.LeftShift)) dirVector3.x = -3;
            else dirVector3.x = -1;
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            if (Input.GetKey(KeyCode.LeftShift)) dirVector3.x = 3;
            else dirVector3.x = 1;
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            if (Input.GetKey(KeyCode.LeftShift)) dirVector3.y = -3;
            else dirVector3.y = -1;
        }
        if (Input.GetKey(KeyCode.Keypad8))
        {
            if (Input.GetKey(KeyCode.LeftShift)) dirVector3.y = 3;
            else dirVector3.y = 1;
        }
        transform.Translate(dirVector3 * paramater, Space.Self);
    }
}
