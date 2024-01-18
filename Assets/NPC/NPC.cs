using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPC : MonoBehaviour, IHit
{
    GameObject target; // Reference to the target object
    Sight sight; // Reference to the Sight component
    PlayerMovement playerMovement; // Reference to the PlayerMovement component
    public float rotationRate = 180f; // Rate at which the NPC rotates towards the target
    public UnityEvent OnTagPlayer = new UnityEvent();
    Animator animator;

    void Awake()
    {
        sight = GetComponentInChildren<Sight>(); // Get the Sight component attached to the NPC
        playerMovement = GetComponent<PlayerMovement>(); // Get the PlayerMovement component attached to the NPC
        target = GameObject.FindWithTag("Player"); // Find the target object with the "Player" tag
        sight.target = target; // Assign the target object to the Sight component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (sight.CanSeeTarget(sight.target, sight.range, sight.angle)) // If the NPC can see the target
        {
            Vector3 lookDirection = Vector3.ProjectOnPlane(target.transform.position - transform.position, Vector3.up); // Calculate the direction to look at the target
            Quaternion newRotation = Quaternion.LookRotation(lookDirection); // Create a rotation towards the target
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotationRate * Time.deltaTime); // Rotate the NPC towards the target
            playerMovement.moveDirection = target.transform.position - transform.position; // Set the movement direction towards the target
        }
        else
        {
            playerMovement.moveDirection = Vector3.zero; // If the NPC cannot see the target, stop moving
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target) // If the NPC collides with the target
        {
            Debug.Log("Collided with target");
            OnTagPlayer.Invoke();
            // Log a message indicating the collision with the target
        }
    }
    public void Hit(GameObject other)
    {
        Debug.Log($"I {gameObject.name} got hit by {other.name}");
        animator.SetTrigger("Hit");
    }
}