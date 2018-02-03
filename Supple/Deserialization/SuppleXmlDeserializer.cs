using Supple.Deserialization.Context;
using Supple.Deserialization.Context.Handlers.Collection;
using Supple.Deserialization.Context.Handlers.MemberAssign;
using Supple.Deserialization.Context.Handlers.References;
using Supple.Deserialization.Deserializers;
using Supple.Deserialization.Deserializers.Collection;
using Supple.Deserialization.Exceptions;
using Supple.Deserialization.NameCreators;
using Supple.Deserialization.References;
using Supple.Deserialization.Xml;
using Supple.TypeResolvers;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Supple.Deserialization
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
            var referenceStore = new ReferenceStore();

            // First check if the node is an interface
            delegator.NodeDeserializers.Add(new InterfaceDeserializer(resolver, delegator));
            delegator.NodeDeserializers.Add(new ArrayNodeDeserializer(delegator));

            // Value Deserializers
            delegator.NodeDeserializers.Add(new ArrayValueDeserializer(delegator));
            delegator.NodeDeserializers.Add(new ConvertableDeserializer());
            delegator.NodeDeserializers.Add(new CollectionValueDeserializer(delegator));
            delegator.NodeDeserializers.Add(new ReferenceVariableDeserializer(referenceStore));

            // then check if the node is an array

            // if not, handle if with the context handlers
            delegator.NodeDeserializers.Add(
                    new ContextNodeDeserializer(
                            new List<INodeHandlerFactory>()
                            {
                                new ReferenceAddingHandlerFactory(referenceStore),
                                new CollectionNodeHandlerFactory(delegator),
                                new MemberNodeHandlerFactory(delegator)
                            },
                            delegator
                        )
                );

            // Type Name Creators
            delegator.TypeNameCreators.Add(new KeyValuePairNameCreator());
            delegator.TypeNameCreators.Add(new GenericNameCreator(delegator));
            return delegator;
        }
        
        private T Deserialize<T>(XElement rootElement)
        {
            var delegator = CreateDelegator(_runtimeTypeResolver);
            string expectedElementName = delegator.CreateName(typeof(T));

            Node node = new ElementNode(rootElement);

            if (expectedElementName != rootElement.Name.LocalName)
            {
                throw new UnexpectedNodeException(expectedElementName, node);
            }

            return (T)delegator.Deserialize(typeof(T), node);
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
