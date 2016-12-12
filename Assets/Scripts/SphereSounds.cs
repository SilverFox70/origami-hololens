
using UnityEngine;

public class SphereSounds : MonoBehaviour
{
    AudioSource audioSource = null;
    AudioClip impactClip = null;
    AudioClip rollingClip = null;

    bool rolling = false;

	// Use this for initialization
	void Start ()
    {
        // Add an AudioSource component and set up some default values
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1.0f;
        audioSource.dopplerLevel = 0.0f;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.maxDistance = 20f;

        // Load the sphere sounds from the resources folder
        impactClip = Resources.Load<AudioClip>("Impact");
        rollingClip = Resources.Load<AudioClip>("Rolling");	
	}

    void OnCollisionEnter(Collision collision)
    {
        // Play an impact sound if the sphere impacts strongly enough
        if (collision.relativeVelocity.magnitude >= 0.1f)
        {
            audioSource.clip = impactClip;
            audioSource.Play();
        }
    }

    // Occurs each frame tha this object continues to collide with another object
    void OnCollisionStay(Collision collision)
    {
        Rigidbody rigid = this.gameObject.GetComponent<Rigidbody>();

        // Play a rolling sound if the sphere is rolling fast enough
        if (!rolling && rigid.velocity.magnitude >= 0.01f)
        {
            rolling = true;
            audioSource.clip = rollingClip;
            audioSource.Play();
        }
        // Stop the rolling sound if rolling slows down
        else if (rolling && rigid.velocity.magnitude < 0.01f)
        {
            rolling = false;
            audioSource.Stop();
        }
    }

    // Occurs when the object stops colliding with another object
    void OnCollisionExit(Collision collision)
    {
        // Stop the rolling sound if the object falls off and stop colliding
        if (rolling)
        {
            rolling = false;
            audioSource.Stop();
        }
    } 

    // Update is called once per frame
    //void Update () {
		
	//}
}
