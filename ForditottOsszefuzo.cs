namespace Orai_0226
{
    internal class ForditottOsszefuzo(string szoveg1, string szoveg2)
    {
        public string szoveg1 { get; } = szoveg1;
        public string szoveg2 { get; } = szoveg2;

        public string ForditottanOsszefuz()
        {
            int tombMeret = szoveg1.Length + szoveg2.Length;
            Span<char> osszefuzottSzoveg = sizeof(char) * tombMeret <= Program.maxStackTombMeret ? stackalloc char[tombMeret] : new char[tombMeret];
            szoveg1.CopyTo(osszefuzottSzoveg);
            for (int i = 0; i < szoveg2.Length; i++)
            {
                osszefuzottSzoveg[^(i + 1)] = szoveg2[i];
            }
            return new string(osszefuzottSzoveg);
        }
    }

}
