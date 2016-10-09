using UnityEngine;
using System.Collections;

public class WaterDetector : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D Hit)
    {
        if (Hit.GetComponent<Rigidbody2D>() != null)
        {
            transform.parent.GetComponent<WaterPhysicsYAxis>().Splash(transform.position.y, Hit.GetComponent<Rigidbody2D>().velocity.x * Hit.GetComponent<Rigidbody2D>().mass / 40f);
        }
    }

}