using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using LINQtoCSV;

public class CSVExampleObjectSpawner : MonoBehaviour {

	private Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

	[Tooltip("The file that contains the objects to be spawned in this scene")]
	public TextAsset sceneDescriptor;

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
		CsvFileDescription inputFileDescription = new CsvFileDescription 
		{
			SeparatorChar = ',',
			FirstLineHasColumnNames = true
		};

		using(var ms = new MemoryStream())
		{
			using (var txtWriter = new StreamWriter(ms))
			{
				using (var txtReader = new StreamReader(ms))
				{
					txtWriter.Write(sceneDescriptor.text);
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
				}
			}
		}
	}
}
