using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class AlmacenGameManager : MonoBehaviour
{
    //para que se evalue el color de los botones.. (verde, gris, negro)
    bool mostrarBotones = true;

    //este para apagar el gameobject
    public GameObject tutorialAlmacenGameobject; 

    //niveles
    public int numEtapa;

    //[SerializeField] Text cajasEntregadas;
    [SerializeField] int cantEntrega = 0;
    [SerializeField] int cantTotalEntrega = 5;

    //para llamar al codigo de almacenMenu
    [SerializeField] AlmacenMenu almacenMenu;
    [SerializeField] SpriteAlmacen spriteAlmacen;
    [SerializeField] GameManager gameManager;
    [SerializeField] TutorialAlmacen tutorialAlmacen; //este para el script..

    //botoon entrega
    [SerializeField] Button entregaPedido;
    bool entregastePedido = false;
    [SerializeField] int cantPedidoEntregado = 0;

    int cantErrores = 0;
    [SerializeField] GameObject[] gameObjectError = new GameObject[3]; //los 5 elementos que deben ir a la caja..

    //botones para cambiar de escenario..
    public GameObject[] escenaSimbolos = new GameObject[5];

    //musica de fondo
    public AudioSource musicaAlmacen;
    public AudioSource sonidoAmbientalAlmacen;

    //para disparar el sonido una vez!. 
    public AudioClip audioCajaRegistradora;
    public AudioSource sonidosEntrega;

    //crear una class para las cajas...
    //nuevos
    List<EtapasAlmacen> guardarEtapasA; //este tiene todas la info de la cajas...

    ////creas la class que va a almacenar los pedidos...
    //public EtapasAlmacen elegirEtapasAlmacen; //este se encarga de crearla..

    // Start is called before the first frame update
    void Start()
    {
        almacenMenu = GameObject.Find("AlmacenCanvas").GetComponent<AlmacenMenu>();
        spriteAlmacen = GameObject.Find("GameManager").GetComponent<SpriteAlmacen>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //boton de entregar cada pedido
        entregaPedido = GameObject.Find("BotonEntrega").GetComponent<Button>();

        for (int i = 0; i < 3; i++)
        {
            gameObjectError[i] = GameObject.Find("Error" + i); //busca el gameobject que tiene el texto de numeros..
            gameObjectError[i].SetActive(false);
        }

        //obtener personaje?
        gameManager.getSexo();
        //obtener posicion en el museo?

        //obtener
    }
    void Update()
    {
        if(mostrarBotones)
        {
            ElementoEscena();

            //para que entre una sola vez..
            mostrarBotones = false;
        }      

        if (entregastePedido)
        {
            Perdiste();
            Ganaste();
        }        
    }
    public int NumEtapa()
    {
        return numEtapa;
    }

    public void ElegirEtapas()
    {       
        switch (numEtapa)
        {
            case 0:
                SceneManager.LoadScene("Almacenes1");
                break;
            case 1:
                SceneManager.LoadScene("Almacenes2");        
                break;
            case 2:
                SceneManager.LoadScene("Almacenes3");
                break;
            case 3:
                SceneManager.LoadScene("Almacenes4");
                break;
            case 4:
                SceneManager.LoadScene("Almacenes5");
                break;
        }
    }

    public void ElementoEscena()
    {
        guardarEtapasA = tutorialAlmacen.GuardadoEtapasAlmacen();

        //para la imagenes de los botones de escena
        for (int i = 0; i < 5; i++)
        {
            escenaSimbolos[i] = GameObject.Find("Escena" + i); //busca el gameobject que tiene el texto de numeros..           
            escenaSimbolos[i].GetComponent<Image>().sprite = guardarEtapasA[i].spriteEscena; // los pone del color correcto..
        }

        //antes de arracar.. pones gris la escena en la que te encuentras...
        escenaSimbolos[numEtapa].GetComponent<Image>().sprite = spriteAlmacen.GetInterfazSprite((int)enumInterfaz.InterfazSprite6); //color gris
    }

    public void BotonEtapaComida()
    {
        if (guardarEtapasA[(int)enumEtapas.EtapaComida].pedidoEntregado == false)
        {
            numEtapa = 0;
            ElegirEtapas(); //pero para mi, solo se podria elegir si no la hiciste... sino tendria que guardar cada cosa elegida!
        }                    
    }
    public void BotonEtapaRopa()
    {
        if (guardarEtapasA[(int)enumEtapas.EtapaRopa].pedidoEntregado == false)
        {
            numEtapa = 1;
            ElegirEtapas(); //pero para mi, solo se podria elegir si no la hiciste... sino tendria que guardar cada cosa elegida!
        }
    }
    public void BotonEtapaBebida()
    {
        if (guardarEtapasA[(int)enumEtapas.EtapaBebida].pedidoEntregado == false)
        {
            numEtapa = 2;
            ElegirEtapas(); //pero para mi, solo se podria elegir si no la hiciste... sino tendria que guardar cada cosa elegida!
        }
    }
    public void BotonEtapaFlores()
    {
        if (guardarEtapasA[(int)enumEtapas.EtapaFlores].pedidoEntregado == false)
        {
            numEtapa = 3;
            ElegirEtapas(); //pero para mi, solo se podria elegir si no la hiciste... sino tendria que guardar cada cosa elegida!
        }
    }
    public void BotonEtapaHerramientas()
    {
        if (guardarEtapasA[(int)enumEtapas.EtapaHerramientas].pedidoEntregado == false)
        {
            numEtapa = 4;
            ElegirEtapas(); //pero para mi, solo se podria elegir si no la hiciste... sino tendria que guardar cada cosa elegida!
        }
    }

    public void CambiarEtapa()
    {
        do
        {
            numEtapa += 1;
            if(numEtapa == 5)
            {
                numEtapa = 0;
            }
        } while (guardarEtapasA[numEtapa].pedidoEntregado == true);
    }

    public void ActivarBotonEntrega()
    {
        entregaPedido.interactable = true;
    }
    public void DesactivarBotonEntrega()
    {
        entregaPedido.interactable = false;
    }
    public void PresionasteBotonEntrega()
    {
        entregastePedido = true;

        //guardas si termino alguna etapa, para que no pueda entrar                
        guardarEtapasA[numEtapa].pedidoEntregado = true;

        //lo pone en verde, si completo el nivel... 
        guardarEtapasA[numEtapa].spriteEscena = spriteAlmacen.GetInterfazSprite((int)enumInterfaz.InterfazSprite5);

        //si entrega pedido lo pones verde...
        escenaSimbolos[numEtapa].GetComponent<Image>().sprite = spriteAlmacen.GetInterfazSprite((int)enumInterfaz.InterfazSprite5);

        //sonido
        sonidosEntrega.PlayOneShot(audioCajaRegistradora);

        for (int i = 0; i < 5; i++)
        {
            if(guardarEtapasA[i].pedidoEntregado == true)
            {
                //
                cantPedidoEntregado += 1;                
            }
        }

        if (cantPedidoEntregado < 5)
        {
            //cambiar numero de etapa...
            CambiarEtapa();
        }
        else
        {
            numEtapa = 5; //como para que salga del juego..
        }
    }
    public void SumaEntregas()
    {
        cantEntrega += 1;
        ControlEntrega();
    }
    public void ErroresCometidos()
    {
        cantErrores += 1;
        ControlEntrega();
    }
    public void ControlEntrega()
    {
        //le pones el sprite de punto economico que corresponda..
        switch (cantErrores)
        {
            case 1:
                gameObjectError[0].SetActive(true);
                break;
            case 2:
                gameObjectError[1].SetActive(true);
                break;
            case 3:
                gameObjectError[2].SetActive(true);
                break;
            case 4:
                //perdistee
                Perdiste();
                break;
        }
        //control Ganaste..
        Ganaste();
    }
    public void Ganaste()
    {
        //Si completaste todos los niveles esto...
        if (cantEntrega >= cantTotalEntrega)
        {
            if(entregastePedido)
            {
                almacenMenu.PanelFinal("Nivel Completo");
                almacenMenu.buttonPerdiste.SetActive(false);
                almacenMenu.buttonGanaste.SetActive(true);
            } 
        }

        if ((entregastePedido) && (cantPedidoEntregado == 5))
        {
            almacenMenu.PanelFinal("Niveles completos");
            almacenMenu.buttonPerdiste.SetActive(false);
            almacenMenu.buttonGanaste.SetActive(true);
        }
    }

    public void Perdiste()
    {
        //cuando entrega ma el pedido ----?
        almacenMenu.PanelFinal("Perdiste");
        almacenMenu.buttonPerdiste.SetActive(true);
        almacenMenu.buttonGanaste.SetActive(false);
        //cuando comete 4 errores
    }
}