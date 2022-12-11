using UnityEngine;

public class Sphere : MonoBehaviour
{
    private Rigidbody RigidBody;
    private AudioSource HitWallSound;
    private AudioSource HitGateSound;
    private AudioSource HitCheckpointSound;
    private Vector3 jump = Vector3.up * 200;
    private Vector3 forceDirection;

    private const float FORCE_AMPL = 2;

    [SerializeField]
    private Camera Camera;

    void Start()
    {
        RigidBody = GetComponent<Rigidbody>();

        var audioSources = GetComponents<AudioSource>();
        HitWallSound = audioSources[0];
        HitGateSound = audioSources[1];
        HitCheckpointSound = audioSources[2];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RigidBody.AddForce(jump);
        }
        float fx = Input.GetAxis("Horizontal");
        float fy = Input.GetAxis("Vertical");

        forceDirection = Camera.transform.forward;
        forceDirection.y = 0;
        forceDirection = forceDirection.normalized * fy;
        forceDirection += Camera.transform.right * fx;

        RigidBody.AddForce(forceDirection * FORCE_AMPL);
    }

    private void OnTriggerEnter(Collider other)
    {
        HitCheckpointSound.volume = GameMenu.SoundsVolume;  
        HitCheckpointSound.Play();
        Destroy(other.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (GameMenu.SoundsEnabled)
        {
            AudioSource sound = other.gameObject.tag switch
            {
                "Wall" => HitWallSound,
                "Gate" => HitGateSound,
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
