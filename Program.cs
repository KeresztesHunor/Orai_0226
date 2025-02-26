using System.Drawing;
using System.Numerics;

namespace Orai_0226
{
    internal class Program
    {
        const int maxStackTombMeret = 1 << 10;

        static void Main(string[] args)
        {
            new Program().Run();
            Console.ReadLine();
        }

        void Run()
        {
            //GyumolcsFeladat();
            //CsereloFeladat();
            //SzovegOsszefuzesFeladat();
            KorFeladat();
        }

        void GyumolcsFeladat()
        {
            Console.WriteLine(new Gyumolcs("Narancs"));
            Console.WriteLine(new Alma("Idared"));
        }

        void CsereloFeladat()
        {
            bool kilep = false;
            List<string> tesztek = [];
            while (!kilep)
            {
                Console.Write("Adj meg egy szöveget (ha nem akarsz több szöveget megadni, írd azt, hogy \"igen\"): ");
                string szoveg = Console.ReadLine() ?? "";
                if (szoveg != "igen")
                {
                    tesztek.Add(new Cserelo(szoveg).Csere(Beker<char>("Adj meg egy betűt: ")));
                }
                else
                {
                    kilep = true;
                }
            }
            tesztek.ForEach(Console.WriteLine);
        }

        void SzovegOsszefuzesFeladat()
        {
            Console.WriteLine(new ForditottOsszefuzo("Macska", "Cica").ForditottanOsszefuz());
        }

        void KorFeladat()
        {
            Kor[] korok = new Kor[FeltetellelBeker("Add meg hány kört akarsz megadni: ", "Csak 0-nál nagyobb számot adhatsz meg.", static (int i) => i > 0)];
            for (int i = 0; i < korok.Length; i++)
            {
                korok[i] = new Kor(
                    Beker<float>("Add meg a kör középpontjának az x koordinátáját: "),
                    Beker<float>("Add meg a kör középpontjának az y koordinátáját: "),
                    FeltetellelBeker("Add meg a kör sugarát: ", "A körnek nem lehet negatív sugara.", static (float f) => f >= 0)
                );
            }
            korok.ForEach(static (Kor kor, int index) => {
                Console.WriteLine(index + ": " + kor);
            });
            Kor kivalasztottKor = korok[FeltetellelBeker("Válassz ki egy kör indexét: ", $"Csak a [0, {korok.Length - 1}] intervallumból választhatsz.", (int i) => i >= 0 && i < korok.Length)];
            SzinesKor szinesKor = new SzinesKor(kivalasztottKor.kozeppont, kivalasztottKor.sugar, SzintBeker());
            Console.WriteLine(szinesKor);
            Console.Write("Adj meg a körnek egy új színt! ");
            szinesKor.szin = SzintBeker();
            Console.WriteLine(szinesKor);
        }

        static Color SzintBeker()
        {
            Color szin = default;
            bool helyesErtek = false;
            while (!helyesErtek)
            {
                Console.Write("Add meg egy ismert szín nevét, vagy kódját! Az ismert színek kilistázásához írj \"?\"-et (ha a szín nem ismert, akkor megadhatod az rgb kódját): ");
                string? szinNev = Console.ReadLine();
                if (szinNev != "?")
                {
                    if (Enum.TryParse(szinNev, true, out KnownColor result))
                    {
                        szin = Color.FromKnownColor(result);
                    }
                    else if (int.TryParse(szinNev, out int szinKod))
                    {
                        szin = Color.FromKnownColor((KnownColor)szinKod);
                    }
                    else
                    {
                        Console.WriteLine("Ez a szín nem ismert! Add meg az rgb kódját!");
                        szin = Color.FromArgb(
                            Beker<byte>("R (piros) [0, 255]: "),
                            Beker<byte>("G (zöld) [0, 255]: "),
                            Beker<byte>("B (kék) [0, 255]: ")
                        );
                    }
                    helyesErtek = true;
                }
                else
                {
                    Enum.GetValues<KnownColor>().ForEach(static (KnownColor knownColor) => {
                        Console.WriteLine(knownColor + ": " + (int)knownColor);
                    });
                }
            }
            return szin;
        }

