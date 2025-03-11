using UnityEngine;

public class PushObject : MonoBehaviour
{
    public float pushForce = 10f; // The force applied to push objects away

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody of the object this script is attached to
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has a Rigidbody
        Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();

        if (otherRigidbody != null)
        {
            // Calculate the direction to push the object away
            Vector3 pushDirection = collision.transform.position - transform.position;

            // Apply force in the direction of the collision
            otherRigidbody.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
        }
    }
}
