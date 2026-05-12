using UnityEngine;

public class Player : MonoBehaviour
{
    // singleton
    public static Player Instance;

    public enum State
    {
        IsPlaying,
        IsInDialogue,
        IsLoading,
        IsViewing,
        IsDisabled,
    }

    public State currentState { get; set; }= State.IsPlaying;

    [SerializeField] private CharacterController cc;
    [SerializeField] private Camera mainCamera;

    // player settings
    [SerializeField] private float PLAYER_SPEED = 5f;
    private float PLAYER_GRAVITY = -9.81f;
    private float X_ROTATION = 0f;
    // camera sens
    [SerializeField] private float CAMERA_SENSITIVITY = 3f;

    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private float INTERACT_DISTANCE = 5f;
    [SerializeField] private GameObject prompt;

    private Transform cameraTransform;

    private PlayerMovement movement = new PlayerMovement();
    private PlayerCamera cam = new PlayerCamera();
    private PlayerInteraction interaction = new PlayerInteraction();

    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitializePlayerController();
    }

    //Initializes Player references and defaults. If fail to find references, return
    public void InitializePlayerController()
    {
        cc = PlayerReference.Instance.cc;
        mainCamera = PlayerReference.Instance.cam;

        if (cc == null && mainCamera == null)
        {
            SetState(State.IsDisabled);
            return;
        }
        
        cameraTransform = mainCamera.transform;

        // Init
        movement.Init(cc, cameraTransform, PLAYER_SPEED, PLAYER_GRAVITY);
        cam.Init(CAMERA_SENSITIVITY, X_ROTATION, cameraTransform);
        interaction.Init(mainCamera, interactableMask, prompt, INTERACT_DISTANCE);

        SetState(State.IsPlaying);
    }

    void Update()
    {
        switch (currentState)
        {
            case State.IsPlaying:
                PlayingTick();
                break;
        }
    }

    private void PlayingTick()
    {
        cam.Tick();
        interaction.Tick();
        movement.Tick();

    }

    public void SetState(State newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case State.IsPlaying:

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                break;

            case State.IsInDialogue:

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                break;
            
            case State.IsLoading:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            
            case State.IsViewing:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
        
        //Place this somewhere decent
        prompt.SetActive(false);
    }
}