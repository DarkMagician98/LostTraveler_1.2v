using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using Cinemachine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    private const float CROUCH_VALUE = 1.50f;

    [Header("Area Type")]

    [SerializeField] private SceneLevel s_SceneLevel;
    //SERIALIZE FIELD

    [Header("Tutorial")]

    [SerializeField] Ease s_TutorialEase;
   
    [Header("Movement")]
    
    [SerializeField] private float s_Speed;
    [SerializeField] private float s_RollSpeed;
    [SerializeField] private float s_Distance;
    [SerializeField] private float s_DashSpeed;
    [SerializeField] private float s_DashTime;
    [SerializeField] private float s_DashEnergy;
    [SerializeField] private float s_StepOffset;
    [SerializeField] private Vector2 s_FixedVelocity;
    [SerializeField] private Animator s_Animator;
    [SerializeField] private GameObject s_CameraController;
    [SerializeField] private GameObject s_Boss;
    [SerializeField] private GameObject s_StartPoint;

    [Header("UI")]

    [SerializeField] private GameObject s_FlashlightUI;
    [SerializeField] private GameObject s_OverheatUI;
    [SerializeField] private float s_OverheatCapacity;
    [SerializeField] private float s_RegenerateRate;
    [SerializeField] private GameObject s_bossBarUI;
    [SerializeField] private GameObject s_hpBarUI;
    [SerializeField] private int s_MaxHealth;
    [SerializeField] private GameObject s_ClairvoyanceUI;
    [SerializeField] private GameObject s_SightStonePrefab;
   
    
    [Header("Tilemap")]
    [SerializeField] private Tilemap s_TileMap;

    [Header("Skills")]
    [SerializeField] private float s_FlashlightSliderMultiplier;
    [SerializeField] private int s_ClairvoyanceCount;

    [Header("BossScene")]

    [SerializeField] private DialogueScript s_BossSceneDialogue;
    //PRIVATE

    private Rigidbody2D p_Rb;

    private float p_HorizontalInput;
    private float p_VerticalInput;
    private float p_DefaultSpeed;
    private float p_StartRollTime;
    private float p_MaxTime;
   
    private Vector2 p_Position;
    private Vector2 p_PlayerDirection;

    private bool p_IsCrouching;
    private bool p_IsRolling;
    private bool p_IsFlashlightOn;
    private bool p_IsFlashlightGlitched;
    private bool p_HasKey;

    //PUBLIC

    private Animator p_PlayerAnimator;
    private Slider p_OverheatSliderController;
    private Slider p_BossSlider;
    private float p_CurrentSliderValue;
    private float p_BossCurrentSliderValue;
    private float p_MaxValue;

    private float p_BossMaxValue;
    private float p_SliderMultiplier;

    private HeartScript p_heartHolder;
    private bool p_IsClairvoyanceOn;

    private AudioManager p_AudioManager;
    private float p_CurrentStep;
    private bool p_WinGame;
    private bool p_ShouldClairvoyance;
    private bool p_MoveCameraToBoss;
    private bool p_BossSceneStarted;
    private bool p_MoveCameraToPlayer;
    private bool p_SetCameraToPlayer;
    private bool p_StillRolling;
    private float p_RolloverTime;
    private bool p_IsWalkingPreviously;
    private float p_CheckWalkTime;
    private bool p_HasClick;
    private Slider p_Slider;

    //HP

    private Slider p_HpSlider;
    private int p_CurrentHealth;
    private bool wasMovingVertical;
    private Vector2 lastMove;
    private GameObject flashlightObject;

    //Overheat

    private float p_OverheatCooldownTime;
    private bool p_ReadyToRegenerate;
    private bool p_IsNotWalking;

    //Stun
        
    private bool p_Stunned;
    private bool p_IsTutorialPlaying;

    //Checkpoint

    private int p_runTutorial;

    CheckpointManagerScript p_CheckpointManager;
    private bool p_IsBossCutscene;

    //Dialogue Manager

    DialogueManager p_DialogueManager;
    private float p_BossValueTime;
    private float p_ShouldClairvoyanceTime;
    private float p_ClairvoyanceOffset;

    public bool PlayerMoving { get; private set; }

    private void Awake()
    {
    }

    void Start()
    {

        for (int i = 0; i < 3; i++)
        {
            GameObject child = Instantiate(s_SightStonePrefab);
            child.transform.localScale = Vector3.one * 2.0f;
            child.transform.SetParent(s_ClairvoyanceUI.transform);
        }
        // PlayerPrefs.DeleteAll();
        /*    if (!PlayerPrefs.HasKey("runTutorial"))
            {
                PlayerPrefs.SetInt("runTutorial", 1);
            }
            else
            {
                transform.position = s_StartPoint.transform.position;
            }*/


      //  p_runTutorial = PlayerPrefs.GetInt("runTutorial");
 

        p_CheckpointManager = FindObjectOfType<CheckpointManagerScript>();
        p_AudioManager = FindObjectOfType<AudioManager>();
        p_heartHolder = FindObjectOfType<HeartScript>(); 
        s_Animator = GetComponent<Animator>();
        p_DialogueManager = FindObjectOfType<DialogueManager>();


        if (s_SceneLevel == SceneLevel.DARK_DUNGEON)
        {
           // Debug.Log("Run Tut: " + p_CheckpointManager.RunTutorial());
            if (p_CheckpointManager.RunTutorial())
            {
                p_IsTutorialPlaying = true;
                StartCoroutine(TutorialScene());
            }
            else
            {
                GameObject child = transform.GetChild(0).gameObject;
                child.SetActive(!transform.GetChild(0).gameObject.activeSelf);
                p_IsFlashlightOn = transform.GetChild(0).gameObject.activeSelf;
                transform.position = s_StartPoint.transform.position; 
            }

            p_Slider = s_FlashlightUI.GetComponent<Slider>();
            p_HpSlider = s_hpBarUI.GetComponent<Slider>();
            p_HpSlider.maxValue = s_MaxHealth;
            p_HpSlider.value = s_MaxHealth;
            p_CurrentHealth = s_MaxHealth;
          
            p_OverheatSliderController = s_OverheatUI.GetComponent<Slider>();
            p_OverheatSliderController.value = s_OverheatCapacity;
            p_OverheatSliderController.maxValue = s_OverheatCapacity;

            p_BossSlider = s_bossBarUI.GetComponent<Slider>();

        }
        p_PlayerAnimator = GetComponent<Animator>();
        p_Rb = GetComponent<Rigidbody2D>();
     
        p_CurrentStep = s_StepOffset;
        p_SliderMultiplier = 5.0f;
        p_MaxValue = 20.0f;
        p_BossMaxValue = 30f;
        p_IsFlashlightOn = false;
        p_DefaultSpeed = s_Speed;
        p_RolloverTime = 0;
        p_ClairvoyanceOffset = 5.0f;
        p_ShouldClairvoyanceTime = 9999.0f;

        p_ReadyToRegenerate = true;
        p_IsNotWalking = true;

        p_AudioManager.queueSound("gameplayMusic");

        // takeDamageEvent += DamagePlayer;
        //takeDamageEvent.Invoke();

        StartCoroutine(RegenerateEnergy());
        StartCoroutine(Walk());
    }

    
    void Update()
    {
      //  Debug.Log("Checkpoint Manager Count: " + FindObjectsOfType<CheckpointManagerScript>().Length);
        if (!p_IsTutorialPlaying && !p_IsBossCutscene)
        {

            if (Mathf.Abs(p_RolloverTime - Time.time) >= 2.0f)
            {
                p_RolloverTime = 0;
                p_ReadyToRegenerate = true;
            }

            p_CurrentStep += Time.deltaTime;
            CheckMovementType();
            MovePlayer();

            if (s_SceneLevel == SceneLevel.DARK_DUNGEON)
            {
                ClairvoyanceController();
                ControlFlashlight();
                SliderController();
            }


            if (IsKeyClicked(KeyCode.G))
            {

                flashlightObject = Instantiate(transform.GetChild(0).gameObject);
                flashlightObject.transform.position = transform.position;
                flashlightObject.transform.DOJump(new Vector2(transform.position.x, transform.position.y) + (p_PlayerDirection * 6), 2.0f, 0, 1.0f);
                flashlightObject.transform.DOMove(flashlightObject.transform.position, .1f).onKill();
            }
        }
        else
        {
           
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            s_Animator.SetBool("isWalking", false);
            p_PlayerAnimator.SetInteger("lastDirection", 1);
        }
    }


   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Key")
        {
            p_AudioManager.queueSound("bell");
            p_HasKey = true;
        }

 

        if(collision.tag == "Stun" && !p_Stunned)
        {
            //Debug.Log("Stunned!");
            p_AudioManager.queueSound("zap");
            Destroy(collision.gameObject);
            StartCoroutine(PlayerStunned());
        }
        if (collision.tag == "HealingStone")
        {
            p_HpSlider.value += 6;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
   
    }

    public void IncrementClairvoyance()
    {
        GameObject sightStone = Instantiate(s_SightStonePrefab);
        sightStone.transform.localScale = Vector3.one * 2.0f;
        sightStone.transform.SetParent(s_ClairvoyanceUI.transform);
        s_ClairvoyanceCount++;
    }

    private void FixedUpdate()
    {

    /*    if (s_SceneLevel == SceneLevel.DARK_DUNGEON)
        {
            if (p_MoveCameraToBoss)
            {//  Debug.Log("Camera moving.");

                Vector3 moveCameraToPosition = Vector3.MoveTowards(s_CameraController.transform.position, s_Boss.transform.position, Time.deltaTime * 15.0f);
                Vector3 moveCameraToPosition2D = new Vector3(moveCameraToPosition.x, moveCameraToPosition.y, -10);
                s_CameraController.transform.position = moveCameraToPosition2D;
                if (Vector2.Distance(s_CameraController.transform.position, s_Boss.transform.position) <= 0.1f)
                {
                    p_MoveCameraToBoss = false;
                }
                // Debug.Log("Camera to Boss: " + Vector2.Distance(s_CameraController.transform.position, s_Boss.transform.position));
            }

            if (p_MoveCameraToPlayer)
            {
                Vector3 moveCameraToPosition = Vector3.MoveTowards(s_CameraController.transform.position, transform.position, Time.deltaTime * 50.0f);
                Vector3 moveCameraToPosition2D = new Vector3(moveCameraToPosition.x, moveCameraToPosition.y, -10);
                s_CameraController.transform.position = moveCameraToPosition2D;

                // float cameraDistanceToPlayer = Vector2.Distance(s_CameraController.transform.position, transform.position);
                //   Debug.Log("Camera to Player Distance: " + cameraDistanceToPlayer);
                *//* if (cameraDistanceToPlayer <= 0f && !p_SetCameraToPlayer)
                 {
                     Debug.Log("Following player");
                     s_CameraController.GetComponent<CinemachineVirtualCamera>().Follow = transform;
                     p_SetCameraToPlayer = true;
                     p_MoveCameraToPlayer = false;
                 }*//*
            }
        }*/
    }

    public Vector2 GetPlayerDirection() => p_PlayerDirection;

    public void ActivateWin()
    {
        p_WinGame = true;
    }

    public void KillPlayer()
    {
        Destroy(this.gameObject);
    }

    public void DamagePlayer(int dmg)
    {
        StartCoroutine(PlayerHurt());
        p_CurrentHealth -= dmg;
        p_HpSlider.value = p_CurrentHealth;
    }

    public void glitchFlashlight()
    {
        p_AudioManager.queueSound("flashlight");
        p_IsFlashlightOn = true;
        p_IsFlashlightGlitched = true; 
        GameObject child = transform.GetChild(0).gameObject;
        child.SetActive(true);
        StartCoroutine(FlashlightGlitch());
    }

    IEnumerator FlashlightGlitch()
    {
        yield return new WaitForSeconds(2.0f);
        p_IsFlashlightGlitched = false;
/*        p_IsFlashlightOn = false;
        GameObject child = transform.GetChild(0).gameObject;
        child.SetActive(false);*/

    }


    void ClairvoyanceController()
    {
        p_IsClairvoyanceOn = false;

        if (p_ShouldClairvoyance)
        {
            if (s_ClairvoyanceCount > 0)
            {
                Debug.Log("Claivoyance Activated.");
                p_AudioManager.queueSound("clairvoyance");
                p_IsClairvoyanceOn = true;
            }
            p_ShouldClairvoyance = false;
        }

        if (p_IsClairvoyanceOn)
        {
            Destroy(s_ClairvoyanceUI.transform.GetChild(0).gameObject);
            s_ClairvoyanceCount--;
        }
    }

    IEnumerator PlayerStunned(float stunTime = .5f)
    {
        while (true)
        {
            p_Stunned = true;
            transform.GetComponent<SpriteRenderer>().color = Color.gray;
            yield return new WaitForSeconds(stunTime);
            transform.GetComponent<SpriteRenderer>().color = Color.white;
            p_Stunned = false;
            break;
        }
    }

    public void StunPlayer(float stunTime)
    {
        StartCoroutine(PlayerStunned(stunTime));
    }

    IEnumerator RegenerateEnergy()
    {
        while (true)
        {
            if (p_IsNotWalking && p_ReadyToRegenerate && p_OverheatSliderController.value <= s_OverheatCapacity)
            {
                p_OverheatSliderController.value += s_RegenerateRate;
            }
            yield return new WaitForSeconds(.5f);
        }
    }
    void SliderController()
    {
        if (p_IsFlashlightOn)
        {
           // Debug.Log("Flashlight is on!");
               if (p_CurrentSliderValue / p_MaxValue < 1.0f)
               {
                   p_CurrentSliderValue += Time.deltaTime;
                   p_Slider.value = p_CurrentSliderValue / p_MaxValue;
               }

            //p_BossCurrentSliderValue += 1f;
            if (Mathf.Abs(p_BossValueTime - Time.time) >= .05f)
            {
                p_BossCurrentSliderValue += .013f;
                p_BossValueTime = Time.time;
                p_BossSlider.value = p_BossCurrentSliderValue / p_BossMaxValue;
            }
        }
        else
        {

            if (p_CurrentSliderValue / p_MaxValue > 0.0f)
            {
                p_CurrentSliderValue -= Time.deltaTime * s_FlashlightSliderMultiplier;
                p_Slider.value = p_CurrentSliderValue / p_MaxValue;
            }

        }

        if (p_BossSlider.value == p_BossSlider.maxValue && !p_BossSceneStarted)
        {
            p_BossSceneStarted = true;
            StartCoroutine(BossCutScene());
        }
    }
    IEnumerator BossCutScene()
    {
        while (true)
        {
            p_IsBossCutscene = true;
            CameraShake shake = s_CameraController.GetComponent<CameraShake>();
            shake.shakeDuration = 1.0f;
            yield return new WaitForSeconds(Mathf.Max(shake.shakeDuration, .5f) + .5f);
            yield return new WaitForSeconds(2.0f);
            p_AudioManager.queueSound("roar");
            yield return new WaitForSeconds(5.0f);
            break;
        }
            p_DialogueManager.activateDialogue();
            p_DialogueManager.addDialogue(s_BossSceneDialogue);
            p_IsBossCutscene = false;
           
    }

    IEnumerator TutorialScene()
    {
       
       p_CheckpointManager.SetTutorial(false);
       // FindObjectOfType<CheckpointManagerScript>().SetTutorial(false);
        while (true)
        {
            p_AudioManager.queueSound("fallingSound");
            yield return new WaitForSeconds(5.15f);
            p_AudioManager.queueSound("bang");
            yield return new WaitForSeconds(.75f);
            p_AudioManager.queueSound("bang");
            yield return new WaitForSeconds(.5f);
            break;

        }

        transform.DOMoveY(transform.position.y - 1.0f, .25f);
        p_IsTutorialPlaying = false;
        //transform.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
    }

    IEnumerator PlayerHurt()
    {
        while (true)
        {
            yield return new WaitForSeconds(.10f);
            transform.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(.10f);
            transform.GetComponent<SpriteRenderer>().color = Color.white;
            break;
        }
    }

    IEnumerator Walk()
    {
        while (true)
        {
            if (p_IsCrouching)
            {
                yield return new WaitForSeconds(.05f);
                p_OverheatSliderController.value -= .50f;
            }
            yield return null;
        }
    }
    public bool IsWinGame() => p_WinGame;
    public bool IsFlashlightOn() => p_IsFlashlightOn;
    public bool IsClairvoyancing() => p_IsClairvoyanceOn;

    void ControlFlashlight()
    {
        if (IsKeyClicked(KeyCode.Z))
        {
            if (!p_IsFlashlightGlitched)
            {
                p_AudioManager.queueSound("flashlight");
                GameObject child = transform.GetChild(0).gameObject;
                child.SetActive(!transform.GetChild(0).gameObject.activeSelf);
                p_IsFlashlightOn = transform.GetChild(0).gameObject.activeSelf;
            }

        }

        
    }

    void CheckMovementType()
    {
        if (Input.GetKey(KeyCode.C))
        {
            p_IsNotWalking = false;
            p_IsCrouching = true;
        }
        else
        {
            p_IsCrouching = false;
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            p_IsNotWalking = true;
        }
        //CheckActiveMovement(KeyCode.C, ref p_IsCrouching);

        if (Mathf.Abs(p_ShouldClairvoyanceTime - Time.time) >= p_ClairvoyanceOffset)
        {
            CheckActiveMovement(KeyCode.V, ref p_ShouldClairvoyance, false);
        }
       
        if (p_ShouldClairvoyance)
        {
            p_ShouldClairvoyanceTime = Time.time;
        }

        CheckActiveMovement(KeyCode.X, ref p_IsRolling,false);

    }
    void CheckActiveMovement(KeyCode key, ref bool checkMovement, bool isUp = true)
    {
        if (IsKeyClicked(key))
        {
            checkMovement = true;
        }
        else if (IsKeyClicked(key, isUp))
        {
            checkMovement = false;
        }
    }

    //Should we do all around movement or 4 or 8 sides movement?
    void MovePlayer()
    {

        if (!p_Stunned)
        {

        s_Speed = p_DefaultSpeed;

       

            p_PlayerDirection = new Vector2(p_HorizontalInput, p_VerticalInput).normalized;
        p_MaxTime = s_Distance / s_RollSpeed;

        bool hasHorizontalInput = !Mathf.Approximately(p_HorizontalInput, 0f);
        bool hasVerticalInput = !Mathf.Approximately(p_VerticalInput, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

            if (p_OverheatSliderController.value >= s_DashEnergy && p_IsRolling && !p_StillRolling)
            {

                s_Animator.SetBool("isRolling", p_IsRolling);
               // Debug.Log("Clicked Rolling");
                p_RolloverTime = Time.time;
                p_ReadyToRegenerate = false;
                StartCoroutine(Dash());
                p_IsRolling = false;
                p_StillRolling = true;
                isWalking = false;
            }
            else if (isWalking)
            {
                s_Animator.SetBool("isWalking", true);
                if (p_IsCrouching && p_OverheatSliderController.value >= .5f)
                {
                    if (p_CurrentStep >= s_StepOffset)
                    {
                        //  Debug.Log("Crouching");
                        p_AudioManager.queueSound("crouch");
                        p_CurrentStep = 0;
                    }
                   // Debug.Log("Crouching...");
                    s_Speed *= CROUCH_VALUE;
                    // p_OverheatSliderController.value -= .10f;
                }

                // p_Rb.MovePosition(new Vector2(transform.position.x,transform.position.y) + p_PlayerDirection * s_Speed * Time.deltaTime);
                isWalking = false;
            }
            else
            {
                s_Animator.SetBool("isWalking", false);
            }

            MovementDirection();
            p_IsRolling = false;
        }
        else
        {
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }


    }

    void MovementDirection()
    {
        float currentMoveSpeed = s_Speed * Time.deltaTime;

        float horizontal = Input.GetAxisRaw("Horizontal");
        bool isMovingHorizontal = Mathf.Abs(horizontal) > 0.5f;

        float vertical = Input.GetAxisRaw("Vertical");
        bool isMovingVertical = Mathf.Abs(vertical) > 0.5f;

    

        if (isMovingVertical && isMovingHorizontal)
        {
            if (wasMovingVertical)
            {
                p_Rb.velocity = new Vector2(horizontal * currentMoveSpeed, 0);
                //   lastMove = new Vector2(horizontal, 0f);
            }
            else
            {
                p_Rb.velocity = new Vector2(0, vertical * currentMoveSpeed);
                //  lastMove = new Vector2(0f, vertical);
            }
        }
        else if (isMovingHorizontal)
        {
            p_Rb.velocity = new Vector2(horizontal * currentMoveSpeed, 0);
            wasMovingVertical = false;
            //lastMove = new Vector2(horizontal, 0f);
        }
        else if (isMovingVertical)
        {
            p_Rb.velocity = new Vector2(0, vertical * currentMoveSpeed);
            wasMovingVertical = true;
            // lastMove = new Vector2(0f, vertical);
        }
        else
        {
            // PlayerMoving = false;
            p_Rb.velocity = Vector2.zero;
        }

        p_HorizontalInput = p_Rb.velocity.x;
        p_VerticalInput = p_Rb.velocity.y;

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;


        if (isWalking)
        {

            if(p_PlayerDirection == Vector2.up)
            {
                p_PlayerAnimator.SetInteger("lastDirection", 1);
            }
            else if (p_PlayerDirection == Vector2.right)
            {
                p_PlayerAnimator.SetInteger("lastDirection", 4);
            }
            else if (p_PlayerDirection == Vector2.left)
            {
                p_PlayerAnimator.SetInteger("lastDirection", 3);
            }
            else
            {
                p_PlayerAnimator.SetInteger("lastDirection", 2);
            }

            s_Animator.SetFloat("xDirection", p_PlayerDirection.x);
            s_Animator.SetFloat("yDirection", p_PlayerDirection.y);
        }
        else
        {
            isWalking = false;
        }
    }

    public void RunSound()
    {
        p_AudioManager.queueSound("step");
    }

    public void DashSound()
    {
        p_AudioManager.queueSound("roll");
    }

    IEnumerator Dash()
    {
        // p_ReadyToRegenerate = false;
        if (s_SceneLevel == SceneLevel.DARK_DUNGEON)
        {
            p_OverheatSliderController.value -= s_DashEnergy;
        }

        float startTime = Time.time;
        Vector2 direction = new Vector3(p_HorizontalInput, p_VerticalInput).normalized;
        s_Animator.SetFloat("xRollDirection", direction.x);
        s_Animator.SetFloat("yRollDirection",direction.y);
        s_DashTime = s_Distance / s_DashSpeed;
        while (Time.time < startTime + s_DashTime)
        {
           // transform.position = p_Rb.position + (direction * s_DashSpeed * Time.deltaTime));
            p_Rb.MovePosition(p_Rb.position + ( direction * s_DashSpeed * Time.deltaTime));
            yield return null;
        }

        p_StillRolling = false;
        //p_RolloverTime = Time.time;
        //s_Animator.
        s_Animator.SetBool("isRolling", p_StillRolling);

    }
    void DEBUG(string debug)
    {
        Debug.Log(debug);
    }
    /// <summary>
    /// Short Method for Mouse Click Events.
    /// </summary>
    /// <param name="mouseButton"></param>
    /// <param name="isClickedOnce"></param>
    /// <param name="isUp"></param>
    /// <returns></returns>
    bool isMouseClicked(int mouseButton, bool isClickedOnce=true, bool isUp = false) {
        bool isClicked;

        if (isClickedOnce)
        {

            if (isUp)
            {
                isClicked = Input.GetMouseButtonUp(mouseButton);
            }
            else
            {
                isClicked = Input.GetMouseButtonDown(mouseButton);
            }
        }
        else
        {
            isClicked = Input.GetMouseButton(mouseButton);
        }

        return isClicked;
    }
    /// <summary>
    ///  Short Method for Keyboard Events.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="isClickedOnce"></param>
    /// <param name="isUp"></param>
    /// <returns></returns>
    /// 
    bool IsKeyClicked(KeyCode key, bool isUp = false, bool isClickedOnce = true)
    {
        bool isClicked;

        if (isClickedOnce)
        {

            if (isUp)
            {
                isClicked = Input.GetKeyUp(key);
            }
            else
            {
                isClicked = Input.GetKeyDown(key);
            }
        }
        else
        {
            isClicked = Input.GetKey(key);
        }

        return isClicked;
    }

}
