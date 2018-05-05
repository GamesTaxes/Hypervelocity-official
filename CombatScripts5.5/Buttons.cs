using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Buttons : MonoBehaviour
{
    private ButtonSideways buttonLeft;
    private ButtonSideways buttonRight;
    private Button buttonLaser;
    //private Button buttonStart;

    public Player player1 { get; set; }

    private float speed = 5.0f;
    private float fireRate = 0.25f;
    private float canFireButton = 0.0f;

    public GameObject laserPrefab;

    public void Start()
    {
        //buttonLeft = GameObject.Find("ButtonLeftTrigger").GetComponent<ButtonSideways>();
        //buttonRight = GameObject.Find("ButtonRightTrigger").GetComponent<ButtonSideways>();
        buttonLaser = GameObject.Find("ButtonLaserTrigger").GetComponent<Button>();
        //buttonStart = GameObject.Find("ButtonStartTrigger").GetComponent<Button>();

        buttonLaser.onClick.AddListener(() => buttonPressed(buttonLaser));
        //buttonStart.onClick.AddListener(() => buttonPressed(buttonStart));

    }

    private void Update()
    {
        //if (buttonLeft.GetPressed)
        //{
        //    player1.transform.Translate(Vector3.right * Time.deltaTime * speed * -1);
        //}

        //if (buttonRight.GetPressed)
        //{
        //    player1.transform.Translate(Vector3.right * Time.deltaTime * speed * 1);
        //}

    }

    public void buttonPressed(Button b)
    {

        Debug.Log("Burp");

        if (b == buttonLaser)
        {
            Player player1 = GetComponent<Player>();

            transform.position = player1.transform.position;

                Debug.Log("asd2");
                Instantiate(laserPrefab, transform.position, Quaternion.identity);
                player1.Fire();

            b.Equals("reset");
        }
    }
}
