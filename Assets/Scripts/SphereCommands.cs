using UnityEngine;

public class SphereCommands : MonoBehaviour
{
    Vector3 originalPosition;

    // Use this for Initialization
    private void Start()
    {
        // Grab the original local position of the sphere when the app starts
        originalPosition = this.transform.localPosition;
    }

	// Called by GazeGestureManager when cursor performs a Select gesture
    void OnSelect()
    {
        // If the sphere has no Rigidbody component, add one to enable physics
        if (!this.GetComponent<Rigidbody>())
        {
            var rigidbody = this.gameObject.AddComponent<Rigidbody>();
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }

    // Called by SpeechManager when the user says the "Reset World" command
    void OnReset()
    {
        // if the sphere has a rigidbody component, remove it to disable physics
        var rigidbody = this.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            DestroyImmediate(rigidbody);
        }

        // Put the sphere back in its original place
        this.transform.localPosition = originalPosition;
    }

    void OnDrop()
    {
        // Just do the same logic as a Select gesture
        OnSelect();
    }
}
