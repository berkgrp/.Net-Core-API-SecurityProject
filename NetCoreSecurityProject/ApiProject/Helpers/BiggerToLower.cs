namespace ApiProject.Helpers
{

    public interface IBiggerToLower
    {
        string CharacterReplacementBiggerToLower(string wordToReplace);
    }
    public class BiggerToLower : IBiggerToLower
    {
        public string CharacterReplacementBiggerToLower(string wordToReplace)
        {
            string word = wordToReplace;
            char[] oldValue = new char[] { 'A', 'B', 'C', 'Ç', 'D', 'E', 'F', 'G', 'Ğ', 'H', 'I', 'İ', 'J', 'K', 'L', 'M', 'N', 'O', 'Ö', 'P', 'R', 'S', 'Ş', 'T', 'U', 'Ü', 'V', 'Y', 'Z', 'Q', 'X', 'W' };
            char[] newValue = new char[] { 'a', 'b', 'c', 'c', 'd', 'e', 'f', 'g', 'g', 'H', 'i', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'o', 'p', 'r', 's', 's', 't', 'u', 'u', 'v', 'y', 'z', 'q', 'x', 'w' };
            for (int sayac = 0; sayac < oldValue.Length; sayac++)
            {
                word = word.Replace(oldValue[sayac], newValue[sayac]);
            }
            return word;
        }
    }
}
