using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{

    class Program
    {
        static double[,] Nokta(Random random,int n, int height, int widht) // rastgele oluşturulan sayıların iki boyutlu listede değerlerinin atanması
        {
            
            double[,] noktalar = new double[height, widht];
            for (int i = 0; i < n; i++)
            {
                
                noktalar[i, 0] = random.NextDouble()*widht; //x koordinatı
                noktalar[i, 1] = random.NextDouble()*height; //y koordinatı
                Console.WriteLine("Nokta: " + noktalar[i, 0] + "     ,     " + noktalar[i, 1]);

            }
            return noktalar;

        }

        static double[,] DistanceMatrix(int n,double[,] noktalar) // uzaklık matrisi oluşturulması
        {
            double[,] uzaklıkMatrisi = new double[n, n];
            double uzaklık =0.0;

            for(int i=0; i<n; i++)
            {
                for (int j = 0; j < n; j++)
                {   // x'ler ve y'ler farkının kareler toplamının karekökünün alınması
                    uzaklık = Math.Sqrt(Math.Pow(noktalar[i, 0] - noktalar[j, 0], 2) + Math.Pow(noktalar[i, 1] - noktalar[j, 1], 2)); 
                    // bulunan değerlerin çift boyutlu uzaklıkMatrisine atanması
                    uzaklıkMatrisi[i, j] = uzaklık;
                    Console.Write(String.Format("{0,10:0.0}", uzaklıkMatrisi[i, j]));
                    

                }
                Console.WriteLine();
            }
            return uzaklıkMatrisi ;
        }
        static void Main(string[] args)
        {
            Random random = new Random();
            Console.WriteLine("      X koordinatı                     Y koordinatı");
            Console.WriteLine("\n--------------n=10 için olan değerler--------------");
            double[,] noktalar1 = Nokta(random,10, 100, 100); 
            Console.WriteLine("\n--------------n=100 için olan değerler-------------");
            double[,] noktalar2 = Nokta(random,100, 100, 100);
            Console.WriteLine("Distance Matrix yazdırılıyor...");
            double[,] uzaklıklar1 = DistanceMatrix(20,noktalar2);


            Console.ReadKey();
        }
        
    }
}
