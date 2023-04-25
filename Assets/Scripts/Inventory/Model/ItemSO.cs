using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public abstract class ItemSO : ScriptableObject
    {
        [field: SerializeField]
        public bool IsStackable { get; set; }

        public int ID => GetInstanceID();

        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1;

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea(3,10)]
        public string Description { get; set; }

        [field: SerializeField]
        public Sprite ItemImage { get; set; }

        [field: SerializeField]
        public Mesh ItemMesh { get; set; }
        [field: SerializeField]
        public Material Material { get; set; }
        [field: SerializeField]
        public List<ItemParameter> DefaultParametersList { get; set; }


    }

    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public ItemParameterSO itemParameter;
        public float value;

        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}
