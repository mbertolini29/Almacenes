using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedidoAlmacen
{
    public bool pedidoEntregado; //esto sirve para ver cada etapa.. si se entrego.. nada que ver a lo otro.. pero ni daba hacer una clase para esto nomas...

    public bool cajaLlena;

    public int _numCaja;
    public int cantElementosCorrectos; // del 1 al 5 de la cantidad que tiene que poner
    public int cantElementosPuestos;
    public List<int> numSpriteCorrecto; //el numero de sprite que debe ir a la caja 
    public List<int> numSpritePuesto; //el numero de sprite que el personaje pone en la caja 

    public PedidoAlmacen()
    {
        pedidoEntregado = false;
        cajaLlena = false;

        cantElementosCorrectos = 0;
        cantElementosPuestos = 0;
        numSpriteCorrecto = new List<int>();
        numSpritePuesto = new List<int>();
    }
    //nuevos
    public void GuardarNumSprite(int _numSprite)
    {
        numSpriteCorrecto.Add(_numSprite);
    }
    public int NumSpriteCajaCorrecto() //num es la posicion en la lista?
    {
        return numSpriteCorrecto[0];
    }
    public int SumarElemEnCaja()
    {
        cantElementosPuestos++;
        return cantElementosPuestos;
    }
    public int RestarElemEnCaja()
    {
        if(cantElementosPuestos > 0)
        {
            cantElementosPuestos--;
        }
        return cantElementosPuestos;
    }

}
