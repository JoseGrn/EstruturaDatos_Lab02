﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaGenerica.Estructuras
{
  public class ArbolBinarioBusqueda<T> : Interfaces.EstruturaBaseArbol<T> where T : IComparable
    {
        Nodo<T> Raiz = new Nodo<T>();
        Nodo<T> Eliminar = new Nodo<T>();
        public void Add(T Valor, Delegate delegado)
        {
            Insertar(Valor, delegado, Raiz);
        }
        public void Delete(T Valor, Delegate Delegado)
        {
            Borrar(Valor, Delegado, Raiz);
        }
        public T Get(T Valor, Delegate Delegado)
        {
            return Obtener(Valor, Delegado);
        }
        public List<T> Mostrar()
        {
            List<T> ListaArbol = new List<T>();
            MostrarRaiz(Raiz, ListaArbol);
            return ListaArbol;
        }

        private void MostrarRaiz(Nodo<T> NodoRaiz, List<T> Lista)
        {
            if (NodoRaiz.Valor != null)
            {
                Lista.Add(NodoRaiz.Valor);
                MostrarRaiz(NodoRaiz.Izquierda, Lista);
                MostrarRaiz(NodoRaiz.Derecha, Lista);
            }
        }
        protected override void Borrar(T Valor, Delegate Delegado, Nodo<T> NodoRaiz)
        {
            if (Convert.ToInt32(Delegado.DynamicInvoke(Valor, NodoRaiz.Valor)) == 1)
            {
                Borrar(Valor, Delegado, NodoRaiz.Derecha);
            }
            else if (Convert.ToInt32(Delegado.DynamicInvoke(Valor, NodoRaiz.Valor)) == -1)
            {
                Borrar(Valor, Delegado, NodoRaiz.Izquierda);
            }
            else if (Convert.ToInt32(Delegado.DynamicInvoke(Valor, NodoRaiz.Valor)) == 0)
            {
                Eliminar = NodoRaiz;
                if (NodoRaiz.Izquierda.Valor == null && NodoRaiz.Derecha.Valor == null)
                {
                    NodoRaiz = new Nodo<T>();
                }
                else
                {
                    NodoRaiz = NodoRaiz.Derecha;
                    Nodo<T> NodoSustituto = new Nodo<T>();
                    while (NodoRaiz.Izquierda.Valor != null)
                    {
                        NodoSustituto = NodoRaiz;
                        NodoRaiz = NodoRaiz.Izquierda;
                    }
                    Eliminar.Valor = NodoRaiz.Valor;
                    Eliminar = NodoRaiz.Derecha;
                    NodoSustituto.Izquierda = Eliminar;
                }
            }
        }

        protected override T Obtener(T Valor, Delegate Delegado)
        {
            Nodo<T> NodoPivote = Raiz;
            while (NodoPivote.Valor != null)
            {
                if (Convert.ToInt32(Delegado.DynamicInvoke(Valor, NodoPivote.Valor)) == 1)
                {
                    if (NodoPivote.Derecha.Valor != null)
                    {
                        NodoPivote = NodoPivote.Derecha;
                    }
                    else
                    {
                        return NodoPivote.Derecha.Valor;
                    }
                }
                else if (Convert.ToInt32(Delegado.DynamicInvoke(Valor, NodoPivote.Valor)) == -1)
                {
                    if (NodoPivote.Izquierda.Valor != null)
                    {
                        NodoPivote = NodoPivote.Izquierda;
                    }
                    else
                    {
                        return NodoPivote.Derecha.Valor;
                    }
                }
                else
                {
                    return NodoPivote.Valor;
                }
            }
            return NodoPivote.Valor;

        }

        protected override void Insertar(T Valor, Delegate Delegado, Nodo<T> NodoRaiz)
        {
            if (NodoRaiz.Valor == null)
            {
                NodoRaiz.Valor = Valor;
                NodoRaiz.Izquierda = new Nodo<T>();
                NodoRaiz.Derecha = new Nodo<T>();
            }
            else if (Convert.ToInt32(Delegado.DynamicInvoke(Valor, NodoRaiz.Valor)) == 1)
            {
                Insertar(Valor, Delegado, NodoRaiz.Derecha);
            }
            else if (Convert.ToInt32(Delegado.DynamicInvoke(Valor, NodoRaiz.Valor)) == -1)
            {
                Insertar(Valor, Delegado, NodoRaiz.Izquierda);
            }
        }

    }
}