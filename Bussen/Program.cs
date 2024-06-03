using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;
using System.Text;

namespace Bussen
{
    class Bus
    {

        public int[] passengers = new int[26]; //plats för 25 bussresenärer, värde 26 behövs för att undvika fel med den sista platsen
        public int passengercount = 0; //agerar räkneverk för hur många sittplatser på bussen som är tagna


        public void Run() //I den här metoden har vi menyn
        {
            while(true) //Evighetsloop
            {
                Console.Clear(); //rensar konsolen.
                Console.WriteLine("Välkommen till bussimulatorn.\n");
                //Menyvalen som användaren ser
                Console.WriteLine("Vad vill du göra?");
                Console.WriteLine("1. Låt en passagerare kliva på.");
                Console.WriteLine("2. Skriv ut passagerarnas ålder.");
                Console.WriteLine("3. Beräkna passagerarnas sammanlagda ålder.");
                Console.WriteLine("4. Räkna ut genomsnittlig ålder.");
                Console.WriteLine("5. Sortera passagerare efter ålder.");
                Console.WriteLine("6. Skriv ut den äldsta passageraren.");
                Console.WriteLine("7. Sök efter passagerare genom ålder.");
                Console.WriteLine("8. Avsluta programmet.");

                
                int choice = 0; //Variabel som lagrar det aktiva menyvalet

                try //Användarens menyval samt felhantering
                {
                    Console.Write("\n\tGör ditt menyval: "); choice = Convert.ToInt32(Console.ReadLine());
                    if (choice < 1 || choice > 8)
                    {
                        Console.WriteLine("Du måste ange en siffra som motsvarar menyvalen.");
                        Console.WriteLine("\nTryck på >>>Valfri Tangent<<< för att fortsätta.");
                        Console.ReadKey();
                    }
                }
                catch
                {
                    Console.WriteLine("Du måste ange en siffra som motsvarar menyvalen.");
                    Console.WriteLine("\nTryck på >>>Valfri Tangent<<< för att fortsätta.");
                    Console.ReadKey();
                }

                Console.WriteLine(); //Blankrad
                
                switch (choice) //De faktiska menyvillkoren
                {
                    case 1:
                        AddPassenger();
                        break;
                    case 2:
                        PrintAge();
                        break;
                    case 3:
                        SumAge();
                        break;
                    case 4:
                        AverageAge();
                        break;
                    case 5:
                        SortBus();
                        break;
                    case 6:
                        MaxAge();
                        break;
                    case 7:
                        FindAge();
                        break;
                    case 8:
                        Quit();
                        break;
                }
            }
        }

        public int Sum() //Anropas av andra metoder
        {
            int sum = 0;
            for (int i = 0; i < passengers.Length; i++)//räknar igenom och summerar vektorn passengers
            {
                sum = sum + passengers[i];
            }
            return sum; //returnerar resultatet till de metoder som har begärt ut det
        }

        public void BubbleSort() //Anropas av andra metoder. Sorterar vektorn i åldersordning.
        {
            int max = passengers.Length - 1; //värdet är antalet fyllda platser minus 1

            //Yttre loop. Vektorns längd.
            for(int i = 0; i < max;  i++)
            {
                //Inre loop. Går igenom varje element i vektorns till alla platser som kan bytas är bytta.
                int nrLeft = max - i;
                for(int j = 0; j < nrLeft; j++)
                {
                    
                    if (passengers[j] < passengers[j+1]) //Jämför element
                    {
                        int temporary = passengers[j]; //Byter plats
                        passengers[j] = passengers[j + 1];
                        passengers[j + 1] = temporary;
                    }
                }
            }
        }

        public void AddPassenger()
        {
            int age = 0; //lagrar passagerarens ålder och används också för undantagshantering i loopen
            bool loop = true; //avgör om loopen ska fortsätta eller avbrytas

            if (passengercount < 25) //om bussen är full får användaren inte lägga till fler passagerare och metoden avslutas
            {
                passengercount++; //lägger till en passagerare i räkningen

                do //iteration för felaktig input(undantagshantering)
                {
                    try
                    {
                        Console.WriteLine("En passagerare stiger på. "); Console.Write("Ange passagerarens ålder: ");
                        age = Convert.ToInt32(Console.ReadLine());
                        loop = false; //korrekt input avbryt loopen

                        if (age < 1 || age > 119)
                        {
                            Console.WriteLine("\nAngiven ålder är orimlig. Försök igen:\n");
                            loop = true; //felaktig input, loopen fortsätter
                        }
                        else
                        {
                            Console.WriteLine("Passageraren har nu stigit på.");
                        }

                    }
                    catch
                    {
                        Console.WriteLine("\nBara siffror får användas. Försök igen:\n");
                    }

                } while (loop);
            }
            else
            {
                Console.WriteLine("Bussen är nu full och inga resenärer kan längre kliva på.");
            }
            int placement = passengercount - 1; //Ser till så att första platsen som fylls på bussen är [0] och inte [1] vilket skulle orsaka problem i några av metoderna
            passengers[placement] = age; //skickar input till array efter att loopen har bedömt input som korrekt, passengercount anger position i array

            Console.WriteLine("\nTryck på >>>Valfri Tangent<<< för att återgå till menyn.");
            Console.ReadKey();
        }

