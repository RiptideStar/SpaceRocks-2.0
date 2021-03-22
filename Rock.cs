using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    float movementSpeed = 5; //Diffculty!!!!
    float randomDirectionX;
    float randomDirectionY;
    float rotationSpeed;

    private MainLogic mainRef;

    public MainLogic MainRef
    {
        set { mainRef = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        randomDirectionX = Random.Range(-1, 1); //direction and scale
        randomDirectionY = Random.Range(-1, 1);
        rotationSpeed = Random.Range(-360, 360);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        transform.position += new Vector3(randomDirectionX * movementSpeed * Time.deltaTime, randomDirectionY * movementSpeed * Time.deltaTime,
            0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT!");
        if (other.gameObject.GetComponent<Bullet>() || other.gameObject.GetComponent<Ship>())
        {
            Destroy(other.gameObject); //destroy(other);
            Explode();
        }
    }
    void Explode()
    {
        // 1 0.5 0.25
        // check the size of the rock
        if (transform.localScale.x > 0.5f)
            mainRef.BreakRock(2, transform.position, 0.5f);
        else if (transform.localScale.x <= 0.5f && transform.localScale.x > 0.25f)
        {
            mainRef.BreakRock(3, transform.position, 0.25f);
        }
        mainRef.updateScore(1);
        mainRef.rocks.Remove(this);
        Destroy(gameObject);
    }
}