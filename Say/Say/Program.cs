int num = 312467;
Console.WriteLine(Say(num));

static string Say(int num)
{
    string numS = num.ToString();
    List<string> chunks = GetChunks(numS);

    string[] placeScales = { "", "thousand", "million", "billion" };
    List<string> words = new();

    for (int i = 0; i < chunks.Count; i++)
    {
        string chunkWord = ThreeDigitToWords(int.Parse(chunks[i]));
        if (!string.IsNullOrEmpty(chunkWord))
        {
            string scale = placeScales[chunks.Count - 1 - i];
            if (!string.IsNullOrEmpty(scale))
                chunkWord += " " + scale;

            words.Add(chunkWord);
        }
    }

    return string.Join(" ", words);
}

static string ThreeDigitToWords(int num)
{
    string[] units = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    string[] teens = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen",
                       "sixteen", "seventeen", "eighteen", "nineteen" };
    string[] tens = { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

    if (num == 0) return "";

    string words = "";

    int hundred = num / 100;
    int remainder = num % 100;

    if (hundred > 0)
        words += units[hundred] + " hundred";

    if (remainder > 0)
    {
        if (words != "") words += " ";
        if (remainder < 10)
            words += units[remainder];
        else if (remainder < 20)
            words += teens[remainder - 10];
        else
        {
            int ten = remainder / 10;
            int unit = remainder % 10;
            words += tens[ten];
            if (unit > 0) words += " " + units[unit];
        }
    }

    return words;
}


static List<string> GetChunks(string num)
{
    List<string> chunks = new();

    int i = num.Length;
    while (i > 0)
    {
        int start = Math.Max(i - 3, 0);
        chunks.Insert(0, num.Substring(start, i - start));
        i -= 3;
    }

    return chunks;
}
