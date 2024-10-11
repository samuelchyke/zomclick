using System;
using Debug = UnityEngine.Debug;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using Zenject;

public class JsonSeeder : ISeeder<string>, IInitializable
{
    private readonly ISeedDao seedDao;
    private readonly ISeedEntityUpdater seedEntityUpdater;
    
    [Inject]
    public JsonSeeder(
        ISeedDao seedDao,
        ISeedEntityUpdater seedEntityUpdater
    ) {
        this.seedDao = seedDao;
        this.seedEntityUpdater = seedEntityUpdater;
    }

    public void Initialize(){}

    public void Seed(string data)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        try { 
            string jsonFilePath = Path.Combine(Application.streamingAssetsPath, data);
            string jsonContent = File.ReadAllText(jsonFilePath);

            ParsedDataDump parsedData = JsonUtility.FromJson<ParsedDataDump>(jsonContent);
            // var dataVersionOnDevice = seedDao.DataVersionOnDevice();

            // if (seedDao.IsDataVersionNewer(incomingDataVersion: parsedData.metadata.dataVersion)) {
                var entitiesToSeed = seedDao.BuildEntitiesList(parsedData);

                seedEntityUpdater.UpsertDataInTransaction(
                    preparedData: entitiesToSeed, 
                    dataVersionOnDevice: 0
                );
            // }
            
            stopwatch.Stop();
            Debug.Log($"Seeding completed in {stopwatch.ElapsedMilliseconds} ms.");
        }
        catch (Exception ex) {
            Debug.LogError($"Error seeding database: {ex.Message}");
        }
    }
}
