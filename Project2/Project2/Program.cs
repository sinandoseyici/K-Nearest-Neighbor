using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project2
{
    class Program
    {
        static void listeSıralama(List<double[]> uzaklıklar)
        {

            double[] deger = new double[6];
            int diziAdet = uzaklıklar.Count;
            for (int i = 0; i < diziAdet; i++)
            {
                for (int j = 1; j < diziAdet - 1; j++)
                {
                    if (uzaklıklar[(j - 1)][5] > uzaklıklar[j][5])
                    {
                        for (int m = 0; m < 6; m++)
                        {
                            deger[m] = uzaklıklar[j - 1][m];
                        }
                        for (int m = 0; m < 6; m++)
                        {
                            uzaklıklar[j - 1][m] = uzaklıklar[j][m];
                        }
                        for (int m = 0; m < 6; m++)
                        {
                            uzaklıklar[j][m] = deger[m];
                        }


                    }

                }

            }
        }

        static double Knn(int k, double[,] veriSeti, double[] kullanıcıDeğerleri)
        {

            List<double[]> uzaklıklar = new List<double[]>();

            for (int i = 0; i < veriSeti.GetLength(0); i++)  //uzaklıklar arrayList'ine uzaklıkların eklenmesi
            {
                double[] dizi = new double[6];
                for (int y = 0; y < 5; y++)
                {
                    dizi[y] = veriSeti[i, y];
                }
                dizi[5] = uzaklıkHesapla(dizi, kullanıcıDeğerleri);
                uzaklıklar.Add(dizi);

            }

            listeSıralama(uzaklıklar);
            int birSayısı = 0;
            int sıfırSayısı = 0;
            for (int i = 0; i < k; i++)
            {

                if (uzaklıklar[i][4] == 0)
                {
                    sıfırSayısı++;
                }
                else
                {
                    birSayısı++;
                }
                Console.Write(String.Format("{0,10:0.000000}", uzaklıklar[i][0]));
                Console.Write(String.Format("{0,10:0.000000}", uzaklıklar[i][1]));
                Console.Write(String.Format("{0,10:0.000000}", uzaklıklar[i][2]));
                Console.Write(String.Format("{0,10:0.000000}", uzaklıklar[i][3]));
                Console.Write(String.Format("{0,3:0}", uzaklıklar[i][4]));
                Console.Write(String.Format("{0,10:0.000000}", uzaklıklar[i][5]));
                Console.WriteLine();

            }

            double sınıflandırma = 0;
            if (birSayısı < sıfırSayısı)
            {
                sınıflandırma = 0;
            }
            else if (sıfırSayısı < birSayısı)
            {
                sınıflandırma = 1;
            }
            else
            {
                sınıflandırma = uzaklıklar[0][4];
            }

            return sınıflandırma;
            listeSıralama(uzaklıklar);

        }

        static double uzaklıkHesapla(double[] veriSetindekiDeğerler, double[] kullanıcınınDeğerleri) // verisetinden girilen değerler ile kullanıcıdan alan değerler arasındaki uzaklığın hesaplanması
        {
            double uzaklık = 0.0;
            for (int i = 0; i < 4; i++)
            {
                uzaklık += Math.Pow(veriSetindekiDeğerler[i] - kullanıcınınDeğerleri[i], 2);
            }
            return Math.Sqrt(uzaklık);

        }

        static void Main(string[] args)
        {

            Console.WriteLine("Lütfen bir k değeri giriniz:");
            int k = Convert.ToInt32(Console.ReadLine()); // kullanıcıdan k değeri alma 

            double[] kullanıcıDeğerleri = new double[4]; // kullanıcıdan alınacak banknot değerlerini tutmak için oluşturulan liste

            Console.WriteLine("Şimdi de banknotun {varyans,çarpıklık,basıklık,entropi) değerlerini giriniz: ");
            for (int i = 0; i < 4; i++)
            {
                Console.Write((i + 1) + ". kullanıcı değeri: "); // kullanıcıdan diğer banknot değerlerini alma
                kullanıcıDeğerleri[i] = Convert.ToDouble(Console.ReadLine()); // kullanıcıdan alınan banknot değerlerini kullanıcıDeğerleri listesinin her bir elemanı haline getirme

            }

            string[] herBirDeğerSatırı = System.IO.File.ReadAllLines(@"C:\\Users\\Casper\\Desktop\\data_banknote_authentication.txt"); // dosya içerisindeki verileri okuma ve herBirDeğer listesinde satırları tutma

            double[,] veriSeti = new double[herBirDeğerSatırı.Length, 5];

            //Veri setindeki verilerin iki boyutlu veriSeti dizisine değişkenlere göre aktarılması (varyans,basıklık,entropi...)

            for (int i = 0; i < herBirDeğerSatırı.Length; i++)
            {
                int j = 0;
                int indexBoyutu = 0; // Substring methodu kullanırken ,'den sonra gelecek değerin o satırdaki değişkenin kaç haneden olduğunun bulunması için kullanılacak değişken
                int sayac = 0; // Substring methodu kullanırken ,'e kadar alınacak değerlerin her seferinde hangi index'ten başladığını saymak için kullanılacak sayaç
                for (int m = 0; m < herBirDeğerSatırı[i].Length; m++)
                {

                    if (herBirDeğerSatırı[i].Substring(m, 1) == "," || (m + 1) == herBirDeğerSatırı[i].Length) // string manipüle edilmesi, ","'e kadar olan değerlerden parçalama işlemi yapılması
                    {
                        indexBoyutu = m - sayac; //ilk , işaretini görüp index boyutu hesaplaması
                        if ((m + 1) == herBirDeğerSatırı[i].Length)
                        {
                            indexBoyutu = 1;
                        }

                        veriSeti[i, j] = double.Parse(herBirDeğerSatırı[i].Substring(sayac, indexBoyutu).Replace(".", ",")); // .'lı şekilde olduğunda program hata veriyor ve 3.6661 değerini 36661 olarak alıyor bu yüzden yerine , atadım
                        sayac = m + 1; // sayaç her , gördüğünde m+1 değerini almalı ki diğer ,'ü gördüğünde kaçıncı indexten başlayacağını ve ne kadar uzunlukta olacağını bulabilelim
                        j++;

                    }

                }

            }

            double knnDöndü = Knn(k, veriSeti, kullanıcıDeğerleri);
            Console.WriteLine("Knn Değeri: " + knnDöndü);

            List<double[]> kontrolEdilecek200Data = new List<double[]>();//200 veri bunun double[6]
            double[,] kontrolHavuzu = new double[1172, 5];//geri kalan 1172 veriyi tutucak double[6]
            for (int i = 662; i < 762; i++) // veri setindeki tür değerlerinin 0 değerlerinden 1 değerlerine geçerkenki son 100 değerin alınması
            {
                double[] dizi = new double[5];
                for (int y = 0; y < 5; y++)
                {
                    dizi[y] = veriSeti[i, y];

                }
                kontrolEdilecek200Data.Add(dizi);
            }
            for (int i = 1272; i < 1372; i++)  //veri setindeki son 100 değerin alınması
            {
                double[] dizi = new double[5];
                for (int y = 0; y < 5; y++)
                {

                    dizi[y] = veriSeti[i, y];

                }
                kontrolEdilecek200Data.Add(dizi);
            }

            for (int i = 0; i < 662; i++)  // veri setinde alınan 100 değerden sonra kalan değerlerin alınması
            {
                for (int y = 0; y < 5; y++)
                {
                    kontrolHavuzu[i, y] = veriSeti[i, y];
                }

            }

            for (int i = 662; i + 100 < 1272; i++) // veri setinde alınan 100 değerden sonra kalan değerlerin alınması
            {
                for (int y = 0; y < 5; y++)
                {
                    kontrolHavuzu[i, y] = veriSeti[i + 100, y];
                }

            }

            Console.WriteLine("Lütfen bir k değeri giriniz:");
            k = Convert.ToInt32(Console.ReadLine());

            double kontrolEdilecekDataDöndür;
            double başarıSayacı = 0;
            for (int i = 0; i < 200; i++)
            {
                kontrolEdilecekDataDöndür = Knn(k, kontrolHavuzu, kontrolEdilecek200Data[i]);
                Console.WriteLine("Gerçek değer: " + kontrolEdilecek200Data[i][4] + " Tahmin değer: " + kontrolEdilecekDataDöndür);
                if (kontrolEdilecekDataDöndür == kontrolEdilecek200Data[i][4])
                {
                    başarıSayacı++;
                }

            }
            Console.WriteLine("Başarı Yüzdesi: " + (başarıSayacı / 200) * 100);

            
            // Veri setindeki değerlerin ekrana yazdırılması
            Console.WriteLine("Veri setindeki değerlerin ekrana yazdırılması");
            for (int x = 0; x < herBirDeğerSatırı.Length; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    Console.Write(veriSeti[x, y] + " ");
                    if (y == 4)
                    {
                        Console.WriteLine("");
                    }
                }
            }
            
            Console.ReadKey();

        }
    }
}
