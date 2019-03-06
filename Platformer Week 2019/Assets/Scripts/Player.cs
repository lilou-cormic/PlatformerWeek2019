using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private static Player _instance;

    private Rigidbody2D rb;

    [SerializeField]
    private Animator Animator = null;

    [SerializeField]
    private SpriteRenderer SpriteRenderer = null;

    [SerializeField]
    private GameObject Light = null;

    [SerializeField]
    private GameObject Wave = null;

    [SerializeField]
    private AudioClip JumpSound = null;

    private void Awake()
    {
        _instance = this;

        rb = GetComponent<Rigidbody2D>();

        Wave.SetActive(false);
    }

    private void Update()
    {
        //if (Wave.activeSelf)
        //    return;

        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal < -0.01)
            SpriteRenderer.flipX = true;
        else if (horizontal > 0.01)
            SpriteRenderer.flipX = false;

        //rb.AddForce(horizontal * Vector2.right * Time.deltaTime * speed, ForceMode2D.Impulse);
        transform.Translate(horizontal * 3 * Time.deltaTime, 0, 0, transform);

        Animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (Input.GetButtonDown("Jump"))
            Jump();


        if (Mathf.Abs(rb.velocity.y) <= 0.01)
            Animator.SetBool("IsJumping", false);

        if (/*rb.velocity == Vector2.zero && horizontal == 0 && */Input.GetButtonDown("Fire2"))
            StartCoroutine(DoWave());
    }

    private void Jump()
    {
        if (rb.velocity.y != 0)
            return;

        SoundPlayer.Play(JumpSound);
        Animator.SetBool("IsJumping", true);

        rb.AddForce(Vector2.up * Time.deltaTime * 300, ForceMode2D.Impulse);
    }

    private IEnumerator DoWave()
    {
        Wave.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        Wave.SetActive(false);
    }

    public static void SetPosition(Vector3 value)
    {
        _instance.transform.position = value;
    }

    public static void SetLightActive(bool value)
    {
        _instance.Light.SetActive(value);
    }

    public static bool IsGoingUp()
    {
        return _instance.rb.velocity.y > 0.01;
    }

    public static float GetY()
    {
        return _instance.transform.position.y;
    }
}
