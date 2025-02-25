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
            CsereloFeladat();
            SzovegOsszefuzesFeladat();
        }

        void CsereloFeladat()
        {
            Console.WriteLine(new Cserelo("Macska").Csere('a'));
        }

        void SzovegOsszefuzesFeladat()
        {
            Console.WriteLine(new ForditottOsszefuzo("Macska", "Cica").ForditottanOsszefuz());
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
        }

        class SzinesKor(Vector2 kozeppont, float sugar, Color szin) : Kor(kozeppont, sugar)
        {
            Color szin { get; set; } = szin;

            public SzinesKor(Vector2 kozeppont, float sugar) : this(kozeppont, sugar, Color.Black)
            {
                
            }

            public SzinesKor(float sugar, Color szin) : this(Vector2.Zero, sugar, szin)
            {

            }

            public SzinesKor(float sugar) : this(sugar, Color.Black)
            {

            }
        }
    }
}
