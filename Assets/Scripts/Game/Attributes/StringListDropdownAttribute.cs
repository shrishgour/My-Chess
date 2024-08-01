using System;
using UnityEngine;

namespace Game.Attribute
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class StringListDropdownAttribute : PropertyAttribute
    {
        public string className;

        public StringListDropdownAttribute(string className)
        {
            this.className = className;
        }
    }
}