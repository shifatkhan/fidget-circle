using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDrag : MonoBehaviour
{
    private Vector3 touchPosition;
    private Rigidbody2D rb;
    private Vector3 direction;
    public float moveSpeed = 10f;

    private bool isDragging = false;

    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        /* TODO: Move this whole block of code to a GameManager object
         * since all it does is check for touch position.
         * Maybe we'll add more circles, and this code wil become redundant.
         */
        if (Input.touchCount > 0)
        {
            //We transform the touch position into word space from screen space and store it.
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            // Calculate where to move game object according to touch position.
            direction = (touchPosition - transform.position);

            Vector2 touchPosWorld2D = new Vector2(touchPosition.x, touchPosition.y);

            //We now raycast with this information. If we have hit something we can process it.
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (hitInformation.collider != null && hitInformation.transform.name == this.transform.name && !isDragging)
            {
                // This game object was touched!
                Debug.Log("Touched " + hitInformation.transform.name);

                isDragging = true;
            }

            // Stop following touch if finger was lifted.
            if (touch.phase == TouchPhase.Ended && isDragging)
            {
                //rb.velocity = Vector2.zero;
                isDragging = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDragging)
        {
            // Make game object follow touch.
            rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed;
        }
        else if(rb.velocity.x > 0 || rb.velocity.y > 0)
        {
            // Slow down gameObject to a stop.
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Time.deltaTime);
        }
    }
}
