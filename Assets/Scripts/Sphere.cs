using UnityEngine;

public class Sphere : MonoBehaviour
{
    private Rigidbody rigidBody;
    private AudioSource hitWallSound;
    private AudioSource hitGateSound;
    private AudioSource hitCheckpointSound;
    private Vector3 jump = Vector3.up * 200;
    private Vector3 forceDirection;

    private const float FORCE_AMPL = 2;

    [SerializeField]
    private Camera Camera;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        var audioSources = GetComponents<AudioSource>();
        hitWallSound = audioSources[0];
        hitGateSound = audioSources[1];
        hitCheckpointSound = audioSources[2];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(jump);
        }
        float fx = Input.GetAxis("Horizontal");
        float fy = Input.GetAxis("Vertical");

        forceDirection = Camera.transform.forward;
        forceDirection.y = 0;
        forceDirection = forceDirection.normalized * fy;
        forceDirection += Camera.transform.right * fx;

        rigidBody.AddForce(forceDirection * FORCE_AMPL);
    }

    private void OnTriggerEnter(Collider other)
    {
        hitCheckpointSound.volume = GameMenu.SoundsVolume;
        hitCheckpointSound.Play();
        Destroy(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (GameMenu.SoundsEnabled)
        {
            AudioSource sound = other.gameObject.tag switch
            {
                "Wall" => hitWallSound,
                "Gate" => hitGateSound,
                _ => null
            };
            
            if (sound != null)
            {
                sound.volume = GameMenu.SoundsVolume;
                sound.Play();
            }
        }
    }
}
