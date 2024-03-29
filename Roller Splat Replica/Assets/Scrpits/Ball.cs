using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public Rigidbody _rb;

    private GameManager _gm;
    

    public Image _levelBar;

    private Vector2  _firstPos;

    private Vector2  _secondPos;

    private Vector2  _currentPos;

    public float _moveSpeed;

    public float _currentGroundNumber;
    void Start()
    {
        _gm = GameObject.FindObjectOfType<GameManager>();
    }

  
    void Update()
    {
        Swipe();
        _levelBar.fillAmount = _currentGroundNumber / _gm._groundNumbers;
       
    }

    public void Swipe()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _firstPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if(Input.GetMouseButtonUp(0))
        {
            _secondPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            _currentPos = new Vector2(
               _secondPos.x - _firstPos.x,
               _secondPos.y - _firstPos.y
           );
            _currentPos.Normalize();
        }

        if (_currentPos.y > 0 && _currentPos.x > -0.5f && _currentPos.x < 0.5f)
        {
            // Forward
            _rb.velocity = Vector3.forward * _moveSpeed;
        }
        else if (_currentPos.y < 0 && _currentPos.x > -0.5f && _currentPos.x < 0.5f)
        {
            // Back
            _rb.velocity = Vector3.back * _moveSpeed;
        }
        else if (_currentPos.x > 0 && _currentPos.y > -0.5f && _currentPos.y < 0.5f)
        {
            // Right
            _rb.velocity = Vector3.right * _moveSpeed;
        }
        else if (_currentPos.x < 0 && _currentPos.y > -0.5f && _currentPos.y < 0.5f)
        {
            // Left
            _rb.velocity = Vector3.left * _moveSpeed;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<MeshRenderer>().material.color != Color.red)
        {
            if (other.gameObject.tag == "Ground")
            {
                Constraints();
                other.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                _currentGroundNumber++;
            }
        }
    }
    private void Constraints()
    {
        _rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }
}

