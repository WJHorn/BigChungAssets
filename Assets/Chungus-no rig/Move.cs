using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
   public AudioSource jumpSound;
   //public AudioSource walkSound;
   //public AudioSource runSound;
   public CharacterController controller;
   public Transform cam;
   
   public float speed = 12f;
   public float runSpeed = 24f;
   public float turnSmooth = 0.1f;
   float turnSmoothV;
   public float gravity = -20f;
   public float jumpHeight = 3f;
   
   public bool running = false;
   
   public Transform groundCheck;
   public float groundDist = 0.4f;
   public LayerMask groundMask;
   bool isGrounded;
   Vector3 velocity;
   
   void Start() {
      Cursor.visible = false;
   }
   // Update is called once per frame
   void Update() {
      isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
      
      float horizontal = Input.GetAxisRaw("Horizontal");
      float vertical = Input.GetAxisRaw("Vertical");
      Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
      
      if (Input.GetKeyDown("left shift")) {
         running = true;
      }
      else if (Input.GetKeyUp("left shift")) {
         running = false;
      }
         
      if (direction.magnitude >= 0.1f) {
         float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
         float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothV, turnSmooth);
         transform.rotation = Quaternion.Euler(0f, angle, 0f);
         
         Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
         
         if (running) {
            controller.Move(moveDirection.normalized * runSpeed * Time.deltaTime);
            //runSound.Play();
         }
         else if (!running) {
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            //walkSound.Play();
         }
      }
      
      if (isGrounded && velocity.y < 0) {
         velocity.y = -2f;
      }
      
      if (Input.GetButtonDown("Jump") && isGrounded) {
         jumpSound.Play();
         velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
      }
        
      velocity.y += gravity * Time.deltaTime;
      controller.Move(velocity * Time.deltaTime);
   }
}
