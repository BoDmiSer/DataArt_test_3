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
        public static string ExtensionFile { get; private set; } = "";
        public static string FileName { get; private set; } = "";

        public static string FullName { get; private set; } = "";

        public static string SearchCurrency { get; private set; }
        public static string Maxlenght { get; private set; }
        public static string ArrayCurrency { get; private set; }
        public static HashSet<string> UniqueWord { get; private set; }
        public static List<String> SearchCurrencyWords { get; private set; }
        public static List<String> ArrayCurencyNew { get; private set; }
        #endregion

        #region Methods
        [STAThread]
        static void Main(string[] args)
        {
            ArrayCurencyNew = new List<string>();
            SearchCurrencyWords = new List<string>();
            ReadFromTxt();
            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
            foreach (var pair in ArrayCurencyNew)
            {
                var splitted = pair.Split(' ');
                if (!graph.ContainsKey(splitted[0]))
                {
                    graph[splitted[0]] = new List<string>();
                }
                graph[splitted[0]].Add(splitted[1]);
            }

            Queue<string> queo = new Queue<string>();
            queo.Enqueue(SearchCurrencyWords[0]);
            HashSet<string> visited = new HashSet<string>();
            Dictionary<string, string> route = new Dictionary<string, string>();

            bool found = false;
            while (queo.Count != 0)
            {
                var item = queo.Dequeue();
                visited.Add(item);
                if (item == SearchCurrencyWords[1])
                {
                    found = true;
                    break;
                }
                else
                {
                    if (graph.ContainsKey(item))
                    {
                        foreach (var child in graph[item])
                        {
                            if (!visited.Contains(child))
                            {
                                route[child] = item;
                                queo.Enqueue(child);
                            }
                        }
                    }
                }
            }

            List<string> resultList = new List<string>();
            string result = "";
            if (found)
            {
                string key = SearchCurrencyWords[1];
                while (route.ContainsKey(key))
                {
                    resultList.Add(key);
                    key = route[key];
                }
                resultList.Add(SearchCurrencyWords[0]);
            }
            resultList.Reverse();
            foreach (var item in resultList)
            {
                result += item + " ";
            }
            SaveToFileTxt(result);
            Console.WriteLine("File Save!");
            Console.ReadLine();
        }

        static void ReadFromTxt()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text documents (.txt)|*.txt";
                while (FullName == "")
                {
                    if (openFileDialog.ShowDialog() == true)
                    {
                        ExtensionFile = Path.GetExtension(openFileDialog.FileName);
                        FullName = Path.GetDirectoryName(openFileDialog.FileName) + "\\"
                                   + Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                        PathFile = Path.GetDirectoryName(openFileDialog.FileName);

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
                                var splitted = line.Split(' ');
                                SearchCurrencyWords.Add(splitted[0]);
                                SearchCurrencyWords.Add(splitted[1]);
                                break;
                            case 2:
                                Maxlenght = line;
                                break;
                            default:
                                ArrayCurencyNew.Add(line);
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
                using (StreamWriter sw = new StreamWriter(PathFile + "\\" + "output" + ".txt", false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(path);
                }
            }
            catch (Exception eSaveInTxt)
            {
                MessageBoxResult message = MessageBox.Show(eSaveInTxt.Message, "Error", MessageBoxButton.OK);

            }

        }
        #endregion
    }
}
