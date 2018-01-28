## Supple
"bending and moving easily and gracefully; flexible"
## What is Supple?
A .NET Library that allows deserialization of complex XML objects. the main motivation of this library is loading application configuration that allows dependency injection in the configuration file
## Features
- Field / Property Assignment
- Deserialization of interfaces 
- References between deserialized objects
- Calling classes with constructors

## Usage
Say you have this printing application, which has an `IPrinter` interface in it 
``` C#
interface IPrinter
{
    void Print();
}

class StringPrinter : IPrinter
{
    public string StringToWrite { get; }
    
    public StringPrinter(string stringToWrite)
    {
        StringToWrite = stringToWrite;
    }

    public void Print()
    {
        Console.WriteLine(StringToWrite);
    }
}

class FilePrinter : IPrinter
{
    public string FilePath { get;}
    
    public FilePrinter(string filePath)
    {
        FilePath = filePath;
    }

    public void Print()
    {
        Console.WriteLine(File.ReadAllText(FilePath));
    }
}

class PrintApp 
{ 
    public void Run(List<IPrinter> printers)
    {
        foreach (var printer in printers) 
        {
            printer.Print();
        }
    }
}
```

List<IPrinter> can be deserialized from this configuration file:

``` XML
<ListOfPrinter>
    <Printer Type="FilePrinter" FilePath="C:\FileToPrint.txt"/>
    <Printer Type="StringPrinter" StringToPrint="I Love Flexibility"/>
</ListOfPrinter>
```

And run it via this code

``` C#
// Specify runtime types
StaticTypeResolver resolver = new StaticTypeResolver();
resolver.AddType<FilePrinter>();
resolver.AddType<StringPrinter>();

// Create Deserializer 
ISuppleDeserializer deserializer = new SuppleXmlDeserializer(resolver);

// Deserialize printers
var printers = deserializer.Deserialize<List<IPrinter>>(xml);

PrintApp.Run(printers);
```

## Backlog
- Provide API for custom user deserialization
- Implement DynamicTypeResolver
