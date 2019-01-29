using System.Collections.Generic;
using UnityEngine;

public class TreeSpawnScript : MonoBehaviour
{

    int maxTypes;

    public GameObject trees;
    public List<GameObject> zones;

    [System.Serializable]
    public class PlantObject
    {
        public GameObject plant;
        public float scaleFactor;
    }

    public List<PlantObject> objects;

    // Start is called before the first frame update
    void Start()
    {

        maxTypes = objects.Count;

        int counter = 0;

        //Type,PosX,PosY,PosZ,RotX,RotY,RotZ,ScaleX,ScaleY,ScaleZ
        foreach (CSVLoader.CSVNode n in CSVLoader.nodes)
        {
            int treeType = 0;
            Vector3 position = Vector3.zero;

            bool positionValid = false;
            bool rotationValid = false;
            bool scaleValid = false;
            Vector3 rotation = Vector3.zero;
            Vector3 scale = Vector3.zero;

            if (int.TryParse(n.Items[0], out treeType))
            {

                if (treeType < maxTypes)
                {
                    if (float.TryParse(n.Items[1], out position.x))
                        if (float.TryParse(n.Items[2], out position.y))
                            if (float.TryParse(n.Items[3], out position.z))
                                positionValid = true;

                    float fX = Mathf.InverseLerp(0, 150, position.x);
                    float fZ = Mathf.InverseLerp(0, 150, position.z);

                    position.x = Mathf.Lerp(-250f, 250f, fX);
                    position.z = Mathf.Lerp(-250f, 250f, fZ);
                    position.y = 0;

                    if (float.TryParse(n.Items[4], out rotation.x))
                        if (float.TryParse(n.Items[5], out rotation.y))
                            if (float.TryParse(n.Items[6], out rotation.z))
                                rotationValid = true;

                    if (float.TryParse(n.Items[7], out scale.x))
                        if (float.TryParse(n.Items[8], out scale.y))
                            if (float.TryParse(n.Items[9], out scale.z))
                                scaleValid = true;
                }
            }

            if (positionValid && rotationValid && scaleValid)
            {
                GameObject t = objects[treeType].plant;
                scale *= objects[treeType].scaleFactor;

                int zone = CalcZone(position);


                GameObject treeSpawn = Instantiate(t, position, Quaternion.Euler(rotation), zones[zone].transform);
                treeSpawn.transform.localScale = scale;
                treeSpawn.name = "Tree_" + counter.ToString();
                counter++;

    
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int CalcZone(Vector3 position)
    {
        if (position.x < -83f)
        {
            if (position.z < -83f)
            {
                return 0;
            }
            else if (position.z < 83f)
            {
                return 3;
            }
            else
                return 6;
        } else if (position.x < 83f)
        {
            if (position.z < -83f)
            {
                return 1;
            }
            else if (position.z < 83f)
            {
                return 4;
            }
            else
                return 7;
        } else
        {
            if (position.z < -83f)
            {
                return 2;
            }
            else if (position.z < 83f)
            {
                return 5;
            }
            else
                return 8;
        }
    }
}