        public void PrintAge()
        {
            Console.WriteLine("Bussen har totalt: " + passengercount + " passagerare.");
            
            if (passengercount >= 1)
            {
                Console.Write("Resenärernas ålder är: | ");
                foreach (int i in passengers) //iteration, skriver ut ålder i array passengers
                {
                    if(i > 0) //ser till så att tomma säten inte skrivs ut
                    {
                        Console.Write(i + " | "); //varje möjlig passagerare skrivs ut
                    }
                }
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Det går inte att skriva ut någon ålder då bussen ej har några passagerare ännu.");
            }

            Console.WriteLine("\nTryck på >>>Valfri Tangent<<< för att återgå till menyn.");
            Console.ReadKey();
            
        }

        public void SumAge() //Summering av ålder. Anropar metoden Sum
        {
            int sum = 0; //ursprungsvärde skrivs bara ut om bussen är tom
            sum = Sum(); //anropar metod Sum och uppdaterar int sum med ett returvärde
            Console.WriteLine("Bussen har nu totalt: " + passengercount + " passagerare.");
            Console.WriteLine("Passagerarnas sammanlagda ålder är: " + sum);

            Console.WriteLine("\nTryck på >>>Valfri Tangent<<< för att återgå till menyn.");
            Console.ReadKey();

        }
        
        public void AverageAge()
        {
            float sum = 0; //flyttal
            sum = Sum(); //får tillbaka en int från metoden Sum men jag typomvandlar den till float genom att lägga returvärdet direkt i variablen sum innan divisionen sker.
            if(passengercount > 0) //undantagshantering, ifall bussen är tom kan man inte dividera med noll, ger ful utskrift
                sum = sum / passengercount;
            Console.WriteLine("Bussen har totalt: " + passengercount + " passagerare.");
            Console.WriteLine("Den genomsnittliga åldern på bussen är " + sum + " år.");
            Console.WriteLine("\nTryck på >>>Valfri Tangent<<< för att återgå till menyn.");
            Console.ReadKey();
        }

        public void SortBus() //Sorterar bussen efter ålder. Anropar andra metoder.
        {
            if(passengercount > 0)
            {
                BubbleSort(); //anropar metod BubbleSort för sortering av vektor
                Console.WriteLine("Passagerarna är nu sorterade från äldst till yngst.\n");
            }

            PrintAge(); //anropar metod PrintAge för att skriva ut nysorterad lista, löser även undantagshantering ifall bussen är tom
        }

        public void MaxAge()
        {
            BubbleSort(); //Anropar metod BubbleSort för att se till så att högst ålder kommer på plats [0] i vektorn
            Console.WriteLine("Den äldsta passageraren på bussen är " + passengers[0] + " år gammal.");
            Console.WriteLine("\nTryck på >>>Valfri Tangent<<< för att återgå till menyn.");
            Console.ReadKey();
        }

        public void FindAge()
        {
            int search = 0; //Åldern vi söker

            try //input och undantagshantering
            {
                Console.Write("Ange åldern du vill söka efter: ");
                search = Convert.ToInt32(Console.ReadLine());

                if (search < 1 || search > 119)
                {
                    Console.WriteLine("\nAngiven ålder är orimlig. Åter till menyn.\n");
                    Console.ReadKey();
                    Run(); //avslutar metoden och återvänder till menyn
                }

            }
            catch
            {
                Console.WriteLine("\nBara siffror får användas. Åter till menyn.\n");
                Console.ReadKey();
                Run(); //avslutar metoden och återvänder till menyn
            }

            if (passengers.Contains(search)) //villkor tar reda på om åldern finns inlagd i vektorn innan for-loop initieras
            {
                Console.WriteLine("Passagerare med ålder " + search + " sitter på: ");

                for (int i = 0; i < passengers.Length; i++) //söker genom array och skriver ut alla platser med angiven ålder
                {
                    int j = i + 1; //ser till så bussens platser skrivs ut från 1-25 istället för 0-24 vilket ser lite snyggare ut för användaren
                    if (passengers[i] == search)
                    {
                        Console.WriteLine("\tPlats: " + j);
                    }
                }
            }
            else //Ingen ålder = ingen sökning
            {
                Console.WriteLine("Det finns inga passagerare som är " + search + " år gamla.");
            }

            Console.WriteLine("\nTryck på >>>Valfri Tangent<<< för att återgå till menyn.");
            Console.ReadKey();
        }
        
        public void Quit() //Avslutar programmet
        { 
            Console.WriteLine("Är du säker på att du vill avsluta programmet?\n");
            Console.WriteLine("Tryck på >>>Enter<<< för att avsluta programmet.");
            Console.WriteLine("Eller tryck på >>>Valfri Tangent<<< för att återgå till menyn.");
            if(Console.ReadKey().Key == ConsoleKey.Enter) //läser in knapptryck av tangent Enter
            {
                Environment.Exit(0); //Avslutar hela programmet
            }
            else
            {
                Run(); //Återgår till menyn
            }         
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //Skapar klassen Bus
            var mybus = new Bus();
            //Anropar metoden Run där menyn finns i klassen Bus.
            mybus.Run();
        }
    }
}
