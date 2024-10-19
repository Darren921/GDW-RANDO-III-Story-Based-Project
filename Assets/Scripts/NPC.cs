using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DialogueEditor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private NPCConversation conversation;
    
    [SerializeField]
    CinemachineInputProvider InputProvider;
    private bool talkedToFirst;
    private bool walkInBypass;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        conversation = GetComponent<NPCConversation>();
        _player = FindObjectOfType<Player>();   
    }

  
     void OnTriggerEnter (Collider other)
     {
         switch (tag)
         {
             case "WalkinBypass":
                 walkInBypass = true;
                 break;
         }

       
    
       

       
        if (!talkedToFirst && !ConversationManager.Instance.IsConversationActive && walkInBypass )
        {
            talkedToFirst = true; 
            ConversationManager.Instance.StartConversation(conversation);
            Cursor.lockState = CursorLockMode.None;
            InputProvider.enabled = false;
            InputManager.DisableInGame(); 
            
        }

        if (other.tag == "Player" && !ConversationManager.Instance.IsConversationActive && _player.isTalking && talkedToFirst && !walkInBypass)
        {
            ConversationManager.Instance.StartConversation(conversation);
            Cursor.lockState = CursorLockMode.None;
            InputProvider.enabled = false;
        }
     
      
    }
    void OnTriggerStay (Collider other)
    {
        switch (this.name)
        {
            case "Npc":
                walkInBypass = true;
                break;
        }
         if (talkedToFirst && !ConversationManager.Instance.IsConversationActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
            InputProvider.enabled = true;
            InputManager.EnableInGame();
        }
        if (other.tag == "Player" && !ConversationManager.Instance.IsConversationActive && _player.isTalking   && !walkInBypass)
        {
            ConversationManager.Instance.StartConversation(conversation);
            Cursor.lockState = CursorLockMode.None;
            InputProvider.enabled = false;
        }
       
      
    }
    void OnTriggerExit (Collider other)
    {
            ConversationManager.Instance.EndConversation();
            Cursor.lockState = CursorLockMode.Locked;
            InputProvider.enabled = true;
            walkInBypass = false;
    }

    // Update is called once per frame
    void Update()
    {
       print(_player.isTalking); ;
    }

    public void DestoyThis()
    {
        gameObject.SetActive(false);
        ConversationManager.Instance.EndConversation();
        Cursor.lockState = CursorLockMode.Locked;
        InputProvider.enabled = true;
        walkInBypass = false;
        InputManager.EnableInGame(); 

    }

    
}
