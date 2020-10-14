using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtapasAlmacen
{
    public bool pedidoEntregado; //esto sirve para ver cada etapa.. si se entrego.. nada que ver a lo otro.. pero ni daba hacer una clase para esto nomas...
    public int numEtapaAlmacen; //esto sirve para ver cada etapa.. si se entrego.. nada que ver a lo otro.. pero ni daba hacer una clase para esto nomas...

    public Sprite spriteEscena;

    // Start is called before the first frame update
    void Start()
    {
        pedidoEntregado = false;
        numEtapaAlmacen = 0;

        //el color del momento..
        spriteEscena = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
