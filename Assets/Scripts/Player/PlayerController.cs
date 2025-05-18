using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{

    // Proprietate pentru a verifica daca playerul este orientat spre stanga
    public bool FacingLeft { get { return facingLeft; } }
    // Singleton pentru acces usor la instanta playerului

    [SerializeField] private float moveSpeed = 1f;  // Viteza normala de miscare
    [SerializeField] private float dashSpeed = 4f; // Multiplicator de viteza in timpul dash-ului
    [SerializeField] private TrailRenderer myTrailRenderer;  // Efect vizual pentru dash
    [SerializeField] private Transform weaponCollider;



    private PlayerControls playerControls; // Referinta la schema de input-uri
    private Vector2 movement; // Vector pentru directia de miscare
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;
    private float startingMoveSpeed;  // Stocheaza viteza initiala pentru a reveni dupa dash


    private bool facingLeft = false;
    private bool isDashing = false;

    protected override void Awake(){
        base.Awake();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Asociaza input-ul de Dash
        playerControls.Combat.Dash.performed += _ => Dash();

        startingMoveSpeed = moveSpeed;
    }

    private void OnEnable()
    {
        // Activeaza schema de input
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        // Actualizeaza orientarea playerului si aplica miscarea
        AdjustPlayerFacingDirection();
        Move();
    }
    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }
    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        // Actualizeaza parametrii animatorului pentru a reflecta miscarea
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {// Muta playerul in functie de input si viteza
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRender.flipX = true;
            facingLeft = true;
        }
        else
        {
            mySpriteRender.flipX = false;
            facingLeft = false;
        }
    }
    private void Dash()
    { // Initiaza miscarea de dash daca nu esti deja in dash
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
