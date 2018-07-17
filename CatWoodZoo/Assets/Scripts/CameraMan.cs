using UnityEngine;
using System.Collections;
using System;

public class CameraMan : MonoBehaviour {

    public float PanHoriz;
    public float PanVert;

    [Range(0f,1f)]
    public float Speed;

    public bool Angle;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

    }


    void Update () {
        CheckEdges();
        CheckWheel();
    }

    void CheckWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) 
        {
            if (transform.rotation.eulerAngles.x < 60f)
            {
                if (!NearGround())
                {
                    transform.Translate(Vector3.up * -(Speed * 4), Space.World);
                }
               // transform.Rotate(-Vector3.left * 2f);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) 
        {
            
                transform.Translate(Vector3.up * (Speed * 4), Space.World);
              //  transform.Rotate(Vector3.left * 2f);
           
        }
    }

    bool NearGround()
    {
        if (transform.position.y > 3) return false;

        return true;

    }

    void CheckEdges()
    {
        

        if (NearLeft() || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Speed, Space.World);

            if (Angle)
            {
                transform.Translate(Vector3.forward * Speed, Space.World);
            }
        }

        if (NearRight() || Input.GetKey(KeyCode.D))
        {
          
            transform.Translate(Vector3.left * -Speed , Space.World);
            if (Angle)
            {
                transform.Translate(Vector3.forward * -Speed, Space.World);
            }
        }

        if (NearTop() || Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Speed , Space.World);
            if (Angle)
            {
                transform.Translate(Vector3.left * -Speed, Space.World);
            }
        }

        if (NearBottom()||Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * -Speed, Space.World);
            if (Angle)
            {
                transform.Translate(Vector3.left * Speed, Space.World);
            }
         }
    }


    bool NearLeft()
    {

        float pos = Input.mousePosition.x;

        if (pos < 20)
        {

            if (transform.position.x > -PanHoriz)
            {
                return true;
            }
        }
        return false;
    }

    bool NearRight()
    {
        float pos = (Screen.width - Input.mousePosition.x);

        if (pos < 20)
        {
            if (transform.position.x < PanHoriz)
            {
                return true;
            }
        }
        return false;

    }

    bool NearTop()
    {
        float pos = (Screen.height - Input.mousePosition.y);

        if (pos < 20)
        {
            if (transform.position.y < PanVert)
            {
                return true;
            }
        }
        return false;

    }

    bool NearBottom()
    {
        float pos = Input.mousePosition.y;

        if (pos < 20)
        {
            if (transform.position.y > -PanVert)
            {
                return true;
            }
        }
        return false;

    }


}
