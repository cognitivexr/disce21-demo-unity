﻿using System.Collections.Generic;
using CognitiveXR.CogStream;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class CogStreamManager : MonoBehaviour
{
    private MediatorClient mediatorClient;
    private List<Engine> engines;
    
    public EngineSelectionMenu EngineSelectionMenu;
    public GameObject emotionEnginePrefab;
    public GameObject yoloEnginePrefab;
    
    public string websocketURL;
    
    public void Awake()
    {
        //create a mediator client
        mediatorClient = new MediatorClient(websocketURL);
        mediatorClient.Open();
        
        EngineSelectionMenu.OnEngineSelected += OnEngineSelected;
    }

    private async void OnEngineSelected(int engineidx)
    {
        if (engines != null && engines.Count > engineidx)
        {
            Engine engine = engines[engineidx];

             string address = await mediatorClient.StartEngine(engine);

             GameObject selectedEngine = null;
             if (engine.name == "yolov5")
             {
                 selectedEngine = yoloEnginePrefab;
             }
             else if (engine.name == "fermx")
             {
                 selectedEngine = emotionEnginePrefab;
             }

             GameObject go = Instantiate(selectedEngine);
             HLImageSenderComponent imageSenderComponent = go.GetComponent<HLImageSenderComponent>();
             
             imageSenderComponent.Launcher(address);
        }
    }

    private void OnDestroy()
    {
        if (mediatorClient != null)
        {
            mediatorClient.Close();
        }
    }

    public async void Start()
    {
        if(mediatorClient == null)
            return;
        
        // wait for a connection to the mediator
        await new WaitUntil(mediatorClient.IsOpen);
        
        // get all available engines from the mediator
        engines = await mediatorClient.GetEngines();

        // show selection of mediators
        EngineSelectionMenu.DisplayEngines(engines);
    }
}
