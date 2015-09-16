using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

using Serializer;

public class SimulationScript : MonoBehaviour
{
    public bool SimulationMode;

    private NodePath Simulation_1;

    public static SimulationScript Instance { get; private set; }

	void Awake () 
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
	}

    void Start()
    {
        SimulationMode = false;

        UiManager.Instance.SaveSimulationButton.onClick.AddListener(() => Save());
        UiManager.Instance.LoadSimulationButton.onClick.AddListener(() => Load());

        WorldScript.Instance.UnlockedPath_Simulation = new NodePath();
    }

    public void Save()
    {
        Simulation_1 = WorldScript.Instance.UnlockedPath_Simulation;

        var serializer = new XmlSerializer(typeof(UserNodeSerializer));
        UserNodeSerializer nodeSerializer = new UserNodeSerializer();

        StringWriter stream = new StringWriter();
        foreach (NodeBase n in WorldScript.Instance.UnlockedPath_Simulation.Path)
        {
            nodeSerializer.Add(n);
        }
        serializer.Serialize(stream, nodeSerializer);

        StartCoroutine("SendSimulationGraph", stream);
    }

    IEnumerator SendSimulationGraph(StringWriter _stream)
    {
        yield return StartCoroutine(User.Instance.Reconnect());
        if (!User.Instance.Connected)
        {
            UiManager.Instance.ShowAlertMessage("An error occured when reconnecting to the server. Please retry or refresh your page.");
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("xml", _stream.ToString());
            WWW www = new WWW("http://" + User.ServerHostname + "/atlas/save_user_simulation_graph.php", form);

            yield return www;

            if (www.error != null)
            {
                UiManager.Instance.ShowAlertMessage("An error occured");
            }
            else
            {
                UiManager.Instance.ShowAlertMessage("Your simulation has been saved successfully");
            }
        }
    }

    public void LoadXML()
    {
        if (User.Instance.Connected)
        {
            // Clear the nodes in case it's already in simulation mode
            if (SimulationMode)
            {
                SwitchSimulation();
                SwitchSimulation();
            }
            StartCoroutine("LoadSimulationXML", "http://" + User.ServerHostname + "/atlas/get_user_simulation_graph.php");
        }
        else
        {
            Debug.Log("Couldn't load simulation because the user is not connected");
        }
    }

    IEnumerator LoadSimulationXML(string _url)
    {
        WWW www = new WWW(_url);

        yield return www;

        // check for errors
        if (www.error != null)
        {
            Debug.Log("WWW error : " + www.error);
        }
        else
        {
            string data = www.text;
            if (!(data == "fail"))
            {
                Simulation_1 = new NodePath();

                var serializer = new XmlSerializer(typeof(UserNodeSerializer));
                UserNodeSerializer nodeSerializer;
                using (XmlReader stream = XmlReader.Create(new StringReader(data)))
                {
                    nodeSerializer = serializer.Deserialize(stream) as UserNodeSerializer;
                }

                foreach (int i in nodeSerializer.Nodes)
                {
                    NodeBase n = WorldScript.Instance.m_nodes[i].GetComponent<NodeBase>();
                    if (n)
                    {
                        Simulation_1.Add(n);
                    }
                }
                Debug.Log("Loaded user simulation 1");
            }
            else
            {
                Debug.Log("Failed to load user simulation graph");
            }
        }
    }

    public void Load()
    {
        if (Simulation_1 != null)
        {
            SetSimulation(true);
            foreach (NodeBase n in Simulation_1.Path)
            {
                n.UnlockSimulationNode();
            }
            WorldScript.Instance.UnlockedPath_Simulation = Simulation_1;
        }
    }

    public void SwitchSimulation()
    {
        SetSimulation(!SimulationMode);
    }

    public void SetSimulation(bool _b)
    {
        SimulationMode = _b;

        // Fix Bug : Need it even for disable because bSimulationUnlock = false remove the node from the simulation saved by the user
        WorldScript.Instance.UnlockedPath_Simulation = new NodePath();
        if (!SimulationMode)
        {
            PathCostPanel.Instance.Clean();
            foreach (Transform n in WorldScript.Instance.m_nodes.Values)
            {
                n.GetComponent<NodeBase>().bSimulationUnlock = false;
            }
            UiManager.Instance.SaveSimulationButton.interactable = false;
            UiManager.Instance.SimulationButton.GetComponentInChildren<Text>().text = "Simulation (OFF)";
        }
        else
        {
            UiManager.Instance.SaveSimulationButton.interactable = true;
            UiManager.Instance.SimulationButton.GetComponentInChildren<Text>().text = "Simulation (ON)";
        }
    }
}
