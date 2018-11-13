using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopulationManager : MonoBehaviour
{
    public GameObject LunarLanderPrefab;
    public GameObject Platform;

    public int PopulationCount = 40;

    public Vector3 SceneHalfExtents = new Vector3 (20.0f, 0.0f, 20.0f);

    public float GenerationDuration = 20.0f;
    public int IterationCount = 1;

    public int EliteCount = 4;
    public float MutationChance = 0.10f;
    public float MutationRate = 0.01f;

    public int InputsCount = 4;
    public int HiddenLayers = 1;
    public int OutputsCount = 2;
    public int NeuronsCountPerHL = 7;
    public float Bias = 1f;
    public float P = 0.5f;


    GeneticAlgorithm genAlg;

    List<LunarLander> populationGOs = new List<LunarLander>();
    List<Genome> population = new List<Genome>();
    List<NeuralNetwork> brains = new List<NeuralNetwork>();
    List<GameObject> mines = new List<GameObject>();
     
    float accumTime = 0;
    int generation = 0;

    static PopulationManager instance = null;

    public static PopulationManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PopulationManager>();

            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Create and confiugre the Genetic Algorithm
        genAlg = new GeneticAlgorithm(EliteCount, MutationChance, MutationRate);

        GenerateInitialPopulation();
    }

    // Generate the random initial population
    void GenerateInitialPopulation()
    {
        RelocatePlatform();

        generation = 0;

        // Destroy previous lunar landers (if there are any)
        DestroyLunarLanders();
        
        for (int i = 0; i < PopulationCount; i++)
        {
            NeuralNetwork brain = CreateBrain();
            
            Genome genome = new Genome(brain.GetTotalWeightsCount());

            brain.SetWeights(genome.genome);
            brains.Add(brain);

            population.Add(genome);
            populationGOs.Add(CreateLunarLander(genome, brain));
        }

        accumTime = 0.0f;
    }

    // Creates a new NeuralNetwork
    NeuralNetwork CreateBrain()
    {
        NeuralNetwork brain = new NeuralNetwork();

        // Add first neuron layer that has as many neurons as inputs
        brain.AddFirstNeuronLayer(InputsCount, Bias, P);

        for (int i = 0; i < HiddenLayers; i++)
        {
            // Add each hidden layer with custom neurons count
            brain.AddNeuronLayer(NeuronsCountPerHL, Bias, P);
        }

        // Add the output layer with as many neurons as outputs
        brain.AddNeuronLayer(OutputsCount, Bias, P);

        return brain;
    }

    // Evolve!!!
    void Epoch()
    {
        SaveFitness();

        // Increment generation counter
        generation++;

        // Evolve each genome and create a new array of genomes
        Genome[] newGenomes = genAlg.Epoch(population.ToArray());

        // Clear current population
        population.Clear();

        // Add new population
        population.AddRange(newGenomes);

        // Set the new genomes as each NeuralNetwork weights
        for (int i = 0; i < PopulationCount; i++)
        {
            NeuralNetwork brain = brains[i];

            brain.SetWeights(newGenomes[i].genome);

            populationGOs[i].SetBrain(newGenomes[i], brain);
            populationGOs[i].transform.position = GetRandomPos();
        }

        RelocatePlatform();

    }

    // Update is called once per frame
    void FixedUpdate () 
	{
        for (int i = 0; i < Mathf.Clamp(IterationCount, 1, 120); i++)
        {
            bool areAllDeadOrLanded = true;

            foreach (LunarLander t in populationGOs)
            {
                // Set platform to current lunar lander
                t.SetPlatform(this.Platform);

                // Think!! 
                t.Think(Time.fixedDeltaTime);

                if (t.state == LunarLander.State.Flying)
                    areAllDeadOrLanded = false;

                // Just adjust lunar lander position when reaching world extents
                Vector3 pos = t.transform.position;
                if (pos.x > SceneHalfExtents.x || pos.x < -SceneHalfExtents.x ||
                    pos.y > SceneHalfExtents.y || pos.y < -SceneHalfExtents.y)
                    t.state = LunarLander.State.Destroyed;
            }

            // Check the time to evolve
            accumTime += Time.fixedDeltaTime;
            if (areAllDeadOrLanded || accumTime >= GenerationDuration)
            {
                accumTime = 0.0f;
                Epoch();
            }
        }
	}

#region Helpers
    LunarLander CreateLunarLander(Genome genome, NeuralNetwork brain)
    {
        Vector3 position = GetRandomPos();
        GameObject go = Instantiate<GameObject>(LunarLanderPrefab, position, Quaternion.identity);
        LunarLander t = go.GetComponent<LunarLander>();
        t.SetBrain(genome, brain);
        return t;
    }

    void DestroyLunarLanders()
    {
        foreach (LunarLander go in populationGOs)
            Destroy(go);

        populationGOs.Clear();
        population.Clear();
        brains.Clear();
    }

    public void RelocatePlatform()
    {
        Platform.transform.position = new Vector3(0.0f, -35.0f, 0.0f); //GetRandomPos();
    }

    Vector3 GetRandomPos()
    {
        return new Vector3(Random.value * SceneHalfExtents.x * 2.0f - SceneHalfExtents.x, SceneHalfExtents.y - 20.0f, 0.0f); 
    }

    float max = 0.0f;
    float min = float.MaxValue;
    float average = 0.0f;

    void SaveFitness()
    {
        max = 0.0f;
        min = float.MaxValue;
        average = 0.0f;

        foreach(LunarLander go in populationGOs)
        {
            if (go.Fitness < min)
                min = go.Fitness;
            else if (go.Fitness > max)
                max = go.Fitness;
            
            average += go.Fitness;
        }

        average /= populationGOs.Count;
    }

    void OnGUI()
    {
        string strFormat = "Generation: {0}";

        GUILayout.Label(string.Format(strFormat, generation));

        strFormat = "Max Fitness: {0}";
        GUILayout.Label(string.Format(strFormat, max));

        strFormat = "Min Fitness: {0}";
        GUILayout.Label(string.Format(strFormat, min));

        strFormat = "Avg. Fitness: {0}";
        GUILayout.Label(string.Format(strFormat, average));
    }
#endregion

}
