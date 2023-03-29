using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public enum DrawMode {NoiseMap, ColourMap, Mesh};
	public DrawMode drawMode;
	
	public GameObject mesh;
	MeshCollider meshCollider;
	
	[Range(10, 241)]
	public int mapChunkSize = 241;
	[Range(0,6)]
	public int levelOfDetail;
	public float noiseScale;
	
	public int octaves;
	[Range(0,1)]
	public float persistance;
	public	float lacunarity;
	
	public int seed;
	public Vector2 offset;
	
	public float meshHeightMultiplier;
	public AnimationCurve meshHeightCurve;
	
	public bool autoUpdate;

	public BakingNavMesh bakingNavMesh;
	
	public TerrainType[] regions;
	
	public void Start()
	{
		seed = Random.Range(-100000, 100000);
		GenerateMap(true);
		Destroy(mesh.GetComponent<MeshCollider>());
		meshCollider = mesh.AddComponent<MeshCollider>();
		bakingNavMesh.Remove();
	}
	
	public void GenerateMap(bool playMode)
	{
		float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);
		
		Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
		for(int y = 0; y < mapChunkSize; y++)
		{
			for(int x = 0; x < mapChunkSize; x++)
			{
				float currentHeight = noiseMap[x,y];
				int width = noiseMap.GetLength(0);
				int height = noiseMap.GetLength(1);
				float topLeftX = (width - 1) / -2f;
				float topLeftZ = (height - 1) / 2f;
				Vector3 posTree = new Vector3 ((topLeftX + x) * 10, meshHeightCurve.Evaluate(noiseMap[x, y]) * 10 * meshHeightMultiplier + 1, (topLeftZ - y) * 10);
				int hasard = 0;
				for(int i = 0; i < regions.Length; i++)
				{
					if (currentHeight <= regions[i].height)
					{
						colourMap[y * mapChunkSize + x] = regions[i].colour;
						hasard = Random.Range(0, 100);
						if (regions[i].tree != null && hasard > 49 && playMode)
						{
							regions[i].autre = Instantiate(regions[i].tree, posTree, Quaternion.identity) as GameObject;
						}
						break;
					}
				}
			}
		}
		
		MapDisplay display = FindObjectOfType<MapDisplay>();
		if(drawMode == DrawMode.NoiseMap)
		{
			display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
		}
		else if(drawMode == DrawMode.ColourMap)
		{
			display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize));
		}else if(drawMode == DrawMode.Mesh)
		{
			display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize));
		}
	}
	
	private void OnValidate()
	{
		if(lacunarity < 1)
		{
			lacunarity = 1;
		}
		if(octaves < 0)
		{
			octaves = 0;
		}
	} 
}

[System.Serializable]
public struct TerrainType
{
	public string name;
	public float height;
	public Color colour;
	public GameObject tree;
	public GameObject autre;
}