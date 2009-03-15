﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Concordion.Api;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace Concordion.Internal
{
    public class XmlSpecificationReader : ISpecificationReader
    {
        #region Properties
        
        private ISource Source
        {
            get;
            set;
        }

        private DocumentParser DocumentParser
        {
            get;
            set;
        }

        private Uri Location
        {
            get;
            set;
        }

        #endregion

        #region Constructors
        
        public XmlSpecificationReader(ISource source, DocumentParser documentParser)
        {
            Source = source;
            DocumentParser = documentParser;
        }

        #endregion

        #region ISpecificationReader Members

        public ISpecification ReadSpecification(Resource resource)
        {
            XDocument document = XDocument.Load(XmlReader.Create(resource.ResourceUri.AbsoluteUri));
            return DocumentParser.Parse(document, resource);
        }

        #endregion
    }
}
