using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class ItemActionPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject buttonPrefab;

        // 
        public void AddButon(string name, Action onClickAction)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            button.GetComponent<Button>().onClick.AddListener(() => onClickAction());
            button.GetComponentInChildren<TMPro.TMP_Text>().text = name;
        }

        // val 매개변수가 true가 되면, 버튼을 삭제하고, 새로운 게임 오브젝트 활성화. 
        public void Toggle(bool val)
        {
            if (val == true)
                RemoveOldButtons();
            gameObject.SetActive(val);
        }

        // 모든 게임오브젝트는 Transform 속성을 가지고 있으므로 Transform를 이용하여 모든 게임오브젝트 삭제
        public void RemoveOldButtons()
        {
            foreach (Transform transformChildObjects in transform)
            {
                Destroy(transformChildObjects.gameObject);
            }
        }
    }
}