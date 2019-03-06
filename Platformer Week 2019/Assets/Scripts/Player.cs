using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject Wave = null;

    [SerializeField]
    private float speed = 10;

    [SerializeField]
    private AudioClip JumpSound = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Wave.SetActive(false);
    }

    private void Update()
    {
        if (Wave.activeSelf)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        //rb.AddForce(horizontal * Vector2.right * Time.deltaTime * speed, ForceMode2D.Impulse);
        transform.Translate(horizontal * 3 * Time.deltaTime, 0, 0, transform);

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (rb.velocity == Vector2.zero && horizontal == 0 && Input.GetKeyDown(KeyCode.C))
            StartCoroutine(DoWave());
    }

    private void Jump()
    {
        SoundPlayer.Play(JumpSound);

        rb.AddForce(Vector2.up * Time.deltaTime * 300, ForceMode2D.Impulse);
    }

    private IEnumerator DoWave()
    {
        Wave.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        Wave.SetActive(false);
    }
}
