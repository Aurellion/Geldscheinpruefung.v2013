using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Geldscheinprüfung
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Banknotenprüfung mit Kontrollnummer");
            bool FormatKorrekt;
            string Kontrollnummer = "";
            do
            {
                Console.Write("Kontrollnummer eingeben:");
                Kontrollnummer = Console.ReadLine();

                //Überprüfung des Eingabeformats: 12 Zeichen,
                //zwei Buchstaben und 10 Ziffern - XX0000000000
                FormatKorrekt = true;
                if (Kontrollnummer.Length != 12)
                {
                    Console.WriteLine("Die Kontrollnummer muss aus 12 Zeichen bestehen!");
                    FormatKorrekt = false;
                }

                if (!Char.IsLetter(Kontrollnummer[0]) || !Char.IsLetter(Kontrollnummer[1]))
                {
                    Console.WriteLine("Die Kontrollnummer muss mit zwei Buchstaben beginnen.");
                    FormatKorrekt = false;
                }

                for (int i = 2; i < Kontrollnummer.Length; i++)
                {
                    if (!Char.IsDigit(Kontrollnummer[i]))
                    {
                        FormatKorrekt = false;
                        Console.WriteLine("Die Kontrollnummer muss mit 10 Ziffern enden.");
                        break;
                    }
                }

            } while (!FormatKorrekt);
            Console.WriteLine("Folgende Kontrollnummer wir geprüft: " + Kontrollnummer);

            //Buchstaben in Zahlen umwandeln
            string KontrollnummerZahlen = "";
            for (int i = 0;i< Kontrollnummer.Length-1;i++)
            {
                if (i < 2)
                {
                    KontrollnummerZahlen += Convert.ToString(Convert.ToInt32(Kontrollnummer[i]) - 64);
                }
                else
                {
                   KontrollnummerZahlen += Kontrollnummer[i];                    
                }
            }

            //mathematische Überprüfung

            //Aus den zwei in Schritt 1 ermittelten Zahlen und der 9stelligen
            //Nummer wird eine Quersumme* gebildet

            int Quersumme=0;
            for (int i = 0; i < KontrollnummerZahlen.Length; i++) 
            {
                Quersumme += Convert.ToInt32(KontrollnummerZahlen[i].ToString());
            }

            //Die Quersumme wird durch 9 dividiert und der Rest aus dieser Division ermittelt

            int Rest = Quersumme % 9;

            //Dieser Rest wird nun von 7 abgezogen,
            //das Ergebnis ergibt die Prüfziffer(Anm.: Wenn die Subtraktion 0 ergibt
            //ist die Prüfziffer 9, ergibt die Subtraktion - 1 ist die Prüfziffer 8)

            int Prüfnummer;
            int Differenz = 7 - Rest;
            if (Differenz == 0) Prüfnummer = 9;
            else if (Differenz == -1) Prüfnummer = 8;
            else Prüfnummer = Differenz;

            // Vergleich der Prüfnummer mit der letzten Stelle der Kontrollnummer
            if (Convert.ToInt32(Kontrollnummer[11].ToString()) == Prüfnummer)
            {
                Console.WriteLine("Die Kontrollnummer ist gültig.");
            }
            else
            {
                Console.WriteLine("Die Kontrollnummer ist ungültig.");
            }

            Console.ReadKey();
        }
    }
}
