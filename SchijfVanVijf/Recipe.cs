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
public class Recipe
{
    public Recipe()
    {
        Dictionary<Ingredient,int> recept = new Dictionary<Ingredient,int> ();
        Recipe r;
        string Instructions;
    }

}
