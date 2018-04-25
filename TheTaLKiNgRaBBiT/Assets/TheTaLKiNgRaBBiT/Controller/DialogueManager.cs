using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * @author TheRaBBiT
 */

/**
* The DialogueManager class is responsible to update all the HUD elements according to the current <dialogue> tag
* Every moment of the conversation (the time where the game is waiting for the player either answer a question or skip the current text) is called step
* Every time you want to start a conversation call the OpenDialogue() method with a int tree as a parameter
* The DialogueManager will activate on the tree passed as parameter and will keep going to the next tree (indicated on the xml) on each step
* This class will then shape the HUD as the dialogue commands
* resourceComponent indicates witch Image object on the array must be replaced, resource is the image that will be placed on the Image object
* speechBallon is the Image that will keep the speechBallon tag of the XML
* DialogueManager is also responsible for check if the dialogue is a question, and for wait for the answer of the player
* DialogueManager is also responsible for set the flags with the options values and call scenes when the dialogue is over
*/
public class DialogueManager : MonoBehaviour {

    //HUD
    [SerializeField] private GameObject DialogueCanvas;
    [SerializeField] private Image[] avatar;
    [SerializeField] private Image speechBallon;
    [SerializeField] private Image skipIcon;
    [SerializeField] private Text dialogueText;
    [SerializeField] private bool[] avatarIsActive;

    //Framework
    [SerializeField] private DialogueReader dialogueReader;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private int tree;
    [SerializeField] private bool isAnswer;
    [SerializeField] private bool answered = false;
    [SerializeField] private string SceneName = null;
    [SerializeField] private bool isCutscene = false;

    //HUD OPTIONS
    [SerializeField] private float offset = 0;
    [SerializeField] private GameObject Option;
    [SerializeField] private Transform Options;


	public void GetStep(){

        if(this.Tree == -1){
            this.CloseDialogue();

            if(!string.IsNullOrEmpty(this.SceneName)){
                SceneManager.LoadScene(this.SceneName);
            }

            return;
        }

        if (!string.IsNullOrEmpty(this.dialogueReader.Dialogues [this.Tree].Scene)) {
            this.SceneName = this.dialogueReader.Dialogues [this.Tree].Scene;
        }

		this.dialogue = this.dialogueReader.Dialogues[this.Tree];
        this.dialogueText.GetComponent<Text>().text = this.dialogueReader.Dialogues[this.Tree].Text;

        this.ManageImages();
        this.ManageOptions();
	}

	public void SetAnswer(int Answer){
        print("????");
		if (this.dialogueReader.Dialogues [this.Tree].Flag != null) {
			Flag.SetFlag(this.dialogueReader.Dialogues [this.Tree].Flag , this.dialogueReader.Dialogues[this.Tree].Options[Answer].Flag);
		}
		this.Tree = this.dialogueReader.Dialogues[this.Tree].Options[Answer].Tree;
        this.GetStep();
	}

	private void ManageImages(){

        int i = 0;
        foreach(bool active in avatarIsActive){
        	this.avatar[i].enabled = active;
            i++;
        }

        this.avatar[this.dialogue.ResourceComponent].sprite = Resources.Load<Sprite>(this.dialogue.Resource) as Sprite;

        this.speechBallon.sprite = Resources.Load<Sprite>(this.dialogue.SpeechBallon) as Sprite;

	}

    //Options Creation
    private void CreateOption(Option option , int i){
        this.offset += Option.GetComponent<RectTransform>().rect.height;
        GameObject optionObject = Instantiate(this.Option);
        optionObject.GetComponent<OptionManager>().DBManager = this;
        optionObject.GetComponent<OptionManager>().Option = option;
        optionObject.SetActive(true);
        optionObject.transform.SetParent(this.Options.transform , false);
        optionObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(optionObject.GetComponent<RectTransform>().anchoredPosition.x , optionObject.GetComponent<RectTransform>().anchoredPosition.y - offset);
        GameObject ChildGameObject = optionObject.transform.GetChild(0).gameObject;
        ChildGameObject.GetComponent<Text>().text = " " + (i)+". " + (option.Text);
    }

    private void ManageOptions(){

        if (this.dialogue.Options.Count > 1) {
            this.IsAnswer = true;
            this.skipIcon.enabled = false;

            this.ClearOptions();

            int i = 0;
            foreach(Option option in this.dialogue.Options){
                i++;
                this.CreateOption(option , i);
            }

        } else {
            this.skipIcon.enabled = true;
            this.ClearOptions();
            this.Tree = this.dialogue.Options[0].Tree;
            this.answered = true;
        }

    }

    private void ClearOptions(){
        int childrens = Options.childCount;
        for (int i = childrens - 1; i >= 0; i--){
            GameObject.Destroy(Options.GetChild(i).gameObject);
        }
        this.offset = 0;
    }
    //Options Creation End

	public void CloseDialogue(){
		this.DialogueCanvas.SetActive(false);
        this.enabled = false;
        this.isCutscene = false;
	}

	public void OpenDialogue(int Tree){
        this.isCutscene = true;
        this.enabled = true;
        this.Tree = Tree;
        this.GetStep();
		this.DialogueCanvas.SetActive(true);
	}

    private bool SkipButton(){
        if((Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))  && !isAnswer){
            return true;
        }
        return false;
    }

    void Update () {
        this.isCutscene = this.DialogueCanvas.activeInHierarchy;
        if(this.isCutscene && SkipButton()){
            this.GetStep();
        }
    }

    void LateUpdate(){
        if(answered){
            answered = false;
            isAnswer = false;
        }
    }

    public bool IsAnswer {
        get {
            return isAnswer;
        }
        set {
            isAnswer = value;
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

    public bool IsCutscene {
        get {
            return isCutscene;
        }
        set {
            isCutscene = value;
        }
    }
}
