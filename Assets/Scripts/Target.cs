using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Target : MonoBehaviour
{

    public float target_moveSpeed;

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            gameObject.transform.position += new Vector3(target_moveSpeed, 0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
            gameObject.transform.position += new Vector3(-target_moveSpeed, 0, 0);
        if (Input.GetKey(KeyCode.UpArrow))
            gameObject.transform.position += new Vector3(0, 0, target_moveSpeed);
        if (Input.GetKey(KeyCode.DownArrow))
            gameObject.transform.position += new Vector3(0,  0,-target_moveSpeed);

    }

}