using UnityEngine;

public class Player : MonoBehaviour
{
    // singleton
    public static Player Instance;

    // player settings
    [SerializeField] private float PLAYER_SPEED = 5f;
    private float PLAYER_GRAVITY = -9.81f;
    private float X_ROTATION = 0f;
    // camera sens
    [SerializeField] private float CAMERA_SENSITIVITY = 3f;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private float INTERACT_DISTANCE = 5f;

    private CharacterController cc;
    private Vector3 velocity;
    private Transform cameraTransform;

    private PlayerMovement movement = new PlayerMovement();
    private PlayerCamera cam = new PlayerCamera();
    private PlayerInteraction interaction = new PlayerInteraction();

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

    }

    void Start()
    {
        cc = GetComponent<CharacterController>();
        cameraTransform = mainCamera.transform;

        // init
        movement.Init(cc, cameraTransform, velocity, PLAYER_SPEED, PLAYER_GRAVITY);
        cam.Init(CAMERA_SENSITIVITY, X_ROTATION, cameraTransform);
        interaction.Init(mainCamera, interactableMask, INTERACT_DISTANCE);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        cam.Tick();
        interaction.Tick();
    }

    void FixedUpdate()
    {
        movement.FixedTick();
    }
}