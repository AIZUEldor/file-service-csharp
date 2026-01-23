using System;
using System.Threading;
using System.Threading.Tasks;
using FileService;

namespace FileService
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://tarteeb-api-prod.azurewebsites.net/api/home/no-auth";

            IFileService fileService = new FileService();
            var cts = new CancellationTokenSource();

            // Thread #1: background heartbeat start
            var heartbeat = new ApiHeartbeat(fileService, url);
            _ = Task.Run(() => heartbeat.RunAsync(cts.Token));

            // Thread #2: menu (main)
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MENU ===");
                Console.WriteLine("1) Filelar ro'yxati");
                Console.WriteLine("2) File o‘qish (tanlab)");
                Console.WriteLine("0) Chiqish");
                Console.Write("Tanlang: ");
                string choice = Console.ReadLine();

                if (choice == "0")
                {
                    cts.Cancel(); // heartbeat to‘xtaydi
                    break;
                }

                switch (choice)
                {
                    case "1":
                        await ShowFiles(fileService);
                        break;

                    case "2":
                        await ReadSelectedFile(fileService);
                        break;

                    default:
                        Console.WriteLine("Noto'g'ri tanlov!");
                        Console.WriteLine("Davom etish uchun tugma bosing...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static async Task ShowFiles(IFileService service)
        {
            Console.Clear();
            var files = await service.GetAllFileNamesAsync();

            if (files.Count == 0)
            {
                Console.WriteLine("Hozircha fayl yo‘q.");
            }
            else
            {
                for (int i = 0; i < files.Count; i++)
                    Console.WriteLine($"{i + 1}) {files[i]}");
            }

            Console.WriteLine("\nMenyuga qaytish uchun tugma bosing...");
            Console.ReadKey();
        }

        private static async Task ReadSelectedFile(IFileService service)
        {
            Console.Clear();
            var files = await service.GetAllFileNamesAsync();

            if (files.Count == 0)
            {
                Console.WriteLine("Hozircha fayl yo‘q.");
                Console.WriteLine("Tugma bosing...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < files.Count; i++)
                Console.WriteLine($"{i + 1}) {files[i]}");

            Console.Write("\nQaysi fayl? (raqam): ");
            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > files.Count)
            {
                Console.WriteLine("Noto‘g‘ri raqam!");
                Console.ReadKey();
                return;
            }

            string selectedDisplay = files[idx - 1];     // "M: 2026-01-23.txt"
            string fileName = selectedDisplay.Replace("M:", "").Trim();

            var content = await service.ReadFileAsync(fileName);

            Console.Clear();
            Console.WriteLine($"=== {fileName} ===");
            if (content == null) Console.WriteLine("File topilmadi.");
            else Console.WriteLine(content);

            Console.WriteLine("\nMenyuga qaytish uchun tugma bosing...");
            Console.ReadKey();
        }
    }
}
