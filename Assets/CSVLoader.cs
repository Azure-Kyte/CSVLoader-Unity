using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVLoader : MonoBehaviour
{
    public string Filename;
    public string CSVTitles;
    public static List<CSVNode> nodes;

    [System.Serializable]
    public class CSVNodeGroup
    {
        public List<CSVNode> nodes = new List<CSVNode>();
    }

    [System.Serializable]
    public class CSVNode
    {
        public string FullContents;
        public List<string> Items = new List<string>();

        public void Split()
        {
            string[] i = FullContents.Split(',');

            foreach(string it in i)
            {
                Items.Add(it);
            }
        }
    }

    public CSVNodeGroup FirstHundred;

    // Start is called before the first frame update
    void Awake()
    {
        nodes = new List<CSVNode>();

        try
        {
            string[] CSVContents = File.ReadAllLines(System.Environment.CurrentDirectory + @"\" + Filename);

            CSVTitles = CSVContents[0];
            for (int i = 1; i < CSVContents.Length; i++)
            {
                CSVNode n = new CSVNode();
                n.FullContents = CSVContents[i];
                n.Split();

                nodes.Add(n);
            }

            FirstHundred = new CSVNodeGroup();
            for (int i = 0; i < 100; i++)
            {
                FirstHundred.nodes.Add(nodes[i]);
            }
        }
        catch(IOException e)
        {
            Debug.LogError(e.ToString());
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
