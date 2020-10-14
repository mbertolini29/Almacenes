using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAlmacen : MonoBehaviour
{
    [SerializeField] AlmacenGameManager almacenGameManager;
    [SerializeField] SpriteAlmacen spriteAlmacen;

    [SerializeField] int numTutorial = 0;
    bool resaltado = true;
    bool prenderResaltadores = true;

    string nameTutorial = "TutorialAlmacen";
    
    public bool mostrarTutorial = true;
    public bool terminoTutorial = false;

    //tiempo
    float waitTime = 0.3f;
    float timer = 0.0f;

    //tutorial
    [SerializeField] GameObject[] tutorial = new GameObject[7];
    [SerializeField] GameObject[] tutoriaResaltados = new GameObject[6];

    //crear una class para las cajas...
    public static List<EtapasAlmacen> guardarEtapasAlmacen; //este tiene todas la info de la cajas...

    //creas la class que va a almacenar los pedidos...
    public EtapasAlmacen elegirEtapasAlmacen; //este se encarga de crearla..

    // 
    void OnDestroy()
    {
        //esto anda en el celu..
        if (Application.isPlaying == false)
        {
            PlayerPrefs.SetInt(nameTutorial, 1);
        }

        //y esto es para trabajar programando.. porque el de arriba no funciona
        //y si lo dejas descomentado... no va dejarte exportarlo...

        if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
        {
            Debug.Log("paro el editor...");
            PlayerPrefs.SetInt(nameTutorial, 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        almacenGameManager = GameObject.Find("GameManager").GetComponent<AlmacenGameManager>();
        spriteAlmacen = GameObject.Find("GameManager").GetComponent<SpriteAlmacen>();

        if (PlayerPrefs.GetInt(nameTutorial, 1) == 1)
        {
            Debug.Log("First Time Opening");

            //Set first time opening to false
            PlayerPrefs.SetInt(nameTutorial, 0);

            //Do your stuff here
            for (int i = 0; i < 7; i++)
            {
                tutorial[i] = GameObject.Find("TutorialAlmacen" + i); //busca el gameobject que tiene el texto de numeros..
                tutorial[i].SetActive(false);
            }

            //arranco el tutorial
            tutorial[numTutorial].SetActive(true);

            //
            //creas la clase que guarda si se termino algun nivel...
            guardarEtapasAlmacen = new List<EtapasAlmacen>(); //este tiene todas la info de la cajas...

            for (int i = 0; i < 5; i++)
            {
                //creas la class que va a almacenar los pedidos...
                elegirEtapasAlmacen = new EtapasAlmacen(); //este se encarga de crearla..

                elegirEtapasAlmacen.numEtapaAlmacen = i;
                elegirEtapasAlmacen.pedidoEntregado = false;

                //elegirEtapasAlmacen.spriteEscenaCompleta = spriteAlmacen.GetInterfazSprite((int)enumInterfaz.InterfazSprite5); //color verde
                //elegirEtapasAlmacen.spriteEscenaMomento = spriteAlmacen.GetInterfazSprite((int)enumInterfaz.InterfazSprite6); //color gris
                //elegirEtapasAlmacen.spriteEscenaNoCompleta = spriteAlmacen.GetInterfazSprite((int)enumInterfaz.InterfazSprite7); //color negro

                //est
                elegirEtapasAlmacen.spriteEscena = spriteAlmacen.GetInterfazSprite((int)enumInterfaz.InterfazSprite7); //color negro

                guardarEtapasAlmacen.Add(elegirEtapasAlmacen);
            }
        }
        else
        {
            if (almacenGameManager.numEtapa == 0)
            {
                almacenGameManager.tutorialAlmacenGameobject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if (almacenGameManager.numEtapa == 0)
        {
            timer += Time.deltaTime;

            if ((Input.GetMouseButtonDown(0)) && (numTutorial < 7))
            {
                tutorial[numTutorial].SetActive(false);

                numTutorial++;

                if (numTutorial < 7)
                {
                    tutorial[numTutorial].SetActive(true);
                    tutoriaResaltados[numTutorial - 1].SetActive(true);
                }

                if (numTutorial > 1)
                {
                    tutoriaResaltados[numTutorial - 2].SetActive(false);
                }
            }
            else if ((timer > waitTime) && (prenderResaltadores))
            {
                if (numTutorial > 6)
                {
                    tutoriaResaltados[numTutorial - 2].SetActive(false);
                    prenderResaltadores = false;
                }
                else if (numTutorial > 0)
                {
                    tutorial[numTutorial].SetActive(true);

                    PrendeResaltador(numTutorial - 1);
                    timer = 0.0f;
                }
            }
        }
    }

    public List<EtapasAlmacen> GuardadoEtapasAlmacen()
    {
        return guardarEtapasAlmacen;
    }

    public void PrendeResaltador(int _i)
    {
        resaltado = !resaltado;
        tutoriaResaltados[_i].SetActive(resaltado);
        //tutoriaResaltados[_i].GetComponent<Image>().enabled = !tutoriaResaltados[_i].GetComponent<Image>().enabled;
    }
}
