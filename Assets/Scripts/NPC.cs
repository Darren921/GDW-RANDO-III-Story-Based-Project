
using DialogueEditor;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private NPCConversation conversation;
    private QuestManager questManager;
    private bool walkInBypass;
    private bool TalkedToFirst;
    private Player _player;
    public static bool Pos1F, Pos2F, Pos3F, Neg1F, Neg2F, Neg3F;

    TextMeshProUGUI interactText;

    // Start is called before the first frame update
    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        conversation = GetComponent<NPCConversation>();
        _player = FindObjectOfType<Player>();
        interactText = GameObject.Find("TalkText").GetComponent<TextMeshProUGUI>();
    }


    void OnTriggerEnter(Collider other)
    {

        switch (tag)
        {
            case "WalkinBypass":
                walkInBypass = true;
                break;
        }




        if (other.tag == "Player" && name != "You" && !ConversationManager.Instance.IsConversationActive)
        {
            interactText.text = "Press 'E' to talk";
            interactText.gameObject.SetActive(true);
        }

        if (!ConversationManager.Instance.IsConversationActive && walkInBypass && !TalkedToFirst)
        {
            ConversationManager.Instance.StartConversation(conversation);
            if (ConversationManager.Instance.GetBool("TalkedToFirst") != null)
            {
                if (ConversationManager.Instance.GetBool("TalkedToFirst") == false)
                {
                    questManager.convoLock();
                    TalkedToFirst = true;
                }

            }



        }

        if (other.tag == "Player" && !ConversationManager.Instance.IsConversationActive && _player.isTalking &&
            !walkInBypass)
        {
            ConversationManager.Instance.StartConversation(conversation);
            questManager.convoLock();
            interactText.gameObject.SetActive(false);
        }


    }

    void OnTriggerStay(Collider other)
    {
        switch (tag)
        {
            case "WalkinBypass":
                walkInBypass = true;
                break;
        }

        if (!ConversationManager.Instance.IsConversationActive && !_player.isTalking)
        {

            print("unlocked");
            questManager.convoUnlock();
            walkInBypass = false;
        }

        if (other.tag == "Player" && !ConversationManager.Instance.IsConversationActive && _player.isTalking &&
            !walkInBypass)
        {
            ConversationManager.Instance.StartConversation(conversation);
            questManager.convoLock();
            interactText.gameObject.SetActive(false);

        }




    }

    void OnTriggerExit(Collider other)
    {
        questManager.convoUnlock();
        walkInBypass = false;
        interactText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DisableThis()
    {
        gameObject.SetActive(false);
        questManager.convoUnlock();
        walkInBypass = false;

    }


    public void CheckConvo()
    {
        if (Pos1F)
        {
            if (ConversationManager.Instance.GetBool("Positive1Found") != null)
            {
                ConversationManager.Instance.SetBool("Positive1Found", true);
            }
        }

        if (Neg1F)
        {
            if (ConversationManager.Instance.GetBool("Negative1Found") != null)
            {
                ConversationManager.Instance.SetBool("Negative1Found", true);
            }
        }

        print(name);
        print(ConversationManager.Instance.GetBool("Positive1Found"));
        print(ConversationManager.Instance.GetBool("Negative1Found")); 
        switch (name)
        {
            case "NPC 1":
                if (ConversationManager.Instance.GetBool("Positive1Found") && !Pos1F)
                {
                    Pos1F = true;
                }
                break;
            case "NPC 2":
                if (ConversationManager.Instance.GetBool("Negative1Found") && !Neg1F)
                {
                    Neg1F = true;
                }
                break;
        }
    }
}


