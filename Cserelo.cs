namespace Orai_0226
{
    internal class Cserelo(string szoveg)
    {
        public string szoveg { get; } = szoveg;

        public string Csere(char c)
        {
            Span<char> ujSzoveg = sizeof(char) * szoveg.Length <= Program.maxStackTombMeret ? stackalloc char[szoveg.Length] : new char[szoveg.Length];
            for (int i = 0; i < szoveg.Length; i++)
            {
                ujSzoveg[i] = szoveg[i] == c ? c : '*';
            }
            return new string(ujSzoveg);
        }
    }
}
