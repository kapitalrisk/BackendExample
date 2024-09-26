using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExamples
{
    internal class LinqBasics
    {
        private List<string> _strings = new List<string> { "hello", "world", "foo", "bar" };

        public void LinqExamples()
        {
            // Here shortStrings is IEnumerable, which means its not already computed
            IEnumerable<string> shortStrings = _strings.Where(x => x.Length < 4).OrderBy(x => x.Length);

            // Calling to list will compute all the resutls of the shortStrings "pipeline" and put it in a newly instanciated list
            var shortStringsList = shortStrings.ToList(); // will create a new list with foo & bar inside

            // However doing for instance a loop on shortStrings pipeline will evaluate it without persisting anything
            foreach (var shortString in shortStrings)
                Console.WriteLine(shortString); // will display foo and bar without creating a new list in memory

            // All Linq queries take advantage of the "yield" keyword where a result is returned only if the expression is evaluated

            // However beware that since non evaluated Linq pipelines (so no "ToList" called on them) are just that, pipelines, if the base collection evolves it will also reflect that change and may lead to unexpected behavior
            var longWordQuery = _strings.Where(x => x.Length > 4); // create the linq query, its just a filtering pipeline
            _strings.Add("baboushka"); // sneak a new element in the _strings list

            foreach (var longWord in longWordQuery)
                Console.WriteLine(longWord); // will display hello & world & baboushka

            // If you need a concrete list with the result at the time T obviously you will call ToList on "longWordQuery" before any new element is sneak in _strings list

            // You can also perform subqueries
            var subQueryExample = _strings.Where(x => x.Where(y => y == 'o').Count() > 0); // will return "hello, world, foo, baboushka", obviously there is more elegant ways to do it

            // Can also be usefull to one-line some changes on a collection
            var stringFirstLetters = _strings.Select(x => x[0]); // will return a char collection with [ 'h', 'w', 'f', 'b', 'b' ] in it
        }
    }
}
