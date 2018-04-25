using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author TheRaBBiT
 */

/**
 * This class works as a storage object for the <entrie> tag in the XML
 * This class purpose is be a "container" for the information on the <entrie> tag
 * Each Option object will become a button on the screen
 * Each button will call the SetAnswer method, with the Option.iD attribute as a parameter
 */
public class Option {

	private string text;
	private int tree;
    private int iD;
    private int flag;

    public int Flag {
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

	public string Text {
		get {
			return text;
		}
		set {
			text = value;
		}
	}

	public int Tree {
		get {
			return tree;
		}
		set {
			tree = value;
		}
	}
}
