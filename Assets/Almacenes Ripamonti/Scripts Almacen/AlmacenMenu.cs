using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AlmacenMenu : MonoBehaviour
{
    [SerializeField] Text title;

    [SerializeField] Button continueButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button mainMenuButton;

    [SerializeField] GameObject panelPause;
    [SerializeField] GameObject panelButton;
    [SerializeField] GameObject panelFinal;

    //botones de perdiste y ganaste...
    public GameObject buttonPerdiste;
    public GameObject buttonGanaste;

    [SerializeField] GameObject MusicaOn; // si lo activas, se apaga la imagen..
    [SerializeField] GameObject MusicaOff;

    [SerializeField] GameObject SonidoOn; // si lo activas, se apaga la imagen..
    [SerializeField] GameObject SonidoOff;

    private bool musica = true;
    private bool sonido = true;

    private bool paused = false;

    [SerializeField] AlmacenGameManager almacenGameManager;
    [SerializeField] GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //agregar el script de itemAlmacen..
        almacenGameManager = GameObject.Find("GameManager").GetComponent<AlmacenGameManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();        
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void Musica()
    {
        musica = !musica;

        if(musica)
        {
            almacenGameManager.musicaAlmacen.mute = false;
            MusicaOn.SetActive(false); //activa la mascara y se apaga el boton
            MusicaOff.SetActive(true);
        }
        else
        {
            almacenGameManager.musicaAlmacen.mute = true;
            MusicaOn.SetActive(true);
            MusicaOff.SetActive(false);
        }        
    }

    public void Sonido()
    {
        sonido = !sonido;

        if (sonido)
        {
            almacenGameManager.sonidoAmbientalAlmacen.mute = false;
            almacenGameManager.sonidosEntrega.mute = false;
            SonidoOn.SetActive(false); //activa la mascara y se apaga el boton
            SonidoOff.SetActive(true);

            //estaria faltando algunos sonidos!
        }
        else
        {
            almacenGameManager.sonidoAmbientalAlmacen.mute = true;
            almacenGameManager.sonidosEntrega.mute = true;
            SonidoOn.SetActive(true); //activa la mascara y se apaga el boton
            SonidoOff.SetActive(false);
        }
    }

    public void Pause()
    {
        paused = !paused;
        panelPause.SetActive(paused);
        Time.timeScale = paused ? 0 : 1;
    }

    public void PanelFinal(string text)
    {
        title.text = text;
        panelFinal.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Museo");

        //gameManager.banderaAlmacenes = 0; //esto para que no se rompa

        //SceneManager.LoadScene((int)Escenas.Portada);
    }

    public void SiguienteEtapa()
    {
        switch (almacenGameManager.NumEtapa())
        {
            case 0:
                SceneManager.LoadScene("Almacenes1");
                //SceneManager.LoadScene((int)Escenas.AlmacenComida);
                break;
            case 1:
                SceneManager.LoadScene("Almacenes2");
                //SceneManager.LoadScene((int)Escenas.AlmacenRopa);                
                break;
            case 2:
                SceneManager.LoadScene("Almacenes3");
                //SceneManager.LoadScene((int)Escenas.AlmacenBebida);
                break;
            case 3:
                SceneManager.LoadScene("Almacenes4");
                //SceneManager.LoadScene((int)Escenas.AlmacenFlor);
                break;
            case 4:
                SceneManager.LoadScene("Almacenes5");
                //SceneManager.LoadScene((int)Escenas.AlmacenHerramientas);
                break;
            case 5:
                //deberia irse a la concha de la lora!

                gameManager.GanoEtapa("almacenes"); //esto para que no vuelva a entrar en el juego..     
                
                SceneManager.LoadScene("Museo");

                break;
        }
    }

    public void EndGame(string text)
    {
        title.text = text;
        continueButton.gameObject.SetActive(false);
        panelButton.SetActive(false);
        Pause();
    }
}