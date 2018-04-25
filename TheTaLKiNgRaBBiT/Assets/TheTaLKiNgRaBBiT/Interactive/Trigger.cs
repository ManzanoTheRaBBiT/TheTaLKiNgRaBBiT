using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * @author TheRaBBiT
 */

/**
 * The Trigger class is responsible for activate the DialogueManager on it.
 * It will either call the OpenDialogue method or load a different Scene.
 * For a Scene the scene attribute must be set, and the dbManager must be null.
 * For open a conversation the dbManager and the tree attributes must be set.
 */
public class Trigger : MonoBehaviour {

    [SerializeField] private int tree;
    [SerializeField] private string flag;
    [SerializeField] private string scene;

    [SerializeField] private bool on;
    [SerializeField] private GameObject skipIcon;

    [SerializeField] private DialogueManager dBManager;

    void Start () {
        this.skipIcon.SetActive(false);
        this.on = false;
    }

    // Update is called once per frame
    void Update () {
        if ((on && !dBManager.IsCutscene) && (Input.GetKeyUp(KeyCode.Space))) {
            this.Triggered();
        }
    }

    private void Triggered(){
        if(!string.IsNullOrEmpty(scene)){
            SceneManager.LoadScene(scene);
        }
        if(!string.IsNullOrEmpty(flag)){
            Flag.SetFlag(flag , 1);
        }
        if(tree != -1){
            this.on = false;
            this.dBManager.OpenDialogue(tree);
        }
    }

    private void ShowHUD(){
        this.skipIcon.SetActive(true);
    }

    private void HideHUD(){
        this.skipIcon.SetActive(false);
    }

    void OnTriggerStay (Collider Col) {
        this.ShowHUD();
        this.on = true;
    }

    void OnTriggerExit (Collider Col) {
        this.HideHUD();
        this.on = false;
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
