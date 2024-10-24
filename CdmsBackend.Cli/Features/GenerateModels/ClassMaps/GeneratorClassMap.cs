namespace CdmsBackend.Cli.Features.GenerateModels.ClassMaps
{
    internal class GeneratorClassMap
    {
        private static readonly Dictionary<string, GeneratorClassMap> classMaps = new Dictionary<string, GeneratorClassMap>();

        public GeneratorClassMap(string className, Action<GeneratorClassMap> classMapInitializer)
        {
            Name = className;
            SourceClassName = className;
            InternalClassName = className;
            classMapInitializer(this);
        }

        public string Name { get; set; }

        public string SourceClassName { get; private set; }

        public string InternalClassName { get; private set; }

        public bool IgnoreInternalClass { get; private set; }

        public List<PropertyMap> Properties { get; private set; } = new List<PropertyMap>();

        public GeneratorClassMap SetClassName(string className)
        {
            SetSourceClassName(className);
            SetInternalClassName(className);
            return this;
        }

        public GeneratorClassMap NoInternalClass()
        {
            IgnoreInternalClass = true;
            return this;
        }

        public GeneratorClassMap SetSourceClassName(string className)
        {
            if (string.IsNullOrEmpty(className))
            {
                throw new ArgumentNullException("className");
            }

            SourceClassName = className;
            return this;
        }

        public GeneratorClassMap SetInternalClassName(string className)
        {
            if (string.IsNullOrEmpty(className))
            {
                throw new ArgumentNullException("className");
            }

            InternalClassName = className;
            return this;
        }

        public PropertyMap MapProperty(string propertyName)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }
            var propertyMap = new PropertyMap(propertyName);
            Properties.Add(propertyMap);
            return propertyMap;
        }

        public static GeneratorClassMap RegisterClassMap(string name, Action<GeneratorClassMap> classMapInitializer)
        {
            var classMap = new GeneratorClassMap(name, classMapInitializer);
            classMaps.Add(classMap.Name, classMap);
            return classMap;
        }

        public static GeneratorClassMap LookupClassMap(string name)
        {
            classMaps.TryGetValue(name, out var classMap);
            return classMap;
        }
    }
}


