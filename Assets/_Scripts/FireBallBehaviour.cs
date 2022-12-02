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
    }

    public IEnumerator Fire()
    {
        while(true)
        {
            transform.localPosition += new Vector3(0, 0.1f * _velocity);
            yield return new WaitForSeconds(0.1f);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy" || collision.collider.tag == "Helicopter")
        {
            StopCoroutine(Fire());
            Destroy(collision.gameObject);
            Destroy(this);
        }
    }
}
