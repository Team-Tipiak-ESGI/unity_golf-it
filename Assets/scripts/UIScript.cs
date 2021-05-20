using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 *
 * /!\ Script à appliquer sur la caméra /!\
 *
 */
public class UIScript : MonoBehaviour
{
    public Button shootButton;
    public Slider powerSlider;
    public GameObject ball;
    public int basePower = 100; // Puissance de tir maximale (quand le powerSlider est à 100%)
    public Button pauseButton;
    public Button restartButton;
    public Button moveButton; // Mouton pour tourner la caméra autour de la balle
    public float camDistance = 10.0f; // Distance entre la caméra et la balle

    private float x = 0;
    private float y = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Shoot listener
        shootButton.GetComponent<Button>().onClick.AddListener(Shoot);

        // Pause listener
        pauseButton.GetComponent<Button>().onClick.AddListener(Pause);

        // Restart listener
        restartButton.GetComponent<Button>().onClick.AddListener(Restart);
        
        
        // Ajoute l'event de drag sur le bouton pour la caméra
        EventTrigger trigger = moveButton.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }
    
    private void OnDragDelegate(PointerEventData data)
    {
        // Récupère l'event de drag
        x += data.delta.x;
        y += data.delta.y;
        //print((x * Mathf.PI / 180));
    }

    private void LateUpdate()
    {
        // Actualise la position de la caméra
        Vector3 position = ball.transform.position;

        float distance = camDistance;

        position.x += Mathf.Cos(x * Mathf.PI / 180) * distance;
        position.y += distance + y;
        position.z += Mathf.Sin(x * Mathf.PI / 180) * distance;

        transform.position = position;
        transform.LookAt(ball.transform.position); // La caméra regarde la balle
    }

    void Shoot()
    {
        // Récupère le slider
        Slider slider = powerSlider.GetComponent<Slider>();

        // Ajoute une force sur la balle
        ball.GetComponent<Rigidbody>().AddForce(this.transform.forward * slider.value * basePower);
    }

    void Restart()
    {
        // Recharge la scène actuelle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Pause()
    {
        // Pause menu
        SceneManager.LoadScene("main_menu");
    }
}
