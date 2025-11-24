using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface IDictionary<K, E>
{
    E remove(K key);
    bool Add(K key, E value);
}

namespace SchijfVanVijf
{
    public class Recipe
    {
        public Recipe()
        {
            Dictionary<Ingredients,int> recept = new Dictionary<Ingredients,int> ();
            //Recipe r;
            //string Instructions;
        }

    }
}

