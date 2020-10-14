using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveAlmacen : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3.5f;
    [SerializeField] float minDistance = 0f;
    [SerializeField] LayerMask layerMask;

    private Rigidbody2D rb;
    private Camera cam;

    private Vector3 targetPosition;
    private Vector3 velocityVector;
    private bool isMoving = false;

    List<ContactPoint2D> contacts;
    ContactFilter2D filter;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        contacts = new List<ContactPoint2D>(8);
        filter = new ContactFilter2D();
        filter.layerMask = layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //truchada para que no se mueva cuando se toca el ui
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            SetTargetPosition(Input.mousePosition);
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            if (Vector3.Distance(transform.position, targetPosition) < minDistance)
            {
                Stop();
            }
            else
            {
                Move();
            }
        }
        rb.velocity = velocityVector * moveSpeed;
    }
    private void SetTargetPosition(Vector3 position)
    {
        targetPosition = cam.ScreenToWorldPoint(position);
        targetPosition.z = transform.position.z;
        isMoving = true;
    }
    private void Move()
    {
        Vector2 moveDirection = targetPosition - transform.position;//.normalized;
        //si estoy chocando algo
        if (rb.GetContacts(filter, contacts) > 0)
        {
            //si esta enfrente horizontal cancelo x
            if (contacts.Exists(x => Mathf.Abs(x.normal.x) > .01f && x.normal.x * moveDirection.x < 0))
            {
                moveDirection = (new Vector3(transform.position.x, targetPosition.y) - transform.position);
            }
            //si esta enfrente vertical cancelo y
            if (contacts.Exists(x => Mathf.Abs(x.normal.y) > .01f && x.normal.y * moveDirection.y < 0))
            {
                moveDirection = (new Vector3(targetPosition.x, transform.position.y) - transform.position);
            }
        }
        if (moveDirection.sqrMagnitude < .02f)
        {
            Stop();
            return;
        }
        velocityVector = moveDirection.normalized;
    }
    private void Stop()
    {
        isMoving = false;
        velocityVector = Vector3.zero;
    }
}