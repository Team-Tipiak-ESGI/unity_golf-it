using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallCamera : MonoBehaviour
{
    public GameObject Balle; // la balle
    public float camDistance = 3.5f; // distance cam-balle
    // angles:
    float x = 0.0f;
    float y = 0.0f;
    Quaternion rotation;
    Touch touch1; // référence si on touche l'écran tactile

    // Start is called before the first frame update
    void Start()
    {
        x = transform.eulerAngles.y;
        y = transform.eulerAngles.x;

    }

  

    void LateUpdate() // s'exécute après Update (on connaît la position de la cam)
    {
        // mises en place des directives
        #if UNITY_EDITOR || UNITY_STANDALONE //fonctionne sur l'éditeur PC
                x += Input.GetAxis("Mouse X") * 3; // Position de la souris à l'écran: axe des x (*3: bonne vitesse)
        #endif

        #if UNITY_IPHONE || UNITY_ANDROID //fonctionne sur iphone ou Android
                if (Input.touches.Length == 1) // si on a un doigt sur l'écran
                {
                    x += Input.GetTouch(0).deltaPosition.x * O.1f// La position du doigt sur l'axe des x est calculée
                }
        #endif

        // si on ne touche pas un élément d'interface
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            rotation = Quaternion.Euler(y, x, 0); // On oriente la caméra
        }

        // calcul de la position de la caméra
        Vector3 position = rotation * new Vector3(0.0f, Balle.transform.position.y / 3, -camDistance) + Balle.transform.position; 
                                                        // O.Of: caméra centrée sur la balle   et  /3: balle à environ 1/4 du bas de l'écran

        // On applique à la cam la rotation et la position calculées précédemment
        transform.rotation = rotation;
        transform.position = position;

        // blocage de la cam en y (pour éviter de tomber ds le vide si la balle tombe du niveau)
        if (transform.rotation.y < 3.0f)
        {
            transform.position = new Vector3(transform.position.x, 3.0f, transform.position.z);
        }


    }
}
