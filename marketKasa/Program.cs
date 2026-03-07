using System;
using System.Collections.Generic;

public class IntQueue
{
    private int kapasite;
    private int[] queueArray;
    private int bas;
    private int son;
    private int elemansayısı;

    public IntQueue(int size)
    {
        kapasite = size;
        queueArray = new int[kapasite];
        bas = 0;
        son = -1;
        elemansayısı = 0;
    }

    public void Insert(int item)
    {
        if (son == kapasite - 1)
            son = -1;
        queueArray[++son] = item;
        elemansayısı++;
    }

    public int Remove()
    {
        int temp = queueArray[bas++];
        if (bas == kapasite)
            bas = 0;
        elemansayısı--;
        return temp;
    }

    public bool IsEmpty()
    {
        return (elemansayısı == 0);
    }

    public int Size()
    {
        return elemansayısı;
    }
}
public class PQ
{
    private List<int> items; // oncelikli queue tutuması için int turunde items adında bir lis oluşrutdum

    public PQ()
    {
        items = new List<int>();// constructor da yeniden  items ı yazarakan yukarıda alan actıgım seyi başlatıyorum.
    }
    public void Insert(int value)
    {
        if (items.Count == 0)
        {
            items.Add(value);
            return;
        }
        int index = 0;
        while (index < items.Count && items[index] < value)// Mevcut eleman ekleyeceğimiz değerden küçükse ekliyoruz burada priority queue oldugu için 
        {
            index++;
        }
        items.Insert(index, value);
    }
    public int Remove()
    {
        if (IsEmpty())
        {
            Console.WriteLine("boştur");
        }
        int value = items[0];// ilk once en oncelikliyi alıyorum
        items.RemoveAt(0);// siliyorum
        return value;// ve onu donduruyorum.
    }

    public bool IsEmpty()
    {
        return items.Count == 0;// elelman sayısı 0 sa bostur
    }

    public int Size()
    {
        return items.Count;// eleman sayısına bakıyorum
    }

    public void Display()
    {
        Console.Write("PQ listesi budur ");
        for (int i = 0; i < items.Count; i++)// for ile listeyi dolaşıp yazdırıyorum.
        {
            Console.Write(items[i]);
            if (i < items.Count - 1)
                Console.Write(",");// araya virgulle ayırdım
        }
        Console.WriteLine();// duzgun gozukmesi için koydum
    }
}

