using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ship : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;

    private int playerScore = 0;
    private float lives = 3;
    float movementSpeed = 5;
    private float rotationSpeed = 200;
    private const float dragAmount = 0.998f;


    Vector3 driftVector = new Vector3(1, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Drift 
        transform.position += driftVector * Time.deltaTime; // distance/s
        driftVector *= dragAmount;

        if (Input.GetKey(KeyCode.A))
        {
            //Go left
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            //Strafe left
            //transform.position += new Vector3(-2 * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //Go right
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            thrust();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void thrust()
    {
        driftVector += transform.up * (movementSpeed * Time.deltaTime);
    }
    void Fire()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = this.transform.position;
        bullet.transform.localRotation = this.transform.localRotation;
    }
}
