namespace Shared
{
    public class Files
    {
        public static string[] ReadPerLine(string path)
        {
            StreamReader reader = new StreamReader(path);

            string[] items = reader
                .ReadToEnd()
                .Split(Environment.NewLine)
                .ToArray();

            reader.Close();

            return items;
        }


    }
}
