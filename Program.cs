using System;
using System.Collections.Generic;

public class Grap
{
    private Dictionary<char, Dictionary<char, int>> düğümler = new Dictionary<char, Dictionary<char, int>>();

    public void DüğümEkle(char isim, Dictionary<char, int> kenarlar)
    {
        düğümler[isim] = kenarlar; //grafa yeni bir node ekleme. isimin altında kenarların ağırlıklarını tutar.
    }

    public List<char> EnKısaYol(char başlangıç, char bitiş) //başlangıç ve bitişi tutar
    {
        var önceki = new Dictionary<char, char>();   // /önceki düğümleri tutar
        var mesafeler = new Dictionary<char, int>();  // başlangıçtan itibaren tutar
        var kuyruk = new Queue<char>();              // henüz üğranmayan düğümleri tutar kuyrukta
        List<char> yol = null;                       // en kısa yolun tutulacağı liste


        foreach (var düğüm in düğümler)//her düğümün mesafesi sonsuz olarak ayarlanır başlangıcı 0dır
            //düğümleri düğüm değişkenine atama
        {
            mesafeler[düğüm.Key] = düğüm.Key == başlangıç ? 0 : int.MaxValue; //mesaferlere düğüm.keyi anahtar olarak atama
            //başlangıç değeri 0 sonraki değerlere sosuz maliyetle ulaşma
            kuyruk.Enqueue(düğüm.Key);
        }

        // kuyruk boşalana kadar işlem yap
        while (kuyruk.Count != 0)
        {
            var enKüçük = kuyruk.Dequeue(); // kuyruktan en küçük mesafeye sahip olanı çıkarır.
            //grap maliyete göre bulmada bulunan nodeı alırız sonra çıkarırız. Kaldırılmazsa sonsuz döngü problemi olabilir.

            // en küçük düğüm hedefse program tamamlanır
            if (enKüçük == bitiş)
            {
                yol = new List<char>();
                while (önceki.ContainsKey(enKüçük))//öncekinde en küçük varsa contains anahtarın daha önce girilip girilmediğini sorgular.
                {
                    yol.Add(enKüçük);
                    //önceki düğümleri kullanarak yeni yol oluşur
                    enKüçük = önceki[enKüçük];
                }
                break;
            }

            // //düğüm mesafesi sonssuzsa devam eder
            if (mesafeler[enKüçük] == int.MaxValue)
            {
                break;
            }

            // komşu düğümleri kontrol eder kısa yol varsa günceller
            foreach (var komşu in düğümler[enKüçük])
            {
                var alternatif = mesafeler[enKüçük] + komşu.Value;
                if (alternatif < mesafeler[komşu.Key])
                {
                    mesafeler[komşu.Key] = alternatif;
                    önceki[komşu.Key] = enKüçük;
                    kuyruk.Enqueue(komşu.Key); //kuyruğa yeni eleman ekleme enqueue
                }
            }
        }

        return yol; // En kısa yol listesini döndür
    }
}

public class AnaProgram
{
    public static void Main()
    {
        Grap g = new Grap();
        g.DüğümEkle('A', new Dictionary<char, int>() { { 'B', 4 }, { 'C', 2 } });
        g.DüğümEkle('B', new Dictionary<char, int>() { { 'C', 5 }, { 'D', 10 } });
        g.DüğümEkle('C', new Dictionary<char, int>() { { 'D', 3 } });
        g.DüğümEkle('D', new Dictionary<char, int>() { { 'E', 7 } });
        g.DüğümEkle('E', new Dictionary<char, int>() { { 'F', 2 } });
        g.DüğümEkle('F', new Dictionary<char, int>() { { 'D', 1 } });

        List<char> enKısaYol = g.EnKısaYol('A', 'D');
        if (enKısaYol != null)
        {
            enKısaYol.Reverse(); // Yolu baştan sona doğru yazdırmak için ters çevir
            Console.WriteLine("En kısa yol: " + string.Join(" -", enKısaYol)); //listeyi string join ile  birleiştiriz + operatörü yerine
        }
        else
        {
            Console.WriteLine("İki node arası yol yok"); 
        }
    }
}
