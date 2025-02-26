using System.Numerics;

namespace Orai_0226
{
    internal class Kor(Vector2 kozeppont, float sugar)
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
}
