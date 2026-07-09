using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerMainSet : DamageableObject
{
    private enum Enviroment {Ciudad, Openworld, Combate};
    [SerializeField, ReadOnly, Tooltip("The Rigidbody component of the player, nedded to calculate the phisycs")] private Rigidbody rb;
    [SerializeField, ReadOnly, Tooltip("The collider trigguer of the player atack")] private Collider OUT_DAMAGE_TRIGGER;
    [SerializeField, ReadOnly, Tooltip("The collider trigguer of the player for damaging they")] private Collider IN_DAMAGE_TRIGGER;
    [SerializeField, ReadOnly, Tooltip("The enviroment ehre the pleyer are in\nif CIUDAD => Player.Walk\nif OPENWORLD => Player.Walks>Player.Run\nif COMBATE => Player.Run")] private Enviroment enviroment = Enviroment.Openworld;

    [Header("Player Parameters")]
    [Header("Health Parametres")]
    [SerializeField, Tooltip("Vida maxima que puede llegar a tener el jugador")] private int VIDA_MAX = 2; // Vida maxima del jugador (constante)
    [Header("Movement Parameters")]
    [SerializeField, Tooltip("Velocidad al andar del jugador, se usa en ciudades, al empezar a andar luego de estar parado y al fijar un objetivo en combate")] private float WALK_SPEED; // Velocidad a la que se mueve el jugador quando anda
    [SerializeField, Tooltip("Velocidad al correr del jugador, se usa en combate y fuera de las ciudades, se llega al andar durante unos segundos")] private float RUN_SPEED; // Velocidad a la que se mueve el jugador cunado corre
    [SerializeField] private float TIME_TO_START_RUNING;
    [SerializeField] private float TIME_BETWEN_MOVE_INPUTS;
    [SerializeField, ReadOnly, Tooltip("Velocidad a la que va el jugador actualmente")] private float speed_actual;  // Velocidad actual del jugador (variable)
    [SerializeField, ReadOnly] bool isRunging = false;
    [SerializeField, Tooltip("Fuerza con la que se impulsa el jugador al hacer el dash")] private float DASH_FORCE; // Distancia que recorre el dash
    [SerializeField, Tooltip("Tiempo que tarda el jugador en poder volver a usar el dash")] private float DASH_COOLDOWN; // Tiempo de recarga para el dash
    [SerializeField, Tooltip("Tiempo total que dura el dash")] private float DASH_TIME; // Tiempo de recarga para el dash
    [SerializeField, ReadOnly, Tooltip("Verificador de si se esta usando el dash en este momento: \nCheck activo = Dasheando")] bool isDashing = false; // Verificador de si se esta usando el dash en este momento
    [SerializeField, ReadOnly, Tooltip("Verificador de si se ha usado el dash recientemente: \nSi se esta recargando (check activo) no se podra usar el dash")] private bool rechargingDash = false; // Verificador de si se ha usado el dash recientemente
    [Header("Combat Parameters")]
    [SerializeField, Tooltip("Daño base del jugador")] private int BASE_DAMAGE = 1; // Daño base del Jugador (constante)
    [SerializeField, ReadOnly, Tooltip("Daño que hace el jugador actualmente")] private float damage_actual;  // DAño actual del jugador (variable)
    [Header("Habilites")]
    [SerializeField, ReadOnly] private bool INMUNITY_DASH_HABILITY;
    [SerializeField, ReadOnly] private bool DAMAGING_DASH_HABILITY;
    [SerializeField, ReadOnly] private bool CHARGED_ATACK_HABILITY;
    [SerializeField, ReadOnly] private bool COMBO_ATACK_HABILITY;
    [SerializeField, ReadOnly] private bool VENENO_ATACK_HABILITY;
    [SerializeField, ReadOnly] private bool LEECHLIVE_ATACK_HABILITY;
    [SerializeField, ReadOnly] private bool LOCKON_ATACK_HABILITY;




    #region Unity methodes
    /// <summary>
    /// Funcion para actualizar valores en el editor:
    /// Poner aqui assignaciones de valores i etc
    /// </summary>
    void OnValidate()
    {
        vida_actual = VIDA_MAX;
        damage_actual = BASE_DAMAGE;
        speed_actual = WALK_SPEED;
        rb = this.GetComponent<Rigidbody>();
        OUT_DAMAGE_TRIGGER = transform.Find("OutDamage_Trigger").GameObject().GetComponent<Collider>();
        IN_DAMAGE_TRIGGER = transform.Find("InDamage_Trigger").GameObject().GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        if (enviroment == Enviroment.Ciudad)
        {
            isRunging = false;
        }
        else if (enviroment == Enviroment.Combate)
        {
            isRunging = true;
        }
        if (!isDashing)
        {
            Move();
            Rotate();
        }
    }
    #endregion Unithy methodes

    #region Move functions
    private Vector2 moveInput;
    /// <summary>
    /// Funcion que se deve llamar cuando se detecte un imput de movimineto (joistic, WASD...) \n
    /// Guarda el input para usar-lo posteriormente
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            StartCoroutine(StopRuning());
        }
        else if (!isRunging) //&& context.started)
        {
            StartCoroutine(StartRuning());
            StopCoroutine(StopRuning());
        }
        moveInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// Funcion que controla el movimiento del personaje
    /// </summary>
    private void Move()
    {
        speed_actual = (isRunging)? RUN_SPEED : WALK_SPEED;
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);
        rb.linearVelocity = direction * speed_actual;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartRuning()
    {
        yield return new WaitForSeconds(TIME_TO_START_RUNING);
        isRunging = true;
    }
    private IEnumerator StopRuning()
    {
        yield return new WaitForSeconds(TIME_BETWEN_MOVE_INPUTS);
        StopCoroutine(StartRuning());
        isRunging = false;
    }

    /// <summary>
    /// Funcion que se deve llamar cuando se detecte un imput de Dash (joistic, WASD...) \n
    /// Guarda el input para usar-lo posteriormente
    /// </summary>
    /// <param name="context"></param>
    public void OnDash(InputAction.CallbackContext context)
    {
        if (!rechargingDash && enviroment != Enviroment.Ciudad)
        {
            StartCoroutine(Dash());
        }

    }

    /// <summary>
    /// Funcion que controla el dahs del personaje
    /// </summary>
    private IEnumerator Dash()
    {
        rb.AddForce(DASH_FORCE * transform.forward, ForceMode.Impulse);
        isDashing = true;
        rechargingDash = true;
        yield return new WaitForSeconds(DASH_TIME);
        isDashing = false;
        yield return new WaitForSeconds(DASH_COOLDOWN);
        rechargingDash = false;
    }

    /// <summary>
    /// Funcion que permite rotar la vista del personaje para que encaje con la direccion en que se camina
    /// </summary>
    private void Rotate()
    {
        if (moveInput.x == 0 && moveInput.y == 0)
        {
            //se queda mirando hacia el lado que estava porque no se esta moviendo
            return;
        }

        int y = 90;
        float smooth = 10;
        if (moveInput.x == 0 || moveInput.y == 0)
        {
            if (moveInput.x == 1)
            {
                y = 90;
            }
            else if (moveInput.x == -1)
            {
                y = -90;
            }
            if (moveInput.y == 1)
            {
                y = 0;
            }
            else if (moveInput.y == -1)
            {
                y = 180;
            }
        }
        else if (moveInput.x > 0 && moveInput.y > 0)
        {
            y = 45;
        }
        else if (moveInput.x > 0 && moveInput.y < 0)
        {
            y = 135;
        }
        else if (moveInput.x < 0 && moveInput.y < 0)
        {
            y = 255;
        }

        else if (moveInput.x < 0 && moveInput.y > 0)
        {
            y = 315;
        }

        Quaternion target = Quaternion.Euler(0, y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, smooth * Time.deltaTime);
    }
    #endregion Move functions
}
