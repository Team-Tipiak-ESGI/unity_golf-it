using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Button shootButton;
    public Slider powerSlider;
    private GameObject _ball;
    private Vector3 _respawnCoordinates;
    public int basePower = 100; // Puissance de tir maximale (quand le powerSlider est à 100%)
    public Button pauseButton;
    public Button restartButton;
    public Button moveButton; // Bouton pour tourner la caméra autour de la balle
    public float camDistance = 10.0f; // Distance entre la caméra et la balle

    private float _x = 0;
    private float _y = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Shoot listener
        shootButton.GetComponent<Button>().onClick.AddListener(Shoot);

        // Pause listener
        pauseButton.GetComponent<Button>().onClick.AddListener(Pause);

        // Restart listener
        restartButton.GetComponent<Button>().onClick.AddListener(Restart);

        _ball = GameObject.FindWithTag("Player");

        try
        {
            GameObject respawn = GameObject.FindWithTag("Respawn");
            _respawnCoordinates = respawn.transform.position;
            _respawnCoordinates.y += 1;
        } catch (Exception e) {}

        // Ajoute l'event de drag sur le bouton pour la caméra
        EventTrigger trigger = moveButton.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData) data); });
        trigger.triggers.Add(entry);
    }

    private void OnDragDelegate(PointerEventData data)
    {
        // Récupère l'event de drag
        _x += data.delta.x;
        _y += data.delta.y;
    }

    private void LateUpdate()
    {
        // Actualise la position de la caméra
        Vector3 position = _ball.transform.position;

        position.x += Mathf.Cos(_x * Mathf.PI / 180) * camDistance;
        position.y += camDistance + _y;
        position.z += Mathf.Sin(_x * Mathf.PI / 180) * camDistance;

        Camera.main.transform.position = position;
        Camera.main.transform.LookAt(_ball.transform.position); // La caméra regarde la balle
    }

    void Shoot()
    {
        // Récupère le slider
        Slider slider = powerSlider.GetComponent<Slider>();

        // Ajoute une force sur la balle
        _ball.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * slider.value * basePower);
    }

    void Restart()
    {
        // Recharge la scène actuelle
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        _ball.transform.position = _respawnCoordinates;
    }

    void Pause()
    {
        // Pause menu
        SceneManager.LoadScene("main_menu");
    }
}