class MarketSimulation// market simulasyou sınfıımı olusturdum. bunun içinde main ile işlemelrimi gerçekleşitirdim.
{
    static void Main()
    {
        int[] ürünler = { 7, 9, 5, 4, 15, 21, 6, 13, 3, 2 };
        double süre = 2.7; // proje metninde denen gibi 2.7 sn varsaydım.
        IntQueue normalQueue = new IntQueue(ürünler.Length);// uurunler uzunlugunda normal bir sınıf oluşturdum.
        foreach (int ürün in ürünler)// foreach ile uruunleri tek tek dolaşıp ekledim listeye
        {
            normalQueue.Insert(ürün);
        }
        double toplamsure = 0;// ilk başta toplamsureyye ve toplambekelleme suresine 0 dedim.
        double toplambeklemesuresi = 0;
        int musteriSayısı = 0;
        while (!normalQueue.IsEmpty())// liste boş değilken
        {
            musteriSayısı++;
            int urunsayısı = normalQueue.Remove();//bir müşterinin sepetindeki ürün sayısını kuyruktan çıkarıp her  muşteri için urun sayısını buldum.
            double harcananzaman = urunsayısı * süre;
            toplamsure += harcananzaman;

            Console.WriteLine($"{musteriSayısı},,{urunsayısı},,{harcananzaman} sn,,{toplamsure} sn");
            toplambeklemesuresi += toplamsure;
        }

        double ortalamabitirmesuresi = toplambeklemesuresi / musteriSayısı;
        Console.WriteLine($"Ortalama İşlem tamamlanma süresi: {ortalamabitirmesuresi} saniye");
        PQ priorityQueue = new PQ();// sonrasında  priority queue oluşturdum
        foreach (int ürün in ürünler)//foreach ile tek tek bu sefefr priority queueye ekledim.
        {
            priorityQueue.Insert(ürün);
        }
        Console.WriteLine("önceliklediğim queue= ");
        priorityQueue.Display();
      
        priorityQueue = new PQ();
        foreach (int ürün in ürünler)
        {
            priorityQueue.Insert(ürün);
        }
        toplamsure = 0;// bunun içinde aynı  şeyleri yaptım normal queue ile 
        toplambeklemesuresi = 0;// sayaycları ilk 0 ile başlattım
        musteriSayısı = 0;
        while (!priorityQueue.IsEmpty())// priority queue bos değilken 
        {
            musteriSayısı++;
            int urunsayısı = priorityQueue.Remove();
            double harcananzaman = urunsayısı * süre;
            toplamsure += harcananzaman;

            Console.WriteLine($"{musteriSayısı}\t{urunsayısı}\t{harcananzaman} sn\t\t{toplamsure} sn");//tab ile arralarına bosluk koydum
            toplambeklemesuresi += toplamsure;
        }
        double ortalamabitirmesuresiPQ = toplambeklemesuresi / musteriSayısı;
        Console.WriteLine($"Ortalama işlem Tamamlanma Sürem pq de : {ortalamabitirmesuresiPQ} saniye");

        Console.WriteLine($"Normal Kuyruk Ortalaması da bu = {ortalamabitirmesuresi} saniye");
        Console.WriteLine($"Öncelikli Kuyruk Ortalaması = {ortalamabitirmesuresiPQ} saniye");
        Console.WriteLine($"İyileşmesi= {(ortalamabitirmesuresi - ortalamabitirmesuresiPQ)} saniye ve de  yuzde hali = ({((ortalamabitirmesuresi - ortalamabitirmesuresiPQ) / ortalamabitirmesuresi * 100)}%)");
        // burada oncelikli ile nromal ararsındaki verim suresiini gordum.
        List<int> sekizdenaz = new List<int>();// sekizden az urun listesi
        List<int> sekizdenFazla = new List<int>(); // sekizden fazla olanalrı ayrı listeleldim

        foreach (int ürün in ürünler)
        {
            if (ürün > 8)
                sekizdenFazla.Add(ürün);// burada  ekledim.
            else
                sekizdenaz.Add(ürün);
        }
        sekizdenaz.Sort();// sort ile sıraladım
        sekizdenFazla.Sort();
        Console.Write("8'den az siparişler = ");
        for (int i = 0; i < sekizdenaz.Count; i++)
        {
            Console.Write(sekizdenaz[i]);// burada yazdırdım
            if (i < sekizdenaz.Count - 1)
                Console.Write(", ");// burada son elelmana gelene kadar aralarını virgul ile ayırdım.
        }
        Console.WriteLine();// bosluk koydum

        Console.Write("8'den fazla siparişler = ");
        for (int i = 0; i < sekizdenFazla.Count; i++)
        {
            Console.Write(sekizdenFazla[i]);// sekizden fazlayı da yazdırdım virgul ile ayırırıarak
            if (i < sekizdenFazla.Count - 1)
                Console.Write(", ");
        }
        Console.WriteLine();
        List<int> karısıksıra = new List<int>();
        karısıksıra.AddRange(sekizdenaz);//adrange ile butunhepsini  karısık listeye ekledim.
        karısıksıra.AddRange(sekizdenFazla);
        toplamsure = 0;
        toplambeklemesuresi = 0;
        musteriSayısı = 0;
        foreach (int urunsayısı in karısıksıra)
        {
            musteriSayısı++;
            double harcananzaman = urunsayısı * süre;
            toplamsure += harcananzaman;

            Console.WriteLine($"{musteriSayısı}\t{urunsayısı}\t{harcananzaman} sn\t\t{toplamsure} sn");// \t ile aralara bosluk koydum.
            toplambeklemesuresi += toplamsure;
        }
        double ortalama = toplambeklemesuresi / musteriSayısı;
        Console.WriteLine($"Ortalama  tamamlanması Süresi = {ortalama} saniye");

    }
}