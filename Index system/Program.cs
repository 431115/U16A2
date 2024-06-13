using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


class Program
{
    
    static void Main()
    {
        string inputFile = @"Data/book_data.csv";
        string outputFile = @"Data/UPDATED_book_data.csv";

        // Read the input CSV file
        string[] lines = File.ReadAllLines(inputFile);

        // Create a new StringBuilder to store the modified CSV data
        StringBuilder sb = new StringBuilder();

        // Process each line of the input CSV file
        foreach (string line in lines)
        {
            // Split the line into columns
            string[] columns = line.Split(',');

            // Generate the hash number based on all three columns
            string hash = GenerateHash(columns);

            // Append the hash number to the line
            sb.AppendLine($"{line},{hash}");
        }


        // Write the modified CSV data to the output file
        File.WriteAllText(outputFile, sb.ToString());

        Console.WriteLine("CSV file processed successfully!");
    }

    static string GenerateHash(string[] inputs)
    {
        using (MD5 md5 = MD5.Create())
        {
            StringBuilder sb = new StringBuilder();

            // Concatenate all inputs into a single string
            string concatenatedInput = string.Join("", inputs);

            byte[] inputBytes = Encoding.UTF8.GetBytes(concatenatedInput);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the hash bytes to a hexadecimal string
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }

            // Take the first 8 characters as the hash number
            return sb.ToString().Substring(0, 8);
        }
    }
}
