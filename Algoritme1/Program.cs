// C# implementation of the approach
using System;

class GFG
{
   // Driver code
    static void Main()
    {
        // n is the number of nodes i.e. V
        int Leverancier = 2;
        int Hubs = 1;
        int Klant = 3;
        int count = 0;
        // Distance matrix: Leverancier, hubs, klanten
        int[,] dist = {
        { 0,100,70,110,80,170 },
        { 100,0,90,130,30,160 },
        { 70,90,0,70,60,130 },
        { 110,130,70,0,100,170},
        { 80,30,60,100,0,150 },
        { 170,160,130,170,150 ,0}
    };

        // Koerier afstand - tot leverancier + hub + klant
        int[] Koerier = { };

        // welke klant bestelt bij leverancier
        int[] L1 = { 1,2,3 };
        int[] L2 = { 1,3 };

        // Boolean array to check if a node
        // has been visited or not
        bool[] v = new bool[Leverancier+Hubs+Klant];
        bool[] h = new bool[Klant];
        int[] list = new int[Leverancier + Hubs + Klant];

        for (int i = 0; i< Klant; i++)
        {
            if(L1.Contains(i+1) && L2.Contains(i+1))
            {
                h[i] = true;
            }
        }
        // kies welke leverancier als eerste te bezoeken
        if (L1.Length >= L2.Length){
            v[0]=true;
            list[count] = 0;
            count++;
        }
        else{
            v[1] = true;
            list[count] = 1;
            count++;
        }

        // Check heeft een van de klanten alle spullen al bij koerier liggen
        for (int i = 0; i < Klant; i++)
        {
            if (L1.Contains(i + 1) == (L1.Length >= L2.Length) && L2.Contains(i + 1) == (L1.Length < L2.Length)) {
                bool c = dist[0,i+Hubs+Leverancier] < dist[0, 1];
                Console.WriteLine(c);
                if (c == true)
                {
                    list[count] = i + Leverancier + Hubs ;
                    count++;
                    v[i + Leverancier + Hubs ] = true;
                }
            }
        }
        if (v[0] == true)
        {
            list[count] = 1; 
            count++;
            v[1] = true;
        }
        else
        {
            list[count] = 0;
            count++;
            v[0] = true;
        }

        // IN DIT GEVAL 1 KOERIER HUB NIET GEBRUIKT
        v[2] = true;

        // route langs overige klanten - NU SUPER SPECIFIEK, WERKT BIJVOORBEELD NIET ALS ER NIET 2 V OP FALSE STAAN
        for (int i = 0; i < Leverancier + Hubs + Klant; i++)
        {
            if (v[i] == false)
            {
                for (int j = 0; j < Leverancier + Hubs + Klant; j++)
                {
                    if(v[j] == false && i!= j){
                        //if (dist[count, i] + dist[list[0],j] < dist[count, j] + dist[list[0], j]) // als tour belangrijk is (terug naar begin)
                        if (dist[count, i] + dist[i, j] < dist[count, j] + dist[j,i])
                        {
                            v[i] = true;
                            list[count] = i;
                            list[count + 1] = j;
                        }
                        else
                        {
                            v[j] = true;
                            list[count] = j;
                            list[count + 1] = i;
                        }
                    }
                }
            }
        }

        // HOE PRINT JE EEN ARRAY?
        for (int i=0; i < list.Length-1; i++) {
            Console.Write(list[i]);
        }
        Console.WriteLine("\n");
        // CHECK TOEVOEGEN ALLES VAN FALSE AF

        // TODO GENERIEKER MAKEN MET n IPV NU VAST VOOR AANTAL LEVERANCIERS BV


        int distance=0;
        for (int i=0; i < list.Length-2; i++)
        {
            distance = distance + dist[list[i], list[i + 1]];
        }
        
        Console.WriteLine(distance);
    }
}

// This code is contributed by mits