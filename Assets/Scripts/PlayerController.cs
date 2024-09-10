using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public static Camera cam;
    public Rigidbody2D rb;

    public Camera camView;

    public float moveSpeed = 5f;

    private Vector2 _moveInput;
    private Vector2 _mouseInput;

    public float maxAngle = 20f;
    public float minAngle = 340f;

    public float mouseSensitivity = 1f;

    public GameObject bulletImpact;
    public int currentAmmo;

    public Animator gunAnim; 
    private void Awake()
    {
        /*
        This code means that multiplayer would be even more of a headache to implement
        because any other reference of the playerController script would replace the previous instances
        , but multiplayer isn't planned so it shouldn't matter
        */
        instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        camView = GetComponentInChildren<Camera>();
        cam = camView;
    }

    // Update is called once per frame
    void Update()
    {
        //Player movement
        _moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 moveInputHorizontal = transform.up * -_moveInput.x;

        Vector3 moveInputVertical = transform.right * _moveInput.y;
        rb.velocity = (moveInputHorizontal + moveInputVertical) * moveSpeed;
        //Player mouse controls
        _mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z - _mouseInput.x);
        Vector3 rotateAmount = camView.transform.localRotation.eulerAngles + new Vector3(0f, _mouseInput.y, 0f);

        camView.transform.localRotation = Quaternion.Euler(rotateAmount.x, Mathf.Clamp(rotateAmount.y, minAngle, maxAngle), rotateAmount.z);

        //Shooting
        if (Input.GetMouseButtonDown(0))
        {
            if (currentAmmo > 0)
            {
                //Ray casts yippee
                Ray ray = camView.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("I'm looking at " + hit.transform.name);
                    Instantiate(bulletImpact, hit.point, transform.rotation);
                }
                else
                {
                    Debug.Log("I'm looking at nothing!");
                }
                currentAmmo--;
                gunAnim.SetTrigger("Shoot");
            }
        }
    }
}
