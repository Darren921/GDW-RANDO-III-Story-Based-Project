using System.Net.Mime;
using Cinemachine;
using DialogueEditor;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private NPCConversation conversation;
    private QuestManager questManager;
    private bool walkInBypass;
    private Player _player;
    private bool Foolish;
    TextMeshProUGUI interactText;
    // Start is called before the first frame update
    void Start()
    {
        questManager =FindObjectOfType<QuestManager>();
        conversation = GetComponent<NPCConversation>();
        _player = FindObjectOfType<Player>();   
        interactText = GameObject.Find("TalkText").GetComponent<TextMeshProUGUI>();
    }

  
     void OnTriggerEnter (Collider other)
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
         
        if (!ConversationManager.Instance.IsConversationActive && walkInBypass )
        {
            ConversationManager.Instance.StartConversation(conversation);
            if (ConversationManager.Instance.GetBool("talkedToFirst") != null)
            {
                if (!ConversationManager.Instance.GetBool("talkedToFirst"))
                {
                    questManager.convoLock();
                }
            }
           
            
        }

        if ( other.tag == "Player" && !ConversationManager.Instance.IsConversationActive && _player.isTalking  && !walkInBypass)
        {
            ConversationManager.Instance.StartConversation(conversation);
            questManager.convoLock();
            interactText.gameObject.SetActive(false);
        }
     
      
    }
    void OnTriggerStay (Collider other)
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
        if (  other.tag == "Player" && !ConversationManager.Instance.IsConversationActive && _player.isTalking   && !walkInBypass)
        {
            ConversationManager.Instance.StartConversation(conversation);
            questManager.convoLock();
        }
        
        
       
      
    }
    void OnTriggerExit (Collider other)
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

   public  void CheckConvo()
    {
        if (Foolish)
        {
            ConversationManager.Instance.SetBool("Foolish",true);

        }
        print(name);
        print(ConversationManager.Instance.GetBool("Foolish"));
        switch (name)
        {
            case "NPC 1":
                if (ConversationManager.Instance.GetBool("Foolish") && !Foolish )
                {
                    print("foolish");
                    Foolish = true;
                }

                
            
                break;
        }
    }
}
