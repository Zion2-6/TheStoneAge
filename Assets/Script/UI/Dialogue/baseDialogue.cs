using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    ItemSlotData[] inventoryItemSlots;
    bool isTwo = false;
    Ray ray;
    public int index = 0;
    private void Start()
    {
        dialoguePanel.gameObject.SetActive(false);
    }
    private void Update()
    {
        inventoryItemSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool isPanelActive = UIManager.Instance.IsDialoguePromptActive();
            if (!isPanelActive)
            {
                dialoguePanel.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool isPanelActive = UIManager.Instance.IsDialoguePromptActive();
            if (!isPanelActive)
            {
                dialoguePanel.gameObject.SetActive(false);
            }
        }
    }

    protected void StartDialogue(GameObject questObject, GameObject quest)
    {
        
        bool getguest = false;
        QuestManager questManager = questObject.GetComponent<QuestManager>();
        npcController npccontroller = quest.GetComponent<npcController>();

        if (DialogueManager.Instance != null)
        {
            if (getguest == false)
            {
                if (questManager.quest[0].CheckCompletion(inventoryItemSlots) && isTwo == false)
                {
                    index = 1;
                    getguest = true;
                    DialogueManager.Instance.StartDialogue(npccontroller.charcterData.secondQuestdialogueLines);
                    isTwo = true;
                    return;
                }
                Debug.Log("완료전");
                DialogueManager.Instance.StartDialogue(npccontroller.charcterData.firstquestdialogueLines);

            }
            else if (getguest == true && index == 1 && isTwo == true)
            {
                if (questManager.quest[1].CheckCompletion(inventoryItemSlots))
                {
                    Debug.Log("두번째 퀘스트 완료");
                    DialogueManager.Instance.StartDialogue(npccontroller.charcterData.completedialogueLines);
                    isTwo = false;
                    index = 0;
                    return;
                }
                Debug.Log("이부분");
                DialogueManager.Instance.StartDialogue(npccontroller.charcterData.secondQuestdialogueLines);

            }
        }
    }

  
}
