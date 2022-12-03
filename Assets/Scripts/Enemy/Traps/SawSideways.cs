using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSideways : MonoBehaviour
{
    [SerializeField] private float damage;//creates a designer chosen amount of damage the saw can do
    [SerializeField] private float mSpeed;//creates a designer chosen speed the saw can move at
    [SerializeField] private float mDistance;//creates a designer chosen distance the saw can move
    private bool moveingLeft;//a bool checking if the saw is moving left
    private float leftEdge;//creates a variable that will hold the where the left edge of the saw is 
    private float rightEdge;//creates a variable that will hold the where the right edge of the saw is 

    private void Awake()
    {
        leftEdge = transform.position.x - mDistance;//assigns the leftSide variable by taking the position and subtracting it by the distance it can move
        rightEdge = transform.position.x + mDistance;//assigns the right edge variable by taking the position and adding it by the distance it can move
    }
    private void Update()
    {
        if (moveingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - mSpeed *Time.deltaTime, transform.position.y, transform.position.z);
                //if the current position is greater than the left edge then it needs to move left by taking the current position and going in the negative direction
                //by mSpeed * the change in time each frame
            }
            else
                moveingLeft = false;//sets the bool movingLeft to false
        }
        else
        {

            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + mSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                //if the current position is greater than the left edge then it needs to right by taking the current position and going in the positive direction
                //by mSpeed * the change in time each frame
            }
            else
                moveingLeft = true;//sets the bool movingLeft to true
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().healthChange(damage);//if something with the "Player" tag touches the saw then call the HealthChange function
                                                                //using the damage that is assigned to the saw
        }
    }
}
