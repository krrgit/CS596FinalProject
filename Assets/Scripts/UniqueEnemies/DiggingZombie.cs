using UnityEngine;

public class DiggingZombie : MonoBehaviour
{
    // Speed of the upward movement
    public float speed = 1.0f;

    // Y position at which to stop moving upwards
    public float stopY = 0.0f;

    // Tags
    public string playerTag = "Player";
    public string wallTag = "Wall";

    private Rigidbody rb;

    private Animator anim;
    private bool isAttacking = false;

    void Start()
    {
        // Set the initial position to a specific Y position
        transform.position = new Vector3(transform.position.x, -0.75f, transform.position.z);

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        GameObject target = null;

        // Check if player or wall is nearby
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        GameObject wall = GameObject.FindGameObjectWithTag(wallTag);

        if (player != null)
            target = player;
        else if (wall != null)
            target = wall;

        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            // If the object is close enough to the target and hasn't reached the desired height
            if (distanceToTarget < 5.0f)
            {
                Fetch();
            }

            if (distanceToTarget < 1.0f)
            {
                anim.SetBool("isAttacking", true);
            }
            else
            {
                anim.SetBool("isAttacking", false);
            }
        }
    }

    void Fetch()
    {
        rb.isKinematic = true;
        // Calculate the movement in the vertical direction
        float verticalMovement = speed * Time.deltaTime;

        // Update the position only in the vertical direction
        transform.position += Vector3.up * verticalMovement;
        

        // Check if the object has reached the stop Y position
        if (transform.position.y >= stopY)
        {
            rb.isKinematic = false;
            // Set the position to stopY to prevent further upward movement
            transform.position = new Vector3(transform.position.x, stopY, transform.position.z);
        }
    }
}
