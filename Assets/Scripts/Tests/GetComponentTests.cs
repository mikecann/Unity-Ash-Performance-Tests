using Assets.Scripts.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Tests
{
    public class GetComponentTests : MonoBehaviour
    {
        public int iterations = 10000000;
        public int repeats = 3;

        public GameObject noComponents;
        public GameObject manyComponents;
        public List<Component> components;

        public void TestAll()
        {
            components = new List<Component>();
            TestHelpers.Execute(iterations, repeats, "Empty Function", TestEmpty);
            TestHelpers.Execute(iterations, repeats, "Get Components With No Components", NoComponentsGetComponents);
            TestHelpers.Execute(iterations, repeats, "Get Components With Many Components", ManyComponentsGetComponents);
            TestHelpers.Execute(iterations, repeats, "Get Components With No Components List", NoComponentsGetComponentsList);
            TestHelpers.Execute(iterations, repeats, "Get Components With Many Components List", ManyComponentsGetComponentsList);

            Debug.Log("components " + components.Count);
        }        

        public void TestEmpty()
        {
        }

        public void NoComponentsGetComponents()
        {
            noComponents.GetComponents<Component>();
        }

        public void ManyComponentsGetComponents()
        {
            manyComponents.GetComponents<Component>();
        }

        public void NoComponentsGetComponentsList()
        {
            noComponents.GetComponents<Component>(components);
        }

        public void ManyComponentsGetComponentsList()
        {     
            manyComponents.GetComponents<Component>(components);
        }
    }
}
