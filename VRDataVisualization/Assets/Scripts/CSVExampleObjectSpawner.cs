using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using LINQtoCSV;

public class CSVExampleObjectSpawner : MonoBehaviour {

	private Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

	[Tooltip("The file that contains the information on the atoms to be spawned.")]
	public TextAsset inputFile;

    [Tooltip("The parent object that will hold all of the atoms as children. It is recommended that AtomHolder be a child of Atom Spawner")]
	public GameObject AtomHolder;

	// Use this for initialization
	void Awake()
	{
		// Places each child object of the object spawner game object
		// into a dictionary with the name as the key.
		foreach (Transform child in this.gameObject.transform)
		{
			prefabs[child.name] = child.gameObject;
		}
	}

	void Start()
	{
        // Set Up CSV Input File
		CsvFileDescription inputFileDescription = new CsvFileDescription 
		{
			SeparatorChar = ',',
			FirstLineHasColumnNames = true
		};

        /*using(var ms = new MemoryStream())
        using (var txtWriter = new StreamWriter(ms))
        using (var txtReader = new StreamReader(ms))
        {
            txtWriter.Write(inputFile.text);
            ms.Seek(0, SeekOrigin.Begin);

            // Read the data from the CSV file
            // Run through all the objects that it creates
            float min = 1000000;
            CsvContext cc = new CsvContext();
            cc.Read<CSVSceneObject>(txtReader, inputFileDescription)
                .ToList()
                .ForEach( so =>
                    {
                        if (so.Y < min)
                        {
                            min = so.Y;
                        }
                    });

            float add = (Mathf.Abs(min) * 2) + 1;
            CsvContext ccc = new CsvContext();
            ccc.Read<CSVSceneObject>(txtReader, inputFileDescription)
                .ToList()
                .ForEach( so =>
                    {
                        GameObject copy = Instantiate(prefabs[so.Type]);

                        copy.transform.position = new Vector3(so.X, so.Y + add, so.Z);
                        copy.transform.parent = AtomHolder.transform;
                        copy.SetActive(true);
                    });
        }*/
        // Read the input file, parse the entries for the atom types and coordinates, adjust the coordinates based on
        // the center of the ball of atoms so that it is closer to the origin (0, 0, 0), and create a game object
        // for each atom
        using (var ms = new MemoryStream())
        using (var txtWriter = new StreamWriter(ms))
        using (var txtReader = new StreamReader(ms))
        {
            txtWriter.Write(inputFile.text);
            ms.Seek(0, SeekOrigin.Begin);

            // Read the data from the CSV file
            // Run through all the objects that it creates
            float cnt = 0;
            float x_sum = 0;
            float y_sum = 0;
            float z_sum = 0;

            // Read through each line of the csv file, and compute the number of entries
            // as well as the mean values of X, Y, and Z.
            CsvContext cc = new CsvContext();
            cc.Read<CSVSceneObject>(txtReader, inputFileDescription)
                .ToList()
                .ForEach(so =>
                {
                    cnt++;
                    x_sum += so.X;
                    y_sum += so.Y;
                    z_sum += so.Z;
                });

            // Calculate the means of X, Y, and Z.
            float x_mean = x_sum / cnt;
            float y_mean = y_sum / cnt;
            float z_mean = z_sum / cnt;

            // For each line of the csv file, create a new gameobject that is a copy of
            // the prefab of that atom type. Adjust the position of the atom using the mean
            // so that it's closer to the origin, and make it a child of the AtomHolder object.
            // Activate the object.
            CsvContext ccc = new CsvContext();
            ccc.Read<CSVSceneObject>(txtReader, inputFileDescription)
                .ToList()
                .ForEach(so =>
                {
                    GameObject copy = Instantiate(prefabs[so.Type]);

                    copy.transform.position = new Vector3(so.X - x_mean, so.Y - y_mean, so.Z - z_mean);
                    copy.transform.parent = AtomHolder.transform;
                    copy.SetActive(true);

                });

        }
    }  
}
