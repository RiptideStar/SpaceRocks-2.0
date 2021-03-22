using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 10;
    float killDistance = 10;
    float distanceTraveled = 0; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;

        //distance = speed * time
        distanceTraveled += speed * Time.deltaTime;

        if (distanceTraveled > killDistance)
        {
            Destroy(gameObject);
        }
    }
}