namespace Sim.Migradata;

class Program
{
    
    static async Task Main(string[] args)
    {
        Console.WriteLine("Iniciando Sim-Migradata!");
        while(true)
        {
            Thread.Sleep(1000);
            Console.WriteLine("MigraData V1.0");
            Console.WriteLine("Choose Options!");
            Console.WriteLine("0 Close");
            Console.WriteLine("------------------------");
            Console.WriteLine("1 Create Bindings");
            Console.WriteLine("2 Show Bindings");
            Console.WriteLine("------------------------");

            string input = Console.ReadLine()!;
            int choice = int.Parse(input);

            switch(choice)
            {
                case 0:
                    Console.WriteLine("Encerrando Sim-Migradata!");
                    return;

                case 1:
                    await Functions.Bindings.Starting();
                    break;
                case 2:
                    await Functions.Bindings.DoListAsync();
                    break;

                default:
                    Console.WriteLine("Opção inválida, escolha novamente!");
                    break;
            }            
        }        
    }
}