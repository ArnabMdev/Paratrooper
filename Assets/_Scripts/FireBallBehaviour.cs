using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private float _velocity;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger("Firing");
        StartCoroutine(Fire());
    }

    public IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.2f);
        while(true)
        {
            transform.position += transform.up * _velocity;
            yield return new WaitForSeconds(0.1f);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy" || collision.transform.tag == "Helicopter")
        {
            StopCoroutine(Fire());
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy" || collision.transform.tag == "Helicopter")
        {
            StopCoroutine(Fire());
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
