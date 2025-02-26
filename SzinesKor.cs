using System.Drawing;
using System.Numerics;

namespace Orai_0226
{
    internal class SzinesKor(Vector2 kozeppont, float sugar, Color szin) : Kor(kozeppont, sugar)
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
