using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLevelControl : MonoBehaviour {
    private bool[] levelPermissions;
    [SerializeField]
    private GameObject[] levelPermissionObjectsUnlock;
    [SerializeField]
    private GameObject[] levelPermissionObjectsLock;
    private void Start()
    {
        if (SaveLoad.Load() == null)//if it is first time only level 1 will be true, if you want to reset level unlocks just delete inside if and write down true and run the game from the menu for once then come here and undo what you did in if and run the game
        {
            levelPermissions = new bool[] { true, false, false, false };
            SaveLoad.Save(levelPermissions,0);

        }
       
            levelPermissions= SaveLoad.Load().savedLevels;
        
       

        for (int i = 0; i < levelPermissionObjectsLock.Length; i++)
        {
           
            if (levelPermissions[i] == true)
            {
                levelPermissionObjectsLock[i].SetActive(false);
                levelPermissionObjectsUnlock[i].SetActive(true);
            }
            else if (levelPermissions[i] == false)
            {
                levelPermissionObjectsLock[i].SetActive(true);
                levelPermissionObjectsUnlock[i].SetActive(false);
            }
        }
    }
}
