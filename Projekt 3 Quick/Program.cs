using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.IO;


namespace Projekt_3_Quick
{

    class Program
    {
        static int[] random = new int[80000];
        static int[] random2 = new int[80000];
        static int[] rosnaca = new int[80000];
        static int[] malejaca = new int[80000];
        static int[] aksztalt = new int[80000];
        static Stopwatch czas;
        static double[] pomiar = new double[1000];
        static int time;
        static int max = 79999;
        static int min = 1;



        static void Gener()
        {
            int wylosowana;
            Random los;
            los = new Random();
            for (int i = 0; i < random.Length; i++)
            {
                wylosowana = los.Next(1, 80000);
                random[i] = wylosowana;
                random2[i] = random[i];
            }
            Array.Sort(random2);
            for (int i = 0; i < rosnaca.Length; i++)
            {
                rosnaca[i] = random2[i];
            }
            Array.Reverse(random2);
            for (int i = 0; i < malejaca.Length; i++)
            {
                malejaca[i] = random2[i];
            }
            for (int i = 0; i < aksztalt.Length; i++)
            {
                if (i <= aksztalt.Length / 2)
                    aksztalt[i] = rosnaca[i];
                else
                    aksztalt[i] = malejaca[i];
            }
        }
        static void qsorti(int[] t)
        {

            int i, j, l, p, sp;
            int[] stos_l = new int[t.Length],
            stos_p = new int[t.Length]; // przechowywanie żądań podziału
            sp = 0; stos_l[sp] = 0; stos_p[sp] = t.Length - 1; // rozpoczynamy od całej tablicy
            time = 0;
            czas = Stopwatch.StartNew();
            do
            {
                l = stos_l[sp]; p = stos_p[sp]; sp--; // pobieramy żądanie podziału
                do
                {
                    int x;
                    i = l; j = p; x = t[(l + p) / 2]; // analogicznie do wersji rekurencyjnej
                    do
                    {
                        while (t[i] < x) i++;
                        while (x < t[j]) j--;
                        if (i <= j)
                        {
                            int buf = t[i]; t[i] = t[j]; t[j] = buf;
                            i++; j--;
                        }
                    } while (i <= j);

                    if (i < p) { sp++; stos_l[sp] = i; stos_p[sp] = p; } // ewentualnie dodajemy żądanie podziału
                    p = j;
                    if (sp >= 0)
                    {
                        if (time < 15)
                        {
                            czas.Stop();
                            pomiar[time] = czas.Elapsed.TotalMilliseconds;
                            czas = Stopwatch.StartNew();
                            time++;
                        }

                    }
                } while (l < p);
            } while (sp >= 0); // dopóki stos żądań nie będzie pusty
            czas.Stop();
        } /* qsort() */
        static void qsortr(int[] t, int l, int p)
        {
            int i, j, x;
            i = l;
            j = p;
            x = t[(l + p) / 2]; // (pseudo)mediana
            time = 0;
            czas = Stopwatch.StartNew();
            do
            {
                while (t[i] < x)
                {
                    if (i == 10000 || i == 5000 || i == 15000 || i == 20000 || i == 25000 || i == 30000 || i == 35000 || i == 2500 || i == 7500 || i == 12500 || i == 17500 || i == 22500 || i == 27500 || i == 32500 || i == 37500)
                    {

                        czas.Stop();
                        pomiar[time] = czas.Elapsed.TotalMilliseconds;
                        czas = Stopwatch.StartNew();
                        time++;


                    }
                    i++;
                } // przesuwamy indeksy z lewej
                while (x < t[j])
                {
                    if (j == 40001 || j == 45000 || j == 50000 || j == 55000 || j == 60000 || j == 65000 || j == 70000 || j == 75000)
                    {

                        czas.Stop();
                        pomiar[time] = czas.Elapsed.TotalMilliseconds;
                        czas = Stopwatch.StartNew();
                        time++;


                    }
                    j--;
                }
                // przesuwamy indeksy z prawej
                if (i <= j) // jeśli nie minęliśmy się indeksami (koniec kroku)
                { // zamieniamy elementy
                    int buf = t[i]; t[i] = t[j]; t[j] = buf;
                    if (i == 10000 || i == 5000 || i == 15000 || i == 20000 || i == 25000 || i == 30000 || i == 35000 || j == 40001 || j == 45000 || j == 50000 || j == 55000 || j == 60000 || j == 65000 || j == 70000 || j == 75000)
                    {

                        czas.Stop();
                        pomiar[time] = czas.Elapsed.TotalMilliseconds;
                        czas = Stopwatch.StartNew();
                        time++;


                    }
                    i++; j--;
                }


            }
            while (i <= j);
            if (l < j) qsortr(t, l, j); // sortujemy lewą część (jeśli jest)
            if (i < p) qsortr(t, i, p); // sortujemy prawą część (jeśli jest)
            czas.Stop();

        } /* qsort() */
        static void Main(string[] args)
        {
            Gener();
            qsorti(random);
            Console.ForegroundColor = (ConsoleColor.Red);
            Console.WriteLine("Qsorti");
            Console.WriteLine();
            Console.WriteLine("random");
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine(i + 1 + " " + pomiar[i]);
            }
            Console.WriteLine();

            qsorti(aksztalt);
            Console.WriteLine("aksztalt");
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine(i + 1 + " " + pomiar[i]);
            }
            Gener();

            qsortr(random, min, max);
            Console.ForegroundColor = (ConsoleColor.Blue);
            Console.WriteLine();
            Console.WriteLine("Qsortr");
            Console.WriteLine();
            Console.WriteLine("random");
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine(i + 1 + " " + pomiar[i]);
            }
            Console.WriteLine();

            qsortr(aksztalt, min, max);
            Console.WriteLine("aksztalt");
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine(i + 1 + " " + pomiar[i]);
            }
            Console.ForegroundColor = (ConsoleColor.White);

            Console.WriteLine("Czy chcesz zapisać pomiary do pliku? Y/N");
            string z = Console.ReadLine();
            if (z == "Y")
            {
                string path = @"C:\Nowy Folder\P3Q.txt";
                StreamWriter plik = new StreamWriter(path);
                Gener();
                qsorti(random);
                plik.WriteLine("Qsorti");
                plik.WriteLine();
                plik.WriteLine("random");
                for (int i = 0; i < 15; i++)
                {
                    plik.WriteLine(i + 1 + ";" + pomiar[i]);
                }
                plik.WriteLine();

                qsorti(aksztalt);
                plik.WriteLine("aksztalt");
                for (int i = 0; i < 15; i++)
                {
                    plik.WriteLine(i + 1 + ";" + pomiar[i]);
                }
                Gener();

                qsortr(random, min, max);
                plik.WriteLine();
                plik.WriteLine("Qsortr");
                plik.WriteLine();
                plik.WriteLine("random");
                for (int i = 0; i < 15; i++)
                {
                    plik.WriteLine(i + 1 + ";" + pomiar[i]);
                }
                plik.WriteLine();

                qsortr(aksztalt, min, max);
                plik.WriteLine("aksztalt");
                for (int i = 0; i < 15; i++)
                {
                    plik.WriteLine(i + 1 + ";" + pomiar[i]);
                }
                plik.Close();
            }
        }
    }
}
