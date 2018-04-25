using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author TheRaBBiT
 */

/**
 * This class works as a storage object for the <dialogue> tag in the XML
 * This class purpose is be a "container" for the information on the <dialogue> tag
 * The base of this dialogue system is have a list with objects of this class, with specific ID's for the DialogueManager class map the entries
 */
public class Dialogue {

	private int iD;
	private string flag;
	private string text;
	private string resource;
    private int resourceComponent;
    private string speechBallon;
    private string scene;

	private List<Option> options;

	public List<Option> Options {
		get {
			return options;
		}
		set {
			options = value;
		}
	}

	public string Flag {
		get {
			return flag;
		}
		set {
			flag = value;
		}
	}

	public int ID {
		get {
			return iD;
		}
		set {
			iD = value;
		}
	}

	public string Resource {
		get {
			return resource;
		}
		set {
			resource = value;
		}
	}

	public string Scene {
		get {
			return scene;
		}
		set {
			scene = value;
		}
	}

    public int ResourceComponent {
        get {
            return resourceComponent;
        }
        set {
            resourceComponent = value;
        }
    }

    public string SpeechBallon {
        get {
            return (string.IsNullOrEmpty(speechBallon)) ? "resources/speechBallon" : speechBallon;
        }
        set {
            speechBallon = value;
        }
    }

	public string Text {
		get {
			return text;
		}
		set {
			text = value;
		}
	}



}
