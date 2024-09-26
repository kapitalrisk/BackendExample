using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExamples
{
    public class Misc
    {
        private class ExampleClass
        {
            private readonly int _value = 0; // default value if not set by a ctor
            public int Value { get => _value; }

            public ExampleClass(int value)
            {
                _value = value;
            }
        }

        public void Variables()
        {
            // Everything in C# is an object (or a struct)
            int a = 1;
            object b = a; // boxing
            int c = (int)b; // unboxing
            var explCls = new ExampleClass(45);
            var explClsAsObj = explCls as object; // explClsAsObj is an object but can be cast back to ExampleClass to find back the value of explCls.Value

            // There is only one compiler in C# and it ensure type safety EVERYWHERE, so "var" is evaluated and check at compilation time
            var d = 10; // d will be an int by default

            // Since everything at least implement "object" everything have base methods like ".Equals()", ".GetType()", ".ToString()" etc
            var e = d.ToString(); // e will be of type string and have the value "10" in it

            // You can also make ANY variable type nullable with "?"
            string? test = null;
            test = "foo"; // obviously "test" type did not changed but we tell the compiler that it should put warning whenever possible about test being potentially null
            var substr = default(string);

            // So this here would be correct
            if (test != null)
                substr = test.Substring(0, test.Length - 1);

            // But there is lighter way to write it
            // This following line does exactly the same
            substr = test?.Substring(0, test.Length - 1);

            // Naturally since everything is an object you can still have a variable not declared as "potentially nullable" that contain null
            string nullStr = null; // But compiler will throw warnings especially if you try to use it after declaration

            // There is also "coallescing" just like in sql on nullable variables
            substr = test ?? b?.ToString() ?? "defaultStr";

            // default keyword can be used on any type
            var toast = default(int); // toast will be "0"
        }

        public void Loops()
        {
            var collection = new List<string>() { "foo", "bar", "baz" }; // internally a list is an array with some misc methods like "Count" to have the size of it

            // All the following will do the same thing

            // 1
            for (int i = 0; i < collection.Count; i++)
                Console.WriteLine(collection[i]);

            // 2
            foreach (var item in collection)
                Console.WriteLine(item);

            // 3
            var collectionEnumerator = collection.GetEnumerator();

            while (collectionEnumerator.MoveNext())
                Console.WriteLine(collectionEnumerator.Current);

            // 4
            collection.ForEach(x => Console.WriteLine(x));
        }

        public void OutVarExempleAndDiscard()
        {
            var str1 = "foo";
            var str2 = default(string);

            if (ExtractFirstCharOfString(str1, out var firstLetterOfStr))
                Console.WriteLine($"str1 first letter is {firstLetterOfStr}");
            else
                Console.WriteLine($"str1 is null or empty");

            // You can discard out parameters if not needed
            if (ExtractFirstCharOfString(str1, out _))
                Console.WriteLine($"str1 does have a valid char at first position");

            // discard can be used also on returned values
            var result = Mul(10, 2); // result will contain 20
            _ = Mul(5, 3); // returned value of "Mul" is discarded
        }

        private bool ExtractFirstCharOfString(string str, out char? firstLetter)
        {
            firstLetter = null;

            if (String.IsNullOrWhiteSpace(str))
                return false;

            firstLetter = str[0];
            return true;
        }

        private int Mul(int a, int b) => a * b;
    }
}
