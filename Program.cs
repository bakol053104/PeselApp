using PeselApp;

MainUserInterface();

void MainUserInterface()
{
    var endProgramm = false;
    while (!endProgramm)
    {
        Console.Clear();
        Console.WriteLine($"\n\n\t\t\t   Witamy w programie sprawdzenia poprawności nr PESEL");
        Console.WriteLine($"\t=======================================================================================\n");
        Console.Write($"\tPodaj nr PESEL: ");
        var inputPesel = Console.ReadLine();
        Console.Write($"\tPodaj czy pesel należy do kobiety czy mężczyzny K[k]/M[m]  Q/q- Wyjście: ");
        var inputSex = Console.ReadLine();

        switch (inputSex)
        {
            case "Q":
            case "q":
                endProgramm = true;
                break;
            default:
                try
                {
                    var pesel = new Pesel(inputPesel, inputSex);
                    if (pesel.IsValid())
                    {
                        Console.WriteLine($"\n\tPodany nr PESEL jest prawidłowy");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"\n\t{exception.Message}");
                }
                finally
                {
                    WaitForKeyPress();
                }
                break;
        }
    }

    void WaitForKeyPress()
    {
        Console.Write($"\n\tNaciśnij dowolny klawisz ");
        Console.ReadKey();
    }
}