using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Test : MonoBehaviour
    {

        private void Update()
        {

            var a = Physics.Linecast(new Vector3(18.94f, -3.03f, -0.01f), new Vector3(11.20f, -3.03f, 0.22f), 0);
            var b = Physics.Linecast(new Vector3(18.94f, -3.03f, -0.01f), new Vector3(11.20f, -3.03f, 0.22f), 1);
            var c = Physics.Linecast(new Vector3(18.94f, -3.03f, -0.01f), new Vector3(11.20f, -3.03f, 0.22f), 2);
            var d = Physics.Linecast(new Vector3(18.94f, -3.03f, -0.01f), new Vector3(11.20f, -3.03f, 0.22f), 4);
            Debug.DrawLine(new Vector3(18.94f, -3.03f, -0.01f), new Vector3(11.20f, -3.03f, 0.22f), Color.yellow);
        }
    }
}
