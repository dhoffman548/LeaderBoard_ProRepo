using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rig;

    private float startTime;
    private float timeTaken;

    private int collectabledPicked;
    public int maxCollectables = 10;

    public GameObject collectable1;
    public GameObject collectable2;
    public GameObject collectable3;
    public GameObject collectable4;
    public GameObject collectable5;
    public GameObject collectable6;
    public GameObject collectable7;
    public GameObject collectable8;
    public GameObject collectable9;
    public GameObject collectable10;
    public GameObject playButton;
    public GameObject speedGroup;
    public GameObject player;
    public TextMeshProUGUI curTimeText;
    private Rigidbody orb;
    private Rigidbody orb1;

    private bool isPlaying;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isPlaying)
            return;

        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;

        rig.velocity = new Vector3(x, rig.velocity.y, z);

        curTimeText.text = (Time.time - startTime).ToString("F2");

        OutofBounds();
    }

    void CollectMove ()
    {
        orb = collectable1.GetComponent<Rigidbody>();
        orb1 = collectable2.GetComponent<Rigidbody>();

        Invoke("Up", 0.0f);
        Invoke("Up1", 0.0f);
    }

    void Up()
    {
        orb.velocity = new Vector3(2, 0, 0);
        Invoke("Down", 1.5f);
    }

    void Up1()
    {
        orb1.velocity = new Vector3(0, 0, 2);
        Invoke("Down1", 2.75f);
    }

    void Down()
    {
        orb.velocity = new Vector3(-2, 0, 0);
        Invoke("Up", 1.5f);
    }

    void Down1()
    {
        orb1.velocity = new Vector3(-2, 0, 0);
        Invoke("Right", 2.75f);
    }

    void Right()
    {
        orb1.velocity = new Vector3(2, 0, 0);
        Invoke("Down2", 2.75f);
    }

    void Down2()
    {
        orb1.velocity = new Vector3(0, 0, -2);
        Invoke("Up1", 2.75f);
    }

    void Stop()
    {
        orb.velocity = new Vector3(0, 0, 0);
        orb1.velocity = new Vector3(0, 0, 0);
    }

    void Reset()
    {
        orb.transform.position = new Vector3(1.5f, 0.5f, -4);
        orb1.transform.position = new Vector3(7, 0.5f, -1.5f);
    }

    public void Begin ()
    {
        playButton.SetActive(false);
        speedGroup.SetActive(true);
        collectablesActive();
        player.transform.position = new Vector3(0, 1, 0);
        collectabledPicked = 0;
        rig.useGravity = true;
        CollectMove();
        Reset();
    }

    public void NormalButton ()
    {
        startTime = Time.time;
        isPlaying = true;
        speed = 3.0f;
        speedGroup.SetActive(false);
    }

    public void FastButton ()
    {
        startTime = Time.time;
        isPlaying = true;
        speed = 5.0f;
        speedGroup.SetActive(false);
    }
    public void InsaneButton()
    {
        startTime = Time.time;
        isPlaying = true;
        speed = 7.5f;
        speedGroup.SetActive(false);
    }

    void OutofBounds()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (!Physics.Raycast(ray, 20f))
        {
            startTime = Time.time;
            End();
        }
    }
    void End ()
    {
        timeTaken = Time.time - startTime;
        isPlaying = false;
        Leaderboard.instance.SetLeaderboardEntry(-Mathf.RoundToInt(timeTaken * 1000.0f));
        playButton.SetActive(true);
        Invoke("NoGravity", 3.0f);
        Stop();

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Collectable"))
        {
            collectabledPicked++;
            other.gameObject.SetActive(false);
            speed += speed * .1f;

            if (collectabledPicked == maxCollectables)
                End();
        }
    }

    void collectablesActive()
    {
        collectable1.SetActive(true);
        collectable2.SetActive(true);
        collectable3.SetActive(true);
        collectable4.SetActive(true);
        collectable5.SetActive(true);
        collectable6.SetActive(true);
        collectable7.SetActive(true);
        collectable8.SetActive(true);
        collectable9.SetActive(true);
        collectable10.SetActive(true);
    }

    void NoGravity()
    {
        rig.useGravity = false;
    }
}