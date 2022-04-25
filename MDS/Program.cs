using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;




namespace MDS
{
    internal class Program
    {


        public struct Resultados
        {
            public int posicion;
            public string categoria;
            public string piloto;
            public int puntos;

            public Resultados(string categoria, int posicion, string piloto, int puntos)
            {
                this.categoria = categoria;
                this.posicion = posicion;
                this.piloto = piloto;
                this.puntos = puntos;
            }

           
        }

        static List<Resultados> listaCampeonato;


        static void Main(string[] args)
        {

            string[] lineas = File.ReadAllLines("./resultados.csv");    //Leo Result y guardo todas las filas en un array de string "lineas"

            Resultados[] vectResultados = new Resultados[lineas.Length];
            listaCampeonato = new List<Resultados>();                         //Creo una lista del Tipo Resultados


            leerResultados(lineas, vectResultados);     

            leerCampeonato(listaCampeonato);    //Hasta acá funciona. 









        }

        static void leerResultados(string[] lineas, Resultados[] vectResultados)
        {
            string[] catPilotos = File.ReadAllLines("./pro-am.csv");    //Leo pro-am y guardo todas las filas en un array de string "catPilotos"
                                                                        // en catPilotos[0] >> PRO,Ariel Pacho,Tomas Anton,Andres Ruiz,Sebastián Gallego,Martin Facal,....
                                                                        // en catPilotos[1] >> AM,Maxi Cerrella,Luis Piloni,MR Mazza,Mattias Pascual,.....

            int[] puntaje = new int[] { 0, 30, 26, 23, 21, 19, 17, 15, 13, 11, 12, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }; //tabla de puntales del [1] al [20]

            var catPro = catPilotos[0].Split(',');                    //   catPro >> PRO,Ariel Pacho,Tomas Anton,Andres Ruiz,Sebastián Gallego,Martin Facal,....    
            var catAm = catPilotos[1].Split(',');                     //    catAm >> AM,Maxi Cerrella,Luis Piloni,MR Mazza,Mattias Pascual,.....

            int i = 0;
            int posPro = 1;
            int posAm = 1;

            foreach (var linea in lineas)
            {
                var valores = linea.Split(',');
                string strPiloto = valores[7].TrimEnd('"');    //Le saco las "" extras
                strPiloto = strPiloto.TrimStart('"');

                for (int x = 0; x < catPro.Length; ++x)
                {           //recorre los pilotos Pro
                    if (strPiloto == catPro[x])
                    {
                        vectResultados[i].piloto = strPiloto;
                        vectResultados[i].posicion = posPro;
                        vectResultados[i].categoria = "Pro";
                        vectResultados[i].puntos = puntaje[posPro];
                        posPro++;
                    }  //cierro if 
                }

                for (int x = 0; x < catAm.Length; ++x)
                {           //recorre los pilotos Am
                    if (strPiloto == catAm[x])
                    {
                        vectResultados[i].piloto = strPiloto;
                        vectResultados[i].posicion = posAm;
                        vectResultados[i].categoria = "Am";
                        vectResultados[i].puntos = puntaje[posAm];
                        posAm++;
                    }  //cierro if 
                }    //cierro for
                i++;
            }  // cierra el foreach  -- En el vector resultados tengo las tablas de puntos del evento    
        }  // cierra leerResultados


        static void leerCampeonato(List<Resultados> listaCampeonato)
        {
            string[] lineasArchivo = File.ReadAllLines("./TablaCampeonatos.csv");    //Leo TablaCampeonato y guardo todas las filas en un array de string "lineasTabla"


            //tengo que recorrer el array "lineasArchivo" e ir argegando (add) los datos a la "lista" de tipo Resultados

            foreach (var linea in lineasArchivo)
            {
                var valores = linea.Split(',');  //valores {Pro,1,Ariel Pacho,30} son todos string

                listaCampeonato.Add(new Resultados(valores[0], Int32.Parse(valores[1]), valores[2], Int32.Parse(valores[3])));

            }  // cierra el foreach  -- Cargo en el vector la tabla del archivo 


            foreach (var linea in listaCampeonato)
            {
                Console.WriteLine(linea.categoria + " - " + linea.piloto + " - " + linea.puntos);
            }  // cierra el foreach  -
        }

    }
}






/*Programa para ller archivo de resultado de iracing archivo csv
"Fin Pos","Car ID","Car","Car Class ID","Car Class","Team ID","Cust ID","Name","Start Pos","Car #","Out ID","Out","Interval","Laps Led","Qualify Time","Average Lap Time","Fastest Lap Time","Fast Lap#","Laps Comp","Inc","League Points","Max Fuel Fill%","Weight Penalty (KG)","League Agg Points","AI"
    0         1       2        3            4           5         6        7        8        9        10      11       12        13          14             15  
Me interesa guardar: 
00 "Fin Pos"
07 "Name"

Que quiero que haga el programa:

- Leer el archivo de resultados del evento de iRacing y guardalor en un vector en memoria
- Leer el archivo de pilotos PRO y AM
- Recorrer el vector y ordenar los resultados en PRO y AM
- Otorgar Puntos de la carrera 
- TablaCampeonato.csv  Leer posiciones del torneo y actualizar las posiciones con los nuevos resultados.


Flujo:
1. - leo el archivo de resultado, con esto se cuantos pilotos corrieron.  El vectResultados es fijo.
   - leo el archivo de pro-am para saber quienes son de cada categoria, separa un vector de pilotos para cada categoria
   -  recorro el resultado y busco el piloto.  Guardo la posicion, categoria y piloto. Separo los resultados en Pro y Am
   Otorgar puntos de la carrera por categoria

2. leer TablaCampeonato (listaCampeonato) 


3. recorrer vectResultados y buscar el piloto en la listaCampeonato
   a. si el piloto está sumar los puntos
   b. si el piloto no está en la TablaCampeonato agregarlo

4. Ordenar las tablas segun los puntos Pro-Am


Puntos:   puntaje {0,30,26, 23,21,19,17,15,13,11,12,10, 9, 8, 7, 6, 5, 4, 3, 2, 1}
                      1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 

*/