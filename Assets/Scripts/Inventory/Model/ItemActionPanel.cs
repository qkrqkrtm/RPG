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

        // val �Ű������� true�� �Ǹ�, ��ư�� �����ϰ�, ���ο� ���� ������Ʈ Ȱ��ȭ. 
        public void Toggle(bool val)
        {
            if (val == true)
                RemoveOldButtons();
            gameObject.SetActive(val);
        }

        // ��� ���ӿ�����Ʈ�� Transform �Ӽ��� ������ �����Ƿ� Transform�� �̿��Ͽ� ��� ���ӿ�����Ʈ ����
        public void RemoveOldButtons()
        {
            foreach (Transform transformChildObjects in transform)
            {
                Destroy(transformChildObjects.gameObject);
            }
        }
    }
}