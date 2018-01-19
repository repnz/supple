using Supple.Xml.Collection;
using Supple.Xml.ElementDeserializers;
using Supple.Xml.Exceptions;
using Supple.Xml.NameCreators;
using Supple.Xml.References;
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
            delegator.ElementDeserializers.Add(new ArrayElementDeserializer(delegator));
            delegator.ElementDeserializers.Add(new CollectionElementDeserializer(
                    delegator,
                    delegator
                ));

            delegator.ElementDeserializers.Add(propertiesDeserializer);

            // Value Deserializers
            delegator.ValueDeserializers.Add(new ConvertableDeserializer());
            delegator.ValueDeserializers.Add(new ArrayValueDeserializer(delegator, delegator));
            delegator.ValueDeserializers.Add(new CollectionValueDeserializer(delegator, delegator));
            delegator.ValueDeserializers.Add(new ReferenceVariableDeserializer(referenceStore));

            // Type Name Creators
            delegator.TypeNameCreators.Add(new GenericNameCreator(delegator));
            return delegator;
        }
        
        public T Deserialize<T>(Stream stream)
        {
            XElement element = XElement.Load(stream);
            var delegator = CreateDelegator(_runtimeTypeResolver);
            string expectedElementName = delegator.CreateName(typeof(T));

            if (expectedElementName != element.Name.LocalName)
            {
                throw new UnexpectedElementException(expectedElementName, element);
            }

            return (T)delegator.Deserialize(typeof(T), element);
        }
    }
}