        static T Beker<T>(string uzenet) where T : struct, IParsable<T>
        {
            T ertek = default;
            bool helyesErtek = false;
            while (!helyesErtek)
            {
                Console.Write(uzenet);
                if (T.TryParse(Console.ReadLine(), null, out T value))
                {
                    ertek = value;
                    helyesErtek = true;
                }
                else
                {
                    Console.WriteLine($"Hiba! Csak {typeof(T).Name} típusú adatot lehet megadni.");
                }
            }
            return ertek;
        }

        static T FeltetellelBeker<T>(string uzenet, string hibaUzenet, Func<T, bool> feltetel) where T : struct, IParsable<T>
        {
            T ertek = default;
            bool helyesErtek = false;
            while (!helyesErtek)
            {
                T esetlegesErtek = Beker<T>(uzenet);
                if (feltetel(esetlegesErtek))
                {
                    ertek = esetlegesErtek;
                    helyesErtek = true;
                }
                else
                {
                    Console.WriteLine("Hiba! " + hibaUzenet);
                }
            }
            return ertek;
        }

        class Cserelo(string szoveg)
        {
            public string szoveg { get; } = szoveg;

            public string Csere(char c)
            {
                Span<char> ujSzoveg = sizeof(char) * szoveg.Length <= maxStackTombMeret ? stackalloc char[szoveg.Length] : new char[szoveg.Length];
                for (int i = 0; i < szoveg.Length; i++)
                {
                    ujSzoveg[i] = szoveg[i] == c ? c : '*';
                }
                return new string(ujSzoveg);
            }
        }

        class Gyumolcs(string nev)
        {
            public string nev { get; } = nev;

            public override string ToString() => nev;
        }

        class Alma(string fajta) : Gyumolcs("Alma")
        {
            public string fajta { get; } = fajta;

            public override string ToString() => base.ToString() + ", Fajtája: " + fajta;
        }

        class ForditottOsszefuzo(string szoveg1, string szoveg2)
        {
            public string szoveg1 { get; } = szoveg1;
            public string szoveg2 { get; } = szoveg2;

            public string ForditottanOsszefuz()
            {
                int tombMeret = szoveg1.Length + szoveg2.Length;
                Span<char> osszefuzottSzoveg = sizeof(char) * tombMeret <= maxStackTombMeret ? stackalloc char[tombMeret] : new char[tombMeret];
                szoveg1.CopyTo(osszefuzottSzoveg);
                for (int i = 0; i < szoveg2.Length; i++)
                {
                    osszefuzottSzoveg[^(i + 1)] = szoveg2[i];
                }
                return new string(osszefuzottSzoveg);
            }
        }

        class Kor(Vector2 kozeppont, float sugar)
        {
            public Vector2 kozeppont { get; set; } = kozeppont;
            public float sugar { get; set; } = sugar >= 0 ? sugar : throw new ArgumentException("A körnek nem lehet negatív hosszúságú sugara.");

            public float Kerulet => sugar * 2 * MathF.PI;
            public float Terulet => sugar * sugar * MathF.PI;

            public Kor(float x, float y, float sugar) : this(new Vector2(x, y), sugar)
            {

            }

            public Kor(float sugar) : this(Vector2.Zero, sugar)
            {

            }

            public override string ToString() => $"Középpont: {kozeppont}, Sugár: {sugar}, Kerület: {Kerulet}, Terület: {Terulet}";
        }

        class SzinesKor(Vector2 kozeppont, float sugar, Color szin) : Kor(kozeppont, sugar)
        {
            public Color szin { get; set; } = szin;

            public SzinesKor(Vector2 kozeppont, float sugar) : this(kozeppont, sugar, Color.Black)
            {
                
            }

            public SzinesKor(float sugar, Color szin) : this(Vector2.Zero, sugar, szin)
            {

            }

            public SzinesKor(float sugar) : this(sugar, Color.Black)
            {

            }

            public override string ToString() => base.ToString() + ", Szín: " + szin.ToString().Substring(7, szin.ToString().Length - 8);
        }
    }
}
