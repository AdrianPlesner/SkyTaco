using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    public Transform character;

    private Transform m_Camera;
    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Camera.position = new Vector3(0,character.position.y+2,-10);
    }
}
