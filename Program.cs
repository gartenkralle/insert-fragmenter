class Program
{
    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines(args[0]);

        using (StreamWriter writer = new(args[1]))
        {
            string[] columns = lines[0].Split(',');

            for (int i = 0; i < columns.Length; i++)
            {
                columns[i] = $"[{columns[i][1..^1]}]";
            }

            lines[0] = string.Join(',', columns);

            for (int i = 1; i < lines.Length; i++)
            {
                if ((i - 1) % 1000 == 0)
                {
                    writer.WriteLine($"INSERT INTO {args[2]} ({lines[0]})");
                    writer.WriteLine("VALUES");
                }

                if (i % 1000 == 0 || i == lines.Length - 1)
                {
                    writer.WriteLine($"({lines[i]});");
                    writer.WriteLine("GO");
                }
                else
                {
                    writer.WriteLine($"({lines[i]}),");
                }
            }
        }

        Console.WriteLine("File updated successfully.");
    }
}
