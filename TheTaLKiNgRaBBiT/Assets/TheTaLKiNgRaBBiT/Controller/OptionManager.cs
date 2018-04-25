using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

/**
 * @author TheRaBBiT
 */

/**
 * The OptionManager is the class responsible to set a answer for the current dialogue
 * The ID is the KeyCode for the keyboard number of the Option.ID + 1 ...
 * This means that if your question has two answers, this class will be instantiated two times.
 * The class will create a keyboard shortcut for the button, with the number of the ID + 1, and set the text to represent this number
 * The class will also set a task on click to represent the action of "answer the question"
 * Either clicking the button, or pressing the number, this class will call SetAnswer() with the Option.ID as a parameter
 */
public class OptionManager : MonoBehaviour {

    [SerializeField] private DialogueManager dBManager;
    [SerializeField] private KeyCode ID;

    private Option option;
    [SerializeField] private Button button;

    void Start () {
        ID = (KeyCode) System.Enum.Parse(typeof(KeyCode), "Alpha"+(this.Option.ID + 1).ToString());
        button = this.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void Update () {
        if(Input.GetKeyUp(ID)){
            this.DBManager.SetAnswer(this.option.ID);
        }
    }

    void TaskOnClick(){
        this.DBManager.SetAnswer(this.option.ID);
    }

    public Option Option {
        get {
            return option;
        }
        set {
            option = value;
        }
    }

    public DialogueManager DBManager {
        get {
            return dBManager;
        }
        set {
            dBManager = value;
        }
    }
}
