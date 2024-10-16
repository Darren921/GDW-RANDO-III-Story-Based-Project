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
    // Start is called before the first frame update
    void Start()
    {
        conversation = GetComponent<NPCConversation>();
    }

     void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player" && !ConversationManager.Instance.IsConversationActive )
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

  
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
