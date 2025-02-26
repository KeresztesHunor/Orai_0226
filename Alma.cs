namespace Orai_0226
{
    internal class Alma(string fajta) : Gyumolcs("Alma")
    {
        public string fajta { get; } = fajta;

        public override string ToString() => base.ToString() + ", Fajtája: " + fajta;
    }
}
