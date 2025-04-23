using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraCtrl : MonoBehaviour
{
    public float vMouseSpeed = 1.0f;
    public float hMouseSpeed = 1.0f;

    public GameObject lookAtTarget = null;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            lookAtTarget = null;
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                float vMove = Input.GetAxis("Mouse Y") * -vMouseSpeed * 0.0166f;
                float hMove = Input.GetAxis("Mouse X") * -hMouseSpeed * 0.0166f;
                transform.localEulerAngles -= new Vector3(vMove, hMove, 0);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                if(!hit.transform.tag.Equals("Map"))
                    lookAtTarget = hit.transform.gameObject;
            }
        }

        float fVertical = 0;
        float fHorizontal = 0;
        float fZoom = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetKey(KeyCode.W))
        {
            fVertical = 0.1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            fVertical = -0.1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            fHorizontal = 0.1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            fHorizontal = -0.1f;
        }

        if (fZoom != 0)
        {
            transform.Translate(Vector3.forward * fZoom);
        }

        if (fVertical != 0)
        {
            if (lookAtTarget == null)
            {
                Vector3 vTranslation = transform.forward * fVertical;
                vTranslation.y = 0.0f;
                transform.position += vTranslation;
            }
            else
            {
                lookAtTarget = null;
            }
        }

        if (fHorizontal != 0)
        {
            if (lookAtTarget == null)
            {
                Vector3 vTranslation = transform.right * fHorizontal;
                vTranslation.y = 0.0f;
                transform.position += vTranslation;
            }
            else
            {
                lookAtTarget = null;
            }
        }

        if (lookAtTarget != null)
        {
            transform.LookAt(lookAtTarget.transform.position);
        }
    }
}
