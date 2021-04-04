using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
	public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
	private Rigidbody rb;
	private float movementX;
	private float movementY;
    private int count;
    public float jumpForce = 5.0f;
    public bool isGrounded = true;
    public int jumpcount = 0;
    public Vector3 jump;
    private int maxjump = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
    	Vector2 movementVector = movementValue.Get<Vector2>();

    	movementX = movementVector.x;
    	movementY = movementVector.y;
    }

    void FixedUpdate()
    {
    	Vector3 movement = new Vector3(movementX, 0.0f, movementY);
    	rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpcount = 0;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 6) 
        {
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
        }
    }

    public void OnJump()
    {
        jump = new Vector3(0.0f, 1.0f, 0.0f);
        if (maxjump > jumpcount)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            jumpcount++;
        }
    }

}
