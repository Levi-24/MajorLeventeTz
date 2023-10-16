using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace MajorLeventeTz
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Terepjaro> terepjarok = new();
            using StreamReader sr = new StreamReader(
                path: @"..\..\..\src\terepjarok.txt",
                Encoding.UTF8
                );
            while (!sr.EndOfStream)
            {
                terepjarok.Add(new Terepjaro(sr.ReadLine()));
            }

            Console.WriteLine("7.Feladat:");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(terepjarok[i].ToString());
            }

            Console.WriteLine("9.Feladat:");
            Console.WriteLine(Math.Round(ToyotaTomeg(terepjarok),2));

            List<Terepjaro> Osszkerek = new();
            foreach (var terepjaro in terepjarok)
            {
                if (terepjaro.Hajtas == "Összkerékhajtás" && terepjaro.Evjarat > 2019)
                {
                    Osszkerek.Add(terepjaro);
                }
            }
            //Osszkerek.Count();
            Console.WriteLine("10.Feladat:");
            Console.WriteLine(Megfelelo(terepjarok));

            Console.WriteLine("11.Feladat:");
            List<Terepjaro> legkonnyebbek = Legkonnyebb(terepjarok);
            foreach (var terepjaro in legkonnyebbek)
            {
                Console.WriteLine(terepjaro.ToString());
            }

            Console.WriteLine("12.Feladat:");
            List<Terepjaro> regiDiesel = new();
            bool van = Hibrid(terepjarok, ref regiDiesel);
            if (!van)
            {
                Console.WriteLine("Nincs ilyen autó!");
            }
            else
            {
                Console.WriteLine(regiDiesel[0].ToString());
            }

            Console.WriteLine("13.Feladat:");
            List<Terepjaro> OsszkerekHajtas = OsszkerekHajt(terepjarok);
            foreach (var terepjaro in OsszkerekHajtas)
            {
                Console.WriteLine(terepjaro.ToString());
            }

            using StreamWriter writer = new StreamWriter(
                path:@"..\..\..\src\FontKiiras.txt",
                append: false);
            foreach (var terepjaro in terepjarok)
            {
                writer.WriteLine($"{terepjaro.MarkaModell};{terepjaro.Evjarat};{terepjaro.Uzemanyag};{terepjaro.TomegFont};{terepjaro.Tomeg};{terepjaro.Kepesseg};");
            }

            Console.WriteLine("Szorgalmi Feladat:");
            Terepjaro[] konnyuNehez = new Terepjaro[2];
            Szorgalmi(terepjarok, ref konnyuNehez);
            foreach (var item in konnyuNehez)
            {
                Console.WriteLine(item.ToString());
            }
        }

        static double ToyotaTomeg(List<Terepjaro> terepjarok)
        {
            List<Terepjaro> Toyota = new();

            foreach (var terepjaro in terepjarok)
            {
                var MarkaModellOsztva = terepjaro.MarkaModell.Split(' ');
                if (MarkaModellOsztva[0] == "Toyota")
                {
                    Toyota.Add(terepjaro);
                }
            }

            return Toyota.Average(t => t.Tomeg);
        }
        static int Megfelelo(List<Terepjaro> terepjarok)
        {
            int megfelelo = 0;
            foreach (var terepjaro in terepjarok)
            {
                if (terepjaro.Hajtas == "Összkerékhajtás" && terepjaro.Evjarat > 2019)
                {
                    megfelelo++;
                }
            }
            return megfelelo;
        }
        static List<Terepjaro> Legkonnyebb(List<Terepjaro> terepjarok)
        {
            List<Terepjaro> legkonnyebb = new();
            Terepjaro legkonnyebbTerepjaro = terepjarok.OrderBy(t => t.Tomeg).First();
            foreach (var terepjaro in terepjarok)
            {
                if (terepjaro.Tomeg == legkonnyebbTerepjaro.Tomeg)
                {
                    legkonnyebb.Add(terepjaro);
                }
            }
            return legkonnyebb;
        }
        static bool Hibrid(List<Terepjaro> terepjarok, ref List<Terepjaro> regiDiesel)
        {
            List<Terepjaro> Diesel = new();
            bool Hibrid = false;

            foreach (var terepjaro in terepjarok)
            {
                if (terepjaro.Uzemanyag == "Dízel")
                {
                    Diesel.Add(terepjaro);
                }
            }

            Terepjaro legujabbDiesel = Diesel.OrderBy(d => d.Uzemanyag).Last();

            foreach (var terepjaro in terepjarok)
            {
                if (terepjaro.Uzemanyag == "Hibrid" && terepjaro.Evjarat < legujabbDiesel.Evjarat)
                {
                    regiDiesel.Add(terepjaro);
                }
            }
            if (regiDiesel.Count() > 0)
            {
                Hibrid = true;
            }

            return Hibrid;
        }
        static List<Terepjaro> OsszkerekHajt(List<Terepjaro> terepjarok)
        {
            List<Terepjaro> osszkerek = new();
            foreach (var terepjaro in terepjarok)
            {
                if (terepjaro.Hajtas == "Összkerékhajtás")
                {
                    osszkerek.Add(terepjaro);
                }
            }
            return osszkerek;
        }
        static void Szorgalmi(List<Terepjaro> terepjarok, ref Terepjaro[] konnyuNehez)
        {
            var legkonnyebb = terepjarok.OrderBy(t => t.Tomeg).First();
            var legnehezebb = terepjarok.OrderBy(t => t.Tomeg).Last();
            konnyuNehez[0] = legkonnyebb;
            konnyuNehez[1] = legnehezebb;
        }
    }
}
