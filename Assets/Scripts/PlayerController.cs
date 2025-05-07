using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //private variable type Rigidbody called rb
    // private = not accessible from inspector or other scripts
    public float speed = 25.0f;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private Rigidbody rb;
    private int count;
    private float movementX; 
    private float movementY;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
        {
        Debug.Log("Start wurde aufgerufen");

        rb = GetComponent<Rigidbody>();

            // 👉 Input registrieren
            var playerInput = GetComponent<PlayerInput>();
            playerInput.actions["Move"].performed += OnMove;
            playerInput.actions["Move"].canceled += OnMove; // optional, damit Bewegung bei loslassen stoppt

        count = 0;

        SetCountText();

        winTextObject.SetActive(false);

    }



        void FixedUpdate()
        {
            Vector3 movement = new Vector3(movementX, 0.5f, movementY);
            rb.AddForce(movement * speed);

        
    }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PickUp"))
            {

                other.gameObject.SetActive(false);
                count = count + 1;
                SetCountText();

            }
        
        }

        // 🧠 Die eigentliche Input-Verarbeitung
        private void OnMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            movementX = input.x;
            movementY = input.y;

            Debug.Log("Bewegung: " + input);
        }

        void SetCountText()
        {
            countText.text = "Count: " + count.ToString();
            if (count >= 8)
            {
                winTextObject.SetActive(true);
            }
    }
}