using System.Drawing;

namespace Orai_0226
{
    internal class Program
    {
        public const int maxStackTombMeret = 1 << 10;

        static void Main(string[] args)
        {
            new Program().Run();
            Console.ReadLine();
        }

        void Run()
        {
            GyumolcsFeladat();
            CsereloFeladat();
            SzovegOsszefuzesFeladat();
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
            Console.Write("Add meg a kiválasztott kör színét!");
            SzinesKor szinesKor = new SzinesKor(kivalasztottKor.kozeppont, kivalasztottKor.sugar, SzintBeker());
            Console.WriteLine(szinesKor);
            Console.Write("Adj meg a körnek egy új színt! ");
            szinesKor.szin = SzintBeker();
            Console.WriteLine(szinesKor);
        }

        static Color SzintBeker()
        {
            Color? szin = null;
            while (szin is null)
            {
                Console.Write("Add meg egy ismert szín nevét, vagy kódját! Az ismert színek kilistázásához írj \"?\"-et (ha a szín nem ismert, akkor megadhatod az rgb kódját): ");
                string? s = Console.ReadLine()?.Trim();
                if (s != "?")
                {
                    if (Enum.TryParse(s, true, out KnownColor szinNev) && typeof(KnownColor).IsEnumDefined(szinNev))
                    {
                        szin = Color.FromKnownColor(szinNev);
                    }
                    else
                    {
                        Console.WriteLine("Ez a szín nem ismert! Add meg az rgb kódját!");
                        szin = Color.FromArgb(
                            Beker<byte>("R [0, 255]: "),
                            Beker<byte>("G [0, 255]: "),
                            Beker<byte>("B [0, 255]: ")
                        );
                    }
                }
                else
                {
                    Enum.GetValues<KnownColor>().ForEach(static (KnownColor knownColor) => {
                        Console.WriteLine((int)knownColor + ": " + knownColor);
                    });
                }
            }
            return szin.Value;
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
    }
}
