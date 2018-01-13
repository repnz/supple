using Supple.Xml.ElementDeserializers;
using Supple.Xml.List;
using Supple.Xml.NameCreators;
using Supple.Xml.References;
using System;
using System.IO;
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

            delegator.ElementDeserializers.Add(new ListElementDeserializer(
                    delegator,
                    delegator
                ));

            delegator.ElementDeserializers.Add(propertiesDeserializer);

            // Value Deserializers
            delegator.ValueDeserializers.Add(new ReferenceVariableDeserializer(referenceStore));
            delegator.ValueDeserializers.Add(new ConvertableDeserializer());
            delegator.ValueDeserializers.Add(new ListValueDeserializer(delegator));

            // Type Name Creators
            delegator.TypeNameCreators.Add(new GenericNameCreator(delegator));
            delegator.TypeNameCreators.Add(new InterfaceNameCreator());
            delegator.TypeNameCreators.Add(new DefaultTypeNameCreator());

            return delegator;
        }
        
        public T Deserialize<T>(Stream stream)
        {
            XElement element = XElement.Load(stream);
            var delegator = CreateDelegator(_runtimeTypeResolver);
            
            if (delegator.CreateName(typeof(T)) != element.Name.LocalName)
            {
                throw new InvalidOperationException();
            }

            return (T)delegator.Deserialize(typeof(T), element);
        }
    }
}
