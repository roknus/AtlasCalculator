using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

using UnityEngine;

namespace Serializer
{
    public class XMLGodForm : XMLNodeWithCost
    {
        public XMLGodForm()
        {

        }

        public XMLGodForm(GodForm node)
            : base(node)
        {

        }
    }
}