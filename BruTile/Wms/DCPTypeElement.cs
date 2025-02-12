// Copyright (c) BruTile developers team. All rights reserved. See License.txt in the project root for license information.

using System.Xml;
using System.Xml.Linq;

namespace BruTile.Wms
{
    public class DCPTypeElement : XmlObject
    {
        private readonly string _name;

        public string Name => _name;

        private OnlineResource _onlineResourceField;

        protected DCPTypeElement(string name)
        {
            _name = name;
        }

        protected DCPTypeElement(string name, XElement node, string @namespace)
            : this(name)
        {
            var element = node.Element(XName.Get("OnlineResource", @namespace));
            if (element != null)
                OnlineResource = new OnlineResource(element);
        }

        public OnlineResource OnlineResource
        {
            get => _onlineResourceField ?? (_onlineResourceField = new OnlineResource());
            set => _onlineResourceField = value;
        }

        #region Overrides of XmlObject

        public override void ReadXml(XmlReader reader)
        {
            if (CheckEmptyNode(reader, _name))

                OnlineResource = new OnlineResource();
            OnlineResource.ReadXml(reader);
            reader.ReadEndElement();
        }

        public override void WriteXml(XmlWriter writer)
        {
            WriteXmlItem(_name, Namespace, writer, OnlineResource);
        }

        public override XElement ToXElement(string @namespace)
        {
            return new XElement(XName.Get(_name, @namespace),
                                OnlineResource.ToXElement(@namespace));
        }

        #endregion Overrides of XmlObject
    }
}
