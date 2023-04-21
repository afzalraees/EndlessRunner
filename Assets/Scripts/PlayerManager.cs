using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { Left, Mid, Right};
public class PlayerManager : MonoBehaviour
{
    public UIManager uiManager;
    Vector3 actualGravity;
    public float moveSpeed;
    float timer;
    public Side side = Side.Mid;
    public bool swipeLeft, swipeRight;
    public float xValue;
    public int pointCounter;
    float newXPos = 0;
    public BoxCollider boxCollider;
    Rigidbody rb;
    Animator anim;
    public Animator camAnim;
    public MoonManager moon;

    public int score;
    public Transform directionalLight;
    public bool isMoonSpawn;

    [SerializeField] float jumpForce;
    bool isRolling, isJumping, moonJump;

    public GameObject hurdle, points;

    public static PlayerManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Time.timeScale = 1;
    }
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        actualGravity = Physics.gravity;
        Debug.Log(actualGravity);
    }

    public void RollingBack()
    {
        isRolling = false;
        boxCollider.enabled = true;
    }

    public void GameComplete()
    {
        hurdle.SetActive(false);
        points.SetActive(false);
        anim.SetTrigger("Walk");
        camAnim.SetTrigger("GameComplete");
        directionalLight.eulerAngles = new Vector3(directionalLight.eulerAngles.x, -150, directionalLight.eulerAngles.z);
    }
    IEnumerator Jumping()
    {
        yield return new WaitForSeconds(1);
        isJumping= false;
    }

    IEnumerator Dead()
    {
        anim.SetTrigger("Dead");
        yield return new WaitForSeconds(1);
        GameOver();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        uiManager.gameOverScreen.SetActive(true);
        
    }
    // Update is called once per frame
    void Update()
    {
        Controls();
        DifficultyClock();
    }

    void DifficultyClock()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {
            PlayerManager.instance.moveSpeed++;
            timer = 0;
        }
    }
    float x = 0;
    void Controls()
    {
        swipeLeft = Input.GetKeyDown(KeyCode.A);
        swipeRight = Input.GetKeyDown(KeyCode.D);
        if (swipeLeft)
        {
            if (side == Side.Mid)
            {
                newXPos = -xValue;
                side = Side.Left;
            }
            else if (side == Side.Right)
            {
                newXPos = 0;
                side = Side.Mid;
            }
        }
        else if (swipeRight)
        {
            if (side == Side.Mid)
            {
                newXPos = xValue;
                side = Side.Right;
            }
            else if (side == Side.Left)
            {
                newXPos = 0;
                side = Side.Mid;
            }
        }

        else if (Input.GetKeyDown(KeyCode.S) && !isRolling)
        {
            anim.SetTrigger("Roll");
            isRolling = true;
            boxCollider.enabled = false;
        }
        
            
        else if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            if (!moonJump)
            {
                rb.AddForce(Vector3.up * jumpForce);
                anim.SetTrigger("Jump");
                StartCoroutine(Jumping());
            }
            else
            {
                rb.AddForce(Vector3.up * jumpForce);
                anim.SetTrigger("MoonJump");
                StartCoroutine(CanMoonJump());
            }
            
        }
        x = Mathf.Lerp(x, newXPos, Time.deltaTime * 5);
        transform.Translate((x - transform.position.x) * Vector3.right);
    }


    IEnumerator CanMoonJump()
    {
        yield return new WaitForSeconds(5);
        isJumping = false;
        moonJump = false;
    }

    int moonSpawnCollection;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Hurdle")
        {
            StartCoroutine(Dead());
        }
        if (collision.collider.gameObject.tag == "Point")
        {
            score++;
            uiManager.UpdateScore();
            if(score == 100)
            {
                GameComplete();
            }
            if(!isMoonSpawn)
            {
                moonSpawnCollection++;
                if (moonSpawnCollection == 25)
                {
                    isMoonSpawn = true;
                    moon.gameObject.SetActive(true);
                    moon.NewRandomSpawn();
                    moonSpawnCollection = 0;
                    StartCoroutine(WaitForMoon());
                }
            }


            pointCounter++;
            collision.collider.gameObject.SetActive(false);
            
            if(pointCounter == collision.collider.GetComponent<PointManager>().transform.parent.GetComponent<PointScript>().pointLength)
            {
                collision.collider.GetComponent<PointManager>().transform.parent.GetComponent<PointScript>().GeneratePointsrandomly();
            }

        }
        if (collision.collider.gameObject.tag == "Moon")
        {
            moonJump = true;
            /*collision.collider.GetComponent<MoonManager>().RandomSpawn();*/
            Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y / 5, Physics.gravity.z);
            StartCoroutine(BackToNormal());
            moon.gameObject.SetActive(false);
        }
    }

    IEnumerator WaitForMoon()
    {
        yield return new WaitForSeconds(5);
        isMoonSpawn = false;
        moon.gameObject.SetActive(false);
    }

    IEnumerator BackToNormal()
    {
        yield return new WaitForSeconds(5.0f);
        jumpForce = 250f;
        Physics.gravity = actualGravity;
    }
}
