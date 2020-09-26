using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataArt_test_3
{
    class Program
    {
        #region Properties
        public static string PathFile { get; private set; }
        public static string FileName { get; private set; } = "";

        public static string FullName { get; private set; } = "";

        public static string SearchCurrency { get; private set; }
        public static string Maxlenght { get; private set; }
        public static string ArrayCurrency { get; private set; }
        public static HashSet<string> UniqueWord { get; private set; }
        public static Graph g { get; private set; }
        public static List<String> SearchCurrencyWords { get; private set; }
        #endregion

        #region Methods
        [STAThread]
        static void Main(string[] args)
        {
            g = new Graph();
            ReadFromTxt();
            UniqueWord = SearchUnicWords(ArrayCurrency);
            CreateVertex(g, UniqueWord);
            CreateEdge(g, ArrayCurrency);
            SearchCurrencyWords = SearchWords(SearchCurrency);
            var dijkstra = new Dijkstra(g);
            var path = dijkstra.FindShortestPath(SearchCurrencyWords[0], SearchCurrencyWords[1]);
            Console.WriteLine(path);
            SaveToFileTxt(path);
            Console.ReadLine();
        }

        static void CreateVertex(Graph graph, HashSet<string> unique)
        {
            foreach (string item in unique)
            {
                graph.AddVertex(item);
            }
        }

        static void CreateEdge(Graph graph, string stringarray)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            List<String> list = stringarray.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries).ToList();
            for (int i = 0; i < list.Count-1; i++)
            {
                graph.AddEdge(list[i], list[i + 1], 1);
            }
        }

        static void ReadFromTxt()
        {
            try
            {
                while (FullName == "")
                {

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text documents (.txt)|*.txt";
                if (openFileDialog.ShowDialog() == true)
                {
                    PathFile = Path.GetDirectoryName(openFileDialog.FileName);
                    FileName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                    FullName = PathFile + "\\" + FileName;

                }
                }
            }
            catch (Exception eReadFromTxt)
            {
                MessageBoxResult message = MessageBox.Show(eReadFromTxt.Message, "Error", MessageBoxButton.OK);
            }
            try
            {
                using (StreamReader sr = new StreamReader(FullName + ".txt", System.Text.Encoding.Default))
                {
                    string line;
                    int count = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        count++;
                        switch (count)
                        {
                            case 1:
                                SearchCurrency = line;
                                break;
                            case 2:
                                Maxlenght = line;
                                break;
                            default:
                                ArrayCurrency += line + " ";
                                break;
                        }
                    }
                }
            }
            catch (Exception eReadFromTxt)
            {
                MessageBoxResult message = MessageBox.Show(eReadFromTxt.Message, "Error", MessageBoxButton.OK);
            }
        }
        private static void SaveToFileTxt(string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(PathFile+"\\" + "output" + ".txt", false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(path);
                }
            }
            catch (Exception eSaveInTxt)
            {
                MessageBoxResult message = MessageBox.Show(eSaveInTxt.Message, "Error", MessageBoxButton.OK);
               
            }

        }
        static HashSet<String> SearchUnicWords(string stringarray)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            List<String> list = stringarray.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries).ToList();
            HashSet<String> uniqueWords = new HashSet<string>(list);
            return uniqueWords;
        }

        static List<String> SearchWords(string stringarray)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            List<String> list = stringarray.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries).ToList();
            return list;
        }
        #endregion
    }
}
