class Program
{
    static void Main(string[] args)
    {
        var heap = new MaxHeap();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1. Inserir elementos de um arquivo");
            Console.WriteLine("2. Inserir elemento manualmente");
            Console.WriteLine("3. Remover o elemento de maior prioridade");
            Console.WriteLine("4. Imprimir a fila de prioridade");
            Console.WriteLine("5. Alterar a prioridade de um elemento");
            Console.WriteLine("6. Sair");

            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    Console.Write("Digite o nome do arquivo: ");
                    string fileName = Console.ReadLine();
                    InsertFromFile(heap, fileName);
                    break;
                case "2":
                    InsertManually(heap);
                    break;
                case "3":
                    RemoveMax(heap);
                    break;
                case "4":
                    PrintHeap(heap);
                    break;
                case "5":
                    ChangePriority(heap);
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void InsertFromFile(MaxHeap heap, string fileName)
    {
        try
        {
            var lines = File.ReadAllLines(fileName);

            if (lines.Length < 2)
            {
                Console.WriteLine("Arquivo inválido. Deve ter pelo menos 2 linhas.");
                return;
            }

            int totalElements = int.Parse(lines[0].Trim());
            int heapCapacity = int.Parse(lines[1].Trim());

            if (heapCapacity <= 0)
            {
                Console.WriteLine("Capacidade do heap inválida.");
                return;
            }

            for (int i = 2; i < lines.Length; i++)
            {
                var line = lines[i].Trim();

                if (string.IsNullOrEmpty(line))
                {
                    Console.WriteLine($"Linha vazia ignorada na linha {i + 1}.");
                    continue;
                }

                var parts = line.Split(',');

                if (parts.Length != 2)
                {
                    Console.WriteLine($"Linha ignorada (formato incorreto) na linha {i + 1}: {line}");
                    continue;
                }

                try
                {
                    string taskName = parts[0].Trim();
                    int priority = int.Parse(parts[1].Trim());

                    heap.Insert(priority, taskName);

                    if (heap.GetSize() > heapCapacity)
                    {
                        Console.WriteLine($"Capacidade do heap excedida. A fila de prioridade possui {heap.GetSize()} elementos, mas a capacidade é {heapCapacity}.");
                        return;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Erro ao processar a linha: {line}. Formato incorreto da prioridade.");
                }
            }
            Console.WriteLine($"Elementos inseridos com sucesso! Total de elementos inseridos: {heap.GetSize()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao ler o arquivo: {ex.Message}");
        }
    }

    static void InsertManually(MaxHeap heap)
    {
        Console.Write("Digite a prioridade: ");
        int priority = int.Parse(Console.ReadLine());
        Console.Write("Digite o valor: ");
        string value = Console.ReadLine();
        heap.Insert(priority, value);
        Console.WriteLine("Elemento inserido com sucesso!");
    }

    static void RemoveMax(MaxHeap heap)
    {
        try
        {
            var max = heap.ExtractMax();
            Console.WriteLine($"Elemento removido: {max.Item2} com prioridade {max.Item1}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    static void PrintHeap(MaxHeap heap)
    {
        if (heap.IsEmpty())
        {
            Console.WriteLine("A fila está vazia.");
            return;
        }
        Console.WriteLine("Fila de Prioridade (Heap):");
        heap.PrintHeap();
    }

    static void ChangePriority(MaxHeap heap)
    {
        Console.Write("Digite o índice do elemento: ");
        int index = int.Parse(Console.ReadLine());
        Console.Write("Digite a nova prioridade: ");
        int newPriority = int.Parse(Console.ReadLine());
        try
        {
            heap.ChangePriority(index, newPriority);
            Console.WriteLine("Prioridade alterada com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}