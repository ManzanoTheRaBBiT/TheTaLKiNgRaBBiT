using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
using UnityEngine;

/**
 * @author TheRaBBiT
 */

/**
 * This class works as a storage object for the <dialogue> tag in the XML
 * The purpose is be a "container" for the information on the <dialogue> tag
 * The base of this dialogue system is have a list with objects of this class, with specific ID's for the DialogueManager class be able to map the entries
 *
 * ALWAYS REMEMBER that the id on the <dialogue> tag is not only the tag identification, but also his index on the dialogues list
 * ALWAYS set the <dialogue> tags beginning with id="0" , and keep incrementing the id's in one by one
 * if this pattern is not respected, than the tree will be pointing to a incorrect dialogue object, and there will be a logic error on the conversation
 * The conversation don't need to have a coherent sequence on their texts, but every dialogue must have his tree attribute pointing to the correct dialogue
 * Basically the tree attribute is not a reference to the id of the dialogue, but to his position on the XML
 */
public class DialogueReader : MonoBehaviour {

    [SerializeField] private List<Dialogue> dialogues;
    [SerializeField] private Dialogue dialogueAux;

    //XML Attributes
    [SerializeField] private XmlReader XmlFile;
    [SerializeField] private Stream XmlPathStream;
    [SerializeField] private TextAsset XmlPath;

	void Start () {

		Dialogues = new List<Dialogue>();

		XmlPathStream = new MemoryStream(Encoding.UTF8.GetBytes(XmlPath.text));

		XmlFile = XmlReader.Create(XmlPathStream);

        this.ReadConversation();
	}

    public void ReadConversation(){
        while (XmlFile.Read()) {

            if (XmlFile.IsStartElement("dialogue")) {

                this.ReadDialogue();

            }
        }
    }

    public void ReadDialogue(){
        int optionNumber = int.Parse(XmlFile.GetAttribute("entries"));

        dialogueAux = new Dialogue();

        dialogueAux.ID = int.Parse(XmlFile.GetAttribute("id"));
        dialogueAux.Resource = XmlFile.GetAttribute("resource");
        dialogueAux.ResourceComponent = int.Parse(XmlFile.GetAttribute("resourceComponent"));
        dialogueAux.Flag = XmlFile.GetAttribute("flag");
        dialogueAux.Scene = XmlFile.GetAttribute("scene");
        dialogueAux.SpeechBallon = XmlFile.GetAttribute("speechBallon");

        XmlFile.Read();
        if (XmlFile.IsStartElement("text")) {
            dialogueAux.Text = XmlFile.ReadString();
        }

        dialogueAux.Options = new List<Option>();

        this.ReadOptions(optionNumber);

        Dialogues.Add(dialogueAux);
    }

    public void ReadOptions(int optionNumber){
        for (int i = 0; i < optionNumber; i++) {

            XmlFile.Read();

            dialogueAux.Options.Add(new Option());

            if (XmlFile.IsStartElement("entrie")) {
                dialogueAux.Options[i].ID = int.Parse(XmlFile.GetAttribute("id"));
                dialogueAux.Options[i].Flag = int.Parse( (XmlFile.GetAttribute("flag") != null) ?  XmlFile.GetAttribute("flag") : "0");
                XmlFile.Read();
            }

            if (XmlFile.IsStartElement("text")) {
                dialogueAux.Options[i].Text = XmlFile.ReadString();
                XmlFile.Read();
            }

            if (XmlFile.IsStartElement("tree")) {
                dialogueAux.Options[i].Tree = int.Parse(XmlFile.ReadString());
                XmlFile.Read();
            }

            XmlFile.Read();

        }
    }

    public List<Dialogue> Dialogues {
        get {
            return dialogues;
        }
        set {
            dialogues = value;
        }
    }
}
	