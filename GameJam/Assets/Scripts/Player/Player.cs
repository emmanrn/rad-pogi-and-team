using UnityEngine;

public class Player : MonoBehaviour
{
    // singleton
    public static Player Instance;

    // player settings
    [SerializeField] private float PLAYER_SPEED = 5f;

    // camera sens
    [SerializeField] private float CAMERA_SENSITIVITY = 3f;
    private float X_ROTATION = 0f;

    private CharacterController cc;
    [SerializeField] private Transform cameraTransform;


    private PlayerMovement movement = new PlayerMovement();
    private PlayerCamera cam = new PlayerCamera();

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        cc = GetComponent<CharacterController>();

        // init
        movement.Init(cc, cameraTransform, PLAYER_SPEED);
        cam.Init(CAMERA_SENSITIVITY, X_ROTATION, cameraTransform);
    }

    void Update()
    {
        cam.Tick();
    }

    void FixedUpdate()
    {
        movement.FixedTick();
    }
}