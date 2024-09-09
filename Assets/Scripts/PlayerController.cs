using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;

    public Transform camView;

    public float moveSpeed = 5f;

    private Vector2 _moveInput;
    private Vector2 _mouseInput;

    public float maxAngle = 110f;
    public float minAngle = 70f;

    public float mouseSensitivity = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        camView.localRotation = Quaternion.Euler(rotateAmount.x, Mathf.Clamp(rotateAmount.y, minAngle, maxAngle), rotateAmount.z);
    }
}
