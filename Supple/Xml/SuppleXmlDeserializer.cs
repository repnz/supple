﻿using Supple.Xml.Collection;
using Supple.Xml.ElementDeserializers;
using Supple.Xml.Exceptions;
using Supple.Xml.NameCreators;
using Supple.Xml.References;
using Supple.Xml.ValueDeserializers;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Supple.Xml
{
    public class SuppleXmlDeserializer : ISuppleDeserializer
    {
        private readonly IRuntimeTypeResolver _runtimeTypeResolver;

        public SuppleXmlDeserializer(IRuntimeTypeResolver runtimeTypeResolver)
        {
            _runtimeTypeResolver = runtimeTypeResolver;
        }

        public SuppleXmlDeserializer() : this(new StaticTypeResolver())
        {
        }

        private DelegatorDeserializer CreateDelegator(IRuntimeTypeResolver resolver)
        {
            DelegatorDeserializer delegator = new DelegatorDeserializer();

            var interfaceDeserializer = new InterfaceDeserializer(resolver, delegator);
            var referenceStore = new ReferenceStore();
            var propertiesDeserializer = new ReferenceAddingElementDeserializer(
                delegator,
                delegator,
                referenceStore
                );

            // Elemenet Deserializers
            delegator.ElementDeserializers.Add(interfaceDeserializer);
            delegator.ElementDeserializers.Add(new ElementValueDeserializer(delegator));
            delegator.ElementDeserializers.Add(new ArrayElementDeserializer(delegator));
            delegator.ElementDeserializers.Add(new CollectionElementDeserializer(
                    delegator,
                    delegator
                ));

            delegator.ElementDeserializers.Add(propertiesDeserializer);

            // Value Deserializers
            //delegator.ValueDeserializers.Add(new DateTimeValueDeserializer());
            delegator.ValueDeserializers.Add(new ConvertableDeserializer());
            delegator.ValueDeserializers.Add(new ArrayValueDeserializer(delegator, delegator));
            delegator.ValueDeserializers.Add(new CollectionValueDeserializer(delegator, delegator));
            delegator.ValueDeserializers.Add(new ReferenceVariableDeserializer(referenceStore));

            // Type Name Creators
            delegator.TypeNameCreators.Add(new GenericNameCreator(delegator));
            return delegator;
        }
        
        private T Deserialize<T>(XElement rootElement)
        {
            var delegator = CreateDelegator(_runtimeTypeResolver);
            string expectedElementName = delegator.CreateName(typeof(T));

            if (expectedElementName != rootElement.Name.LocalName)
            {
                throw new UnexpectedElementException(expectedElementName, rootElement);
            }

            return (T)delegator.Deserialize(typeof(T), rootElement);
        }

        public T Deserialize<T>(Stream stream)
        {
            return Deserialize<T>(XElement.Load(stream));
            
        }

        public T Deserialize<T>(string xml)
        {
            using (TextReader textReader = new StringReader(xml))
            {
                return Deserialize<T>(XElement.Load(textReader));
            }
        }
    }
}
